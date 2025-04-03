using System.Drawing;

namespace PdfConverer.PdfProcessing
{
    public interface IPdfPage : IDisposable
    {
        public IPdf Parent { get; }

        int PageNumber { get; }

        SizeF Size { get; }

        string ImagePath { get; }

        Task<Bitmap?> GetImageAsync();

        Task SavePdfAsync(string saveFilePath);
    }
}