namespace Framework.Common.UI.Utils.Mapper.Profiles
{
    using AutoMapper;

    using Framework.Common.UI.Raw.WestlawEdge.Items.TrDiscover;
    using Framework.Common.UI.Raw.WestlawEdge.Models.TrDiscover;

    /// <inheritdoc />
    /// <summary>
    /// The typeahead map profile.
    /// </summary>
    public class TypeaheadMapProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeaheadMapProfile"/> class.
        /// </summary>
        public TypeaheadMapProfile()
        {
            // TrdSnapshotsItem mapping configuration to TrdSuggestionModel
            this.CreateMap<TrdSnapshotsItem, TrdSuggestionModel>();

            // TrdWestlawAnswersItem mapping configuration to TrdSuggestionModel
            this.CreateMap<TrdWestlawAnswersItem, TrdSuggestionModel>();

            // TrdBaseCategoryItem mapping configuration to TrdSuggestionModel
            this.CreateMap<TrdBaseCategoryItem, TrdSuggestionModel>();

            // TrdSearchSuggestionItem mapping configuration to TrdSuggestionModel
            this.CreateMap<TrdSearchSuggestionItem, TrdSuggestionModel>();

            // TrdStateAndFederalItem mapping configuration to TrdSuggestionModel
            this.CreateMap<TrdStateAndFederalItem, TrdSuggestionModel>();

            // TrdOtherItem mapping configuration to TrdSuggestionModel
            this.CreateMap<TrdOtherItem, TrdSuggestionModel>();

            // TrdSecondarySourcesItem mapping configuration to TrdSuggestionModel
            this.CreateMap<TrdSecondarySourcesItem, TrdSuggestionModel>();

            // TrdLegislationItem mapping configuration to TrdSuggestionModel
            this.CreateMap<TrdLegislationItem, TrdSuggestionModel>();

            // TrdGovRegItem mapping configuration to TrdSuggestionModel
            this.CreateMap<TrdGovRegItem, TrdSuggestionModel>();
        }
    }
}