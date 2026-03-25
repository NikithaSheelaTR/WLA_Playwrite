namespace Framework.Core.QrtBaseTests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.CommonTypes.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;
    using Framework.Core.Utils;
    using Framework.Core.Utils.Extensions;

    using log4net;
    using log4net.Config;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Base test controller class with a test execution context.
    /// IBM.XMS and AMQ libs are required by Thomson.Novus.ProductAPI
    /// </summary>
    /// <typeparam name="TTestContextType">The type of the test execution context.</typeparam>
    [TestClass]
    [DeploymentItem(TestConfigurationRepository.DefaultConfigFileName, "Resources")]
    [DeploymentItem(TestConfigurationRepository.FedRampConfigFileName, "Resources")]
    [DeploymentItem(PathsToSourceFiles.JsonsForEnumPropertyMaps, PathsToSourceFiles.JsonsForEnumPropertyMaps)]
    [DeploymentItem("app.config")]
    [DeploymentItem("Resources/Rerunner/ReportPortalReruner.exe")]
    [DeploymentItem("ReportPortal.config.json")]
    [DeploymentItem("ReportPortal.VSTest.TestLogger.dll")]
    [DeploymentItem("amqmdnet.dll")]
    [DeploymentItem("amqmdnsp.dll")]
    [DeploymentItem("amqmdxcs.dll")]
    [DeploymentItem("IBM.XMS.Admin.dll")]
    [DeploymentItem("IBM.XMS.Admin.Objects.dll")]
    [DeploymentItem("IBM.XMS.Client.Impl.dll")]
    [DeploymentItem("IBM.XMS.Client.WMQ.dll")]
    [DeploymentItem("IBM.XMS.Core.dll")]
    [DeploymentItem("IBM.XMS.dll")]
    [DeploymentItem("IBM.XMS.Impl.dll")]
    [DeploymentItem("IBM.XMS.Match.dll")]
    [DeploymentItem("IBM.XMS.NLS.dll")]
    [DeploymentItem("IBM.XMS.Provider.dll")]
    [DeploymentItem("IBM.XMS.Util.dll")]
    [DeploymentItem("IBM.XMS.WMQ.dll")]
    public abstract class BaseContextualTest<TTestContextType> : BaseTest
        where TTestContextType : TestExecutionContext
    {
        private static readonly object Locker = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseContextualTest{TTestContextType}"/> class. 
        /// </summary>
        protected BaseContextualTest()
        {
            this.TestExecutionContext =
                Activator.CreateInstance(typeof(TTestContextType), this.Settings) as TTestContextType;
            this.InitTestSettings();
            this.GenerateQrtConfiguration();
        }

        /// <summary>
        /// Gets or sets the test execution context.
        /// </summary>
        public TTestContextType TestExecutionContext { get; set; }

        /// <summary>
        /// The generate qrt configuration.
        /// </summary>
        protected void GenerateQrtConfiguration()
        {
            const string Qrt2FileName = "Qrt2TestConfig.xml";
            string executionPath = this.TestExecutionContext.TestExecutionDirectoryName;

            lock (Locker)
            {
                if (!File.Exists(Path.Combine(executionPath, Qrt2FileName)))
                {
                    var qrtConfig = new QrtTestConfigInfo
                    {
                        BusinessCase = this.TestExecutionContext.BusinessCaseName,
                        TestRunName = this.TestExecutionContext.TestRunName
                    };

                    qrtConfig.Tags.AppendUniqueValues(
                                 new[]
                                     {
                                 this.TestExecutionContext.TestClient.TagName,
                                 this.TestExecutionContext.TestEnvironment.TagName
                                     })
                             .AppendUniqueValues(this.TestExecutionContext.Products.Select(p => p.TagName))
                             .AppendUniqueValues(this.TestExecutionContext.Modules.Select(m => m.TagName))
                             .AppendUniqueValues(this.TestExecutionContext.QrtTags);
                    qrtConfig.NotificationEmailAddresses.AppendUniqueValues(this.TestExecutionContext.SummaryResultsEmail);
                    qrtConfig.ToXmlFile(executionPath != null ? Path.Combine(executionPath, Qrt2FileName) : Qrt2FileName);
                }
            }
        }

        /// <summary>
        /// The get default test settings.
        /// </summary>
        /// <returns>
        /// The IDictionary.
        /// </returns>
        protected override IDictionary<string, string> GetDefaultTestSettings()
        {
            return new Dictionary<string, string>
                       {
                           { EnvironmentConstants.NameOfBrowserUnderTest, null },
                           { EnvironmentConstants.NameOfEnvironmentId, null },
                           { EnvironmentConstants.NameOfBusinessCase, string.Empty },
                           { EnvironmentConstants.NameOfTestSuiteToExecute, "LocalTestRun" },
                           { EnvironmentConstants.NamesOfModulesUnderTest, null },
                           { EnvironmentConstants.NamesOfProductsUnderTest, null },
                           { EnvironmentConstants.NameOfTestExecutionFolder, string.Empty },
                           { EnvironmentConstants.QrtSummaryResultsEmail, string.Empty },
                           { EnvironmentConstants.NameOfCultureOfTests, null },
                           { EnvironmentConstants.NameOfQrtTags, string.Empty },
                           { EnvironmentConstants.LoggerName, string.Empty },
                           { EnvironmentConstants.DriverLocation, null }
                       };
        }
        /// <summary>
        /// InitializeLogger
        /// To use your own configuration, specify it in your own proj. configs and change this parameter accordingly
        /// </summary>
        protected override void InitLogger()
        {
            GlobalContext.Properties["LogName"] = this.TestContext.TestName + ".txt";
            XmlConfigurator.ConfigureAndWatch(new FileInfo(Environment.CurrentDirectory + @"\app.config"));
            Logger.InitializeLogger(this.Settings.GetValue(
                EnvironmentConstants
                    .LoggerName));
        }
    }
}