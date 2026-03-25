namespace Framework.Core.DataModel.Configuration.Collections
{
    using System.Configuration;

    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;
    using Framework.Core.DataModel.Configuration.Sections;

    /// <summary>
    /// A collection of Cobalt module entities in a repository.
    /// </summary>
    internal sealed class ModuleElementCollection
        : EntityElementCollection<CobaltModuleId, CobaltModuleType, CobaltModuleInfo>
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new CobaltModuleElement();
        }
    }
}