namespace Framework.Common.UI.Products.Shared.Dialogs
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Enums;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;

    using OpenQA.Selenium;

    /// <summary>
    /// If after running a query,reult list uploaded > 30 sec
    /// The 'Longer queries' dialog displayed
    /// </summary>
    public class LongerQueriesDialog : BaseModuleRegressionDialog
    {
        private static readonly By ContinueButtonLocator = By.XPath(".//input[@alt='Continue']");
        private static readonly By CloseButtonLocator = By.XPath(".//button[contains(@class,'close')]");
        private static readonly By InfoMessageTextLocator = By.XPath(".//div[contains(@class, 'content')]");
        private static readonly By TitleTextLocator = By.XPath(".//*[@id='co_headerMessage']");

        /// <summary>
        /// Element Container of dialog
        /// </summary>
        private IWebElement Container = DriverExtensions.WaitForElement(
            By.XPath(EnumPropertyModelCache.GetMap<Dialogs, WebElementInfo>()[Dialogs.LongerQueriesDialog].LocatorString));       

        #region Buttons 
        /// <summary>
        /// Continue Button
        /// </summary>
        public IButton ContinueButton => new Button(this.Container, ContinueButtonLocator);

        /// <summary>
        /// Close Continue
        /// </summary>
        public IButton CloseButton => new Button(this.Container, CloseButtonLocator);
        #endregion Buttons

        /// <summary>
        /// InfoMessage
        /// </summary>
        public ILabel InfoMessage => new Label(this.Container, InfoMessageTextLocator);

        /// <summary>
        /// Gets Title text
        /// </summary>
        public ILabel TitleText => new Label(this.Container, TitleTextLocator);
    }
}
