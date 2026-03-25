namespace Framework.Common.UI.Products.WestlawEdge.Components.ResultList
{
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Snippet navigation component
    /// </summary>
    public class EdgeSnippetNavigationComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[contains(@class, 'snippetsContainer')]");
        private static readonly By SnippetLinkLocator = By.XPath(".//div[@class= 'co_snippet']/a");
        private static readonly By NavigationPanelLabelLocator = By.XPath(".//span[@class = 'statusText']");
        private static readonly By PreviousButtonLocator = By.XPath(".//button[contains(@class, 'co_prev co_tbButton')]");
        private static readonly By NextButtonLocator = By.XPath(".//button[contains(@class, 'co_next co_tbButton')]");
        private static readonly By InfoBoxLocator = By.XPath(".//div[@class = 'co_infoBox_message']");
        
        /// <summary>
        /// Initializes a new instance of the <see cref="EdgeSnippetNavigationComponent"/> class.
        /// </summary>
        /// <param name="resultListItemContainer"> result list container</param>
        public EdgeSnippetNavigationComponent(IWebElement resultListItemContainer)
        {
            this.ContainerElement = DriverExtensions.GetElement(resultListItemContainer, ContainerLocator);
        }

        /// <summary>
        /// Navigation panel label
        /// </summary>
        public ILabel NavigationPanelLabel => new Label(this.ContainerElement, NavigationPanelLabelLocator);

        /// <summary>
        /// Yellow info box
        /// </summary>
        public ILabel InfoBox => new Label(this.ContainerElement, InfoBoxLocator);

        /// <summary>
        /// Previous button
        /// </summary>
        public IButton PreviousButton => new Button(this.ContainerElement, PreviousButtonLocator);
        
        /// <summary>
        /// Next button
        /// </summary>
        public IButton NextButton => new Button(this.ContainerElement, NextButtonLocator);
        
        /// <summary>
        /// Snippet list
        /// </summary>
        public IReadOnlyCollection<ILink> SnippetList =>
            new ElementsCollection<Link>(this.ContainerElement, SnippetLinkLocator);

        /// <summary>
        /// Snippet navigation container
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Result list container locator + snippet navigation container locator
        /// </summary>
        protected IWebElement ContainerElement { get; set; }
    }
}