open Chap0503.C050301

printfn "Chapter 5.3"

let customerId = CustomerId 42
let orderId = OrderId 42

// println $"%b{orderId = customerId}"
printfn """
./domain_modeling_made_functional/F#/chap05/Program.fs(15,25): error FS0001: この式に必要な型は    'OrderId'    ですが、ここでは次の型が指定されています    'CustomerId'
"""

let processCustomerId (id: CustomerId) = printfn $"Customer ID is {id}"

// processCustomerId orderId
printfn """
./domain_modeling_made_functional/F#/chap05/Program.fs(27,19): error FS0001: この式に必要な型は    'CustomerId'    ですが、ここでは次の型が指定されています    'OrderId'
"""

let (CustomerId innerValue) = customerId
printfn $"innerValue is {innerValue}"