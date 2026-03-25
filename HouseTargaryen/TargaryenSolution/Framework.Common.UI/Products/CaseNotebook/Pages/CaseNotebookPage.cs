namespace Framework.Common.UI.Products.CaseNotebook.Pages
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.WestLawNext.Pages.SearchResult;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Pricing Page
    /// </summary>
    public class CaseNotebookPage : BaseModuleRegressionPage
    {
        private const string CheckboxLctMask = "//li[./label[text()='{0}']]/input";

        private static readonly By ChangeJurisdictionLinkLocator = By.Id("co_cnbJurisWidgetShowHide");

        private static readonly By SelectedCheckboxLocator = By.XPath("//li[@class='co_formInline co_active']");

        private static readonly By JurisdictionComponentLocator = By.Id("co_cnbJurisWidget");

        private EnumPropertyMapper<Jurisdiction, JurisdictionInfo> jurisdictionsMap;

        /// <summary>
        /// Header
        /// </summary>
        public WestlawNextHeaderComponent Header { get; } = new WestlawNextHeaderComponent();

        /// <summary>
        /// Gets the jurisdiction enumeration to JurisdictionInfo map.
        /// </summary>
        protected EnumPropertyMapper<Jurisdiction, JurisdictionInfo> JurisdictionsMap
            => this.jurisdictionsMap = this.jurisdictionsMap ?? EnumPropertyModelCache.GetMap<Jurisdiction, JurisdictionInfo>();

        /// <summary>
        /// Gets the number of selected checkboxes.
        /// </summary>
        /// <returns> Number of selected checkboxes </returns>
        public int GetNumberOfSelectedCheckedboxes() => DriverExtensions.GetElements(SelectedCheckboxLocator).Count;

        /// <summary>
        /// Checks if the jurisdiction area exists
        /// </summary>
        /// <returns>If the area exists</returns>
        public bool IsJurisdictionAreaDisplayed()
        {
            DriverExtensions.WaitForElement(ChangeJurisdictionLinkLocator).Click();
            DriverExtensions.WaitForAnimation();
            return DriverExtensions.IsDisplayed(JurisdictionComponentLocator);
        }

        /// <summary>
        /// Selects the jurisdictions
        /// </summary>
        /// <param name="jurisdictions"> list of jurisdictions to select </param>
        /// <param name="isSet"> The is Set. </param>
        public void SetJurisdictions(List<Jurisdiction> jurisdictions, bool isSet = true)
            => jurisdictions.ToList().ForEach(jurisdiction => this.GetJurisdictionCheckbox(jurisdiction).SetCheckbox(isSet));

        /// <summary>
        /// Search for tem and open new tab
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="query"> Query </param>
        /// <returns> New instance of the page </returns>
        public T SearchAndOpenNewBrowserTab<T>(string query) where T : ICreatablePageObject
        {
            const string NewTabName = "Westlaw Sign In";
            int browserTabsCount = BrowserPool.CurrentBrowser.TabHandles.Count;
            this.Header.EnterSearchQueryAndClickSearch<CategorySearchResultPage>(query);
            BrowserPool.CurrentBrowser.CreateTab(NewTabName);
            BrowserPool.CurrentBrowser.ActivateTab(NewTabName);
            DriverExtensions.WaitForNewTabLoaded(browserTabsCount);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Gets the checkbox on the Jurisdiction widget for a specified Jurisdiction
        /// </summary>
        /// <param name="jurisdiction">the jurisdiction to get the checkbox element for</param>
        /// <returns>the Jurisdiction checkbox element</returns>
        private IWebElement GetJurisdictionCheckbox(Jurisdiction jurisdiction)
            => DriverExtensions.WaitForElement(
                    By.XPath(string.Format(CheckboxLctMask, this.JurisdictionsMap[jurisdiction].JurisdictionName)));
    }
}