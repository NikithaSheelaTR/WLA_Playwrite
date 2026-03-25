namespace Framework.Common.UI.Utils.Mapper.Profiles
{
    using AutoMapper;

    using Framework.Common.UI.Products.WestLawAnalytics.Items;
    using Framework.Common.UI.Products.WestLawAnalytics.Models.BusinessObjects;

    /// <summary>
    /// Location Grid Profile
    /// </summary>
    public class LocationGridMapProfile : Profile
    {
        /// <summary>
        /// Location Grid Map Profile Counstructor
        /// </summary>
        public LocationGridMapProfile()
        {
            this.CreateMap<LocationGridItem, LocationGridModel>();
        }
    }
}
