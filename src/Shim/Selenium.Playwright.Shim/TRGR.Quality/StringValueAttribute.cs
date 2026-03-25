using System;

namespace TRGR.Quality.QedArsenal.QualityLibrary.Core.Utils.Enums
{
    /// <summary>
    /// Indicates the string value for an enumeration value.
    /// Mirrors Framework.Core.Utils.Enums.StringValueAttribute for QedArsenal compatibility.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class StringValueAttribute : Attribute
    {
        public StringValueAttribute(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }
    }
}
