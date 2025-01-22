printfn "Chapter 8"

module C080201 =
    let plus3 x = x + 3

    let times2 x = x * 2

    // let square = (fun x -> x * x)
    let square x = x * x // 上の定義と同じ

    let addThree = plus3

    let listOfFunctions = [ addThree; times2; square ]

    for fn in listOfFunctions do
        let result = fn 100
        printfn $"If 1000 is the input, the output is %i{result}"

    let myString = "hello"

module C080202 =
    let evalWith5ThenAdd2 fn = fn (5) + 2

    let add1 x = x + 1
    let res1 = evalWith5ThenAdd2 add1

    printfn $"If add1 function is the input, the output is %i{res1}"

    let square x = x * x
    let res2 = evalWith5ThenAdd2 square
    printfn $"If square function is the input, the output is %i{res2}"

module C080203 =
    let add1 x = x + 1
    let add2 x = x + 2
    let add3 x = x + 3

    let adderGenerator numberToAdd = fun x -> numberToAdd + x

    let adderGeneratorEx numberToAdd =
        let innerFn x = numberToAdd + x
        innerFn

    let add1Ex = adderGeneratorEx 1
    printfn $"add1 1 = %i{add1Ex 1}"

    let add100 = adderGenerator 100
    printfn $"add100 2 = %i{add100 2}"

module C080204 =
    let add x y = x + y
    let adderGenerator x = fun y -> x + y

module C080205 =
    let sayGreeting greeting name = printfn $"%s{greeting} %s{name}"
    let sayHello = sayGreeting "Hello"
    let sayGoodbye = sayGreeting "Goodbye"

    sayHello "Alex"
    sayGoodbye "Alex"

module C0803 =
    let twelveDivideBy n =
        match n with
        | 6 -> 2
        | 5 -> 2
        | 4 -> 3
        | 3 -> 4
        | 2 -> 6
        | 1 -> 12
        | 0 -> failwith "Can't divide by zero"
        | _ -> failwith "Unknown input"

    type NoneZeroInteger = private NoneZeroInteger of int

    let twelveDivideBy2 (NoneZeroInteger n) =
        match n with
        | 6 -> 2
        | 5 -> 2
        | 4 -> 3
        | 3 -> 4
        | 2 -> 6
        | 1 -> 12
        | 0 -> failwith "Can't divide by zero" // 制限はまだかけてないがここには来ないようにする
        | _ -> failwith "Unknown input" // こっちにも制限をかけて来ないようにする

    let twelveDivideBy3 n =
        match n with
        | 6 -> Some 2
        | 5 -> Some 2
        | 4 -> Some 3
        | 3 -> Some 4
        | 2 -> Some 6
        | 1 -> Some 12
        | 0 -> None
        | _ -> None

module C080401 =
    let add1 x = x + 1
    let square x = x * x
    let add1TheSquare x = x |> add1 |> square

    printfn $"add1ThenSquare 5 : %i{add1TheSquare 5}"

    let isEven x = (x % 2) = 0
    let printBool x = sprintf $"value is %b{x}"
    let isEvenThenPrint x = x |> isEven |> printBool

    printfn $"isEvenThenPrint 2 : %s{isEvenThenPrint 2}"

module C080403 =
    let add1 x = x + 1

    let printOption x =
        match x with
        | Some i -> printfn $"The int is %i{i}"
        | None -> printfn "No value"

    5 |> add1 |> Some |> printOption
