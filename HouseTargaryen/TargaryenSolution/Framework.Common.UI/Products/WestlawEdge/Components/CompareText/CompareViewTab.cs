namespace Framework.Common.UI.Products.WestlawEdge.Components.CompareText
{
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.WestlawEdge.Components.Toolbar;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Raw.WestlawEdge.Items.CompareText;

    using OpenQA.Selenium;

    /// <summary>
    /// Compare View tab
    /// </summary>
    public class CompareViewTab : BaseTabComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[./div[@id='panel_InlineCompareContent']]");
        private static readonly By DeletionsLocator = By.XPath(".//span[contains(@class, 'co_redlineDelete')]");
        private static readonly By AdditionsLocator = By.XPath(".//span[contains(@class, 'co_redlineAdd')]");
        private static readonly By InvertComparisonButtonLocator = By.XPath(".//span[text() = 'Invert comparison']");
        private static readonly By CompareContentItemsLocator = By.XPath(".//div[./span[@class = 'inlineCompareTitle']]");

        /// <summary>
        /// Toolbar
        /// </summary>
        public CompareViewTabToolbarComponent Toolbar => new CompareViewTabToolbarComponent(this.ComponentLocator);

        /// <summary>
        /// Inline compare content items
        /// </summary>
        public ItemsCollection<InlineCompareContentItem> CompareContentItems => new ItemsCollection<InlineCompareContentItem>(this.ComponentLocator, CompareContentItemsLocator);

        /// <summary>
        /// Invert comparison button
        /// </summary>
        public IButton InvertComparisonButton => new CustomEdgeButton(this.ComponentLocator, InvertComparisonButtonLocator);

        /// <summary>
        /// List of deletions
        /// </summary>
        public IReadOnlyCollection<IButton> DeletionsList => new ElementsCollection<Button>(this.ComponentLocator, DeletionsLocator);

        /// <summary>
        /// List of additions
        /// </summary>
        public IReadOnlyCollection<IButton> AdditionsList => new ElementsCollection<Button>(this.ComponentLocator, AdditionsLocator);

        /// <summary>
        /// Tab name
        /// </summary>
        protected override string TabName => "Compare View";

        /// <summary>
        /// Component locator 
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}