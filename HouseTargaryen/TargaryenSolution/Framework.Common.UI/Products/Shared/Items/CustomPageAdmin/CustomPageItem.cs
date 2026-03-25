namespace Framework.Common.UI.Products.Shared.Items.CustomPageAdmin
{
    using System;
    using System.Text.RegularExpressions;

    using Framework.Common.UI.Products.Shared.Components.ResultList;
    using Framework.Common.UI.Products.Shared.Pages.CustomPages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    using Framework.Common.UI.Interfaces;
    /// <summary>
    /// CustonPageItem
    /// </summary>
    public class CustomPageItem : BaseItem
    {
        private static readonly By PageNameLocator = By.XPath(".//*[contains(@id,'co_SuperAdmin_pageLabel_')]");
        private static readonly By ContactsCountLinkLocator = By.XPath(".//a[@class = 'cp_childPageList_nbrContacts']");

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomPageItem"/> class.
        /// </summary>
        /// <param name="containerElement">
        /// The container element.
        /// </param>
        public CustomPageItem(IWebElement containerElement)
            : base(containerElement)
        {
        }

        /// <summary>
        /// Contact Name
        /// </summary>
        public string CustomPageName =>
            DriverExtensions.GetElement(this.Container, PageNameLocator).Text;

        /// <summary>
        /// Contacts Count
        /// </summary>
        public int ContactsCount =>
              Int32.Parse(Regex.Match(DriverExtensions.GetElement(this.Container, ContactsCountLinkLocator).Text, @"\d+").Value);

        /// <summary>
        /// Is Page Editable
        /// </summary>
        public bool IsPageEditable =>
            DriverExtensions.GetElement(this.Container, PageNameLocator).TagName.Equals("a");

        /// <summary>
        /// Click on Contacts Count Link
        /// </summary>
        /// <returns></returns>
        public AdminSettingsPage ClickViewAssignedContactsLink()
        {
            DriverExtensions.GetElement(this.Container, ContactsCountLinkLocator).CustomClick();
            return new AdminSettingsPage();
        }

        /// <summary>
        /// Click on Custom Page name
        /// </summary>
        /// <returns></returns>
        public T ClickCustomPageName<T>() where T : ICreatablePageObject
        {
            DriverExtensions.GetElement(this.Container, PageNameLocator).CustomClick();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}
