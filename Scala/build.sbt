ThisBuild / organization := "com.example"
ThisBuild / scalaVersion := "3.6.2"
ThisBuild / version := "0.1.0-SNAPSHOT"

lazy val commonSettings = Seq(
  libraryDependencies += "org.typelevel" %% "cats-core" % "2.12.0",
  libraryDependencies += "org.scalameta" %% "munit" % "1.0.4" % Test,
)

lazy val root = project
  .in(file("."))
  .settings(commonSettings)
  .aggregate(chap04)

lazy val chap04 = project
  .in(file("chap04"))
  .settings(commonSettings)

lazy val chap05 = project
  .in(file("chap05"))
  .settings(commonSettings)

lazy val chap06 = project
  .in(file("chap06"))
  .settings(commonSettings)

lazy val chap07 = project
  .in(file("chap07"))
  .settings(commonSettings)

lazy val chap08 = project
  .in(file("chap08"))
  .settings(commonSettings)

lazy val ordertaking = project
  .in(file("ordertaking"))
  .settings(commonSettings)
