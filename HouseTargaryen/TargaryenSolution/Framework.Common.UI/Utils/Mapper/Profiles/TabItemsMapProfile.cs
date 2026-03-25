namespace Framework.Common.UI.Utils.Mapper.Profiles
{
    using AutoMapper;

    using Framework.Common.UI.Raw.WestlawEdge.Items.RelatedDocument;
    using Framework.Common.UI.Raw.WestlawEdge.Models.RelatedDocuments;

    /// <inheritdoc />
    /// <summary>
    /// The tab items.
    /// </summary>
    public class TabItemsMapProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TabItemsMapProfile"/> class. 
        /// </summary>
        public TabItemsMapProfile()
        {
            // SecondarySourcesItem mapping configuration to RelatedDocumentsBaseItemModel
            this.CreateMap<SecondarySourcesItem, RelatedDocumentsBaseItemModel>();
        }
    }
}