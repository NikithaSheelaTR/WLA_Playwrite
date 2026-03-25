namespace Framework.Core.CommonTypes.Enums
{
    /// <summary>
    /// These are the tabs on the search home page
    /// </summary>
    public enum KeyCiteFlag
    {
        /// <summary>
        /// Blue KeyCite Flag (new flag for docket appeal notification)
        /// </summary>
        Blue,

        /// <summary>
        /// Blue KeyCite History Flag (KeyCite history available)
        /// </summary>
        BlueH,

        /// <summary>
        /// Court Docs available
        /// </summary>
        CourtDocs,

        /// <summary>
        /// Gray KeyCite "Flag" (not sure if this is used anywhere besides Inline KeyCite flags anymore)
        /// </summary>
        Gray,

        /// <summary>
        /// KeyCite citing references available
        /// </summary>
        GreenC,

        /// <summary>
        /// Implied Overruling Flag
        /// </summary>
        ImpliedOverruling,

        /// <summary>
        /// Legal Memos Available Flag
        /// </summary>
        LegalMemo,

        /// <summary>
        /// Legal Memoranda Available Flag
        /// </summary>
        LegalMemoranda,

        /// <summary>
        /// No KeyCite Flag ("white flag" or not "KeyCiteable")
        /// </summary>
        NoFlag,

        /// <summary>
        /// Red KeyCite Flag
        /// </summary>
        Red,

        /// <summary>
        /// Red KeyCite Flag
        /// </summary>
        RedF,

        /// <summary>
        /// White-Red-White KeyCite Flag - Negative Treatment
        /// </summary>
        Striped,

        /// <summary>
        /// White-Red-White KeyCite Flag - Negative Treatment for popup
        /// </summary>
        PopupStriped,

        /// <summary>
        /// Yellow KeyCite Flag - Negative Treatment
        /// </summary>
        Yellow,

        /// <summary>
        /// Yellow KeyCite Flag - Negative Treatment
        /// </summary>
        YellowF
    }
}