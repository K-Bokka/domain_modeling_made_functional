object Domains:
  case class OrderId private(value: String)

  object OrderId:
    def apply(value: String): OrderId =
      if value.isEmpty
      then throw new Exception("OrderId must not be empty")
      else if value.length > 50 then throw new Exception("OrderId must not be more than 50 characters")
      else new OrderId(value)

end Domains
