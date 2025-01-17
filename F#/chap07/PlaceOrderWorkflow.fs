namespace PlaceOrderWorkflow

open Domain
open chap0702

// 注文のライフサイクル

// 検証済み
type ValidatedOrderLine = Undefined

type ValidatedOrder =
    { OrderId: OrderId
      CustomerInfo: CustomerInfo
      ShippingAddress: Address
      BillingAddress: Address
      OrderLines: ValidatedOrderLine list }

and OrderId = Undefined
and CustomerInfo = Undefined
and Address = Undefined

// 計算済み
type PricedOrderLine = Undefined
type PricedOrder = Undefined

// 注文の全状態
type Order =
    | Unvalidated of UnvalidatedOrder
    | Validated of ValidatedOrder
    | Priced of PricedOrder

// 内部ステップ

// 注文の検証
type ProductCode = Undefined
type CheckProductCodeExists = ProductCode -> bool

type AddressValidationError = Undefined
type CheckedAddress = Undefined
type CheckAddressExists = UnvalidatedAddress -> AsyncResult<CheckedAddress, AddressValidationError>

type ValidateOrder =
    CheckProductCodeExists // 依存関係
        -> CheckAddressExists // 依存関係
        -> UnvalidatedOrder // 入力
        -> AsyncResult<ValidatedOrder, ValidationError list>

and ValidationError = Undefined

// 注文の価格計算
type Price = Undefined
type GetProductPrice = ProductCode -> Price
type PricingError = Undefined

type PriceOrder =
    GetProductPrice // 依存関係
        -> ValidatedOrder // 入力
        -> Result<PricedOrder, PricingError>
