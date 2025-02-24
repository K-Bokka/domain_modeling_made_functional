printfn "Chapter 12"

type Undefined = Undefined of string
let notImplemented _ = failwith "Not impl"
type AsyncResult<'success, 'failure> = Async<Result<'success, 'failure>>

module C1201 =
    // 純粋ではない
    type Invoice =
        | Invoice of unit

        member this.ApplyPayment _ = notImplemented ()
        member this.IsFullyPaid = true

    let loadInvoiceFromDatabase _ : Invoice = notImplemented ()
    let markAsFullyPaidInDb _ = notImplemented ()
    let markAsPartiallyPaidInDb _ = notImplemented ()
    let postInvoicePaidEvent _ = notImplemented ()

    let payInvoice invoiceId payment =
        let invoice: Invoice = loadInvoiceFromDatabase invoiceId

        invoice.ApplyPayment payment

        if invoice.IsFullyPaid then
            markAsFullyPaidInDb payment
            postInvoicePaidEvent payment
        else
            markAsPartiallyPaidInDb payment

    // 純粋
    type InvoicePaymentResult =
        | FullyPaid
        | PartiallyPaid of Undefined

    let applyPayment _ = notImplemented ()
    let isFullyPaid _ = notImplemented ()

    let rec applyPaymentToInvoice unpaidInvoice payment : InvoicePaymentResult =
        let updatedInvoice = unpaidInvoice |> applyPayment payment

        if isFullyPaid updatedInvoice then
            FullyPaid
        else
            PartiallyPaid updatedInvoice

    type PayInvoiceCommand =
        { InvoiceId: Undefined
          Payment: Undefined }

    let payInvoice' payInvoiceCommand =
        let invoiceId = payInvoiceCommand.InvoiceId
        let unpaidInvoice = loadInvoiceFromDatabase invoiceId
        let payment = payInvoiceCommand.Payment
        let paymentResult = applyPaymentToInvoice unpaidInvoice payment

        match paymentResult with
        | FullyPaid ->
            markAsFullyPaidInDb invoiceId
            postInvoicePaidEvent invoiceId
        | PartiallyPaid updatedInvoice -> markAsPartiallyPaidInDb updatedInvoice

    // DBアクセス部分を外部から注入
    let payInvoice'' loadUnpaidInvoiceFromDatabase markAsFullyPaidInDb markAsPartiallyPaidInDb payInvoiceCommand =
        let invoiceId = payInvoiceCommand.InvoiceId
        let unpaidInvoice = loadUnpaidInvoiceFromDatabase invoiceId
        let payment = payInvoiceCommand.Payment

        let paymentResult = applyPaymentToInvoice unpaidInvoice payment

        match paymentResult with
        | FullyPaid ->
            markAsFullyPaidInDb invoiceId
            postInvoicePaidEvent invoiceId
        | PartiallyPaid updatedInvoice -> markAsPartiallyPaidInDb updatedInvoice

module C1202 =
    type DataStoreState = Undefined
    type Data = Undefined
    type NewDataStoreState = Undefined
    type Query = Undefined
    type Key = Undefined

    module CQS =
        type InsertData = DataStoreState -> Data -> NewDataStoreState
        type ReadData = DataStoreState -> Query -> Data
        type UpdateData = DataStoreState -> Data -> NewDataStoreState
        type DeleteData = DataStoreState -> Key -> NewDataStoreState

    type DbConnection = Undefined

    module UseDbConnection =
        type InsertData = DbConnection -> Data -> Unit
        type ReadData = DbConnection -> Query -> Data
        type UpdateData = DbConnection -> Data -> Unit
        type DeleteData = DbConnection -> Key -> Unit

    module DIDbConnection =
        type InsertData = Data -> Unit
        type ReadData = Query -> Data
        type UpdateData = Data -> Unit
        type DeleteData = Key -> Unit

    type DbError = Undefined
    type DbResult<'a> = AsyncResult<'a, DbError>

    module UseAsyncResult =
        type InsertData = Data -> DbResult<Unit>
        type ReadData = Query -> DbResult<Data>
        type UpdateData = Data -> DbResult<Unit>
        type DeleteData = Key -> DbResult<Unit>
        
