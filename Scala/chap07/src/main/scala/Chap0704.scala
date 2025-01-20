object Chap070401:
  case class ProductCode(value: Any)

  case class ValidatedOrder(value: Any)

  type CheckProductCodeExists = ProductCode => Boolean


  case class UnvalidatedAddress(value: Any)

  case class CheckedAddress(value: UnvalidatedAddress)

  case class AddressValidationError(value: String)

  type CheckAddressExists = UnvalidatedAddress => Either[AddressValidationError, CheckedAddress]

  case class ValidationError(value: Any)

  case class UnvalidatedOrder(value: Any)

  // 書けるけど、こういうやり方はしないような気がする
  // 他にいい方法はあるだろうか...
  // trait 辺りを使うとか
  type ValidateOrder =
    CheckAddressExists // 依存
      => CheckAddressExists // 依存
      => UnvalidatedOrder // 入力
      => Either[ValidationError, ValidatedOrder] // 出力

object Chap070402:

  import Chap070401.*

  case class Price(value: Any)

  case class PricedOrder(value: Any)

  type GetProductPrice = ProductCode => Price

  type PriceOrder = GetProductPrice => ValidateOrder => PricedOrder

object Chap070403:

  import Chap070402.*

  case class EmailAddress(value: Any)

  case class HtmlString(value: String)

  case class OrderAcknowledgment(emailAddress: EmailAddress, letter: HtmlString)

  type CreateOrderAcknowledgmentLetter = PricedOrder => HtmlString

  type SenOrderAcknowledgmentRetVolt = OrderAcknowledgment => Unit

  type SendOrderAcknowledgmentRetBool = OrderAcknowledgment => Boolean

  enum SendResult:
    case Sent
    case NotSent

  type SendOrderAcknowledgmentRetRes = OrderAcknowledgment => SendResult

  case class OrderId(value: Any)

  case class OrderAcknowledgmentSent(orderId: OrderId, emailAddress: EmailAddress)

  type SendOrderAcknowledgementRetEvent = OrderAcknowledgmentSent => Option[OrderAcknowledgmentSent]

  type AcknowledgeOrder =
    CreateOrderAcknowledgmentLetter // 依存
      => SendOrderAcknowledgmentRetRes // 依存
      => PricedOrder // 入力
      => Option[OrderAcknowledgmentSent] // 出力

object Chap070404:

  import Chap0702.PricedOrder
  import Chap070403.{OrderId, OrderAcknowledgmentSent}

  case class Address(value: Any)

  case class BillingAmount(value: Any)

  case class OrderPlaced(value: PricedOrder)

  case class BillableOrderPlaced(orderId: OrderId, billingAddress: Address, amountToBill: BillingAmount)

  case class PlaceOrderResult(
                               orderPlaced: OrderPlaced,
                               billableOrderPlaced: BillableOrderPlaced,
                               orderAcknowledgmentSent: Option[OrderAcknowledgmentSent],
                             )

  enum PlaceOrderEvent:
    case Placed(value: OrderPlaced)
    case BillablePlaced(value: BillableOrderPlaced)
    case AcknowledgmentSent(value: OrderAcknowledgmentSent)

  type CreateEvents = PricedOrder => List[PlaceOrderEvent]