import cats.data.EitherT

import java.time.LocalDateTime
import scala.concurrent.Future

object Domain:
  // 入力データ
  case class UnvalidatedCustomer(
                                  name: String,
                                  email: String,
                                )

  case class UnvalidatedAddress(value: String)

  case class UnvalidatedOrder(
                               orderId: String,
                               customerInfo: UnvalidatedCustomer,
                               shippingAddress: UnvalidatedAddress,
                             )

  // 入力コマンド
  case class Command[A](data: A, timestamp: LocalDateTime, userId: String)

  case class PlaceOrderCommand(value: Command[UnvalidatedOrder])

  // パブリックAPI
  enum PlaceOrderEvent:
    case OrderPlaced
    case BillableOrderPlaced
    case AcknowledgmentSent

  case class PlaceOrderError(value: Any)

  type PlaceOrderWorkflow =
    PlaceOrderCommand // In
      => EitherT[Future, PlaceOrderError, Seq[PlaceOrderEvent]] // Out
