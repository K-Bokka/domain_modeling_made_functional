package Chap0505

import scala.concurrent.Future

object C050502b:
  // 型パラメータはつい T を使ってしまう
  type ValidationResponse[T] = UnvalidatedOrder => Future[Either[T, ValidatedOrder]]
  type ValidateOrder = UnvalidatedOrder => ValidationResponse[Seq[ValidationError]]
