namespace WestlawPrecision.Utilities
{
    using System;
    using System.Collections.Generic;
    using iTextSharp.text.pdf.parser;

    public class ImageRenderListener : IRenderListener
    {
        public List<System.Drawing.Image> Images = new List<System.Drawing.Image>();

        public void BeginTextBlock()
        { }

        public void EndTextBlock()
        { }

        public void RenderText(TextRenderInfo renderInfo)
        { }

        public void RenderImage(ImageRenderInfo renderInfo)
        {
            PdfImageObject imageObject = renderInfo.GetImage();
            if (imageObject == null)
            {
                Console.WriteLine($"Image {renderInfo.GetRef().Number} could not be read.");
            }
            else
            {
                this.Images.Add(imageObject.GetDrawingImage());
            }
        }
    }
}