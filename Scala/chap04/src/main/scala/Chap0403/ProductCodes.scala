package Chap0403

object ProductCodes:
  opaque type ProductCode = String

  object ProductCode:
    def apply(value: String): ProductCode = value
