namespace Framework.Common.UI.Products.WestlawAdvantage.Dialogs.KnowYourJudge
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Tips for writing your queries dialog
    /// </summary>
    public class TipsForWritingYourQueriesDialog : BaseModuleRegressionDialog
    {
        private static readonly By ContainerLocator = By.XPath("//*[@dialog-title='Tips for writing Know Your Judge queries']");
        private static readonly By CloseButonLocator = By.XPath(".//saf-button-v3[@data-testid='close-tips-button']");
        private const string InputAreaScript = "return(arguments[0].shadowRoot.querySelector('div[class=dialog-title]'));";
        private static readonly By TipsContentLabelLocator = By.XPath(".//p[contains(@class,'ReportFocusTipsModal-module__tipInfo')]");

        /// <summary>
        /// Tips Content label
        /// </summary>
        public ILabel TipsContent => new Label(ContainerLocator, TipsContentLabelLocator);

        /// <summary>
        /// Close button
        /// </summary>
        public IButton CloseButton => new Button(ContainerLocator, CloseButonLocator);

        /// <summary>
        /// Get Tips Modal Header text
        /// </summary>        
        public string GetTipsModalHeader()
        {
            IWebElement TipsModal = DriverExtensions.GetElement(ContainerLocator);
            IWebElement TipsHeader = (IWebElement)DriverExtensions.ExecuteScript(InputAreaScript, TipsModal);
            return TipsHeader.Text;
        }
    }
}

