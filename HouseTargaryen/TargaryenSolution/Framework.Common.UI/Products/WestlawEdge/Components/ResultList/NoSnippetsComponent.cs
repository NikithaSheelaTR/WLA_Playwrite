namespace Framework.Common.UI.Products.WestlawEdge.Components.ResultList
{
    using System.Linq;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// No snippets component
    /// </summary>
    public class NoSnippetsComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath(".//div[@class = 'noSnippetsInfoBox']");
        private static readonly By ShowOriginalButtonLocator = By.XPath(".//button[@class = 'co_tbButton co_showOriginalButton']");
        private static readonly By SkipToNextButtonLocator = By.CssSelector("button.co_tbButton.co_skipToNextButton");
        private static readonly By InfoboxLocator = By.XPath(".//div[@class = 'co_infoBox_message']/span");
        private static readonly By InfoboxColorLocator = By.XPath("./div[contains(@class, 'co_infoBox')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="NoSnippetsComponent"/> class.
        /// </summary>
        /// <param name="resultListItemContainer"> result list container</param>
        public NoSnippetsComponent(IWebElement resultListItemContainer)
        {
            this.ResultListContainer = resultListItemContainer;
        }

        /// <summary>
        /// Show/Hide original button
        /// </summary>
        public IButton ShowOriginalButton => new Button(this.ContainerElement, ShowOriginalButtonLocator);

        /// <summary>
        /// Skip to next button 
        /// </summary>
        public IButton SkipToNextButton => new JsClickButton(this.ContainerElement, SkipToNextButtonLocator);

        /// <summary>
        /// Infobox
        /// </summary>
        public ILabel Infobox =>
            new Label(
                DriverExtensions.GetElements(this.ContainerElement, InfoboxLocator)
                                .First(i => !i.GetAttribute("class").Contains("co_hideState")));

        /// <summary>
        /// No snippets container locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Result list container locator + no snippets container locator
        /// </summary>
        protected IWebElement ContainerElement => DriverExtensions.GetElement(this.ResultListContainer, this.ComponentLocator);

        /// <summary>
        /// Result list container locator 
        /// </summary>
        private IWebElement ResultListContainer { get; }

        /// <summary>
        /// Get Infobox color
        /// </summary>
        /// <returns>Css value of background-color property</returns>
        public string GetInfoboxColor() =>
            DriverExtensions.GetElement(this.ContainerElement, InfoboxColorLocator).GetCssValue("background-color");
    }
}