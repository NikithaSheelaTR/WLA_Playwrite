namespace Framework.Common.UI.Products.WestlawEdge.Components.CompareText
{
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Base content component for the LeftContentComponent and RightContentComponent
    /// </summary>
    public abstract class BaseContentComponent : BaseModuleRegressionComponent
    {
        private static readonly By HeadingLocator = By.XPath("./h4");
        private static readonly By SimilaritiesLocator = By.XPath(".//span[contains(@class, 'CompareVersionsHighlight--similar')]");
        private static readonly By DifferencesLocator = By.XPath(".//span[contains(@class, 'CompareVersionsHighlight--different')]");
        private static readonly By MetadataLocator = By.XPath(".//div[@class = 'Meta']");

        /// <summary>
        /// Navigation component
        /// </summary>
        public CompareTextNavigationComponent NavigationComponent => new CompareTextNavigationComponent(this.ComponentLocator);

        /// <summary>
        /// Heading
        /// </summary>
        public ILabel Heading => new Label(this.ComponentLocator, HeadingLocator);

        /// <summary>
        /// Metadata
        /// </summary>
        public ILabel Metadata => new Label(this.ComponentLocator, MetadataLocator);

        /// <summary>
        /// List of Similarities
        /// </summary>
        public IReadOnlyCollection<IButton> SimilaritiesList =>
            new ElementsCollection<Button>(this.ComponentLocator, SimilaritiesLocator);

        /// <summary>
        /// List of Differences
        /// </summary>
        public IReadOnlyCollection<IButton> DifferencesList =>
            new ElementsCollection<Button>(this.ComponentLocator, DifferencesLocator);

        /// <summary>
        /// Get text of a focused item
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>Text of focused item.</returns>
        public string GetFocusedItemText() => DriverExtensions.GetFocusedElement().Text;
    }
}