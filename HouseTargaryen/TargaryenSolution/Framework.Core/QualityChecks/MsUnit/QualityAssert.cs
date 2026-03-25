namespace Framework.Core.QualityChecks.MsUnit
{
    using System;
    using System.Globalization;

    using Framework.Core.QualityChecks.Result;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// A set of assertion methods useful for writing test cases.
    /// </summary>
    /// <remarks>
    /// All <see cref="Assert"/> methods available in the Visual Studio Unit Testing Framework are supported.  Each method requires a <see cref="QualityCheck"/>.  Upon completion of a method, the provided <see cref="QualityCheck"/> will have a date and time associated with the execution time of the method, a <see cref="QualityCheckType"/> of Assertion, and a <see cref="Result.Outcome"/> indicating success or failure of the underlying assertion.
    /// </remarks>
    /// <seealso cref="Assert"/>
    /// <seealso cref="QualityCheck"/>
    /// <seealso cref="Result.Outcome"/>
    /// <seealso cref="QualityCheckType"/>
    public static class QualityAssert
    {
        private const QualityCheckType Type = QualityCheckType.Assertion;

        #region AreEqual
        /// <summary>
        /// Asserts that two specified doubles are equal, or within the specified accuracy of each other. The assertion fails if they are not within the specified accuracy of each other. Displays a message if the assertion fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the assertion.</param>
        /// <param name="expected">The first double to compare. This is the double the unit test expects.</param>
        /// <param name="actual">The second double to compare. This is the double the unit test produced.</param>
        /// <param name="delta">The required accuracy. The assertion will fail only if <c>expected</c> is different from <c>actual</c> by more than <c>delta</c>.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public static void AreEqual(QualityCheck qualityCheck, double expected, double actual, double delta, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                Assert.AreEqual(expected, actual, delta, message);
                qualityCheck.SetSuccessfulValues();
            }
            catch (AssertFailedException afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                throw;
            }
        }

        /// <summary>
        /// Asserts that two specified doubles are equal, or within the specified accuracy of each other. The assertion fails if they are not within the specified accuracy of each other. Displays a message if the assertion fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the assertion.</param>
        /// <param name="expected">The first double to compare. This is the double the unit test expects.</param>
        /// <param name="actual">The second double to compare. This is the double the unit test produced.</param>
        /// <param name="delta">The required accuracy. The assertion will fail only if <c>expected</c> is different from <c>actual</c> by more than <c>delta</c>.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public static void AssertEquals(QualityCheck qualityCheck, double expected, double actual, double delta, string message = null)
        {
            QualityAssert.AreEqual(qualityCheck, expected, actual, delta, message);
        }

        /// <summary>
        /// Asserts that two specified floats are equal, or within the specified accuracy of each other. The assertion fails if they are not within the specified accuracy of each other. Displays a message if the assertion fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the assertion.</param>
        /// <param name="expected">The first float to compare. This is the single the unit test expects.</param>
        /// <param name="actual">The float single to compare. This is the single the unit test produced.</param>
        /// <param name="delta">The required accuracy. The assertion will fail only if <c>expected</c> is different from <c>actual</c> by more than <c>delta</c>.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public static void AreEqual(QualityCheck qualityCheck, float expected, float actual, float delta, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                Assert.AreEqual(expected, actual, delta, message);
                qualityCheck.SetSuccessfulValues();
            }
            catch (AssertFailedException afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                throw;
            }
        }

        /// <summary>
        /// Asserts that two specified floats are equal, or within the specified accuracy of each other. The assertion fails if they are not within the specified accuracy of each other. Displays a message if the assertion fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the assertion.</param>
        /// <param name="expected">The first float to compare. This is the single the unit test expects.</param>
        /// <param name="actual">The float single to compare. This is the single the unit test produced.</param>
        /// <param name="delta">The required accuracy. The assertion will fail only if <c>expected</c> is different from <c>actual</c> by more than <c>delta</c>.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public static void AssertEquals(QualityCheck qualityCheck, float expected, float actual, float delta, string message = null)
        {
            QualityAssert.AreEqual(qualityCheck, expected, actual, delta, message);
        }

        /// <summary>
        /// Asserts that two specified objects are equal. The assertion fails if the objects are not equal. Displays a message if the assertion fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the assertion.</param>
        /// <param name="expected">The first object to compare. This is the object the unit test expects.</param>
        /// <param name="actual">The second object to compare. This is the object the unit test produced.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public static void AreEqual(QualityCheck qualityCheck, object expected, object actual, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                Assert.AreEqual(expected, actual, message);
                qualityCheck.SetSuccessfulValues();
            }
            catch (AssertFailedException afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                throw;
            }
        }

        /// <summary>
        /// Asserts that two specified objects are equal. The assertion fails if the objects are not equal. Displays a message if the assertion fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the assertion.</param>
        /// <param name="expected">The first object to compare. This is the object the unit test expects.</param>
        /// <param name="actual">The second object to compare. This is the object the unit test produced.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public static void AssertEquals(QualityCheck qualityCheck, object expected, object actual, string message = null)
        {
            QualityAssert.AreEqual(qualityCheck, expected, actual, message);
        }

        /// <summary>
        /// Asserts that two specified strings are equal, ignoring case or not as specified, and using the culture info specified. The assertion fails if they are not equal. Displays a message if the assertion fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the assertion.</param>
        /// <param name="expected">The first string to compare. This is the string the unit test expects.</param>
        /// <param name="actual">The second string to compare. This is the string the unit test produced.</param>
        /// <param name="ignoreCase">A Boolean value that indicates a case-sensitive or insensitive comparison. <c>true</c> indicates a case-insensitive comparison.</param>
        /// <param name="culture">A <see cref="CultureInfo"/> object that supplies culture-specific comparison information.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public static void AreEqual(QualityCheck qualityCheck, string expected, string actual, bool ignoreCase, CultureInfo culture = null, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                Assert.AreEqual(expected, actual, ignoreCase, culture, message);
                qualityCheck.SetSuccessfulValues();
            }
            catch (AssertFailedException afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                throw;
            }
        }

        /// <summary>
        /// Asserts that two specified strings are equal, ignoring case or not as specified, and using the culture info specified. The assertion fails if they are not equal. Displays a message if the assertion fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the assertion.</param>
        /// <param name="expected">The first string to compare. This is the string the unit test expects.</param>
        /// <param name="actual">The second string to compare. This is the string the unit test produced.</param>
        /// <param name="ignoreCase">A Boolean value that indicates a case-sensitive or insensitive comparison. <c>true</c> indicates a case-insensitive comparison.</param>
        /// <param name="culture">A <see cref="CultureInfo"/> object that supplies culture-specific comparison information.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public static void AssertEquals(QualityCheck qualityCheck, string expected, string actual, bool ignoreCase, CultureInfo culture = null, string message = null)
        {
            QualityAssert.AreEqual(qualityCheck, expected, actual, ignoreCase, culture, message);
        }

        /// <summary>
        /// Asserts that two specified generic type data are equal by using the equality operator. The assertion fails if they are not equal. Displays a message if the assertion fails.
        /// </summary>
        /// <typeparam name="T">The type of objects to be compared.</typeparam>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the assertion.</param>
        /// <param name="expected">The first generic type data to compare. This is the generic type data the unit test expects.</param>
        /// <param name="actual">The second generic type data to compare. This is the generic type data the unit test produced.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public static void AreEqual<T>(QualityCheck qualityCheck, T expected, T actual, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                Assert.AreEqual(expected, actual, message);
                qualityCheck.SetSuccessfulValues();
            }
            catch (AssertFailedException afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                throw;
            }
        }

        /// <summary>
        /// Asserts that two specified generic type data are equal by using the equality operator. The assertion fails if they are not equal. Displays a message if the assertion fails.
        /// </summary>
        /// <typeparam name="T">The type of objects to be compared.</typeparam>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the assertion.</param>
        /// <param name="expected">The first generic type data to compare. This is the generic type data the unit test expects.</param>
        /// <param name="actual">The second generic type data to compare. This is the generic type data the unit test produced.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public static void AssertEquals<T>(QualityCheck qualityCheck, T expected, T actual, string message = null)
        {
            QualityAssert.AreEqual(qualityCheck, expected, actual, message);
        }

        #endregion

        #region AreNotEqual
        /// <summary>
        /// Asserts that two specified doubles are not equal, and not within the specified accuracy of each other. The assertion fails if they are equal or within the specified accuracy of each other. Displays a message if the assertion fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the assertion.</param>
        /// <param name="notExpected">The first double to compare. This is the double the unit test expects not to match <c>actual</c>.</param>
        /// <param name="actual">The second double to compare. This is the double the unit test produced.</param>
        /// <param name="delta">The required inaccuracy. The assertion fails only if <c>notExpected</c> is equal to actual or different from it by less than <c>delta</c>.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public static void AreNotEqual(QualityCheck qualityCheck, double notExpected, double actual, double delta, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                Assert.AreNotEqual(notExpected, actual, delta, message);
                qualityCheck.SetSuccessfulValues();
            }
            catch (AssertFailedException afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                throw;
            }
        }

        /// <summary>
        /// Asserts that two specified doubles are not equal, and not within the specified accuracy of each other. The assertion fails if they are equal or within the specified accuracy of each other. Displays a message if the assertion fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the assertion.</param>
        /// <param name="notExpected">The first double to compare. This is the double the unit test expects not to match <c>actual</c>.</param>
        /// <param name="actual">The second double to compare. This is the double the unit test produced.</param>
        /// <param name="delta">The required inaccuracy. The assertion fails only if <c>notExpected</c> is equal to actual or different from it by less than <c>delta</c>.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public static void AssertNotEquals(QualityCheck qualityCheck, double notExpected, double actual, double delta, string message = null)
        {
            QualityAssert.AreNotEqual(qualityCheck, notExpected, actual, delta, message);
        }

        /// <summary>
        /// Asserts that two specified floats are not equal, and not within the specified accuracy of each other. The assertion fails if they are equal or within the specified accuracy of each other. Displays a message if the assertion fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the assertion.</param>
        /// <param name="notExpected">The first float to compare. This is the double the unit test expects not to match <c>actual</c>.</param>
        /// <param name="actual">The second float to compare. This is the float the unit test produced.</param>
        /// <param name="delta">The required inaccuracy. The assertion fails only if <c>notExpected</c> is equal to actual or different from it by less than <c>delta</c>.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public static void AreNotEqual(QualityCheck qualityCheck, float notExpected, float actual, float delta, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                Assert.AreNotEqual(notExpected, actual, delta, message);
                qualityCheck.SetSuccessfulValues();
            }
            catch (AssertFailedException afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                throw;
            }
        }

        /// <summary>
        /// Asserts that two specified floats are not equal, and not within the specified accuracy of each other. The assertion fails if they are equal or within the specified accuracy of each other. Displays a message if the assertion fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the assertion.</param>
        /// <param name="notExpected">The first float to compare. This is the double the unit test expects not to match <c>actual</c>.</param>
        /// <param name="actual">The second float to compare. This is the float the unit test produced.</param>
        /// <param name="delta">The required inaccuracy. The assertion fails only if <c>notExpected</c> is equal to actual or different from it by less than <c>delta</c>.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public static void AssertNotEquals(QualityCheck qualityCheck, float notExpected, float actual, float delta, string message = null)
        {
            QualityAssert.AreNotEqual(qualityCheck, notExpected, actual, delta, message);
        }

        /// <summary>
        /// Asserts that two specified objects are not equal. The assertion fails if the objects are equal. Displays a message if the assertion fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the assertion.</param>
        /// <param name="notExpected">The first object to compare. This is the object the unit test expects not to match <c>actual</c>.</param>
        /// <param name="actual">The second object to compare. This is the object the unit test produced.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public static void AreNotEqual(QualityCheck qualityCheck, object notExpected, object actual, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                Assert.AreNotEqual(notExpected, actual, message);
                qualityCheck.SetSuccessfulValues();
            }
            catch (AssertFailedException afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                throw;
            }
        }

        /// <summary>
        /// Asserts that two specified objects are not equal. The assertion fails if the objects are equal. Displays a message if the assertion fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the assertion.</param>
        /// <param name="notExpected">The first object to compare. This is the object the unit test expects not to match <c>actual</c>.</param>
        /// <param name="actual">The second object to compare. This is the object the unit test produced.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public static void AssertNotEquals(QualityCheck qualityCheck, object notExpected, object actual, string message = null)
        {
            QualityAssert.AreNotEqual(qualityCheck, notExpected, actual, message);
        }

        /// <summary>
        /// Asserts that two specified strings are not equal, ignoring case or not as specified, and using the culture info specified. The assertion fails if they are equal. Displays a message if the assertion fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the assertion.</param>
        /// <param name="notExpected">The first string to compare. This is the string the unit test expects not to match <c>actual</c>.</param>
        /// <param name="actual">The second string to compare. This is the string the unit test produced.</param>
        /// <param name="ignoreCase">A Boolean value that indicates a case-sensitive or insensitive comparison. <c>true</c> indicates a case-insensitive comparison.</param>
        /// <param name="culture">A <see cref="CultureInfo"/> object that supplies culture-specific comparison information.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public static void AreNotEqual(QualityCheck qualityCheck, string notExpected, string actual, bool ignoreCase, CultureInfo culture = null, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                Assert.AreNotEqual(notExpected, actual, ignoreCase, culture, message);
                qualityCheck.SetSuccessfulValues();
            }
            catch (AssertFailedException afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                throw;
            }
        }

        /// <summary>
        /// Asserts that two specified strings are not equal, ignoring case or not as specified, and using the culture info specified. The assertion fails if they are equal. Displays a message if the assertion fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the assertion.</param>
        /// <param name="notExpected">The first string to compare. This is the string the unit test expects not to match <c>actual</c>.</param>
        /// <param name="actual">The second string to compare. This is the string the unit test produced.</param>
        /// <param name="ignoreCase">A Boolean value that indicates a case-sensitive or insensitive comparison. <c>true</c> indicates a case-insensitive comparison.</param>
        /// <param name="culture">A <see cref="CultureInfo"/> object that supplies culture-specific comparison information.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public static void AssertNotEquals(QualityCheck qualityCheck, string notExpected, string actual, bool ignoreCase, CultureInfo culture = null, string message = null)
        {
            QualityAssert.AreNotEqual(qualityCheck, notExpected, actual, ignoreCase, culture, message);
        }

        /// <summary>
        /// Asserts that two specified generic type data are not equal. The assertion fails if they are equal. Displays a message if the assertion fails.
        /// </summary>
        /// <typeparam name="T">The type of objects to be compared.</typeparam>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the assertion.</param>
        /// <param name="notExpected">The first generic type data to compare. This is the generic type data the unit test expects not to match <c>actual</c>.</param>
        /// <param name="actual">The second generic type data to compare. This is the generic type data the unit test produced.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public static void AreNotEqual<T>(QualityCheck qualityCheck, T notExpected, T actual, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                Assert.AreNotEqual(notExpected, actual, message);
                qualityCheck.SetSuccessfulValues();
            }
            catch (AssertFailedException afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                throw;
            }
        }

        /// <summary>
        /// Asserts that two specified generic type data are not equal. The assertion fails if they are equal. Displays a message if the assertion fails.
        /// </summary>
        /// <typeparam name="T">The type of objects to be compared.</typeparam>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the assertion.</param>
        /// <param name="notExpected">The first generic type data to compare. This is the generic type data the unit test expects not to match <c>actual</c>.</param>
        /// <param name="actual">The second generic type data to compare. This is the generic type data the unit test produced.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public static void AssertNotEquals<T>(QualityCheck qualityCheck, T notExpected, T actual, string message = null)
        {
            QualityAssert.AreNotEqual(qualityCheck, notExpected, actual, message);
        }
        #endregion

        #region AreNotSame
        /// <summary>
        /// Asserts that two specified object variables refer to different objects. The assertion fails if they refer to the same object. Displays a message if the assertion fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the assertion.</param>
        /// <param name="notExpected">The first object to compare. This is the object the unit test expects not to match <c>actual</c>.</param>
        /// <param name="actual">The second object to compare. This is the object the unit test produced.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public static void AreNotSame(QualityCheck qualityCheck, object notExpected, object actual, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                Assert.AreNotSame(notExpected, actual, message);
                qualityCheck.SetSuccessfulValues();
            }
            catch (AssertFailedException afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                throw;
            }
        }

        /// <summary>
        /// Asserts that two specified object variables refer to different objects. The assertion fails if they refer to the same object. Displays a message if the assertion fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the assertion.</param>
        /// <param name="notExpected">The first object to compare. This is the object the unit test expects not to match <c>actual</c>.</param>
        /// <param name="actual">The second object to compare. This is the object the unit test produced.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public static void AssertNotSame(QualityCheck qualityCheck, object notExpected, object actual, string message = null)
        {
            QualityAssert.AreNotSame(qualityCheck, notExpected, actual, message);
        }
        #endregion

        #region AreSame
        /// <summary>
        /// Asserts that two specified object variables refer to the same object. The assertion fails if they refer to different objects. Displays a message if the assertion fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the assertion.</param>
        /// <param name="expected">The first object to compare. This is the object the unit test expects.</param>
        /// <param name="actual">The second object to compare. This is the object the unit test produced.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public static void AreSame(QualityCheck qualityCheck, object expected, object actual, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                Assert.AreSame(expected, actual, message);
                qualityCheck.SetSuccessfulValues();
            }
            catch (AssertFailedException afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                throw;
            }
        }

        /// <summary>
        /// Asserts that two specified object variables refer to the same object. The assertion fails if they refer to different objects. Displays a message if the assertion fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the assertion.</param>
        /// <param name="expected">The first object to compare. This is the object the unit test expects.</param>
        /// <param name="actual">The second object to compare. This is the object the unit test produced.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public static void AssertSame(QualityCheck qualityCheck, object expected, object actual, string message = null)
        {
            QualityAssert.AreSame(qualityCheck, expected, actual, message);
        }
        #endregion

        #region Fail

        /// <summary>
        /// Fails the assertion without checking any condition, and displays a message.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the value of the assertion.</param>
        /// <param name="message">A message to display.</param>
        /// <param name="realCause"></param>
        public static void Fail(QualityCheck qualityCheck, string message = null, Exception realCause = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                if (realCause == null)
                {
                    Assert.Fail(message);
                }
                else
                {
                    Assert.Fail(message, realCause);
                }

            }
            catch (AssertFailedException afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                throw;
            }
        }

        /// <summary>
        /// Fails the assertion without checking any condition, and displays a message.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the value of the assertion.</param>
        /// <param name="message">A message to display.</param>
        /// <param name="realCause"></param>
        public static void AssertFail(QualityCheck qualityCheck, string message = null, Exception realCause = null)
        {
            QualityAssert.Fail(qualityCheck, message, realCause);
        }
        #endregion

        #region Inconclusive
        /// <summary>
        /// Indicates that an assertion cannot be verified, and displays a message.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the value of the assertion.</param>
        /// <param name="message">A message to display.</param>
        public static void Inconclusive(QualityCheck qualityCheck, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                Assert.Inconclusive(message);
            }
            catch (AssertInconclusiveException aie)
            {
                qualityCheck.SetFailedValues(aie.Message);
                throw;
            }
        }

        /// <summary>
        /// Indicates that an assertion cannot be verified, and displays a message.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the value of the assertion.</param>
        /// <param name="message">A message to display.</param>
        public static void AssertInconclusive(QualityCheck qualityCheck, string message = null)
        {
            QualityAssert.Inconclusive(qualityCheck, message);
        }
        #endregion


        #region IsFalse
        /// <summary>
        /// Asserts that the specified condition is <c>false</c>. The assertion fails if the condition is <c>true</c>. Displays a message if the assertion fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the assertion.</param>
        /// <param name="condition">The condition to verify is <c>false</c>.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public static void IsFalse(QualityCheck qualityCheck, bool condition, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                Assert.IsFalse(condition, message);
                qualityCheck.SetSuccessfulValues();
            }
            catch (AssertFailedException afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                throw;
            }
        }

        /// <summary>
        /// Asserts that the specified condition is <c>false</c>. The assertion fails if the condition is <c>true</c>. Displays a message if the assertion fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the assertion.</param>
        /// <param name="condition">The condition to verify is <c>false</c>.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public static void AssertFalse(QualityCheck qualityCheck, bool condition, string message = null)
        {
            QualityAssert.IsFalse(qualityCheck, condition, message);
        }
        #endregion

        #region IsInstanceOfType
        /// <summary>
        /// Asserts that the specified object is an instance of the specified type. The assertion fails if the type is not found in the inheritance hierarchy of the object. Displays a message if the assertion fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the assertion.</param>
        /// <param name="value">The object to verify is of <c>expectedType</c>.</param>
        /// <param name="expectedType">The type expected to be found in the inheritance hierarchy of <c>value</c>.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public static void IsInstanceOfType(QualityCheck qualityCheck, object value, Type expectedType, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                Assert.IsInstanceOfType(value, expectedType, message);
                qualityCheck.SetSuccessfulValues();
            }
            catch (AssertFailedException afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                throw;
            }
        }

        /// <summary>
        /// Asserts that the specified object is an instance of the specified type. The assertion fails if the type is not found in the inheritance hierarchy of the object. Displays a message if the assertion fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the assertion.</param>
        /// <param name="value">The object to verify is of <c>expectedType</c>.</param>
        /// <param name="expectedType">The type expected to be found in the inheritance hierarchy of <c>value</c>.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public static void AssertInstanceOfType(QualityCheck qualityCheck, object value, Type expectedType, string message = null)
        {
            QualityAssert.IsInstanceOfType(qualityCheck, value, expectedType, message);
        }
        #endregion

        #region IsNotInstanceOfType
        /// <summary>
        /// Asserts that the specified object is not an instance of the specified type. The assertion fails if the type is found in the inheritance hierarchy of the object. Displays a message if the assertion fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the assertion.</param>
        /// <param name="value">The object to verify is not of <c>wrongType</c>.</param>
        /// <param name="wrongType">The type that should not be found in the inheritance hierarchy of <c>value</c>.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public static void IsNotInstanceOfType(QualityCheck qualityCheck, object value, Type wrongType, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                Assert.IsNotInstanceOfType(value, wrongType, message);
                qualityCheck.SetSuccessfulValues();
            }
            catch (AssertFailedException afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                throw;
            }
        }

        /// <summary>
        /// Asserts that the specified object is not an instance of the specified type. The assertion fails if the type is found in the inheritance hierarchy of the object. Displays a message if the assertion fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the assertion.</param>
        /// <param name="value">The object to verify is not of <c>wrongType</c>.</param>
        /// <param name="wrongType">The type that should not be found in the inheritance hierarchy of <c>value</c>.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public static void AssertNotInstanceOfType(QualityCheck qualityCheck, object value, Type wrongType, string message = null)
        {
            QualityAssert.IsNotInstanceOfType(qualityCheck, value, wrongType, message);
        }
        #endregion

        #region IsNotNull
        /// <summary>
        /// Asserts that the specified object is not <c>null</c>. The assertion fails if it is <c>null</c>. Displays a message if the assertion fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the assertion.</param>
        /// <param name="value">The object to verify is not <c>null</c>.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public static void IsNotNull(QualityCheck qualityCheck, object value, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                Assert.IsNotNull(value, message);
                qualityCheck.SetSuccessfulValues();
            }
            catch (AssertFailedException afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                throw;
            }
        }

        /// <summary>
        /// Asserts that the specified object is not <c>null</c>. The assertion fails if it is <c>null</c>. Displays a message if the assertion fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the assertion.</param>
        /// <param name="value">The object to verify is not <c>null</c>.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public static void AssertNotNull(QualityCheck qualityCheck, object value, string message = null)
        {
            QualityAssert.IsNotNull(qualityCheck, value, message);
        }
        #endregion

        #region IsNull
        /// <summary>
        /// Asserts that the specified object is <c>null</c>. The assertion fails if it is not <c>null</c>. Displays a message if the assertion fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the assertion.</param>
        /// <param name="value">The object to verify is <c>null</c>.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public static void IsNull(QualityCheck qualityCheck, object value, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                Assert.IsNull(value, message);
                qualityCheck.SetSuccessfulValues();
            }
            catch (AssertFailedException afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                throw;
            }
        }

        /// <summary>
        /// Asserts that the specified object is <c>null</c>. The assertion fails if it is not <c>null</c>. Displays a message if the assertion fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the assertion.</param>
        /// <param name="value">The object to verify is <c>null</c>.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public static void AssertNull(QualityCheck qualityCheck, object value, string message = null)
        {
            QualityAssert.IsNull(qualityCheck, value, message);
        }
        #endregion

        #region IsTrue
        /// <summary>
        /// Asserts that the specified condition is <c>true</c>. The assertion fails if the condition is <c>false</c>. Displays a message if the assertion fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the assertion.</param>
        /// <param name="condition">The condition to verify is <c>true</c>.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public static void IsTrue(QualityCheck qualityCheck, bool condition, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                Assert.IsTrue(condition, message);
                qualityCheck.SetSuccessfulValues();
            }
            catch (AssertFailedException afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                throw;
            }
        }

        /// <summary>
        /// Asserts that the specified condition is <c>true</c>. The assertion fails if the condition is <c>false</c>. Displays a message if the assertion fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the assertion.</param>
        /// <param name="condition">The condition to verify is <c>true</c>.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public static void AssertTrue(QualityCheck qualityCheck, bool condition, string message = null)
        {
            QualityAssert.IsTrue(qualityCheck, condition, message);
        }
        #endregion
    }
}
