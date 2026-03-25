namespace Framework.Common.UI.Products.Shared.Enums.Core
{
    /// <summary>
    /// Options for search text
    /// </summary>
    public enum TextSearchOption
    {
        /// <summary>
        /// Partial match
        /// </summary>
        PartialMatch,

        /// <summary>
        /// Ignore case
        /// </summary>
        IgnoreCase,

        /// <summary>
        /// Regular expression
        /// </summary>
        RegularExpression,

        /// <summary>
        /// Normalize whitespace
        /// </summary>
        NormalizeWhitespace,

        /// <summary>
        /// Ignore punctuation
        /// </summary>
        IgnorePunctuation,

        /// <summary>
        /// Hidden text
        /// </summary>
        HiddenText,

        /// <summary>
        /// Immediate text
        /// </summary>
        ImmediateText
    }
}