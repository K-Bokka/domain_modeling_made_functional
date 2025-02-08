import Utils.Pipe._
import Helpers._
import Domains._

object Workflows:

  private def validateOrder(order: Any) = ???

  private def priceOrder(order: Any) = ???

  private def acknowledgeOrder(order: Any) = ???

  private def createEvents(order: Any) = ???

  def placeOrder(unvalidatedOrder: Any): Any =
    unvalidatedOrder
      |> validateOrder
      |> priceOrder
      |> acknowledgeOrder
      |> createEvents

  type CheckAddressExists = UnvalidatedAddress => CheckedAddress

  val checkAddressExists: CheckAddressExists = ???

  private type CheckProductCodeExists = ProductCode => Boolean

  private type ValidateOrder =
    CheckProductCodeExists
      => CheckAddressExists
      => UnvalidatedOrder
      => ValidatedOrder

  val validateOrder: ValidateOrder =
    checkProductCodeExists => checkAddressExists => unvalidatedOrder =>
      val toAddressCurried = toAddress(checkAddressExists, _)
      val orderId = unvalidatedOrder.orderId |> OrderId.apply
      val customerInfo = unvalidatedOrder.customerInfo |> toCustomerInfo
      val shippingAddress = unvalidatedOrder.shippingAddress |> toAddressCurried
      val billingAddress = unvalidatedOrder.billingAddress |> toAddressCurried
      val orderLines = for {
        line <- unvalidatedOrder.lines
      } yield line |> toOrderLine
      
      ValidatedOrder(
        orderId = orderId,
        customerInfo = customerInfo,
        shippingAddress = shippingAddress,
        billingAddress = billingAddress,
        lines = orderLines
      )

end Workflows
