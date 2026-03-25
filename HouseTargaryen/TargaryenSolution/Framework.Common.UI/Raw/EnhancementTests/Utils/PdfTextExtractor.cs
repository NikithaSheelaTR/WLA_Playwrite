namespace Framework.Common.UI.Raw.EnhancementTests.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Framework.Core.Utils;

    using java.awt.geom;
    using java.io;
    using java.lang;
    using java.util;

    using org.apache.pdfbox.pdmodel;
    using org.apache.pdfbox.pdmodel.common;
    using org.apache.pdfbox.pdmodel.interactive.action.type;
    using org.apache.pdfbox.pdmodel.interactive.annotation;
    using org.apache.pdfbox.pdmodel.interactive.documentnavigation.destination;
    using org.apache.pdfbox.util;

    using PdfSharp.Pdf;
    using PdfSharp.Pdf.Annotations;
    using PdfSharp.Pdf.Content.Objects;
    using PdfSharp.Pdf.IO;

    using StringBuilder = System.Text.StringBuilder;

    /// <summary>
    /// The PDF text extractor.
    /// </summary>
    public static class PdfTextExtractor
    {
        /// <summary>
        /// The extract text from PDF.
        /// </summary>
        /// <param name="filePath">
        /// The file path.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ExtractTextFromPdf(string filePath)
        {
            PDDocument doc = null;
            try
            {
                doc = PDDocument.load(filePath);
                var stripper = new PDFTextStripper();
                return stripper.getText(doc);
            }
            finally
            {
                if (doc != null)
                {
                    doc.close();
                }
            }
        }

        /// <summary>
        /// Get links.
        /// </summary>
        /// <param name="pdfFileName">
        /// The PDF file name.
        /// </param>
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        public static List<string> GetLinks(string pdfFileName)
        {
            using (PdfDocument document = PdfReader.Open(pdfFileName, PdfDocumentOpenMode.ReadOnly))
            {
                var result = new List<string>();
                foreach (PdfPage page in document.Pages.OfType<PdfPage>())
                {
                    foreach (var element in page.Annotations)
                    {
                        if (element is PdfAnnotation)
                        {
                            var value = element as PdfAnnotation;
                            PdfItem item = value.Elements.GetValue("/A");
                            var pdfDictionary = item as PdfDictionary;
                            if (pdfDictionary != null)
                            {
                                var dictionaryValue = pdfDictionary.Elements.GetValue("/URI");
                                if (dictionaryValue != null)
                                {
                                    string url = dictionaryValue.ToString();
                                    if (!string.IsNullOrWhiteSpace(url))
                                    {
                                        result.Add(url);
                                    }
                                }
                            }
                        }
                    }
                }

                return result;
            }
        }

        /// <summary>
        /// Get links.
        /// </summary>
        /// <param name="pdfFileName">
        /// The PDF file name.
        /// </param>
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        public static Dictionary<string, string> GoToLinks(string pdfFileName)
        {
            var temp = new Dictionary<string, string>();
            PDDocument doc = null;
            try
            {
                doc = PDDocument.load(pdfFileName);
                List allPages = doc.getDocumentCatalog().getAllPages();
                for (var i = 0; i < allPages.size(); i++)
                {
                    var stripper = new PDFTextStripperByArea();
                    var page = (PDPage)allPages.get(i);
                    List annotations = page.getAnnotations();

                    // first setup text extraction regions
                    for (var j = 0; j < annotations.size(); j++)
                    {
                        var annot = (PDAnnotation)annotations.get(j);
                        if (annot is PDAnnotationLink)
                        {
                            var link = (PDAnnotationLink)annot;
                            PDRectangle rect = link.getRectangle();

                            // need to reposition link rectangle to match text space
                            float x = rect.getLowerLeftX();
                            float y = rect.getUpperRightY();
                            float width = rect.getWidth();
                            float height = rect.getHeight();
                            int rotation = page.findRotation();
                            if (rotation == 0)
                            {
                                PDRectangle pageSize = page.findMediaBox();
                                y = pageSize.getHeight() - y;
                            }

                            var awtRect = new Rectangle2D.Float(x, y, width, height);
                            stripper.addRegion(string.Empty + j, awtRect);
                        }
                    }

                    stripper.extractRegions(page);

                    for (var j = 0; j < annotations.size(); j++)
                    {
                        var annot = (PDAnnotation)annotations.get(j);
                        if (annot.GetType() != typeof(PDAnnotationLink))
                        {
                            continue;
                        }

                        var link = (PDAnnotationLink)annot;
                        PDAction action = link.getAction();
                        string urlText = stripper.getTextForRegion(string.Empty + j);

                        if (action.GetType() != typeof(PDActionGoTo))
                        {
                            continue;
                        }

                        var go = (PDActionGoTo)action;
                        var destination = (PDPageXYZDestination)go.getDestination();
                        if (destination.getPageNumber() > -1)
                        {
                            continue;
                        }

                        var stripperByArea = new PDFTextStripperByArea();
                        stripperByArea.setSortByPosition(true);

                        Rectangle2D awtRect = PdfTextExtractor.GetRectangleBelowDestination(destination);
                        stripperByArea.addRegion("goTo", awtRect);

                        stripperByArea.extractRegions(destination.getPage());

                        string goToText = stripperByArea.getTextForRegion("goTo");

                        string key = urlText.RemoveCarriageReturn();
                        string value = Regex.Split(goToText, "\r\n|\r|\n")[0];

                        if (temp.ContainsValue(value))
                        {
                            string oldKey = temp.FirstOrDefault(x => x.Value == value).Key;
                            temp.Remove(oldKey);
                            temp.Add(string.Format("{0} {1}", oldKey, key), value);
                            continue;
                        }

                        if (!temp.ContainsKey(key))
                        {
                            temp.Add(key, value);
                        }
                    }
                }
            }
            catch (FileNotFoundException exception)
            {
                Logger.LogError(exception.getMessage());
            }
            catch (IllegalArgumentException exception)
            {
                Logger.LogError(exception.getMessage());
            }
            finally
            {
                if (doc != null)
                {
                    doc.close();
                }
            }

            return temp;
        }

        private static void ExtractText(CObject obj, StringBuilder target)
        {
            if (obj is CArray)
            {
                PdfTextExtractor.ExtractText((CArray)obj, target);
            }
            else if (obj is CComment)
            {
                PdfTextExtractor.ExtractText((CComment)obj, target);
            }
            else if (obj is CInteger)
            {
                PdfTextExtractor.ExtractText((CInteger)obj, target);
            }
            else if (obj is CName)
            {
                PdfTextExtractor.ExtractText((CName)obj, target);
            }
            else if (obj is CNumber)
            {
                PdfTextExtractor.ExtractText((CNumber)obj, target);
            }
            else if (obj is COperator)
            {
                PdfTextExtractor.ExtractText((COperator)obj, target);
            }
            else if (obj is CReal)
            {
                PdfTextExtractor.ExtractText((CReal)obj, target);
            }
            else if (obj is CSequence)
            {
                PdfTextExtractor.ExtractText((CSequence)obj, target);
            }
            else if (obj is CString)
            {
                PdfTextExtractor.ExtractText((CString)obj, target);
            }
            else
            {
                throw new NotImplementedException(obj.GetType().AssemblyQualifiedName);
            }
        }

        private static void ExtractText(CArray obj, StringBuilder target)
        {
            foreach (CObject element in obj)
            {
                PdfTextExtractor.ExtractText(element, target);
            }
        }

        /// <summary>
        /// The extract text.
        /// </summary>
        /// <param name="obj">
        /// The object.
        /// </param>
        /// <param name="target">
        /// The target.
        /// </param>
        private static void ExtractText(CComment obj, StringBuilder target)
        {
            /* nothing */
        }

        /// <summary>
        /// The extract text.
        /// </summary>
        /// <param name="obj">
        /// The object.
        /// </param>
        /// <param name="target">
        /// The target.
        /// </param>
        private static void ExtractText(CInteger obj, StringBuilder target)
        {
            /* nothing */
        }

        /// <summary>
        /// The extract text.
        /// </summary>
        /// <param name="obj">
        /// The object.
        /// </param>
        /// <param name="target">
        /// The target.
        /// </param>
        private static void ExtractText(CName obj, StringBuilder target)
        {
            /* nothing */
        }

        /// <summary>
        /// The extract text.
        /// </summary>
        /// <param name="obj">
        /// The object.
        /// </param>
        /// <param name="target">
        /// The target.
        /// </param>
        private static void ExtractText(CNumber obj, StringBuilder target)
        {
            /* nothing */
        }

        /// <summary>
        /// The extract text.
        /// </summary>
        /// <param name="obj">
        /// The object.
        /// </param>
        /// <param name="target">
        /// The target.
        /// </param>
        private static void ExtractText(COperator obj, StringBuilder target)
        {
            if (obj.OpCode.OpCodeName == OpCodeName.Tj || obj.OpCode.OpCodeName == OpCodeName.TJ)
            {
                foreach (CObject element in obj.Operands)
                {
                    PdfTextExtractor.ExtractText(element, target);
                }

                target.Append(" ");
            }

            if (obj.OpCode.OpCodeName == OpCodeName.Q)
            {
                target.Append("\n");

                // target.Replace("\n\n", "\n");
            }
        }

        /// <summary>
        /// The extract text.
        /// </summary>
        /// <param name="obj">
        /// The object.
        /// </param>
        /// <param name="target">
        /// The target.
        /// </param>
        private static void ExtractText(CReal obj, StringBuilder target)
        {
            /* nothing */
        }

        /// <summary>
        /// The extract text.
        /// </summary>
        /// <param name="obj">
        /// The object.
        /// </param>
        /// <param name="target">
        /// The target.
        /// </param>
        private static void ExtractText(CSequence obj, StringBuilder target)
        {
            foreach (CObject element in obj)
            {
                PdfTextExtractor.ExtractText(element, target);
            }
        }

        /// <summary>
        /// The extract text.
        /// </summary>
        /// <param name="obj">
        /// The object.
        /// </param>
        /// <param name="target">
        /// The target.
        /// </param>
        private static void ExtractText(CString obj, StringBuilder target)
        {
            target.Append(obj.Value);
        }

        /// <summary>
        /// The get rectangle below destination.
        /// </summary>
        /// <param name="destination">
        /// The destination.
        /// </param>
        /// <returns>
        /// The <see cref="Rectangle2D"/>.
        /// </returns>
        private static Rectangle2D GetRectangleBelowDestination(PDPageXYZDestination destination)
        {
            PDPage page = destination.getPage();
            PDRectangle pageSize = page.findMediaBox();
            float x = destination.getLeft();
            float y = pageSize.getHeight() - destination.getTop();
            float width = pageSize.getWidth();
            float height = destination.getTop();
            return new Rectangle2D.Float(x, y, width, height);
        }

        private static string RemoveCarriageReturn(this string originalString)
        {
            return originalString.TrimEnd('\r', '\n');
        }
    }
}