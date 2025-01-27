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

        let createOption str =
            if String.IsNullOrEmpty(str) then None
            elif str.Length > 50 then None
            else String50 str |> Some

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

    type ZipCode = private ZipCode of string

    module ZipCode =
        let create str =
            if String.IsNullOrEmpty(str) then
                failwith "ZipCode must not be null or empty"
            elif str.Length > 50 then
                failwith "ZipCode must not be more than 50 chars"
            else
                ZipCode str

        let value (OrderId str) = str

    // For chap0903
    type Address =
        { AddressLine1: String50
          AddressLine2: String50 option
          AddressLine3: String50 option
          AddressLine4: String50 option
          City: String50
          ZipCode: ZipCode }

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

    and UnvalidatedAddress =
        { AddressLine1: string
          AddressLine2: string
          AddressLine3: string
          AddressLine4: string
          City: string
          ZipCode: string }

    and UnvalidatedOrderLine =
        { ProductCode: string
          OrderQuantity: string }

    type ValidatedOrder =
        { OrderId: OrderId
          CustomerInfo: CustomerInfo
          ShippingAddress: Address
          BillingAddress: Address
          OrderLines: ValidatedOrderLine list }

    and CustomerInfo =
        { Name: PersonalName
          EmailAddress: EmailAddress }

    and PersonalName =
        { FirstName: String50
          LastName: String50 }

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

    type CheckedAddress = CheckedAddress of UnvalidatedAddress
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

        let toAddress (checkAddressExists: CheckAddressExists) unvalidatedAddress =

            let checkedAddress = checkAddressExists unvalidatedAddress
            let (CheckedAddress checkedAddress) = checkedAddress

            let addressLine1 = checkedAddress.AddressLine1 |> String50.create
            let addressLine2 = checkedAddress.AddressLine2 |> String50.createOption
            let addressLine3 = checkedAddress.AddressLine3 |> String50.createOption
            let addressLine4 = checkedAddress.AddressLine4 |> String50.createOption
            let city = checkedAddress.City |> String50.create
            let zipCode = checkedAddress.ZipCode |> ZipCode.create

            let address: Address =
                { AddressLine1 = addressLine1
                  AddressLine2 = addressLine2
                  AddressLine3 = addressLine3
                  AddressLine4 = addressLine4
                  City = city
                  ZipCode = zipCode }

            address

        let toOrderLines _ = failwith "hoge"

        fun checkProductCodeExists checkAddressExists unvalidatedOrder ->

            let orderId = unvalidatedOrder.OrderId |> OrderId.create
            let customerInfo = unvalidatedOrder.CustomerInfo |> toCustomerInfo
            let shippingAddress = unvalidatedOrder.ShippingAddress |> toAddress checkAddressExists
            let billingAddress = unvalidatedOrder.BillingAddress |> toAddress checkAddressExists
            let orderLines = unvalidatedOrder.OrderLines |> toOrderLines


            { OrderId = orderId
              CustomerInfo = customerInfo
              ShippingAddress = shippingAddress
              BillingAddress = billingAddress
              OrderLines = orderLines }
