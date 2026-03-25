namespace Framework.Common.UI.Products.Shared.DropDowns
{
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Products.Shared.Components.Contacts;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Results View dropdown
    /// </summary>
    public class GroupSearchScopeDropdown : BaseModuleRegressionCustomDropdown<MyContactsGroupSearchScope>
    {
        private static readonly By GroupSearchScopeDropdownLinkLocator =
           By.XPath("//div[@id='coid_contacts_groups_filterDropDownTab']");

        /// <summary>
        /// get the already selected option
        /// </summary>
        public override MyContactsGroupSearchScope SelectedOption => (DriverExtensions.GetElements(GroupSearchScopeDropdownLinkLocator)?.FirstOrDefault()?.Text ?? string.Empty)
            .GetEnumValueByText<MyContactsGroupSearchScope>();

        /// <summary>
        /// get the list of options
        /// </summary>
        protected override IEnumerable<MyContactsGroupSearchScope> OptionsFromExpandedDropdown => DriverExtensions
            .GetElements(GroupSearchScopeDropdownLinkLocator)?.Select(
                webElement =>
                    webElement.Text.GetEnumValueByPropertyModel<MyContactsGroupSearchScope, WebElementInfo>(webElInfo => webElInfo.Text))
            .ToList();

        /// <inheritdoc />
        protected override IWebElement Dropdown { get; } = DriverExtensions.GetElement(GroupSearchScopeDropdownLinkLocator);

        /// <summary>
        /// Gets the dropdown map.
        /// </summary>
        private EnumPropertyMapper<MyContactsGroupSearchScope, WebElementInfo> DropdownMap { get; } =
            EnumPropertyModelCache.GetMap<MyContactsGroupSearchScope, WebElementInfo>();

        /// <summary>
        /// To check the provided option is selected or not
        /// </summary>
        public override bool IsSelected(MyContactsGroupSearchScope option) => DriverExtensions
                                                                              .GetElement(GroupSearchScopeDropdownLinkLocator, By.XPath(this.DropdownMap[option].LocatorString)).Selected;

        /// <summary>
        /// selecting option from dropdown
        /// </summary>
        protected override void SelectOptionFromExpandedDropdown(MyContactsGroupSearchScope option) =>
            DriverExtensions.GetElement(By.XPath(this.DropdownMap[option].LocatorString)).Click();

        /// <summary>
        /// ClickDropdownArrow
        /// </summary>
        protected override void ClickDropdownArrow() => this.Dropdown.Click();

    }
}
