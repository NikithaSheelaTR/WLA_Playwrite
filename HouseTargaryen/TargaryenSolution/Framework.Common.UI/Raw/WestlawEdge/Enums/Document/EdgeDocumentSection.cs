namespace Framework.Common.UI.Raw.WestlawEdge.Enums.Document
{
    /// <summary>
    /// Edge document sections, that do not match WLN
    /// </summary>
    public enum EdgeDocumentSection
    {
        /// <summary>
        /// The concurring opinion.
        /// </summary>
        ConcurringOpinion,

        /// <summary>
        /// The concurring in part opinion.
        /// </summary>
        ConcurringInPartOpinion,

        /// <summary>
        /// Content block(Exists only for StatutesAndCourtRules content type)
        /// </summary>
        ContentBlock,

        /// <summary>
        /// The dissenting opinion.
        /// </summary>
        DissentingOpinion,

        /// <summary>
        /// The dissenting in part opinion.
        /// </summary>
        DissentingInPartOpinion
    }
}