namespace Framework.Common.UI.Products.Shared.Components.CustomPage.EnhancedCustomPageSharing
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs.CustomPages.EnhancedCustomPageSharing;
    using Framework.Common.UI.Products.Shared.Elements.WrapperEements.InfoBox;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Sharing Tab
    /// </summary>
    public class AssignTabComponent : BaseTabComponent
    {
        private static readonly By AddContactButtonLocator = By.Id("co_CustomPagesShareAddContacts");
        private static readonly By SaveButtonLocator = By.Id("co_CustomPagesShareSave");
        private static readonly By CancelButtonLocator = By.Id("co_CustomPagesShareCancel");
        private static readonly By InfoBoxLocator = By.XPath("//div[@class = 'co_infoBox_inner']/a[not (@ id ='co_website_errorSumarryCloseLink')]/parent::*/div");
        private static readonly By InfoBoxCloseButton = By.XPath("//div[@class = 'co_infoBox_inner']/a[not (@ id ='co_website_errorSumarryCloseLink')]");

        private static readonly By ContainerLocator = By.ClassName("co_shareFolder_collaboratorsAndRolesForm");

        /// <summary>
        /// The contacts component.
        /// </summary>
        public ContactsComponent ContactsComponent => new ContactsComponent();

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Assign";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// InfoBox
        /// </summary>
        public IInfoBox InfoBox => new InfoBox(DriverExtensions.GetElement(InfoBoxLocator), InfoBoxCloseButton);

        /// <summary>
        /// The click cancel button.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        public T ClickCancelButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(CancelButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// The click add contacts button.
        /// </summary>
        /// <returns>
        /// The <see cref="ShareCustomPageDialog"/>.
        /// </returns>
        public ShareCustomPageDialog ClickAddContactsButton()
        {
            DriverExtensions.WaitForElement(AddContactButtonLocator).Click();
            return new ShareCustomPageDialog();
        }
        /// <summary>
        /// Is Save button disabled
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsSaveButtonEnabled() => DriverExtensions.GetElement(SaveButtonLocator).Enabled;

        /// <summary>
        /// The click save button.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        public T ClickSaveButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(SaveButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}
