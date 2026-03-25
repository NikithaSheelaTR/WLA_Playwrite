namespace Framework.Core.DataModel.Configuration.Collections
{
    using System.Configuration;

    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;
    using Framework.Core.DataModel.Configuration.Sections;

    /// <summary>
    /// A collection of Cobalt product entities in a repository.
    /// </summary>
    internal sealed class ProductElementCollection
        : EntityElementCollection<CobaltProductId, CobaltProductType, CobaltProductInfo>
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new CobaltProductElement();
        }
    }
}