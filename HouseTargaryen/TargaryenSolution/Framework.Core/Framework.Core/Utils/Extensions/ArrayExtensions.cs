namespace Framework.Core.Utils.Extensions
{
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;

    /// <summary>
    /// The array extensions.
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// The are equal.
        /// </summary>
        /// <param name="firstArray">
        /// The first array.
        /// </param>
        /// <param name="secondArray">
        /// The second array.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool AreEqual(byte[] firstArray, byte[] secondArray)
        {
            if (object.ReferenceEquals(firstArray, secondArray))
            {
                return true;
            }

            if (firstArray == null || secondArray == null)
            {
                return false;
            }

            // Validate buffers are the same length.
            // This also ensures that the count does not exceed the length of either buffer.  
            return firstArray.Length == secondArray.Length
                   && ArrayExtensions.memcmp(firstArray, secondArray, firstArray.Length) == 0;
        }

        [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter",
            Justification = "Reviewed. Suppression is OK here.")]
        private static extern int memcmp(byte[] b1, byte[] b2, long count);
    }
}