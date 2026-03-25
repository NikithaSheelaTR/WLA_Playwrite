namespace Framework.Core.Utils.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Extension methods for <see cref="Random"/>
    /// </summary>
    public static class RandomExtensions
    {
        /// <summary>
        /// Returns a collection of unique random numbers in the defined range
        /// </summary>
        /// <param name="random"><see cref="Random"/></param>
        /// <param name="minValue">Minimal value</param>
        /// <param name="maxValue">Maximum value</param>
        /// <param name="requiredCollectionSize">The required size of a collection of unique random numbers</param>
        /// <returns></returns>
        public static IEnumerable<int> GetCollectionOfUniqueNumbers(
            this Random random,
            int minValue,
            int maxValue,
            int requiredCollectionSize)
        {
            if (random == null)
                throw new NullReferenceException(nameof(random));

            if (minValue > maxValue)
                throw new ArgumentOutOfRangeException(
                    $@"The condition '{nameof(minValue)}'<'{nameof(maxValue)}' must be satisfied. 
                                                         \nActual values are : '{nameof(minValue)}'={minValue}, '{nameof
                        (maxValue)}'={maxValue}");

            if (requiredCollectionSize > maxValue - minValue + 1)
                throw new ArgumentOutOfRangeException(
                    nameof(requiredCollectionSize),
                    $@"The argument '{nameof(requiredCollectionSize)}' is out of the expected range, it should be in the range: [0...'{nameof
                        (maxValue)}-{nameof(minValue)}'].
                                                         \nActual value: {requiredCollectionSize}, should be in range: [0...{maxValue
                                                                                                                             - minValue}]");

            HashSet<int> result;

            if (requiredCollectionSize < (maxValue - minValue) / 2)
            {
                result = new HashSet<int>();
                while (result.Count < requiredCollectionSize)
                {
                    result.Add(random.Next(minValue, maxValue));
                }
            }
            else
            {
                result = new HashSet<int>(Enumerable.Range(minValue, maxValue - minValue + 1));
                while (result.Count > requiredCollectionSize)
                {
                    result.Remove(random.Next(minValue, maxValue));
                }
            }

            return result;
        }

        /// <summary>
        /// Generate a random from the given limits
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int GetRandomNumber(int min, int max)
        {
            return new Random().Next(min, max);
        }

        /// <summary>
        /// Generate a random from the given limits
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int GetRandomNumber(int max)
        {
            return GetRandomNumber(0, max);
        }
    }
}