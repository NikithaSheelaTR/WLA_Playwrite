namespace Framework.Common.UI.Products.Shared.Components.HomePage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Dialogs.CustomPages;
    using Framework.Common.UI.Products.Shared.Pages.CustomPages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Custom Pages Component (on the Home page)
    /// </summary>
    public class CustomPagesComponent : BaseModuleRegressionComponent
    {
        private static readonly By CloseButtonLocator = By.Id("co_customPages_close");

        private static readonly By ContentListItemlocator = By.CssSelector("li > a[href*='CustomPages']");

        private static readonly By ContentListLocator = By.Id("co_customPages_list");

        private static readonly By CreateNewLinkLocator = By.Id("co_customPages_createLink");

        private static readonly By CustomPagesComponentLocator = By.Id("coid_website_customPagesWidget");

        private static readonly By TitleLocator = By.XPath("//div[@id='coid_website_customPagesWidget']/div/h2");

        private static readonly By ToggleButtonLocator = By.Id("co_customPages_expansion_toggle");

        private static readonly By ViewAllCustomPagesLinkLocator = By.Id("co_customPages_viewAllLink");

        private static readonly By CreateViewAllCustomPageLink = By.XPath("//*[@id='coid_customPageViewAll_createNew']");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => CustomPagesComponentLocator;

        /// <summary>
        /// Click Close Button on the Custom Pages component
        /// </summary>
        public void ClickCloseButton()
        {
            DriverExtensions.Hover(TitleLocator);
            DriverExtensions.WaitForElement(CloseButtonLocator).Click();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Click on the Create New link on the Custom Pages Component
        /// </summary>
        /// <returns> Create Custom Page dialog </returns>
        public CreateCustomPageDialog ClickCreateLink()
        {
            DriverExtensions.WaitForElement(CreateNewLinkLocator);
            DriverExtensions.ClickUsingJavaScript(CreateNewLinkLocator);
            return new CreateCustomPageDialog();
        }

        /// <summary>
        /// Click Custom Page Link By Name
        /// </summary>
        /// <param name="linkName"> Link name </param>
        /// <returns> The <see cref="CustomPage"/>. </returns>
        public CustomPage ClickCustomPageLinkByName(string linkName)
        {
            IReadOnlyCollection<IWebElement> customPagesLinksList = this.GetListCustomPagesLinksFromWidget();
            IWebElement link = customPagesLinksList.FirstOrDefault(element => element.Text.Contains(linkName));
            if (link == null)
            {
                throw new Exception("Cannot find Custom page link in widget with Link name " + linkName);
            }

            link.Click();
            return new CustomPage();
        }

        /// <summary>
        /// Click View all Custom Pages Link
        /// </summary>
        /// <returns></returns>
        public ManageCustomPagesPage ClickViewAllCustomPages()
        {
            DriverExtensions.GetElement(ViewAllCustomPagesLinkLocator).Click();
            return new ManageCustomPagesPage();
        }

        /// <summary>
        /// Collapse Custom Pages component (if expanded)
        /// </summary>
        /// <returns> The <see cref="CustomPagesComponent"/>. </returns>
        public CustomPagesComponent CollapseWidget()
        {
            if (this.IsExpanded())
            {
                this.ClickToggleButton();
            }

            return this;
        }

        /// <summary>
        /// Expand Custom Pages component (if collapsed)
        /// </summary>
        /// <returns> The <see cref="CustomPagesComponent"/>. </returns>
        public CustomPagesComponent ExpandCustomPageComponent()
        {
            if (!this.IsExpanded())
            {
                this.ClickToggleButton();
            }

            return this;
        }

        /// <summary>
        /// Get Custom Pages component title text
        /// </summary>
        /// <returns> Title </returns>
        public string GetCustomPagesComponentTitle() => DriverExtensions.GetText(TitleLocator);

        /// <summary>
        /// Get List of Custom Pages Names from component
        /// </summary>
        /// <returns> List of Names </returns>
        public List<string> GetCustomPagesNames()
            => DriverExtensions.GetElements(ContentListItemlocator).Select(item => item.Text).ToList();

        /// <summary>
        /// Check is Custom Page exist in component
        /// </summary>
        /// <param name="customPageName"> Custom Page Name </param>
        /// <returns> True if exist, false otherwise </returns>
        public bool IsCustomPageLinkExistInComponent(string customPageName)
        {
            return DriverExtensions.IsDisplayed(CustomPagesComponentLocator, 5)
                       ? this.GetCustomPagesNames().Any(element => element.Equals(customPageName))
                       : false;
        }

        /// <summary>
        /// Check if Custom Pages component is displayed
        /// </summary>
        /// <returns> True if component is displayed, false otherwise </returns>
        public override bool IsDisplayed() => DriverExtensions.IsDisplayed(CustomPagesComponentLocator, 5);

        /// <summary>
        /// Is Custom Page Component Expanded
        /// </summary>
        /// <returns> True if expanded, false otherwise </returns>
        public bool IsExpanded() => DriverExtensions.IsDisplayed(ContentListLocator, 5);

        private void ClickToggleButton()
        {
            DriverExtensions.Hover(TitleLocator);
            DriverExtensions.WaitForElement(ToggleButtonLocator).Click();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Get List of Custom Pages Links from component
        /// </summary>
        /// <returns> Collection of IWebElements </returns>
        private IReadOnlyCollection<IWebElement> GetListCustomPagesLinksFromWidget()
        {
            DriverExtensions.WaitForElement(ContentListLocator, 45000);
            return DriverExtensions.GetElements(ContentListItemlocator);
        }

        /// <summary>
        /// Click on the Create New link on the Custom Pages
        /// </summary>
        public CreateCustomPageDialog ClickCreateViewAllLink()
        {
            DriverExtensions.WaitForElement(CreateViewAllCustomPageLink);
            DriverExtensions.ClickUsingJavaScript(CreateViewAllCustomPageLink);
            return new CreateCustomPageDialog();
        }
    }
}