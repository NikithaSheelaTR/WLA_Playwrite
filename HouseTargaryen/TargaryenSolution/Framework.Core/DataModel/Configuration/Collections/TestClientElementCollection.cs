namespace Framework.Core.DataModel.Configuration.Collections
{
    using System.Configuration;

    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;
    using Framework.Core.DataModel.Configuration.Sections;

    /// <summary>
    /// A collection of test client entities in a repository.
    /// </summary>
    internal sealed class TestClientElementCollection
        : EntityElementCollection<TestClientId, TestClientType, TestClientInfo>
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new TestClientElement();
        }
    }
}