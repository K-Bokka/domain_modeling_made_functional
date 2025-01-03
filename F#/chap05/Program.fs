open Chap0503.C050301
open Chap0507
open Program.Chap0406

printfn "Chapter 5.3"

let customerId = CustomerId 42
let orderId = OrderId 42

// println $"%b{orderId = customerId}"
printfn
    """
./domain_modeling_made_functional/F#/chap05/Program.fs(15,25): error FS0001: この式に必要な型は    'OrderId'    ですが、ここでは次の型が指定されています    'CustomerId'
"""

let processCustomerId (id: CustomerId) = printfn $"Customer ID is {id}"

// processCustomerId orderId
printfn
    """
./domain_modeling_made_functional/F#/chap05/Program.fs(27,19): error FS0001: この式に必要な型は    'CustomerId'    ですが、ここでは次の型が指定されています    'OrderId'
"""

let (CustomerId innerValue) = customerId
printfn $"innerValue is {innerValue}"

printfn ""
printfn "Chapter 5.6"

let widgetCode1 = WidgetCode "W1234"
let widgetCode2 = WidgetCode "W1234"

printfn $"widgetCode1 = widgetCode2 : {widgetCode1 = widgetCode2}"

let name1 =
    { FirstName = "Alex"
      MiddleInitial = None
      LastName = "Adams" }

let name2 =
    { FirstName = "Alex"
      MiddleInitial = None
      LastName = "Adams" }

printfn $"name1 = name2 : {name1 = name2}"

printfn ""
printfn "Chapter 5.7"

let printInvoiceId invoice =
    match invoice with
    | C050702a.Invoice.Unpaid iv -> printfn $"The unpaid invoiceId is {iv.InvoiceId}"
    | C050702a.Invoice.Paid iv -> printfn $"The paid invoiceId is {iv.InvoiceId}"

let invoice = C050702a.Paid { InvoiceId = "hoge" }

printInvoiceId invoice

let contactId = ContactId 1

let contact1: C050703.Contact =
    { ContactId = contactId
      PhoneNumber = PhoneNumber "123-456-7890"
      EmailAddress = EmailAddress "bob@example.com" }

let contact2: C050703.Contact =
    { ContactId = contactId
      PhoneNumber = PhoneNumber "123-456-7890"
      EmailAddress = EmailAddress "robert@example.com" }

printfn $"contact1 = contact2 : {contact1 = contact2}"

let contact3: C050703a.Contact =
    { ContactId = contactId
      PhoneNumber = PhoneNumber "123-456-7890"
      EmailAddress = EmailAddress "bob@example.com" }

let contact4: C050703a.Contact =
    { ContactId = contactId
      PhoneNumber = PhoneNumber "123-456-7890"
      EmailAddress = EmailAddress "robert@example.com" }

// printfn $"contact3 = contact4 : {contact3 = contact4}"
printfn
    """
./domain_modeling_made_functional/F#/chap05/Program.fs(83,34): error FS0001: The type 'C050703a.Contact' does not support the 'equality' constraint because it has the 'NoEquality' attribute
"""

printfn $"contact3.ContactId = contact4.ContactId : {contact3.ContactId = contact4.ContactId}"
printfn $"contact3.Key = contact4.Key : {contact3.Key = contact4.Key}"

let initialPerson =
    { PersonId = PersonId 42
      Name = "Joseph" }

printfn $"Initial name is {initialPerson.Name}"

let updatedPerson = { initialPerson with Name = "Joe" }
printfn $"Updated name is {updatedPerson.Name}"
