namespace Framework.Common.Api.Endpoints.Search.DataModel.DynamicQa
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// The question and answer.
    /// </summary>
    public class QuestionAndAnswer
    {
        /// <summary>
        /// Gets or sets the questions.
        /// </summary>
        [JsonProperty("questions")]
        public List<string> Questions { get; set; }

        /// <summary>
        /// Gets or sets the answers.
        /// </summary>
        [JsonProperty("answers")]
        public List<Answer> Answers { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether has more answers.
        /// </summary>
        [JsonProperty("hasMoreAnswers")]
        public bool HasMoreAnswers { get; set; }
    }
}
