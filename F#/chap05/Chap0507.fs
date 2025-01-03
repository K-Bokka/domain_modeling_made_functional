namespace Chap0507

type Undefined = exn
type InvoiceId = string

module C050702 =
    type UnpaidInvoiceInfo = Undefined
    type PaidInvoiceInfo = Undefined

    type InvoiceInfo =
        | Unpaid of UnpaidInvoiceInfo
        | Paid of PaidInvoiceInfo


    type Invoice =
        { InvoiceId: InvoiceId
          InvoiceInfo: InvoiceInfo }

module C050702a =
    type UnpaidInvoice = { InvoiceId: InvoiceId }
    type PaidInvoice = { InvoiceId: InvoiceId }

    type Invoice =
        | Unpaid of UnpaidInvoice
        | Paid of PaidInvoice
