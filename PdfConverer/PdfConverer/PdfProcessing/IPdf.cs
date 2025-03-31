using System.Drawing;

namespace PdfConverer.PdfProcessing
{
    public interface IPdf
    {
        DpiType Dpi { get; set; }

        int NumberOfPage { get; }

        IEnumerable<IPdfPage> Pages { get; }

        IPdfPage? this[int pageIndex] { get; }

        void Remove(IPdfPage page);
    }
}
