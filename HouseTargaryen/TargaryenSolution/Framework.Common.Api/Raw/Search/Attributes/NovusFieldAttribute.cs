namespace Framework.Common.Api.Raw.Search.Attributes
{
    using Framework.Core.CommonTypes.Attributes;

    /// <summary>
    /// Attributes for the Novus Field enum
    /// </summary>
    public class NovusFieldAttribute : ExtendedEnumAttribute
    {
        /// <summary>
        /// the Novus field name
        /// </summary>
        public string FieldName { get; set; }
    }
}