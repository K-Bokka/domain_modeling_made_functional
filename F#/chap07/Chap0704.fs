namespace Chap0704

module C070401 =
    type ProductCode = Undefined
    type CheckProductCodeExits = ProductCode -> bool

    type UnvalidatedAddress = Undefined
    type CheckedAddress = CheckedAddress of UnvalidatedAddress
    type AddressValidationError = AddressValidationError of string
    type CheckAddressExists = UnvalidatedAddress -> Result<CheckedAddress, AddressValidationError>

    type ValidationError = Undefined
    type UnvalidatedOrder = Undefined
    type ValidatedOrder = Undefined

    type ValidateOrder =
        CheckProductCodeExits // 依存関係
            -> CheckAddressExists // 依存関係
            -> UnvalidatedOrder // 入力
            -> Result<ValidatedOrder, ValidationError> // 出力
