@main def main(): Unit =
  println("Chapter 8")

  C080201.listFunctions foreach :
    fn =>
      val result = fn(100)
      println(s"If 100 is the input, the output is $result")


object C080201:
  val plus3: Int => Int = (x: Int) => x + 3

  val times2: Int => Int = (x: Int) => x * 2

//  val square: Int => Int = (x: Int) => x * x
  def square(x: Int): Int = x * x // 上と同じ。ScalaはLambda式とメソッドでは書き方が違う

  val addThree: Int => Int = plus3

  val listFunctions: List[Int => Int] = List(addThree, times2, square)
