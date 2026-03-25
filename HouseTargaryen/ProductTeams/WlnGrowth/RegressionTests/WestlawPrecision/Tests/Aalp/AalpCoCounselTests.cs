using Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP.CoCounsel;
using Framework.Common.UI.Products.WestlawEdgePremium.Pages;
using Framework.Common.UI.Products.WestLawNext.Utils;
using Framework.Core.CommonTypes.Configuration;
using Framework.Core.DataModel.Security.Proxies;
using Framework.Core.DataModel.Security.Specialized;
using Framework.Core.DataModel.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Framework.Common.UI.Products.WestlawEdgePremium.Enums.CoCounsel;
using Framework.Common.UI.Products.Shared.Pages;
using Framework.Common.UI.Utils.Browser;
using Framework.Common.UI.Products.CoCounsel;
using Framework.Core.Utils.Execution;

namespace WestlawPrecision.Tests.Aalp
{
    //Password_Pool ="CoCounsel"
    [TestClass]
    public class AalpCoCounselTests : AalpBaseTest
    {
        private const string CoCounselAJSTestCategory = "CoCounselAJSTestCategory"; 
        [TestMethod]
        [TestCategory(CoCounselAJSTestCategory)]
        public void SearchAIJurisdictionalSurveysAskQuestionTest()
        {
            string checkOpenInWestlawExpandsCoCounselChatAssistant = "Verify: 'Please enter your question' alert is shown, Submit button is disabled";
           
            var homePage = this.GetHomePage<PrecisionHomePage>();

            var coCounselChatAssistantDialog = new CoCounselChatAssistantDialog();

            var signOnPage = coCounselChatAssistantDialog.MenuDropdown.SelectOption<CommonSignOnPage>(TrayOptions.OpenInCoCounsel);

            BrowserPool.CurrentBrowser.CreateTab("CoCounsel Signon");
            BrowserPool.CurrentBrowser.ActivateTab("CoCounsel Signon");

            var userCredential = new UserDbCredential(this.TestContext, PasswordVertical.WlnGrowth, "CoCounsel")
            { ClientId = "Aalp Test" };

            signOnPage.EnterUserNameAndPassword(userCredential.ToWlnUserInfo().Email, userCredential.Password);
            var cocounselhomePage = signOnPage.ClickSignOn<CoCounselHomePage>();

            SafeMethodExecutor.WaitUntil(() => cocounselhomePage.Chat.ChatTextBox.Displayed);
            cocounselhomePage.Chat.ChatTextBox.SendKeys("What is the statutory definition of \"credit union\"? I am  interested in New Hampshire, Wisconsin, Minnesota, Washington DC, Maine, and Georgia.");
            cocounselhomePage.Chat.SendButton.Click();
            SafeMethodExecutor.WaitUntil(() => cocounselhomePage.Chat.ResponceComponent.SubmitButton.Displayed);
            cocounselhomePage.Chat.ResponceComponent.ClearSubmitQuestionTextBox();

            this.TestCaseVerify.IsTrue(
              checkOpenInWestlawExpandsCoCounselChatAssistant,
              cocounselhomePage.Chat.ResponceComponent.InfoMessageLabel.Text.Equals("Please enter your question")
              && cocounselhomePage.Chat.ResponceComponent.SubmitButton.Displayed,
              "'Please enter your question' alert is not shown, Submit button is not disabled");
        }
    }
}
