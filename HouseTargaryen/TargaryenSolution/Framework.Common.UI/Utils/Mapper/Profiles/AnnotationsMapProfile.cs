namespace Framework.Common.UI.Utils.Mapper.Profiles
{
    using AutoMapper;
    
    using Framework.Common.UI.Raw.WestlawEdge.Items.PreviousInteractions;
    using Framework.Common.UI.Raw.WestlawEdge.Models.PreviousInteractions;

    /// <inheritdoc />
    /// <summary>
    /// Annotations Map Profile
    /// </summary>
    public class AnnotationsMapProfile : Profile
    {
        /// <summary>
        /// Initializes new instance of the <see cref="AnnotationsMapProfile"/> class
        /// </summary>
        public AnnotationsMapProfile()
        {
            // AnnotationsItem mapping configuration to AnnotatedItem
            this.CreateMap<AnnotatedItem, AnnotatedModel>();
        }
    }
}