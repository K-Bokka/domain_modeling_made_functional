object Chap0401:
  def add1(x: Int): Int = x + 1

  def add(x: Int, y: Int): Int = x + y

  def squarePlusOne(x: Int): Int =
    def square(x: Int): Int = x * x

    square(x) + 1

  def areEqual[T](x: T, y: T): Boolean = x == y
