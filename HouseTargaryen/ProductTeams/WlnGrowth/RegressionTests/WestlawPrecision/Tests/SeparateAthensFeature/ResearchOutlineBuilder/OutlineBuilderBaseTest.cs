namespace WestlawPrecision.Tests.SeparateAthensFeature.ResearchOutlineBuilder
{
    using System.IO;

    using Framework.Common.UI.Products.WestLawNext.Utils;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Tests;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.CommonTypes.Constants;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Security;
    using Framework.Core.DataModel.Security.Proxies;
    using Framework.Core.DataModel.Security.Specialized;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using System.Linq;

    using Framework.Core.Utils.Extensions;
    using System.Threading;

    public class OutlineBuilderBaseTest : BaseWebUiTest
    {
        protected const string CurrentTestCategory = "AthensDahlUi";
        protected const string FeatureTestCategory = "OutlineBuilder";

        protected const string FolderToSave = @"C:\Temp\ExternalEnhancementTest";

        public OutlineBuilderBaseTest()
        {
            this.DefaultCobaltProduct =
                TestConfigurationRepository.DefaultInstance.FindProduct(CobaltProductId.WestlawEdge);
            this.Settings.Append(
                EnvironmentConstants.PasswordPoolName,
                "IndigoPremium",
                SettingUpdateOption.Overwrite);
        }

        protected static void DeleteFilesInFolder(string folderName)
        {
            var directory = new DirectoryInfo(folderName);
            if (directory.Exists)
            {
                foreach (FileInfo file in directory.GetFiles()) file.Delete();
            }
        }

        protected override ChromeOptions GetChromeOptions(string pathToBrowserExecutable)
        {
            ChromeOptions browserOptions = base.GetChromeOptions(pathToBrowserExecutable);
            browserOptions.AddUserProfilePreference("download.default_directory", FolderToSave);
            return browserOptions;
        }

        protected override void OnManageCredential()
        {

            var userCredential = new UserDbCredential(
                    this.TestContext,
                    PasswordVertical.WlnGrowth,
                    this.GetPasswordPool())
            {
                ClientId = "ROBtest"
            };

            CredentialPool.RegisterUser(userCredential);
            this.DefaultSignOnContext = new WlnSignOnContext<IUserInfo>
                (this.TestExecutionContext, userCredential.ToWlnUserInfo());
        }

        /// <summary>
        /// Initialize routing page settings
        /// </summary>
        protected override void InitializeRoutingPageSettings()
        {
            base.InitializeRoutingPageSettings();

            this.Settings.AppendValues(
                EnvironmentConstants.InfrastructureAccessControlsOff,
                SettingUpdateOption.Append,
                "IAC-QUICKCHECK-SUBMIT-HIGHLIGHTING",
                "IAC-A11Y-DROPDOWN-DELIVERY-SEARCH");
        }

        protected override void PerformUiPostconditionRoutines()
        {
            var document = new EdgeCommonDocumentPage();

            if (this.TestContext.CurrentTestOutcome == UnitTestOutcome.Failed && !BrowserPool.CurrentBrowser.Driver.ToString().Contains("null"))
            {
                this.ScreenshotTaker.TakeScreenshotRp();
            }

            if (document.RightPanel.OutlineBuilderPanel.OutlineBuilderFullPagePanel.IsDisplayed())
            {
                document = document.RightPanel.OutlineBuilderPanel.OutlineBuilderFullPagePanel
                    .ReturnToDocumentButton.Click<EdgeCommonDocumentPage>();
            }

            if (!document.RightPanel.Toggle.State)
            {
                document = document.RightPanel.Toggle.ToggleState<EdgeCommonDocumentPage>(true);
            }

            if (document.RightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel.BackToListOfOutlinesButton.Displayed)
            {
                document = document.RightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel
                    .BackToListOfOutlinesButton.Click<EdgeCommonDocumentPage>();
            }

            while (document.RightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel.ListOfOutlines.Count > 0)
            {
                document = document.RightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel.ListOfOutlines.First()
                    .DeleteOutline<EdgeCommonDocumentPage>();
            }
            document.RightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel.Toggle.ToggleState<EdgeCommonDocumentPage>(false);
        }

        public EdgeCommonDocumentPage CreateNewOutline(EdgeCommonDocumentPage document, string title, bool deleteExistingOutlines = true)
        {
            if (document.RightPanel.OutlineBuilderPanel.OutlineBuilderFullPagePanel.IsDisplayed())
            {
                document = document.RightPanel.OutlineBuilderPanel.OutlineBuilderFullPagePanel
                    .CreateNewOutlineButton.Click<EdgeCommonDocumentPage>();
                Thread.Sleep(2000);
                return EditOutline(document, title);
            }
            else
            {
                if(document.RightPanel.Toggle.State == false)
                    document.RightPanel.OutlineBuilderPanel.OutlinesPanelButton.Click<EdgeCommonDocumentPage>();

                if (deleteExistingOutlines)
                {
                    while (document.RightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel.ListOfOutlines.Count > 0)
                    {
                        document = document.RightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel.ListOfOutlines.First()
                            .DeleteOutline<EdgeCommonDocumentPage>();
                    }
                }

                document = document.RightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel
                    .CreateNewOutlineButton.Click<EdgeCommonDocumentPage>();
                Thread.Sleep(2000);
                return EditOutline(document, title);
            }
        }

        public EdgeCommonDocumentPage EditOutline(EdgeCommonDocumentPage document, string title)
        {
            if (document.RightPanel.OutlineBuilderPanel.OutlineBuilderFullPagePanel.IsDisplayed())
            {
                return document.RightPanel.OutlineBuilderPanel.OutlineBuilderFullPagePanel
                    .EditOutlineTitleButton.Click<EdgeCommonDocumentPage>().RightPanel.OutlineBuilderPanel
                    .OutlineBuilderFullPagePanel.CurrentOutlineTextbox.SetText<EdgeCommonDocumentPage>(title + Keys.Enter);
            }
            else
            {
                return document.RightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel
                    .EditOutlineTitleButton.Click<EdgeCommonDocumentPage>().RightPanel.OutlineBuilderPanel
                    .OutlineBuilderRightPanel.CurrentOutlineTextbox.SetText<EdgeCommonDocumentPage>(title + Keys.Enter);
            }
        }

        public EdgeCommonDocumentPage CreateHeadingInOutline(EdgeCommonDocumentPage document,
                                                             string headingLevel, string headingString)
        {
            if (document.RightPanel.OutlineBuilderPanel.OutlineInternalFullPagePanel.IsDisplayed())
            {
                document = document.RightPanel.OutlineBuilderPanel.OutlineInternalFullPagePanel
                    .AddHeadingOrNoteButton.Click<EdgeCommonDocumentPage>().RightPanel.OutlineBuilderPanel
                    .OutlineInternalFullPagePanel.HeadingButton.First(item => item.HeadingLevel.Equals(headingLevel))
                    .Click<EdgeCommonDocumentPage>();
                return EditHeading(document, headingString);
            }
            else
            {
                document = document.RightPanel.OutlineBuilderPanel.OutlineInternalRightPanel
                    .AddHeadingOrNoteButton.Click<EdgeCommonDocumentPage>().RightPanel.OutlineBuilderPanel
                    .OutlineInternalRightPanel.HeadingButton.First(item => item.HeadingLevel.Equals(headingLevel))
                    .Click<EdgeCommonDocumentPage>();
                return EditHeading(document, headingString);
            }
        }

        public EdgeCommonDocumentPage EditHeading(EdgeCommonDocumentPage document, string headingString)
        {
            if (document.RightPanel.OutlineBuilderPanel.OutlineInternalFullPagePanel.IsDisplayed())
            {
                return document.RightPanel.OutlineBuilderPanel.OutlineInternalFullPagePanel
                    .AddHeadingTextbox.SetText<EdgeCommonDocumentPage>(headingString + Keys.Enter);
            }
            else
            {
                return document.RightPanel.OutlineBuilderPanel.OutlineInternalRightPanel
                    .AddHeadingTextbox.SetText<EdgeCommonDocumentPage>(headingString + Keys.Enter);
            }
        }

        public EdgeCommonDocumentPage CreateNoteInOutline(EdgeCommonDocumentPage document, string noteString)
        {
            if (document.RightPanel.OutlineBuilderPanel.OutlineInternalFullPagePanel.IsDisplayed())
            {
                return document.RightPanel.OutlineBuilderPanel.OutlineInternalFullPagePanel.NoteButton.Click<EdgeCommonDocumentPage>()
               .RightPanel.OutlineBuilderPanel.OutlineInternalFullPagePanel.AddHeadingTextbox.SetText<EdgeCommonDocumentPage>(noteString)
               .RightPanel.OutlineBuilderPanel.OutlineInternalFullPagePanel.SaveHeadingButton.Click<EdgeCommonDocumentPage>();
            }
            else
            {
                return document.RightPanel.OutlineBuilderPanel.OutlineInternalRightPanel.NoteButton.Click<EdgeCommonDocumentPage>()
               .RightPanel.OutlineBuilderPanel.OutlineInternalRightPanel.AddHeadingTextbox.SetText<EdgeCommonDocumentPage>(noteString)
               .RightPanel.OutlineBuilderPanel.OutlineInternalRightPanel.SaveHeadingButton.Click<EdgeCommonDocumentPage>();
            }
        }
    }
}

