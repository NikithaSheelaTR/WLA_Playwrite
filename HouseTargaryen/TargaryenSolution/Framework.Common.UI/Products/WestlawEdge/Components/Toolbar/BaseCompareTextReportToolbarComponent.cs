namespace Framework.Common.UI.Products.WestlawEdge.Components.Toolbar
{
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestlawEdge.DropDowns;
    using Framework.Common.UI.Products.WestlawEdge.Elements;

    using OpenQA.Selenium;

    /// <summary>
    /// Base compare text report toolbar
    /// </summary>
    public abstract class BaseCompareTextReportToolbarComponent : BaseModuleRegressionComponent
    {
        private static readonly By HighlightsToggleLocator = By.XPath(".//input[@id ='co_statuteCompare_highlight_toggle']");
        private static readonly By ChangeSelectionsButtonLocator = By.XPath(".//button[text() = 'Change selections']");
        private static readonly By InfoBoxIconLocator = By.XPath(".//span[@class = 'co_moreInfo']");
        private static readonly By InfoBoxMessageLocator = By.XPath(".//div[@class = 'co_infoBox_message']");
        private static readonly By KeyLabelLocator = By.XPath(".//li[@class = 'co_statutesCompare_key']/span");
        
        /// <summary>
        /// Delivery dropdown
        /// </summary>
        public CompareTextDeliveryDropdown DeliveryDropdown => new CompareTextDeliveryDropdown(this.ComponentLocator);

        /// <summary>
        /// Highlights toggle 
        /// </summary>
        public ICheckBox HighlightsToggle => new CheckBox(this.ComponentLocator, HighlightsToggleLocator);

        /// <summary>
        /// Change Selections button
        /// </summary>
        public IButton ChangeSelectionsButton => new Button(this.ComponentLocator, ChangeSelectionsButtonLocator);

        /// <summary>
        /// Key label
        /// </summary>
        public IReadOnlyCollection<ILabel> KeyLabel =>
            new ElementsCollection<Label>(this.ComponentLocator, KeyLabelLocator);

        /// <summary>
        /// Info box icon(next to Key label)
        /// </summary>
        public ILabel InfoBoxIcon => new InfoBoxIconLabel(this.ComponentLocator, InfoBoxIconLocator);

        /// <summary>
        /// Info box message that appears after hovering over info box icon
        /// </summary>
        public ILabel InfoBoxMessage => new Label(this.ComponentLocator, InfoBoxMessageLocator);
    }
}