using PdfiumViewer;

namespace PdfConverer.PdfProcessing
{
    /// <summary>
    /// 
    /// </summary>
    public class PdfItem : IPdf, IPdfFile, IDisposable
    {
        /// <summary>
        /// コピーしたPDFファイルのパス
        /// </summary>
        private string? _filePath;
        /// <summary>
        /// コピー元のPDFファイルのパス
        /// </summary>
        private string? _baseFilePath;
        /// <summary>
        /// ページアイテム
        /// </summary>
        private IList<IPdfPage> _pdfItems = [];
        /// <summary>
        /// ページアイテム
        /// </summary>
        public IEnumerable<IPdfPage> Pages => _pdfItems ?? [];
        /// <summary>
        /// 元のファイルパス
        /// </summary>
        public string? FilePath { get; }
        /// <summary>
        /// PDFドキュメントのページ数
        /// </summary>
        public int NumberOfPage { get; protected set; } = -1;
        /// <summary>
        /// 出力解像度
        /// </summary>
        public DpiType Dpi { get; set; } = DpiType.DPI300;
        /// <summary>
        /// ページにアクセスするためのインデクサ
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public IPdfPage? this[int pageIndex]
        {
            get
            {
                if (0 > pageIndex || pageIndex > NumberOfPage)
                {
                    return null;
                }
                return _pdfItems[pageIndex];
            }
        }

        internal string ImageDirPath { get; }

        public PdfItem(string filePath, string tmpDirPath)
        {
            ImageDirPath = tmpDirPath;
            _filePath = Path.Combine(ImageDirPath,$"{Guid.NewGuid()}.pdf");
            File.Copy(filePath, _filePath, true);
        }

        public async Task InitilizePageAsync(IProgress<(int, int)>? progress = null)
        {
            if (_pdfItems.Any())
            {
                return;
            }
            using var pdfDoc = PdfDocument.Load(_filePath);
            if (pdfDoc is not null)
            {
                NumberOfPage = pdfDoc.PageCount;
                for (var pageIndex = 0; NumberOfPage > pageIndex; pageIndex++)
                {
                    progress?.Report(new(pageIndex, NumberOfPage));
                    var page = await PdfItemPage.CreateAsync(this, pdfDoc, pageIndex);
                    _pdfItems.Add(page);
                }
            }
            else
            {
                File.Delete(_filePath!);
                _filePath = null;
                _baseFilePath = null;
            }
        }
        /// <summary>
        /// ページ削除
        /// </summary>
        /// <param name="page"></param>
        public void Remove(IPdfPage page)
        {
            page.Dispose();
            _pdfItems.Remove(page);
        }

        public void RemoveAll()
        {
            foreach (var page in _pdfItems)
            {
                page.Dispose();
            }
            _pdfItems.Clear();
        }

        public void Dispose()
        {
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
        }
    }
}
