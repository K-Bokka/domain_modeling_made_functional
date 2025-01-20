import cats.data.EitherT

import scala.concurrent.Future

object Chap0707:

  case class CheckProductCodeExists(value: Any)

  case class CheckAddressExists(value: Any)

  case class UnvalidatedOrder(value: Any)

  case class ValidatedOrder(value: Any)

  case class ValidationError(value: Any)

  case class GetProductPrice(value: Any)

  case class PricedOrder(value: Any)

  case class PricingError(value: Any)

  object PatternA:
    type ValidateOrder =
      CheckProductCodeExists // 依存
        => CheckAddressExists // 依存
        => UnvalidatedOrder // In
        => EitherT[Future, ValidationError, ValidatedOrder] // Out

    type PriceOrder =
      GetProductPrice // 依存
        => ValidatedOrder // In
        => Either[PricingError, PricedOrder] // Out

  object PatternB:
    type ValidateOrder =
      UnvalidatedOrder // In
        => EitherT[Future, ValidationError, ValidatedOrder] // Out

    type PriceOrder =
      ValidatedOrder // In
        => Either[PricingError, PricedOrder] // Out

  case class PlaceOrder(value: Any)

  case class PlaceOrderEvent(value: Any)

  case class PlaceOrderError(value: Any)

  type PlaceOrderWorkflow =
    PlaceOrder // In
      => EitherT[Future, PlaceOrderError, List[PlaceOrderEvent]]

end Chap0707
