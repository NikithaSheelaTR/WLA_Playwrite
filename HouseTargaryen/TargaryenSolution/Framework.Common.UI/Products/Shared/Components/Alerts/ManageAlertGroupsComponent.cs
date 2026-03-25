using Framework.Common.UI.Interfaces.Elements;
using Framework.Common.UI.Products.Shared.Elements.Labels;
using Framework.Common.UI.Products.Shared.Elements.Links;
using Framework.Common.UI.Products.Shared.Elements.Textboxes;
using Framework.Common.UI.Products.WestLawAnalytics.Pages.Alerts;
using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
using java.text;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Common.UI.Products.Shared.Components.Alerts
{
    /// <summary>
    /// Manage alert groups component under alert setting
    /// </summary>
    public class ManageAlertGroupsComponent : BaseAlertComponent
    {
        private static readonly By ContainerLocator = By.Id("co_ManageAlertGroups");

        private static readonly By MoreOwnersLocator = By.Id("coid_alertsalertGroupsOwner_select_anchor");
        
        private static readonly By AdministratorLinkLocator = By.XPath("//ul[@id='co_facet_alertGroupsOwner_availableOptions']//span[text()='Administrator']");

        private static readonly By FilterButtonLocator = By.XPath("//div[@class='co_overlayBox_optionsBottom']//input");

        private static readonly By SelectedFilterTextLocator = By.XPath("//input[@id='coid_alert_alertGroupsOwner_Firm Owned']//following-sibling::span");

        private static readonly By GroupNameLocator = By.XPath("//div[@class='co_listItem']/span/span");

        private static readonly By SearchGroupBoxLocator = By.XPath("//input[@id='coid_alerts_widgets_alertGroups_searchInput']");

        private const string AlphabetsLinkLctMask = "//div[@id='coid_alerts_widgets_alertGroups_alphabetCategories']//li//button[text()='{0}']";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        ///  More owners link locator
        /// </summary>
        public ILink MoreOwnerLink => new Link(this.ComponentLocator, MoreOwnersLocator);

        /// <summary>
        ///  Administrator link locator
        /// </summary>
        public ILink AdministratorLink => new Link(this.ComponentLocator, AdministratorLinkLocator);

        /// <summary>
        ///  Filter button locator
        /// </summary>
        public ILink FilterButton => new Link(this.ComponentLocator, FilterButtonLocator);

        /// <summary>
        ///  Owner List locator
        /// </summary>
        public ILink GroupName => new Link(this.ComponentLocator, GroupNameLocator);

        /// <summary>
        ///  Search box locator to search group by name
        /// </summary>
        public ITextbox SearchGroupBox => new Textbox(this.ComponentLocator, SearchGroupBoxLocator);

        /// <summary>
        /// Select owner from the more owner list
        /// </summary>
        /// <return> "return select owner name" </return>
        public string SelectOwner()
        {
            MoreOwnerLink.Click();
            AdministratorLink.Click();
            FilterButton.Click();
            DriverExtensions.WaitForAnimation();
            DriverExtensions.WaitForElementDisplayed(SelectedFilterTextLocator);
            return DriverExtensions.GetElement(SelectedFilterTextLocator).Text;
        }

        /// <summary>
        /// Clicks the alphabet link to search group name
        /// </summary>
        /// <returns>
        /// The <see cref="ManageAlertGroupsComponent"/>.
        /// </returns>
        public ManageAlertGroupsComponent ClickOnAlphabetLink(string text)
        {
            DriverExtensions.GetElement(By.XPath(string.Format(AlphabetsLinkLctMask, text))).Click();
            return this;
        }

        /// <summary>
        /// Texts box to search group by name
        /// </summary>
        public void SearchGroupByName(string name)
        {
            SearchGroupBox.Clear();
            SearchGroupBox.SendKeys(name);
            SearchGroupBox.SendKeys(Keys.Enter);
        }
    }
}
