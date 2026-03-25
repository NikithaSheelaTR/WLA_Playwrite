namespace Framework.Common.UI.Utils.Mapper.Profiles
{
    using AutoMapper;

    using Framework.Common.UI.Products.Shared.Items.FolderHistory;
    using Framework.Common.UI.Products.Shared.Models.GridModels;
    using Framework.Common.UI.Products.WestLawAnalytics.Items.BillingResultGridItems;
    using Framework.Common.UI.Products.WestLawAnalytics.Models.BusinessObjects;

    /// <summary>
    /// Grid Row Profile
    /// </summary>
    class GridRowProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GridRowProfile"/> class.
        /// </summary>
        public GridRowProfile()
        {
            // GridRowItem mapping configuration to GridRowModel
            this.CreateMap<GridRowItem, GridRowModel>();

            // GridRowDetailItem mapping configuration to GridRowDetailItemModel
            this.CreateMap<GridRowDetailItem, GridRowDetailItemModel>();

            // FolderGridItem mapping configuration to FolderGridModel
            this.CreateMap<FolderGridItem, FolderGridModel>();
        }
    }
}