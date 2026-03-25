namespace Framework.Common.UI.Products.WestlawEdge.Components.Document
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.StatutesCompare;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Documents compare component
    /// </summary>
    public class CompareDocumentsComponent : BaseModuleRegressionComponent
    {
        private static readonly By AdditionsLocator = By.XPath(".//ins");

        private static readonly By ContainerLocator = By.XPath("//div[contains(@class,'statutesCompare_content')]//div[contains(@id,'co_document_')]");

        private static readonly By DeletionsLocator = By.XPath(".//del");

        private static readonly By DifferenceLocator = By.XPath("//div[@class='co_overlayBox_content']//*[contains(@class,'co_ruleBookRedline')]");

        private const string HighlightedAdditionsCssValue = "rgba(190, 237, 228, 1)";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Verifies that the additions is highlighted.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if the additions is highlighted. </returns>
        public bool AreAllAdditionsHighlighted() => DriverExtensions
            .GetElements(DriverExtensions.GetElement(ContainerLocator), AdditionsLocator)
            .ToList().TrueForAll(add => add.GetAttribute("class").Contains("co_highlightView") &&
                                        add.GetCssValue("background-color").Contains(HighlightedAdditionsCssValue));

        /// <summary>
        /// Verifies that the deletions has strikethrough.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if the additions is highlighted. </returns>
        public bool AreAllDeletionsStrikethrough() => DriverExtensions
            .GetElements(DriverExtensions.GetElement(ContainerLocator), DeletionsLocator)
            .ToList().TrueForAll(add => add.GetAttribute("class").Contains("co_highlightView") &&
                                        add.GetCssValue("text-decoration").Contains("line-through"));

        /// <summary>
        /// Gets count of deletions.
        /// </summary>
        /// <returns> The <see cref="int"/>. Count of deletions. </returns>
        public int GetCountOfDeletions() => DriverExtensions.GetElements(DriverExtensions.GetElement(ContainerLocator), DeletionsLocator).Count;

        /// <summary>
        /// Gets count of additions.
        /// </summary>
        /// <returns> The <see cref="int"/>. Count of additions. </returns>
        public int GetCountOfAdditions() => DriverExtensions.GetElements(DriverExtensions.GetElement(ContainerLocator), AdditionsLocator).Count;

        /// <summary>
        /// Gets list of differences.
        /// </summary>
        /// <returns> List of Differences </returns>
        public List<IWebElement> GetListOfDifferences() => DriverExtensions.GetElements(DifferenceLocator).ToList();

        /// <summary>
        /// Gets Differences text
        /// </summary>
        /// <returns> Differences text </returns>
        public List<string> GetDifferences() => this.GetListOfDifferences().Select(elem => elem.Text).ToList();

        /// <summary>
        /// Gets all deletions
        /// </summary>
        /// <returns> Differences text </returns>
         internal IEnumerable<IWebElement> GetAllDeletions() => DriverExtensions.GetElements(ContainerLocator, DeletionsLocator);

        /// <summary>
        /// Gets all additions
        /// </summary>
        /// <returns> Differences text </returns>
        internal IEnumerable<IWebElement> GetAllAdditions() => DriverExtensions.GetElements(ContainerLocator, AdditionsLocator);
    }
}
