namespace Framework.Common.UI.Products.WestlawEdge.Components.FocusHighlighting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.FocusHighlighting;
    using Framework.Common.UI.Raw.WestlawEdge.Models.FocusHighlighting;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Focus highlihgt seleted terms component
    /// </summary>
    public class FocusHighlightSelectedTermsComponent : BaseModuleRegressionComponent
    {
        private const string TermLctMask = @"//label[text() = '{0}']//parent::span[contains(@class, 'TermButton')]";
        private static readonly By ContainerLocator = By.XPath("//div[@id = 'coid_focusHighlightSelectedTerms']");
		private static readonly By ShowTermDensityHeatmapToggleLocator = By.XPath("//button[@id='SlideToggle_focusHighlightToggleHeatmap']");
		private static readonly By SelectedTermLocator = By.XPath(".//div[@class = 'co_focusHighlightSelectedTerms']//span[contains(@class, 'TermButton')]");
        private static readonly By InputTermLocator = By.XPath(".//input");
        private static readonly By LabelTermLocator = By.XPath("./label");
        private static readonly By SlideToggleBarLocator = By.XPath(".//preceding-sibling::div[@id = 'coid_focusHighlightToggleHeatmap']//div[@class = 'SlideToggle-thumb-container']");
		private static readonly By SlideToggleContentLocator = By.XPath(".//preceding-sibling::div[@id = 'coid_focusHighlightToggleHeatmap']//div[@class = 'SlideToggle-content']");

		/// <summary>
		/// The term color map.
		/// </summary>
		private EnumPropertyMapper<TermColors, FocusHighlightingTermInfo> termColorMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Component container
        /// </summary>
        private IWebElement Container => DriverExtensions.GetElement(this.ComponentLocator);

        /// <summary>
        /// Gets the TermColors enumeration to FocusHighlightingTermInfo map.
        /// </summary>
        private EnumPropertyMapper<TermColors, FocusHighlightingTermInfo> TermColorMap =>
            this.termColorMap = this.termColorMap
                                ?? EnumPropertyModelCache.GetMap<TermColors, FocusHighlightingTermInfo>(
                                    string.Empty,
                                    @"Resources/EnumPropertyMaps/WestlawEdge/FocusHighlighting");

        /// <summary>
        /// Is term displayed
        /// </summary>
        /// <param name="termName">Term name</param>
        /// <returns>True if term is displayed, false otherwise.</returns>
        public bool IsTermDisplayed(string termName) => DriverExtensions.IsDisplayed(By.XPath(string.Format(TermLctMask, termName)), 2);

        /// <summary>
        /// The 'show density heatmap' toggle button displayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsHeatmapToggleDisplayed() =>
              DriverExtensions.IsDisplayed(this.Container, SlideToggleBarLocator);

        /// <summary>
        /// Check 'Show term density heatmap' toggle button
        /// </summary>
        /// <param name="showHeatMap">Show/hide heat map</param>
        public void CheckHeatmapToggle(bool showHeatMap)
        {
            if (!showHeatMap.Equals(this.IsHeatmapToggleChecked()))
            {
                DriverExtensions.Click(DriverExtensions.GetElement(this.Container, ShowTermDensityHeatmapToggleLocator));
            }
        }

        /// <summary>
        /// Is heatmap toggle checked
        /// </summary>
        public bool IsHeatmapToggleChecked() =>
                DriverExtensions.GetElement(this.Container, ShowTermDensityHeatmapToggleLocator).GetAttribute("aria-pressed") != null;

        /// <summary>
        /// Is term enabled
        /// </summary>
        public bool IsTermEnabled(string termName)
           => !DriverExtensions.GetAttribute("class", DriverExtensions.GetElement(By.XPath(string.Format(TermLctMask, termName)))).Contains("termNotFound");

        /// <summary>
        /// Select a term by name
        /// </summary>
        /// <param name="termName">Term name.</param>
        /// <param name="selected">Select/ deselect a term by name.</param>
        public void SelectTermByName(string termName, bool selected = true)
            => DriverExtensions.GetElement(By.XPath(string.Format(TermLctMask, termName)), InputTermLocator).SetCheckbox(selected);

        /// <summary>
        /// Select a term by name
        /// </summary>
        /// <typeparam name="T">Page type</typeparam>
        /// <param name="termName">Term name.</param>
        /// <param name="selected">Select/deselect a term by name.</param>
        public T SelectTermByName<T>(string termName, bool selected = true) where T : ICreatablePageObject
        {
            DriverExtensions.GetElement(By.XPath(string.Format(TermLctMask, termName)), InputTermLocator).SetCheckbox(selected);

            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Get selected terms titles
        /// </summary>
        /// <returns>List of selected terms titles.</returns>
        public List<string> GetSelectedTermsTitles() =>
           DriverExtensions.GetElements(this.Container, SelectedTermLocator, LabelTermLocator).Select(label => label.Text).ToList();

        /// <summary>
        /// Get toggle content text
        /// </summary>
        /// <returns>Toggle content text</returns>
        public string GetToggleContentText() => DriverExtensions.GetElement(this.Container, SlideToggleContentLocator).Text;

        /// <summary>
        /// Returns term color by term name
        /// </summary>
        /// <param name="termName">Term name</param>
        /// <returns>Term color</returns>
        public TermColors GetTermColor(string termName)
        => this.GetColorTypeByCode(DriverExtensions.GetElement(SafeXpath.BySafeXpath(string.Format(TermLctMask, termName))).GetCssValue("background-color"));

        /// <summary>
        /// Get color type by a code
        /// </summary>
        /// <param name="termCode">Term color rgb code</param>
        /// <returns>Term color</returns>
        private TermColors GetColorTypeByCode(string termCode) =>
            Enum.GetValues(typeof(TermColors))
            .Cast<TermColors>()
            .First(color => this.TermColorMap[color].BackgroundColorCode.Equals(termCode));
    }
}
