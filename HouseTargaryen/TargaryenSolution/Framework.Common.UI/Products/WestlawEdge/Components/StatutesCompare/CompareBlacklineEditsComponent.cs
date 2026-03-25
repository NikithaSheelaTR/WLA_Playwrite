namespace Framework.Common.UI.Products.WestlawEdge.Components.StatutesCompare
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.StatutesCompare;
    using Framework.Common.UI.Raw.WestlawEdge.Pages.RelatedInfo;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Compare Blackline Edits Widget
    /// </summary>
    public class CompareBlacklineEditsComponent : BaseModuleRegressionComponent
    {
        private const string DocContainerLctMask
            = "//div[@class='co_statuteCompare_selectedDoc_body']/div[contains(@class,'co_statuteCompare_emptySelectedDoc')][{0}]";
        private const string DocContainerCloseButtonLctMask
            = "//div[@class='co_statuteCompare_selectedDoc_body']/div[{0}]/button";

        private static readonly By ComponentHeaderLocator = By.XPath("//header[@class='co_statuteCompare_selectedDoc_header']/h4");
        private static readonly By ComponentTextLocator = By.XPath("//header[@class='co_statuteCompare_selectedDoc_header']/p");
        private static readonly By CompareButtonLocator = By.Id("co_compareStatute");

        private static readonly By ContainerLocator = By.Id("co_statuteCompareSelectorContainer");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Clicks compare button.
        /// </summary>
        /// <returns> The <see cref="CompareVersionsDialog"/>. </returns>
        public CompareVersionsDialog ClickCompareButton()
        {
            DriverExtensions.WaitForElement(CompareButtonLocator).CustomClick();
            return new CompareVersionsDialog();
        }

        /// <summary>
        /// Verifies that the compare button is displayed.
        /// </summary>
        /// <returns> The <see cref="bool"/>.True if the compare button is displayed </returns>
        public bool IsCompareButtonDisplayed() => DriverExtensions.IsDisplayed(CompareButtonLocator);

        /// <summary>
        /// Verifies that the compare button is enabled.
        /// </summary>
        /// <returns> The <see cref="bool"/>.True if the compare button is enabled </returns>
        public bool IsCompareButtonEnabled() => DriverExtensions.IsEnabled(CompareButtonLocator);

        /// <summary>
        /// Verifies that widget header is displayed.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if widget header is displayed </returns>
        public bool IsComponentHeaderDisplayed() => DriverExtensions.IsDisplayed(ComponentHeaderLocator);

        /// <summary>
        /// Gets widget header text.
        /// </summary>
        /// <returns> The <see cref="string"/>. The widget header text. </returns>
        public string GetComponentHeaderText() => DriverExtensions.GetText(ComponentHeaderLocator);

        /// <summary>
        /// Verifies that widget text is displayed.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if widget text is displayed </returns>
        public bool IsComponentTextDisplayed() => DriverExtensions.IsDisplayed(ComponentTextLocator);

        /// <summary>
        /// Gets widget text.
        /// </summary>
        /// <returns> The <see cref="string"/>. The widget text. </returns>
        public string GetComponentText() => DriverExtensions.GetText(ComponentTextLocator);

        /// <summary>
        /// Verifies that Select Version To Compare Container is displayed.
        /// </summary>
        /// <param name="containerNumber"> The container Number. </param>
        /// <returns> The <see cref="bool"/>. True if Select Version To Compare Container is displayed  </returns>
        public bool IsDocContainerDisplayed(int containerNumber)
            => DriverExtensions.IsDisplayed(By.XPath(string.Format(DocContainerLctMask, containerNumber)));

        /// <summary>
        /// Verifies that the Select Version To Compare Container is empty.
        /// </summary>
        /// <param name="containerNumber"> The container Number. </param>
        /// <returns> The <see cref="string"/>. True if the Select Version To Compare Container Effective is empty. </returns>
        public bool IsDocContainerEmpty(int containerNumber)
            => DriverExtensions.IsDisplayed(By.XPath(string.Format(DocContainerLctMask, containerNumber)));

        /// <summary>
        /// Clicks select version to compare container close button.
        /// </summary>
        /// <param name="containerNumber"> The container number. </param>
        /// <returns> The <see cref="EdgeVersionsPage"/>. </returns>
        public EdgeVersionsPage ClickDocContainerCloseButton(int containerNumber)
        {
            DriverExtensions.WaitForElement(By.XPath(string.Format(DocContainerCloseButtonLctMask, containerNumber))).Click();
            return new EdgeVersionsPage();
        }
    }
}