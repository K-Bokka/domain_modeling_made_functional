import Chap0604.*

object Chap0604:
  case class EmailAddress(value: String)

  case class VerifiedEmailAddress(value: String)

  case class Name(value: String)

  case class EmailContactInfo(value: String)

  case class PostalContactInfo(value: String)

object C0604a:
  case class CustomerEmail(
                            email: EmailAddress,
                            isVerified: Boolean,
                          )

object C0604b:
  enum CustomerEmail:
    case Unverified(email: EmailAddress)
    case Verified(email: EmailAddress)


object C0604c:
  enum CustomerEmail:
    case Unverified(email: EmailAddress)
    case Verified(email: VerifiedEmailAddress)


object C0606d:
  case class Contact(
                      name: Name,
                      email: EmailContactInfo,
                      address: PostalContactInfo,
                    )

object C0606e:
  case class Contact(
                      name: Name,
                      email: Option[EmailContactInfo],
                      address: Option[PostalContactInfo],
                    )

object C0606f:
  case class BothContactMethods(
                                 email: EmailContactInfo,
                                 address: PostalContactInfo,
                               )

  enum ContactInfo:
    case Email(email: EmailContactInfo)
    case Address(address: PostalContactInfo)
    case EmailAndAddr(email: EmailContactInfo, address: PostalContactInfo)

  case class Contact(
                      name: Name,
                      info: ContactInfo,
                    )

object C060401:
  case class UnvalidatedAddress(value: String)

  case class ValidatedAddress(value: String)

  type AddressValidationService = UnvalidatedAddress => Option[ValidatedAddress]

  case class UnvalidatedOrder(shippingAddress: UnvalidatedAddress)

  case class ValidatedOrder(shippingAddress: ValidatedAddress)
