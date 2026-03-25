namespace Framework.Core.DataModel.Configuration.Sections
{
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;

    /// <summary>
    /// Represents a Cobalt module.
    /// </summary>
    internal sealed class CobaltModuleElement : EntityElement<CobaltModuleId, CobaltModuleType, CobaltModuleInfo>
    {
        /// <summary>
        /// Gets a light-weight proxy object for a corresponding configuration element.
        /// </summary>
        public override CobaltModuleInfo ProxyObject => new CobaltModuleInfo { Id = this.Id, Type = this.Type, TagName = this.TagName };
    }
}