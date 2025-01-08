package Chap0505

import scala.concurrent.Future

object C050502a:
  // 非同期処理は Future を使う
  type ValidateOrder = UnvalidatedOrder => Future[Either[Seq[ValidationError], ValidatedOrder]]
