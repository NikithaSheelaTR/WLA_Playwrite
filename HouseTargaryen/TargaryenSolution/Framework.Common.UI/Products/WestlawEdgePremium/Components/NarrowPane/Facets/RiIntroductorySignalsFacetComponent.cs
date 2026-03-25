namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.NarrowPane.Facets
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using System.Collections.Generic;

    /// <summary>
    /// Citing proximity facet
    /// </summary>
    public class RiIntroductorySignalsFacetComponent : BaseSearchHierarchyFacetComponent
    {
        private static readonly By MoreInfoButtonLocator = By.XPath("//*[@aria-label = 'Introductory Signals More info']");
        private static readonly By IntroSignalsBreadCrumbLocator = By.ClassName("SearchFacet-breadcrumbText");

        /// <summary>
        /// Initializes a new instance of the <see cref="Framework.Common.UI.Products.WestlawEdgePremium.Components.NarrowPane.Facets.RiIntroductorySignalsFacetComponent"/> class.
        /// </summary>
        /// <param name="componentLocator">
        /// The container locator.
        /// </param>
        public RiIntroductorySignalsFacetComponent(By componentLocator) : base(componentLocator)
        {
        }

        /// <summary>
        /// More info button
        /// </summary>
        public IButton MoreInfoButton => new Button(this.ComponentLocator, MoreInfoButtonLocator);

        /// <summary>
        /// Applied signals filter text
        /// </summary>
        public ILabel BreadCrumb => new Label(this.ComponentLocator, IntroSignalsBreadCrumbLocator);

        /// <summary>
        /// Returns list of Introductory Signals applied
        /// </summary>
        public IList<string> GetAppliedIntroSignals()
        {
            DriverExtensions.WaitForElement(IntroSignalsBreadCrumbLocator);
            IReadOnlyCollection<IWebElement> elementList = DriverExtensions.GetElements(IntroSignalsBreadCrumbLocator);
            List<string> appliedFilterList = new List<string>();

            foreach (var element in elementList)
            {
                appliedFilterList.Add(element.Text);
            }

            return appliedFilterList;
        }
    }
}

