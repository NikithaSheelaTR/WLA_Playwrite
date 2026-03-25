namespace Framework.Core.CommonTypes.Configuration
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;
    using Framework.Core.DataModel.Configuration.Sections;
    using Framework.Core.Utils.Configuration;

    /// <summary>
    /// The class to read the test repository.
    /// </summary>
    public sealed class TestConfigurationRepository
    {
        /// <summary>
        /// The default config file name.
        /// </summary>
        public const string DefaultConfigFileName = "Resources/TestEnvironmentRepository.xml";

        /// <summary>
        /// The FedRamp env config file name.
        /// </summary>
        public const string FedRampConfigFileName = "Resources/TestEnvironmentRepositoryFedRamp.xml";

        private static readonly object LockObject = new object();

        private static volatile TestConfigurationRepository defaultInstance;

        private static volatile TestConfigurationRepository fedRampInstance;

        private readonly IList<EndpointInfo> endpoints;

        private readonly IList<EnvironmentInfo> environments;

        private readonly IList<CobaltModuleInfo> modules;

        private readonly IList<CobaltProductInfo> products;

        private readonly IList<TestClientInfo> testClients;

        private readonly IDictionary<Type, IList> typeCollectionMap;

        /// <summary>
        /// Indicates if Default/FedRamp instance are been used
        /// </summary>
        public static bool IsDefaultInstance = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestConfigurationRepository"/> class.
        /// </summary>
        public TestConfigurationRepository()
        {
            this.environments = new List<EnvironmentInfo>();
            this.endpoints = new List<EndpointInfo>();
            this.modules = new List<CobaltModuleInfo>();
            this.products = new List<CobaltProductInfo>();
            this.testClients = new List<TestClientInfo>();
            this.Environments = new ReadOnlyCollection<EnvironmentInfo>(this.environments);
            this.Endpoints = new ReadOnlyCollection<EndpointInfo>(this.endpoints);
            this.Modules = new ReadOnlyCollection<CobaltModuleInfo>(this.modules);
            this.Products = new ReadOnlyCollection<CobaltProductInfo>(this.products);
            this.TestClients = new ReadOnlyCollection<TestClientInfo>(this.testClients);
            this.typeCollectionMap = new Dictionary<Type, IList>
                                         {
                                             { typeof(EnvironmentInfo), this.Environments },
                                             { typeof(CobaltModuleInfo), this.Modules },
                                             { typeof(CobaltProductInfo), this.Products },
                                             { typeof(TestClientInfo), this.TestClients }
                                         };
        }

        /// <summary>
        /// Gets the default test repository instance.
        /// </summary>
        public static TestConfigurationRepository DefaultInstance
        {
            get
            {
                if (defaultInstance == null)
                {
                    lock (LockObject)
                    {
                        if (defaultInstance == null)
                        {
                            defaultInstance = TestConfigurationRepository.CreateDefaultInstance(DefaultConfigFileName);
                        }
                    }

                    return defaultInstance;
                }

                return defaultInstance;
            }
        }

        /// <summary>
        /// Gets the default test repository instance.
        /// </summary>
        public static TestConfigurationRepository FedRampInstance
        {
            get
            {
                if (fedRampInstance == null)
                {
                    lock (LockObject)
                    {
                        if (fedRampInstance == null)
                        {
                            fedRampInstance = TestConfigurationRepository.CreateDefaultInstance(FedRampConfigFileName);
                            defaultInstance = fedRampInstance;//overwrite default for FedRamp
                            IsDefaultInstance = false;
                        }
                    }

                    return fedRampInstance;
                }

                return fedRampInstance;
            }
        }

        /// <summary>
        /// Gets the list of endpoints.
        /// </summary>
        public ReadOnlyCollection<EndpointInfo> Endpoints { get; private set; }

        /// <summary>
        /// Gets the list of environments.
        /// </summary>
        public ReadOnlyCollection<EnvironmentInfo> Environments { get; private set; }

        /// <summary>
        /// Gets the list of Cobalt modules.
        /// </summary>
        public ReadOnlyCollection<CobaltModuleInfo> Modules { get; private set; }

        /// <summary>
        /// Gets the list of Cobalt products.
        /// </summary>
        public ReadOnlyCollection<CobaltProductInfo> Products { get; private set; }

        /// <summary>
        /// Gets the list of test clients to run tests.
        /// </summary>
        public ReadOnlyCollection<TestClientInfo> TestClients { get; private set; }

        /// <summary>
        /// Finds an endpoint.
        /// </summary>
        /// <param name="module">The module.</param>
        /// <param name="product">The product.</param>
        /// <param name="environment">The environment.</param>
        /// <returns>The <see cref="EndpointInfo"/>.</returns>
        public EndpointInfo FindEndpoint(
            CobaltModuleInfo module,
            CobaltProductInfo product,
            EnvironmentInfo environment)
        {
            return module == null || product == null || environment == null
                       ? null
                       : this.FindEndpoint(module.Id, product.Id, environment.Id);
        }

        /// <summary>
        /// Finds an endpoint.
        /// </summary>
        /// <param name="moduleId">The module ID.</param>
        /// <param name="productId">The product ID.</param>
        /// <param name="envId">The environment ID.</param>
        /// <returns>The <see cref="EndpointInfo"/>.</returns>
        public EndpointInfo FindEndpoint(CobaltModuleId moduleId, CobaltProductId productId, EnvironmentId envId)
        {
            return
                this.endpoints.FirstOrDefault(
                    e => e.Environment.Id == envId && e.Module.Id == moduleId && e.Product.Id == productId);
        }

        /// <summary>
        /// Finds a Cobalt entity by its ID.
        /// </summary>
        /// <param name="entityId">The ID.</param>
        /// <typeparam name="TEntity">The type of the Cobalt entity.</typeparam>
        /// <typeparam name="TId">The type of the Cobalt entity ID.</typeparam>
        /// <typeparam name="TType">The Type of the Cobalt entity type.</typeparam>
        /// <returns>The new entity</returns>
        public TEntity FindEntityById<TEntity, TId, TType>(TId entityId) where TEntity : CobaltEntityInfo<TId, TType>
                                                                         where TId : struct where TType : struct
        {
            var lookupCollection = this.typeCollectionMap[typeof(TEntity)] as ICollection<TEntity>;

            return lookupCollection == null
                       ? default(TEntity)
                       : lookupCollection.FirstOrDefault(e => e.Id.Equals(entityId));
        }

        /// <summary>
        /// Finds a Cobalt entity by its string ID.
        /// </summary>
        /// <param name="entityId">The string ID.</param>
        /// <typeparam name="TEntity">The type of the Cobalt entity.</typeparam>
        /// <typeparam name="TId">The type of the Cobalt entity ID.</typeparam>
        /// <typeparam name="TType">The Type of the Cobalt entity type.</typeparam>
        /// <returns>The new Entity</returns>
        public TEntity FindEntityById<TEntity, TId, TType>(string entityId)
            where TEntity : CobaltEntityInfo<TId, TType> where TId : struct where TType : struct
        {
            TId id;

            return Enum.TryParse(entityId, true, out id)
                       ? this.FindEntityById<TEntity, TId, TType>(id)
                       : default(TEntity);
        }

        /// <summary>
        /// Finds a Cobalt entity by its reporting tag.
        /// </summary>
        /// <param name="reportingTag">The reporting tag.</param>
        /// <typeparam name="TEntity">The type of the Cobalt entity.</typeparam>
        /// <returns>The new Entity.</returns>
        public TEntity FindEntityByTag<TEntity>(string reportingTag) where TEntity : ICobaltEntityInfo
        {
            var lookupCollection = this.typeCollectionMap[typeof(TEntity)] as ICollection<TEntity>;

            reportingTag = reportingTag == null ? null : reportingTag.Trim();
            return lookupCollection == null
                       ? default(TEntity)
                       : lookupCollection.FirstOrDefault(
                           e => e.TagName.Equals(reportingTag, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Finds an environment by its ID.
        /// </summary>
        /// <param name="envId">The environment ID.</param>
        /// <returns>The <see cref="EnvironmentInfo"/>.</returns>
        public EnvironmentInfo FindEnvironment(EnvironmentId envId)
        {
            return this.FindEntityById<EnvironmentInfo, EnvironmentId, EnvironmentType>(envId);
        }

        /// <summary>
        /// Finds an environment by its string ID.
        /// </summary>
        /// <param name="envId">The string environment ID.</param>
        /// <returns>The <see cref="EnvironmentInfo"/>.</returns>
        public EnvironmentInfo FindEnvironment(string envId)
        {
            return this.FindEntityById<EnvironmentInfo, EnvironmentId, EnvironmentType>(envId);
        }

        /// <summary>
        /// Finds a Cobalt Module by its ID.
        /// </summary>
        /// <param name="moduleId">The Cobalt module ID.</param>
        /// <returns>The <see cref="CobaltModuleInfo"/>.</returns>
        public CobaltModuleInfo FindModule(CobaltModuleId moduleId)
        {
            return this.FindEntityById<CobaltModuleInfo, CobaltModuleId, CobaltModuleType>(moduleId);
        }

        /// <summary>
        /// Finds a Cobalt product by its ID.
        /// </summary>
        /// <param name="productId">The Cobalt product ID.</param>
        /// <returns>The <see cref="CobaltProductInfo"/>.</returns>
        public CobaltProductInfo FindProduct(CobaltProductId productId)
        {
            return this.FindEntityById<CobaltProductInfo, CobaltProductId, CobaltProductType>(productId);
        }

        /// <summary>
        /// Finds a test client by its ID.
        /// </summary>
        /// <param name="clientId">The test client's ID.</param>
        /// <returns>The <see cref="TestClientInfo"/>.</returns>
        public TestClientInfo FindTestClient(TestClientId clientId)
        {
            return this.FindEntityById<TestClientInfo, TestClientId, TestClientType>(clientId);
        }

        /// <summary>
        /// Finds a test client by its string alias.
        /// </summary>
        /// <param name="clientAlias">The string alias of the test client.</param>
        /// <returns>The <see cref="TestClientInfo"/>.</returns>
        public TestClientInfo FindTestClientByAlias(string clientAlias)
        {
            return this.testClients.FirstOrDefault(e => e.Alias == clientAlias);
        }

        /// <summary>
        ///  Reads the repository data from the specified configuration file.
        /// </summary>
        /// <param name="configFileName">The configuration file name.
        /// </param>
        public void InitRepository(string configFileName)
        {
            var reader = new TestRepositoryReader();

            try
            {
                reader.ReadSettings(configFileName);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(
                    string.Format(
                        "The file '{0}' may be absent or corrupted. Test execution cannot continue.",
                        configFileName),
                    e);
            }

            this.ClearRepositories();
            TestConfigurationRepository.ConvertAndAppend<EnvironmentElement, EnvironmentInfo>(
                this.environments,
                reader.EnvironmentDefinitions);
            TestConfigurationRepository.ConvertAndAppend<CobaltModuleElement, CobaltModuleInfo>(
                this.modules,
                reader.ModuleDefinitions);
            TestConfigurationRepository.ConvertAndAppend<CobaltProductElement, CobaltProductInfo>(
                this.products,
                reader.ProductDefinitions);
            TestConfigurationRepository.ConvertAndAppend<TestClientElement, TestClientInfo>(
                this.testClients,
                reader.TestClientDefinitions);

            foreach (EndpointElement endpointElement in reader.EndpointDefinitions)
            {
                this.endpoints.Add(
                    new EndpointInfo
                    {
                        Environment = this.FindEnvironment(endpointElement.EnvironmentId),
                        Module = this.FindModule(endpointElement.ModuleId),
                        Product = this.FindProduct(endpointElement.ProductId),
                        Uri = endpointElement.Uri
                    });
            }
        }

        /// <summary>
        /// Finds the environment contains the specified site
        /// </summary>
        /// <param name="site">
        /// The site.
        /// </param>
        /// <param name="environment">
        /// The environment.
        /// </param>
        /// <returns>
        /// The <see cref="EnvironmentInfo"/>.
        /// </returns>
        public EnvironmentInfo ResolveEnvironmentForSite(string site, EnvironmentInfo environment)
        {
            if (string.Equals(environment.Site, site, StringComparison.InvariantCultureIgnoreCase))
            {
                return environment;
            }

            return
                this.Environments.First(
                    env =>
                        string.Equals(env.Name, environment.Name)
                        && string.Equals(env.Site, site, StringComparison.InvariantCultureIgnoreCase));
        }

        private static void ConvertAndAppend<TIn, TOut>(ICollection<TOut> destination, IEnumerable collectionToAppend)
            where TIn : IProxyEmittable<TOut>
        {
            if (collectionToAppend != null)
            {
                foreach (TIn item in collectionToAppend.Cast<TIn>())
                {
                    destination.Add(item.ProxyObject);
                }
            }
        }

        private static TestConfigurationRepository CreateDefaultInstance(string configFilePath)
        {
            var repository = new TestConfigurationRepository();

            repository.InitRepository(configFilePath);
            return repository;
        }

        private void ClearRepositories()
        {
            this.environments.Clear();
            this.endpoints.Clear();
            this.modules.Clear();
            this.products.Clear();
        }
    }
}