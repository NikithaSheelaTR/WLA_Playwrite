namespace Framework.Common.UI.Products.WestLawNext.Items.IpItems
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Tree checkBox item
    /// </summary>
    public class BaseClassItem : BaseItem
    {
        private static readonly By CheckBoxLocator = By.XPath("./input");

        private static readonly By ExpandCollapseButtonLocator = By.XPath("./a[contains(@class, 'co_facet_')]");

        private static readonly By LabelLocator = By.XPath("./label");

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseClassItem"/> class. 
        /// The constructor.
        /// </summary>
        /// <param name="container">IWebElement container</param>
        public BaseClassItem(IWebElement container): base(container)
        {
        }

        /// <summary>
        /// Check box
        /// </summary>
        public ICheckBox CheckBox => new CheckBox(this.Container, CheckBoxLocator);

        /// <summary>
        /// Expand collapse button
        /// </summary>
        public IButton ExpandCollapseButton => DriverExtensions.IsDisplayed(this.Container, ExpandCollapseButtonLocator) ? new Button(this.Container, ExpandCollapseButtonLocator): null;

        /// <summary>
        /// Check label
        /// </summary>
        public ILabel Label => new Label(this.Container, LabelLocator);

        /// <summary>
        /// Is item expanded
        /// </summary>
        public bool IsExpanded() => this.ExpandCollapseButton.GetAttribute("class").Equals("co_facet_collapse");
    }
}
