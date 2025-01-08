package Chap0505


case class UnvalidatedOrder(value: Any)

case class ValidatedOrder(value: Any)

case class AcknowledgmentSent(value: Any)

case class OrderPlaced(value: Any)

case class BillableOrderPlaced(value: Any)

case class QuoteForm(value: Any)

case class OrderForm(value: Any)

case class ProductCatalog(value: Any)

case class PricedOrder(value: Any)

case class EnvelopeContents(value: String)

case class ValidationError(fieldName: String, errorDescription: String)
