package Chap0507

object C050703a:
  class Contact(contactId: ContactId, phone: PhoneNumber, email: EmailAddress) {
    val key: (PhoneNumber, EmailAddress) = (phone, email)
  }
