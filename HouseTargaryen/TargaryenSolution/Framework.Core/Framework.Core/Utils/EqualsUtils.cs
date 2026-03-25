namespace Framework.Core.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Adapted from source code at <a href="http://www.javapractices.com">Java Practices</a>, and contains a collected set of methods which allow for easy implementation of <c>Equals</c>.
    /// </summary>
    /// <remarks>
    /// Example use case for a class called <c>Car</c>:
    /// <code>
    /// public override bool Equals(Object aThat)
    /// {
    ///     if (aThat == null || GetType() != aThat.GetType())
    ///     {
    ///         return false;
    ///     }
    /// 
    ///     // cast to native object is now safe
    ///     Car that = (Car) aThat;
    /// 
    ///     // now a proper field-by-field evaluation can be made
    ///     return EqualsUtils.AreEqual(this.Name, that.Name) &amp;&amp; EqualsUtils.AreEqual(this.NumDoors, that.NumDoors) &amp;&amp; EqualsUtils.AreEqual(this.GasMileage, that.GasMileage)
    ///             &amp;&amp; EqualsUtils.AreEqual(this.Color, that.Color) &amp;&amp; EqualsUtils.AreEqual(this.MaintenanceChecks, that.MaintenanceChecks);
    /// }
    /// </code>
    /// </remarks>
    public static class EqualsUtils
    {
        /// <summary>
        /// Performs an <c>Equals</c> comparison of two booleans.
        /// </summary>
        /// <param name="aThis">The first boolean to compare.</param>
        /// <param name="aThat">The second boolean to compare.</param>
        /// <returns>An indication whether the two booleans are equal.</returns>
        public static bool AreEqual(bool aThis, bool aThat)
        {
            return aThis == aThat;
        }


        /// <summary>
        /// Performs an <c>Equals</c> comparison of two characters.
        /// </summary>
        /// <param name="aThis">The first character to compare.</param>
        /// <param name="aThat">The second character to compare.</param>
        /// <returns>An indication whether the two characters are equal.</returns>
        public static bool AreEqual(char aThis, char aThat)
        {
            return aThis == aThat;
        }


        /// <summary>
        /// Performs an <c>Equals</c> comparison of two longs.
        /// </summary>
        /// <param name="aThis">The first long to compare.</param>
        /// <param name="aThat">The second long to compare.</param>
        /// <returns>An indication whether the two longs are equal.</returns>
        public static bool AreEqual(long aThis, long aThat)
        {
            return aThis == aThat;
        }


        /// <summary>
        /// Performs an <c>Equals</c> comparison of two floats.
        /// </summary>
        /// <param name="aThis">The first float to compare.</param>
        /// <param name="aThat">The second float to compare.</param>
        /// <returns>An indication whether the two floats are equal.</returns>
        public static bool AreEqual(float aThis, float aThat)
        {
            return BitConverter.DoubleToInt64Bits(aThis) == BitConverter.DoubleToInt64Bits(aThat);
        }


        /// <summary>
        /// Performs an <c>Equals</c> comparison of two doubles.
        /// </summary>
        /// <param name="aThis">The first double to compare.</param>
        /// <param name="aThat">The second double to compare.</param>
        /// <returns>An indication whether the two doubles are equal.</returns>
        public static bool AreEqual(double aThis, double aThat)
        {
            return BitConverter.DoubleToInt64Bits(aThis) == BitConverter.DoubleToInt64Bits(aThat);
        }


        /// <summary>
        /// Performs an <c>Equals</c> comparison of two lists. Equality implies the lists will only be equal if they are the same size and contain the same items.
        /// </summary>
        /// <typeparam name="T">The type of object contained in both lists.</typeparam>
        /// <param name="aThis">The first list to compare.</param>
        /// <param name="aThat">The second list to compare.</param>
        /// <param name="isSameOrderRequired">If <c>true</c>, the lists will only be equal if they are the same size and contain the same items in the same order.</param>
        /// <returns>An indication whether the two lists are equal.</returns>
        public static bool AreEqual<T>(IList<T> aThis, IList<T> aThat, bool isSameOrderRequired = true)
        {
            // determine if both are valued or both are null
            if ((aThis == null) != (aThat == null))
            {
                return false;
            }

            // determine if both are the same object
            if (!object.ReferenceEquals(aThis, aThat))
            {
                // if not same object, do they have the same count?
                if (aThis.Count != aThat.Count)
                {
                    return false;
                }

                // if same length, then compare value-by-value
                if (isSameOrderRequired)
                {
                    return !aThis.Where((t, index) => !EqualsUtils.AreEqual(t, aThat[index])).Any();
                }
                return aThis.All(aThat.Contains) && aThat.All(aThis.Contains);
            }

            // same object
            return true;
        }


        /// <summary>
        /// Performs an <c>Equals</c> comparsion of objects.
        /// </summary>
        /// <param name="aThis">The first object to compare.</param>
        /// <param name="aThat">The second object to compare.</param>
        /// <returns>An indication whether the objects are equal.</returns>
        public static bool AreEqual(Object aThis, Object aThat)
        {
            return aThis == null ? aThat == null : aThis.Equals(aThat);
        }
    }
}
