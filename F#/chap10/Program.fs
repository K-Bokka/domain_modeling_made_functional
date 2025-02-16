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
