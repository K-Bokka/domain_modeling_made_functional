package Chap0505

object C050501:
  type ValidateOrder = UnvalidatedOrder => ValidatedOrder

  case class PlaceOrderEvents(
                               acknowledgmentSent: AcknowledgmentSent,
                               orderPlaced: OrderPlaced,
                               billableOrderPlaced: BillableOrderPlaced,
                             )

  type PlaceOrder = UnvalidatedOrder => PlaceOrderEvents

  enum CategorizedMail:
    case Quote(value: QuoteForm)
    case Order(value: OrderForm)

  type CategorizeInboundMail = EnvelopeContents => CategorizedMail
  type CalculatePrices = OrderForm => ProductCatalog => PricedOrder
