namespace Chap0705

module C070501 =
    type UnvalidatedAddress = Undefined
    type CheckedAddress = Undefined
    type AddressValidationError = Undefined

    type AsyncResult<'success, 'failure> = Async<Result<'success, 'failure>>
    type CheckAddressExists = UnvalidatedAddress -> AsyncResult<CheckedAddress, AddressValidationError>

    type CheckProductCodeExists = Undefined
    type UnvalidatedOrder = Undefined
    type ValidatedOrder = Undefined
    type ValidationError = Undefined

    type ValidateOrder =
        CheckProductCodeExists // 依存関係
            -> CheckAddressExists // AsyncResult を返す依存関係
            -> UnvalidatedOrder
            -> AsyncResult<ValidatedOrder, ValidationError list>


module C070502 =
    open C070501

    type PricingError = PricingError of string
    type GetProductPrice = Undefined
    type PricedOrder = Undefined

    type PriceOrder =
        GetProductPrice // 依存関係
            -> ValidatedOrder // 入力
            -> Result<PricedOrder, PricingError>


module C070503 =
    open C070502

    type OrderAcknowledgement = Undefined
    type SendResult = Undefined
    type SendOrderAcknowledgment = OrderAcknowledgement -> Async<SendResult>

    type CreateOrderAcknowledgmentLetter = Undefined
    type OrderAcknowledgmentSent = Undefined

    type AcknowledgeOrder =
        CreateOrderAcknowledgmentLetter // 依存関係
            -> SendOrderAcknowledgment
            -> PricedOrder
            -> Async<OrderAcknowledgmentSent option>
