import Domain.{UnvalidatedAddress, UnvalidatedOrder}
import cats.data.EitherT

import scala.concurrent.Future

object PlaceOrderWorkflow:

  // 検証済みデータ
  case class ValidatedOrderLine(value: Any)

  case class OrderId(value: Any)

  case class CustomerInfo(value: Any)

  case class Address(value: Any)

  case class ValidatedOrder(
                             orderId: OrderId,
                             customerInfo: CustomerInfo,
                             shippingAddress: Address,
                             billingAddress: Address,
                             orderLines: List[ValidatedOrderLine],
                           )

  // 計算済みデータ
  case class PricedOrderLine(value: Any)

  case class PricedOrder(value: Any)

  // 注文の全状態
  enum Order:
    case Unvalidated(value: UnvalidatedOrder)
    case Validated(value: ValidatedOrder)
    case Priced(value: PricedOrder)

  // 注文の検証
  case class ProductCode(value: Any)

  type CheckProductCodeExists = ProductCode => Boolean

  case class AddressValidationError(value: Any)

  case class CheckedAddress(value: Any)

  type CheckAddressExists = UnvalidatedAddress => EitherT[Future, AddressValidationError, CheckedAddress]

  case class ValidationError(value: Any)

  type ValidateOrder =
    CheckProductCodeExists // 依存
      => CheckAddressExists // 依存
      => UnvalidatedOrder // In
      => EitherT[Future, List[ValidationError], ValidatedOrder] // Out

  // 価格計算
  case class Price(value: Any)

  type GetProductPrice = ProductCode => Price

  case class PricingError(value: Any)

  type PriceOrder =
    GetProductPrice // 依存
      => ValidateOrder // In
      => Either[PricingError, PricedOrder] // Out
