namespace Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.NarrowPanel
{
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;

    using OpenQA.Selenium;

    /// <summary>
    /// SetDefaultComponent
    /// </summary>
    public class SetDefaultComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.Id("co_contentTypeDefaultTypes");
        private static readonly By ContentTypeRadioButtonLocator = By.XPath(".//div/ul/li/label");
        private static readonly By CancelButtonLocator = By.XPath(".//a[@class = 'co_multifacet_cancel co_defaultBtn']");
       
        /// <summary>
        /// Content Types radioButtons
        /// </summary>
        public IReadOnlyCollection<IRadiobutton> ContentTypeRadioButtons =>
            new ElementsCollection<Radiobutton>(this.ComponentLocator, ContentTypeRadioButtonLocator);

        /// <summary>
        /// Cancel Button
        /// </summary>
        public IButton CancelButton => new Button(this.ComponentLocator, CancelButtonLocator);

        /// <summary>
        /// Component Locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}
