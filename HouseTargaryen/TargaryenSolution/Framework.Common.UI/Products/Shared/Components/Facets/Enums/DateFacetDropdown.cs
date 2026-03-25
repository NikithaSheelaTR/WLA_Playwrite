namespace Framework.Common.UI.Products.Shared.Components.Facets.Enums
{
    using TRGR.Quality.QedArsenal.QualityLibrary.Core.Utils.Enums;

    /// <summary>
    /// Options for date facet dropdown
    /// </summary>
    public enum DateFacetDropdown
    {
        /// <summary>
        /// The last six months.
        /// </summary>
        [StringValue("Last 6 Months")]
        LastSixMonths,

        /// <summary>
        /// The last twelve months.
        /// </summary>
        [StringValue("Last 12 Months")]
        LastTwelveMonths,

        /// <summary>
        /// The last three years.
        /// </summary>
        [StringValue("Last 3 Years")]
        LastThreeYears,

        /// <summary>
        /// The all.
        /// </summary>
        [StringValue("All")]
        All,

        /// <summary>
        /// The all dates before.
        /// </summary>
        [StringValue("All Dates Before")]
        AllDatesBefore,

        /// <summary>
        /// The all dates after.
        /// </summary>
        [StringValue("All Dates After")]
        AllDatesAfter,

        /// <summary>
        /// The specific date.
        /// </summary>
        [StringValue("Specific Date")]
        SpecificDate,

        /// <summary>
        /// The date range.
        /// </summary>
        [StringValue("Date Range")]
        DateRange,
    }
}