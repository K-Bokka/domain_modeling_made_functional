package Chap0404

enum OrderQuantity:
  case UnitQuantity(value: Int)
  case KilogramQuantity(value: BigDecimal)

object OrderQuantity:
  def printQuantity(oq: OrderQuantity): Unit = oq match
    case UnitQuantity(v) => println(s"$v units")
    case KilogramQuantity(v) => println(s"$v kg")