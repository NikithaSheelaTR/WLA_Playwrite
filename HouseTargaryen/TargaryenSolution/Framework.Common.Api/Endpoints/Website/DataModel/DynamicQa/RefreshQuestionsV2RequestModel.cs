namespace Framework.Common.Api.Endpoints.Website.DataModel.DynamicQa
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// The refresh questions v 2 request model.
    /// </summary>
    [DataContract]
    public class RefreshQuestionsV2RequestModel
    {
        /// <summary>
        /// Questions
        /// </summary>
        [DataMember(Name = "questions")]
        public List<RefreshQuestionRequest> Questions { get; set; }
    }
}
