namespace Framework.Common.UI.Utils.Mapper.Profiles
{
    using AutoMapper;

    using Framework.Common.UI.Raw.WestlawEdge.Items.Facets;
    using Framework.Common.UI.Raw.WestlawEdge.Models.Facets;

    /// <summary>
    /// The facet options map profile.
    /// </summary>
    public class FacetOptionsMapProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FacetOptionsMapProfile"/> class.
        /// </summary>
        public FacetOptionsMapProfile()
        {
            // FacetOptionItem mapping configuration to FacetOptionModel
            this.CreateMap<FacetOptionItem, FacetOptionModel>();
        }
    }
}