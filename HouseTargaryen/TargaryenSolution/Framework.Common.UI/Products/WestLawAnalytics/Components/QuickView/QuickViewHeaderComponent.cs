namespace Framework.Common.UI.Products.WestLawAnalytics.Components.QuickView
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Represents Header section of QuickView component.
    /// </summary>
    public class QuickViewHeaderComponent : BaseModuleRegressionComponent
    {
        private static readonly By FaqLinkLocator = By.Id("headerControl_iFAQ");

        private static readonly By HelpLinkLocator = By.Id("headerControl_iHelp");

        private static readonly By TransferDropdownLocator = By.Id("headerControl_Transfer");

        private static readonly By TransferOptionSelectedlocator =
            By.XPath("//select[@id='headerControl_Transfer']/.//option[contains(@selected,'')]");

        private static readonly By ContainerLocator = By.Id("tblTransfer");

        /// <summary>
        /// Checks whether transfer dropdown is displayed
        /// </summary>
        /// <returns><see cref="bool"/></returns>
        public bool IsDropdownTransferDisplayed => DriverExtensions.IsDisplayed(TransferDropdownLocator);

        /// <summary>
        /// Checks whether faq is displayed
        /// </summary>
        /// <returns><see cref="bool"/></returns>
        public bool IsFaqLinkDisplayed => DriverExtensions.IsDisplayed(FaqLinkLocator);

        /// <summary>
        /// Checks whether help is displayed
        /// </summary>
        /// <returns><see cref="bool"/></returns>
        public bool IsHelpLinkDisplayed => DriverExtensions.IsDisplayed(HelpLinkLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Returns selected item in dropdown
        /// </summary>
        /// <returns> selected option text </returns>
        public string GetSelectedOptionText() => DriverExtensions.GetText(TransferOptionSelectedlocator);
    }
}