namespace WestlawAdvantage.Tests.AJSForWLAdvantage
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Core.Utils.Execution;
    using Framework.Common.UI.Products.WestlawAdvantage.Pages;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages.AALP;
    using System.Threading;
    using Framework.Common.UI.Products.Shared.Managers;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;

    /// <summary>
    /// AJS on WLA Base Test class
    /// </summary>
    [TestClass]
    public class WlaAjsBaseTest : WlaBaseTest
    {
        protected const string AjsTestFolder = "WlaAjsTestFolder";

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
    }
}

