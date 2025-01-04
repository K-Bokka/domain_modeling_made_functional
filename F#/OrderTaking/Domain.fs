namespace OrderTaking.Domain

// 製品コード関連
/// TODO: 制約: 先頭が "W" + 数字4桁
type WidgetCode = WidgetCode of string
/// TODO: 制約: 先頭が "W" + 数字4桁
type GizmoCode = GizmoCode of string

type ProductCode =
    | Widget of WidgetCode
    | Gizmo of GizmoCode

// 注文数量関連
type UnitQuantity = UnitQuantity of int
type KilogramQuantity = KilogramQuantity of decimal

type OrderQuantity =
    | Unit of UnitQuantity
    | Kilos of KilogramQuantity

// 注文関連の識別子
type OrderId = Undefined
type OrderLineId = Undefined
type CustomerId = Undefined

// 注文関連
type CustomerInfo = Undefined
type ShippingAddress = Undefined
type BillingAddress = Undefined
type Price = Undefined
type BillingAmount = Undefined

/// Order集約のルート
type Order =
    { Id: OrderId
      CustomerId: CustomerId
      ShippingAddress: ShippingAddress
      BillingAddress: BillingAddress
      OrderLines: OrderLine list
      AmountToBill: BillingAmount }

and OrderLine =
    { id: OrderLineId
      OrderId: OrderId
      ProductCode: ProductCode
      OrderQuantity: OrderQuantity
      Price: Price }

// ワークフローの構成要素
type UnvalidatedOrder =
    { OrderId: string
      CustomerInfo: string
      ShippingAddress: string
      BillingAddress: string
      OrderLines: UnvalidatedOrderLine list }

and UnvalidatedOrderLine =
    { ProductCode: string
      OrderQuantity: string }

type PlaceOrderEvents =
    { AcknowledgmentSent: bool
      OrderPlaced: Order
      BillableOrderPlaced: BillableOrderPlaced }

and BillableOrderPlaced =
    { OrderId: OrderId
      BillingAddress: BillingAddress
      AmountToBill: BillingAmount }

type PlaceOrderError = ValidationError of ValidationError list

and ValidationError =
    { FieldName: string
      ErrorDescription: string }

// 注文確定プロセス
type PlaceOrder = UnvalidatedOrder -> Result<PlaceOrderEvents, PlaceOrderError>
