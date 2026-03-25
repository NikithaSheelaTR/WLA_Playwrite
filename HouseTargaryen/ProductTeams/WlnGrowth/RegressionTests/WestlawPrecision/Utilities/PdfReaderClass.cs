namespace WestlawPrecision.Utilities
{
    using System.Collections.Generic;
    using iTextSharp.text.pdf;
    using iTextSharp.text.pdf.parser;

    using Image = System.Drawing.Image;
    using Path = System.IO.Path;

    public static class PdfReaderClass
    {
        public static List<Image> GetPicturesFromPdf(string path, string fileName)
        {
            var pdfReader = new PdfReader(new ReaderProperties().SetPartialRead(true), Path.Combine(path, fileName));
            var parser = new PdfReaderContentParser(pdfReader);
            var images = new List<Image>();
            for (int i = 1; i < pdfReader.NumberOfPages; i++)
            {
                images.AddRange(parser.ProcessContent(i, new ImageRenderListener()).Images);
            }
            pdfReader.Dispose();
            return images;
        }
    }
}
