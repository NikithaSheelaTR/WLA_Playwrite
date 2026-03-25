namespace Framework.Common.UI.Products.TaxnetPro.Dialog
{
    using Framework.Core.Utils.Enums;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.TaxnetPro.Enums.NewTypeahead;
    using Framework.Common.UI.Products.TaxnetPro.Components.NewTypeahead;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.FocusHighlighting;
    using Framework.Common.UI.Raw.WestlawEdge.Models.FocusHighlighting;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using System;
    using System.Linq;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;

    /// <summary>
    /// New Typeahead Dialog Winwow
    /// </summary>
    public class TNPNewTypeaheadDialog : BaseModuleRegressionDialog
    {
        /// <summary>
        /// Container Locator
        /// </summary>
        private static readonly By ContainerLocator = By.Id("coid_trDiscover");
        private static readonly By PayPerViewSignLocator = By.XPath("//span[text()='Pay-per-view Document']");
        private const string HighlightLctMask = "//span[@class='co_searchTerm' and contains(.,'{0}')]";

        private EnumPropertyMapper<TermColors, FocusHighlightingTermInfo> termColorMap;

        /// <summary>
        /// Cases Component
        /// </summary>
        public TaxnewsComponent TaxnewsComponent
            => this.LeftMenu.SelectContentType<TaxnewsComponent>(TNPNewTypeaheadContentType.Taxnews);

        /// <summary>
        /// Cases Component
        /// </summary>
        public AnswerPathComponent AnswerPathComponent
            => this.LeftMenu.SelectContentType<AnswerPathComponent>(TNPNewTypeaheadContentType.AnswerPath);

        /// <summary>
        /// Cases Component
        /// </summary>
        public CaseLawComponent CaseLawComponent
            => this.LeftMenu.SelectContentType<CaseLawComponent>(TNPNewTypeaheadContentType.CaseLaw);

        /// <summary>
        /// Cases Component
        /// </summary>
        public CRAViewsComponent CRAViewsComponent
            => this.LeftMenu.SelectContentType<CRAViewsComponent>(TNPNewTypeaheadContentType.CRAViews);

        /// <summary>
        /// Legislation Component
        /// </summary>
        public LegislationComponent LegislationComponent
            => this.LeftMenu.SelectContentType<LegislationComponent>(TNPNewTypeaheadContentType.Legislation);

        /// <summary>
        /// The Left Menu
        /// </summary>
        public LeftPanelContentTypesComponent LeftMenu = new LeftPanelContentTypesComponent();

        /// <summary>
        /// Gets the TermColors enumeration to FocusHighlightingTermInfo map.
        /// </summary>
        protected EnumPropertyMapper<TermColors, FocusHighlightingTermInfo> TermColorMap =>
            this.termColorMap = this.termColorMap
                                ?? EnumPropertyModelCache.GetMap<TermColors, FocusHighlightingTermInfo>(
                                    string.Empty,
                                    @"Resources/EnumPropertyMaps/WestlawEdge/FocusHighlighting");
        
        /// <summary>
        /// Get color type by a code
        /// </summary>
        /// <param name="termCode"> Term color rgb code </param>
        /// <returns> Term color </returns>
        protected TermColors GetColorTypeByCode(string termCode) =>
            Enum.GetValues(typeof(TermColors)).Cast<TermColors>().First(
                color => this.TermColorMap[color].BackgroundColorCode.Equals(termCode));

        /// <summary>
        /// Get hightlighted terms color by text
        /// </summary>
        /// <param name="terms"></param>
        /// <returns></returns>
        public Dictionary<TermColors, bool> GetTermsColorByText(string terms)
        {
            var dividedTerms = terms.Split(' ');
            List<TermColors> termColors = new List<TermColors>();
            Dictionary<TermColors, bool> termColorsDictionary = new Dictionary<TermColors, bool>();
            
            foreach (var term in dividedTerms)
            {
                termColors.Add(GetTermColorByText(term));
            }

            bool areTermColorsSame = termColors.All(t => t == termColors[0]);

            termColorsDictionary.Add(termColors[0], areTermColorsSame);

            return termColorsDictionary;
        }

        /// <summary>
        /// Get highlighted term color by term text
        /// </summary>
        /// <param name="term"> The term text </param>
        /// <returns> Term color </returns>
        public TermColors GetTermColorByText(string term) =>
            this.GetColorTypeByCode(
                DriverExtensions.GetElement(By.XPath(string.Format(HighlightLctMask, term)))
                                .GetCssValue("background-color"));

        /// <summary>
        /// Is Dollar sign displayed for out of plan documents
        /// </summary>
        /// <returns></returns>
        public ILabel PayPerViewSignLabel => new Label(ContainerLocator, PayPerViewSignLocator);

        /// <summary>
        /// Is displayed method.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsDisplayed() => DriverExtensions.IsDisplayed(ContainerLocator, 5);
    }
}
