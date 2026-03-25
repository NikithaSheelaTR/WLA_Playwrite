namespace Framework.Core.Utils.Enums
{
    /// <summary>
    /// Base enum attribute, used for adding enum extensions
    /// </summary>
    public interface IEnumAttributeBase
    {
        /// <summary>
        /// returns the string value of the enum
        /// </summary>
        /// <returns>the string value of the enum</returns>
        string GetValue();
    }
}
