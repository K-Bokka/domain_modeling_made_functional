namespace Chap0601

type UnitQuantity = private UnitQuantity of int

module UnitQuantity =
    let create qty =
        if qty < 1 then
            Error "UnitQuantity can not be negative"
        else if qty > 1000 then
            Error "UnitQuantity can not be more than 1000"
        else
            Ok(UnitQuantity qty)

    let value (UnitQuantity qty) = qty
