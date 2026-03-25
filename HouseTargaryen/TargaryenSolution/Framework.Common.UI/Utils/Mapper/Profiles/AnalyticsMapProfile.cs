namespace Framework.Common.UI.Utils.Mapper.Profiles
{
    using AutoMapper;
    
    using Framework.Common.UI.Products.WestLawAnalytics.Items;
    using Framework.Common.UI.Products.WestLawAnalytics.Models.BusinessObjects;

    /// <summary>
    /// Analytics Map Profile
    /// </summary>
    public class AnalyticsMapProfile : Profile
    {
        /// <summary>
        /// Initializes new instance of the <see cref="AnalyticsMapProfile"/> class
        /// </summary>
        public AnalyticsMapProfile()
        {
            // BillingGroupItem mapping configuration to BillingGroupModel
            this.CreateMap<BillingGroupItem, BillingGroupModel>();
        }
    }
}
