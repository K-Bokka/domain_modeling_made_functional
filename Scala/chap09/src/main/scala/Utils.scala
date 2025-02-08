import Domains.*

import scala.annotation.targetName

object Utils:
  object Pipe:
    extension [A](a: A) @targetName("pipe") def |>[B](f: A => B): B = f(a)
  end Pipe

  case class String50 private(value: String)

  object String50:
    def apply(value: String): String50 =
      if value.isEmpty then throw new IllegalArgumentException("String cannot be empty")
      else if value.length > 50 then throw new IllegalArgumentException("String cannot be longer than 50 characters")
      else new String50(value)

    def applyOpt(value: String): Option[String50] =
      if value.isEmpty then None
      else if value.length > 50 then None
      else Some(new String50(value))
end Utils
