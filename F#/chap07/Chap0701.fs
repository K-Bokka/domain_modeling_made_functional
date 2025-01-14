namespace Chap0701

open System

type UnvalidatedCustomerInfo = Undefined
type UnvalidatedAddress = Undefined

type UnvalidatedOrder =
    { OrderId: string
      CustomerInfo: UnvalidatedCustomerInfo
      ShippingAddress: UnvalidatedAddress }

module C070101 =
    type PlaceOrder =
        { OrderForm: UnvalidatedOrder
          Timestamp: DateTime
          UserId: string }

type Command<'data> =
    { Data: 'data
      Timestamp: DateTime
      UserId: string }

type PlaceOrder = Command<UnvalidatedOrder>

type ChangeOrder = Undefined
type CancelOrder = Undefined

type OrderTakingCommand =
    | Place of PlaceOrder
    | Change of ChangeOrder
    | Cancel of CancelOrder
