namespace Chap0504

open Chap0503.C050301

type Undefined = exn
type CustomerInfo = Undefined
type ShippingAddress = Undefined
type BillingAddress = Undefined
type OrderLine = Undefined
type BillingAmount = Undefined

type Order =
    { CustomerInfo: CustomerInfo
      ShippingAddress: ShippingAddress
      BillingAddress: BillingAddress
      OrderLines: OrderLine list
      AmountToBill: BillingAmount }

type GizmoCode = Undefined

type ProductCode =
    | Widget of WidgetCode
    | Gizmo of GizmoCode

type OrderQuantity =
    | Unit of UnitQuantity
    | Kilogram of KilogramQuantity
