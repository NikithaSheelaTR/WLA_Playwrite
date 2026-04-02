namespace WestlawPrecision.Tests.Aalp
{
    using Framework.Common.UI.DataModel;
    using Framework.Common.UI.Products.Shared.DomainObjects.SignOn;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.WestLawNext.Utils;
    using Framework.Common.UI.Utils;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.CommonTypes.Constants;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.DataModel.Security;
    using Framework.Core.DataModel.Security.Specialized;
    using Framework.Core.Utils;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.IO;
    using System.Linq;
    using Framework.Common.UI.Interfaces;
    using Framework.Core.DataModel.Security.Proxies;
    using Framework.Core.Utils.Execution;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages.AALP;
    using Framework.Common.UI.Products.WestlawAdvantage.Pages.DeepResearch;

    /// <summary>
    /// The AALP base test.
    /// </summary>
    [DeploymentItem(@"Resources\RP.config.json")]
    [DeploymentItem("Resources/TestData/Aalp/ComplaintAnalyzerDocs")]
    [DeploymentItem("Resources/TestData/Aalp/CocounselQuickCheckDoc")]
    [TestClass]
    public class AalpBaseTest : BaseWestlawTest
    {
        protected const string CurrentTestCategory = "Aalp";
        protected const string SmokeTestCategory = "AalpSmoke";
        protected const string TeamDahlNonAjsCategory = "TeamDahlNonAjsTests";
        protected const string TeamDahlCategory = "TeamDahlTests";
        protected const string TeamMatzekCategory = "AalpMatzekTests";
        protected const string TeamSahniCategory = "AalpSahniTests";
        protected const string TeamSahniFedRampCategory = "AalpSahniFedRampTests";
        protected const string TeamMatzekFedRampCategory = "AalpMatzekFedRampTests";
        protected const string TeamMatzekAdvantageSmokeCategory = "AalpMatzekAdvantageSmokeTests";
        protected const string TeamSahniSmokeTestCategory = "AalpSahniSmokeTests";

        protected const string AiAssistedResearchTab = "AI-Assisted Research page";
        protected const string BrowsePageTab = "Browse Page";
        protected const string SecondarySourcesContentTypePageTab = "Secondary Sources Page";
        protected const string ClaimsExplorerTab = "Claims Explorer page";
        protected const string HistoryPageTab = "History page";
        protected const string HomePageTab = "Home page";
        protected const string DocumentTab = "Document page";
        protected const string NegativeTreatmentTab = "Negative treatment page";
        protected const string JurisdictionalSurveysTab = "Jurisdictional Surveys page";
        protected const string QuickCheckTab = "Quick Check page";

        protected const string AIAssistedResearchHeadingLabel = "AI-Assisted Research";
        protected const string SearchAndSummarizeTax = "Search & Summarize Tax";
        protected const string ClaimsExplorerHeadingLabel = "Claims Explorer";
        protected const string AIJurisdictionalSurveysHeadingLabel = "AI Jurisdictional Surveys";

        protected const string FolderToSaveConst = @"C:\Temp\Aalp";
        protected const string DeliveryDateFormat = "MM-dd-yyyy";
       
        protected string TestDocsFolderPath = Environment.CurrentDirectory;

        /// <summary>
        /// Initializes a new instance of the <see cref="AalpBaseTest"/> class.
        /// </summary>
        public AalpBaseTest()
        {
            // IMPORTANT NOTE: NOW PLEASE CHANGE THE USER POOL IN LocalTestConfig.xml ('PASSWORD_POOL' field)

            this.FolderingManager = new FolderingUiManager(this.TestExecutionContext, this.DefaultCobaltProduct);

            this.UiExecutionSettings = UiExecutionSettings.SetFlags(
                UiExecutionFlags.AllowUiPreconditionRoutines,
                UiExecutionFlags.AllowScreenshotOnFailedQualityCheck);
        }

        /// <summary>
        /// Gets the foldering manager.
        /// </summary>
        protected FolderingUiManager FolderingManager { get; }

        /// <summary>
        /// The folder to save.
        /// </summary>
        /// <value>
        /// The folder to save items.
        /// </value>
        protected override string FolderToSave => Path.Combine(FolderToSaveConst, this.TestContext.TestName);

        /// <summary>
        /// Navigate to ClientId page using current user and without ClientId page
        /// </summary>
        /// <returns> The <see cref="CommonClientIdPage"/>. </returns>
        public CommonClientIdPage SignOnBack()
        {
            WlnUserInfo userInfo = GetUserInfo();
            userInfo.ClientId = null;
            return this.DefaultSignOnManager.SignOn<CommonClientIdPage, ISignOnContext<IUserInfo>>(
                new WlnSignOnContext<IUserInfo>(this.TestExecutionContext, userInfo));
        }

        /// <summary>
        /// Sign on to wln and clear cookies
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        protected T SignInToWlnAndClearCookies<T>(IUserInfo userInfo)
             where T : ICreatablePageObject => this.SignInToWlnAndClearCookies<T>(
             new WlnSignOnContext<IUserInfo>(this.TestExecutionContext, userInfo));

        /// <summary>
        /// Sign in to wln and clear cookies
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="userSignonContext">User sign on context</param>
        /// <returns></returns>
        protected T SignInToWlnAndClearCookies<T>(ISignOnContext<IUserInfo> userSignonContext) where T : ICreatablePageObject
        {
            ExecutionResult clearCookie = SafeMethodExecutor.Execute(DriverExtensions.DeleteAllCookies);

            if (clearCookie.ResultType == ExecutionResultType.Failure)
            {
                Console.WriteLine($"Could not clear cookies: {Environment.NewLine}{clearCookie.Details}");
            }

            return this.DefaultSignOnManager.SignOn<T, ISignOnContext<IUserInfo>>(userSignonContext);
        }

        /// <summary>
        /// On manage credential
        /// </summary>
        protected override void OnManageCredential()
        {
            if (TestContext.Properties["MutipleUserPools"] != null && TestContext.Properties["MutipleUserPools"].Equals("On"))
            {            
                var userCredential = new UserDbCredential(this.TestContext, PasswordVertical.WlnGrowth, "IndigoPremium")
                { ClientId = "Aalp Test" };

                var secondUser = new UserDbCredential(this.TestContext, PasswordVertical.WlnGrowth, "IndigoAcadia")
                { ClientId = "Aalp Test" };

                CredentialPool.RegisterUser(userCredential);
                CredentialPool.RegisterUser(secondUser);

                this.DefaultSignOnContext = new WlnSignOnContext<IUserInfo>(this.TestExecutionContext, userCredential.ToWlnUserInfo());

                this.FirstUserInfo = userCredential.ToWlnUserInfo();

                this.SecondUserInfo = CredentialPool.GetAllRegisteredUsers<WlnUserInfo>().Skip(1).FirstOrDefault();

                this.DefaultSignOnContext = new WlnSignOnContext<IUserInfo>(this.TestExecutionContext, this.FirstUserInfo);
            }
            else
            {
                var userCredential = new UserDbCredential(this.TestContext, PasswordVertical.WlnGrowth, this.GetUserPool())
                { ClientId = "Aalp Test" };

                CredentialPool.RegisterUser(userCredential);

                this.DefaultSignOnContext = new WlnSignOnContext<IUserInfo>(this.TestExecutionContext, userCredential.ToWlnUserInfo());

                this.CurrentUser = userCredential;
            }
        }

        /// <summary>
        /// First User Information
        /// </summary>
        protected WlnUserInfo FirstUserInfo { get; set; }

        /// <summary>
        /// Second User Information
        /// </summary>
        protected WlnUserInfo SecondUserInfo { get; set; }

        /// <summary>
        /// The get user info.
        /// </summary>
        /// <returns>
        /// The <see cref="WlnUserInfo"/>.
        /// </returns>
        protected WlnUserInfo GetUserInfo() => this.DefaultSignOnContext.UserInfo as WlnUserInfo;

        /// <summary>
        /// The perform ui postcondition routines.
        /// </summary>
        protected override void PerformUiPostconditionRoutines()
        {
            string executionDir = Path.GetDirectoryName(Path.GetDirectoryName(this.TestContext.DeploymentDirectory));
            string query = File.ReadAllText(Environment.CurrentDirectory + @"\RP.config.json");
            File.WriteAllText(executionDir + @"\ReportPortal.config.json", query);
            Logger.LogInfo($"Report portal config json was updated {executionDir}");

            if (Directory.Exists(FolderToSave))
            {
                Directory.Delete(FolderToSave, true);
            }

            if (TestContext.CurrentTestOutcome == UnitTestOutcome.Failed && !BrowserPool.CurrentBrowser.Driver.ToString().Contains("null"))
            {
                ScreenshotTaker.TakeScreenshotRp();
            }

            base.PerformUiPostconditionRoutines();
        }

        /// <summary>
        /// Initialize routing page settings
        /// </summary>
        protected override void InitializeRoutingPageSettings()
        {
            base.InitializeRoutingPageSettings();
        }

        private string GetUserPool() => this.TestContext.Properties["Pool"] != null
                       ? TestContext.Properties["Pool"].ToString()
                       : GetPasswordPool();

        protected void PrintSessionIdFromPageSource()
        {
            string pageSource = BrowserPool.CurrentBrowser.Driver.PageSource;
            Console.WriteLine("**SessionId:" + pageSource.Substring(pageSource.IndexOf(",\"SessionId") + 13, 34));
        }

        /// <summary>
        /// Waits for AI Jurisdictional Survey results to be fully loaded.
        /// Replaces the pattern: WaitUntil(!surveysPage.ProgressLabel.Displayed) + immediate element read,
        /// which is a race condition — the spinner can disappear before the DOM is rendered.
        /// </summary>
        /// <param name="surveysPage">The surveys page instance.</param>
        /// <param name="timeoutFromSec">Maximum seconds to wait. Defaults to 600.</param>
        protected void WaitForSurveyResultsLoaded(AiJurisdictionalSurveysPage surveysPage, int timeoutFromSec = 600)
            => surveysPage.WaitForResultsLoaded(timeoutFromSec);

        /// <summary>
        /// Waits for AI Deep Research results to be fully rendered.
        /// Replaces the pattern: WaitUntil(!ProgressBarLabel.Displayed) + immediate element read,
        /// which is a race condition — the spinner can disappear before the result DOM is ready.
        /// </summary>
        /// <param name="deepResearchPage">The deep research page instance.</param>
        /// <param name="timeoutFromSec">Maximum seconds to wait. Defaults to 600.</param>
        protected void WaitForDeepResearchResultsLoaded(AiDeepResearchPage deepResearchPage, int timeoutFromSec = 600)
            => deepResearchPage.ResultComponent.SingleColumnComponent.WaitForResultsLoaded(timeoutFromSec);
    }
}
