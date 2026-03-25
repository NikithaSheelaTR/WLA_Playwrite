namespace Framework.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.CommonTypes.Constants;
    using Framework.Core.Utils;

    using log4net;
    using log4net.Config;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Base test controller class with a test execution context.
    /// </summary>
    /// <typeparam name="TTestContextType">The type of the test execution context.</typeparam>
    [TestClass]
    [DeploymentItem(TestConfigurationRepository.DefaultConfigFileName, "Resources")]
    [DeploymentItem(TestConfigurationRepository.FedRampConfigFileName, "Resources")]
    [DeploymentItem(PathsToSourceFiles.JsonsForEnumPropertyMaps, PathsToSourceFiles.JsonsForEnumPropertyMaps)]
    [DeploymentItem("app.config")]
    [DeploymentItem("ReportPortal.VSTest.TestLogger.dll")]
    public abstract class BaseContextualTest<TTestContextType> : BaseTest where TTestContextType : TestExecutionContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QrtBaseTests.BaseContextualTest{TTestContextType}"/> class. 
        /// </summary>
        protected BaseContextualTest()
        {
            this.TestExecutionContext =
                Activator.CreateInstance(typeof(TTestContextType), this.Settings) as TTestContextType;
            this.InitTestSettings();
        }

        /// <summary>
        /// Gets or sets the test execution context.
        /// </summary>
        public TTestContextType TestExecutionContext { get; set; }

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
                           { EnvironmentConstants.NameOfTestSuiteToExecute, "LocalTestRun" },
                           { EnvironmentConstants.NamesOfProductsUnderTest, null },
                           { EnvironmentConstants.NameOfTestExecutionFolder, string.Empty },
                           { EnvironmentConstants.NameOfCultureOfTests, null },
                           { EnvironmentConstants.LoggerName, string.Empty }
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
