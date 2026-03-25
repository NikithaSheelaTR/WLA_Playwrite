namespace Framework.Common.UI.Products.WestlawEdge.Components.Document
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Documents compare key component
    /// </summary>
   public class CompareKeyLineComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//li[@class='co_statutesCompare_key']");

        private static readonly By KeyLineAddedTextLocator = By.XPath("//li[@class='co_statutesCompare_key']//ins");

        private static readonly By KeyLineDeletedTextLocator = By.XPath("//li[@class='co_statutesCompare_key']//del");

        private static readonly By KeyLineDeletedLocator = By.XPath("//li[@class='co_statutesCompare_key']//del/parent::*");

        private static readonly By KeyLineAddedLocator = By.XPath("//li[@class='co_statutesCompare_key']//ins/parent::*");

        private static readonly By TexViewDeletedTextKeyLineLocator = By.XPath("//li[@class='co_statutesCompare_key']/span[1]");

        private static readonly By TexViewAddedTextKeyLineLocator = By.XPath("//li[@class='co_statutesCompare_key']/span[2]");

        private const string HighlightedAdditionsCssValue = "rgba(190, 237, 228, 1)";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the visible key line text.
        /// (This excludes screen reader and tool tip text.)
        /// </summary>
        /// <returns> The <see cref="string"/>. The key line text. </returns>     
        public string GetKeyLineText()
        {
            IEnumerable<string> keyLineText = DriverExtensions.GetText(ContainerLocator).Split(new[] { "\r\n" }, StringSplitOptions.None);
            IEnumerable<string> filteredKeyLineText = keyLineText.Where(el => !el.Equals("start of deleted text") && !el.Equals("end of deleted text")
                                                                                && !el.Equals("end of added text") && !el.Equals("start of added text")
                                                                                && !el.Contains("Deleted text and added text may include formatting differences."));
            return string.Join(string.Empty, filteredKeyLineText.ToArray());
        }

        /// <summary>
        /// Get deleted key line text 
        /// </summary>
        /// <returns> The <see cref="string"/>. True if element is deleted text view. </returns>
        public string GetDeletedKeyLineText() => DriverExtensions.GetText(TexViewDeletedTextKeyLineLocator);

        /// <summary>
        /// Get added key line text 
        /// </summary>
        /// <returns> The <see cref="string"/>. True if element is deleted text view. </returns>
        public string GetAddedKeyLineText() => DriverExtensions.GetText(TexViewAddedTextKeyLineLocator);

        /// <summary>
        /// Verifies that the element is highlighted.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if element is highlighted. </returns>
        public bool IsKeyLineAddedTextHighlighted()
            => DriverExtensions.GetAttribute("class", KeyLineAddedTextLocator).Contains("co_highlightView") &&
               DriverExtensions.WaitForElement(KeyLineAddedTextLocator).GetCssValue("background-color").Contains(HighlightedAdditionsCssValue);

        /// <summary>
        /// Verifies that the element is Strike Through.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if element is Strike Through. </returns>
        public bool IsKeyLineDeletedTextStrikeThrough()
            => DriverExtensions.GetAttribute("class", KeyLineDeletedTextLocator).Contains("co_highlightView") &&
               DriverExtensions.WaitForElement(KeyLineDeletedTextLocator).GetCssValue("text-decoration").Contains("line-through");

        /// <summary>
        /// Verifies that the additions word in keyLine contains textview markup.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if element is highlighted. </returns>
        public bool IsKeyLineAddedTextView()
        {
            IEnumerable<string> addedText = DriverExtensions.GetText(KeyLineAddedLocator).Split(new[] { "\r\n" }, StringSplitOptions.None);
            IEnumerable<string> filteredAddedText = addedText.Where(el => !el.Equals("start of added text") && !el.Equals("end of added text"));
            string keyLine = string.Join(string.Empty, filteredAddedText.ToArray());
            return keyLine.Contains("<<+") && keyLine.Contains("+>>");
        }

        /// <summary>
        /// Verifies that the deletions word in keyLine contains textview markup
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if element is deleted text view. </returns>
        public bool IsKeyLineDeletedTextView()
        {
            IEnumerable<string> deletedText = DriverExtensions.GetText(KeyLineDeletedLocator).Split(new[] { "\r\n" }, StringSplitOptions.None);
            IEnumerable<string> filteredDeletedTex = deletedText.Where(el => !el.Equals("start of deleted text") && !el.Equals("end of deleted text"));
            string keyLine = string.Join(string.Empty, filteredDeletedTex.ToArray());
            return keyLine.Contains("<<-") && keyLine.Contains("->>");
        }
    }
}
