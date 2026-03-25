namespace Framework.Common.Api.Endpoints.Website.DataModel.DynamicQa
{
    using System.Collections.Generic;

    /// <summary>
    /// The refresh question v 2 response model.
    /// </summary>
    public class RefreshQuestionV2ResponseModel
    {
        /// <summary>
        /// Questions 
        /// </summary>
        public List<RefreshQuestionResponse> Questions { get; set; }
    }
}
