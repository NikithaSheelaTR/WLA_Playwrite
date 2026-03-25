namespace WestlawPrecision.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Xml;
    using DocumentFormat.OpenXml.Packaging;

    public class DocxTextExtractor
    {
        public static string ExtractTextFromWord(string filePath)
        {
            StringBuilder stringBuilder;
            using (WordprocessingDocument wordprocessingDocument = WordprocessingDocument.Open(filePath, false))
            {
                var nameTable = new NameTable();
                var xmlNamespaceManager = new XmlNamespaceManager(nameTable);
                xmlNamespaceManager.AddNamespace("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");

                string wordprocessingDocumentText;
                using (var streamReader = new StreamReader(wordprocessingDocument.MainDocumentPart.GetStream()))
                {
                    wordprocessingDocumentText = streamReader.ReadToEnd();
                }

                stringBuilder = new StringBuilder(wordprocessingDocumentText.Length);

                var xmlDocument = new XmlDocument(nameTable);
                xmlDocument.LoadXml(wordprocessingDocumentText);

                var paragraphNodes = xmlDocument.SelectNodes("//w:p", xmlNamespaceManager);
                foreach (XmlNode paragraphNode in paragraphNodes)
                {
                    var textNodes = paragraphNode.SelectNodes(".//w:t | .//w:tab | .//w:br", xmlNamespaceManager);
                    foreach (XmlNode textNode in textNodes)
                    {
                        switch (textNode.Name)
                        {
                            case "w:t":
                                stringBuilder.Append(textNode.InnerText);
                                break;

                            case "w:tab":
                                stringBuilder.Append("\t");
                                break;

                            case "w:br":
                                stringBuilder.Append("\v");
                                break;
                        }
                    }
                }
            }

            return stringBuilder.ToString();
        }

        public static List<string> GetLinks(string filePath)
        {
            var links = new List<string>();

            using (var wordDocument = WordprocessingDocument.Open(filePath, true))
            {
                var hyperlinks = wordDocument.MainDocumentPart.HyperlinkRelationships;
                foreach (var hyperlink in hyperlinks)
                {
                    links.Add(hyperlink.Uri.ToString());
                }
            }

            return links;
        }
    }
}
