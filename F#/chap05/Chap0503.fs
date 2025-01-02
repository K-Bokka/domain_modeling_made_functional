namespace Chap0503

module C050301 =
    type CustomerId = CustomerId of int
    type OrderId = OrderId of int
    type WidgetCode = WidgetCode of string
    type UnitQuantity = UnitQuantity of int
    type KilogramQuantity = KilogramQuantity of decimal

module C050303 =
    type UnitQuantity = int

module C050303a =
    [<Struct>]
    type UnitQuantity = UnitQuantity of int
    type UnitQuantities = UnitQuantities of int[]
