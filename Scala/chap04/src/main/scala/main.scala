import Chap0404.*
import Chap0404.OrderQuantity.{KilogramQuantity, UnitQuantity}
import Chap0405.{Currency, Payment}
import chap0406.PersonalName

@main def hello(): Unit =
  println("Chapter 4.1")
  println(s"ex) add1 2 -> ${Chap0401.add1(2)}")
  println(s"ex) add 2 4 -> ${Chap0401.add(2, 4)}")
  println(s"ex) squarePlusOne 3 -> ${Chap0401.squarePlusOne(3)}")
  println(s"ex) areEqual 2 3 -> ${Chap0401.areEqual(2, 3)}")
  println(s"ex) areEqual 4.2 4.2 -> ${Chap0401.areEqual(4.2, 4.2)}")
  println()

  println("Chapter 4.4")
  val aPerson = Person("Alex", "Adams")
  val Person(firstName, lastName) = aPerson
  println(s"First name is $firstName, last name is $lastName")

  val anOrderQtyInUnits = UnitQuantity(10)
  val anOrderQtyInKg = KilogramQuantity(2.5)
  OrderQuantity.printQuantity(anOrderQtyInUnits)
  OrderQuantity.printQuantity(anOrderQtyInKg)
  println()

  println("Chapter 4.5")
  type ConvertPaymentCurrency = Payment => Currency => Payment
  println()

  println("Chapter 4.6")
  val aFullPersonalName = PersonalName("Alex", Some("Adam"), "Adams")
  val aPersonalName = PersonalName("Alex", None, "Adams")
  PersonalName.printName(aFullPersonalName)
  PersonalName.printName(aPersonalName)
