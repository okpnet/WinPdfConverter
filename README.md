 # WinPdfConverter

**WinPdfConverter** は、PDFファイルの生成と表示を行う Windows アプリケーションです。  
このプロジェクトでは、[QuestPDF](https://www.questpdf.com/) と [Pdfium](https://pdfium.googlesource.com/pdfium/) の NuGet パッケージを使用しています。

---

## 特徴

- **PDF生成**: QuestPDF を使用して、高品質な PDF ドキュメントを簡単に生成できます。
- **PDF表示**: Pdfium を利用して、アプリケーション内で PDF ファイルを直接表示できます。

---

## 必要条件

- .NET 6.0 以上
- Windows 10 以上

---

## インストール手順

1. このリポジトリをクローンします。

   ```bash
   git clone https://github.com/okpnet/WinPdfConverter.git
   ```

2. 必要な NuGet パッケージを復元します。

   ```bash
   dotnet restore
   ```

3. プロジェクトをビルドして実行します。

   ```bash
   dotnet build
   dotnet run
   ```

---

## 使用方法

アプリケーションを起動すると、PDFファイルを生成および表示するオプションが提供されます。  
ユーザーインターフェースの指示に従って操作してください。

---

## 使用ライブラリとライセンス

### QuestPDF

- **ライセンス**: 商用/非商用に応じた独自ライセンス
- **無料で使用可能な条件**:
  - 年間総収益が100万米ドル未満の個人または企業
  - 非営利団体
  - FOSS（オープンソース）プロジェクト

詳しくは [QuestPDF ライセンスページ](https://www.questpdf.com/license) を参照してください。

### Pdfium

- **ライセンス**: Apache License 2.0  
- ソースコードとライセンスについては、[Pdfium 公式リポジトリ](https://pdfium.googlesource.com/pdfium/) をご覧ください。

---

## 貢献について

バグ報告、機能提案、プルリクエストなど、どんな形でも貢献を歓迎します。  
Issue または Pull Request を通じてご連絡ください。

---

## ライセンス

このプロジェクトは [MIT License](LICENSE) の下で提供されています。
```
