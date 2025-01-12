case class UnitQuantity private(value: Int)

object UnitQuantity {
  def apply(value: Int): Either[String, UnitQuantity] = value match
    case i if i < 1 => Left(s"UnitQuantity can not be negative")
    case i if i > 1000 => Left(s"UnitQuantity can not be more than 1000")
    case i => Right(new UnitQuantity(i))
}
