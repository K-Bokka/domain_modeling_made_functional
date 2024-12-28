val scala3Version = "3.6.2"

lazy val commonSettings = Seq(
  version := "0.1.0-SNAPSHOT",
  scalaVersion := scala3Version,
  libraryDependencies += "org.scalameta" %% "munit" % "1.0.3" % Test
)

lazy val root = project
  .in(file("."))
  .settings(commonSettings)
  .aggregate(chap04)

lazy val chap04 = project
  .in(file("chap04"))
  .settings(commonSettings)
