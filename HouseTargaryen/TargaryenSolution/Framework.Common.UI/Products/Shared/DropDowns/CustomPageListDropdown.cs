namespace Framework.Common.UI.Products.Shared.DropDowns
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;
    using OpenQA.Selenium;

    /// <summary>
    /// CustomPageListDropdown
    /// </summary>
    public class CustomPageListDropdown : BaseModuleRegressionCustomDropdown<string>
    {
        private const string CustomPageListLinkLocator = "//*[contains(@class,'a11yDropdown-menu')]//a[span[text()={0}]]";

        private static readonly By DropDownOptionLocator = By.XPath(".//span[@class='a11yDropdown-itemText']");

        private static readonly By CustomPageListContainerLocator = By.XPath("//ul[contains(@class, 'a11yDropdown-menu')]");

        private static readonly By CustomPageListButtonLocator = By.XPath("//div[@id='cp_pageList_dropdown']/button[@type='button']");

        private static readonly By DropdownArrowLocator = By.XPath("//span[contains(@class, 'icon25 icon_downMenu-gray')]");

        /// <summary>
        /// Returns Selected Option
        /// </summary>
        /// <returns> Throw exception, because there is no selected option for Manage Page dropdown </returns>
        public override string SelectedOption
        {
            get { throw new NotImplementedException("Can't get selected item for the GoTo dropdown"); }
        }

        /// <summary>
        /// The dropdown
        /// </summary>
        protected override IWebElement Dropdown => DriverExtensions.GetElement(CustomPageListButtonLocator);

        /// <summary>
        /// Get Options
        /// </summary>
        protected override IEnumerable<string> OptionsFromExpandedDropdown
            => DriverExtensions.GetElements(CustomPageListContainerLocator, DropDownOptionLocator).Select(e => DriverExtensions.GetImmediateText(e)).ToList();

        /// <summary>
        /// Click Custom Page List Link By Name
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="linkName">LinkName</param>
        /// <returns>The new instance of T page</returns>
        public T ClickCustomPageListLinkByName<T>(string linkName) where T : ICreatablePageObject
        {
            if (!DriverExtensions.IsDisplayed(CustomPageListContainerLocator))
            {
                DriverExtensions.WaitForElement(CustomPageListButtonLocator).Click();
            }

            DriverExtensions.WaitForElement(SafeXpath.BySafeXpath(CustomPageListLinkLocator, linkName)).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Verify that Manage Page Dropdown option is selected
        /// </summary>
        /// <param name="option"> Manage Page Dropdown option </param>
        /// <returns> Throw exception, because there is no selected option for Manage Page dropdown</returns>
        public override bool IsSelected(string option)
        {
            throw new NotImplementedException("Can't get selected item for the Select Custom Page dropdown");
        }

        /// <summary>
        /// Select Dropdown Option
        /// </summary>
        /// <param name="option">The option.</param>
        protected override void SelectOptionFromExpandedDropdown(string option)
            => DriverExtensions.Click(SafeXpath.BySafeXpath(CustomPageListLinkLocator, option));

        /// <summary>
        /// ClickDropdownArrow
        /// </summary>
        protected override void ClickDropdownArrow() => DriverExtensions.GetElement(DropdownArrowLocator).Click();

        /// <summary>
        /// Verifies if dropdown is expanded
        /// </summary>
        /// <returns>bool</returns>
        protected override bool IsDropdownExpanded() =>
            DriverExtensions.GetElement(CustomPageListContainerLocator).GetAttribute("style").Contains("block");
    }
}