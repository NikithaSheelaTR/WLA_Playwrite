namespace Framework.Core.QualityChecks.MsUnit
{
    using System;
    using System.Collections;

    using Framework.Core.QualityChecks.Result;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// A set of Collection assertion methods useful for writing test cases.
    /// </summary>
    /// <remarks>
    /// All <see cref="CollectionAssert"/> methods available in Visual Studio Unit Testing Framework are supported.  Each method requires a <see cref="QualityCheck"/>.  Upon completion of a method, the provided <see cref="QualityCheck"/> will have a date and time associated with the execution time of the method, a <see cref="QualityCheckType"/> of Assertion, and a <see cref="Result.Outcome"/> indicating success or failure of the underlying assertion.
    /// </remarks>
    /// <seealso cref="CollectionAssert"/>
    /// <seealso cref="QualityCheck"/>
    /// <seealso cref="Result.Outcome"/>
    /// <seealso cref="QualityCheckType"/>
    public static class QualityCollectionAssert
    {
        private const QualityCheckType Type = QualityCheckType.Assertion;


        #region AllItemsAreInstancesOfType
        /// <summary>
        /// Asserts that all elements in the specified collection are instances of the specified type. The assertion fails if there exists one element in the collection for which the specified type is not found in its inheritance hierarchy. Displays a message if the assertion fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the assertion.</param>
        /// <param name="collection">The collection upon which to assert.</param>
        /// <param name="expectedType">The type expected to be found in the inheritance hierarchy of every element in <c>collection</c>.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public static void AllItemsAreInstancesOfType(QualityCheck qualityCheck, ICollection collection, Type expectedType, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                CollectionAssert.AllItemsAreInstancesOfType(collection, expectedType, message);
                qualityCheck.SetSuccessfulValues();
            }
            catch (AssertFailedException afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                throw;
            }
        }
        #endregion


        #region AllItemsAreNotNull
        /// <summary>
        /// Asserts that all items in the specified collection are not null. The assertion fails if any element is null. Displays a message if the assertion fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the assertion.</param>
        /// <param name="collection">The collection upon which to assert.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public static void AllItemsAreNotNull(QualityCheck qualityCheck, ICollection collection, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);

            try
            {
                CollectionAssert.AllItemsAreNotNull(collection, message);
                qualityCheck.SetSuccessfulValues();
            }
            catch (AssertFailedException afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                throw;
            }
        }
        #endregion


        #region AllItemsAreUnique
        /// <summary>
        /// Asserts that all items in the specified collection are unique. The assertion fails if any two elements in the collection are equal. Displays a message if the assertion fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the assertion.</param>
        /// <param name="collection">The collection upon which to assert.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public static void AllItemsAreUnique(QualityCheck qualityCheck, ICollection collection, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                CollectionAssert.AllItemsAreUnique(collection, message);
                qualityCheck.SetSuccessfulValues();
            }
            catch (AssertFailedException afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                throw;
            }
        }
        #endregion


        #region AreEqual
        /// <summary>
        /// Asserts that two specified collections are equal. The assertion fails if the collections are not equal. Displays a message if the assertion fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the assertion.</param>
        /// <param name="expected">The first collection to compare. This is the collection the unit test expects.</param>
        /// <param name="actual">The second collection to compare. This is the collection the unit test produced.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public static void AreEqual(QualityCheck qualityCheck, ICollection expected, ICollection actual, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                CollectionAssert.AreEqual(expected, actual, message);
                qualityCheck.SetSuccessfulValues();
            }
            catch (AssertFailedException afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                throw;
            }
        }


        /// <summary>
        /// Asserts that two specified collections are equal, using the specified method to compare the values of elements. The assertion fails if the collections are not equal. Displays a message if the assertion fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the assertion.</param>
        /// <param name="expected">The first collection to compare. This is the collection the unit test expects.</param>
        /// <param name="actual">The second collection to compare. This is the collection the unit test produced.</param>
        /// <param name="comparer">The compare implementation to use when comparing elements of the collection.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public static void AreEqual(QualityCheck qualityCheck, ICollection expected, ICollection actual, IComparer comparer, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                CollectionAssert.AreEqual(expected, actual, comparer, message);
                qualityCheck.SetSuccessfulValues();
            }
            catch (AssertFailedException afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                throw;
            }
        }
        #endregion


        #region AreEquivalent
        /// <summary>
        /// Asserts that two specified collections are equivalent. The assertion fails if the collections are not equivalent. Displays a message if the assertion fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the assertion.</param>
        /// <param name="expected">The first collection to compare. This is the collection the unit test expects.</param>
        /// <param name="actual">The second collection to compare. This is the collection the unit test produced.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public static void AreEquivalent(QualityCheck qualityCheck, ICollection expected, ICollection actual, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                CollectionAssert.AreEquivalent(expected, actual, message);
                qualityCheck.SetSuccessfulValues();
            }
            catch (AssertFailedException afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                throw;
            }
        }
        #endregion


        #region AreNotEqual
        /// <summary>
        /// Asserts that two specified collections are not equal. The assertion fails if the collections are equal. Displays a message if the assertion fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the assertion.</param>
        /// <param name="expected">The first collection to compare. This is the collection the unit test expects.</param>
        /// <param name="actual">The second collection to compare. This is the collection the unit test produced.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public static void AreNotEqual(QualityCheck qualityCheck, ICollection expected, ICollection actual, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                CollectionAssert.AreNotEqual(expected, actual, message);
                qualityCheck.SetSuccessfulValues();
            }
            catch (AssertFailedException afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                throw;
            }
        }


        /// <summary>
        /// Asserts that two specified collections are not equal, using the specified method to compare the values of elements. The assertion fails if the collections are equal. Displays a message if the assertion fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the assertion.</param>
        /// <param name="expected">The first collection to compare. This is the collection the unit test expects.</param>
        /// <param name="actual">The second collection to compare. This is the collection the unit test produced.</param>
        /// <param name="comparer">The compare implementation to use when comparing elements of the collection.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public static void AreNotEqual(QualityCheck qualityCheck, ICollection expected, ICollection actual, IComparer comparer, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                CollectionAssert.AreNotEqual(expected, actual, comparer, message);
                qualityCheck.SetSuccessfulValues();
            }
            catch (AssertFailedException afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                throw;
            }
        }
        #endregion


        #region AreNotEquivalent
        /// <summary>
        /// Asserts that two specified collections are not equivalent. The assertion fails if the collections are equivalent. Displays a message if the assertion fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the assertion.</param>
        /// <param name="expected">The first collection to compare. This is the collection the unit test expects.</param>
        /// <param name="actual">The second collection to compare. This is the collection the unit test produced.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public static void AreNotEquivalent(QualityCheck qualityCheck, ICollection expected, ICollection actual, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                CollectionAssert.AreNotEquivalent(expected, actual, message);
                qualityCheck.SetSuccessfulValues();
            }
            catch (AssertFailedException afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                throw;
            }
        }
        #endregion


        #region Contains
        /// <summary>
        /// Asserts that the specified collection contains the specified element. The assertion fails if the element is not found in the collection. Displays a message if the assertion fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the assertion.</param>
        /// <param name="collection">The collection upon which to assert.</param>
        /// <param name="element">The element that is expected to be in the collection.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public static void Contains(QualityCheck qualityCheck, ICollection collection, object element, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                CollectionAssert.Contains(collection, element, message);
                qualityCheck.SetSuccessfulValues();
            }
            catch (AssertFailedException afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                throw;
            }
        }
        #endregion


        #region DoesNotContain
        /// <summary>
        /// Asserts that the specified collection does not contain the specified element. The assertion fails if the element is found in the collection. Displays a message if the assertion fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the assertion.</param>
        /// <param name="collection">The collection upon which to assert.</param>
        /// <param name="element">The element that is not expected to be in the collection.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public static void DoesNotContain(QualityCheck qualityCheck, ICollection collection, object element, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                CollectionAssert.DoesNotContain(collection, element, message);
                qualityCheck.SetSuccessfulValues();
            }
            catch (AssertFailedException afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                throw;
            }
        }

        #endregion


        #region IsNotSubsetOf
        /// <summary>
        /// Asserts that the first collection is not a subset of the second collection. Displays a message if the assertion fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the assertion.</param>
        /// <param name="subset">The collection not expected to be a subset of <c>superset</c></param>
        /// <param name="superset">The collection not expected to be a superset of <c>subset</c>.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public static void IsNotSubsetOf(QualityCheck qualityCheck, ICollection subset, ICollection superset, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                CollectionAssert.IsNotSubsetOf(subset, superset, message);
                qualityCheck.SetSuccessfulValues();
            }
            catch (AssertFailedException afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                throw;
            }
        }
        #endregion


        #region IsSubsetOf
        /// <summary>
        /// Asserts that the first collection is a subset of the second collection. Displays a message if the assertion fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the assertion.</param>
        /// <param name="subset">The collection expected to be a subset of <c>superset</c></param>
        /// <param name="superset">The collection expected to be a superset of <c>subset</c>.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public static void IsSubsetOf(QualityCheck qualityCheck, ICollection subset, ICollection superset, string message = null)
        {
            qualityCheck.SetPreConditionValues(Type);
            try
            {
                CollectionAssert.IsSubsetOf(subset, superset, message);
                qualityCheck.SetSuccessfulValues();
            }
            catch (AssertFailedException afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                throw;
            }
        }
        #endregion
    }
}