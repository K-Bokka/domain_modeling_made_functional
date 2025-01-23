@main def main(): Unit =
  println("Chapter 8")

  C080201.listFunctions foreach :
    fn =>
      val result = fn(100)
      println(s"If 100 is the input, the output is $result")

  def res1 = C080202.evalWith5ThenAdd2(C080202.add1)

  println(s"If add1 function is the input, the output is $res1")

  def res2 = C080202.evalWith5ThenAdd2(C080202.square)

  println(s"If square function is the input, the output is $res2")

object C080201:
  val plus3: Int => Int = (x: Int) => x + 3

  val times2: Int => Int = (x: Int) => x * 2

  //  val square: Int => Int = (x: Int) => x * x
  def square(x: Int): Int = x * x // 上と同じ。ScalaはLambda式とメソッドでは書き方が違う

  val addThree: Int => Int = plus3

  val listFunctions: List[Int => Int] = List(addThree, times2, square)

object C080202:
  def evalWith5ThenAdd2(fn: Int => Int): Int = fn(5) + 2

  def add1(x: Int): Int = x + 1

  def square(x: Int): Int = x * x
