import sun.security.util.Length

@main def main(): Unit =
  println("Chapter 6.1")

  // コンストラクタを潰した後に再実装している
  // 別にオーバーライドするだけでも良さそうではある
  // やろうと思えば似たような書き方もできるけど...
  val unitQty = UnitQuantity(1)

  unitQty match
    case Left(msg) => println(s"Failure. Message is $msg")
    case Right(qty) =>
      println(s"Success. Value is $qty")
      val innerValue = qty.value // コンストラクタをオーバーライドしただけなので、パラメータのアクセさは残ってる
      println(s"Inner value is $innerValue")
