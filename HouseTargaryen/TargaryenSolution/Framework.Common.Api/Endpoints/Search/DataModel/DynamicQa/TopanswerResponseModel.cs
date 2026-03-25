namespace Framework.Common.Api.Endpoints.Search.DataModel.DynamicQa
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// The topanswer response model.
    /// </summary>
    public class TopanswerResponseModel
    {
        /// <summary>
        /// Gets or sets the question and answers.
        /// </summary>
        [JsonProperty("questionAndAnswers")]
        public List<QuestionAndAnswer> QuestionAndAnswers { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is static qa.
        /// </summary>
        [JsonProperty("isStaticQA")]
        public bool IsStaticQA { get; set; }

        /// <summary>
        /// Gets or sets the query.
        /// </summary>
        [JsonProperty("query")]
        public string Query { get; set; }

        /// <summary>
        /// Gets or sets the jurisdiction.
        /// </summary>
        [JsonProperty("jurisdiction")]
        public string Jurisdiction { get; set; }
    }
}
