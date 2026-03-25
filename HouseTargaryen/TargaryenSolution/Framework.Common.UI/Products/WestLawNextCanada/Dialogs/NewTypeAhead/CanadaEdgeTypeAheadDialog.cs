namespace Framework.Common.UI.Products.WestLawNextCanada.Dialogs.NewTypeAhead
{
    using Framework.Common.UI.Products.WestlawEdge.Components.NewTypeAhead;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.NewTypeAhead;
    using Framework.Common.UI.Products.WestLawNextCanada.Components.NewTypeAhead;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.NewTypeAhead;

    /// <summary>
    /// Type ahead dialog in Canada Edge
    /// </summary>
    public class CanadaEdgeTypeAheadDialog : TrdTypeAheadDialog
    {
        /// <summary>
        /// Legal Topics component
        /// </summary>
        public LegalTopicsComponent LegalTopicsComponent
            => this.LeftMenu.SelectContentType<LegalTopicsComponent>(NewTypeAheadContentType.LegalTopics);

        /// <summary>
        /// Cases and Decisions Component
        /// </summary>
        public CasesComponent CasesAndDecisionsComponent
            => this.LeftMenu.SelectContentType<CasesComponent>(NewTypeAheadContentType.CasesAndDecisions);

    }
}