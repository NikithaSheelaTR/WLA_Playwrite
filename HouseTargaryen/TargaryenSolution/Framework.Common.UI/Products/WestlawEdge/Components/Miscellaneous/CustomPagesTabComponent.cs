namespace Framework.Common.UI.Products.WestlawEdge.Components.Miscellaneous
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Dialogs.CustomPages;
    using Framework.Common.UI.Products.Shared.Pages.CustomPages;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;

    using OpenQA.Selenium;

    /// <summary>
    /// Custom Pages tab component
    /// </summary>
    public class CustomPagesTabComponent : BaseTabComponent
    {
        private const string CustomPageLinkLctMask = "//div[contains(@id, 'CustomPages') or @class = 'Home-box-content Home-customPages']//a[text() = {0}]";

        private static readonly By ContentListItemLocator = By.XPath("//*[@id='panel_CustomPagesPaneId']//li/a[not(.='Create Custom Page')]");
        private static readonly By CreateCustomPageButtonLocator = By.XPath("//*[@id='panel_CustomPagesPaneId']//*[contains(text(), 'Create')]");
        private static readonly By ViewAllCustomPagesButtonLocator = By.XPath("//div[@id='panel_CustomPagesPaneId']//a[text()='View all' or text()='View all Custom Pages']");
        private static readonly By ContainerLocator = By.Id("panel_CustomPagesPaneId");

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Custom Pages";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Click 'Create Custom Page' button
        /// </summary>
        /// <returns>new instance of CreateCustomPageDialog</returns>
        public CreateCustomPageDialog ClickCreateCustomPage()
        {
            DriverExtensions.WaitForElement(CreateCustomPageButtonLocator).Click();
            return new CreateCustomPageDialog();
        }

        /// <summary>
        /// Click Custom page link by name
        /// </summary>
        /// <param name="linkName"> Link name </param>
        /// <returns> The <see cref="CustomPage"/>. </returns>
        public CustomPage ClickCustomPageLinkByName(string linkName)
        {
            DriverExtensions.WaitForElement(SafeXpath.BySafeXpath(CustomPageLinkLctMask, linkName)).Click();
            return new CustomPage();
        }

        /// <summary>
        /// Click Custom page link by name
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="linkName">
        /// Link name 
        /// </param>
        /// <returns>
        /// The <see cref="CustomPage"/>. 
        /// </returns>
        public T ClickCustomPageLinkByName<T>(string linkName) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(SafeXpath.BySafeXpath(CustomPageLinkLctMask, linkName)).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Check is Custom Page exist in component
        /// </summary>
        /// <param name="customPageName"> Custom Page Name </param>
        /// <returns> True if exist, false otherwise </returns>
        public bool IsCustomPageLinkExistInComponent(string customPageName) => DriverExtensions.IsDisplayed(CreateCustomPageButtonLocator, 5) && this.GetCustomPagesNames().Any(element => element.Equals(customPageName));

        /// <summary>
        /// Check is View Al Custom Pages Button displayed i the component
        /// </summary>
        /// <returns> True if exist, false otherwise </returns>
        public bool IsViewAllCustomPagesButtonDisplayed() => DriverExtensions.IsDisplayed(ViewAllCustomPagesButtonLocator, 5);

        /// <summary>
        /// Get List of Custom Pages Names from component
        /// </summary>
        /// <returns> List of Names </returns>
        public List<string> GetCustomPagesNames()
            => DriverExtensions.GetElements(ContentListItemLocator).Select(item => item.Text).ToList();

        /// <summary>
        /// Click on View all Custom Page button
        /// </summary>
        /// <returns>ManageCustomPagesPage</returns>
        public ManageCustomPagesPage ClickViewAllCustomPageButton()
        {
            DriverExtensions.WaitForElement(ViewAllCustomPagesButtonLocator).Click();
            return new ManageCustomPagesPage();
        }
    }
}
