namespace Framework.Common.UI.Products.WestLawNext.Dialogs.PortalManagerSearch
{
    using Framework.Common.UI.Products.Shared.Dialogs;

    using OpenQA.Selenium;

    /// <summary>
    /// POM for the delete confirmation dialog
    /// </summary>
    public class DeleteFormDialog : BaseModuleRegressionDialog
    {
        private static readonly By YesButtonLocator = By.XPath("//div[@id='co_delete_item_lightbox']//input[@value='Yes']");
        
        /// <summary>
        /// Click on the yes button
        /// </summary>
        public void ClickYes() => this.ClickElement(YesButtonLocator);
    }
}