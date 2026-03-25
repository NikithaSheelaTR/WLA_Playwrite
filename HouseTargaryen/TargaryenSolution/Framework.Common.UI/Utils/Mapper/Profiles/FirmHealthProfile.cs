namespace Framework.Common.UI.Utils.Mapper.Profiles
{
    using AutoMapper;

    using Framework.Common.UI.Products.WestLawAnalytics.Items.FirmHealthItems;
    using Framework.Common.UI.Products.WestLawAnalytics.Models.BusinessObjects;

    class FirmHealthProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FirmHealthProfile"/> class.
        /// </summary>
        public FirmHealthProfile()
        {
            // FirmHealthItem mapping configuration to FirmHealthModel
            this.CreateMap<FirmHealthItem, FirmHealthModel>();
        }
    }
}