namespace Framework.Common.UI.Utils.Mapper.Profiles
{
    using AutoMapper;

    using Framework.Common.UI.Products.WestLawAnalytics.Items;
    using Framework.Common.UI.Products.WestLawAnalytics.Models.BusinessObjects;

    /// <summary>
    /// Upload History Table Profile
    /// </summary>
    public class UploadHistoryTableProfile : Profile
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public UploadHistoryTableProfile()
        {
            this.CreateMap<UploadHistoryTableItem, UploadHistoryTableModel>();
        }
    }
}
