namespace Chap0704

module C070401 =
    type ProductCode = Undefined
    type ValidatedOrder = Undefined

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
    open C070401

    type Price = Undefined
    type PricedOrder = Undefined
    type GetProductPrice = ProductCode -> Price

    type PriceOrder =
        GetProductPrice // 依存関係
            -> ValidatedOrder // 入力
            -> PricedOrder // 出力


module C070403 =
    open C070402

    type EmailAddress = Undefined
    type HtmlString = HtmlString of string

    type OrderAcknowledgment =
        { EmailAddress: EmailAddress
          Letter: HtmlString }

    type CreateOrderAcknowledgmentLetter = PricedOrder -> HtmlString

    type SendOrderAcknowledgmentRetVoid = OrderAcknowledgment -> unit
    type SendOrderAcknowledgmentRetBool = OrderAcknowledgment -> bool

    type SendResult =
        | Sent
        | NotSent

    type SendOrderAcknowledgment = OrderAcknowledgment -> SendResult

    type OrderId = Undefined

    type OrderAcknowledgmentSent =
        { OrderId: OrderId
          EmailAddress: EmailAddress }

    type SendOrderAcknowledgmentRetEvent = OrderAcknowledgment -> OrderAcknowledgmentSent option

    type AcknowledgementOrder =
        CreateOrderAcknowledgmentLetter // 依存関係
            -> SendOrderAcknowledgment // 依存関係
            -> PricedOrder // 入力
            -> OrderAcknowledgmentSent option // 出力


module C070404 =
    open chap0702
    open C070403

    type Address = Undefined
    type BillingAmount = Undefined
    type OrderPlaced = PricedOrder

    type BillableOrderPlaced =
        { OrderId: OrderId
          BillingAddress: Address
          AmountToBill: BillingAmount }

    type PlaceOrderResult =
        { OrderPlaced: OrderPlaced
          BillableOrderPlaced: BillableOrderPlaced
          OrderAcknowledgmentSent: OrderAcknowledgmentSent }

    type PlaceOrderEvent =
        | OrderPlaced of OrderPlaced
        | BillableOrderPlaced of BillableOrderPlaced
        | AcknowledgmentSent of OrderAcknowledgmentSent

    type CreateEvents = PricedOrder -> PlaceOrderEvent list
