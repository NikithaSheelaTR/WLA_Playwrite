namespace Framework.Core.DataModel.Configuration.Proxies
{
    using Framework.Core.DataModel.Configuration.Constants;

    /// <summary>
    /// Represents a product.
    /// </summary>
    public sealed class CobaltProductInfo : CobaltEntityInfo<CobaltProductId, CobaltProductType>
    {
        /// <summary>
        /// Gets the alias of a Cobalt product.
        /// </summary>
        public string InternalName { get; internal set; }

        /// <summary>
        /// A string representation of the current object.
        /// </summary>
        /// <returns>Returns a string representation of the current object.</returns>
        public override string ToString() => string.Format("{0}, InternalName={1}", base.ToString(), this.InternalName);
    }
}