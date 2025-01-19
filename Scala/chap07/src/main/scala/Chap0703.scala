import Chap0703.ShoppingCart.{ActiveCart, EmptyCart, PaidCart}

object Chap0703:
  case class Item(value: Any)

  case class ActiveCartData(unpaidItems: List[Item])

  case class PaidCartData(paidItems: List[Item], payment: Float)

  enum ShoppingCart:
    case EmptyCart
    case ActiveCart(data: ActiveCartData)
    case PaidCart(data: PaidCartData)

  def addItem(cart: ShoppingCart, item: Item): ShoppingCart = cart match
    case EmptyCart => ActiveCart(ActiveCartData(List(item)))
    case ActiveCart(data) => ActiveCart(data.copy(unpaidItems = item :: data.unpaidItems))
    case PaidCart(_) => cart

  def makePayment(cart: ShoppingCart, payment: Float): ShoppingCart = cart match
    case EmptyCart => cart
    case ActiveCart(data) => PaidCart(PaidCartData(data.unpaidItems, payment))
    case PaidCart(_) => cart
