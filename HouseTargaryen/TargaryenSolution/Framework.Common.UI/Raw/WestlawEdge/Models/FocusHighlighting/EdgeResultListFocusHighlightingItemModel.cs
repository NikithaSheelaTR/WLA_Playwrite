namespace Framework.Common.UI.Raw.WestlawEdge.Models.FocusHighlighting
{
    using System.Collections.Generic;

    using Framework.Common.UI.Raw.WestlawEdge.Enums.FocusHighlighting;

    /// <summary>
    /// Indigo result list focus highlighing item model
    /// </summary>
    public class EdgeResultListFocusHighlightingItemModel : EdgeCasesResultListItemModel
    {
        /// <summary>
        /// Get term-color pair
        /// </summary>
        public Dictionary<string, TermColors> TermsColors { get; set; }
    }
}
