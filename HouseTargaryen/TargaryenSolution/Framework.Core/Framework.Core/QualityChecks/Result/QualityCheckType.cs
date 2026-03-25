namespace Framework.Core.QualityChecks.Result
{
    /// <summary>
    /// Enumerates all valid Quality Check types.
    /// </summary>
    public enum QualityCheckType
    {
        /// <summary>
        /// Indicates a quality check type of assertion.
        /// </summary>
        [Utils.Enums.StringValue("Assertion")]
        Assertion,

        /// <summary>
        /// Indicates a quality check type of verification.
        /// </summary>
        [Utils.Enums.StringValue("Verification")]
        Verification,

        /// <summary>
        /// Indicates a quality check type of not applicable.
        /// </summary>
        [Utils.Enums.StringValue("Not Applicable")]
        NotApplicable
    }
}
