// Scala で書くならこんな感じだと思う
// けど、素直に Cats あたりを使う方が良さそう
case class NonEmptyList[+A](head: A, tail: List[A])
