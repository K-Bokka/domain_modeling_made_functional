namespace chap0702

open Chap0701

type OrderId = Undefined

module C0702a =
    type Order =
        { OrderId: OrderId
          IsValidated: bool
          IsPriced: bool
          AmountToBill: decimal option }

type CustomerInfo = Undefined
type Address = Undefined
type ValidatedOrderLine = Undefined

type ValidatedOrder =
    { OrderId: OrderId
      CustomerInfo: CustomerInfo
      BillingAddress: Address
      ShippingAddress: Address
      OrderLines: ValidatedOrderLine list }

type PricedOrderLine = Undefined
type BillingAmount = Undefined

type PricedOrder =
    { OrderId: OrderId
      CustomerInfo: CustomerInfo
      BillingAddress: Address
      ShippingAddress: Address
      OrderLines: PricedOrderLine list
      AmountToBill: BillingAmount }

type Order =
    | Unvalidated of UnvalidatedOrder
    | Validated of ValidatedOrder
    | Priced of PricedOrder
