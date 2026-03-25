namespace Framework.Core.DataModel.Configuration.Collections
{
    using System.Configuration;

    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;
    using Framework.Core.DataModel.Configuration.Sections;

    /// <summary>
    /// A collection of environment entities in a repository.
    /// </summary>
    internal sealed class EnvironmentElementCollection
        : EntityElementCollection<EnvironmentId, EnvironmentType, EnvironmentInfo>
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new EnvironmentElement();
        }
    }
}