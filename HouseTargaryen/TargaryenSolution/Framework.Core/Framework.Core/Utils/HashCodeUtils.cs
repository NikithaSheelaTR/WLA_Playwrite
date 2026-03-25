namespace Framework.Core.Utils
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Adapted from source code at <a href="http://www.javapractices.com">Java Practices</a>, and contains a collected set of methods which allow for easy implementation of <c>GetHashCode</c>.
    /// </summary>
    /// <remarks>
    /// Example use case:
    /// <code>
    /// public override int GetHashCode()
    /// {
    ///     int result = HashCodeUtils.Seed;
    ///     
    ///     // collect the contributions of various fields
    ///     result = HashCodeUtils.Hash(result, this.fPrimitive);
    ///     result = HashCodeUtils.Hash(result, this.fObject);
    ///     result = HashCodeUtils.Hash(result, this.fArray);
    ///     return result;
    /// }
    /// </code>
    /// </remarks>
    public static class HashCodeUtils
    {
        private const int OddPrimeNumber = 37;


        private static int FirstTerm(int aSeed)
        {
            return OddPrimeNumber * aSeed;
        }


        private static bool IsArray(object aObject)
        {
            return aObject.GetType().IsArray;
        }


        /// <summary>
        /// An initial value for a <c>hashCode</c>, to which is added contributions from fields. Using a non-zero value decreases collisions of <c>hashCode</c> values.
        /// </summary>
        public const int Seed = 29;


        /// <summary>
        /// Hashes for booleans.    
        /// </summary>
        /// <param name="aSeed">The seed value.</param>
        /// <param name="aBool">The boolean value to be hashed.</param>
        /// <returns>A hash code.</returns>
        public static int Hash(int aSeed, bool aBool)
        {
            return HashCodeUtils.FirstTerm(aSeed) + (aBool ? 1 : 0);
        }


        /// <summary>
        /// Hashes for characters.
        /// </summary>
        /// <param name="aSeed">The seed value.</param>
        /// <param name="aChar">The character value to be hashed.</param>
        /// <returns>A hash code.</returns>
        public static int Hash(int aSeed, char aChar)
        {
            return HashCodeUtils.FirstTerm(aSeed) + (int)aChar;
        }


        /// <summary>
        /// Hashes for integers.
        /// </summary>
        /// <param name="aSeed">The seed value.</param>
        /// <param name="anInt">The integer value to be hashed.</param>
        /// <returns>A hash code.</returns>
        public static int Hash(int aSeed, int anInt)
        {
            return HashCodeUtils.FirstTerm(aSeed) + anInt;
        }


        /// <summary>
        /// Hashes for longs.
        /// </summary>
        /// <param name="aSeed">A seed value.</param>
        /// <param name="aLong">The long value to be hashed.</param>
        /// <returns>A hash code.</returns>
        public static int Hash(int aSeed, long aLong)
        {
            return HashCodeUtils.FirstTerm(aSeed) + (int)(aLong ^ (aLong >> 32));
        }


        /// <summary>
        /// Hashes for floats.
        /// </summary>
        /// <param name="aSeed">A seed value.</param>
        /// <param name="aFloat">The float value to be hashed.</param>
        /// <returns>A hash code.</returns>
        public static int Hash(int aSeed, float aFloat)
        {
            return HashCodeUtils.Hash(aSeed, BitConverter.DoubleToInt64Bits(aFloat));
        }


        /// <summary>
        /// Hashes for doubles.
        /// </summary>
        /// <param name="aSeed">A seed value.</param>
        /// <param name="aDouble">A double value to be hashed.</param>
        /// <returns>A hash code.</returns>
        public static int Hash(int aSeed, double aDouble)
        {
            return HashCodeUtils.Hash(aSeed, BitConverter.DoubleToInt64Bits(aDouble));
        }


        /// <summary>
        /// Hashes an enumerated object.
        /// </summary>
        /// <typeparam name="T">The type of objects contained within the enumerable.</typeparam>
        /// <param name="aSeed">A seed value.</param>
        /// <param name="anEnumerable">The enumerable to be hashed.</param>
        /// <returns>A hash code.</returns>
        public static int Hash<T>(int aSeed, IEnumerable<T> anEnumerable)
        {
            int result = aSeed;
            if (anEnumerable == null)
            {
                result = HashCodeUtils.Hash(result, 0);
            }
            else
            {
                foreach (var item in anEnumerable)
                {
                    result = HashCodeUtils.Hash(result, item);
                }
                return result;
            }
            return result;
        }


        /// <summary>
        /// Hashes for objects.
        /// </summary>
        /// <param name="aSeed">A seed value.</param>
        /// <param name="aObject">The object to be hashed.</param>
        /// <returns>A hash code.</returns>
        public static int Hash(int aSeed, object aObject)
        {
            int result = aSeed;
            result = (aObject == null) ? HashCodeUtils.Hash(result, 0) : HashCodeUtils.Hash(result, aObject.GetHashCode());
            return result;
        }
    }
}
