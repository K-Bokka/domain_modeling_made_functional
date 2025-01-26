printfn "Chapter 9"

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

module Domain =
    open System

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
