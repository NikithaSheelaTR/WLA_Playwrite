namespace Framework.Common.UI.Products.Shared.Components.Document
{
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Enums.Document;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.Shared.Pages.Document;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Footnotes
    /// </summary>
    public class FootnotesComponent : BaseModuleRegressionComponent
    {
        private const string FootnoteLctMask = "//div[@class='co_footnoteNumber']//a[text()='{0}']";

        private const string FootnoteLinkInFootnoteSectionLctMask = "//div[@class='co_footnoteBody']//a[contains(text(),{0})]";

        private static readonly By FootnoteSectionHeaderLocator = By.XPath("//h2[@class='co_footnoteSectionTitle co_printHeading']");

        private static readonly By ContainerLocator = By.Id("co_footnoteSection");

        /// <summary>
        /// Gets the DocumentSection enumeration to WebElementInfo map.
        /// </summary>
        private EnumPropertyMapper<DocumentSection, WebElementInfo> DocSectionsMap => EnumPropertyModelCache.GetMap<DocumentSection, WebElementInfo>();

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// The is footnote section header displayed.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsFootnoteSectionHeaderDisplayed() => DriverExtensions.IsDisplayed(FootnoteSectionHeaderLocator);

        /// <summary>
        /// The is section footnote displayed.
        /// </summary>
        /// <param name="footnoteName">
        /// The footnote name.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsFootnoteNumberLinkDisplayed(string footnoteName)
            => DriverExtensions.IsDisplayed(By.XPath(string.Format(FootnoteLctMask, footnoteName)));

        /// <summary>
        /// The click footnote title in footnote section.
        /// </summary>
        /// <param name="footnoteName">
        /// The footnote name.
        /// </param>
        public void ClickFootnoteNumberLink(string footnoteName) =>
            DriverExtensions.GetElement(By.XPath(string.Format(FootnoteLctMask, footnoteName))).Click();

        /// <summary>
        /// The click footnote link in footnote section.
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="linkText"> The link text. </param>
        /// <returns> New instance of the page </returns>
        public T ClickLink<T>(string linkText) where T : DocumentWithFootnotesPage
        {
            By link = SafeXpath.BySafeXpath(FootnoteLinkInFootnoteSectionLctMask, linkText);
            DriverExtensions.ScrollTo(link);
            DriverExtensions.GetElement(link).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Get footnote text by index
        /// </summary>
        /// <param name="itemIndex">Index of the footnote</param>
        /// <returns>Footnote text</returns>
        public string GetFootnoteTextByIndex(int itemIndex) =>
            DriverExtensions.GetElements(
                                this.ComponentLocator,
                                By.CssSelector(this.DocSectionsMap[DocumentSection.ParagraphText].LocatorString))
                            .ElementAt(itemIndex).Text;
    }
}