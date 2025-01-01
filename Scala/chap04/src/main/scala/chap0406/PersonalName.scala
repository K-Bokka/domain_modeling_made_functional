package chap0406

case class PersonalName(
                         firstName: String,
                         middleInitial: Option[String],
                         lastName: String,
                       )

object PersonalName:
  def printName(pn: PersonalName): Unit = pn.middleInitial match
    case Some(mi) => println(s"I'm ${pn.firstName} $mi ${pn.lastName}")
    case None => println(s"I'm ${pn.firstName} ${pn.lastName}")
