import Chap0701.UnvalidatedOrder

import java.time.LocalDateTime

object Chap0701:
  case class UnvalidatedCustomerInfo(value: Any)

  case class UnvalidatedAddress(value: Any)

  case class UnvalidatedOrder(
                               orderId: Int,
                               customerInfo: UnvalidatedCustomerInfo,
                               shippingAddress: UnvalidatedAddress
                             )

  case class Command[A](data: A, timestamp: LocalDateTime, userId: String)

  enum OrderTakingCommand:
    case PlaceOrder(value: Command[UnvalidatedOrder])
    case ChangeOrder(value: Any)
    case CancelOrder(value: Any)

object Chap070101:
  case class PlaceOrder(orderForm: UnvalidatedOrder, timestamp: LocalDateTime, userId: String)
