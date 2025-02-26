open System
open Azure.Storage.Blobs.Specialized

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

    // C120201
    type Customer = Undefined
    type CustomerId = Undefined

    module UseSameType =
        type SaveCustomer = Customer -> DbResult<Unit>
        type LoadCustomer = CustomerId -> DbResult<Customer>

    module WriteModel =
        type Customer = Undefined

    module ReadModel =
        type Customer = Undefined

    module CQRS =
        type SaveCustomer = WriteModel.Customer -> DbResult<Unit>
        type LoadCustomer = CustomerId -> DbResult<ReadModel.Customer>

module Json =

    open Newtonsoft.Json

    let serialize obj = JsonConvert.SerializeObject obj

    let deserialize<'a> str =
        try
            JsonConvert.DeserializeObject<'a> str |> Result.Ok
        with ex ->
            Result.Error ex

module C1204 =
    open Azure.Storage.Blobs

    let connString = "... Azure connection string ..."
    let containerName = "person"

    let blobServiceClient = BlobServiceClient(connString)
    let containerClient = blobServiceClient.GetBlobContainerClient(containerName)


    containerClient.CreateIfNotExists() |> ignore

    type PersonDto = { PersonId: int }

    let savePersonDtoToBlob personDto =
        let blobId = $"Person%i{personDto.PersonId}"
        let blobClient = containerClient.GetBlobClient(blobId)
        let json = Json.serialize personDto
        blobClient.Upload(BinaryData(json), overwrite = true)

module C1205 =
    type CustomerId = CustomerId of int
    type String50 = String50 of string
    type Birthdate = Birthdate of DateTime

    type Customer =
        { CustomerId: CustomerId
          Name: String50
          Birthdate: Birthdate }

    (*>
    CREATE TABLE Customer (
      CustomerId int NOT NULL,
      Name NVARCHAR(50) NOT NULL,
      Birthdate DATETIME NULL,
      CONSTRAINT PK_Customer PRIMARY KEY (CustomerId)
    <*)

    // C120501
    type Contact =
        { ContactId: ContactId
          Info: ContactInfo }

    and ContactInfo =
        | Email of EmailAddress
        | Phone of PhoneNumber

    and ContactId = ContactId of int
    and EmailAddress = EmailAddress of string
    and PhoneNumber = PhoneNumber of string

    (*>
    CREATE TABLE ContactInfo (
       ContactId int NOT NULL,
       IsEmail bit NOT NULL,
       IsPhone bit NOT NULL,
       EmailAddress NVARCHAR(100),
       PhoneNumber NVARCHAR(25),
       CONSTRAINT PK_ContactInfo PRIMARY KEY (ContactId)
    )
    <*)

    (*>
    CREATE TABLE ContactInfo (
       ContactId int NOT NULL,
       IsEmail bit NOT NULL,
       IsPhone bit NOT NULL,
       CONSTRAINT PK_ContactInfo PRIMARY KEY (ContactId)
    )

    CREATE TABLE ContactEmail (
       ContactId int NOT NULL,
       EmailAddress NVARCHAR(100) NOT NULL,
       CONSTRAINT PK_ContactEmail PRIMARY KEY (ContactId)
    )

    CREATE TABLE ContactPhone (
       ContactId int NOT NULL,
       PhoneNumber NVARCHAR(25) NOT NULL,
       CONSTRAINT PK_ContactPhone PRIMARY KEY (ContactId)
    )
    <*)

    // C120502
    (*>
    CREATE TABLE Order (
       OrderId int NOT NULL
       -- ...
    )

    CREATE TABLE OrderLine (
       OrderLineId int NOT NULL,
       OrderId int NOT NULL,
    )
    <*)

    (*>
    CREATE TABLE Order (
       OrderId int NOT NULL,
       -- ...

       ShippingAddress1 varchar(50)
       ShippingAddress2 varchar(50)
       ShippingAddressCity varchar(50)
       -- ...

       BillingAddress1 varchar(50)
       BillingAddress2 varchar(50)
       BillingAddressCity varchar(50)
       -- ...
    )
    <*)
