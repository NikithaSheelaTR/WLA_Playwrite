namespace Framework.Core.DataModel.Configuration.Sections
{
    using System.Configuration;

    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;

    /// <summary>
    /// Represents a Cobalt product.
    /// </summary>
    internal sealed class CobaltProductElement : EntityElement<CobaltProductId, CobaltProductType, CobaltProductInfo>
    {
        /// <summary>
        /// Gets the alias of a Cobalt product.
        /// </summary>
        [ConfigurationProperty(ConfigurationConstants.AliasPropertyName, IsRequired = true)]
        public string Alias => (string)this[ConfigurationConstants.AliasPropertyName];

        /// <summary>
        /// Gets a light-weight proxy object for a corresponding configuration element.
        /// </summary>
        public override CobaltProductInfo ProxyObject =>
            new CobaltProductInfo
                {
                    Id = this.Id,
                    Type = this.Type,
                    TagName = this.TagName,
                    InternalName = this.Alias
                };
    }
}