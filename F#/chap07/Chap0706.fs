namespace Chap0706

type AsyncResult<'success, 'failure> = Async<Result<'success, 'failure>>

type UnvalidatedOrder = Undefined
type ValidatedOrder = Undefined
type ValidationError = Undefined
type PricedOrder = Undefined
type OrderAcknowledgmentSent = Undefined
type PricingError = Undefined
type PlaceOrderEvent = Undefined

type ValidateOrder =
    UnvalidatedOrder // In
        -> AsyncResult<ValidatedOrder, ValidationError> // Out

type PriceOrder =
    ValidatedOrder // In
        -> Result<PricedOrder, PricingError> // Out

type AcknowledgeOrder =
    PricedOrder // In
        -> Async<OrderAcknowledgmentSent option> // Out

type CreateEvents =
    PricedOrder // In
        -> PlaceOrderEvent list // Out
