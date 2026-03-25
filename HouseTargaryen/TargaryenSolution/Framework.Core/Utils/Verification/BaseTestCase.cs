namespace Framework.Core.Utils.Verification
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Framework.Core.QualityChecks.Result;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Base class for TestCaseVerify and TestCaseAssert
    /// </summary>
    public abstract class BaseTestCase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTestCase"/> class. 
        /// </summary>
        /// <param name="handle">  For API tests TakeScreenshotDelegate is null
        /// for UI - TakeScreenshot method
        /// </param>
        public BaseTestCase(TakeScreenshotDelegate handle = null)
        {
            this.ScreenshotTaker = handle;
        }

        /// <summary>
        /// Take Screen shot Delegate
        /// </summary>
        public delegate void TakeScreenshotDelegate();

        /// <summary>
        /// Checks
        /// </summary>
        public Dictionary<string, Outcome> Checks { get; set; } = new Dictionary<string, Outcome>();

        /// <summary>
        /// Screenshot Taker
        /// </summary>
        protected TakeScreenshotDelegate ScreenshotTaker { get; set; }

        /// <summary>
        /// Register checks
        /// </summary>
        /// <param name="checks"></param>
        public void AddChecks(params string[] checks)
        {
            foreach (string check in checks)
            {
                if (this.Checks.ContainsKey(check))
                {
                    throw new ArgumentException($"A check with name '{check}' already exists in the collection.");
                }

                this.Checks.Add(check, Outcome.Inconclusive);
            }
        }

        #region Verifications
        /// <summary>
        /// Verify True
        /// </summary>
        /// <param name="check"> Check </param>
        /// <param name="condition"> Condition </param>
        /// <param name="message"> Message </param>
        public void IsTrue(string check, bool condition, string message = null)
        {
            try
            {
                Assert.IsTrue(condition, message);
                this.HandlePassedVerification(check);
            }
            catch (AssertFailedException ex)
            {
                this.HandleException(check, ex);
            }
        }

        /// <summary>
        /// Verify False
        /// </summary>
        /// <param name="check"> Check </param>
        /// <param name="condition"> Condition </param>
        /// <param name="message"> Message </param>
        public void IsFalse(string check, bool condition, string message = null)
        {
            try
            {
                Assert.IsFalse(condition, message);
                this.HandlePassedVerification(check);
            }
            catch (AssertFailedException ex)
            {
                this.HandleException(check, ex);
            }
        }

        /// <summary>
        /// Verify delegate action
        /// </summary>
        /// <param name="check"> Check </param>
        /// <param name="action">A void-return delegate.</param>
        /// <param name="message"> Message </param>
        public void Verify(string check, Action action, string message = null)
        {
            try
            {
                action();
                this.HandlePassedVerification(check);
            }
            catch (AssertFailedException ex)
            {
                this.HandleException(check, ex);
            }
        }

        /// <summary>
        /// Verify delegate action
        /// </summary>
        /// <param name="check"> Check </param>
        /// <param name="actions">A void-return delegate.</param>
        public void Verify(string check, params Action[] actions)
        {
            try
            {
                foreach (Action action in actions)
                {
                    action();
                }

                this.HandlePassedVerification(check);
            }
            catch (AssertFailedException ex)
            {
                this.HandleException(check, ex);
            }
        }

        /// <summary>
        /// Verify Are Equal
        /// </summary>
        /// <typeparam name="T"> Type </typeparam>
        /// <param name="check"> Check </param>
        /// <param name="expected"> Expected </param>
        /// <param name="actual"> Actual </param>
        /// <param name="message"> Message </param>
        public void AreEqual<T>(
            string check,
            T expected,
            T actual,
           string message = null)
        {
            try
            {
                Assert.AreEqual(expected, actual, message);
                this.HandlePassedVerification(check);
            }
            catch (AssertFailedException ex)
            {
                this.HandleException(check, ex);
            }
        }

        /// <summary>
        /// Verify Is Not Null
        /// </summary>
        /// <param name="check"> Check </param>
        /// <param name="value"> Value </param>
        /// <param name="message"> Message </param>
        public void IsNotNull(string check, object value, string message = null)
        {
            try
            {
                Assert.IsNotNull(value, message);
                this.HandlePassedVerification(check);
            }
            catch (AssertFailedException ex)
            {
                this.HandleException(check, ex);
            }
        }

        /// <summary>
        /// Verify Is Null
        /// </summary>
        /// <param name="check"> Check </param>
        /// <param name="value"> Value </param>
        /// <param name="message"> Message </param>
        public void IsNull(string check, object value, string message = null)
        {
            try
            {
                Assert.IsNull(value, message);
                this.HandlePassedVerification(check);
            }
            catch (AssertFailedException ex)
            {
                this.HandleException(check, ex);
            }
        }

        /// <summary>
        /// Verify Are The Same
        /// </summary>
        /// <param name="check"> Check </param>
        /// <param name="expected"> Expected </param>
        /// <param name="actual"> Actual </param>
        /// <param name="message"> Message </param>
        public void AreSame(string check, object expected, object actual, string message = null)
        {
            try
            {
                Assert.AreSame(expected, actual, message);
                this.HandlePassedVerification(check);
            }
            catch (AssertFailedException ex)
            {
                this.HandleException(check, ex);
            }
        }

        /// <summary>
        /// Verify Are Not The Same
        /// </summary>
        /// <param name="check"> Check </param>
        /// <param name="expected"> Expected </param>
        /// <param name="actual"> Actual </param>
        /// <param name="message"> Message </param>
        public void AreNotSame(string check, object expected, object actual, string message = null)
        {
            try
            {
                Assert.AreNotSame(expected, actual, message);
                this.HandlePassedVerification(check);
            }
            catch (AssertFailedException ex)
            {
                this.HandleException(check, ex);
            }
        }
        #endregion

        /// <summary>
        /// The handle exception.
        /// </summary>
        /// <param name="check">
        /// The check.
        /// </param>
        /// <param name="ex">
        /// The ex.
        /// </param>
        protected abstract void HandleException(string check, AssertFailedException ex);

        /// <summary>
        /// The handle passed verification.
        /// </summary>
        /// <param name="check">
        /// The check.
        /// </param>
        protected abstract void HandlePassedVerification(string check);

        /// <summary>
        /// The write logs.
        /// </summary>
        /// <param name="checkType">
        /// The check type.
        /// </param>
        protected void WriteLogs(string checkType)
        {
            if (this.Checks.Any())
            {
                var outputMessage = new StringBuilder("Test Outcome:\r\n");
                foreach (KeyValuePair<string, Outcome> check in this.Checks)
                {
                    outputMessage.Append($"{checkType} | {check.Value} | {check.Key}\r\n");
                }

                Logger.LogInfo(outputMessage.ToString());
            }
        }
    }
}
