namespace Framework.Common.UI.Utils.Mapper.Profiles
{
    using AutoMapper;

    using Framework.Common.UI.Raw.WestlawEdge.Items.NotesOfDecisions;
    using Framework.Common.UI.Raw.WestlawEdge.Models.NotesOfDecisions;

    /// <summary>
    /// The notes of decisions profile.
    /// </summary>
    public class NotesOfDecisionsProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotesOfDecisionsProfile"/> class.
        /// </summary>
        public NotesOfDecisionsProfile()
        {
            // NotesOfDecisionsItem mapping configuration to NotesOfDecisionsItenModel
            this.CreateMap<NotesOfDecisionsItem, NotesOfDecisionsItemModel>();

            // add your mapping configurations here
        }
    }
}
