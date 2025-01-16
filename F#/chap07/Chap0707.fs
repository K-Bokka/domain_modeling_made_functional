namespace Chap0707

open Chap0705.C070502
open Chap0706

type AsyncResult<'success, 'failure> = Async<Result<'success, 'failure>>

type CheckProductCodeExists = Undefined
type CheckAddressExists = Undefined
type UnvalidatedOrder = Undefined
type ValidatedOrder = Undefined
type ValidationError = Undefined
type GetProductPrice = Undefined
type PricedOrder = Undefined
type PricingError = Undefined

module PatternA =
    type ValidateOrder =
        CheckProductCodeExists // 依存を明示
            -> CheckAddressExists // 依存を明示
            -> UnvalidatedOrder // In
            -> AsyncResult<ValidatedOrder, ValidationError list> // Out

    type PriceOrder =
        GetProductPrice // 依存を明示
            -> ValidatedOrder // In
            -> Result<PricedOrder, PricingError> // Out

module PatternB =
    type ValidateOrder =
        UnvalidatedOrder // In
            -> AsyncResult<ValidatedOrder, ValidationError list> // Out

    type PriceOrder =
        ValidatedOrder // In
            -> Result<PricedOrder, PricingError> // Out


type PlaceOrder = Undefined
type PlaceOrderEvent = Undefined
type PlaceOrderError = Undefined

type PlaceOrderWorkflow =
    PlaceOrder // In
        -> AsyncResult<PlaceOrderEvent list, PlaceOrderError> // Out
