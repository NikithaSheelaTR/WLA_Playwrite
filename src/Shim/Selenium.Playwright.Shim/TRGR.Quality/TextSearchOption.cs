namespace TRGR.Quality.QedArsenal.QualityLibrary.Core.Enums.WebDriver
{
    /// <summary>
    /// Options for text search operations.
    /// </summary>
    public enum TextSearchOption
    {
        /// <summary>
        /// Search using the text content of the element.
        /// </summary>
        Text,

        /// <summary>
        /// Search using the inner HTML of the element.
        /// </summary>
        InnerHtml,

        /// <summary>
        /// Search using the value attribute of the element.
        /// </summary>
        Value,

        /// <summary>
        /// Match the text exactly (case-sensitive).
        /// </summary>
        ExactMatch,

        /// <summary>
        /// Match text using contains logic.
        /// </summary>
        Contains,

        /// <summary>
        /// Trim whitespace before comparing.
        /// </summary>
        TrimWhitespace,

        /// <summary>
        /// Ignore case when comparing.
        /// </summary>
        IgnoreCase
    }
}
