using PdfConverer.Helper;
using PdfiumViewer;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Imaging;

namespace PdfConverer.PdfProcessing
{
    /// <summary>
    /// PDFのページイメージ
    /// PDFのビットマップイメージを生成する
    /// </summary>
    public class PdfItemPage : IPdfPage, IDisposable
    {
        /// <summary>
        /// 従属するPDF
        /// </summary>
        public IPdf Parent { get; }
        /// <summary>
        /// PDFのページ番号
        /// </summary>
        [Range(typeof(int), "-1", "1000")]
        public int PageNumber { get; }
        /// <summary>
        /// ページのサイズ
        /// </summary>
        public SizeF Size { get; }
        /// <summary>
        /// 出力したイメージのパス
        /// </summary>
        public string ImagePath { get; }

        private PdfItemPage(PdfItem paretn, int page , SizeF size,string imagePath)
        {
            Parent = paretn;
            PageNumber = page;
            Size = size;
            ImagePath = imagePath;
        }

        public static async Task<PdfItemPage> CreateAsync(PdfItem parent,PdfDocument pdfDocument,int page)
        {
            var result = await Task.Run(() =>
            {
                var pageNum = pdfDocument.PageCount > page && page >= 0 ? page : -1;
                var size = pageNum != -1 ? pdfDocument.PageSizes[pageNum] : new SizeF();
                var imagePath = Path.Combine(parent.ImageDirPath, $"{Guid.NewGuid()}.bmp");
                var dpi = (int)parent.Dpi;
                using var image = pdfDocument.Render(pageNum, dpi, dpi, PdfRenderFlags.CorrectFromDpi);
                image.Save(imagePath, ImageFormat.Bmp);
                return new PdfItemPage(parent, pageNum, size, imagePath);
            });
            return result;
        }

        public async Task SavePdfAsync(string saveFilePath)
        {
            using var stream = new FileStream(ImagePath, FileMode.Open);
            var image = new Bitmap(stream);
            await image.SaveFitImageToPdfAsync(saveFilePath);
            image.Dispose();
        }

        public Task<Bitmap?> GetImageAsync()
        {
            var result = Task.Run(() =>
            {
                try
                {
                    if (!System.IO.File.Exists(ImagePath))
                    {
                        return null;
                    }
                    using var stream = new FileStream(ImagePath, FileMode.Open);
                    var image= new Bitmap(stream);
                    var resultBitmap=(Bitmap)image.Clone();
                    image.Dispose();
                    stream.Close();
                    return resultBitmap;
                }
                catch (Exception ex)
                {
                    return null;
                }
            });
            return result;
        }

        public void Dispose()
        {
            if (System.IO.File.Exists(ImagePath))
            {
                for (int i = 0; i < 10; i++)
                {
                    try
                    {
                        Task.Delay(100).Wait();
                        //GC.Collect();
                        //GC.WaitForPendingFinalizers();
                        System.IO.File.Delete(ImagePath);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"EXCEPTION FILE DELETE RETRY {ImagePath}:{ex.Message}");
                    }
                }
            }
        }
    }
}
