package Chap0404


type UnitQuantity2 = Int
type KilogramQuantity2 = BigDecimal

type OrderQuantity2 = UnitQuantity2 | KilogramQuantity2


object OrderQuantity2:
  def printQuantity(v: UnitQuantity2): Unit = println(s"$v units")

  def printQuantity(v: OrderQuantity2): Unit = println(s"$v units")
