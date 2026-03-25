namespace Framework.Common.UI.Products.WestlawAdvantage.Components.QuickCheck
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawAdvantage.Enums.LitigationDocumentAnalyzer;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck;
    using Framework.Common.UI.Products.WestlawEdge.Enums.QuickCheck;
    using Framework.Common.UI.Products.WestlawEdge.Pages.QuickCheck;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;
    using System.Collections.Generic;

    /// <summary>
    /// Argument Counterargument narrow pane
    /// </summary>
    public class ArgumentCounterargumentNarrowPaneComponent : RecommendationsNarrowPaneComponent
    {
        private static readonly By NavigationLinkLabelLocator = By.XPath(".//saf-tree-view[@role='tree']/saf-tree-item");
        private static readonly By ArgumentsTitlesLinksLocator = By.XPath(".//saf-tree-view[@role='tree']/saf-tree-item[text()='Arguments']/saf-tree-item");
        private static readonly By RelatedArgumentsLinksLocator=By.XPath(".//saf-tree-view[@role='tree']/saf-tree-item[text()='Related arguments']/saf-tree-item");
        private static readonly By RelatedDefensesLinksLocator = By.XPath(".//saf-tree-view[@role='tree']/saf-tree-item[text()='Related defenses']/saf-tree-item");
        private static string ExpandNavigationMsk = "div.expand-collapse-button";

        /// <summary>
        /// Sort Navigation Type Map
        /// </summary>
        private EnumPropertyMapper<ArgumentsCounterargumentsNavigationType, WebElementInfo> NavigationTypeMap =>
            EnumPropertyModelCache.GetMap<ArgumentsCounterargumentsNavigationType, WebElementInfo>("", @"Resources\EnumPropertyMaps\WestlawAdvantage");

        /// <summary> 
        /// Navigation Link Label
        /// </summary>
        public ILabel NavigationLinkLabel => new Label(NavigationLinkLabelLocator);

        /// <summary>
        /// Click Navigation link
        /// </summary>
        /// <param name="type">Navigation type</param>
        /// <returns>The <see cref="QuickCheckRecommendationsPage"/></returns>
        public void ExpandNavigationLink(ArgumentsCounterargumentsNavigationType type)
        {
            IWebElement element = (IWebElement)DriverExtensions.ExecuteScript($"return(arguments[0].shadowRoot.querySelector('{ExpandNavigationMsk}'));",
            DriverExtensions.GetElement(By.XPath(this.NavigationTypeMap[type].LocatorString)));
            element.Click();
        }

        /// <summary>
        /// Arguments Titles Links
        /// </summary>
        public IReadOnlyCollection<ILink> ArgumentsTitlesLinks => new ElementsCollection<Link>(this.ComponentLocator, ArgumentsTitlesLinksLocator);

        /// <summary>
        /// Related Arguments Titles Links
        /// </summary>
        public IReadOnlyCollection<ILink> RelatedArgumentsTitlesLinks => new ElementsCollection<Link>(this.ComponentLocator, RelatedArgumentsLinksLocator);

        /// <summary>
        /// Related Defenses Titles Links
        /// </summary>
        public IReadOnlyCollection<ILink> RelatedDefensesTitlesLinks => new ElementsCollection<Link>(this.ComponentLocator, RelatedDefensesLinksLocator);
    }
}
