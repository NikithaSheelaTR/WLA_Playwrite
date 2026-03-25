namespace Framework.Core.Utils.Extensions
{
    /// <summary>
    /// Object extensions.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Gets the hash code of the object even if it is NULL.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <returns>The hash code.</returns>
        public static int GetSafeHashCode<T>(this T obj)
        {
            int typeHashCode = typeof(T).GetHashCode();

            return obj == null ? typeHashCode : obj.GetHashCode() ^ typeHashCode;
        }
    }
}