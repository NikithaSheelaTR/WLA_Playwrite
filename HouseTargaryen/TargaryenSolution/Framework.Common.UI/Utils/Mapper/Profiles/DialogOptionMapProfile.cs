namespace Framework.Common.UI.Utils.Mapper.Profiles
{
    using AutoMapper;

    using Framework.Common.UI.Products.Shared.Items.Facets;
    using Framework.Common.UI.Products.Shared.Models;

    /// <summary>
    /// Dialog Option Map Profile
    /// </summary>
    class DialogOptionMapProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DialogOptionMapProfile"/> class.
        /// </summary>
        public DialogOptionMapProfile()
        {
            // DialogOptionItem mapping configuration to DialogOptionItemModel
            this.CreateMap<DialogOptionItem, DialogOptionItemModel>();
        }
    }
}