namespace Framework.Common.UI.Products.Shared.Dialogs
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;

    using OpenQA.Selenium;

    /// <summary>
    /// 'Extend session?' dialog.
    /// Appears when the session is about to expire.
    /// </summary>
    public class ExtendSessionDialog : BaseModuleRegressionDialog
    {
        private static readonly By ExtendSessionDialogContainerLocator = By.XPath("//*[@id='co_websiteExtendSessionDialog'] | //div[@id='co_suspendBilling']");

        private static readonly By ExtentSessionButtonLocator = By.XPath(".//*[@id='coid_extendSessionButton'] | .//*[@id='co_suspendBillingContinueButton']");

        private static readonly By SignOutButtonLocator = By.XPath(".//button[@class='co_defaultBtn' and @name='SignOut']");

        /// <summary>
        /// Resume session button
        /// </summary>
        public IButton ResumeSessionButton => new Button(ExtendSessionDialogContainerLocator, ExtentSessionButtonLocator);

        /// <summary>
        /// Sign out button
        /// </summary>
        public IButton SignOutButton => new Button(ExtendSessionDialogContainerLocator, SignOutButtonLocator);
    }
}
