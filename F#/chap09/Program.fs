open System.Text.RegularExpressions

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
    let createString50 name tp str =
        if String.IsNullOrEmpty(str) then
            $"%s{name} must not be null or empty" |> failwith
        elif str.Length > 50 then
            $"%s{name} must not be more than 50 chars" |> failwith
        else
            tp str

    let createOptionString50 tp str =
        if String.IsNullOrEmpty(str) then None
        elif str.Length > 50 then None
        else tp str |> Some

    let createStringPattern name tp pattern str =
        if String.IsNullOrEmpty(str) then
            $"%s{name} must not be null or empty" |> failwith
        elif Regex.IsMatch(str, pattern) then
            tp str
        else
            $"%s{name}: %s{str} must match the pattern %s{pattern}" |> failwith

    type String50 = private String50 of string

    module String50 =
        let create str = createString50 "String" String50 str
        let createOption str = createOptionString50 String50 str

    let predicateToPassthru errorMsg f x = if f x then x else failwith errorMsg

module Domain =
    open Common

    type OrderId = private OrderId of string

    module OrderId =
        let create str = createString50 "OrderId" OrderId str

    type ZipCode = private ZipCode of string

    module ZipCode =
        let create str = createString50 "ZipCode" ZipCode str

    type OrderLineId = private OrderLineId of string

    module OrderLineId =
        let create str =
            createString50 "OrderLineId" OrderLineId str

    type WidgetCode = private WidgetCode of string

    module WidgetCode =
        let pattern = "W\d{4}"

        let create str =
            createStringPattern "WidgetCode" WidgetCode "W\d{4}" str

        let check (str: String) = Regex.IsMatch(str, pattern)

    type GizmoCode = private GizmoCode of string

    module GizmoCode =
        let create str =
            createStringPattern "GizmoCode" GizmoCode "G\d{3}" str

    type ProductCode =
        | Widget of WidgetCode
        | Gizmo of GizmoCode

    module ProductCode =
        let create str =
            if String.IsNullOrEmpty(str) then
                "ProductCode must not be null or empty" |> failwith
            elif str.StartsWith("W") then
                WidgetCode.create (str) |> ProductCode.Widget
            elif str.StartsWith("G") then
                GizmoCode.create (str) |> ProductCode.Gizmo
            else
                $"ProductCode format error: %s{str}" |> failwith

    type UnitQuantity = UnitQuantity of int

    module UnitQuantity =
        let create i =
            if i < 1 || 1000 < i then
                $"UnitQuantity range is 1 ~ 1000, this value is %i{i}" |> failwith
            else
                UnitQuantity i

    type KilogramQuantity = KilogramQuantity of decimal // TODO: 0.05以上100.00以下

    module KilogramQuantity =
        let create d =
            if 0.05M < d || d < 100.00M then
                $"KilogramQuantity range is 0.05 ~ 100.00, this value is %M{d}" |> failwith
            else
                KilogramQuantity d

    type OrderQuantity =
        | Unit of UnitQuantity
        | Kilos of KilogramQuantity

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
        { OrderLineId: string
          ProductCode: string
          Quantity: decimal }

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

    and ValidatedOrderLine =
        { OrderLineId: OrderLineId
          ProductCode: ProductCode
          Quantity: OrderQuantity }

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

        // TODO: bool を返してしまう...
        // let toProductCode (checkProductCodeExists: CheckProductCodeExists) productCode =
        //     productCode |> ProductCode.create |> checkProductCodeExists

        // 関数変換器（一般化したものは Common に記載
        let convertToPassthru checkProductCodeExists productCode =
            if checkProductCodeExists productCode then
                productCode
            else
                failwith "Invalid ProductCode"

        let toProductCode (checkProductCodeExists: CheckProductCodeExists) productCode =
            let checkProduct productCode =
                let errorMsg = $"Invalid: %A{productCode}"
                predicateToPassthru errorMsg checkProductCodeExists productCode

            productCode |> ProductCode.create |> checkProduct

        let toProductCode (checkProductCodeExists: CheckProductCodeExists) productCode =
            productCode |> ProductCode.create

        let toOrderQuantity productCode quantity =
            match productCode with
            | Widget _ -> quantity |> int |> UnitQuantity.create |> OrderQuantity.Unit
            | Gizmo _ -> quantity |> KilogramQuantity.create |> OrderQuantity.Kilos

        let toOrderLine checkProductCodeExists (unvalidatedOrderLine: UnvalidatedOrderLine) =
            let orderLineId = unvalidatedOrderLine.OrderLineId |> OrderLineId.create

            let productCode =
                unvalidatedOrderLine.ProductCode |> toProductCode checkProductCodeExists

            let quantity = unvalidatedOrderLine.Quantity |> toOrderQuantity productCode

            { OrderLineId = orderLineId
              ProductCode = productCode
              Quantity = quantity }

        fun checkProductCodeExists checkAddressExists unvalidatedOrder ->

            let orderId = unvalidatedOrder.OrderId |> OrderId.create
            let customerInfo = unvalidatedOrder.CustomerInfo |> toCustomerInfo

            let shippingAddress =
                unvalidatedOrder.ShippingAddress |> toAddress checkAddressExists

            let billingAddress = unvalidatedOrder.BillingAddress |> toAddress checkAddressExists

            let orderLines =
                unvalidatedOrder.OrderLines |> List.map (toOrderLine checkProductCodeExists)


            { OrderId = orderId
              CustomerInfo = customerInfo
              ShippingAddress = shippingAddress
              BillingAddress = billingAddress
              OrderLines = orderLines }
