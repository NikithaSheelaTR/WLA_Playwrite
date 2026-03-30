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
            AiJurisdictionalSurveysPage jurisdictionalSurveysPage = homePage.FeaturesIncludedPanel.GetWidgetLinkByTitle(JurisdictionalLabel).Click<AiJurisdictionalSurveysPage>();

            BrowserPool.CurrentBrowser.CreateTab(JurisdictionalSurveysTab);
            BrowserPool.CurrentBrowser.ActivateTab(JurisdictionalSurveysTab);

            Thread.Sleep(2000);
            jurisdictionalSurveysPage.ClosePendoMessage();
            SafeMethodExecutor.ExecuteUntil(() => jurisdictionalSurveysPage.PageDescription.Displayed, timeoutFromSec: 10);
            SafeMethodExecutor.ExecuteUntil(() => jurisdictionalSurveysPage.Jurisdictions.SelectedCountLabel.Displayed, timeoutFromSec: 15);

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
        /// </summary>
        protected void WaitForSurveyResultsLoaded(AiJurisdictionalSurveysPage surveysPage, int timeoutFromSec = 30)
        {
            SafeMethodExecutor.WaitUntil(
                () => surveysPage.WlaSurveyResult.GetAllJurisdictionLabels()
                    .Any(text => !string.IsNullOrWhiteSpace(text)),
                timeoutFromSec: timeoutFromSec);
            Thread.Sleep(6000); // Extra wait to ensure all elements are loaded after labels are present
            // Wait for the Federal Statutes heading to appear — this indicates the survey is fully
            // rendered and the toolbar buttons (save-to-folder, copy-link) are enabled.
            SafeMethodExecutor.WaitUntil(
                () => surveysPage.WlaSurveyResult.FederalStatutesRegulationsHeading.Displayed,
                timeoutFromSec: 20);
        }
    }
}

