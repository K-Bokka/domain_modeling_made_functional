import Chap0503.C050301.*

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

  def processCustomerId(id: CustomerId) = println(s"Customer ID is $id")
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
