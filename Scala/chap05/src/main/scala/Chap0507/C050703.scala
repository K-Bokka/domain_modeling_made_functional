package Chap0507

object C050703:
  // Scala は equals を override してやると等値性を変更できる
  case class Contact(contactId: ContactId, phone: PhoneNumber, email: EmailAddress) {
    override def equals(obj: Any): Boolean = obj match
      case Contact(cid, _, _) => cid == contactId
      case _ => false
  }
