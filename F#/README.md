# 説明

このディレクトリでは F# を使って写経します

## F# のインストール方法

正確には .NET を入れる

https://dotnet.microsoft.com/ja-jp/download ここから pkg をダウンロードして入れるだけ

## 構成

基本、章ごとにプロジェクトを作っていく形にする予定
```console
.
├── F#/  <- ソリューション
│   ├── F#.sln
│   ├── README.md
│   └── chap04/ <- プロジェクト
│       ├── Program.fs
│       ├── bin
│       ├── chap04.fsproj
│       └── obj/
```

### 作り方
```console
$ cd <this repository root>
$ dotnet new sln -o "F#"               
テンプレート "ソリューション ファイル" が正常に作成されました。

$ cd F\#
$ dotnet new gitignore
テンプレート "dotnet gitignore ファイル" が正常に作成されました。

$ dotnet new console -lang F# -n chap04
テンプレート "コンソール アプリ" が正常に作成されました。

作成後の操作を処理しています...
/Users/ak_yama/git/k-bokka/domain_modeling_made_functional/F#/chap04/chap04.fsproj を復元しています:
正常に復元されました。


$ dotnet sln add chap04/chap04.fsproj  
プロジェクト `chap04/chap04.fsproj` をソリューションに追加しました。
```

後は Rider でこのリポジトリを開くと自動で認識して、実行可能な状態まで持って行ってくれます

## REPL の使い方

```console
$ dotnet fsi

Microsoft (R) F# インタラクティブ バージョン F# 9.0 のための 12.9.100.0
Copyright (C) Microsoft Corporation. All rights reserved.

ヘルプを表示するには次を入力してください: #help;;

> #help;;

  F# インタラクティブ ディレクティブ:

    #r "file.dll";;                               // 指定された DLL を参照します (動的読み込み)
    #i "package source uri";;                     // パッケージの検索時にパッケージ ソースの URI を含める
    #I "path";;                                   // 参照されている DLL に対し、指定された検索パスを追加します
    #load "file.fs" ...;;                         // コンパイルおよび参照されているように、指定されたファイルを読み込みます
    #time ["on"|"off"];;                          // タイミングのオンとオフを切り替えます
    #help;;                                       // ヘルプの表示
    #help "idn";;                                 // 識別子のドキュメントを表示する (例:#help "List.map";;
    #r "nuget:FSharp.Data, 3.1.2";;               // NuGet パッケージの読み込み 'FSharp.Data' バージョン '3.1.2'
    #r "nuget:FSharp.Data";;                      // NuGet パッケージの読み込み 'FSharp.Data' 最新バージョン
    #clear;;                                      // 画面をクリアする
    #quit;;                                       // 終了

  F# インタラクティブ コマンド ライン オプション:

      オプションについては 'dotnet fsi --help' を参照してください


> let square x = x *  x;;
val square: x: int -> int

> square 3;;
val it: int = 9

> #quit;;
```