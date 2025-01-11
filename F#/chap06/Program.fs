// For more information see https://aka.ms/fsharp-console-apps

open Chap0601

printfn "Chapter 6.1"

// let unitQty = UnitQuantity 1
printfn
    """
./domain_modeling_made_functional/F#/chap06/Program.fs(7,15): error FS1093: 型 'UnitQuantity' の共用体ケースまたはフィールドは、このコードの場所からアクセスできません
"""

let unitQtyResult = UnitQuantity.create 1

match unitQtyResult with
| Error msg -> printfn $"Failure. Message is {msg}"
| Ok uQty ->
    printfn $"Success. Value is {uQty}"
    let innerValue = UnitQuantity.value uQty
    printfn $"inner Value is {innerValue}"
