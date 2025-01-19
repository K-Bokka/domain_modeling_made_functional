import Chap0702.OrderId

object Chap0702:
  case class OrderId(value: Any)

  case class CustomerInfo(value: Any)

  case class Address(value: Any)

  case class ValidatedOrderLine(value: Any)

  case class ValidatedOrder(
                             orderId: OrderId,
                             customerInfo: CustomerInfo,
                             shippingAddress: Address,
                             billingAddress: Address,
                             orderLines: Seq[ValidatedOrderLine],
                           )

  case class PricedOrderLine(value: Any)

  case class BillingAmount(value: Any)

  case class PricedOrder(
                          orderId: OrderId,
                          customerInfo: CustomerInfo,
                          shippingAddress: Address,
                          billingAddress: Address,
                          orderLines: Seq[PricedOrderLine],
                          amountToBill: BillingAmount,
                        )

  case class UnvalidatedOrder(value: Any)

  enum Order:
    case Unvalidated(value: UnvalidatedOrder)
    case Validated(value: ValidatedOrder)
    case Priced(value: PricedOrder)

object C0702a:
  case class Order(
                    orderId: OrderId,
                    isValidated: Boolean,
                    isPriced: Boolean,
                    AmountToBill: Option[BigDecimal],
                  )
