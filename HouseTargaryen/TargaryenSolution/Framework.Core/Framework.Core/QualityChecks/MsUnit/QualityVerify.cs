namespace Framework.Core.QualityChecks.MsUnit
{
    using System;
    using System.Globalization;

    using Framework.Core.QualityChecks.Result;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// A set of verification methods useful for writing test cases.
    /// </summary>
    /// <remarks>
    /// A verification does not throw an <see cref="AssertFailedException"/> upon failure, but instead returns a boolean indicating the result of the underlying <see cref="Assert"/> method.  All <see cref="Assert"/> methods available in Visual Studio Unit Testing Framework are supported.  Each method requires a <see cref="QualityCheck"/>.  Upon completion of a method, the provided <see cref="QualityCheck"/> will have a date and time associated with the execution time of the method, a <see cref="QualityCheckType"/> of Verification, and a <see cref="Result.Outcome"/> indicating success or failure of the underlying assertion.
    /// </remarks>
    /// <seealso cref="Assert"/>
    /// <seealso cref="QualityCheck"/>
    /// <seealso cref="Result.Outcome"/>
    /// <seealso cref="QualityCheckType"/>
    public static class QualityVerify
    {
        private const QualityCheckType Type = QualityCheckType.Verification;

        #region AreEqual
        /// <summary>
        /// Verifies that two specified doubles are equal, or within the specified accuracy of each other. The verification fails if they are not within the specified accuracy of each other. Displays a message if the verification fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the verification.</param>
        /// <param name="expected">The first double to compare. This is the double the unit test expects.</param>
        /// <param name="actual">The second double to compare. This is the double the unit test produced.</param>
        /// <param name="delta">The required accuracy. The verification will fail only if <c>expected</c> is different from <c>actual</c> by more than <c>delta</c>.</param>
        /// <param name="message">A message to display if the verification fails. This message can be seen in the unit test results.</param>
        /// <returns>An indication whether the verification does not fail.</returns>
        public static bool AreEqual(QualityCheck qualityCheck, double expected, double actual, double delta, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                Assert.AreEqual(expected, actual, delta, message);
                qualityCheck.SetSuccessfulValues();
                return true;
            }
            catch (AssertFailedException afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                return false;
            }
        }


        /// <summary>
        /// Verifies that two specified floats are equal, or within the specified accuracy of each other. The verification fails if they are not within the specified accuracy of each other. Displays a message if the verification fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the verification.</param>
        /// <param name="expected">The first float to compare. This is the single the unit test expects.</param>
        /// <param name="actual">The float single to compare. This is the single the unit test produced.</param>
        /// <param name="delta">The required accuracy. The verification will fail only if <c>expected</c> is different from <c>actual</c> by more than <c>delta</c>.</param>
        /// <param name="message">A message to display if the verification fails. This message can be seen in the unit test results.</param>
        /// <returns>An indication whether the verification does not fail.</returns>
        public static bool AreEqual(QualityCheck qualityCheck, float expected, float actual, float delta, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                Assert.AreEqual(expected, actual, delta, message);
                qualityCheck.SetSuccessfulValues();
                return true;
            }
            catch (AssertFailedException afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                return false;
            }
        }


        /// <summary>
        /// Verifies that two specified objects are equal. The verification fails if the objects are not equal. Displays a message if the verification fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the verification.</param>
        /// <param name="expected">The first object to compare. This is the object the unit test expects.</param>
        /// <param name="actual">The second object to compare. This is the object the unit test produced.</param>
        /// <param name="message">A message to display if the verification fails. This message can be seen in the unit test results.</param>
        /// <returns>An indication whether the verification does not fail.</returns>
        public static bool AreEqual(QualityCheck qualityCheck, object expected, object actual, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                Assert.AreEqual(expected, actual, message);
                qualityCheck.SetSuccessfulValues();
                return true;
            }
            catch (AssertFailedException afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                return false;
            }
        }


        /// <summary>
        /// Verifies that two specified strings are equal, ignoring case or not as specified, and using the culture info specified. The verification fails if they are not equal. Displays a message if the verification fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the verification.</param>
        /// <param name="expected">The first string to compare. This is the string the unit test expects.</param>
        /// <param name="actual">The second string to compare. This is the string the unit test produced.</param>
        /// <param name="ignoreCase">A Boolean value that indicates a case-sensitive or insensitive comparison. <c>true</c> indicates a case-insensitive comparison.</param>
        /// <param name="culture">A <see cref="CultureInfo"/> object that supplies culture-specific comparison information.</param>
        /// <param name="message">A message to display if the verification fails. This message can be seen in the unit test results.</param>
        /// <returns>An indication whether the verification does not fail.</returns>
        public static bool AreEqual(QualityCheck qualityCheck, string expected, string actual, bool ignoreCase, CultureInfo culture = null, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                Assert.AreEqual(expected, actual, ignoreCase, culture, message);
                qualityCheck.SetSuccessfulValues();
                return true;
            }
            catch (AssertFailedException afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                return false;
            }
        }


        /// <summary>
        /// Verifies that two specified generic type data are equal by using the equality operator. The verification fails if they are not equal. Displays a message if the verification fails.
        /// </summary>
        /// <typeparam name="T">The type of objects to be compared.</typeparam>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the verification.</param>
        /// <param name="expected">The first generic type data to compare. This is the generic type data the unit test expects.</param>
        /// <param name="actual">The second generic type data to compare. This is the generic type data the unit test produced.</param>
        /// <param name="message">A message to display if the verification fails. This message can be seen in the unit test results.</param>
        /// <returns>An indication whether the verification does not fail.</returns>
        public static bool AreEqual<T>(QualityCheck qualityCheck, T expected, T actual, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                Assert.AreEqual(expected, actual, message);
                qualityCheck.SetSuccessfulValues();
                return true;
            }
            catch (AssertFailedException afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                return false;
            }
        }

        #endregion


        #region AreNotEqual
        /// <summary>
        /// Verifies that two specified doubles are not equal, and not within the specified accuracy of each other. The verification fails if they are equal or within the specified accuracy of each other. Displays a message if the verification fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the verification.</param>
        /// <param name="notExpected">The first double to compare. This is the double the unit test expects not to match <c>actual</c>.</param>
        /// <param name="actual">The second double to compare. This is the double the unit test produced.</param>
        /// <param name="delta">The required inaccuracy. The verification fails only if <c>notExpected</c> is equal to actual or different from it by less than <c>delta</c>.</param>
        /// <param name="message">A message to display if the verification fails. This message can be seen in the unit test results.</param>
        /// <returns>An indication whether the verification does not fail.</returns>
        public static bool AreNotEqual(QualityCheck qualityCheck, double notExpected, double actual, double delta, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                Assert.AreNotEqual(notExpected, actual, delta, message);
                qualityCheck.SetSuccessfulValues();
                return true;
            }
            catch (AssertFailedException afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                return false;
            }
        }


        /// <summary>
        /// Verifies that two specified floats are not equal, and not within the specified accuracy of each other. The verification fails if they are equal or within the specified accuracy of each other. Displays a message if the verification fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the verification.</param>
        /// <param name="notExpected">The first float to compare. This is the double the unit test expects not to match <c>actual</c>.</param>
        /// <param name="actual">The second float to compare. This is the float the unit test produced.</param>
        /// <param name="delta">The required inaccuracy. The verification fails only if <c>notExpected</c> is equal to actual or different from it by less than <c>delta</c>.</param>
        /// <param name="message">A message to display if the verification fails. This message can be seen in the unit test results.</param>
        /// <returns>An indication whether the verification does not fail.</returns>
        public static bool AreNotEqual(QualityCheck qualityCheck, float notExpected, float actual, float delta, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                Assert.AreNotEqual(notExpected, actual, delta, message);
                qualityCheck.SetSuccessfulValues();
                return true;
            }
            catch (AssertFailedException afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                return false;
            }
        }


        /// <summary>
        /// Verifies that two specified objects are not equal. The verification fails if the objects are equal. Displays a message if the verification fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the verification.</param>
        /// <param name="notExpected">The first object to compare. This is the object the unit test expects not to match <c>actual</c>.</param>
        /// <param name="actual">The second object to compare. This is the object the unit test produced.</param>
        /// <param name="message">A message to display if the verification fails. This message can be seen in the unit test results.</param>
        /// <returns>An indication whether the verification does not fail.</returns>
        public static bool AreNotEqual(QualityCheck qualityCheck, object notExpected, object actual, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                Assert.AreNotEqual(notExpected, actual, message);
                qualityCheck.SetSuccessfulValues();
                return true;
            }
            catch (AssertFailedException afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                return false;
            }
        }


        /// <summary>
        /// Verifies that two specified strings are not equal, ignoring case or not as specified, and using the culture info specified. The verification fails if they are equal. Displays a message if the verification fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the verification.</param>
        /// <param name="notExpected">The first string to compare. This is the string the unit test expects not to match <c>actual</c>.</param>
        /// <param name="actual">The second string to compare. This is the string the unit test produced.</param>
        /// <param name="ignoreCase">A Boolean value that indicates a case-sensitive or insensitive comparison. <c>true</c> indicates a case-insensitive comparison.</param>
        /// <param name="culture">A <see cref="CultureInfo"/> object that supplies culture-specific comparison information.</param>
        /// <param name="message">A message to display if the verification fails. This message can be seen in the unit test results.</param>
        /// <returns>An indication whether the verification does not fail.</returns>
        public static bool AreNotEqual(QualityCheck qualityCheck, string notExpected, string actual, bool ignoreCase, CultureInfo culture = null, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                Assert.AreNotEqual(notExpected, actual, ignoreCase, culture, message);
                qualityCheck.SetSuccessfulValues();
                return true;
            }
            catch (AssertFailedException afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                return false;
            }
        }


        /// <summary>
        /// Verifies that two specified generic type data are not equal. The verification fails if they are equal. Displays a message if the verification fails.
        /// </summary>
        /// <typeparam name="T">The type of objects to be compared.</typeparam>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the verification.</param>
        /// <param name="notExpected">The first generic type data to compare. This is the generic type data the unit test expects not to match <c>actual</c>.</param>
        /// <param name="actual">The second generic type data to compare. This is the generic type data the unit test produced.</param>
        /// <param name="message">A message to display if the verification fails. This message can be seen in the unit test results.</param>
        /// <returns>An indication whether the verification does not fail.</returns>
        public static bool AreNotEqual<T>(QualityCheck qualityCheck, T notExpected, T actual, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                Assert.AreNotEqual(notExpected, actual, message);
                qualityCheck.SetSuccessfulValues();
                return true;
            }
            catch (AssertFailedException afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                return false;
            }
        }

        #endregion


        #region AreNotSame
        /// <summary>
        /// Verifies that two specified object variables refer to different objects. The verification fails if they refer to the same object. Displays a message if the verification fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the verification.</param>
        /// <param name="notExpected">The first object to compare. This is the object the unit test expects not to match <c>actual</c>.</param>
        /// <param name="actual">The second object to compare. This is the object the unit test produced.</param>
        /// <param name="message">A message to display if the verification fails. This message can be seen in the unit test results.</param>
        /// <returns>An indication whether the verification does not fail.</returns>
        public static bool AreNotSame(QualityCheck qualityCheck, object notExpected, object actual, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                Assert.AreNotSame(notExpected, actual, message);
                qualityCheck.SetSuccessfulValues();
                return true;
            }
            catch (AssertFailedException afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                return false;
            }
        }

        #endregion


        #region AreSame
        /// <summary>
        /// Verifies that two specified object variables refer to the same object. The verification fails if they refer to different objects. Displays a message if the verification fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the verification.</param>
        /// <param name="expected">The first object to compare. This is the object the unit test expects.</param>
        /// <param name="actual">The second object to compare. This is the object the unit test produced.</param>
        /// <param name="message">A message to display if the verification fails. This message can be seen in the unit test results.</param>
        /// <returns>An indication whether the verification does not fail.</returns>
        public static bool AreSame(QualityCheck qualityCheck, object expected, object actual, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                Assert.AreSame(expected, actual, message);
                qualityCheck.SetSuccessfulValues();
                return true;
            }
            catch (AssertFailedException afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                return false;
            }
        }

        #endregion


        #region IsFalse
        /// <summary>
        /// Verifies that the specified condition is <c>false</c>. The verification fails if the condition is <c>true</c>. Displays a message if the verification fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the verification.</param>
        /// <param name="condition">The condition to verify is <c>false</c>.</param>
        /// <param name="message">A message to display if the verification fails. This message can be seen in the unit test results.</param>
        /// <returns>An indication whether the verification does not fail.</returns>
        public static bool IsFalse(QualityCheck qualityCheck, bool condition, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                Assert.IsFalse(condition, message);
                qualityCheck.SetSuccessfulValues();
                return true;
            }
            catch (AssertFailedException afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                return false;
            }
        }

        #endregion


        #region IsInstanceOfType
        /// <summary>
        /// Verifies that the specified object is an instance of the specified type. The verification fails if the type is not found in the inheritance hierarchy of the object. Displays a message if the verification fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the verification.</param>
        /// <param name="value">The object to verify is of <c>expectedType</c>.</param>
        /// <param name="expectedType">The type expected to be found in the inheritance hierarchy of <c>value</c>.</param>
        /// <param name="message">A message to display if the verification fails. This message can be seen in the unit test results.</param>
        /// <returns>An indication whether the verification does not fail.</returns>
        public static bool IsInstanceOfType(QualityCheck qualityCheck, object value, Type expectedType, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                Assert.IsInstanceOfType(value, expectedType, message);
                qualityCheck.SetSuccessfulValues();
                return true;
            }
            catch (AssertFailedException afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                return false;
            }
        }

        #endregion


        #region IsNotInstanceOfType
        /// <summary>
        /// Verifies that the specified object is not an instance of the specified type. The verification fails if the type is found in the inheritance hierarchy of the object. Displays a message if the verification fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the verification.</param>
        /// <param name="value">The object to verify is not of <c>wrongType</c>.</param>
        /// <param name="wrongType">The type that should not be found in the inheritance hierarchy of <c>value</c>.</param>
        /// <param name="message">A message to display if the verification fails. This message can be seen in the unit test results.</param>
        /// <returns>An indication whether the verification does not fail.</returns>
        public static bool IsNotInstanceOfType(QualityCheck qualityCheck, object value, Type wrongType, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                Assert.IsNotInstanceOfType(value, wrongType, message);
                qualityCheck.SetSuccessfulValues();
                return true;
            }
            catch (AssertFailedException afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                return false;
            }
        }

        #endregion


        #region IsNotNull
        /// <summary>
        /// Verifies that the specified object is not <c>null</c>. The verification fails if it is <c>null</c>. Displays a message if the verification fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the verification.</param>
        /// <param name="value">The object to verify is not <c>null</c>.</param>
        /// <param name="message">A message to display if the verification fails. This message can be seen in the unit test results.</param>
        /// <returns>An indication whether the verification does not fail.</returns>
        public static bool IsNotNull(QualityCheck qualityCheck, object value, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                Assert.IsNotNull(value, message);
                qualityCheck.SetSuccessfulValues();
                return true;
            }
            catch (AssertFailedException afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                return false;
            }
        }

        #endregion


        #region IsNull
        /// <summary>
        /// Verifies that the specified object is <c>null</c>. The verification fails if it is not <c>null</c>. Displays a message if the verification fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the verification.</param>
        /// <param name="value">The object to verify is <c>null</c>.</param>
        /// <param name="message">A message to display if the verification fails. This message can be seen in the unit test results.</param>
        /// <returns>An indication whether the verification does not fail.</returns>
        public static bool IsNull(QualityCheck qualityCheck, object value, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                Assert.IsNull(value, message);
                qualityCheck.SetSuccessfulValues();
                return true;
            }
            catch (AssertFailedException afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                return false;
            }
        }

        #endregion


        #region IsTrue
        /// <summary>
        /// Verifies that the specified condition is <c>true</c>. The verification fails if the condition is <c>false</c>. Displays a message if the verification fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the verification.</param>
        /// <param name="condition">The condition to verify is <c>true</c>.</param>
        /// <param name="message">A message to display if the verification fails. This message can be seen in the unit test results.</param>
        /// <returns>An indication whether the verification does not fail.</returns>
        public static bool IsTrue(QualityCheck qualityCheck, bool condition, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                Assert.IsTrue(condition, message);
                qualityCheck.SetSuccessfulValues();
                return true;
            }
            catch (AssertFailedException afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                return false;
            }
        }

        #endregion
    }
}
