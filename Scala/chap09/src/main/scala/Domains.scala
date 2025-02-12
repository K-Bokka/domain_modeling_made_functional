import Utils.*
import Utils.Pipe.|>

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

  type CheckedAddress = UnvalidatedAddress

  case class ZipCode(value: String)

  case class Address(
                      addressLine1: String50,
                      addressLine2: Option[String50],
                      addressLine3: Option[String50],
                      addressLine4: Option[String50],
                      city: String50,
                      zipCode: ZipCode,
                    )

  case class WidgetCode(value: String)

  case class GizmoCode(value: String)

  enum ProductCode:
    case Widget(code: WidgetCode)
    case Gizmo(code: GizmoCode)

  object ProductCode:
    def apply(str: String): ProductCode =
      if str.isEmpty then throw new Exception("ProductCode must not be empty")
      else if str.length > 50 then throw new Exception("ProductCode must not be more than 50 characters")
      else if str.startsWith("W") then str |> WidgetCode.apply |> ProductCode.Widget.apply
      else if str.startsWith("G") then str |> GizmoCode.apply |> ProductCode.Gizmo.apply
      else throw new Exception("ProductCode must start with W or G")

  case class EmailAddress(value: String)

  case class PersonalName(fistName: String50, lastName: String50)

  case class CustomerInfo(
                           name: PersonalName,
                           emailAddress: EmailAddress,
                         )

  case class OrderLineId private(value: String)

  object OrderLineId:
    def apply(value: String): OrderLineId =
      if value.isEmpty
      then throw new Exception("OrderLineId must not be empty")
      else if value.length > 50 then throw new Exception("OrderLineId must not be more than 50 characters")
      else new OrderLineId(value)

  case class UnitQuantity(value: Int)

  case class KilogramQuantity(value: BigDecimal)

  enum OrderQuantity:
    case Unit(value: UnitQuantity)
    case Kilos(value: KilogramQuantity)

  object OrderQuantity:
    def value(qty: OrderQuantity): BigDecimal = qty match
      case Unit(u) => u.value |> BigDecimal.apply
      case Kilos(k) => k.value

  case class OrderLine(
                        orderLineId: OrderLineId,
                        productId: ProductCode,
                        quantity: OrderQuantity,
                      )

  case class ValidatedOrder(
                             orderId: OrderId,
                             customerInfo: CustomerInfo,
                             shippingAddress: Address,
                             billingAddress: Address,
                             lines: List[OrderLine],
                           )

  // 価格付き
  case class Price private(value: BigDecimal)

  object Price:
    def apply(value: BigDecimal): Price =
      if 0D < value || value < 1_000_000D then throw new Exception("Price must be between 1,000 and 1,000,000")
      else new Price(value)

    def multiply(quantity: BigDecimal, price: Price) =
      quantity * price.value |> Price.apply


  case class PricedOrderLine(
                              orderLineId: OrderLineId,
                              productId: ProductCode,
                              quantity: OrderQuantity,
                              price: Price,
                            )

  case class BillingAmount private(value: BigDecimal)

  object BillingAmount:
    def apply(value: BigDecimal): BillingAmount =
      if 0D <= value || value < 1_000_000D then throw new Exception("BillingAmount must be between 1,000 and 1,000,000")
      else new BillingAmount(value)

    def sumPrices(lines: List[Price]): BillingAmount =
      lines.map(_.value).sum |> BillingAmount.apply

  end BillingAmount

  case class PricedOrder(
                          orderId: OrderId,
                          customerInfo: CustomerInfo,
                          shippingAddress: Address,
                          billingAddress: Address,
                          lines: List[PricedOrderLine],
                          amountToBill: BillingAmount,
                        )

  // 確認ステップ
  case class HtmlString(value: String)

  case class OrderAcknowledgement(
                                   emailAddress: EmailAddress,
                                   letter: HtmlString,
                                 )

  enum SendResult:
    case Sent
    case NotSent

  case class OrderAcknowledgmentSent(
                                      orderId: OrderId,
                                      emailAddress: EmailAddress,
                                    )

  type OrderPlaced = PricedOrder

  case class BillableOrderPlaced(
                                  orderId: OrderId,
                                  billingAddress: Address,
                                  amountToBill: BillingAmount,
                                )

  enum PlaceOrderEvent:
    case Placed(value: OrderPlaced)
    case BillablePlaced(value: BillableOrderPlaced)
    case AcknowledgmentSent(value: OrderAcknowledgmentSent)

end Domains
