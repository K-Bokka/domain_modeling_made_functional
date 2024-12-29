open System

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

//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
printfn "\nChapter 4.3"

type AppleVariety =
    | GoldenDelicious
    | GrannySmith
    | Fuji

type BananaVariety =
    | Cavendish
    | GrosMichel
    | Manzano

type CherryVariety =
    | Montmorency
    | Bing

type FruitSalad =
    { Apple: AppleVariety
      Banana: BananaVariety
      Cherry: CherryVariety }

type FruitSnack =
    | Apple of AppleVariety
    | Banana of BananaVariety
    | Cherry of CherryVariety

type ProductCode = ProductCode of string

//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
printfn "\nChapter 4.4"

type Person = { First: string; Last: string }

let aPerson = { First = "Alex"; Last = "Adams" }

let { First = first; Last = last } = aPerson
let firstEx = $"{aPerson.First}!"
let lastEx = $"{aPerson.Last}!"

printfn $"First name is {first}, last name is {last}"
printfn $"First! name is {firstEx}, last! name is {lastEx}"

type OrderQuantity =
    | UnitQuantity of int
    | KilogramQuantity of decimal

let anOrderQtyInUnits = UnitQuantity 10
let anOrderQtyInKg = KilogramQuantity 2.5m

let printQuantity aOrderQty =
    match aOrderQty with
    | UnitQuantity uQty -> printfn $"{uQty} units"
    | KilogramQuantity kgQty -> printfn $"{kgQty} kg"

printQuantity anOrderQtyInUnits
printQuantity anOrderQtyInKg

//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
printfn "\nChapter 4.5"

type CheckNumber = CheckNumber of int
type CardNumber = CardNumber of string

type CardType =
    | Visa
    | Master

type CreditCardInfo =
    { CardType: CardType
      CardNumber: CardNumber }

type PaymentMethod =
    | Cash
    | Check of CheckNumber
    | Card of CardNumber

type PaymentAmount = PaymentAmount of decimal

type Currency =
    | EUR
    | USD

type Payment =
    { Amount: PaymentAmount
      Currency: Currency
      Method: PaymentMethod }

type UnpaidInvoice =
    { Amount: PaymentAmount
      Currency: Currency }

type PaidInvoice =
    { Amount: PaymentAmount
      Currency: Currency
      Method: PaymentMethod
      Date: DateTime }


type PayInvoice = UnpaidInvoice -> Payment -> PaidInvoice
type ConvertPaymentCurrency = Payment -> Currency -> Payment

