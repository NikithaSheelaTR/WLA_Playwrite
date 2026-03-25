namespace Framework.Common.UI.Raw.WestlawEdge.Pages.Document
{
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Redlining Document page
    /// </summary>
    public class EdgeRedliningDocumentPage : EdgeCommonDocumentPage
    {
        /// <summary>
        /// Display\Hide Amendments link
        /// </summary>
        private const string HideAmendmentsLinkAttribute = "Hide blacklines reflecting most recent amendments";

        private const string WhiteColor = "background-color: white;";

        private const string RgbWhiteColor = "background-color: rgb(255, 255, 255);";

        private static readonly By HeaderLinkLocator = By.Id("co_redlineHeaderLink");

        private static readonly By ToggleLocator = By.Id("co_redlineToggleContainer");

        private static readonly By CompareVersionsLocator = By.Id("co_statuteCompareButton");

        private static readonly By AddedPartLocator = By.XPath("//ins[contains(@class,'co_ruleBookRedline')]");

        private static readonly By DeletedPartLocator = By.XPath("//del[contains(@class,'co_ruleBookRedline')]");

        /// <summary>
        /// Clicks the amendment.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>object</returns>
        public T ClickAmendment<T>() where T : ICreatablePageObject
        {
            DriverExtensions.Click(HeaderLinkLocator);
            DriverExtensions.WaitForJavaScript();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Clicks the toggle.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>object</returns>
        public T ClickToggle<T>() where T : ICreatablePageObject
        {
            DriverExtensions.Click(ToggleLocator);
            DriverExtensions.WaitForJavaScript();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Determine whether hide amendments link exist.
        /// </summary>
        /// <returns>bool</returns>
        public string GetAmendmentsText() => DriverExtensions.GetAttribute("title", HeaderLinkLocator);
            
        /// <summary>
        /// Determine whether Redlinings toggle and markup exist.
        /// </summary>
        /// <returns>bool</returns>
        public bool RedliningToggleAndMarkupExist() => this.ToggleExist() && this.IsRedliningMarkupDisplayed();

        /// <summary>
        /// Determine Redlinings toggle exist but no markup.
        /// </summary>
        /// <returns>bool</returns>
        public bool RedliningToggleExistButNoMarkup() => this.ToggleExist() && !this.IsRedliningMarkupDisplayed();

        /// <summary>
        /// Determine Compare Versions exist but no markup.
        /// </summary>
        /// <returns>bool</returns>
        public bool CompareVersionsExistButNoMarkup() => this.CompareVersionsExist() && !this.IsRedliningMarkupDisplayed();

        /// <summary>
        /// Determine whether redlining markup is displayed
        /// </summary>
        /// <returns>bool</returns>
        public bool IsRedliningMarkupDisplayed() => (DriverExtensions.GetElements(AddedPartLocator).Count > 0
            && DriverExtensions.GetElements(AddedPartLocator).Select(el => el.GetAttribute("style")).ToList()
            .TrueForAll(attr => (attr != RgbWhiteColor && attr != WhiteColor))) || DriverExtensions.GetElements(DeletedPartLocator).Count > 0;

        /// <summary>
        /// Determine whether Toggles exist.
        /// </summary>
        /// <returns>bool</returns>
        public bool ToggleExist() => DriverExtensions.IsDisplayed(ToggleLocator);

        /// <summary>
        /// Determine whether CompareVersions exist.
        /// </summary>
        /// <returns>bool</returns>
        public bool CompareVersionsExist() => DriverExtensions.IsDisplayed(CompareVersionsLocator);

        /// <summary>
        /// Determine whether hide amendments link exist.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsHideAmendmentsLinkDisplayed()
            => DriverExtensions.GetAttribute("title", HeaderLinkLocator).Equals(HideAmendmentsLinkAttribute);
    }
}