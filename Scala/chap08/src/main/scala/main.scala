import C0803.NonZeroInteger

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

  val add1: Int => Int = C080203.adderGeneratorEx(1)
  println(s"add1(1) : ${add1(1)}")

  val add100: Int => Int = C080203.adderGenerator(100)
  println(s"add100(2) : ${add100(2)}")

  C080205.sayHello("Alice")
  C080205.sayGoodbye("Alice")

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

object C080203:
  def add1(x: Int): Int = x + 1

  def add2(x: Int): Int = x + 2

  def add3(x: Int): Int = x + 3

  def adderGenerator(num: Int): Int => Int = (x: Int) => x + num

  def adderGeneratorEx(num: Int): Int => Int =
    def innerFn(x: Int) = x + num

    innerFn

object C080204:
  def add(x: Int, y: Int): Int = x + y

  def adderGenerator(x: Int): Int => Int = add(x, _)

object C080205:
  def sayGreeting(greeting: String, name: String): Unit = println(s"$greeting, $name")

  val sayHello: String => Unit = sayGreeting("Hello", _)

  val sayGoodbye: String => Unit = sayGreeting("Goodbye", _)

object C0803:
  def twelveDivideBy(n: Int): Int = n match
    case 6 => 2
    case 5 => 2
    case 4 => 3
    case 3 => 4
    case 2 => 6
    case 1 => 12
    case 0 => throw new Exception("Can't divide by zero")
    case _ => throw new Exception("Unknown input")

  case class NonZeroInteger private(value: Int)

  def twelveDivideBy2(n: NonZeroInteger): Int = n match
    case NonZeroInteger(6) => 2
    case NonZeroInteger(5) => 2
    case NonZeroInteger(4) => 3
    case NonZeroInteger(3) => 4
    case NonZeroInteger(2) => 6
    case NonZeroInteger(1) => 12
    case NonZeroInteger(0) => throw new Exception("Can't divide by zero") // 制限はまだかけてないが、ここには来ない
    case _ => throw new Exception("Unknown input") // こっちも

  def twelveDivideBy3(n: Int): Option[Int] = n match
    case 6 => Some(2)
    case 5 => Some(2)
    case 4 => Some(3)
    case 3 => Some(4)
    case 2 => Some(6)
    case 1 => Some(12)
    case 0 => None
    case _ => None
