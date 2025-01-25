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
