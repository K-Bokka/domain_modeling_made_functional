namespace Chap0505

open Chap0504
open Microsoft.FSharp.Core

type UnvalidatedOrder = Undefined
type ValidatedOrder = Undefined

type AcknowledgmentSent = Undefined
type OrderPlaced = Undefined
type BillableOrderPlaced = Undefined

type QuoteForm = Undefined
type OrderForm = Undefined
type ProductCatalog = Undefined
type PricedOrder = Undefined
type EnvelopeContents = EnvelopeContents of string

type ValidationError =
    { FieldName: string
      ErrorDescription: string }

module C050501 =
    type ValidateOrder = UnvalidatedOrder -> ValidatedOrder

    type PlaceOrderEvents =
        { AcknowledgmentSent: AcknowledgmentSent
          OrderPlaced: OrderPlaced
          BillableOrderPlaced: BillableOrderPlaced }

    type PlaceOrder = UnvalidatedOrder -> PlaceOrderEvents

    type CategorizedMail =
        | Quote of QuoteForm
        | Order of OrderForm

    type CategorizeInboundMail = EnvelopeContents -> CategorizedMail

    type CalculatePrices = OrderForm -> ProductCatalog -> PricedOrder

module C050501a =
    type CalculatedPricesInput =
        { OrderForm: OrderForm
          ProductCatalog: ProductCatalog }

    type CalculatePrices = CalculatedPricesInput -> PricedOrder

module C050502 =
    type ValidateOrder = UnvalidatedOrder -> Result<ValidatedOrder, ValidationError list>

module C050502a =
    type ValidateOrder = UnvalidatedOrder -> Async<Result<ValidatedOrder, ValidationError list>>

module C050502b =
    type ValidationResponse<'a> = Async<Result<'a, ValidationError list>>
    type ValidateOrder = UnvalidatedOrder -> ValidationResponse<ValidatedOrder>
