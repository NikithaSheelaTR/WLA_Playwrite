namespace Framework.Core.QualityChecks.Result
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Xml;

    using Framework.Core.CommonTypes.Enums.TestCapture;
    using Framework.Core.CommonTypes.Settings;
    using Framework.Core.QualityChecks.MsUnit;
    using Framework.Core.TestCapture;
    using Framework.Core.Utils;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Provides a means of storing information about a test case.
    /// </summary>
    public class QualityTestCase
    {
        #region Class Static Constant Declarations

        private const string DEFAULT_SCREENSHOT_FILE_NAME = "screenshot";
        private readonly ImageFileType DEFAULT_SCREENSHOT_FILE_TYPE = ImageFileType.PNG;

        #endregion

        #region Class Variable Declarations

        private string _testPath;
        private string _testName;
        private TestSettings _testSettings;
        private IScreenshotTaker _screenshotTaker;

        /// <summary>
        /// Gets or sets the list of Quality Checks.
        /// </summary>
        /// <value>The list of Quality Checks.</value>
        public IList<QualityCheck> QualityChecks { get; set; }

        /// <summary>
        /// Gets or sets the name of the test path.
        /// </summary>
        /// <value>The name of the test path.</value>
        public string TestPath
        {
            get { return this._testPath; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                var trimmedName = value.Trim();
                if (trimmedName.Length == 0)
                {
                    throw new ArgumentOutOfRangeException("value",
                        "Name of test case cannot be an empty or blank string.");
                }
                if (trimmedName.Length > 4000)
                {
                    throw new ArgumentOutOfRangeException("value", "Name of test case cannot exceed 4000 characters.");
                }
                this._testPath = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the test case.
        /// </summary>
        /// <value>The name of the test case.</value>
        public string TestName
        {
            get { return this._testName; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                var trimmedName = value.Trim();
                if (trimmedName.Length == 0)
                {
                    throw new ArgumentOutOfRangeException("value",
                        "Name of test case cannot be an empty or blank string.");
                }
                if (trimmedName.Length > 256)
                {
                    throw new ArgumentOutOfRangeException("value", "Name of test case cannot exceed 256 characters.");
                }
                this._testName = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<string> Attachments { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TestSettings TestSettings
        {
            get { return this._testSettings; }
            set
            {
                if (value == null)
                {
                    throw new NullReferenceException("The testSettings cannot be null.");
                }
                this._testSettings = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public IScreenshotTaker ScreenshotTaker
        {
            get { return this._screenshotTaker; }
            set
            {
                if (value == null)
                {
                    //TODO - Disabling for now
                    //throw new NullReferenceException("The screenshotTaker cannot be null.");
                }
                this._screenshotTaker = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int ScreenshotCount { get; set; }

        #endregion

        #region Class Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="testName"></param>
        /// <param name="testPath"></param>
        public QualityTestCase(string testName, string testPath)
        {
            this.ScreenshotCount = 0;
            this.TestName = testName;
            this.TestPath = testPath;
            this.ScreenshotTaker = null;
            this.TestSettings = new TestSettings();
            this.QualityChecks = new List<QualityCheck>();
            this.Attachments = new List<String>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="testName"></param>
        /// <param name="testPath"></param>
        /// <param name="testSettings"></param>
        /// <param name="screenshotTaker"></param>
        public QualityTestCase(string testName, string testPath, TestSettings testSettings,
            IScreenshotTaker screenshotTaker)
        {
            this.ScreenshotCount = 0;
            this.TestName = testName;
            this.TestPath = testPath;
            this.ScreenshotTaker = screenshotTaker;
            this.TestSettings = testSettings;
            this.QualityChecks = new List<QualityCheck>();
            this.Attachments = new List<String>();
        }

        #endregion

        #region Getters

        /// <summary>
        /// Retrieves the list of Quality Checks.
        /// </summary>
        /// <returns>the list of Quality Checks.</returns>
        public IList<QualityCheck> GetCheckList()
        {
            return this.GetCheckList(null, null);
        }

        /// <summary>
        /// Retrieves all Quality Checks in the list, filtered by Quality Check Type.
        /// </summary>
        /// <param name="qualityCheckType">a Quality Check Type</param>
        /// <returns>all Quality Checks in the list, filtered by Quality Check Type</returns>
        public IList<QualityCheck> GetCheckList(QualityCheckType qualityCheckType)
        {
            return this.GetCheckList(qualityCheckType, null);
        }

        /// <summary>
        /// Retrieves all Quality Checks in the list, filtered by Quality Check Outcome.
        /// </summary>
        /// <param name="qualityCheckOutcome">a Quality Check Outcome</param>
        /// <returns>all Quality Checks in the list, filtered by Quality Check Outcome</returns>
        public IList<QualityCheck> GetCheckList(Outcome qualityCheckOutcome)
        {
            return this.GetCheckList(null, qualityCheckOutcome);
        }

        /// <summary>
        /// Retrieves the QualityCheck from the QualityTestCase with the specified name,
        /// primarily for backwards compatibility in older test suites
        /// </summary>
        /// <param name="checkName">the name of the QualityCheck</param>
        /// <returns>the corresponding QualityCheck</returns>
        /// <exception cref="ArgumentException">thrown if a QualityCheck cannot be forund with the specified name</exception>
        public QualityCheck GetCheck(string checkName)
        {
            foreach (var check in this.QualityChecks)
            {
                if (check.Name.Equals(checkName))
                {
                    return check;
                }
            }
            throw new ArgumentException("No QualityCheck could be found corresponding to the name: \'" + checkName +
                                        "\'.");
        }

        /// <summary>
        /// Retrieves the QualityCheck from the QualityTestCase with the specified name,
        /// primarily for backwards compatibility in older test suites
        /// </summary>
        /// <param name="checkName">the name of the QualityCheck</param>
        /// <returns>the corresponding QualityCheck</returns>
        /// <exception cref="ArgumentException">thrown if a QualityCheck cannot be forund with the specified name</exception>
        public QualityCheck GetQualityCheckByName(string checkName)
        {
            return this.GetCheck(checkName);
        }

        /// <summary>
        /// Checks to see if a QualityCheck from the QualityTestCase has the specified name.
        /// </summary>
        /// <param name="checkName">The name of the QualityCheck.</param>
        /// <returns>True if a QualityCheck from the QualityTestCase has the specified name and false if it doesn't.</returns>
        public bool HasCheck(string checkName)
        {
            try
            {
                return (this.GetCheck(checkName) != null);
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        /// <summary>
        /// Retrieves all Quality Checks in the list, filtered by Quality Check Type and Quality Check Outcome. null values are supported.
        /// </summary>
        /// <param name="qualityCheckType">a Quality Check Type</param>
        /// <param name="qualityCheckOutcome">a Quality Check Outcome</param>
        /// <returns>all Quality Checks in the list, filtered by Quality Check Type and Quality Check Outcome</returns>
        public IList<QualityCheck> GetCheckList(QualityCheckType? qualityCheckType, Outcome? qualityCheckOutcome)
        {
            // return all checks if both type and status are null
            if ((qualityCheckType == null) && (qualityCheckOutcome == null))
            {
                return this.QualityChecks;
            }

            var filteredQualityChecks = new List<QualityCheck>();
            foreach (var qualityCheck in this.QualityChecks)
            {
                // look based on status
                if (qualityCheckType == null)
                {
                    if (qualityCheck.Outcome.Equals(qualityCheckOutcome))
                    {
                        filteredQualityChecks.Add(qualityCheck);
                    }
                }
                // look based on type
                else if (qualityCheckOutcome == null)
                {
                    if (qualityCheck.QualityCheckType.Equals(qualityCheckType))
                    {
                        filteredQualityChecks.Add(qualityCheck);
                    }
                }
                // look based on both type and status
                else
                {
                    if (qualityCheck.QualityCheckType.Equals(qualityCheckType) &&
                        qualityCheck.Outcome.Equals(qualityCheckOutcome))
                    {
                        filteredQualityChecks.Add(qualityCheck);
                    }
                }
            }
            return filteredQualityChecks;
        }

        /// <summary>
        /// Returns the current Outcome of the test.
        /// This function will loop through the checks and see if there are any failures or inconclusives.
        /// If there is a failing check, it will return a failed outcome.
        /// If there is no failing check, but there is an inconclusive check, it will return an inconclusive outcome.
        /// If there are not failures or inconclusives, it will return a passing outcome.
        /// </summary>
        /// <returns></returns>
        public Outcome GetTestOutcome()
        {
            var finalOutcome = Outcome.Passed;
            foreach (var check in this.QualityChecks)
            {
                var checkOutcome = check.Outcome;
                if (checkOutcome.Equals(Outcome.Failed))
                {
                    return checkOutcome;
                }

                if (checkOutcome.Equals(Outcome.Inconclusive))
                {
                    finalOutcome = checkOutcome;
                }
            }
            return finalOutcome;
        }

        #endregion

        #region Add Functions 

        /// <summary>
        /// Adds a new QualityCheck to the QualityTestCase.
        /// </summary>
        /// <param name="checkName">The name of the new check being added.</param>
        /// <returns>The QualityCheck that has been newly-added to the QualityTestCase. </returns>
        /// <exception cref="ArgumentException">Thrown if a duplicate check is already present</exception>
        public QualityCheck AddCheck(string checkName)
        {
            if (this.HasCheck(checkName))
            {
                throw new ArgumentException(
                    string.Format("A quality check with name '{0}' already exists in the collection.", checkName));
            }

            var newCheck = new QualityCheck(checkName);
            this.QualityChecks.Add(newCheck);
            return newCheck;
        }

        /// <summary>
        /// Adds multiple QualityCheck objects to the list of QualityCheck objects for a QualityTestCase.
        /// </summary>
        /// <param name="checks">A comma-delimited list of QualityCheck objects.</param>
        /// <exception cref="ArgumentException">Thrown if a duplicate check is already present</exception>
        public void AddQualityChecks(params QualityCheck[] checks)
        {
            foreach (var check in checks)
            {
                if (this.QualityChecks.Any(qc => qc.Name.Equals(check.Name, StringComparison.InvariantCulture)))
                {
                    throw new ArgumentException(
                        string.Format("A quality check with name '{0}' already exists in the collection.", check.Name));
                }

                this.QualityChecks.Add(check);
            }

        }

        #endregion

        #region Generic Object Functions 

        /// <summary>
        /// Determines whether the specified <see cref="QualityTestCase"/> is equal to the current <see cref="QualityTestCase"/>.
        /// </summary>
        /// <param name="aThat">The <see cref="QualityTestCase"/> to compare with the current <see cref="QualityTestCase"/>.</param>
        /// <returns><b>true</b> if the specified Object is equal to the current <see cref="QualityCheck"/>; otherwise, <b>false</b>.</returns>
        public override bool Equals(Object aThat)
        {
            if (aThat == null || this.GetType() != aThat.GetType())
            {
                return false;
            }
            var that = (QualityTestCase)aThat;
            return EqualsUtils.AreEqual(this.TestName, that.TestName) && EqualsUtils.AreEqual(this.TestPath, that.TestPath) &&
                   EqualsUtils.AreEqual(this.QualityChecks, that.QualityChecks);
        }


        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>A hash code for the current <see cref="QualityTestCase"/>.</returns>
        public override int GetHashCode()
        {
            var result = HashCodeUtils.Seed;
            result = HashCodeUtils.Hash(result, this.TestName);
            result = HashCodeUtils.Hash(result, this.TestPath);
            result = HashCodeUtils.Hash(result, this.QualityChecks);
            return result;
        }


        /// <summary>
        /// Returns a string that represents the current <see cref="QualityTestCase"/>.
        /// </summary>
        /// <returns>A string that represents the current <see cref="QualityTestCase"/>.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            var xmlSettings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "\t",
                OmitXmlDeclaration = true
            };
            using (var xmlWriter = XmlWriter.Create(sb, xmlSettings))
            {
                xmlWriter.WriteStartDocument();
                this.ToString(xmlWriter);
                xmlWriter.WriteEndDocument();
            }
            return sb.ToString();
        }


        /// <summary>
        /// Serializes this <see cref="QualityTestCase"/> to XML.
        /// </summary>
        /// <param name="xmlWriter">An XmlWriter.</param>
        /// <exception cref="ArgumentNullException">Thrown if xmlWriter has not been initialized or is provided as null.</exception>
        /// <remarks>Ensure that the provided XmlWriter has already written the start of the XML document via <see cref="XmlWriter.WriteStartDocument()"/>, as this action is not taken as part of this method. Correspondingly, be sure to write the end of the XML document via <see cref="XmlWriter.WriteEndDocument()"/> after calling this method.</remarks>
        internal void ToString(XmlWriter xmlWriter)
        {
            // validate input
            if (xmlWriter == null)
            {
                throw new ArgumentNullException("xmlWriter");
            }

            // sort the list of checks and set new order values
            ((List<QualityCheck>)this.QualityChecks).Sort();
            this.SetOrderOfChecks();

            // write xml
            xmlWriter.WriteStartElement("QualityTestCase");
            xmlWriter.WriteElementString("TestName", this.TestName);
            xmlWriter.WriteElementString("TestPath", this.TestPath);
            if (this.Attachments.Count > 0)
            {
                xmlWriter.WriteStartElement("Attachments");
                foreach (string attachment in this.Attachments)
                {
                    xmlWriter.WriteElementString("Attachment", attachment);
                }
                xmlWriter.WriteEndElement();
            }
            if (this.QualityChecks != null && this.QualityChecks.Any())
            {
                xmlWriter.WriteStartElement("QualityChecks");
                foreach (var qualityCheck in this.QualityChecks)
                {
                    qualityCheck.ToString(xmlWriter);
                }
                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();
        }

        #endregion

        #region Cleanup 

        /// <summary>
        /// Sets the Order value of each Quality Check based upon its position in the list of Quality Checks.
        /// </summary>
        internal void SetOrderOfChecks()
        {
            var order = 0;
            foreach (var qualityCheck in this.QualityChecks)
            {
                qualityCheck.Order = order++;
            }
        }

        /// <summary>
        /// Ensures an <see cref="AssertFailedException"/> is thrown if any of the Quality Verifications have an Outcome of Failed.
        /// </summary>
        /// <param name="testContext">A test context for the currently executing test case.</param>
        /// <exception cref="AssertFailedException">Thrown if any Quality Verifications have an outcome of Failed.</exception>
        /// <remarks>
        /// An <see cref="AssertFailedException"/> is not thrown for Quality Assertions via this method, as the Visual Studio Unit Testing Framework will handle throwing their exceptions.
        /// </remarks>
        public void Cleanup(TestContext testContext)
        {
            // determine whether any verifications failed, and fail the test case, if that occurs
            IList<QualityCheck> failedQualityChecks =
                this.QualityChecks.Where(
                    qc => qc.QualityCheckType == QualityCheckType.Verification && qc.Outcome == Outcome.Failed).ToList();
            if (failedQualityChecks.Count > 0)
            {
                var sb = new StringBuilder();
                foreach (var failedQualityCheck in failedQualityChecks)
                {
                    sb.AppendLine(failedQualityCheck.ToString());
                }
                Assert.Fail(sb.ToString());
            }

            // examine remaining verifications only if an exception has not occurred during the test case; hence, check for Passed
            if (testContext.CurrentTestOutcome == UnitTestOutcome.Passed ||
                testContext.CurrentTestOutcome == UnitTestOutcome.InProgress)
            {
                // determine whether any verifications are still inconclusive, and mark the test case inconclusive, if that occurs
                IList<QualityCheck> inconclusiveQualityChecks =
                    this.QualityChecks.Where(qc => qc.Outcome == Outcome.Inconclusive).ToList();
                if (inconclusiveQualityChecks.Count > 0)
                {
                    var sb = new StringBuilder();
                    foreach (var inconclusiveQualityCheck in inconclusiveQualityChecks)
                    {
                        sb.AppendLine(inconclusiveQualityCheck.ToString());
                    }
                    Assert.Inconclusive(sb.ToString());
                }
            }
        }


        #endregion

        #region Screenshot Functionality

        /// <summary>
        /// 
        /// </summary>
        public void TakeScreenshot()
        {
            this.ScreenshotTaker.TakeScreenshot(this.GetScreenshotDirectory(), this.GetScreenshotFileName(), this.GetScreenshotReferencePath());
        }

        private string GetScreenshotReferencePath()
        {
            // Get the testCaptureSetting and test capture directory
            var testCaptureSetting =
                this.TestSettings.GetValue<TestCaptureSetting>(TestSettingKeys.TEST_CAPTURE_SETTING);
            var captureDir = this.TestSettings.GetValue<string>(testCaptureSetting.Equals(TestCaptureSetting.VIDEO_CAPTURE) ? TestSettingKeys.TEST_VIDEO_DIR : TestSettingKeys.TEST_SCREENSHOT_DIR);
            return captureDir;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="checkType"></param>
        /// <param name="outcome"></param>
        private void HandleScreenshot(QualityCheckType checkType, Outcome outcome)
        {
            // If no screenshot taker is specified, return without taking a screenshot
            if (this.ScreenshotTaker == null)
            {
                return;
            }

            // Get the testCaptureSetting
            var testCaptureSetting =
                this.TestSettings.GetValue<TestCaptureSetting>(TestSettingKeys.TEST_CAPTURE_SETTING);

            // Handle if a screenshot should be taken on a failed check
            if (outcome.Equals(Outcome.Failed))
            {
                if (checkType.Equals(QualityCheckType.Verification) &&
                    ((testCaptureSetting.Equals(TestCaptureSetting.SCREEN_CAPTURE_FIRST_FAILED_VERIFY) &&
                      this.ScreenshotCount == 0) ||
                     testCaptureSetting.Equals(TestCaptureSetting.SCREEN_CAPTURE_EACH_FAILED_VERIFY)))
                {
                    this.TakeScreenshot();
                }
                else if (checkType.Equals(QualityCheckType.Assertion) &&
                         ((testCaptureSetting.Equals(TestCaptureSetting.SCREEN_CAPTURE_FIRST_FAILED_ASSERT) &&
                           this.ScreenshotCount == 0) ||
                          testCaptureSetting.Equals(TestCaptureSetting.SCREEN_CAPTURE_EACH_FAILED_ASSERT)))
                {
                    this.TakeScreenshot();
                }
                else if ((testCaptureSetting.Equals(TestCaptureSetting.SCREEN_CAPTURE_FIRST_FAILED_CHECK) &&
                          this.ScreenshotCount == 0) ||
                         testCaptureSetting.Equals(TestCaptureSetting.SCREEN_CAPTURE_EACH_FAILED_CHECK))
                {
                    this.TakeScreenshot();
                }
            }
            // Handle if a screenshot should be taken on every check
            else if (testCaptureSetting.Equals(TestCaptureSetting.SCREEN_CAPTURE_EACH_CHECK) ||
                     (testCaptureSetting.Equals(TestCaptureSetting.SCREEN_CAPTURE_EACH_VERIFY) &&
                      checkType.Equals(QualityCheckType.Verification)) ||
                     (testCaptureSetting.Equals(TestCaptureSetting.SCREEN_CAPTURE_EACH_ASSERT) &&
                      checkType.Equals(QualityCheckType.Assertion)))
            {
                this.TakeScreenshot();
            }
        }

        private string GetScreenshotDirectory()
        {
            // Get the testCaptureSetting and test capture directory
            var testCaptureSetting =
                this.TestSettings.GetValue<TestCaptureSetting>(TestSettingKeys.TEST_CAPTURE_SETTING);
            var captureDir = FileUtils.RootDir + this.TestSettings.GetValue<string>(testCaptureSetting.Equals(TestCaptureSetting.VIDEO_CAPTURE) ? TestSettingKeys.TEST_VIDEO_DIR : TestSettingKeys.TEST_SCREENSHOT_DIR);
            return captureDir;
        }

        private string GetScreenshotFileName()
        {
            // Get the testCaptureSetting and test capture directory
            var testCaptureSetting =
                this.TestSettings.GetValue<TestCaptureSetting>(TestSettingKeys.TEST_CAPTURE_SETTING);

            // Get the name of the new screenshot
            string screenshotName = this.TestName ?? DEFAULT_SCREENSHOT_FILE_NAME;

            // If there can be multiple check screenshots, append a number to the end
            if (testCaptureSetting.Equals(TestCaptureSetting.SCREEN_CAPTURE_EACH_CHECK) ||
                testCaptureSetting.Equals(TestCaptureSetting.SCREEN_CAPTURE_EACH_VERIFY) ||
                testCaptureSetting.Equals(TestCaptureSetting.SCREEN_CAPTURE_EACH_ASSERT) ||
                testCaptureSetting.Equals(TestCaptureSetting.SCREEN_CAPTURE_EACH_FAILED_CHECK) ||
                testCaptureSetting.Equals(TestCaptureSetting.SCREEN_CAPTURE_EACH_FAILED_VERIFY) ||
                testCaptureSetting.Equals(TestCaptureSetting.SCREEN_CAPTURE_EACH_FAILED_ASSERT))
            {
                screenshotName += "_" + this.ScreenshotCount++;
            }

            screenshotName += this.DEFAULT_SCREENSHOT_FILE_TYPE.GetExtension();
            return screenshotName;
        }

        #endregion

        #region  Verify Functionality 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        private void PerformPreVerify(QualityCheck qualityCheck)
        {
            if (qualityCheck == null)
            {
                throw new NullReferenceException("QualityCheck cannot be null.");
            }
            qualityCheck.QualityCheckType = (QualityCheckType.Verification);
            qualityCheck.DateTime = DateTime.UtcNow;
        }

        #region Verify True

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="condition"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool VerifyTrue(QualityCheck qualityCheck, bool condition, string message = null)
        {
            this.PerformPreVerify(qualityCheck);
            try
            {
                Assert.IsTrue(condition, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Passed);
                return true;
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Failed);
                return false;
            }

        }

        #endregion

        #region VerifyFalse

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="condition"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool VerifyFalse(QualityCheck qualityCheck, bool condition, String message = null)
        {
            this.PerformPreVerify(qualityCheck);
            try
            {
                Assert.IsFalse(condition, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Passed);
                return true;
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Failed);
                return false;
            }
        }

        #endregion

        #region VerifyEquals


        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool VerifyEquals<T>(QualityCheck qualityCheck, T expected, T actual, String message = null)
        {
            this.PerformPreVerify(qualityCheck);
            try
            {
                Assert.AreEqual(expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Passed);
                return true;
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Failed);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool VerifyEquals<T>(QualityCheck qualityCheck, T[] expected, T[] actual, string message = null)
        {
            this.PerformPreVerify(qualityCheck);
            try
            {
                Assert.AreEqual(expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Passed);
                return true;
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Failed);
                return false;
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool VerifyEquals(QualityCheck qualityCheck, byte[] expected, byte[] actual, string message = null)
        {
            this.PerformPreVerify(qualityCheck);
            try
            {
                Assert.AreEqual(expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Passed);
                return true;
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Failed);
                return false;
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool VerifyEquals(QualityCheck qualityCheck, short[] expected, short[] actual, string message = null)
        {
            this.PerformPreVerify(qualityCheck);
            try
            {
                Assert.AreEqual(expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Passed);
                return true;
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Failed);
                return false;
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool VerifyEquals(QualityCheck qualityCheck, int[] expected, int[] actual, string message = null)
        {
            this.PerformPreVerify(qualityCheck);
            try
            {
                Assert.AreEqual(expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Passed);
                return true;
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Failed);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool VerifyEquals(QualityCheck qualityCheck, long[] expected, long[] actual, string message = null)
        {
            this.PerformPreVerify(qualityCheck);
            try
            {
                Assert.AreEqual(expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Passed);
                return true;
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Failed);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool VerifyEquals(QualityCheck qualityCheck, float expected, float actual, string message = null)
        {
            this.PerformPreVerify(qualityCheck);
            try
            {
                Assert.AreEqual(expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Passed);
                return true;
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Failed);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="delta"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool VerifyEquals(QualityCheck qualityCheck, float expected, float actual, float delta,
            string message = null)
        {
            this.PerformPreVerify(qualityCheck);
            try
            {
                Assert.AreEqual(expected, actual, delta, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Passed);
                return true;
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Failed);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool VerifyEquals(QualityCheck qualityCheck, float[] expected, float[] actual, string message = null)
        {
            this.PerformPreVerify(qualityCheck);
            try
            {
                Assert.AreEqual(expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Passed);
                return true;
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Failed);
                return false;
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool VerifyEquals(QualityCheck qualityCheck, double expected, double actual, string message = null)
        {
            this.PerformPreVerify(qualityCheck);
            try
            {
                Assert.AreEqual(expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Passed);
                return true;
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Failed);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="delta"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool VerifyEquals(QualityCheck qualityCheck, double expected, double actual, double delta,
            string message = null)
        {
            this.PerformPreVerify(qualityCheck);
            try
            {
                Assert.AreEqual(expected, actual, delta, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Passed);
                return true;
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Failed);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool VerifyEquals(QualityCheck qualityCheck, double[] expected, double[] actual, string message = null)
        {
            this.PerformPreVerify(qualityCheck);
            try
            {
                Assert.AreEqual(expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Passed);
                return true;
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Failed);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool VerifyEquals(QualityCheck qualityCheck, bool[] expected, bool[] actual, string message = null)
        {
            this.PerformPreVerify(qualityCheck);
            try
            {
                Assert.AreEqual(expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Passed);
                return true;
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Failed);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool VerifyEquals(QualityCheck qualityCheck, char[] expected, char[] actual, string message = null)
        {
            this.PerformPreVerify(qualityCheck);
            try
            {
                Assert.AreEqual(expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Passed);
                return true;
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Failed);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool VerifyEquals<T>(QualityCheck qualityCheck, Collection<T> expected, Collection<T> actual,
            string message = null)
        {
            this.PerformPreVerify(qualityCheck);
            try
            {
                Assert.AreEqual(expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Passed);
                return true;
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Failed);
                return false;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TK"></typeparam>
        /// <typeparam name="TV"></typeparam>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool VerifyEquals<TK, TV>(QualityCheck qualityCheck, IDictionary<TK, TV> expected,
            IDictionary<TK, TV> actual, string message = null)
        {
            this.PerformPreVerify(qualityCheck);
            try
            {
                Assert.AreEqual(expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Passed);
                return true;
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Failed);
                return false;
            }
        }

        #endregion

        #region VerifyEqualsNoOrder
        /*
        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool VerifyEqualsNoOrder(QualityCheck qualityCheck, byte[] expected, byte[] actual, string message = null)
        {
            PerformPreVerify(qualityCheck);
            try
            {
                QualityAssert.AssertEqualsNoOrder(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                HandleScreenshot(QualityCheckType.Verification, Outcome.Passed);
                return true;
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                HandleScreenshot(QualityCheckType.Verification, Outcome.Failed);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool VerifyEqualsNoOrder(QualityCheck qualityCheck, short[] expected, short[] actual,
            string message = null)
        {
            PerformPreVerify(qualityCheck);
            try
            {
                QualityAssert.AssertEqualsNoOrder(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                HandleScreenshot(QualityCheckType.Verification, Outcome.Passed);
                return true;
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                HandleScreenshot(QualityCheckType.Verification, Outcome.Failed);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool VerifyEqualsNoOrder(QualityCheck qualityCheck, int[] expected, int[] actual, string message = null)
        {
            PerformPreVerify(qualityCheck);
            try
            {
                QualityAssert.AssertEqualsNoOrder(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                HandleScreenshot(QualityCheckType.Verification, Outcome.Passed);
                return true;
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                HandleScreenshot(QualityCheckType.Verification, Outcome.Failed);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool VerifyEqualsNoOrder(QualityCheck qualityCheck, long[] expected, long[] actual, string message = null)
        {
            PerformPreVerify(qualityCheck);
            try
            {
                QualityAssert.AssertEqualsNoOrder(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                HandleScreenshot(QualityCheckType.Verification, Outcome.Passed);
                return true;
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                HandleScreenshot(QualityCheckType.Verification, Outcome.Failed);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool VerifyEqualsNoOrder(QualityCheck qualityCheck, bool[] expected, bool[] actual, string message = null)
        {
            PerformPreVerify(qualityCheck);
            try
            {
                QualityAssert.AssertEqualsNoOrder(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                HandleScreenshot(QualityCheckType.Verification, Outcome.Passed);
                return true;
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                HandleScreenshot(QualityCheckType.Verification, Outcome.Failed);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool VerifyEqualsNoOrder(QualityCheck qualityCheck, char[] expected, char[] actual, string message = null)
        {
            PerformPreVerify(qualityCheck);
            try
            {
                QualityAssert.AssertEqualsNoOrder(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                HandleScreenshot(QualityCheckType.Verification, Outcome.Passed);
                return true;
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                HandleScreenshot(QualityCheckType.Verification, Outcome.Failed);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool VerifyEqualsNoOrder<T>(QualityCheck qualityCheck, T[] expected, T[] actual, string message = null)
        {
            PerformPreVerify(qualityCheck);
            try
            {
                QualityAssert.AssertEqualsNoOrder(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                HandleScreenshot(QualityCheckType.Verification, Outcome.Passed);
                return true;
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                HandleScreenshot(QualityCheckType.Verification, Outcome.Failed);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool VerifyEqualsNoOrder(QualityCheck qualityCheck, double[] expected, double[] actual,
            string message = null)
        {
            PerformPreVerify(qualityCheck);
            try
            {
                QualityAssert.AssertEqualsNoOrder(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                HandleScreenshot(QualityCheckType.Verification, Outcome.Passed);
                return true;
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                HandleScreenshot(QualityCheckType.Verification, Outcome.Failed);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="delta"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool VerifyEqualsNoOrder(QualityCheck qualityCheck, double[] expected, double[] actual, double delta,
            string message = null)
        {
            PerformPreVerify(qualityCheck);
            try
            {
                QualityAssert.AssertEqualsNoOrder(qualityCheck, expected, actual, delta, message);
                qualityCheck.Outcome = (Outcome.Passed);
                HandleScreenshot(QualityCheckType.Verification, Outcome.Passed);
                return true;
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                HandleScreenshot(QualityCheckType.Verification, Outcome.Failed);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool VerifyEqualsNoOrder(QualityCheck qualityCheck, float[] expected, float[] actual,
            string message = null)
        {
            PerformPreVerify(qualityCheck);
            try
            {
                QualityAssert.AssertEqualsNoOrder(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                HandleScreenshot(QualityCheckType.Verification, Outcome.Passed);
                return true;
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                HandleScreenshot(QualityCheckType.Verification, Outcome.Failed);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="delta"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool VerifyEqualsNoOrder(QualityCheck qualityCheck, float[] expected, float[] actual, float delta,
            string message = null)
        {
            PerformPreVerify(qualityCheck);
            try
            {
                QualityAssert.AssertEqualsNoOrder(qualityCheck, expected, actual, delta, message);
                qualityCheck.Outcome = (Outcome.Passed);
                HandleScreenshot(QualityCheckType.Verification, Outcome.Passed);
                return true;
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                HandleScreenshot(QualityCheckType.Verification, Outcome.Failed);
                return false;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool VerifyEqualsNoOrder<T>(QualityCheck qualityCheck, Collection<T> expected, Collection<T> actual,
            string message = null)
        {
            PerformPreVerify(qualityCheck);
            try
            {
                QualityAssert.AssertEqualsNoOrder(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                HandleScreenshot(QualityCheckType.Verification, Outcome.Passed);
                return true;
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                HandleScreenshot(QualityCheckType.Verification, Outcome.Failed);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TK"></typeparam>
        /// <typeparam name="TV"></typeparam>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool VerifyEqualsNoOrder<TK, TV>(QualityCheck qualityCheck, IDictionary<TK, TV> expected,
            IDictionary<TK, TV> actual, string message = null)
        {
            PerformPreVerify(qualityCheck);
            try
            {
                QualityAssert.AssertEqualsNoOrder(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                HandleScreenshot(QualityCheckType.Verification, Outcome.Passed);
                return true;
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                HandleScreenshot(QualityCheckType.Verification, Outcome.Failed);
                return false;
            }
        }
        */
        #endregion

        #region VerifySame

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool VerifySame(QualityCheck qualityCheck, byte expected, byte actual, string message = null)
        {
            this.PerformPreVerify(qualityCheck);
            try
            {
                Assert.AreSame(expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Passed);
                return true;
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Failed);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool VerifySame(QualityCheck qualityCheck, short expected, short actual, string message = null)
        {
            this.PerformPreVerify(qualityCheck);
            try
            {
                Assert.AreSame(expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Passed);
                return true;
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Failed);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool VerifySame(QualityCheck qualityCheck, int expected, int actual, string message = null)
        {
            this.PerformPreVerify(qualityCheck);
            try
            {
                Assert.AreSame(expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Passed);
                return true;
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Failed);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool VerifySame(QualityCheck qualityCheck, long expected, long actual, string message = null)
        {
            this.PerformPreVerify(qualityCheck);
            try
            {
                Assert.AreSame(expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Passed);
                return true;
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Failed);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool VerifySame(QualityCheck qualityCheck, float expected, float actual, string message = null)
        {
            this.PerformPreVerify(qualityCheck);
            try
            {
                Assert.AreSame(expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Passed);
                return true;
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Failed);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool VerifySame(QualityCheck qualityCheck, double expected, double actual, string message = null)
        {
            this.PerformPreVerify(qualityCheck);
            try
            {
                Assert.AreSame(expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Passed);
                return true;
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Failed);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool VerifySame(QualityCheck qualityCheck, bool expected, bool actual, string message = null)
        {
            this.PerformPreVerify(qualityCheck);
            try
            {
                Assert.AreSame(expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Passed);
                return true;
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Failed);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool VerifySame(QualityCheck qualityCheck, char expected, char actual, string message = null)
        {
            this.PerformPreVerify(qualityCheck);
            try
            {
                Assert.AreSame(expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Passed);
                return true;
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Failed);
                return false;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool VerifySame<T>(QualityCheck qualityCheck, T expected, T actual, string message = null)
        {
            this.PerformPreVerify(qualityCheck);
            try
            {
                Assert.AreSame(expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Passed);
                return true;
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Failed);
                return false;
            }
        }

        #endregion

        #region VerifyNotSame

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool VerifyNotSame(QualityCheck qualityCheck, byte expected, byte actual, string message = null)
        {
            this.PerformPreVerify(qualityCheck);
            try
            {
                Assert.AreNotSame(expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Passed);
                return true;
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Failed);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool VerifyNotSame(QualityCheck qualityCheck, short expected, short actual, string message = null)
        {
            this.PerformPreVerify(qualityCheck);
            try
            {
                Assert.AreNotSame(expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Passed);
                return true;
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Failed);
                return false;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool VerifyNotSame(QualityCheck qualityCheck, int expected, int actual, string message = null)
        {
            this.PerformPreVerify(qualityCheck);
            try
            {
                Assert.AreNotSame(expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Passed);
                return true;
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Failed);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool VerifyNotSame(QualityCheck qualityCheck, long expected, long actual, string message = null)
        {
            this.PerformPreVerify(qualityCheck);
            try
            {
                Assert.AreNotSame(expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Passed);
                return true;
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Failed);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool VerifyNotSame(QualityCheck qualityCheck, float expected, float actual, string message = null)
        {
            this.PerformPreVerify(qualityCheck);
            try
            {
                Assert.AreNotSame(expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Passed);
                return true;
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Failed);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool VerifyNotSame(QualityCheck qualityCheck, double expected, double actual, string message = null)
        {
            this.PerformPreVerify(qualityCheck);
            try
            {
                Assert.AreNotSame(expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Passed);
                return true;
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Failed);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool VerifyNotSame(QualityCheck qualityCheck, bool expected, bool actual, string message = null)
        {
            this.PerformPreVerify(qualityCheck);
            try
            {
                Assert.AreNotSame(expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Passed);
                return true;
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Failed);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool VerifyNotSame(QualityCheck qualityCheck, char expected, char actual, string message = null)
        {
            this.PerformPreVerify(qualityCheck);
            try
            {
                Assert.AreNotSame(expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Passed);
                return true;
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Failed);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool VerifyNotSame<T>(QualityCheck qualityCheck, T expected, T actual, string message = null)
        {
            this.PerformPreVerify(qualityCheck);
            try
            {
                Assert.AreNotSame(expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Passed);
                return true;
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Failed);
                return false;
            }
        }

        #endregion

        #region VerifyNull

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="qualityCheck"></param>
        /// <param name="obj"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool VerifyNull<T>(QualityCheck qualityCheck, T obj, string message = null)
        {
            this.PerformPreVerify(qualityCheck);
            try
            {
                Assert.IsNull(obj, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Passed);
                return true;
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Failed);
                return false;
            }
        }

        #endregion

        #region VerifyNotNull



        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="qualityCheck"></param>
        /// <param name="obj"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool VerifyNotNull<T>(QualityCheck qualityCheck, T obj, string message = null)
        {
            this.PerformPreVerify(qualityCheck);
            try
            {
                Assert.IsNotNull(obj, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Passed);
                return true;
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Verification, Outcome.Failed);
                return false;
            }
        }

        #endregion

        #endregion

        #region Assert Functionality

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        private void PerformPreAssert(QualityCheck qualityCheck)
        {
            if (qualityCheck == null)
            {
                throw new NullReferenceException("QualityCheck cannot be null.");
            }
            qualityCheck.QualityCheckType = QualityCheckType.Assertion;
            qualityCheck.DateTime = (new DateTime());
        }

        #region AssertTrue

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="condition"></param>
        /// <param name="message"></param>
        public void AssertTrue(QualityCheck qualityCheck, bool condition, string message = null)
        {
            this.PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertTrue(qualityCheck, condition, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }

        #endregion

        #region AssertFalse

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="condition"></param>
        /// <param name="message"></param>
        public void AssertFalse(QualityCheck qualityCheck, bool condition, string message = null)
        {
            this.PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertFalse(qualityCheck, condition, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }

        #endregion

        #region AssertEquals

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        public void AssertEquals<T>(QualityCheck qualityCheck, T expected, T actual, string message = null)
        {
            this.PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertEquals(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>

        public void AssertEquals<T>(QualityCheck qualityCheck, T[] expected, T[] actual, string message = null)
        {
            this.PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertEquals(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        public void AssertEquals(QualityCheck qualityCheck, byte[] expected, byte[] actual, string message = null)
        {
            this.PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertEquals(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        public void AssertEquals(QualityCheck qualityCheck, short[] expected, short[] actual, string message = null)
        {
            this.PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertEquals(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        public void AssertEquals(QualityCheck qualityCheck, int[] expected, int[] actual, string message = null)
        {
            this.PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertEquals(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        public void AssertEquals(QualityCheck qualityCheck, long[] expected, long[] actual, string message = null)
        {
            this.PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertEquals(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        public void AssertEquals(QualityCheck qualityCheck, float expected, float actual, string message = null)
        {
            this.PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertEquals(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="delta"></param>
        /// <param name="message"></param>
        public void AssertEquals(QualityCheck qualityCheck, float expected, float actual, float delta,
            string message = null)
        {
            this.PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertEquals(qualityCheck, expected, actual, delta, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        public void AssertEquals(QualityCheck qualityCheck, float[] expected, float[] actual, string message = null)
        {
            this.PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertEquals(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        public void AssertEquals(QualityCheck qualityCheck, double expected, double actual, string message = null)
        {
            this.PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertEquals(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="delta"></param>
        /// <param name="message"></param>
        public void AssertEquals(QualityCheck qualityCheck, double expected, double actual, double delta,
            string message = null)
        {
            this.PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertEquals(qualityCheck, expected, actual, delta, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        public void AssertEquals(QualityCheck qualityCheck, double[] expected, double[] actual, string message = null)
        {
            this.PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertEquals(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        public void AssertEquals(QualityCheck qualityCheck, bool[] expected, bool[] actual, string message = null)
        {
            this.PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertEquals(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        public void AssertEquals(QualityCheck qualityCheck, char[] expected, char[] actual, string message = null)
        {
            this.PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertEquals(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        public void AssertEquals<T>(QualityCheck qualityCheck, Collection<T> expected, Collection<T> actual,
            string message = null)
        {
            this.PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertEquals(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TK"></typeparam>
        /// <typeparam name="TV"></typeparam>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        public void AssertEquals<TK, TV>(QualityCheck qualityCheck, IDictionary<TK, TV> expected,
            IDictionary<TK, TV> actual, string message = null)
        {
            this.PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertEquals(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }

        #endregion

        #region AssertEqualsNoOrder
        /*
        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        public void AssertEqualsNoOrder(QualityCheck qualityCheck, byte[] expected, byte[] actual, string message = null)
        {
            PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertEqualsNoOrder(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        public void AssertEqualsNoOrder(QualityCheck qualityCheck, short[] expected, short[] actual,
            string message = null)
        {
            PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertEqualsNoOrder(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        public void AssertEqualsNoOrder(QualityCheck qualityCheck, int[] expected, int[] actual, string message = null)
        {
            PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertEqualsNoOrder(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        public void AssertEqualsNoOrder(QualityCheck qualityCheck, long[] expected, long[] actual, string message = null)
        {
            PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertEqualsNoOrder(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        public void AssertEqualsNoOrder(QualityCheck qualityCheck, bool[] expected, bool[] actual, string message = null)
        {
            PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertEqualsNoOrder(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        public void AssertEqualsNoOrder(QualityCheck qualityCheck, char[] expected, char[] actual, string message = null)
        {
            PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertEqualsNoOrder(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        public void AssertEqualsNoOrder<T>(QualityCheck qualityCheck, T[] expected, T[] actual, string message = null)
        {
            PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertEqualsNoOrder(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        public void AssertEqualsNoOrder(QualityCheck qualityCheck, double[] expected, double[] actual,
            string message = null)
        {
            PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertEqualsNoOrder(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="delta"></param>
        /// <param name="message"></param>
        public void AssertEqualsNoOrder(QualityCheck qualityCheck, double[] expected, double[] actual, double delta,
            string message = null)
        {
            PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertEqualsNoOrder(qualityCheck, expected, actual, delta, message);
                qualityCheck.Outcome = (Outcome.Passed);
                HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        public void AssertEqualsNoOrder(QualityCheck qualityCheck, float[] expected, float[] actual,
            string message = null)
        {
            PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertEqualsNoOrder(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="delta"></param>
        /// <param name="message"></param>
        public void AssertEqualsNoOrder(QualityCheck qualityCheck, float[] expected, float[] actual, float delta,
            string message = null)
        {
            PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertEqualsNoOrder(qualityCheck, expected, actual, delta, message);
                qualityCheck.Outcome = (Outcome.Passed);
                HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        public void AssertEqualsNoOrder<T>(QualityCheck qualityCheck, Collection<T> expected, Collection<T> actual,
            string message = null)
        {
            PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertEqualsNoOrder(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TK"></typeparam>
        /// <typeparam name="TV"></typeparam>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        public void AssertEqualsNoOrder<TK, TV>(QualityCheck qualityCheck, IDictionary<TK, TV> expected,
            IDictionary<TK, TV> actual, string message = null)
        {
            PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertEqualsNoOrder(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }
        */
        #endregion

        #region AssertSame

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        public void AssertSame(QualityCheck qualityCheck, byte expected, byte actual, string message = null)
        {
            this.PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertSame(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        public void AssertSame(QualityCheck qualityCheck, short expected, short actual, string message = null)
        {
            this.PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertSame(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        public void AssertSame(QualityCheck qualityCheck, int expected, int actual, string message = null)
        {
            this.PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertSame(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        public void AssertSame(QualityCheck qualityCheck, long expected, long actual, string message = null)
        {
            this.PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertSame(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        public void AssertSame(QualityCheck qualityCheck, float expected, float actual, string message = null)
        {
            this.PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertSame(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        public void AssertSame(QualityCheck qualityCheck, double expected, double actual, string message = null)
        {
            this.PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertSame(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        public void AssertSame(QualityCheck qualityCheck, bool expected, bool actual, string message = null)
        {
            this.PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertSame(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        public void AssertSame(QualityCheck qualityCheck, char expected, char actual, string message = null)
        {
            this.PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertSame(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        public void AssertSame<T>(QualityCheck qualityCheck, T expected, T actual, string message = null)
        {
            this.PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertSame(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }

        #endregion

        #region AssertNotSame

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        public void AssertNotSame(QualityCheck qualityCheck, byte expected, byte actual, string message = null)
        {
            this.PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertNotSame(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        public void AssertNotSame(QualityCheck qualityCheck, short expected, short actual, string message = null)
        {
            this.PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertNotSame(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        public void AssertNotSame(QualityCheck qualityCheck, int expected, int actual, string message = null)
        {
            this.PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertNotSame(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        public void AssertNotSame(QualityCheck qualityCheck, long expected, long actual, string message = null)
        {
            this.PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertNotSame(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        public void AssertNotSame(QualityCheck qualityCheck, float expected, float actual, string message = null)
        {
            this.PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertNotSame(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        public void AssertNotSame(QualityCheck qualityCheck, double expected, double actual, string message = null)
        {
            this.PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertNotSame(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        public void AssertNotSame(QualityCheck qualityCheck, bool expected, bool actual, string message = null)
        {
            this.PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertNotSame(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        public void AssertNotSame(QualityCheck qualityCheck, char expected, char actual, string message = null)
        {
            this.PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertNotSame(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="qualityCheck"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        public void AssertNotSame<T>(QualityCheck qualityCheck, T expected, T actual, string message = null)
        {
            this.PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertNotSame(qualityCheck, expected, actual, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }

        #endregion

        #region AssertNotNull

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="qualityCheck"></param>
        /// <param name="obj"></param>
        /// <param name="message"></param>
        public void AssertNull<T>(QualityCheck qualityCheck, T obj, string message = null)
        {
            this.PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertNull(qualityCheck, obj, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="qualityCheck"></param>
        /// <param name="obj"></param>
        /// <param name="message"></param>
        public void AssertNotNull<T>(QualityCheck qualityCheck, T obj, string message = null)
        {
            this.PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertNotNull(qualityCheck, obj, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }

        #endregion

        #region Fail   

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="message"></param>
        /// <param name="realCause"></param>
        /// <exception cref="AssertFailedException"></exception>
        public void Fail(QualityCheck qualityCheck, String message = null, Exception realCause = null)
        {
            this.PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.Fail(qualityCheck, message, realCause);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="message"></param>
        /// <param name="realCause"></param>
        /// <exception cref="AssertFailedException"></exception>
        public void AssertFail(QualityCheck qualityCheck, String message = null, Exception realCause = null)
        {
            this.PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertFail(qualityCheck, message, realCause);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertFailedException e)
            {
                qualityCheck.Outcome = (Outcome.Failed);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Failed);
                throw;
            }
        }

        #endregion

        #region Inconclusive

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="message"></param>
        /// <exception cref="AssertInconclusiveException"></exception>
        public void Inconclusive(QualityCheck qualityCheck, string message = null)
        {
            this.PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.Inconclusive(qualityCheck, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertInconclusiveException e)
            {
                qualityCheck.Outcome = (Outcome.Inconclusive);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Inconclusive);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityCheck"></param>
        /// <param name="message"></param>
        /// <exception cref="AssertInconclusiveException"></exception>
        public void AssertInconclusive(QualityCheck qualityCheck, string message = null)
        {
            this.PerformPreAssert(qualityCheck);
            try
            {
                QualityAssert.AssertInconclusive(qualityCheck, message);
                qualityCheck.Outcome = (Outcome.Passed);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Passed);
            }
            catch (AssertInconclusiveException e)
            {
                qualityCheck.Outcome = (Outcome.Inconclusive);
                qualityCheck.Message = (e.Message);
                this.HandleScreenshot(QualityCheckType.Assertion, Outcome.Inconclusive);
                throw;
            }
        }

        #endregion

        #endregion
    }
}
