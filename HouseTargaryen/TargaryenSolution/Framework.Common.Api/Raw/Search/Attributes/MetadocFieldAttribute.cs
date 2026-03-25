namespace Framework.Common.Api.Raw.Search.Attributes
{
    using Framework.Core.CommonTypes.Attributes;

    /// <summary>
    /// Attributes for the Metadoc Field enum
    /// </summary>
    public class MetadocFieldAttribute : ExtendedEnumAttribute
    {
        /// <summary>
        /// the name of the Metadoc Field to be retrieved
        /// </summary>
        public string MetadocFieldName { get; set; }
    }
}