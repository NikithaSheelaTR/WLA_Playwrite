namespace Framework.Common.UI.Products.Shared.Dialogs.Contacts
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Custom Page Contacts Groups dialog
    /// </summary>
    public class CustomContactsGroupsDialog : BaseContactsGroupsDialog
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomContactsGroupsDialog"/> class. 
        /// </summary>
        public CustomContactsGroupsDialog()
        {
            this.SaveGroupButton = By.Id("co_customPages_CreateGroup");
            this.CancelButton = By.Id("co_CustomPagesCreateGroupCancel");
            this.CloseDialogButton = By.XPath("//div[@class='co_overlayBox_topLeft']//a[contains(.,'Close')]");
            DriverExtensions.WaitForJavaScript();
        }
    }
}