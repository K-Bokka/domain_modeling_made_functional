package Chap0505

object C050501a:
  case class CalculatedPricesInput(orderForm: OrderForm, productCatalog: ProductCatalog)

  type CalculatePrices = CalculatedPricesInput => PricedOrder
