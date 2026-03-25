namespace Framework.Common.UI.Products.WestLawAnalytics.Dialogs
{
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.WestLawAnalytics.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Print Analytics Report dialog
    /// Print Billing Investigation Results Dialog
    /// </summary>
    public class PrintDialog : BaseModuleRegressionDialog
    {
        private static readonly By TitleLocator = By.XPath("//div[@class='co_overlayBox_headline']//h3");

        private static readonly By CancelLinkLocator = By.XPath("//a[@class='co_overlayBox_buttonCancel']");

        private static readonly By PrintButtonLocator = By.XPath("//input[@id='co_deliveryPrintButton']");

        private static readonly By WhatToDeliverRadiobuttonLocator = By.XPath("//input[contains(@id,'co_deliveryWhatToDeliverDisplayed')]");
        
        /// <summary>
        /// Get dialog title
        /// </summary>
        /// <returns> Title </returns>
        public string GetDialogTitle() => DriverExtensions.GetText(TitleLocator);

        /// <summary>
        /// Verify that Cancel link is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsCancelLinkDisplayed() => DriverExtensions.IsDisplayed(CancelLinkLocator, 5);

        /// <summary>
        /// Verify that Print button is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsPrintButtonDIsplayed() => DriverExtensions.IsDisplayed(PrintButtonLocator, 5);

        /// <summary>
        /// Click on the 'Cancel' link
        /// </summary>
        /// <returns> The <see cref="AnalyticsPage"/>. </returns>
        public AnalyticsPage ClickCancelLink()
        {
            this.ClickElement(CancelLinkLocator);
            DriverExtensions.WaitForElementNotDisplayed(TitleLocator);
            return new AnalyticsPage();
        }

        /// <summary>
        /// Verify that radio button is checked
        /// </summary>
        /// <returns> True if checked, false otherwise </returns>
        public bool IsRadiobuttonChecked() => DriverExtensions.IsRadioButtonSelected(WhatToDeliverRadiobuttonLocator);
    }
}
