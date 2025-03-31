using System.Drawing;

namespace PdfConverer.PdfProcessing
{
    public interface IPdfPage : IDisposable
    {
        public IPdf Parent { get; }

        int PageNumber { get; }

        SizeF Size { get; }

        Task<Bitmap?> GetImageAsync();
    }
}