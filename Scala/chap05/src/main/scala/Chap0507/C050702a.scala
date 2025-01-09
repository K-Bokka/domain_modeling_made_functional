package Chap0507

object C050702a:
  enum Invoice:
    case Unpaid(invoiceId: InvoiceId)
    case Paid(invoiceId: InvoiceId)
