namespace Framework.Common.UI.Products.WestLawNext.Models
{
    using System;

    /// <summary>
    /// Enumeration describes variants of verifications for full text search functionality 
    /// </summary>
    [Flags]
    public enum SnippetVerificationResults
    {
        /// <summary>
        /// The none.
        /// </summary>
        None = 0x0,

        /// <summary>
        /// The contains at least one highlighted term.
        /// </summary>
        ContainsAtLeastOneHighlightedTerm = 0x1,

        /// <summary>
        /// The contains correct terms.
        /// </summary>
        ContainsCorrectTerms = 0x2,

        /// <summary>
        /// The contains exact match.
        /// </summary>
        ContainsExactMatch = 0x4,

        /// <summary>
        /// The is of correct form type.
        /// </summary>
        IsOfCorrectFormType = 0x8
    }
}