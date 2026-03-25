namespace Framework.Common.UI.Products.WestlawEdge.Components.Document
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Credits Component inside document
    /// </summary>
    public class CreditsComponent : BaseModuleRegressionComponent
    {
        private static readonly By ExpandSectionButtonLocator = By.XPath("//div[@class='co_statuteCreditHeaderContainer']/button");
        private static readonly By CreditsComponentLocator = By.XPath("//div[@class='co_statuteCreditHeaderContainer']");
        private static readonly By CreditsComponentTitleLocator = By.XPath("//div[@class='co_statuteCreditTitle']");
        private static readonly By FilterByYearLocator = By.XPath("//div[@class='co_statuteCreditYearFilter']");
        private static readonly By FilterByYearTitleLocator = By.XPath("//div[@class='co_statuteCreditYearFilter']/label");
        private static readonly By CreditsToggleLocator = By.XPath("//div[@class='StatuteCreditToggle']");
        private static readonly By GridViewButtonLocator = By.XPath("//button[@title='Grid View']");
        private static readonly By ListViewButtonLocator = By.XPath("//button[@title='List View']");
        private static readonly By ShowMoreLinkLocator = By.XPath("//a[@class='ShowMoreCredits']");
        private static readonly By ShowLessLinkLocator = By.XPath("//a[@class='ShowLessCredits']");
        private static readonly By OtherContentSectionLocator = By.XPath("//div[@class='co_statuteCreditMain']//div[@class='co_statuteCreditOtherContent']");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => CreditsComponentLocator;

        /// <summary>
        /// Get credits component title
        /// </summary>
        /// <returns> The <see cref="string"/>. </returns>
        public string GetCreditsComponentTitle() => DriverExtensions.GetText(CreditsComponentTitleLocator);

        /// <summary>
        /// Is expand button displayed
        /// </summary>
        /// <returns> The <see cref="bool"/>. </returns>
        public bool IsExpandButtonDisplayed() => DriverExtensions.IsDisplayed(ExpandSectionButtonLocator);

        /// <summary>
        /// Is credits component collapsed
        /// </summary>
        /// <returns> The <see cref="bool"/>. </returns>
        public bool IsCreditsComponentCollapsed() => DriverExtensions
            .GetElement(ExpandSectionButtonLocator).GetAttribute("class").Contains("Icon-collapsed");

        /// <summary>
        /// Expand credits
        /// </summary>
        public void ExpandCredits()
        {
            DriverExtensions.GetElement(ExpandSectionButtonLocator).CustomClick();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Is filter by year displayed
        /// </summary>
        /// <returns> The <see cref="bool"/>. </returns>
        public bool IsFilterByYearDisplayed() => DriverExtensions.IsDisplayed(FilterByYearLocator);

        /// <summary>
        /// Is show more link displayed
        /// </summary>
        /// <returns> The <see cref="bool"/>. </returns>
        public bool IsShowMoreLinkDisplayed() => DriverExtensions.IsDisplayed(ShowMoreLinkLocator);

        /// <summary>
        /// Is show less link displayed
        /// </summary>
        /// <returns> The <see cref="bool"/>. </returns>
        public bool IsShowLessLinkDisplayed() => DriverExtensions.IsDisplayed(ShowLessLinkLocator);

        /// <summary>
        /// Click show more link
        /// </summary>
        public void ClickShowMoreLink() => DriverExtensions.GetElement(ShowMoreLinkLocator).CustomClick();

        /// <summary>
        /// Get filter by year title
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetFilterByYearTitle() => DriverExtensions.GetText(FilterByYearTitleLocator);

        /// <summary>
        /// Is toggle section displayed
        /// </summary>
        /// <returns> The <see cref="bool"/>. </returns>
        public bool IsToggleSectionDisplayed() => DriverExtensions.IsDisplayed(CreditsToggleLocator);

        /// <summary>
        /// Is grid view selected
        /// </summary>
        /// <returns> The <see cref="bool"/>. </returns>
        public bool IsGridViewSelected() => DriverExtensions
            .GetElement(GridViewButtonLocator).GetAttribute("class").Contains("co_selected");

        /// <summary>
        /// Is list view selected
        /// </summary>
        /// <returns> The <see cref="bool"/>. </returns>
        public bool IsListViewSelected() => DriverExtensions
            .GetElement(ListViewButtonLocator).GetAttribute("class").Contains("co_selected");

        /// <summary>
        /// Toggle grid view
        /// </summary>
        public void ToggleGridView() => DriverExtensions.GetElement(GridViewButtonLocator).Click();

        /// <summary>
        /// Toggle list view
        /// </summary>
        public void ToggleListView() => DriverExtensions.GetElement(ListViewButtonLocator).Click();

        /// <summary>
        /// Is Other Content displayed
        /// </summary>
        /// <returns> True if displayed </returns>
        public bool IsOtherContentDisplayed() =>
            DriverExtensions.GetElement(OtherContentSectionLocator).Displayed;

        /// <summary>
        /// Change view
        /// </summary>
        public void ChangeView()
        {
            if (this.IsGridViewSelected())
            {
                this.ToggleListView();
            }
            else
            {
                this.ToggleGridView();
            }
        }
    }
}