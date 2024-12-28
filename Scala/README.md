# 説明

このディレクトリでは Scala を使って写経します

## Scala のインストール方法

基本的に sbt を使うのでその手順に従う

https://www.scala-sbt.org/1.x/docs/ja/Installing-sbt-on-Mac.html

### Java のインストール
使ったことがないので SDKMAN を採用

https://sdkman.io/

```console
$ curl -s "https://get.sdkman.io" | bash
$ source "$HOME/.sdkman/bin/sdkman-init.sh"
$ sdk list java
================================================================================
Available Java Versions for macOS ARM 64bit
================================================================================
 Vendor        | Use | Version      | Dist    | Status     | Identifier
--------------------------------------------------------------------------------
 Corretto      |     | 23.0.1       | amzn    |            | 23.0.1-amzn         
               |     | 21.0.5       | amzn    |            | 21.0.5-amzn         
               |     | 17.0.13      | amzn    |            | 17.0.13-amzn        
               |     | 11.0.25      | amzn    |            | 11.0.25-amzn        
               |     | 8.0.432      | amzn    |            | 8.0.432-amzn        
 Gluon         |     | 22.1.0.1.r17 | gln     |            | 22.1.0.1.r17-gln    
               |     | 22.1.0.1.r11 | gln     |            | 22.1.0.1.r11-gln    
...

$ sdk install java 17.0.13-tem
$ java -version
openjdk version "17.0.13" 2024-10-15
OpenJDK Runtime Environment Temurin-17.0.13+11 (build 17.0.13+11)
OpenJDK 64-Bit Server VM Temurin-17.0.13+11 (build 17.0.13+11, mixed mode, sharing)
```

### Scala のインストール

brew のインストールは割愛

https://www.scala-lang.org/download/

```console
$ brew install coursier/formulas/coursier && cs setup
$ source ~/.zprofile
$ scala -version
Scala code runner version: 1.5.4
Scala version (default): 3.6.2    scalafmt         scandeps5.34.pl  scp              screencapture    scselect         
$ sbt -version                                       
copying runtime jar...
[info] [launcher] getting org.scala-sbt sbt 1.10.5  (this may take some time)...
[info] [launcher] getting Scala 2.12.20 (for sbt)...
sbt version in this project: 1.10.5
sbt script version: 1.10.5
```

## 構成

```console
.
├── scala/  <- ベースディレクトリ
│   ├── README.md
│   └── chap04/ <- サブプロジェクト
│       └── src/
```

### 作り方

とりあえず、テンプレートを使ってディレクトリを作成
```console
$ cd <this repository root>
$ sbt new scala/scala3.g8
SLF4J: Failed to load class "org.slf4j.impl.StaticLoggerBinder".
SLF4J: Defaulting to no-operation (NOP) logger implementation
SLF4J: See http://www.slf4j.org/codes.html#StaticLoggerBinder for further details.
A template to demonstrate a minimal Scala 3 application 

name [Scala 3 Project Template]: Scala

Template applied in <this repository root>/./scala

$ cd scala 
$ sbt clean compile run
[info] welcome to sbt 1.10.7 (Eclipse Adoptium Java 17.0.13)

...

[success] Total time: 1 s, completed 2024/12/28 11:08:46
[info] running hello 
Hello world!
I was compiled by Scala 3. :)
[success] Total time: 0 s, completed 2024/12/28 11:08:46
$ sbt clean compile test
[info] welcome to sbt 1.10.7 (Eclipse Adoptium Java 17.0.13)

...

MySuite:
  + example test that succeeds 0.003s
[info] Passed: Total 1, Failed 0, Errors 0, Passed 1
[success] Total time: 1 s, completed 2024/12/28 11:08:53
```

### IntelliJ での設定

紆余曲折があったが、素直に <repo-root>/scala を project として開くのが一番楽だった
