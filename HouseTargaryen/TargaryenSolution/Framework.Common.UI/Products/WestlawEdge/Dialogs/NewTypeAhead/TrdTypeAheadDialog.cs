namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.NewTypeAhead
{
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.WestlawEdge.Components.NewTypeAhead;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.NewTypeAhead;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The trd type ahead dialog.
    /// </summary>
    public class TrdTypeAheadDialog : BaseModuleRegressionDialog
    {
        private static readonly By ContainerLocator = By.Id("coid_trDiscover");

        /// <summary>
        /// Suggestion component
        /// </summary>
        public virtual SuggestionsComponent SuggestionsComponent
            => this.LeftMenu.SelectContentType<SuggestionsComponent>(NewTypeAheadContentType.Suggestions);

        /// <summary>
        /// Cases Component
        /// </summary>
        public CasesComponent CasesComponent
            => this.LeftMenu.SelectContentType<CasesComponent>(NewTypeAheadContentType.Cases);

        /// <summary>
        /// Legislation Component
        /// </summary>
        public LegislationComponent LegislationComponent
            => this.LeftMenu.SelectContentType<LegislationComponent>(NewTypeAheadContentType.Legislation);

        /// <summary>
        /// Statutes Component
        /// </summary>
        public StatutesAndCourtRulesComponent StatutesAndCourtRulesComponent
            => this.LeftMenu.SelectContentType<StatutesAndCourtRulesComponent>(NewTypeAheadContentType.StatutesAndCourtRules);

        /// <summary>
        /// Secondary Sources Component
        /// </summary>
        public SecondarySourcesComponent SecondarySourcesComponent
            => this.LeftMenu.SelectContentType<SecondarySourcesComponent>(NewTypeAheadContentType.SecondarySources);

        /// <summary>
        /// The regulations component.
        /// </summary>
        public RegulationsComponent RegulationsComponent
           => this.LeftMenu.SelectContentType<RegulationsComponent>(NewTypeAheadContentType.Regulations);

        /// <summary>
        /// Other Component
        /// </summary>
        public OtherComponent OtherComponent
            => this.LeftMenu.SelectContentType<OtherComponent>(NewTypeAheadContentType.Other);

        /// <summary>
        /// Other Component
        /// </summary>
        public GovRegComponent GovRegComponent
            => this.LeftMenu.SelectContentType<GovRegComponent>(NewTypeAheadContentType.GovernmentAndRegulatoryMaterials);

        /// <summary>
        /// The Left Menu
        /// </summary>
        public LeftPaneContentTypesComponent LeftMenu = new LeftPaneContentTypesComponent();

        /// <summary>
        /// The is displayed.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsDisplayed() => DriverExtensions.IsDisplayed(ContainerLocator, 5);
    }
}