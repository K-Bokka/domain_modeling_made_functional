namespace Chap0704

open chap0702

type ProductCode = Undefined
type ValidatedOrder = Undefined

module C070401 =
    type CheckProductCodeExits = ProductCode -> bool

    type UnvalidatedAddress = Undefined
    type CheckedAddress = CheckedAddress of UnvalidatedAddress
    type AddressValidationError = AddressValidationError of string
    type CheckAddressExists = UnvalidatedAddress -> Result<CheckedAddress, AddressValidationError>

    type ValidationError = Undefined
    type UnvalidatedOrder = Undefined

    type ValidateOrder =
        CheckProductCodeExits // 依存関係
            -> CheckAddressExists // 依存関係
            -> UnvalidatedOrder // 入力
            -> Result<ValidatedOrder, ValidationError> // 出力

module C070402 =
    type Price = Undefined
    type GetProductPrice = ProductCode -> Price

    type PricedOrder = Undefined

    type PriceOrder =
        GetProductPrice // 依存関係
            -> ValidatedOrder // 入力
            -> PricedOrder // 出力

