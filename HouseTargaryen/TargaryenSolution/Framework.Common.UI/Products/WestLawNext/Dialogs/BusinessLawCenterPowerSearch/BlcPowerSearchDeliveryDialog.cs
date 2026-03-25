namespace Framework.Common.UI.Products.WestLawNext.Dialogs.BusinessLawCenterPowerSearch
{
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The delivery popup.
    /// </summary>
    public class BlcPowerSearchDeliveryDialog : BaseModuleRegressionDialog
    {
        /// <summary>
        /// The download spinner.
        /// </summary>
        protected static readonly By DownloadSpinnerLocator = By.Id("deliverySpinner");

        private static readonly By CancelButtonLocator = By.LinkText("Cancel");

        private static readonly By LayoutAndLimitsLocator = By.LinkText("Layout and Limits");

        private static readonly By LayoutAndLimitsSelectCoverPageLocator = By.Id("coid_chkDdcLayoutCoverPage");

        /// <summary>
        /// Initializes a new instance of the <see cref="BlcPowerSearchDeliveryDialog"/> class.
        /// </summary>
        public BlcPowerSearchDeliveryDialog()
        {
            DriverExtensions.WaitForElementDisplayed(CancelButtonLocator);
        }

        /// <summary>
        /// ClickOnSelectCoverPage - Sets Or Clears Cover Page check box in Layout and Limits tab.
        /// </summary>
        public void ClickOnSelectCoverPage()
        {
            IWebElement coverPageCheckbox = DriverExtensions.WaitForElement(LayoutAndLimitsSelectCoverPageLocator);
            if (!coverPageCheckbox.Selected)
            {
                DriverExtensions.Click(coverPageCheckbox);
            }
        }

        /// <summary>
        /// NavigateToLayoutAndLimits  - Navigate to the Layout and Limits tab.
        /// </summary>
        public void NavigateToLayoutAndLimits() => DriverExtensions.WaitForElement(LayoutAndLimitsLocator).Click();
    }
}