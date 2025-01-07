package Chap0503

object C050301:
  case class CustomerId(value: Int)

  case class OrderId(value: Int)

  case class WidgetCode(value: String)

  case class UnitQuantity(value: Int)

  case class KilogramQuantity(value: BigDecimal)

  opaque type CustomerId2 = Int

  object CustomerId2:
    def apply(value: Int): CustomerId2 = value

  opaque type OrderId2 = Int

  object OrderId2:
    def apply(value: Int): OrderId2 = value

