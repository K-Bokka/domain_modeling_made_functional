import scala.annotation.targetName

object Utils:
  object Pipe:
    extension [A](a: A) @targetName("pipe") def |>[B](f: A => B): B = f(a)
  end Pipe
end Utils
