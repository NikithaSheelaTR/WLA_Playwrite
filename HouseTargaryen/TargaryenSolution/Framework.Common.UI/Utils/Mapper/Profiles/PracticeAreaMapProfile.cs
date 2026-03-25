namespace Framework.Common.UI.Utils.Mapper.Profiles
{
    using AutoMapper;

    using Framework.Common.UI.Products.WestLawAnalytics.Items;
    using Framework.Common.UI.Products.WestLawAnalytics.Models.BusinessObjects;

    /// <summary>
    /// Practice Area Map Profile
    /// </summary>
    public class PracticeAreaMapProfile : Profile
    {
        /// <summary>
        /// Practice Area Map Profile Constructor
        /// </summary>
        public PracticeAreaMapProfile()
        {
            this.CreateMap<PracticeAreaGridItem, PracticeAreaModel>();
        }
    }
}
