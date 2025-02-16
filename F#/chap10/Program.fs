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
