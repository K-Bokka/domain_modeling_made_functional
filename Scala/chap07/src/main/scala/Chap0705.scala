import scala.concurrent.Future

object Chap070501:
  case class UnvalidatedAddress(Value: Any)

  case class CheckedAddress(Value: Any)

  case class AddressValidationError(Value: Any)

  type FutureEither[A, B] = Future[Either[A, B]]
  type CheckAddressExists = UnvalidatedAddress => FutureEither[AddressValidationError, CheckedAddress]

  case class CheckProductCodeExists(Value: Any)

  case class UnvalidatedOrder(Value: Any)

  case class ValidatedOrder(Value: Any)

  case class ValidationError(Value: Any)

  type ValidateOrder =
    CheckProductCodeExists // 依存
      => CheckAddressExists // 依存
      => UnvalidatedOrder // In
      => FutureEither[ValidationError, ValidatedOrder] // Out

object Chap070502:

  import Chap070501.ValidatedOrder

  case class PricingError(Value: String)

  case class GetProductPrice(Value: Any)

  case class PricedOrder(Value: Any)

  type PriceOrder =
    GetProductPrice // 依存
      => ValidatedOrder // In
      => Either[PricingError, PricedOrder] // Out

object Chap070503:

  import Chap070502.PricedOrder

  case class OrderAcknowledgement(Value: Any)

  case class SendResult(Value: Any)

  type SendOrderAcknowledgment = OrderAcknowledgement => Future[SendResult]

  case class CreateOrderAcknowledgmentLetter(Value: Any)

  case class OrderAcknowledgmentSent(Value: Any)

  type AcknowledgeOrder =
    CreateOrderAcknowledgmentLetter // 依存
      => SendOrderAcknowledgment // 依存
      => PricedOrder // In
      => Future[Option[OrderAcknowledgmentSent]] // Out
