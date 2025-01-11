namespace Chap0604

type EmailAddress = Undefiend

type VerifiedEmailAddress = Undefiend

module C0604a =
    type CustomerEmail =
        { EmailAddress: EmailAddress
          IsVerified: bool }

module C0604b =
    type CustomerEmail =
        | Unverified of EmailAddress
        | Verified of EmailAddress

module C0604c =
    type CustomerEmail =
        | Unverified of EmailAddress
        | Verified of VerifiedEmailAddress

type Name = Undefiend
type EmailContactInfo = Undefiend
type PostalContactInfo = Undefiend


module C0604d =
    type Contact =
        { Name: Name
          Email: EmailContactInfo
          Address: PostalContactInfo }

module C0604e =
    type Contact =
        { Name: Name
          Email: EmailContactInfo option
          Address: PostalContactInfo option }

module C0604f =
    type BothContactMethods =
        { Email: EmailContactInfo
          Address: PostalContactInfo }

    type ContactInfo =
        | EmailOnly of EmailContactInfo
        | AddrOnly of PostalContactInfo
        | EmailAndAddr of BothContactMethods

    type Contact =
        { Name: Name; ContactInfo: ContactInfo }

module C060401 =
    type UnvalidatedAddress = UnvalidatedAddress of string
    type ValidatedAddress = private UnvalidatedAddress of string
    type AddressValidationService = UnvalidatedAddress -> ValidatedAddress option
    type UnvalidatedOrder = { ShippingAddress: UnvalidatedAddress }
    type ValidatedOrder = { ShippingAddress: ValidatedAddress }
