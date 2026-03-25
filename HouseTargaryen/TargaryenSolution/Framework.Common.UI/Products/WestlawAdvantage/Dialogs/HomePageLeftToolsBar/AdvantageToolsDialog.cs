namespace Framework.Common.UI.Products.WestlawAdvantage.Dialogs.HomePageLeftToolsBar
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.WestlawAdvantage.Dialogs.HomePageLeftToolsBar;
    using OpenQA.Selenium;
    using System.Collections.Generic;

    /// <summary>
    /// Tools Dialog
    /// </summary>
    public class AdvantageToolsDialog : AdvantageBaseDialog
    {
        private static readonly By ToolsElementLocator = By.XPath(".//li//a");
        private static readonly By ContentTypeContainerLocator = By.XPath("//div[contains(@class,'__panelContent')]");

        /// <summary>
        ///  All Tools Items
        /// </summary>
        public IReadOnlyCollection<ILink> ToolsButton => new ElementsCollection<Link>(ContentTypeContainerLocator, ToolsElementLocator);
    }
}
