namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.NotesOfDecisions
{
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using Framework.Common.UI.Products.Shared.Elements.WrapperEements.InfoBox;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    /// <summary>
    /// The notes of decisions search within dialog.
    /// </summary>
    public class NotesOfDecisionsSearchWithinDialog : BaseModuleRegressionDialog
    {
        private const string RecentSearchSuggestionLctMask = "//ul[@id='co_searchNODRecentSearchesLink']//a[contains(text(), '{0}')]";

        private static readonly By DialogContainerLocator = By.XPath("//div[@id='co_dropdownContainerInnerContent']");

        private static readonly By SearchButtonLocator = By.XPath(".//button[contains(@class,'co_buttonSubmit')]");

        private static readonly By CloseButtonLocator = By.XPath(".//span[@class='Icon-close']");

        private static readonly By UndoSearchButtonLocator = By.XPath(".//button[contains(@class,'co_buttonUndo')]");

        private static readonly By HeaderLocator = By.XPath(".//div[@class='co_headerContainer']");

        private static readonly By TexboxLocator = By.XPath(".//input[@class='co_inputText']");

        private static readonly By ShowBooleanInstructionsButtonLocator = By.XPath(".//button[@class='co_buttonLink']");

        private static readonly By InstructionTextLocator = By.XPath(".//div[@class='co_instructionsContainer']");

        private static readonly By InstructionsPlusMinusIconLocator = By.XPath(".//span[contains(@class,'co_toggleIcon')]");

        private static readonly By InfoBoxMessageLocator = By.XPath(".//div[@class='co_infoBox_message']");

        private static readonly By InfoBoxCloseButtonLocator = By.XPath(".//a[@class='co_infoBox_closeButton']");

        private static readonly By RecentSearchesButtonLocator = By.XPath(".//button[@class='co_recentSearchesLink']");

        private static readonly By RecentSearchesSuggestionLocator = By.XPath(".//ul[@id='co_searchNODRecentSearchesLink']//a");

        private static readonly By InfoBoxContainerLocator = By.ClassName("co_infoBox_container");

        /// <summary>
        /// Header label
        /// </summary>
        public ILabel HeaderLabel => new Label(DialogContainerLocator, HeaderLocator);

        /// <summary>
        /// Instruction Text label
        /// </summary>
        public ILabel InstructionsTextLabel => new Label(DialogContainerLocator, InstructionTextLocator);

        /// <summary>
        /// Search within InfoBox
        /// </summary>
        /// <returns></returns>
        public IInfoBox InfoBox => new InfoBox(new ByChained(DialogContainerLocator, InfoBoxContainerLocator), InfoBoxMessageLocator, InfoBoxCloseButtonLocator);

        /// <summary>
        /// Close button
        /// </summary>
        public IButton CloseButton => new Button(DialogContainerLocator, CloseButtonLocator);

        /// <summary>
        /// Search button
        /// </summary>
        public IButton SearchButton => new Button(DialogContainerLocator, SearchButtonLocator);

        /// <summary>
        /// Undo Search button
        /// </summary>
        public IButton UndoSearchButton => new Button(DialogContainerLocator, UndoSearchButtonLocator);

        /// <summary>
        /// Show Boolean Instructions button
        /// </summary>
        public IButton ShowBooleanInstructionsButton => new Button(DialogContainerLocator, ShowBooleanInstructionsButtonLocator);

        /// <summary>
        /// Recent Searches button
        /// </summary>
        public IButton RecentSearchesButton => new Button(DriverExtensions.GetElement(DialogContainerLocator), RecentSearchesButtonLocator);

        /// <summary>
        /// Search TextBox
        /// </summary>
        public ITextbox SearchTextBox => new Textbox(DialogContainerLocator, TexboxLocator);        

        /// <summary>
        /// Verifies that the Plus/Minus Icon is displayed.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if the Plus/Minus Icon is displayed. </returns>
        public bool IsPlusMinusIconDisplayed() => DriverExtensions.IsDisplayed(
            DialogContainerLocator,
            InstructionsPlusMinusIconLocator);        

        /// <summary>
        /// Verifies that the Show Boolean Instructions is expanded.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if the Show Boolean Instructions is expanded. </returns>
        public bool IsInstructionsExpanded() => DriverExtensions.GetElement(
            DialogContainerLocator,
            ShowBooleanInstructionsButtonLocator).GetAttribute("aria-expanded").Equals("true");

        /// <summary>
        /// Verifies that the Plus/Minus Icon is expanded.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if the Plus/Minus Icon is expanded. </returns>
        public bool IsPlusMinusIconExpanded() => DriverExtensions.GetElement(
            DialogContainerLocator,
            InstructionsPlusMinusIconLocator).GetAttribute("class").Contains("expanded");           

        /// <summary>
        /// Click Recent Searches suggestion.
        /// </summary>
        /// <param name="query"></param>
        /// <returns>The <see cref="NotesOfDecisionsSearchWithinDialog"/>.</returns>
        public NotesOfDecisionsSearchWithinDialog ClickRecentSearchesSuggestion(string query)
            => this.ClickElement<NotesOfDecisionsSearchWithinDialog>(By.XPath(string.Format(RecentSearchSuggestionLctMask, query)));

        /// <summary>
        /// Get list of Recent Searches suggestions.
        /// </summary>
        /// <returns>list of previously searched queries</returns>
        public List<string> GetRecentSearchesList() =>
            DriverExtensions.GetElements(DialogContainerLocator, RecentSearchesSuggestionLocator).Select(suggestion => suggestion.Text).ToList();
    }
}