package Chap0507

object C050702:
  enum InvoiceInfo:
    case Unpaid(value: Any)
    case Paid(value: Any)

  case class InvoiceId(value: Any)

  case class Invoice(invoiceId: InvoiceId, invoiceInfo: InvoiceInfo)