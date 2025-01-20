import cats.data.EitherT

import scala.concurrent.Future

object Chap0706:
  case class UnvalidatedOrder(value: Any)

  case class ValidatedOrder(value: Any)

  case class ValidationError(value: Any)

  case class PricedOrder(value: Any)

  case class OrderAcknowledgmentSent(value: Any)

  case class PricingError(value: Any)

  case class PlaceOrderEvent(value: Any)

  type ValidateOrder =
    UnvalidatedOrder // In
      => EitherT[Future, ValidationError, ValidatedOrder] // Out

  type PriceOrder =
    ValidatedOrder // In
      => Either[PricingError, PricedOrder] // Out

  type AcknowledgeOrder =
    PricedOrder // In
      => Future[Option[OrderAcknowledgmentSent]] // Out

  type CreateEvents =
    PricedOrder // In
      => List[PlaceOrderEvent] // Out
