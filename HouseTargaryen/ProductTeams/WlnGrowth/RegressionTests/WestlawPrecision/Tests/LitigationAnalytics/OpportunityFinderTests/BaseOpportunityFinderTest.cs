namespace WestlawPrecision.Tests.LitigationAnalytics.OpportunityFinderTests
{
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsHomePageComponents.Entities;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums.LitigationAnalytics;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages.LitigationAnalytics;
    using Framework.Core.Utils.Execution;
    using System.Threading;
    public class BaseOpportunityFinderTest : BaseAnalyticsTest
    {
        public LitigationAnalyticsOpportunityFinderProfilerPage SelectMergersAndAcquisitions(OpportunityFinderCompanydropdownItems companyItem)
        {

            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var opportunityFinderTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsOpportunityFinderEntityTabComponent>(LitigationAnalyticsProfiles.OpportunityFinder);
            opportunityFinderTabComponent.CompanyMenuButton.Click<LitigationAnalyticsOpportunityFinderEntityTabComponent>().CompanyMenuItem(companyItem).Click();
            var opportunityFinderPage = opportunityFinderTabComponent.ContinueButton.Click<LitigationAnalyticsOpportunityFinderProfilerPage>();
            var MergersAndAcquisitionsRadiobutton = opportunityFinderPage.StepFirstLegalActivityComponent.LegalActivityTypeRadiobutton(OpportunityFinderChooseOneLegalActivityType.MergersAndAcquisitions);
            MergersAndAcquisitionsRadiobutton.Select();
            Thread.Sleep(1000);

            SafeMethodExecutor.WaitUntil(() => MergersAndAcquisitionsRadiobutton.Selected);

            return opportunityFinderPage;
        }

        public LitigationAnalyticsOpportunityFinderProfilerPage SelectPatents(OpportunityFinderCompanydropdownItems companyItem)
        {

            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var opportunityFinderTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsOpportunityFinderEntityTabComponent>(LitigationAnalyticsProfiles.OpportunityFinder);
            opportunityFinderTabComponent.CompanyMenuButton.Click<LitigationAnalyticsOpportunityFinderEntityTabComponent>().CompanyMenuItem(companyItem).Click();
            var opportunityFinderPage = opportunityFinderTabComponent.ContinueButton.Click<LitigationAnalyticsOpportunityFinderProfilerPage>();
            var MergersAndAcquisitionsRadiobutton = opportunityFinderPage.StepFirstLegalActivityComponent.LegalActivityTypeRadiobutton(OpportunityFinderChooseOneLegalActivityType.PatentAssignments);
            MergersAndAcquisitionsRadiobutton.Select();
            Thread.Sleep(1000);

            SafeMethodExecutor.WaitUntil(() => MergersAndAcquisitionsRadiobutton.Selected);

            return opportunityFinderPage;
        }
    }
}
