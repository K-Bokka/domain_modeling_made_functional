import Utils.Pipe.{|>, *}
import Helpers.*
import Domains.*

object Workflows:

//  private def validateOrder(order: Any) = ???
//
//  private def priceOrder(order: Any) = ???
//
//  private def acknowledgeOrder(order: Any) = ???
//
//  private def createEvents(order: Any) = ???
//
//  def placeOrder(unvalidatedOrder: Any): Any =
//    unvalidatedOrder
//      |> validateOrder
//      |> priceOrder
//      |> acknowledgeOrder
//      |> createEvents

  type CheckAddressExists = UnvalidatedAddress => CheckedAddress

  private val checkAddressExists: CheckAddressExists = ???

  type CheckProductCodeExists = ProductCode => Boolean

  private val checkProductCodeExists: CheckProductCodeExists = ???

  private type ValidateOrder =
    CheckProductCodeExists
      => CheckAddressExists
      => UnvalidatedOrder
      => ValidatedOrder

  val validateOrder: ValidateOrder =
    checkProductCodeExists => checkAddressExists => unvalidatedOrder =>
      val toAddressCurried = toAddress(checkAddressExists, _)
      val toOrderLineCurried = toOrderLine(checkProductCodeExists, _)
      val orderId = unvalidatedOrder.orderId |> OrderId.apply
      val customerInfo = unvalidatedOrder.customerInfo |> toCustomerInfo
      val shippingAddress = unvalidatedOrder.shippingAddress |> toAddressCurried
      val billingAddress = unvalidatedOrder.billingAddress |> toAddressCurried
      val orderLines = for {
        line <- unvalidatedOrder.lines
      } yield line |> toOrderLineCurried

      ValidatedOrder(
        orderId = orderId,
        customerInfo = customerInfo,
        shippingAddress = shippingAddress,
        billingAddress = billingAddress,
        lines = orderLines
      )

  type GetProductPrice = ProductCode => Price

  private val getProductPrice: GetProductPrice = ???

  type PriceOrder =
    GetProductPrice // 依存
      => ValidatedOrder // In
      => PricedOrder // Out

  val priceOrder: PriceOrder = getProductPrice => validatedOrder =>
    val lines = for {
      line <- validatedOrder.lines
    } yield {
      val toPricedOrderLineCurried = toPricedOrderLine(getProductPrice, _)
      line |> toPricedOrderLineCurried
    }
    val amountToBill = lines.map(_.price) |> BillingAmount.sumPrices

    PricedOrder(
      validatedOrder.orderId,
      validatedOrder.customerInfo,
      validatedOrder.shippingAddress,
      validatedOrder.billingAddress,
      lines,
      amountToBill
    )

  type CreateOrderAcknowledgementLetter = PricedOrder => HtmlString

  private val createAcknowledgmentLetter: CreateOrderAcknowledgementLetter = ???

  type SendOrderAcknowledgement = OrderAcknowledgement => SendResult

  private val sendAcknowledgment: SendOrderAcknowledgement = ???

  type AcknowledgeOrder =
    CreateOrderAcknowledgementLetter // 依存
      => SendOrderAcknowledgement // 依存
      => PricedOrder // In
      => Option[OrderAcknowledgmentSent] // Out

  val acknowledgeOrder: AcknowledgeOrder =
    createOrderAcknowledgementLetter => sendOrderAcknowledgement => pricedOrder =>
    val letter = createOrderAcknowledgementLetter(pricedOrder)
    val acknowledgement = OrderAcknowledgement(
      emailAddress = pricedOrder.customerInfo.emailAddress,
      letter = letter,
    )
    sendOrderAcknowledgement(acknowledgement) match
      case SendResult.Sent =>
        val event = OrderAcknowledgmentSent(
          orderId = pricedOrder.orderId,
          emailAddress = pricedOrder.customerInfo.emailAddress,
        )
        Some(event)
      case SendResult.NotSent => None

  type CreateEvents =
    PricedOrder // In
      => Option[OrderAcknowledgmentSent] // In
      => List[PlaceOrderEvent] // Out

  val createEvents: CreateEvents =
    pricedOrder => acknowledgementEventOpt =>
      val event1 = pricedOrder |> PlaceOrderEvent.Placed.apply |> Nil.::
      val event2 = acknowledgementEventOpt.map(PlaceOrderEvent.AcknowledgmentSent(_)).toList
      val event3 = for {
       event <- pricedOrder |> createBillingEvent
      } yield event |> PlaceOrderEvent.BillablePlaced.apply

      List(
        event1,
        event2,
        event3.toList,
      ).flatten

  type PlaceOrder = UnvalidatedOrder => List[PlaceOrderEvent]

  val placeOrder : PlaceOrder =
    val validateOrderCurried = validateOrder(checkProductCodeExists)(checkAddressExists)
    val priceOrderCurried = priceOrder(getProductPrice)
    val acknowledgeOrderCurried = acknowledgeOrder(createAcknowledgmentLetter)(sendAcknowledgment)
    unvalidatedOrder =>
        val validatedOrder = unvalidatedOrder |> validateOrderCurried
        val pricedOrder = validatedOrder |> priceOrderCurried
        val acknowledgementOpt = pricedOrder |> acknowledgeOrderCurried
        val events = createEvents(pricedOrder)(acknowledgementOpt)
        events


end Workflows
