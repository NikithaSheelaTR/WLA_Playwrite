namespace Framework.Common.UI.Products.Shared.Items.EnhancedCustomPageSharing
{
    using System.Linq;
   
    using Framework.Common.UI.Products.Shared.Components.ResultList;
    using Framework.Common.UI.Products.Shared.Enums.CustomPage;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    
    /// <summary>
    /// Custom Page Contact Item
    /// </summary>
    public class CustomPageAdminContactItem : BaseItem
    {
        private const string RoleRadioButtonLctMask = ".//input[@value = '{0}']";
        private static readonly By ContactNameLocator = By.XPath(".//*[contains(@class,'co_detailsTable_content')]");
        private static readonly By RolesLocator = By.XPath(".//td[@class = 'co_detailsTable_roles']//input");
        private static readonly By InactiveBadgeLocator = By.XPath(".//span[@class = 'Badge badge--gray']");
        private static readonly By RemoveButtonLocator = By.XPath(".//td[@class = 'co_detailsTable_remove']/button");
        private static readonly By RemoveCheckboxLocator = By.XPath(".//td[@class = 'co_detailsTable_remove']//input");

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomPageAdminContactItem"/> class.
        /// </summary>
        /// <param name="containerElement">
        /// The container element.
        /// </param>
        public CustomPageAdminContactItem(IWebElement containerElement)
            : base(containerElement)
        {
        }

        /// <summary>
        /// Contact Name
        /// </summary>
        public string ContactName
            => DriverExtensions.GetImmediateText(this.Container, ContactNameLocator).Trim();

        /// <summary>
        /// Role
        /// </summary>
        public string ActiveRole
            => DriverExtensions.GetElements(this.Container, RolesLocator).First(el => el.Selected).GetAttribute("value");

        /// <summary>
        /// Is User Active
        /// </summary>
        public bool IsUserActive =>
            !DriverExtensions.GetElement(this.Container, ContactNameLocator).GetAttribute("class").Contains("inactiveContactName");

        /// <summary>
        /// Is Inactive badge displyed
        /// </summary>
        public bool IsInactiveBadgeDisplayed =>
            DriverExtensions.IsDisplayed(this.Container, InactiveBadgeLocator);

        /// <summary>
        /// Is user disabled
        /// </summary>
        public bool IsUserDisabled =>
            DriverExtensions.GetElement(this.Container).GetAttribute("class").Equals("co_disabled");

        /// <summary>
        /// Remove Contact 
        /// </summary>
        public void ClickRemoveContact() => DriverExtensions.WaitForElement(this.Container, RemoveButtonLocator).Click();

        /// <summary>
        /// Set remove contact checkbox
        /// </summary>
        /// <param name="selected">
        /// The selected.
        /// </param>
        public void SetRemoveContactCheckbox(bool selected) =>
            DriverExtensions.SetCheckbox(
                selected,
                DriverExtensions.WaitForElement(this.Container, RemoveCheckboxLocator));

        /// <summary>
        /// Set Role
        /// </summary>
        /// <param name="role"></param>
        public void SelectRole(CustomPageSharingRole role)
            => DriverExtensions.Click(this.Container, By.XPath(string.Format(RoleRadioButtonLctMask, role)));
    }
}
