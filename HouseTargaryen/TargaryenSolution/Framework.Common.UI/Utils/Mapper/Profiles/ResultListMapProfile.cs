namespace Framework.Common.UI.Utils.Mapper.Profiles
{
    using AutoMapper;

    using Framework.Common.UI.Raw.WestlawEdge.Items.DocumentListItems;
    using Framework.Common.UI.Raw.WestlawEdge.Models;
    using Framework.Common.UI.Raw.WestlawEdge.Models.FocusHighlighting;

    /// <summary>
    /// The results list items maps configuration profiles
    /// </summary>
    public class ResultListMapProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResultListMapProfile"/> class. 
        /// The constructor
        /// </summary>
        public ResultListMapProfile()
        {
            // IndigoCasesResultListItem mapping configuration to EdgeCasesResultListItemModel
            this.CreateMap<EdgeResultListItem, EdgeCasesResultListItemModel>();

            // BaseFolderAnalysisResultListItem mapping configuration to BaseFolderAnalysisResultListItemModel
            this.CreateMap<EdgeResultListItem, BaseFolderAnalysisResultListItemModel>();

            // BaseFolderAnalysisResultListItem mapping configuration to BaseEdgeResultListItemModel
            this.CreateMap<EdgeResultListItem, BaseEdgeResultListItemModel>();

            // EdgeResultListItem mapping configuration to EdgeResultListFocusHighlightingItemModel
            this.CreateMap<EdgeResultListItem, EdgeResultListFocusHighlightingItemModel>();

            // add your mapping configurations here
        }
    }
}