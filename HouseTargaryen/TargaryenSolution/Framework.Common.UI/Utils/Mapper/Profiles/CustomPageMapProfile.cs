namespace Framework.Common.UI.Utils.Mapper.Profiles
{
    using AutoMapper;

    using Framework.Common.UI.Products.Shared.Items.CustomPageAdmin;
    using Framework.Common.UI.Products.Shared.Items.EnhancedCustomPageSharing;
    using Framework.Common.UI.Products.Shared.Models.CustomPages;
    using Framework.Common.UI.Products.Shared.Models.CustomPages.EnhancedCustomPageSharing;

    /// <summary>
    /// Custom Page Map Profile
    /// </summary>
    public class CustomPageMapProfile : Profile
    {
        /// <summary>
        /// Initializes new instance of the <see cref="CustomPageMapProfile"/> class
        /// </summary>
        public CustomPageMapProfile()
        {
            // CustomPageAdminContactItem mapping configuration to CustomPageAdminContactModel
            this.CreateMap<CustomPageAdminContactItem, CustomPageAdminContactModel>();

            // CustomPageContactItem mapping configuration to CustomPageContactModel
            this.CreateMap<CustomPageContactItem, CustomPageContactModel>();

            // CustomPageItem mapping configuration to CustomPageModel
            this.CreateMap<CustomPageItem, CustomPageModel>();
        }
    }
}
