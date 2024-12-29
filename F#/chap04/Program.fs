printfn "Chapter 4.1"
let add1 x = x + 1

printfn $"ex) add1 2 -> {add1 2}"

let add x y = x + y

printfn $"ex) add 2 4 -> {add 2 4}"

let squarePlusOne x =
    let square = x * x
    square + 1

printfn $"ex) squarePlusOne 3 -> {squarePlusOne 3}"

let areEqual x y = (x = y)

printfn $"ex) areEqual 2 3 -> {areEqual 2 3}"
printfn $"ex) areEqual 4.2 4.2 -> {areEqual 4.2 4.2}"
