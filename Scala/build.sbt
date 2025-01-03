ThisBuild / organization := "com.example"
ThisBuild / scalaVersion := "3.6.2"
ThisBuild / version      := "0.1.0-SNAPSHOT"

lazy val commonSettings = Seq(
  libraryDependencies += "org.scalameta" %% "munit" % "1.0.3" % Test
)

lazy val root = project
  .in(file("."))
  .settings(commonSettings)
  .aggregate(chap04)

lazy val chap04 = project
  .in(file("chap04"))
  .settings(commonSettings)
