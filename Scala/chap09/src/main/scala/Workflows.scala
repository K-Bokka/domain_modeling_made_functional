import Utils.Pipe._

object Workflows:

  private def validateOrder(order: Any) = ???

  private def priceOrder(order: Any) = ???

  private def acknowledgeOrder(order: Any) = ???

  private def createEvents(order: Any) = ???

  def placeOrder(unvalidatedOrder: Any): Any =
    unvalidatedOrder
      |> validateOrder
      |> priceOrder
      |> acknowledgeOrder
      |> createEvents

end Workflows
