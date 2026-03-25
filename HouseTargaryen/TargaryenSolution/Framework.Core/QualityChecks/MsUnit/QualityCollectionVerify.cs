namespace Framework.Core.QualityChecks.MsUnit
{
    using System;
    using System.Collections;

    using Framework.Core.QualityChecks.Result;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// A set of Collection verification methods useful for writing test cases.
    /// </summary>
    /// <remarks>
    /// A verification does not throw an <see cref="AssertFailedException"/> upon failure, but instead returns a boolean indicating the result of the underlying <see cref="CollectionAssert"/> method.  All <see cref="CollectionAssert"/> methods available in Visual Studio Unit Testing Framework are supported.  Each method requires a <see cref="QualityCheck"/>.  Upon completion of a method, the provided <see cref="QualityCheck"/> will have a date and time associated with the execution time of the method, a <see cref="QualityCheckType"/> of Verification, and a <see cref="Result.Outcome"/> indicating success or failure of the underlying assertion.
    /// </remarks>
    /// <seealso cref="CollectionAssert"/>
    /// <seealso cref="QualityCheck"/>
    /// <seealso cref="Result.Outcome"/>
    /// <seealso cref="QualityCheckType"/>
    public static class QualityCollectionVerify
    {
        private const QualityCheckType Type = QualityCheckType.Verification;


        #region AllItemsAreInstancesOfType
        /// <summary>
        /// Verifies that all elements in the specified collection are instances of the specified type. The verification fails if there exists one element in the collection for which the specified type is not found in its inheritance hierarchy. Displays a message if the verification fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the verification.</param>
        /// <param name="collection">The collection upon which to verify.</param>
        /// <param name="expectedType">The type expected to be found in the inheritance hierarchy of every element in <c>collection</c>.</param>
        /// <param name="message">A message to display if the verification fails. This message can be seen in the unit test results.</param>
        /// <returns>An indication whether the verification does not fail.</returns>
        public static bool AllItemsAreInstancesOfType(QualityCheck qualityCheck, ICollection collection, Type expectedType, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                CollectionAssert.AllItemsAreInstancesOfType(collection, expectedType, message);
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

        #region AllItemsAreNotNull
        /// <summary>
        /// Verifies that all items in the specified collection are not null. The verification fails if any element is null. Displays a message if the verification fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the verification.</param>
        /// <param name="collection">The collection upon which to verify.</param>
        /// <param name="message">A message to display if the verification fails. This message can be seen in the unit test results.</param>
        /// <returns>An indication whether the verification does not fail.</returns>
        public static bool AllItemsAreNotNull(QualityCheck qualityCheck, ICollection collection, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                CollectionAssert.AllItemsAreNotNull(collection, message);
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

        #region AllItemsAreUnique
        /// <summary>
        /// Verifies that all items in the specified collection are unique. The verification fails if any two elements in the collection are equal. Displays a message if the verification fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the verification.</param>
        /// <param name="collection">The collection upon which to verify.</param>
        /// <param name="message">A message to display if the verification fails. This message can be seen in the unit test results.</param>
        /// <returns>An indication whether the verification does not fail.</returns>
        public static bool AllItemsAreUnique(QualityCheck qualityCheck, ICollection collection, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                CollectionAssert.AllItemsAreUnique(collection, message);
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

        #region AreEqual
        /// <summary>
        /// Verifies that two specified collections are equal. The verification fails if the collections are not equal. Displays a message if the verification fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the verification.</param>
        /// <param name="expected">The first collection to compare. This is the collection the unit test expects.</param>
        /// <param name="actual">The second collection to compare. This is the collection the unit test produced.</param>
        /// <param name="message">A message to display if the verification fails. This message can be seen in the unit test results.</param>
        /// <returns>An indication whether the verification does not fail.</returns>
        public static bool AreEqual(QualityCheck qualityCheck, ICollection expected, ICollection actual, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                CollectionAssert.AreEqual(expected, actual, message);
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
        /// Verifies that two specified collections are equal, using the specified method to compare the values of elements. The verification fails if the collections are not equal. Displays a message if the verification fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the verification.</param>
        /// <param name="expected">The first collection to compare. This is the collection the unit test expects.</param>
        /// <param name="actual">The second collection to compare. This is the collection the unit test produced.</param>
        /// <param name="comparer">The compare implementation to use when comparing elements of the collection.</param>
        /// <param name="message">A message to display if the verification fails. This message can be seen in the unit test results.</param>
        /// <returns>An indication whether the verification does not fail.</returns>
        public static bool AreEqual(QualityCheck qualityCheck, ICollection expected, ICollection actual, IComparer comparer, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);

            try
            {
                CollectionAssert.AreEqual(expected, actual, comparer, message);
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

        #region AreEquivalent
        /// <summary>
        /// Verifies that two specified collections are equivalent. The verification fails if the collections are not equivalent. Displays a message if the verification fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the verification.</param>
        /// <param name="expected">The first collection to compare. This is the collection the unit test expects.</param>
        /// <param name="actual">The second collection to compare. This is the collection the unit test produced.</param>
        /// <param name="message">A message to display if the verification fails. This message can be seen in the unit test results.</param>
        /// <returns>An indication whether the verification does not fail.</returns>
        public static bool AreEquivalent(QualityCheck qualityCheck, ICollection expected, ICollection actual, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                CollectionAssert.AreEquivalent(expected, actual, message);
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
        /// Verifies that two specified collections are not equal. The verification fails if the collections are equal. Displays a message if the verification fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the verification.</param>
        /// <param name="expected">The first collection to compare. This is the collection the unit test expects.</param>
        /// <param name="actual">The second collection to compare. This is the collection the unit test produced.</param>
        /// <param name="message">A message to display if the verification fails. This message can be seen in the unit test results.</param>
        /// <returns>An indication whether the verification does not fail.</returns>
        public static bool AreNotEqual(QualityCheck qualityCheck, ICollection expected, ICollection actual, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                CollectionAssert.AreNotEqual(expected, actual, message);
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
        /// Verifies that two specified collections are not equal, using the specified method to compare the values of elements. The verification fails if the collections are equal. Displays a message if the verification fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the verification.</param>
        /// <param name="expected">The first collection to compare. This is the collection the unit test expects.</param>
        /// <param name="actual">The second collection to compare. This is the collection the unit test produced.</param>
        /// <param name="comparer">The compare implementation to use when comparing elements of the collection.</param>
        /// <param name="message">A message to display if the verification fails. This message can be seen in the unit test results.</param>
        /// <returns>An indication whether the verification does not fail.</returns>
        public static bool AreNotEqual(QualityCheck qualityCheck, ICollection expected, ICollection actual, IComparer comparer, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                CollectionAssert.AreNotEqual(expected, actual, comparer, message);
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

        #region AreNotEquivalent
        /// <summary>
        /// Verifies that two specified collections are not equivalent. The verification fails if the collections are equivalent. Displays a message if the verification fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the verification.</param>
        /// <param name="expected">The first collection to compare. This is the collection the unit test expects.</param>
        /// <param name="actual">The second collection to compare. This is the collection the unit test produced.</param>
        /// <param name="message">A message to display if the verification fails. This message can be seen in the unit test results.</param>
        /// <returns>An indication whether the verification does not fail.</returns>
        public static bool AreNotEquivalent(QualityCheck qualityCheck, ICollection expected, ICollection actual, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                CollectionAssert.AreNotEquivalent(expected, actual, message);
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

        #region Contains
        /// <summary>
        /// Verifies that the specified collection contains the specified element. The verification fails if the element is not found in the collection. Displays a message if the verification fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the verification.</param>
        /// <param name="collection">The collection upon which to verify.</param>
        /// <param name="element">The element that is expected to be in the collection.</param>
        /// <param name="message">A message to display if the verification fails. This message can be seen in the unit test results.</param>
        /// <returns>An indication whether the verification does not fail.</returns>
        public static bool Contains(QualityCheck qualityCheck, ICollection collection, object element, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                CollectionAssert.Contains(collection, element, message);
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

        #region DoesNotContain
        /// <summary>
        /// Verifies that the specified collection does not contain the specified element. The verification fails if the element is found in the collection. Displays a message if the verification fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the verification.</param>
        /// <param name="collection">The collection upon which to verify.</param>
        /// <param name="element">The element that is not expected to be in the collection.</param>
        /// <param name="message">A message to display if the verification fails. This message can be seen in the unit test results.</param>
        /// <returns>An indication whether the verification does not fail.</returns>
        public static bool DoesNotContain(QualityCheck qualityCheck, ICollection collection, object element, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                CollectionAssert.DoesNotContain(collection, element, message);
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

        #region IsNotSubsetOf
        /// <summary>
        /// Verifies that the first collection is not a subset of the second collection. Displays a message if the verification fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the verification.</param>
        /// <param name="subset">The collection not expected to be a subset of <c>superset</c></param>
        /// <param name="superset">The collection not expected to be a superset of <c>subset</c>.</param>
        /// <param name="message">A message to display if the verification fails. This message can be seen in the unit test results.</param>
        /// <returns>An indication whether the verification does not fail.</returns>
        public static bool IsNotSubsetOf(QualityCheck qualityCheck, ICollection subset, ICollection superset, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                CollectionAssert.IsNotSubsetOf(subset, superset, message);
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

        #region IsSubsetOf
        /// <summary>
        /// Verifies that the first collection is a subset of the second collection. Displays a message if the verification fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the verification.</param>
        /// <param name="subset">The collection expected to be a subset of <c>superset</c></param>
        /// <param name="superset">The collection expected to be a superset of <c>subset</c>.</param>
        /// <param name="message">A message to display if the verification fails. This message can be seen in the unit test results.</param>
        /// <returns>An indication whether the verification does not fail.</returns>
        public static bool IsSubsetOf(QualityCheck qualityCheck, ICollection subset, ICollection superset, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                CollectionAssert.IsSubsetOf(subset, superset, message);
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