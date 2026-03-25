namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.ResultList
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Items.ResultList;
    using Framework.Common.UI.Products.WestlawEdge.Components.ResultList;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Complaint Analyser Event Timeline result list
    /// </summary>
    public class ComplaintAnalyserEventsResultListComponent : EdgeLegacyResultListComponent
    {
        private static readonly By EventsCardLocator = By.XPath(".//li[contains(@class,'EventTimelineSection-module__sectionContainer')]");
        private static readonly By EventsCardPlantiffLocator = By.XPath(".//b[contains(text(),'Plaintiff(s):')]//following-sibling::div | .//p[contains(text(),'Plaintiff(s):')]//following-sibling::ul");
        private static readonly By EventsCardDefendantLocator = By.XPath(".//b[contains(text(),'Defendant(s):')]//following-sibling::div | .//p[contains(text(),'Defendant(s):')]//following-sibling::ul");
        private static readonly By EventsTypeLocator = By.XPath(".//li[@data-testid ='event-timeline-type']");

        /// <summary>
        /// Initializes a new instance of the  class. 
        /// </summary>
        /// <param name="container"></param>
        public ComplaintAnalyserEventsResultListComponent(IWebElement container) : base(container)
        {
            this.Container = container;
        }

        /// <summary>
        /// Event timeline card plantif list locator
        /// </summary>
        public IReadOnlyCollection<ILabel> EventsCardPlantiffLabels => new ElementsCollection<Label>(this.Container, EventsCardPlantiffLocator);

        /// <summary>
        /// Event timeline Defendant list locator
        /// </summary>
        public IReadOnlyCollection<ILabel> EventsCardDefendantLabels => new ElementsCollection<Label>(this.Container, EventsCardDefendantLocator);

        /// <summary>
        /// Event timeline Defendant list locator
        /// </summary>
        public IReadOnlyCollection<ILabel> EventsCardLabels => new ElementsCollection<Label>(this.Container, EventsCardLocator);

        /// <summary>
        /// Event timeline types list
        /// </summary>
        public IReadOnlyCollection<ILabel> EventsTypeLabels => new ElementsCollection<Label>(this.Container, EventsTypeLocator);

        private IWebElement Container { get; }
    }
}
