

namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.FocusHighlighting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.FocusHighlighting;
    using Framework.Common.UI.Raw.WestlawEdge.Models.FocusHighlighting;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// New Focus Highlighting Widget
    /// </summary>
    public abstract class BaseFocusHighlightingDialog : BaseModuleRegressionDialog
    {
        private static readonly By CancelButtonLocator = By.XPath("//button[@class = 'co_overlayBox_buttonCancel']");

        private static readonly By ApplyButtonLocator = By.XPath(".//ul[@class = 'co_focusHighlightFooterContainer']//*[@class = 'co_primaryBtn']");

        private static readonly By TitleLocator = By.XPath("//*[contains(@id, 'coid_lightboxAriaLabel_')]");

        private static readonly By CloseButtonLocator = By.XPath("//button[@id = 'coid_focusHighlighting_cancel']");

        private static readonly By ContainerLocator = By.XPath("//div[@id='coid_focus_highlighting']");

        private static readonly By AdditionalMessageLocator = By.XPath(".//h5 | .//p");

        /// <summary>
        /// Apply button
        /// </summary>
        public IButton ApplyButton => new Button(this.Container, ApplyButtonLocator);

        /// <summary>
        /// Get smart terms dialog title
        /// </summary>
        public string Title => DriverExtensions.GetText(TitleLocator);

        /// <summary>
        /// Get dialog additional message text
        /// </summary>
        public string AdditionalMessage => DriverExtensions.GetText(ContainerLocator, AdditionalMessageLocator);

        /// <summary>
        /// List of terms
        /// </summary>
        protected abstract List<IWebElement> ListOfTerms { get; }

        /// <summary>
        /// Gets the TermColors enumeration to FocusHighlightingTermInfo map.
        /// </summary>
        protected EnumPropertyMapper<TermColors, FocusHighlightingTermInfo> TermColorMap =>
            EnumPropertyModelCache.GetMap<TermColors, FocusHighlightingTermInfo>(
                String.Empty,
                @"Resources/EnumPropertyMaps/WestlawEdge/FocusHighlighting");

        /// <summary>
        /// Container element
        /// </summary>
        protected IWebElement Container => DriverExtensions.GetElement(ContainerLocator);

        /// <summary>
        /// Clicks 'Cancel' button
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickCancelButton<T>() where T : ICreatablePageObject => this.ClickElement<T>(CancelButtonLocator);

        /// <summary>
        /// Gets terms count
        /// </summary>
        /// <returns>Number of terms.</returns>
        public int GetTermsCount() => this.ListOfTerms.Count;

        /// <summary>
        /// Gets terms titles
        /// </summary>
        /// <returns>
        /// List of terms titles
        /// </returns>
        public List<string> GetTermsTitles() => this.ListOfTerms.Select(term => term.Text).ToList();

        /// <summary>
        /// Clicks 'Close' button
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickCloseButton<T>() where T : ICreatablePageObject => this.ClickElement<T>(CloseButtonLocator);

        /// <summary>
        /// Returns term color by term name
        /// </summary>
        /// <param name="termName">Term name</param>
        /// <returns>Term color</returns>
        public abstract TermColors GetTermColor(string termName);

        /// <summary>
        /// Gets terms colors
        /// </summary>
        /// <returns>List of terms colors</returns>
        public List<TermColors> GetTermsColors() =>
                this.ListOfTerms.Select(term => term.GetCssValue("background-color"))
                .Select(termCode => this.GetColorTypeByCode(termCode)).ToList();

        /// <summary>
        /// Get color type by a code
        /// </summary>
        /// <param name="termCode">Term color rgb code</param>
        /// <returns>Term color</returns>
        protected TermColors GetColorTypeByCode(string termCode) =>
            Enum.GetValues(typeof(TermColors))
            .Cast<TermColors>()
            .First(color => TermColorMap[color].BackgroundColorCode.Equals(termCode));
    }
}