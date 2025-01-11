// For more information see https://aka.ms/fsharp-console-apps

open Chap0601
open Chap0602

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

printfn "\nChapter 6.2"

let fiveKilos = 5.0<kg>
let fiveMeters = 5.0<m>

// let res = fiveKilos = fiveMeters
printfn """
./domain_modeling_made_functional/F#/chap06/Program.fs(28,13): error FS0001: 型が一致しません。    'float<kg>'    という指定が必要ですが、    'float<m>'    が指定されました。測定単位 'm' と一致しません
"""

// コンパイルエラーにならないな...
let listOfWeights = [fiveKilos, fiveMeters]
