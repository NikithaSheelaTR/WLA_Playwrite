namespace Framework.Core.CommonTypes.Attributes
{
    using System;

    /// <summary>
    /// Base attribute to extend enumeration values.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public abstract class ExtendedEnumAttribute : Attribute
    {
    }
}