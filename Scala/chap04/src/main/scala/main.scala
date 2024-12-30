@main def hello(): Unit =
  println("Chapter 4.1")
  println(s"ex) add1 2 -> ${Chap0401.add1(2)}")
  println(s"ex) add 2 4 -> ${Chap0401.add(2, 4)}")
  println(s"ex) squarePlusOne 3 -> ${Chap0401.squarePlusOne(3)}")
  println(s"ex) areEqual 2 3 -> ${Chap0401.areEqual(2, 3)}")
  println(s"ex) areEqual 4.2 4.2 -> ${Chap0401.areEqual(4.2, 4.2)}")
  println()
