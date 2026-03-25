namespace Framework.Common.UI.Products.WestlawEdgePremium.Dialogs
{   
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.NewTypeAhead;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.Typeahead;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.NewTypeAhead;

    /// <summary>
    /// The Precision type ahead dialog.
    /// </summary>
    public class PrecisionGlobalTypeAheadDialog : TrdTypeAheadDialog
    {
        /// <summary>
        /// Suggestion component
        /// </summary>
        public new PrecisionSuggestionsComponent SuggestionsComponent
            => this.LeftMenu.SelectContentType<PrecisionSuggestionsComponent>(NewTypeAheadContentType.PrecisionSuggestions);
    }
}
