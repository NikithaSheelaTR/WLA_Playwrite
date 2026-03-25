namespace Framework.Core.DataModel.Configuration.Constants
{
    /// <summary>
    /// Represents a domain type of a Cobalt Module.
    /// </summary>
    public enum CobaltModuleType : byte
    {
        /// <summary>
        /// A stub type of an entity which is not a module or whose module identity is not significant.
        /// </summary>
        Undefined,

        /// <summary>
        /// The Cobalt Services module.
        /// </summary>
        CobaltServices,

        /// <summary>
        /// The Legal Services module.
        /// </summary>
        LegalServices,

        /// <summary>
        /// The shared, common, or core Cobalt module.
        /// </summary>
        CobaltModules
    }
}