namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.WestlawEdgePremium.Items.AALP;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// AI Jurisdictional Surveys result component
    /// </summary>
    public class AiJurisdictionalSurveysResultComponent : BaseModuleRegressionComponent
    {
        private static readonly By ResultsContainerLocator = By.XPath("//div[contains(@class,'resultJurisdiction')]");
        private static readonly By QuestionLabelLocator = By.XPath(".//*[@class='__resultsQuestion--Z7ISF25gugcWzcAU3CSN']/h3");
        private static readonly By SummaryLabelLocator = By.XPath(".//*@class='__resultsSummaryContent--toywiWFAJuqNKJ1F5knn']");
        private static readonly By ResultItemLocator = By.XPath(".//*[@class='__resultJurisdiction--uvjNgz4aJd9WpypGQBc2']");
        private static readonly By TimeStampLabelLocator = By.XPath("//time[contains(@class,'resultsTimeStamp')]");
        private static readonly By DisclaimerLabelLocator = By.XPath("//p[contains(@class,'disclaimer')]");
        private static readonly By JurisdictionNameLocator = By.XPath(".//h4");
        private const string StateStatutesRegulationsHeadingLabelLctMask = ".//h4[contains(@class,'resultsSummaryHeading') and normalize-space(.)='{0}']/following-sibling::h5[contains(@class,'resultsSummarySubHeading') and normalize-space(.)='State statutes and regulations']";

        //TODO: filter panel
        //TODO: result toggle

        /// <summary>
        /// Disclaimer label
        /// </summary>
        public ILabel DisclaimerLabel => new Label(this.ComponentLocator, DisclaimerLabelLocator);

        /// <summary>
        /// Question label
        /// </summary>
        public ILabel QuestionLabel => new Label(this.ComponentLocator, QuestionLabelLocator);

        /// <summary>
        /// Survey results summary label
        /// </summary>
        public ILabel SummaryLabel => new Label(this.ComponentLocator, SummaryLabelLocator);

        /// <summary>
        /// TimeStamp Label
        /// </summary>
        public ILabel TimeStampLabel => new Label(TimeStampLabelLocator);

        /// <summary>
        /// State statutes and regulations heading under a state
        /// </summary>
        /// <param name="stateName">Name of US state</param>
        public ILabel StateStatutesRegulationsHeading(string stateName)
            => new Label(By.XPath(string.Format(StateStatutesRegulationsHeadingLabelLctMask, stateName)));

        /// <summary>
        /// Survey results items
        /// </summary>
        /// <returns>List of survey result items</returns>
        public ItemsCollection<AiJurisdictionalurveysResultItem> ResultItems => new ItemsCollection<AiJurisdictionalurveysResultItem>(this.ComponentLocator, new ByChained(this.ComponentLocator, ResultItemLocator));

        /// <summary>
        /// Gets all jurisdiction labels.
        /// </summary>
        /// <returns>A list of jurisdiction label texts.</returns>
        public List<string> GetAllJurisdictionLabels()
        {
            var labelElements = DriverExtensions.GetElements(new ByChained(this.ComponentLocator, JurisdictionNameLocator));
            return labelElements.Select(e => e.Text).ToList();
        }

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ResultsContainerLocator;
    }
}



