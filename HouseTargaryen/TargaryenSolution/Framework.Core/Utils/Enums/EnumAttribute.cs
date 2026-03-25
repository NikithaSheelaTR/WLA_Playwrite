namespace Framework.Core.Utils.Enums
{
    using System;

    /// <summary>
    /// A base attribute type for Cobalt testing attributes.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public abstract class EnumAttribute : Attribute
    {
    }
}
