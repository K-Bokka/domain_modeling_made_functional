namespace Domain

open System

// 入力データ
type UnvalidatedOrder =
    { OrderId: string
      CustomerInfo: UnvalidatedCustomer
      ShippingAddress: UnvalidatedAddress }

and UnvalidatedCustomer = { Name: string; Email: string }
and UnvalidatedAddress = UnvalidatedAddress of string

// 入力コマンド
type Command<'data> =
    { Data: 'data
      Timestamp: DateTime
      UserId: string }

type PlaceOrderCommand = Command<UnvalidatedOrder>

// パブリックAPI
type OrderPlaced = Undefined
type BillableOrderPlaced = Undefined
type AcknowledgmentSent = Undefined

type PlaceOrderEvent =
    | OrderPlaced of OrderPlaced
    | BillableOrderPlaced of BillableOrderPlaced
    | AcknowledgmentSent of AcknowledgmentSent

type PlaceOrderError = Undefined
type AsyncResult<'success, 'failure> = Async<Result<'success, 'failure>>

type PlaceOrderWorkflow =
    PlaceOrderCommand // 入力コマンド
        -> AsyncResult<PlaceOrderEvent list, PlaceOrderError>
