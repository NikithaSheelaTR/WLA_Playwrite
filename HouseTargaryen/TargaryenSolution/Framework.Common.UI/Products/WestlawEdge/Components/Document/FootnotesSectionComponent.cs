namespace Framework.Common.UI.Products.WestlawEdge.Components.Document
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Footnotes Section Component
    /// </summary>
    public class FootnotesSectionComponent : BaseModuleRegressionComponent
    {
        private const string FootnoteLctMask = "//div[@class='co_footnoteNumber']//a[text()='{0}']";
        private const string FootnoteLinkInFootnoteSectionLctMask = "//div[@class='co_footnoteBody']//a[contains(text(),'{0}')]";

        private static readonly By FootnoteSectionHeaderLocator = By.Id("co_footnoteSectionHeader");
        private static readonly By ContainerLocator = By.Id("co_footnoteSection");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Is footnote section header displayed
        /// </summary>
        /// <returns> The <see cref="bool"/>. </returns>
        public bool IsHeaderDisplayed => DriverExtensions.IsDisplayed(FootnoteSectionHeaderLocator, 5);

        /// <summary>
        /// Is footnote section displayed
        /// </summary>
        /// <param name="footnoteName"> The footnote name </param>
        /// <returns> True if displayed </returns>
        public bool IsDisplayed(string footnoteName) =>
            DriverExtensions.IsDisplayed(By.XPath(string.Format(FootnoteLctMask, footnoteName)), 5);

        /// <summary>
        /// Click footnote title
        /// </summary>
        /// <param name="footnoteName"> Footnote Name </param>
        public void ClickTitle(string footnoteName) => DriverExtensions.Click(By.XPath(string.Format(FootnoteLctMask, footnoteName)));

        /// <summary>
        /// Click footnote link in footnote section
        /// </summary>
        /// <param name="linkText"> The link text </param>
        /// <returns> The <see cref="EdgeCommonDocumentPage"/>. </returns>
        public T ClickLink<T>(string linkText) where T : ICreatablePageObject
        {
            DriverExtensions.Click(By.XPath(string.Format(FootnoteLinkInFootnoteSectionLctMask, linkText)));
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}