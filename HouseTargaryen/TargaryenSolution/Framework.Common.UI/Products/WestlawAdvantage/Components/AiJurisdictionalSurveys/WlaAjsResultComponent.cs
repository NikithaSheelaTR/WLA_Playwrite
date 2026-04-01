namespace Framework.Common.UI.Products.WestlawAdvantage.Components.AiJurisdictionalSurveys
{
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// WLA AI Jurisdictional Surveys result component
    /// </summary>
    public class WlaAjsResultComponent : AiJurisdictionalSurveysResultComponent
    {
        private static readonly By FederalStatutesRegulationsHeadingLabelLocator = By.XPath("//h4[contains(@class,'resultsSummaryHeading') and normalize-space(.)='Federal']/following-sibling::h5[contains(@class,'resultsSummarySubHeading') and normalize-space(.)='Statutes and regulations']");
        private static readonly By AllJurisdictionHeadingsLocator = By.XPath("//h4[contains(@class,'resultsSummaryHeading')]");
        private static readonly By CasesStateHeadingLabelLocator = By.XPath("//h5[contains(@class, 'casesSectionHeading') and normalize-space(.)='Cases']/ancestor::*[contains(@class, 'casesSection')][1]//h6[contains(@class,'resultsSummaryCases') and normalize-space(.)='State']");
        private static readonly By CasesFederalHeadingLabelLocator = By.XPath("//h5[contains(@class, 'casesSectionHeading') and normalize-space(.)='Cases']/ancestor::*[contains(@class, 'casesSection')][1]//h6[contains(@class,'resultsSummaryCases') and normalize-space(.)='Federal']");

        /// <summary>
        /// Statutes and Regulations heading under Federal
        /// </summary>
        public ILabel FederalStatutesRegulationsHeading => new Label(FederalStatutesRegulationsHeadingLabelLocator);

        /// <summary>
        /// State heading under Cases
        /// </summary>
        public ILabel CasesStateHeading => new Label(CasesStateHeadingLabelLocator);

        /// <summary>
        /// Federal heading under Cases
        /// </summary>
        public ILabel CasesFederalHeading => new Label(CasesFederalHeadingLabelLocator);

        /// <summary>
        /// Returns all jurisdiction heading labels from the WLA result page.
        /// Overrides the base implementation to search globally for h4 headings —
        /// on WLA pages state sections live outside the base resultJurisdiction container.
        /// </summary>
        public new List<string> GetAllJurisdictionLabels()
            => DriverExtensions.GetElements(AllJurisdictionHeadingsLocator)
                .Select(e => e.Text)
                .ToList();
    }
}