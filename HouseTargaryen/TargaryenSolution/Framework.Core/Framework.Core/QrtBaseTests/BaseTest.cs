namespace Framework.Core.QrtBaseTests
{
    using System.Collections.Generic;
    using System.IO;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.DataModel.Configuration.Proxies;
    using Framework.Core.QualityChecks.Result;
    using Framework.Core.Utils;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Base test controller class.
    /// </summary>
    [DeploymentItem(QrtTestConfigInfo.DefaultLocalTestConfigFileName, "Resources")]   
    public abstract class BaseTest
    {
        private static readonly object LockObject = new object();

        private static volatile string localTestConfigFileLocation;

        private readonly TestSettings settingsBackUp;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTest"/> class. 
        /// </summary>
        protected BaseTest()
        {
            this.Settings = new TestSettings();
            this.settingsBackUp = new TestSettings();
        }

        /// <summary>
        /// Test settings
        /// </summary>
        public TestSettings Settings { get; private set; }

        /// <summary>
        /// Gets or sets the test context of the test case currently being executed.
        /// </summary>
        /// <value>The value of the test context of the test case currently being executed.</value>
        public TestContext TestContext { get; set; }

        /// <summary>
        /// The file path of the configuration file to use for local test runs; if not set the 
        /// default location will be used instead.
        /// Note that this configuration file should only be used for local runs, nightly regression runs
        /// should be configured through the use of environment variables set directly in the TEX script.
        /// </summary>
        protected static string LocalTestConfigFileLocation
        {
            get
            {
                if (localTestConfigFileLocation == null)
                {
                    lock (LockObject)
                    {
                        if (localTestConfigFileLocation == null)
                        {
                            localTestConfigFileLocation = QrtTestConfigInfo.DefaultLocalTestConfigFileName;
                        }
                    }
                }

                return localTestConfigFileLocation;
            }

            set
            {
                lock (LockObject)
                {
                    localTestConfigFileLocation = value ?? QrtTestConfigInfo.DefaultLocalTestConfigFileName;
                }
            }
        }

        /// <summary>
        /// Gets or sets the Quality Test Run.
        /// </summary>
        /// <value>The value of the Quality Test Run.</value>
        protected static QualityTestRun QualityTestRun { get; set; }

        /// <summary>
        /// Gets or set the Quality Test Case that is currently being executed.
        /// </summary>
        /// <value>The value of the Quality Test Case that is currently being executed.</value>
        protected QualityTestCase QualityTestCase { get; set; }

        /// <summary>
        /// Cleans up the test run.
        /// </summary>
        public static void CleanupTestRun()
        {
            lock (LockObject)
            {
                if (File.Exists("QualityTestRun.xml"))
                {
                    // Read test results from one dll and deserialize it
                    QualityTestRunInfo firstQualityTestRunInfo =
                        new QualityTestRunInfo().DeserializeQualityTestRun("QualityTestRun.xml");

                    // Convert QualityTestRun (info of test results from another one dll) to QualityTestRunInfo
                    QualityTestRunInfo secondQualityTestRunInfo =
                        new QualityTestRunInfo().ConvertToQualityTestRunInfo(QualityTestRun);

                    // Merge all test results
                    firstQualityTestRunInfo.QualityTestCases.ListQualityTestCase.AddRange(
                        secondQualityTestRunInfo.QualityTestCases.ListQualityTestCase);

                    // Serializes all test results and save to QualityTestRun.xml
                    firstQualityTestRunInfo.SerializeQualityTestRun();
                }
                else
                {
                    QualityTestRun.ToXmlFile();
                }

                // Delete Log folder and all log files if logging to the file wasn't provided by logger type
                Logger.DeleteLogFolderIfEmpty();
            }
        }

        /// <summary>
        /// Initializes the test run.
        /// </summary>
        /// <param name="forceInitQualityTestRun">Specifies, whether the quality test run should always be reset even if not NULL.</param>
        public static void InitializeTestRun(bool forceInitQualityTestRun = false)
        {
            if (forceInitQualityTestRun || QualityTestRun == null)
            {
                lock (LockObject)
                {
                    if (QualityTestRun == null)
                    {
                        QualityTestRun = new QualityTestRun();
                    }
                }
            }
        }

        /// <summary>
        /// Cleans up the currently executing test case in the test run.
        /// </summary>
        public virtual void CleanupTestCase()
        {
            this.QualityTestCase.Cleanup(this.TestContext);
            this.Settings.InitFromSettings(this.settingsBackUp);
        }

        /// <summary>
        /// Initializes the currently executing test case in the test run.
        /// </summary>
        public virtual void InitializeTestCase()
        {
            this.InitLogger();
            if (this.TestContext.DataRow == null)
            {
                this.QualityTestCase = new QualityTestCase(
                    this.TestContext.TestName,
                    this.TestContext.FullyQualifiedTestClassName);
            }
            else
            {
                string testName =
                    $"{this.TestContext.TestName} (Data Row {this.TestContext.DataRow.Table.Rows.IndexOf(this.TestContext.DataRow)})";

                this.QualityTestCase = new QualityTestCase(testName, this.TestContext.FullyQualifiedTestClassName);
            }

            QualityTestRun.QualityTestCases.Add(this.QualityTestCase);

            this.Settings.UpdateFromTestProperties(this.TestContext.Properties);
            this.settingsBackUp.InitFromSettings(this.Settings);
        }

        /// <summary>
        /// The get default test settings.
        /// </summary>
        /// <returns>
        /// The IDictionary.
        /// </returns>
        protected abstract IDictionary<string, string> GetDefaultTestSettings();

        /// <summary>
        /// The init test settings.
        /// </summary>
        /// <returns>
        /// The <see cref="TestSettings"/>.
        /// </returns>
        protected TestSettings InitTestSettings()
        {
            this.Settings.InitFromPairs(this.GetDefaultTestSettings(), SettingUpdateOption.Overwrite)
                .InitFromFile(QrtTestConfigInfo.DefaultLocalTestConfigFileName, SettingUpdateOption.Overwrite)
                .InitFromEnvironment(SettingUpdateOption.Overwrite);
            return this.Settings;
        }

        /// <summary>
        /// InitializeLogger
        /// To use your own configuration, specify it in your own proj. configs and change this parameter accordingly
        /// </summary>
        protected abstract void InitLogger();
    }
}