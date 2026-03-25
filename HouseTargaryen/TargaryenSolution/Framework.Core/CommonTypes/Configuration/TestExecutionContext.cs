using System;

namespace Framework.Core.CommonTypes.Configuration
{
    using Framework.Core.CommonTypes.Constants;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;
    using Framework.Core.DataModel.Security;
    using Framework.Core.DataModel.Security.Proxies;
    using Framework.Core.DataModel.Security.Specialized;
    using Framework.Core.Utils;
    using Framework.Core.Utils.Execution;
    using Framework.Core.Utils.Extensions;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading;

    /// <summary>
    /// The test execution context.
    /// </summary>
    public class TestExecutionContext : IObserver<SettingUpdateContext>
    {
        private CultureInfo cultureOfTests;

        private EnvironmentInfo testEnvironment;

        private IDisposable unsubscriber;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestExecutionContext"/> class.
        /// </summary>
        /// <param name="settings">
        /// The settings.
        /// </param>
        public TestExecutionContext(TestSettings settings)
        {
            this.Settings = settings;
            this.InitialSystemCulture = CultureInfo.CurrentCulture;

            //// ReSharper disable once VirtualMemberCallInContructor
            this.GenerateSettingUpdaterMap();
            this.Subscribe(this.Settings);
        }

        /// <summary>
        /// Gets the Business Case.
        /// </summary>
        public string BusinessCaseName { get; private set; }

        /// <summary>
        /// CultureOfTests
        /// </summary>
        public CultureInfo CultureOfTests
        {
            get
            {
                if (this.cultureOfTests == null)
                {
                    this.CultureOfTests = null;
                }

                return this.cultureOfTests;
            }

            set
            {
                if (value == null)
                {
                    this.SetCultureOfTests(null);
                }
                else
                {
                    this.cultureOfTests = value;
                    Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = this.cultureOfTests;
                }
            }
        }

        /// <summary>
        /// Preserves the locale that was configured in OS before the test was run.
        /// </summary>
        public CultureInfo InitialSystemCulture { get; private set; }

        /// <summary>
        /// Gets the modules.
        /// </summary>
        public IList<CobaltModuleInfo> Modules { get; private set; }

        /// <summary>
        /// The OnePass environment.
        /// </summary>
        public EnvironmentInfo OnePassEnvironment { get; private set; }

        /// <summary>
        /// PasswordPool
        /// </summary>
        public string PasswordPool { get; set; }

        /// <summary>
        /// Gets the products.
        /// </summary>
        public IList<CobaltProductInfo> Products { get; private set; }

        /// <summary>
        /// Gets the qrt tags.
        /// </summary>
        public IList<string> QrtTags { get; private set; }

        /// <summary>
        /// Test settings
        /// </summary>
        public TestSettings Settings { get; private set; }

        /// <summary>
        /// The list of email addresses to send QRT summary results to.
        /// </summary>
        public IList<string> SummaryResultsEmail { get; private set; }

        /// <summary>
        /// The default test client to perform tests.
        /// </summary>
        public TestClientInfo TestClient { get; private set; }

        /// <summary>
        /// The environment under test.
        /// </summary>
        public EnvironmentInfo TestEnvironment
        {
            get
            {
                return this.testEnvironment;
            }

            set
            {
                this.testEnvironment = value;
                this.OnePassEnvironment = this.testEnvironment == null
                                              ? null
                                              : TestConfigurationRepository.DefaultInstance.Environments.FirstOrDefault(
                                                  e =>
                                                      e.Type == EnvironmentType.OnePass
                                                      && e.IsLower
                                                      == this.testEnvironment.IsLower);
            }
        }

        /// <summary>
        /// TestExecutionDirectoryName
        /// </summary>
        public string TestExecutionDirectoryName { get; private set; }

        /// <summary>
        /// Gets the test run name.
        /// </summary>
        public string TestRunName { get; private set; }

        /// <summary>
        /// Gets the updater map.
        /// </summary>
        protected Dictionary<string, Action> UpdaterMap { get; private set; }

        /// <summary>
        /// Notifies the observer that the provider has finished sending push-based notifications.
        /// </summary>
        public void OnCompleted()
        {
            this.unsubscriber?.Dispose();
        }

        /// <summary>
        /// Notifies the observer that the provider has experienced an error condition.
        /// </summary>
        /// <param name="error">The error.</param>
        public virtual void OnError(Exception error)
        {
            Logger.LogError($"'{this.GetType().Name}' has encountered an error: {error}");
            throw error;
        }

        /// <summary>
        /// Provides the observer with new data.
        /// </summary>
        /// <param name="value">The promoted information.</param>
        public virtual void OnNext(SettingUpdateContext value)
        {
            if (!string.IsNullOrEmpty(value.TestSetting.Key)
                && (value.UpdateOption == SettingUpdateOption.Overwrite
                    || value.UpdateOption == SettingUpdateOption.Append)
                && this.UpdaterMap.ContainsKey(value.TestSetting.Key))
            {
                this.UpdaterMap[value.TestSetting.Key]();
            }
        }

        /// <summary>
        /// Prints the test execution context.
        /// </summary>
        public virtual void PrintTestContext()
        {
            Logger.LogInfo("=> GENERAL SETTINGS <=");
            Logger.LogInfo("Test run name:\t\t" + this.TestRunName);
            Logger.LogInfo("Test environment:\t\t" + this.TestEnvironment);
            Logger.LogInfo("Test client:\t\t" + this.TestClient);
            Logger.LogInfo("Locale:\t\t\t" + this.CultureOfTests.Name);
            Logger.LogInfo("Products under test:\t" + string.Join(", ", this.Products.Select(p => $"[{p}]")));
            Logger.LogInfo("Modules under test:\t" + string.Join(", ", this.Modules.Select(m => $"[{m}]")));           
        }

        /// <summary>
        /// Subscribes the observer for events generated by the specified provider.
        /// </summary>
        /// <param name="provider">The provider to subscribe to.</param>
        public void Subscribe(IObservable<SettingUpdateContext> provider)
        {
            if (provider != null)
            {
                this.unsubscriber = provider.Subscribe(this);
            }
        }

        /// <summary>
        /// Initializes the list of actions to change the context on changes to the settings.
        /// </summary>
        protected virtual void GenerateSettingUpdaterMap()
        {
            this.UpdaterMap = new Dictionary<string, Action>
                                  {
                                      {
                                          EnvironmentConstants.PasswordPoolName,
                                          () =>
                                              this.PasswordPool =
                                                  this.Settings.GetValue(
                                                      EnvironmentConstants.PasswordPoolName)
                                      },
                                      {
                                          EnvironmentConstants.NameOfCultureOfTests,
                                          () =>
                                              this.SetCultureOfTests(
                                                  this.Settings.GetValue(
                                                      EnvironmentConstants
                                                          .NameOfCultureOfTests))
                                      },
                                      {
                                          EnvironmentConstants.NameOfBusinessCase,
                                          () =>
                                              this.BusinessCaseName =
                                                  this.Settings.GetValue(
                                                      EnvironmentConstants
                                                          .NameOfBusinessCase)
                                      },
                                      {
                                          EnvironmentConstants.NameOfQrtTags,
                                          () =>
                                              this.QrtTags =
                                                  this.Settings.GetValues(
                                                      EnvironmentConstants.NameOfQrtTags)
                                      },
                                      {
                                          EnvironmentConstants.QrtSummaryResultsEmail,
                                          () =>
                                              this.SummaryResultsEmail =
                                                  this.Settings.GetValues(
                                                      EnvironmentConstants
                                                          .QrtSummaryResultsEmail)
                                      },
                                      {
                                          EnvironmentConstants.NameOfBrowserUnderTest,
                                          () =>
                                              this.TestClient =
                                                  TestConfigurationRepository.DefaultInstance
                                                                             .FindTestClientByAlias
                                                                             (
                                                                                 this
                                                                                     .Settings
                                                                                     .GetValue
                                                                                     (
                                                                                         EnvironmentConstants
                                                                                             .NameOfBrowserUnderTest))
                                                  ?? TestConfigurationRepository
                                                      .DefaultInstance.FindTestClient(
                                                          TestClientId.None)
                                      },
                                          {
                                              EnvironmentConstants.NameOfRemoteDriverUri,
                                              () =>
                                                  this.TestClient.RemoteDriverUri = string.IsNullOrEmpty(this
                                                      .Settings
                                                      .GetValue
                                                      (
                                                          EnvironmentConstants
                                                              .NameOfRemoteDriverUri)) ? this.TestClient.RemoteDriverUri : new Uri(
                                                                                     this
                                                                                         .Settings
                                                                                         .GetValue
                                                                                         (
                                                                                         EnvironmentConstants
                                                                                         .NameOfRemoteDriverUri))

                                          },
                                      {
                                          EnvironmentConstants.NameOfEnvironmentId,
                                          () =>
                                              this.TestEnvironment =
                                                  TestConfigurationRepository.DefaultInstance
                                                                             .FindEnvironment
                                                                             (
                                                                                 this
                                                                                     .Settings
                                                                                     .GetValue
                                                                                     (
                                                                                         EnvironmentConstants
                                                                                             .NameOfEnvironmentId))
                                                  ?? TestConfigurationRepository
                                                      .DefaultInstance.FindEnvironment(
                                                          EnvironmentId.None)
                                      },
                                      {
                                          EnvironmentConstants.NameOfTestExecutionFolder,
                                          () =>
                                              this.TestExecutionDirectoryName =
                                                  this.Settings.GetValue(
                                                      EnvironmentConstants
                                                          .NameOfTestExecutionFolder)
                                      },
                                      {
                                          EnvironmentConstants.NameOfTestSuiteToExecute,
                                          () =>
                                              this.TestRunName =
                                                  this.Settings.GetValue(
                                                      EnvironmentConstants
                                                          .NameOfTestSuiteToExecute)
                                      },
                                      {
                                          EnvironmentConstants.NamesOfModulesUnderTest,
                                          this.GetModules
                                      },
                                      {
                                          EnvironmentConstants.NamesOfProductsUnderTest,
                                          this.GetCobaltProducts
                                      }
                                  };
        }

        /// <summary>
        /// Tries to apply the specified locale or default one, otherwise.
        /// </summary>
        /// <param name="cultureName">The code of the culture in the format [region]-[country]: xx-YY or xx</param>
        protected void SetCultureOfTests(string cultureName)
        {
            CultureInfo culture = this.InitialSystemCulture;

            if (!string.IsNullOrWhiteSpace(cultureName))
            {
                ExecutionResult execResult = SafeMethodExecutor.Execute(() => culture = new CultureInfo(cultureName));

                if (execResult.ResultType != ExecutionResultType.Success)
                {
                    Logger.LogInfo($"{cultureName} is not a supported culture of tests.");
                }
            }

            this.CultureOfTests = culture;
        }

        private static TEntity[] ConvertStringIdsToEntity<TEntity, TId, TType>(
            IEnumerable<string> idsToConvert,
            out IEnumerable<string> nonConvertables) where TEntity : CobaltEntityInfo<TId, TType> where TId : struct
                                                     where TType : struct
        {
            return TestExecutionContext.ConvertStringToEntity(
                idsToConvert,
                id => TestConfigurationRepository.DefaultInstance.FindEntityById<TEntity, TId, TType>(id),
                out nonConvertables);
        }

        private static TEntity[] ConvertStringTagsToEntity<TEntity>(
            IEnumerable<string> tagsToConvert,
            out IEnumerable<string> nonConvertables) where TEntity : ICobaltEntityInfo
        {
            return TestExecutionContext.ConvertStringToEntity(
                tagsToConvert,
                tag => TestConfigurationRepository.DefaultInstance.FindEntityByTag<TEntity>(tag),
                out nonConvertables);
        }

        private static TEntity[] ConvertStringToEntity<TEntity>(
            IEnumerable<string> stringsToConvert,
            Func<string, TEntity> entityRetriever,
            out IEnumerable<string> nonConvertables) where TEntity : ICobaltEntityInfo
        {
            var result = new List<TEntity>();

            nonConvertables = new List<string>();

            if (stringsToConvert != null)
            {
                foreach (string stringId in stringsToConvert)
                {
                    TEntity entity = entityRetriever(stringId);

                    if (entity != null)
                    {
                        if (!result.Any(e => e.Id.Equals(entity.Id)))
                        {
                            result.Add(entity);
                        }
                    }
                    else
                    {
                        ((List<string>)nonConvertables).Add(stringId);
                    }
                }
            }

            return result.ToArray();
        }

        private void GetCobaltProducts()
        {
            var products = new List<CobaltProductInfo>();
            IEnumerable<string> nonConvertables;
            List<string> productStrings =
                new List<string>().AppendUniqueValues(
                    this.Settings.GetValues(EnvironmentConstants.NamesOfProductsUnderTest));

            products.AddRange(
                TestExecutionContext.ConvertStringIdsToEntity<CobaltProductInfo, CobaltProductId, CobaltProductType>(
                    productStrings,
                    out nonConvertables));
            products.AddRange(
                TestExecutionContext.ConvertStringTagsToEntity<CobaltProductInfo>(nonConvertables, out nonConvertables));
            this.Products = products.ToArray();
        }

        private void GetModules()
        {
            var modules = new List<CobaltModuleInfo>();
            IEnumerable<string> nonConvertables;
            List<string> moduleStrings =
                new List<string>().AppendUniqueValues(
                    this.Settings.GetValues(EnvironmentConstants.NamesOfModulesUnderTest));

            modules.AddRange(
                TestExecutionContext.ConvertStringIdsToEntity<CobaltModuleInfo, CobaltModuleId, CobaltModuleType>(
                    moduleStrings,
                    out nonConvertables));
            modules.AddRange(
                TestExecutionContext.ConvertStringTagsToEntity<CobaltModuleInfo>(nonConvertables, out nonConvertables));
            this.Modules = modules.ToArray();
        }
    }
}