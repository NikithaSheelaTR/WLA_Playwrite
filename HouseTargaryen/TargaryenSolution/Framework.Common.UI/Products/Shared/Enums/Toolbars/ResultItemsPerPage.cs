using Framework.Core.Utils.Enums;

namespace Framework.Common.UI.Products.Shared.Enums.Toolbars
{
    /// <summary>
    /// The different options for the items per page on the search result page
    /// </summary>
    public enum ResultItemsPerPage
    {
        /// <summary>
        /// Twenty
        /// </summary>
        [StringValue("20 per page")]
        Twenty,

        /// <summary>
        /// Twenty In French
        /// </summary>
        [StringValue("20 par page")]
        TwentyFrench,

        /// <summary>
        /// Fifty
        /// </summary>
        [StringValue("50 per page")]
        Fifty,

        /// <summary>
        /// Fifty In French
        /// </summary>
        [StringValue("50 par page")]
        FiftyFrench,

        /// <summary>
        /// OneHundred
        /// </summary>
        [StringValue("100 per page")]
        OneHundred,

        /// <summary>
        /// OneHundred In French
        /// </summary>
        [StringValue("100 par page")]
        OneHundredFrench,
    }
}