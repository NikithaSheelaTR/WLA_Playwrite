namespace Framework.Core.Utils.Enums
{
    using System;

    /// <summary>
    /// Indicates the string value for an enumeration value.
    /// </summary>
    /// <example>
    /// <code>
    /// [StringValue("value")]
    /// EnumValue
    /// </code>
    /// </example>
    public class StringValueAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the StringValueAttribute class.
        /// </summary>
        public StringValueAttribute(string value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets the string value that is paired with the enumeration value.
        /// </summary>
        /// <value>The string value that is paired with the enumeration value.</value>
        public string Value
        {
            get;
            private set;
        }
    }
}
