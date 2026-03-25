namespace Framework.Common.UI.Products.WestlawEdge.Components.EdgeResponsive
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Enums.RI;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Left panel component containing Content types on RI pages
    /// </summary>
    public class ContentTypesLeftPanelComponent : BaseModuleRegressionComponent
    {
        private static readonly EnumPropertyMapper<HistorySections, WebElementInfo> HistorySectionsMap =
            EnumPropertyModelCache.GetMap<HistorySections, WebElementInfo>("WestlawEdge");

        private static readonly By ContainerLocator = By.Id("co_leftColumn");
        private static readonly By SearchWithinTextAreaLocator = By.Id("co_searchWithinWidget_textArea");
        private static readonly By CollapseButtonLocator = By.Id("co_collapseButtonLeft");
        private static readonly By CloseButtonLocator = By.XPath("//button[contains(@class, 'Panel-close')]");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Search within button
        /// </summary>
        public IButton SearchWithinButton => new Button(this.ComponentLocator, SearchWithinTextAreaLocator);

        /// <summary>
        /// Collapse button for panel with filters
        /// </summary>
        public IButton CollapseButton => new Button(this.ComponentLocator, CollapseButtonLocator);

        /// <summary>
        /// Close button for panel with table of contents
        /// </summary>
        public IButton CloseButton => new Button(this.ComponentLocator, CloseButtonLocator);

        /// <summary>
        /// Switch to History Section by click on section on left panel
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="historySection">The history Section.</param>
        /// <returns>The new instance of T page.</returns>
        public T SwitchToHistorySection<T>(HistorySections historySection) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(By.Id(HistorySectionsMap[historySection].Id)).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Verify if scroll bar displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsScrollbarDisplayed()
        {
            IWebElement footnoteHoverContent = DriverExtensions.SafeGetElement(ComponentLocator);
            return footnoteHoverContent != null
                   && footnoteHoverContent.GetElementScrollHight() > footnoteHoverContent.Size.Height;
        }
    }
}

