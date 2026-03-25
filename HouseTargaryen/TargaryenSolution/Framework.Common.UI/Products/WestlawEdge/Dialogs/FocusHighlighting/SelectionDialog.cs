namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.FocusHighlighting
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Toggles;
    using Framework.Common.UI.Products.Shared.Elements.WrapperEements.InfoBox;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.FocusHighlighting;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// New Focus Highlighting Widget
    /// </summary>
    public class SelectionDialog : BaseFocusHighlightingDialog
    {
        private const string TermTitleLctMask = ".//label[text() = '{0}']//parent::span[contains(@class, 'TermButton')]";
        private const string ToggleLctMask = ".//div[@class = 'SlideToggle'][{0}]";

        private static readonly By ToggleStateLocator = By.XPath(".//button[contains(@id,'SlideToggle_')]");
        private static readonly By ChangeColorButtonLocator = By.XPath(".//li[@class = 'co_colorConfigChangeColors']/a | .//li[@class = 'co_colorConfigChangeColors']/button");
        private static readonly By WarningMessageContainerLocator = By.XPath("//div[contains(@class, 'co_maximumSearchTermsErrorMessage')]");
        private static readonly By TermCheckboxLocator = By.XPath(".//input");
        private static readonly By TermLocator = By.XPath("//div[@class = 'co_focusHighlightTermList']//span[contains(@class, 'TermButton')]");
        private static readonly By NewBadgeLocator = By.XPath(".//span[contains(@class, 'Badge')]");

        /// <summary>
        /// 'Only show search results snippets with selected terms' toggle
        /// </summary>
        public IToggle ShowSnippetsWithSelectedTermsToggle =>
            new Toggle(this.Container, By.XPath(string.Format(ToggleLctMask, 2)), ToggleStateLocator, "aria-pressed", "true");

        /// <summary>
        /// 'Remove default highlighting' toggle
        /// </summary>
        public IToggle RemoveDefaultHighlightingToggle =>
            new Toggle(this.Container, By.XPath(string.Format(ToggleLctMask, 1)), ToggleStateLocator, "checked", "true");

        /// <summary>
        /// Change color button
        /// </summary>
        public IButton ChangeColorButton => new CustomEdgeButton(this.Container, ChangeColorButtonLocator);

        /// <summary>
        /// Maximum search terms info-box
        /// </summary>
        public IInfoBox InfoBox => new InfoBox(WarningMessageContainerLocator);

        /// <summary>
        /// New badge of 'Only show search results snippets with selected terms' toggle
        /// </summary>
        public ILabel NewBadge =>
            new Label(
                DriverExtensions.GetElement(this.Container, By.XPath(string.Format(ToggleLctMask, 2))),
                NewBadgeLocator);

        /// <summary>
        /// List of Terms
        /// </summary>
        protected override List<IWebElement> ListOfTerms => DriverExtensions.GetElements(TermLocator).ToList();

        /// <summary>
        /// Returns term color by term name
        /// </summary>
        /// <param name="termName">Term name</param>
        /// <returns>Term color</returns>
        public override TermColors GetTermColor(string termName) =>
            this.GetColorTypeByCode(
                DriverExtensions.GetElement(this.Container, By.XPath(string.Format(TermTitleLctMask, termName)))
                                .GetCssValue("background-color"));

        /// <summary>
        /// Select term by term name
        /// </summary>
        /// <param name="selected">
        /// Select/deselect a term
        /// </param>
        /// <param name="termNames">
        /// Term name
        /// </param>
        public void SelectTermsByName(bool selected, params string[] termNames)
        {
            foreach (string termName in termNames)
            {
                DriverExtensions.GetElement(this.ListOfTerms.First(x => x.Text.Equals(termName)), TermCheckboxLocator)
                                .SetCheckbox(selected);
            }

            // Hovers Apply button to stop focus on the term
            this.ApplyButton.Hover();
        }

        /// <summary>
        /// Select a term by index
        /// </summary>
        /// <param name="termIndex">Term index.</param>
        /// <param name="selected">Select/deselect a term.</param>
        public void SelectTermByIndex(int termIndex, bool selected = true)
        {
            DriverExtensions.GetElement(this.ListOfTerms.ElementAt(termIndex), TermCheckboxLocator)
                            .SetCheckbox(selected);

            // Hovers Apply button to stop focusing on the term
            this.ApplyButton.Hover();
        }

        /// <summary>
        /// Select several terms started from the first
        /// </summary>
        /// <param name="numberOfTerms">Number of terms to select/deselect.</param>
        /// <param name="selected">Select/deselect a term.</param>
        public void SelectSeveralTerms(int numberOfTerms, bool selected = true)
        {
            for (int i = 0; i < numberOfTerms; i++)
            {
                this.SelectTermByIndex(i, selected);
            }
        }

        /// <summary>
        /// Is term displayed
        /// </summary>
        /// <param name="termName">
        /// The term Name.
        /// </param>
        /// <returns>
        /// True if a term is displayed, false otherwise.
        /// </returns>
        public bool IsTermDisplayed(string termName) =>
            DriverExtensions.IsDisplayed(this.Container, By.XPath(string.Format(TermTitleLctMask, termName)));
    }
}