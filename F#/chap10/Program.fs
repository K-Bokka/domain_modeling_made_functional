// For more information see https://aka.ms/fsharp-console-apps

open System

printfn "Chapter 10"

module C1001 =
    type UnvalidatedAddress = Undefined
    type CheckedAddress = Undefined

    module SimplePattern =
        type CheckAddressExists = UnvalidatedAddress -> CheckedAddress

    type CheckAddressExists = UnvalidatedAddress -> Result<CheckedAddress, AddressValidationError>

    and AddressValidationError =
        | InvalidFormat of string
        | AddressNotFound of string

module C1002 =
    let workflowPart1 = 0

    let workflowPart2 input =
        if input = 0 then
            raise (DivideByZeroException())

        10 / input

    let main () =
        try
            let result1 = workflowPart1
            let result2 = workflowPart2 result1
            printfn $"the result is %A{result2}"
        with
        | :? OutOfMemoryException -> printfn "exited with OutOfMemoryException"
        | :? DivideByZeroException -> printfn "exited with DivideByZeroException"
        | ex -> printfn $"exited with %s{ex.Message}"

C1002.main ()

module C100201 =
    type ProductCode = Undefined
    type RemoteServiceError = Undefined

    type PlaceOrderError =
        | ValidationError of string
        | ProductOutOfStock of ProductCode
        | RemoteServiceError of RemoteServiceError

module C100301 =
    module Pattern1 =
        let bind switchFn =
            fun twoTrackInput ->
                match twoTrackInput with
                | Ok success -> switchFn success
                | Error failure -> Error failure

    let bind switchFn twoTrackInput =
        match twoTrackInput with
        | Ok success -> switchFn success
        | Error failure -> Error failure

    let map f aResult =
        match aResult with
        | Ok success -> Ok(f success)
        | Error failure -> Error failure

type Result<'Success, 'Failure> =
    | Ok of 'Success
    | Error of 'Failure

module Result =
    let bind f aResult =
        match aResult with
        | Ok success -> f success
        | Error failure -> Error failure

    let map f aResult =
        match aResult with
        | Ok success -> Ok(f success)
        | Error failure -> Error failure

    let mapError f aResult =
        match aResult with
        | Ok success -> Ok success
        | Error failure -> Error(f failure)

module C100303 =
    type Apple = Apple of string
    type Bananas = Bananas of string
    type Cherries = Cherries of string
    type Lemon = Lemon of string

    type FunctionA = Apple -> Result<Bananas, string>
    type FunctionB = Bananas -> Result<Cherries, string>
    type FunctionC = Cherries -> Result<Lemon, string>

    let functionA: FunctionA = fun _ -> Ok(Bananas "bananas")
    let functionB: FunctionB = fun _ -> Ok(Cherries "cherries")
    let functionC: FunctionC = fun _ -> Ok(Lemon "lemon")

    let functionABC input =
        input |> functionA |> Result.bind functionB |> Result.bind functionC

    // エラーになる
    // let functionAC input =
    //     input |> functionA |> Result.bind functionC
    printfn
        "./domain_modeling_made_functional/F#/chap10/Program.fs(102,43): error FS0001: 型が一致しません。    'Bananas -> Result<'a,string>'    という指定が必要ですが、    'FunctionC'    が指定されました。型 'Cherries' は型 'Bananas' と一致しません"

module C100304 =
    type Apple = Apple of string
    type AppleError = AppleError of string
    type Bananas = Bananas of string
    type BananaError = BananaError of string
    type Cherries = Cherries of string

    type FunctionA = Apple -> Result<Bananas, AppleError>
    type FunctionB = Bananas -> Result<Cherries, BananaError>
    let functionA: FunctionA = fun _ -> Ok(Bananas "bananas")
    let functionB: FunctionB = fun _ -> Ok(Cherries "cherries")

    type FruitError =
        | AppleErrorCase of AppleError
        | BananaErrorCase of BananaError

    let functionAWithFruitError input =
        input |> functionA |> Result.mapError AppleErrorCase

    let functionBWithFruitError input =
        input |> functionB |> Result.mapError BananaErrorCase

    let functionAB input =
        input |> functionAWithFruitError |> Result.bind functionBWithFruitError

module C1004 =
    type UnvalidatedOrder = Undefined
    type ValidatedOrder = ValidatedOrder of string
    type ValidationError = Undefined
    type ValidateOrder = UnvalidatedOrder -> Result<ValidatedOrder, ValidationError>
    let validateOrder: ValidateOrder = fun _ -> Ok(ValidatedOrder "validated")

    type PricedOrder = PricedOrder of string
    type PricingError = Undefined
    type PriceOrder = ValidatedOrder -> Result<PricedOrder, PricingError>
    let priceOrder: PriceOrder = fun _ -> Ok(PricedOrder "priced")

    type OrderAcknowledgmentSent = Undefined
    type AcknowledgeOrder = PricedOrder -> OrderAcknowledgmentSent option
    let acknowledgeOrder: AcknowledgeOrder = fun _ -> None

    type PlaceOrderEvent = Undefined
    type CreateEvents = PricedOrder -> OrderAcknowledgmentSent option -> PlaceOrderEvent list
    let createEvents: CreateEvents = fun pricedOrder orderAcknowledgmentSentOpt -> []

    type PlaceOrderError =
        | Validation of ValidationError
        | Pricing of PricingError

    let validateOrderAdapted input =
        input |> validateOrder |> Result.mapError PlaceOrderError.Validation

    let priceOrderAdapted input =
        input |> priceOrder |> Result.mapError PlaceOrderError.Pricing

    let placeOrder unvalidatedOrder =
        unvalidatedOrder
        |> validateOrderAdapted
        |> Result.bind priceOrderAdapted
        |> Result.map acknowledgeOrder
    // |> Result.map createEvents

    // acknowledgeOrder の出力と createEvents の入力が違うのでコンパイルできない
    printfn
        "./domain_modeling_made_functional/F#/chap10/Program.fs(173,23): error FS0001: 型が一致しません。    'OrderAcknowledgmentSent option -> 'a'    という指定が必要ですが、    'CreateEvents'    が指定されました。型 'PricedOrder' は型 'OrderAcknowledgmentSent option' と一致しません"

module C100501 =
    type ServiceInfo = { Name: string; Endpoint: Uri }

    type RemoteServiceError =
        { Service: ServiceInfo
          Exception: Exception }

    exception AuthorizationException of string

    let ServiceExceptionAdapter serviceInfo serviceFn x =
        try
            Ok(serviceFn x)
        with
        | :? TimeoutException as ex ->
            Error
                { Service = serviceInfo
                  Exception = ex }
        | :? AuthorizationException as ex ->
            Error
                { Service = serviceInfo
                  Exception = ex }

    let ServiceExceptionAdapter2 serviceInfo serviceFn x y =
        try
            Ok(serviceFn x y)
        with
        | :? TimeoutException as ex ->
            Error
                { Service = serviceInfo
                  Exception = ex }
        | :? AuthorizationException as ex ->
            Error
                { Service = serviceInfo
                  Exception = ex }


    type ValidationError = Undefined
    type PricingError = Undefined

    type PlaceOrderError =
        | Validation of ValidationError
        | Pricing of PricingError
        | RemoteService of RemoteServiceError // 追加

    let serviceInfo =
        { Name = "AddressCheckService"
          Endpoint = Uri "https://any.com/endpoint" }

    let checkAddressExists address = failwith "Any exception"

    let checkAddressExistsR address =
        let adaptedService = ServiceExceptionAdapter serviceInfo checkAddressExists

        address |> adaptedService |> Result.mapError RemoteService

let tee f x =
    f x
    x

module C100502 =
    let logError msg = printfn $"Error %s{msg}"

    let adaptDeadEnd f = Result.map (tee f)

// コンピュテーション式
type ResultBuilder() =
    member this.Return(x) = Ok x
    member this.Bind(x, f) = Result.bind f x

let result = ResultBuilder()

module C1006 =
    // コンピュテーション式を使用すると
    // module C1004: 166行目の placeOrder を以下のように書き換えられる
    type ValidationError = Undefined
    type PricingError = Undefined
    type RemoteServiceError = Undefined

    type PlaceOrderError =
        | Validation of ValidationError
        | Pricing of PricingError
        | RemoteService of RemoteServiceError

    let validateOrder _ = failwith "Not impl"
    let priceOrder _ = failwith "Not impl"
    let acknowledgeOrder _ = failwith "Not impl"
    let createEvents _ = failwith "Not impl"


    let placeOrder unvalidatedOrder =
        result {
            let! validatedOrder = validateOrder unvalidatedOrder |> Result.mapError PlaceOrderError.Validation
            let! pricedOrder = priceOrder validatedOrder |> Result.mapError PlaceOrderError.Pricing
            let acknowledgmentOption = acknowledgeOrder pricedOrder
            let events = createEvents pricedOrder acknowledgmentOption
            return events
        }

module C100601 =
    let validateOrder input =
        result {
            let! validatedOrder = failwith "Not impl"
            return validatedOrder
        }

    let priceOrder input =
        result {
            let! pricedOrder = failwith "Not impl"
            return pricedOrder
        }

    let placeOrder unvalidatedOrder =
        result {
            let! validatedOrder = validateOrder unvalidatedOrder
            let! pricedOrder = priceOrder validatedOrder

            return pricedOrder
        }

module C100602 =
    type Undefined = string
    type CheckProductCodeExists = Undefined
    type CheckAddressExists = Undefined

    type UnvalidatedOrder =
        { OrderId: Undefined
          CustomerInfo: Undefined
          ShippingAddress: Undefined
          BillingAddress: Undefined
          Lines: Undefined }

    type ValidatedOrder = UnvalidatedOrder

    module OrderId =
        let create _ = failwith "Not impl"

    let toCustomerInfo _ = failwith "Not impl"
    let toAddress _ = failwith "Not impl"

    module Pattern1 =
        type ValidateOrder =
            CheckProductCodeExists // dependency
                -> CheckAddressExists // dependency
                -> UnvalidatedOrder // input
                -> ValidatedOrder // output

        let validateOrder: ValidateOrder =
            fun checkProductCodeExists checkAddressExists unvalidatedOrder ->
                let orderId = unvalidatedOrder.OrderId |> OrderId.create
                let customerInfo = unvalidatedOrder.CustomerInfo |> toCustomerInfo

                let shippingAddress =
                    unvalidatedOrder.ShippingAddress |> toAddress checkAddressExists

                let billingAddress = failwith "Not impl"
                let lines = failwith "Not impl"

                let validatedOrder: ValidatedOrder =
                    { OrderId = orderId
                      CustomerInfo = customerInfo
                      ShippingAddress = shippingAddress
                      BillingAddress = billingAddress
                      Lines = lines }

                validatedOrder


    type ValidationError = ValidationError of string

    type ValidateOrder =
        CheckProductCodeExists // dependency
            -> CheckAddressExists // dependency
            -> UnvalidatedOrder // input
            -> Result<ValidatedOrder, ValidationError> // output

    let validateOrder: ValidateOrder =
        fun checkProductCodeExists checkAddressExists unvalidatedOrder ->
            result {
                let! orderId = unvalidatedOrder.OrderId |> OrderId.create |> Result.mapError ValidationError
                let! customerInfo = unvalidatedOrder.CustomerInfo |> toCustomerInfo
                let! shippingAddress = unvalidatedOrder.ShippingAddress |> toAddress checkAddressExists

                let billingAddress = failwith "Not impl"
                let lines = failwith "Not impl"

                let validatedOrder: ValidatedOrder =
                    { OrderId = orderId
                      CustomerInfo = customerInfo
                      ShippingAddress = shippingAddress
                      BillingAddress = billingAddress
                      Lines = lines }

                return validatedOrder
            }
