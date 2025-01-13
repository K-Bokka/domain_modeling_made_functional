import scala.util.chaining.scalaUtilChainingOps

object Chap0605:
  case class OrderLine(id: Int, price: Float)

  case class Order(lines: Seq[OrderLine], amountToBill: Float)

  private def findOrderLine(orderLineId: Int, lines: Seq[OrderLine]): Either[String, OrderLine] =
    lines.find(_.id == orderLineId) match
      case Some(line) => Right(line)
      case None => Left(s"OrderLine with id $orderLineId not found")

  private def replaceOrderLine(orderLineId: Int, newLine: OrderLine, lines: Seq[OrderLine]): Seq[OrderLine] =
    lines.map { line => if line.id == orderLineId then newLine else line }

  def changeOrderLinePrice(order: Order, orderLineId: Int, newPrice: Float): Either[String, Order] =
    for {
      // Either lane
      orderLine <- findOrderLine(orderLineId, order.lines)
    } yield {
      orderLine.copy(price = newPrice)
        .pipe(replaceOrderLine(orderLineId, _, order.lines))
        .pipe(lines => Order(lines, lines.map(_.price).sum))
    }
