namespace Framework.Common.UI.Products.WestLawNext.Components.PortalManager
{
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.WestLawNext.Dialogs.PortalManagerSearch;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// POM for the custom page widget, which is displayed in the Global Search form page
    /// </summary>
    public class CustomPageComponent : BaseModuleRegressionComponent
    {
        private const string CustomPageCheckboxLctMask = "//div[@id='customPagesWidget']//a[text()='{0}']/../input[@type='checkbox']";

        private const string CustomPageLinkLctMask = "//div[@id='customPagesWidget']//a[text()='{0}']";

        private static readonly By CustomPagesCheckboxesLocator = By.XPath("//div[@id='customPagesWidget']//input[@type='checkbox']");

        private static readonly By CustomPagesLinksLocator = By.XPath("//div[@id='customPagesWidget']//a[@class='co_customPagesLink']");

        private static readonly By ContainerLocator = By.Id("customPagesWidget");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Click on the specified custom page
        /// </summary>
        /// <param name="customPageName">Custom page name</param>
        /// <returns>The <see cref="CustomPageSectionDialog"/>.</returns>
        public CustomPageSectionDialog ClickCustomPageLink(string customPageName)
        {
            DriverExtensions.WaitForElement(By.XPath(string.Format(CustomPageLinkLctMask, customPageName))).Click();
            return new CustomPageSectionDialog();
        }

        /// <summary>
        /// Checks if custom page includes specific value
        /// </summary>
        /// <param name="customPages">Custom page name</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsCustomPagesInclude(string customPages)
            => DriverExtensions.GetElements(CustomPagesLinksLocator).Select(item => item.Text).ToList().Contains(customPages);

        /// <summary>
        /// Verifies that the specified custom page is checked
        /// </summary>
        /// <param name="customPageName">Custom page name</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsCustomPageChecked(string customPageName)
            => DriverExtensions.WaitForElement(By.XPath(string.Format(CustomPageCheckboxLctMask, customPageName))).Selected;

        /// <summary>
        /// Check all the custom pages listed
        /// </summary>
        public void SelectAllCustomPages()
        {
            DriverExtensions.WaitForElement(CustomPagesCheckboxesLocator);
            DriverExtensions.GetElements(CustomPagesCheckboxesLocator).ToList().ForEach(u => u.SetCheckbox(true));
        }

        /// <summary>
        /// check the specified custom page
        /// </summary>
        /// <param name="customPageName">Custom page name</param>
        public void SelectCustomPage(string customPageName)
            => DriverExtensions.WaitForElementDisplayed(By.XPath(string.Format(CustomPageCheckboxLctMask, customPageName))).SetCheckbox(true);

        /// <summary>
        /// Unchecks all the custom pages
        /// </summary>
        public void UnselectCustomPages()
        {
            DriverExtensions.WaitForElement(CustomPagesCheckboxesLocator);
            DriverExtensions.GetElements(CustomPagesCheckboxesLocator).ToList().ForEach(u => u.SetCheckbox(false));
        }
    }
}