namespace Framework.Common.UI.Products.WestlawEdge.Components.Toolbar
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Components.Toolbar;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Elements.WrapperEements.InfoBox;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdge.Components.Document;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.NarrowPane.Facets;
    using Framework.Common.UI.Products.WestlawEdge.DropDowns;
    using Framework.Common.UI.Products.WestlawEdge.Enums.Toolbars;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Toolbars;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;

    /// <summary>
    /// The indigo toolbar.
    /// </summary>
    public class EdgeToolbarComponent : Toolbar
    {
        private static readonly By ToolbarLocator = By.XPath("//*[@class='co_navTools' or @id='co_docToolbar']");
        private static readonly By AarInfoBoxMessageLabelLocator = By.XPath("//div[@role='alert']//div[@class = 'co_infoBox_message']");
        private static readonly By SummarizeDocketButtonLocator = By.XPath("//li[@id='co_docDocumentAnalyzer']/saf-button");

        /// <summary>
        /// AAR InfoBox Message label
        /// </summary>
        public ILabel AarInfoBoxMessageLabel => new Label(AarInfoBoxMessageLabelLocator);

        /// <summary>
        /// Summarize docket button 
        /// </summary>
        public IButton SummarizeDocketButton => new Button(SummarizeDocketButtonLocator);

        /// <summary>
        /// Summarize docket Element 
        /// </summary>
        public IWebElement SummarizeDocketElement => DriverExtensions.GetElement(SummarizeDocketButtonLocator);

        /// <summary>
        /// Annotations Dropdown
        /// /// </summary>
        public new EdgeAnnotationsDropdown AnnotationsDropdown { get; protected set; } = new EdgeAnnotationsDropdown();

        /// <summary>
        /// Custom Sort By
        /// </summary>
        public override CustomSortByDropdown CustomSortBy { get; } = new CustomSortByDropdown(
            "Edge",
            @"Resources/EnumPropertyMaps/WestlawEdge/Toolbars");

        /// <summary>
        /// Trash menu dropdown
        /// </summary>
        public TrashMenuDropdown TrashDropdown { get; } = new TrashMenuDropdown();

        /// <summary>
        /// QuickCheckToolbarComponent
        /// </summary>
        public QuickCheckToolbarComponent SubmitToQuickCheckToolbarComponent { get; } = new QuickCheckToolbarComponent();

        /// <summary>
        /// StarPageNavigation component
        /// </summary>
        public StarPageNavigationComponent StarPageNavigationComponent { get; } = new StarPageNavigationComponent();

        /// <summary>
        /// Gets Keep List component.
        /// </summary>
        public PrecisionKeepListComponent KeepList { get; } = new PrecisionKeepListComponent();

        /// <summary>
        /// Sort By
        /// </summary>
        public override SortByDropdown SortBy { get; } = new SortByDropdown()
                                                             {
                                                                 AdditionalInfo = "Edge",
                                                                 SourceFolder = @"Resources/EnumPropertyMaps/WestlawEdge/Toolbars"
                                                             };
            
        /// <summary>
        /// This is the instance of the NavigationComponent we will use in the toolbar
        /// </summary>
        public new NavigationComponent NavigationComponent { get; protected set; } = new NavigationComponent();

        /// <summary>
        ///  Gets the quotation to quotation navigate component.
        /// </summary>
        public QuoteToQuoteNavigationComponent QuoteToQuoteNavigationComponent { get; protected set; } = new QuoteToQuoteNavigationComponent();

        /// <summary>
        /// Gets the navigate headings component.
        /// </summary>
        public NavigateHeadingsComponent NavigateHeadingsComponent { get; } = new NavigateHeadingsComponent();

        /// <summary>
        /// Toolbar Options Map
        /// </summary>
        protected new virtual EnumPropertyMapper<EdgeToolbarElements, WebElementInfo> ToolbarMap =>
            EnumPropertyModelCache.GetMap<EdgeToolbarElements, WebElementInfo>(
                                  string.Empty,
                                  @"Resources/EnumPropertyMaps/WestlawEdge/Toolbars");

        /// <summary>
        /// Toolbar Box Map
        /// </summary>
        protected virtual EnumPropertyMapper<EdgeToolbarInfoBoxes, WebElementInfo> InfoBoxesMap =>
            EnumPropertyModelCache.GetMap<EdgeToolbarInfoBoxes, WebElementInfo>(
                                  string.Empty,
                                  @"Resources/EnumPropertyMaps/WestlawEdge/Toolbars");
        
        /// <summary>
        /// Get InfoBox
        /// </summary>
        /// <param name="infoBox"></param>
        /// <returns></returns>
        public IInfoBox GetInfoBox(EdgeToolbarInfoBoxes infoBox)
        {
            By infoBoxLocator = By.XPath(this.InfoBoxesMap[infoBox].LocatorString);
            return this.InfoBoxesMap[infoBox].ClassName == null ? new InfoBox(infoBoxLocator) :
                new InfoBox(infoBoxLocator, By.ClassName(this.InfoBoxesMap[infoBox].ClassName), By.XPath(".//*[contains(@class, 'closeButton')]"));            
        }

        /// <summary>
        /// Click on the toolbar button.
        /// </summary>
        /// <param name="toolbarElement"> Toolbar option to click. </param>
        public virtual void ClickToolbarElement(EdgeToolbarElements toolbarElement)
        {
            DriverExtensions.GetElement(By.XPath(this.ToolbarMap[toolbarElement].LocatorString)).CustomClick();
            DriverExtensions.WaitForJavaScript();
        }
        
        /// <summary>
        /// Click on the toolbar button.
        /// </summary>
        /// <typeparam name="T">Page returned from clicking the button.</typeparam>
        /// <param name="toolbarElement">Element to click on the toolbar.</param>
        /// <returns>The expected page.</returns>
        public T ClickToolbarElement<T>(EdgeToolbarElements toolbarElement) where T : ICreatablePageObject =>
            this.ClickToolbarElementAndCreatePageObject<T>(toolbarElement);

        /// <summary>
        /// Click Search Within button
        /// </summary>
        /// <returns> The <see cref="EdgeSearchWithinDialog"/>. </returns>
        public EdgeSearchWithinDialog OpenSearchWithinDialog() =>
            this.ClickToolbarElementAndCreatePageObject<EdgeSearchWithinDialog>(EdgeToolbarElements.SearchWithin, "Toolbar");

        /// <summary>
        /// Verify that toolbar option is displayed
        /// </summary>
        /// <param name="toolbarElement"> Option to verify </param>
        /// <returns> True if the toolbar option is visible, false otherwise </returns>
        public bool IsToolbarElementDisplayed(EdgeToolbarElements toolbarElement) => 
            DriverExtensions.IsDisplayed(new ByChained(ToolbarLocator, By.XPath($".{this.ToolbarMap[toolbarElement].LocatorString}")));       

        /// <summary>
        /// Verify that toolbar option is enabled
        /// </summary>
        /// <param name="toolbarElement"> Option to verify </param>
        /// <returns> True if the toolbar option is enabled, false otherwise </returns>
        public bool IsToolbarElementEnabled(EdgeToolbarElements toolbarElement)
            => !DriverExtensions.WaitForElement(By.XPath(this.ToolbarMap[toolbarElement].LocatorString)).GetAttribute("class").Contains("disable");

        /// <summary>
        /// Get Toolbar option's title
        /// </summary>
        /// <param name="toolbarElement"> Toolbar option </param>
        /// <returns>
        /// string
        /// </returns>
        public string GetToolbarElementTitle(EdgeToolbarElements toolbarElement)
            => DriverExtensions.WaitForElement(By.XPath(this.ToolbarMap[toolbarElement].LocatorString)).GetAttribute("title");

        /// <summary>
        /// Get Toolbar option's text
        /// </summary>
        /// <param name="toolbarElement"> Toolbar option </param>
        /// <returns> Toolbar option text </returns>
        public string GetToolbarElementText(EdgeToolbarElements toolbarElement)
            => DriverExtensions.GetText(By.XPath(this.ToolbarMap[toolbarElement].LocatorString));

        /// <summary>
        /// Get Toolbar Element Tooltip
        /// </summary>
        /// <param name="toolbarElement">
        /// toolbarElement
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetToolbarElementTooltip(EdgeToolbarElements toolbarElement) =>
            DriverExtensions.GetElement(By.XPath(this.ToolbarMap[toolbarElement].LocatorString)).GetAttribute("title");

        /// <summary>
        /// Click on the toolbar button.
        /// </summary>
        /// <typeparam name="T">Page returned from clicking the button.</typeparam>
        /// <param name="toolbarElement">Element to click on the toolbar.</param>
        /// /// <param name="args"> Constructor parameters </param>
        /// <returns>The expected page.</returns>
        private T ClickToolbarElementAndCreatePageObject<T>(EdgeToolbarElements toolbarElement, params object[] args) where T : ICreatablePageObject
        {
            this.ClickToolbarElement(toolbarElement);
            return DriverExtensions.CreatePageInstance<T>(args);
        }
    }
}