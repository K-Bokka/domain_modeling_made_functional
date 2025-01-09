// TODO: コンストラクタを隠蔽したり、apply のすり抜けを気にしないといけない感じになってくるなら、opaque の方が良さそう

// 製品コード関連
/// TODO: 先頭が "W" + 数字4桁
case class WidgetCode(value: String)

/// TODO: 先頭が "G" + 数字3桁
case class GizmoCode(value: String)

enum ProductCode:
  case Widget(code: WidgetCode)
  case Gizmo(code: GizmoCode)

// 注文数量関連
case class UnitQuantity(value: Int)

case class KilogramQuantity(value: BigDecimal)

enum OrderQuantity:
  case Unit(value: UnitQuantity)
  case Kilos(value: KilogramQuantity)

// 注文関連の識別子
// TODO: Any は未確定
case class OrderId(value: Any)

case class OrderLineId(value: Any)

case class CustomerId(value: Any)

// 注文関連
// TODO: Any は未確定
case class CustomerInfo(value: Any)

case class ShippingAddress(value: Any)

case class BillingAddress(value: Any)

case class Price(value: Any)

case class BillingAmount(value: Any)

// Order集約
case class OrderLine(
                      id: OrderLineId,
                      orderId: OrderId,
                      productCode: ProductCode,
                      orderQuantity: OrderQuantity,
                      price: Price,
                    )

case class Order(
                  id: OrderId,
                  customerInfo: CustomerInfo,
                  shippingAddress: ShippingAddress,
                  billingAddress: BillingAddress,
                  orderLines: Seq[OrderLine],
                  amountToBill: BillingAmount,
                )

// ワークフローの構成要素
case class UnvalidatedOrderLine(
                                 id: String,
                                 orderId: String,
                                 productCode: String,
                                 orderQuantity: String,
                               )

case class UnvalidatedOrder(
                             id: String,
                             customerInfo: String,
                             shippingAddress: String,
                             billingAddress: String,
                             orderLines: Seq[UnvalidatedOrderLine],
                           )

case class BillableOrderPlaced(
                                orderId: OrderId,
                                billingAddress: BillingAddress,
                                amountToBill: BillingAmount,
                              )

case class PlaceOrderEvents(
                             acknowledgementSent: Boolean,
                             orderPlaced: Order,
                             billableOrderPlaced: BillableOrderPlaced,
                           )

case class ValidationErrors(fieldName: String, errorDescription: String)

case class PlaceOrderError(errors: Seq[ValidationErrors])

// 注文確定プロセス
type PlaceOrder = UnvalidatedOrder => Either[PlaceOrderError, PlaceOrderEvents]
