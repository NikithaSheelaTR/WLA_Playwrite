using Framework.Common.UI.Interfaces.Elements;
using Framework.Common.UI.Products.Shared.Dialogs;
namespace Framework.Common.UI.Products.WestlawAdvantage.Dialogs.HomePageLeftToolsBar
{
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// BaseAdvantage Dialog
    /// </summary>
    public class AdvantageBaseDialog : BaseModuleRegressionDialog
    {
        private static readonly By ContentTypeContainerLocator = By.XPath("//div[@role ='dialog']");
        private static readonly By DialogHeaderLocator = By.XPath(".//div[contains(@class, '__panelHeader')]/h2");
        private static readonly By CancelButtonLocator = By.XPath("//div[contains(@class, '__panelHeader')]/saf-button");
       
        /// <summary>
        /// Dialog header
        /// </summary>
        public ILabel DialogHeader = new Label(DialogHeaderLocator);

        /// <summary>
        /// Cancel button
        /// </summary>
        public IButton CancelButton = new Button(CancelButtonLocator);
    }
}
