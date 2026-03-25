namespace WestlawAdvantage.Tests.DeepResearch
{
    using Framework.Common.Api.Endpoints.CARI;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RestSharp;

    /// <summary>
    /// Deep Research CARI endpoints test
    /// </summary>
    [TestClass]
    public class WlaDeepResearchCariEndpointTests : CariBaseApiTest
    {
        private const string DeepResearchFlowTestCategory = "WestlawAdvantageFullRegression";

        /// <summary>
        /// Deep Research CARI endpoints test
        /// </summary>
        [TestMethod]
        //[TestCategory(DeepResearchFlowTestCategory)]
        public void CreateFlowAndGetFlowAreSuccessfulTest()
        {
            string checkGetFlowIsSuccessful = "Verify: Get flow call returns 200 (OK)";

            var cariClient = this.GetApiClient<CariClient>();

            var flowId = cariClient.CreateFlow().FlowId;

            IRestResponse getResponse = cariClient.GetFlow(flowId);

            this.TestCaseVerify.IsTrue(
                checkGetFlowIsSuccessful,
                ((int)getResponse.StatusCode).Equals(200),
                $"Get flow call failed. StatusCode: {(int)getResponse.StatusCode}");
        }
    }
}
