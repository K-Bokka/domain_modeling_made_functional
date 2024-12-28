#  目的
> 関数型ドメインモデリング  
> ドメイン駆動設計とF#でソフトウェアの複雑さに立ち向かおう

https://asciidwango.jp/post/754242099814268928/関数型ドメインモデリング

を写経すること

## 方針

- とりあえず F# で写経する
- それを Scala でも書いてみる

## なぜそんなことを...

- F# は馴染みが薄いし今後あまり使うことがなさそうなので、この本を避けてしまっていた
- しかし、流れ的に読んだ方が良さそうなので、買って少し読んでは見たが、F#の構文に慣れない
- なので、とりあえずF#の構文に慣れるために写経
- さらに、別の言語で同じことを実装するとさらに理解が深まりそう
  - [すごいHaskell 楽しく学ぼう！](https://tatsu-zine.com/books/sugoi-haskell-ja) を読んだ時、最新のHaskellで写経したら身についた気がした
    - https://github.com/K-Bokka/amazing_h_book
- 最近、Scala触ってないし、Scala3触ってみたい

## ディレクトリ構成

こんな感じのディレクトリ構成にする  
それぞれの言語のセットアップ方法などは、それぞれのディレクトリ内のREADMEを参照する
```console
<repo-root>
├── F#/
│   ├── chap04/
│   └── README.md
├── scala/
│   ├── chap04/
│   └── README.md
└── README.md
```

## 実行環境
- マシン
  - MacBook Pro 2024
- ツール
  - F#: Rider
  - Scala: IntelliJ IDEA
