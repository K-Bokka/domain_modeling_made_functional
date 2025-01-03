namespace Chap0507

open Microsoft.FSharp.Core

type Undefined = exn
type InvoiceId = string

module C050702 =
    type UnpaidInvoiceInfo = Undefined
    type PaidInvoiceInfo = Undefined

    type InvoiceInfo =
        | Unpaid of UnpaidInvoiceInfo
        | Paid of PaidInvoiceInfo


    type Invoice =
        { InvoiceId: InvoiceId
          InvoiceInfo: InvoiceInfo }

module C050702a =
    type UnpaidInvoice = { InvoiceId: InvoiceId }
    type PaidInvoice = { InvoiceId: InvoiceId }

    type Invoice =
        | Unpaid of UnpaidInvoice
        | Paid of PaidInvoice


type ContactId = ContactId of int
type PhoneNumber = PhoneNumber of string
type EmailAddress = EmailAddress of string

module C050703 =
    [<CustomEquality; NoComparison>]
    type Contact =
        { ContactId: ContactId
          PhoneNumber: PhoneNumber
          EmailAddress: EmailAddress }

        override this.Equals obj =
            match obj with
            | :? Contact as c -> this.ContactId = c.ContactId
            | _ -> false

        override this.GetHashCode() = hash this.ContactId

module C050703a =
    [<NoEquality; NoComparison>]
    type Contact =
        { ContactId: ContactId
          PhoneNumber: PhoneNumber
          EmailAddress: EmailAddress }

        member this.Key = (this.PhoneNumber, this.EmailAddress)

type PersonId = PersonId of int

type Person = { PersonId: PersonId; Name: string }
