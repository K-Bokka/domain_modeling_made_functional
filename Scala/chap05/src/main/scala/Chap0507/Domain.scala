package Chap0507


case class InvoiceId(value: String)

case class ContactId(value: Int)

case class PhoneNumber(value: String)

case class EmailAddress(value: String)

case class PersonId(value: Int)

case class Person(personId: PersonId, name: String)
