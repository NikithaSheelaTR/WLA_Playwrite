namespace WestlawAdvantage.Tests.AJSForWLAdvantage
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Core.Utils.Execution;
    using Framework.Common.UI.Products.WestlawAdvantage.Pages;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages.AALP;
    using System.Linq;
    using System.Threading;
    using Framework.Common.UI.Products.Shared.Managers;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.CommonTypes.Constants;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.Utils.Extensions;

    /// <summary>
    /// AJS on WLA Base Test class
    /// </summary>
    [TestClass]
    public class WlaAjsBaseTest : WlaBaseTest
    {
        protected const string AjsTestFolder = "WlaAjsTestFolder";

        protected override void InitializeRoutingPageSettings()
        {
            base.InitializeRoutingPageSettings();

            this.Settings.AppendValues(
                EnvironmentConstants.FeatureAccessControlsOn,
                SettingUpdateOption.Append,
                FeatureAccessControl.LinkBuilder);
        }

        /// <summary>
        /// Go to AJS landing page by clicking the feature card on home page.
        /// </summary>
        protected AiJurisdictionalSurveysPage NavigateToLandingPage()
        {
            const string JurisdictionalLabel = "AI Jurisdictional Surveys";

            var homePage = this.GetHomePage<AdvantageHomePage>();
            var widgetLink = homePage.FeaturesIncludedPanel.GetWidgetLinkByTitle(JurisdictionalLabel);
            SafeMethodExecutor.WaitUntil(() => widgetLink.Displayed, timeoutFromSec: 15);
            AiJurisdictionalSurveysPage jurisdictionalSurveysPage = widgetLink.Click<AiJurisdictionalSurveysPage>();

            BrowserPool.CurrentBrowser.CreateTab(JurisdictionalSurveysTab);
            BrowserPool.CurrentBrowser.ActivateTab(JurisdictionalSurveysTab);

            Thread.Sleep(2000);
            jurisdictionalSurveysPage.ClosePendoMessage();
            SafeMethodExecutor.WaitUntil(() => jurisdictionalSurveysPage.PageDescription.Displayed, timeoutFromSec: 10);
            SafeMethodExecutor.WaitUntil(() => jurisdictionalSurveysPage.Jurisdictions.SelectedCountLabel.Displayed, timeoutFromSec: 15);

            return jurisdictionalSurveysPage;
        }

        /// <summary>
        /// If test folder does not exist, create it. If it exists, delete all contents.
        /// </summary>
        protected void PrepareTestFolder()
        {
            var researchOrganizerPage = EdgeNavigationManager.Instance.GoToFoldersPage<EdgeResearchOrganizerPage>();
            if (!researchOrganizerPage.LeftFolder.FolderTree.IsFolderExist(AjsTestFolder))
                researchOrganizerPage.CreateNewFolder(AjsTestFolder);
            else
            {
                researchOrganizerPage.LeftFolder.FolderTree.SelectFolderByName(AjsTestFolder);
                researchOrganizerPage.ClearFolderGrid();
            }
            researchOrganizerPage.Header.ClickLogo<AdvantageHomePage>();
        }

        /// <summary>
        /// Wait for survey results to fully load by polling until jurisdiction labels are present
        /// and the Federal Statutes heading is visible (which indicates toolbar buttons are enabled).
        /// WLA AI surveys can take several minutes to generate; use a 10-minute timeout.
        /// </summary>
        protected void WaitForSurveyResultsLoaded(AiJurisdictionalSurveysPage surveysPage, int timeoutFromSec = 600)
        {
            // First: wait for the timestamp — this is set only when the AI has fully generated the result.
            // Waiting for this is the generic signal; the page's WaitForResultsLoaded encapsulates it.
            surveysPage.WaitForResultsLoaded(timeoutFromSec);

            // Then: wait for the WLA-specific Federal Statutes heading which confirms the full
            // result set (including include-related-federal content) has rendered.
            SafeMethodExecutor.WaitUntil(
                () => surveysPage.WlaSurveyResult.FederalStatutesRegulationsHeading.Displayed,
                timeoutFromSec: 60);
        }
    }
}

