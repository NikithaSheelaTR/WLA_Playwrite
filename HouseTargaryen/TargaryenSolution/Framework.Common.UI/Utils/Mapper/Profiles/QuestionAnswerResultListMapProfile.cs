namespace Framework.Common.UI.Utils.Mapper.Profiles
{
    using AutoMapper;

    using Framework.Common.UI.Raw.WestlawEdge.Items.QuestionAnswerItem;
    using Framework.Common.UI.Raw.WestlawEdge.Models.QuestionAnswer;

    /// <summary>
    /// 
    /// </summary>
    public class QuestionAnswerResultListMapProfile : Profile
    {
        /// <summary>
        /// Initializes new instance os <see cref="QuestionAnswerResultListMapProfile"/> class
        /// </summary>
        public QuestionAnswerResultListMapProfile()
        {
            // EdgeQuestionAnswerResultListItem mapping configuration to EdgeQuestionAnswerResultListItemModel
            this.CreateMap<EdgeQuestionAnswerResultListItem, EdgeQuestionAnswerResultListItemModel>();
        }
    }
}