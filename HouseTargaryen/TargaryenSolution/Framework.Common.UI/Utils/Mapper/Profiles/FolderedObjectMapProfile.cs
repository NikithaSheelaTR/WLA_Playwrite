namespace Framework.Common.UI.Utils.Mapper.Profiles
{
    using AutoMapper;

    using Framework.Common.UI.Raw.WestlawEdge.Items.FolderedObject;
    using Framework.Common.UI.Raw.WestlawEdge.Items.PreviousInteractions;
    using Framework.Common.UI.Raw.WestlawEdge.Models.PreviousInteractions;

    /// <summary>
    /// 
    /// </summary>
    public class FolderedObjectMapProfile : Profile
    {
        /// <summary>
        ///  Initializes new instance of the <see cref="FolderedObjectMapProfile"/> class
        /// </summary>
        public FolderedObjectMapProfile()
        {
            this.CreateMap<FolderedObjectItem, FolderedObjectModel>();

            this.CreateMap<ViewedObjectItem, ViewedObjectModel>();
        }
    }
}
