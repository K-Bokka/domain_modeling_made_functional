module Chap0401 =
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

    printfn ""

module Chap0403 =
    printfn "Chapter 4.3"

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

    printfn ""

module Chap0404 =
    printfn "Chapter 4.4"
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

    printfn ""

module Chap0405 =
    printfn "Chapter 4.5"
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

    // Move to Chapter 4.6
    // type UnpaidInvoice = Undefined
    // type PaidInvoice = Undefined
    // type PayInvoice = UnpaidInvoice -> Payment -> PaidInvoice
    type ConvertPaymentCurrency = Payment -> Currency -> Payment

    printfn ""

module Chap0406 =
    printfn "\nChapter 4.6"

    type PersonalName =
        { FirstName: string
          MiddleInitial: string option
          LastName: string }

    let aFullPersonalName =
        { FirstName = "Alex"
          MiddleInitial = Some "Adam"
          LastName = "Adams" }

    let aPersonalName =
        { FirstName = "Alex"
          MiddleInitial = None
          LastName = "Adams" }

    let printName pn =
        match pn.MiddleInitial with
        | Some x -> printfn $"I'm {pn.FirstName} {x} {pn.LastName}"
        | None -> printfn $"I'm {pn.FirstName} {pn.LastName}"

    printName aPersonalName
    printName aFullPersonalName

    type UnpaidInvoice = Undefined
    type PaidInvoice = Undefined

    type PaymentError =
        | CardTypeNotRecognized
        | PaymentRejected
        | PaymentProviderOffline

    type PayInvoice = UnpaidInvoice -> Chap0405.Payment -> Result<PaidInvoice, PaymentError>

    type Customer = Customer of string
    type SaveCustomer = Customer -> unit
    type NextRandom = unit -> unit

    type OrderId = OrderId of int
    type OrderLine = OrderLine of string

    type Order =
        { OrderId: OrderId
          Lines: OrderLine list }

    let aList = [ 1; 2; 3 ]
    printfn $"{aList}"

    let aNewList = 0 :: aList
    printfn $"{aNewList}"

    let printList1 aList =
        match aList with
        | [] -> printfn "list is empty"
        | [ x ] -> printfn $"list has one element: {x}"
        | [ x; y ] -> printfn $"list has two elements: {x} and {y}"
        | _ -> printfn $"list has more than two elements"

    let printList2 aList =
        match aList with
        | [] -> printfn "list is empty"
        | first :: _ -> printfn $"list is non-empty with the first element being: {first}"

    printList1 aList
    printList2 aList
