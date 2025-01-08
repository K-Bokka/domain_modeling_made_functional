package Chap0505

object C050502:
  // Left がエラーなのでResultとは位置が変わる。あと、型消去が問題になりそう
  type ValidateOrder = UnvalidatedOrder => Either[Seq[ValidationError], ValidatedOrder]
