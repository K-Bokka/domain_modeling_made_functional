import Chap0503.C050301.*
import Chap0507.C050702a.Invoice
import Chap0507.C050702a.Invoice.{Paid, Unpaid}
import Chap0507.C050703.Contact
import Chap0507.C050703a.{Contact => Contact2}
import Chap0507.{ContactId, EmailAddress, InvoiceId, PhoneNumber}

@main def hello(): Unit =
  println("Chapter 5.3")

  val customerId = CustomerId(42)
  val customerId2 = CustomerId2(42)
  val orderId = OrderId(42)
  val orderId2 = OrderId2(42)
  // 比較は F# とかなり動きが違う
  println(s"${customerId == orderId}")
  println(s"${customerId eq orderId}")
  println(s"${customerId2 == orderId2}")
  //  println(s"${customerId2 eq orderId2}")
  println(
    """
      |[error] -- [E008] Not Found Error: ./domain_modeling_made_functional/scala/chap05/src/main/scala/main.scala:13:26
      |[error] 13 |  println(s"${customerId2 eq orderId2}")
      |[error]    |              ^^^^^^^^^^^^^^
      |[error]    |      value eq is not a member of Chap0503.C050301.CustomerId2.
      |""".stripMargin)

  def processCustomerId(id: CustomerId): Unit = println(s"Customer ID is $id")
  //  processCustomerId(orderId)
  println(
    """
      |[error] -- [E007] Type Mismatch Error: ./domain_modeling_made_functional/scala/chap05/src/main/scala/main.scala:22:20
      |[error] 22 |  processCustomerId(orderId)
      |[error]    |                    ^^^^^^^
      |[error]    |                    Found:    (orderId : Chap0503.C050301.OrderId)
      |[error]    |                    Required: Chap0503.C050301.CustomerId
      |[error]    |
      |[error]    | longer explanation available when compiling with `-explain`
      |[error] one error found
      |""".stripMargin)

  val CustomerId(innerValue) = customerId
  // これは動かない
  //  val CustomerId2(innerValue2) = customerId2
  println(s"innerValue is $innerValue")

  println
  println("Chapter 5.6")

  val widgetCode1 = WidgetCode("w1234")
  val widgetCode2 = WidgetCode("w1234")
  println(s"${widgetCode1 == widgetCode2}") // true
  //  eq は参照先が同じかどうかを見るメソッド ここら辺は明確に動作が違うので割愛
  println(s"${widgetCode1 eq widgetCode2}") // false:

  println
  println("Chapter 5.7")

  def printInvoiceId(invoice: Invoice): Unit = invoice match
    case iv: Unpaid => println(s"The unpaid invoiceId is ${iv.invoiceId.value}")
    case iv: Paid => println(s"The paid invoiceId is ${iv.invoiceId.value}")

  val invoice = Paid(InvoiceId("hoge"))

  printInvoiceId(invoice)

  val contactId = ContactId(42)

  val contact1 = Contact(contactId, PhoneNumber("123-456-7890"), EmailAddress("bob@example.com"))
  val contact2 = Contact(contactId, PhoneNumber("123-456-7890"), EmailAddress("robert@example.com"))

  println(s"contact1 = contact2 : ${contact1 == contact2}") // true

  val contact3 = Contact2(contactId, PhoneNumber("123-456-7890"), EmailAddress("bob@example.com"))
  val contact4 = Contact2(contactId, PhoneNumber("123-456-7890"), EmailAddress("robert@example.com"))

  // エラーにはならない。ただ、case class じゃないので、Java の Object.equals が呼び出されている
  println(s"contact3 = contact4 : ${contact3 == contact4}") // false
  // case class じゃないのでプロパティを参照するメソッドはない。普通にエラーになる
  // println(s"contact3.ContactId = contact4.ContactId : ${contact3.contactId == contact4.contactId}")
  // これは比較できる
  println(s"contact3.Key = contact4.Key : ${contact3.key == contact4.key}")
