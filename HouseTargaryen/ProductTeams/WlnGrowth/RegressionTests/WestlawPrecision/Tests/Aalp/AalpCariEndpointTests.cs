namespace WestlawPrecision.Tests.Aalp
{
    using Framework.Core.Utils;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RestSharp;
    using System;

    /// <summary>
    /// Aalp CARI Endpoint Tests
    /// </summary>
    [TestClass]
    public class AalpCariEndpointTests : AalpBaseTest
    {
        private const string AalpCariEndpointTestCategory = "AalpCariEndpointTest";
        private const string AuthHeaderKey = "Authorization";
        private const string AuthHeaderValue = "Bearer 0525241137260705oVsVREdPutgzI2vUHtbGgkE21TvT66dZ3aHGOOFPRVnWX5ajoS_y_peMw44VIhGDz2RejTs7cIyiR0PfPt7QRy5_P1qYwNqTVpq3y_5SpO48TjAFnNvqHkfQVNAjtjwdDuG5XcsVeTJS5h52qO-XRVJRFK7DVHMEXaWL3wz_naX07Tw6qTn0QwyT4lK5rjEkejEA7bWIP6-_QJ5C905q78Q7CZ6zZ9gat6kZ5qsitCs-56TGYahx846c5aKbNwvpw2zgvFU7z2wkoMQccEa6KW_K1q9ebKLKCWeTG9k7aSYbRzl2qmKzy2AWZP8eNY6kAFMGQp49ddamcu8alNA";

        /// <summary>
        /// **** CARI endpoint tests have been moved to run on Postman ****
        /// Verify CARI API calls for initiating a conversation
        /// Test cases: 1900020 1900827 User Story: 1886862
        /// 1. Send POST request to initiate a conversation with short query "What?"
        /// 2. Check: Response contains error code 400 and error description ERR_QUERY_TOO_SHORT
        /// 3. *Send POST request to initiate a conversation with invalid jurisdiction "Minnesoda"
        /// 4. Check: Response contains error code 400 and error description ERR_INVALID_JURISDICTION
        /// 5. Send POST request to initiate a conversation with valid query and jurisdiction
        /// 6. Check: Response returns with sucessful status code
        /// 7. Check: Response contains conversationId and conversationEntryId
        /// 8. *ERR_INVALID_JURISDICTION not implemented yet
        /// </summary>
        [TestMethod]
        [TestCategory(AalpCariEndpointTestCategory)]
        public void InitiateConversationPostResponseTest()
        {
            // CARI endpoint tests have been moved to run on Postman
            const string normalRequest = "{\"jurisdictions\": [\"ALLCASES\"],\"query\": \"What is fraud?\"}";
            const string shortQueryRequest = "{\"jurisdictions\": [\"ALLCASES\"],\"query\": \"What?\"}";
            const string shortQueryError = "ERR_QUERY_TOO_SHORT";
            //const string invalidJurisRequest = "{\"jurisdictions\": [\"California\",\"Minnesoda\"],\"query\": \"What is fraud?\"}";
            //const string invalidJurisError = "ERR_INVALID_JURISDICTION";

            var client = GetClient("InitiateConversation");
            var request = new RestRequest("", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader(AuthHeaderKey, AuthHeaderValue);
            request.AddParameter("application/json", shortQueryRequest, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            Logger.LogInfo($"**shortQueryRequest Status: {(int)response.StatusCode} Response: {response.Content}");
            CariResponse cariResponse = ObjectSerializer.DeserializeJsonToObject<CariResponse>(response.Content);

            this.TestCaseVerify.IsTrue(
                $"Cari initial conversation short query returns {shortQueryError} (400)",
                (int)response.StatusCode > 201 && Int32.Parse(cariResponse.errorCode) == 400 && cariResponse.errorDescription.Contains(shortQueryError),
                $"Cari initial conversation short query failed. StatusCode: {(int)response.StatusCode}");

            // Not implemented yet
            //client = GetClient("InitiateConversation");
            //request = new RestRequest("", Method.POST);
            //request.RequestFormat = DataFormat.Json;
            //request.AddHeader(AuthHeaderKey, AuthHeaderValue);
            //request.AddParameter("application/json", invalidJurisRequest, ParameterType.RequestBody);

            //response = client.Execute(request);
            //cariResponse = ObjectSerializer.DeserializeJsonToObject<CariResponse>(response.Content);
            //Logger.LogInfo($"**invalidJurisRequest Status: {(int)response.StatusCode} Response: {response.Content}");

            //this.TestCaseVerify.IsTrue(
            //    $"Cari initial conversation invalid jurisdiction returns {invalidJurisError} (400)",
            //    (int)response.StatusCode > 201 && Int32.Parse(cariResponse.errorCode) == 400 && cariResponse.errorDescription.Contains(invalidJurisError),
            //    $"Cari initial conversation invalid jurisdiction failed. StatusCode: {(int)response.StatusCode}");

            client = GetClient("InitiateConversation");
            request = new RestRequest("", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader(AuthHeaderKey, AuthHeaderValue);
            request.AddParameter("application/json", normalRequest, ParameterType.RequestBody);

            response = client.Execute(request);
            cariResponse = ObjectSerializer.DeserializeJsonToObject<CariResponse>(response.Content);
            Logger.LogInfo($"Status: {(int)response.StatusCode} Response: {response.Content}");
            Logger.LogInfo($"conversationId: {cariResponse.conversationId} conversationEntryId: {cariResponse.conversationEntryId}");

            this.TestCaseVerify.IsTrue(
                "Cari initial conversation post request is successful",
                (int)response.StatusCode <= 201,
                $"Cari initial conversation post request failed. StatusCode: {(int)response.StatusCode}");

            this.TestCaseVerify.IsTrue(
                "Cari initial conversation response contains conversationId and conversationEntryId",
                response.Content.Contains("conversationId") && response.Content.Contains("conversationEntryId"),
                $"Cari initial conversation response does not contain conversationId and conversationEntryId. StatusCode: {(int)response.StatusCode}");
        }

        private RestClient GetClient(string apiCall = "InitiateConversation,ContinueConversatiion")
        {
            const string initiateConversationUrlCi = "https://ai-assisted-legal-research-ci.plexus-cari-ppuse1.97328.aws-int.thomsonreuters.com/skill/ai-assisted-legal-research/v1/conversation";
            const string initiateConversationUrlInt = "https://ai-assisted-legal-research-int.plexus-cari-ppuse1.97328.aws-int.thomsonreuters.com/skill/ai-assisted-legal-research/v1/conversation";
            const string initiateConversationUrlQa = "https://ai-assisted-legal-research-qa.plexus-cari-ppuse1.97328.aws-int.thomsonreuters.com/skill/ai-assisted-legal-research/v1/conversation";
            const string initiateConversationUrlProd = "https://ai-assisted-legal-research-prod.plexus-cari-ppuse1.97328.aws-int.thomsonreuters.com/skill/ai-assisted-legal-research/v1/conversation";

            const string continueConversationUrl = "https://URL4ContinuingConversation";

            string environment = this.TestExecutionContext.TestEnvironment.Id.ToString().ToLower();//CI-CI;INT-DEMO;QA-QED;PROD-PROD
            string url = initiateConversationUrlProd;

            switch (apiCall)
            {
                case "InitiateConversation":
                    if (environment.Contains("ci"))
                        url = initiateConversationUrlCi;
                    else if (environment.Contains("demo"))
                        url = initiateConversationUrlInt;
                    else if (environment.Contains("qed"))
                        url = initiateConversationUrlQa;
                    break;
                case "ContinueConversatiion":
                    url = continueConversationUrl;
                    break;
                default:
                    url = "Invalid API call value";
                    break;
            }

            return new RestClient(url);
        }
    }

    public class CariResponse
    {
        public string conversationId { get; set; }
        public string conversationEntryId { get; set; }
        public string errorCode { get; set; }
        public string errorDescription { get; set; }
    }
}