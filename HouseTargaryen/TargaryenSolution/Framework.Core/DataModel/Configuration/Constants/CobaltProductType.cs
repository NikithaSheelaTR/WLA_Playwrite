namespace Framework.Core.DataModel.Configuration.Constants
{
    /// <summary>
    /// Represents a type of a Cobalt product.
    /// </summary>
    public enum CobaltProductType : byte
    {
        /// <summary>
        /// A stub type of an entity which is not a product or whose identity is not significant.
        /// </summary>
        Undefined,

        /// <summary>
        /// Describes endpoints for Cobalt Tools products.
        /// </summary>
        CobaltTools,

        /// <summary>
        /// Describes endpoints for the WestlawNext product and its product views.
        /// </summary>
        WestlawNext,

        /// <summary>
        /// Describes endpoints for the Cobalt products other than WestlawNext.
        /// </summary>
        LegalSolutions
    }
}