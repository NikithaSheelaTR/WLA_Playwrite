namespace WestlawAdvantage.Tests
{
    using System;
    using System.IO;
    using System.Linq;
    using Framework.Common.UI.Products.Shared.Dialogs.Delivery;
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Products.Shared.Enums.Delivery;
    using Framework.Common.UI.Products.Shared.Enums.RI;
    using Framework.Common.UI.Products.Shared.Managers;
    using Framework.Common.UI.Products.TaxnetPro.Pages;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.NarrowPanel;
    using Framework.Common.UI.Products.WestlawEdge.Enums.NarrowPanel;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages.AALP;
    using Framework.Common.UI.Products.WestLawNext.Enums.Delivery;
    using Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo;
    using Framework.Common.UI.Products.WestLawNextMobile.Pages.RelatedInformation.CitingReferences;
    using Framework.Common.UI.Raw.EnhancementTests.Utils;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Raw.WestlawEdge.Pages.RelatedInfo;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Execution;
    using Framework.Core.Utils.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using WestlawPrecision.Tests.Aalp;

    [TestClass]
    public class RelevancyExplainer : AalpBaseTest
    {
        private const string FeatureTestCategory = "RelevancyExplainer";

        string checkRelevancyExplainerIsDisplayed = "Verify relevancy explainer button is displayed";

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        public void RelevancyExplainerCommonTest()
        {
            const string SearchQuery = "motor";

            var resultPage = this.GetHomePage<EdgeCommonSearchResultPage>().Header.EnterSearchQueryAndClickSearch<EdgeCommonSearchResultPage>(SearchQuery);

            SafeMethodExecutor.WaitUntil(() => resultPage.GetAllLinks().Count > 0);

            this.TestCaseVerify.IsTrue(
                checkRelevancyExplainerIsDisplayed,
                resultPage.RelevancyExplainerButtons().Count>0,
                "Relevancy explainer button is not displayed");

            resultPage.RelevancyExplainerButtons().First().Click<EdgeCommonSearchResultPage>();

            SafeMethodExecutor.WaitUntil(() => resultPage.RelevancyExplainerSummary.Displayed);

            this.TestCaseVerify.IsTrue(
                checkRelevancyExplainerIsDisplayed,
                resultPage.RelevancyExplainerSummary.Displayed,
                "Relevancy explainer summary is not displayed");
        }
    }
}
