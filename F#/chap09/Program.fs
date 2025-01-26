printfn "Chapter 9"

open System

module Chap9 =
    let validateOrder _ = failwith "Undefined"
    let priceOrder _ = failwith "Undefined"
    let acknowledgeOrder _ = failwith "Undefined"
    let createEvents _ = failwith "Undefined"

    let placeOrder unvalidatedOrder =
        unvalidatedOrder
        |> validateOrder
        |> priceOrder
        |> acknowledgeOrder
        |> createEvents

module Common =
    type String50 = private String50 of string

    module String50 =
        let create str =
            if String.IsNullOrEmpty(str) then
                failwith "String must not be null or empty"
            elif str.Length > 50 then
                failwith "String must not be more than 50 chars"
            else
                String50 str

module Domain =
    open Common

    type OrderId = private OrderId of string

    module OrderId =
        let create str =
            if String.IsNullOrEmpty(str) then
                failwith "OrderId must not be null or empty"
            elif str.Length > 50 then
                failwith "OrderId must not be more than 50 chars"
            else
                OrderId str

        let value (OrderId str) = str

    // For chap0903
    type Address = private Address of String
    type EmailAddress = private EmailAddress of String

    module EmailAddress =
        let value (EmailAddress str) = str
        let create email = EmailAddress email

    type UnvalidatedOrder =
        { OrderId: string
          CustomerInfo: UnvalidatedCustomerInfo
          ShippingAddress: UnvalidatedAddress
          BillingAddress: UnvalidatedAddress
          OrderLines: UnvalidatedOrderLine list }

    and UnvalidatedCustomerInfo =
        { FirstName: string
          LastName: string
          EmailAddress: string }

    and UnvalidatedAddress = UnvalidatedAddress of string

    and UnvalidatedOrderLine =
        { ProductCode: string
          OrderQuantity: string }

    type ValidatedOrder =
        { OrderId: OrderId
          CustomerInfo: CustomerInfo
          ShippingAddress: ShippingAddress
          BillingAddress: BillingAddress
          OrderLines: ValidatedOrderLine list }

    and CustomerInfo =
        { Name: PersonalName
          EmailAddress: EmailAddress }

    and PersonalName =
        { FirstName: String50
          LastName: String50 }

    and ShippingAddress = private ShippingAddress of Address
    and BillingAddress = private BillingAddress of Address
    and ValidatedOrderLine = private ValidatedOrderLine of string

module C0902 =
    let validateOrder
        checkProductCodeExists // 依存
        checkAddressExists // 依存
        unvalidatedOrder // Input
        =
        failwith "Undefined"

    type Param1 = Undefined
    type Param2 = Undefined
    type Param3 = Undefined

    type MyFunctionSignature = Param1 -> Param2 -> Param3
    let myFunc: MyFunctionSignature = fun param1 param2 -> failwith "Undefined"

    type UnvalidatedOrder = Undefined
    type ValidatedOrder = Undefined
    type ValidationError = Undefined
    type ProductCode = ProductCode of string
    type CheckProductCodeExists = ProductCode -> bool
    type CheckAddressExists = Undefined

    type ValidateOrder =
        CheckAddressExists // 依存
            -> CheckAddressExists // 依存
            -> UnvalidatedOrder // In
            -> Result<ValidatedOrder, ValidationError list> // Out

    let validateOrder2: ValidateOrder =
        fun checkProductCodeExists checkAddressExists unvalidatedOrder -> failwith "Undefined"

module C0903 =
    open Domain
    open Common

    type UnvalidatedAddress = Undefined
    type CheckedAddress = Undefined
    type CheckAddressExists = UnvalidatedAddress -> CheckedAddress

    type ProductCode = ProductCode of string
    type CheckProductCodeExists = ProductCode -> bool

    type ValidateOrder =
        CheckProductCodeExists // dependency
            -> CheckAddressExists // dependency
            -> UnvalidatedOrder // In
            -> ValidatedOrder // Out

    let validateOrder: ValidateOrder =
        let toCustomerInfo (customer: UnvalidatedCustomerInfo) =
            let firstName = customer.FirstName |> String50.create
            let lastName = customer.LastName |> String50.create
            let emailAddress = customer.EmailAddress |> EmailAddress.create

            { Name =
                { FirstName = firstName
                  LastName = lastName }
              EmailAddress = emailAddress }

        let toAddress _ = failwith "hoge"
        let toOrderLines _ = failwith "hoge"

        fun checkProductCodeExists checkAddressExists unvalidatedOrder ->

            let orderId = unvalidatedOrder.OrderId |> OrderId.create
            let customerInfo = unvalidatedOrder.CustomerInfo |> toCustomerInfo
            let shippingAddress = unvalidatedOrder.ShippingAddress |> toAddress
            let billingAddress = unvalidatedOrder.BillingAddress |> toAddress
            let orderLines = unvalidatedOrder.OrderLines |> toOrderLines


            { OrderId = orderId
              CustomerInfo = customerInfo
              ShippingAddress = shippingAddress
              BillingAddress = billingAddress
              OrderLines = orderLines }
