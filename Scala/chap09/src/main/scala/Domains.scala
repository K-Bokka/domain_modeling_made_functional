import Utils.*

object Domains:
  // 未検証
  case class UnvalidatedAddress(
                                 addressLine1: String,
                                 addressLine2: String,
                                 addressLine3: String,
                                 addressLine4: String,
                                 city: String,
                                 zipCode: String,
                               )

  case class UnvalidatedCustomerInfo(
                                      firstName: String,
                                      lastName: String,
                                      emailAddress: String,
                                    )

  case class UnvalidatedOrderLine(
                                   orderLineId: String,
                                   productCode: String,
                                   quantity: BigDecimal,
                                 )

  case class UnvalidatedOrder(
                               orderId: String,
                               customerInfo: UnvalidatedCustomerInfo,
                               shippingAddress: UnvalidatedAddress,
                               billingAddress: UnvalidatedAddress,
                               lines: List[UnvalidatedOrderLine],
                             )

  // 検証済み
  case class OrderId private(value: String)

  object OrderId:
    def apply(value: String): OrderId =
      if value.isEmpty
      then throw new Exception("OrderId must not be empty")
      else if value.length > 50 then throw new Exception("OrderId must not be more than 50 characters")
      else new OrderId(value)

  case class CheckedAddress(value: String)

  case class ProductCode(value: String)

  case class EmailAddress(value: String)

  case class PersonalName(fistName: String50, lastName: String50)

  case class CustomerInfo(
                           name: PersonalName,
                           emailAddress: EmailAddress,
                         )

  case class OrderLine(orderLineId: String50)

  case class ValidatedOrder(
                             orderId: OrderId,
                             customerInfo: CustomerInfo,
                             shippingAddress: CheckedAddress,
                             billingAddress: CheckedAddress,
                             lines: List[OrderLine],
                           )

end Domains
