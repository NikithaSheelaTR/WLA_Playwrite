namespace Framework.Common.UI.Products.WestlawEdge.Components.Document
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.Document.Notes;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Navigation component for inline notes
    /// </summary>
    public class EdgeInlineNotesNavigationComponent : BaseModuleRegressionComponent
    {
        private static readonly By NextNoteButtonLocator = By.XPath("//button[@id='noteNavNext']");
        private static readonly By PreviousNoteButtonLocator = By.XPath("//button[@id='noteNavNext']");
        private static readonly By NavigationComponentContainerLocator = By.XPath("//div[@id='noteNavigationContainer']");
        private static readonly By ActiveInlineNoteContainerLocator = By.XPath(
            "//div[@id = 'coid_inlineNotesContainer']//span[contains(@class, 'co_noteHolderActive')]");
        private static readonly By NavigationComponentTextLocator = By.XPath("//span[@id ='noteNavDisplay']");
        private static readonly By StrikethroughNoteNumberLocator = By.XPath(".//span[@class = 'redStrikethrough']");
        private static readonly By ContainerLocator = By.Id("coid_inlineNotesContainer");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Clicks Next Inline note Button
        /// </summary>
        public EdgeViewInlineNoteDialog ClickNextNoteButton()
        {
            DriverExtensions.WaitForElement(NextNoteButtonLocator).Click();
            return new EdgeViewInlineNoteDialog(DriverExtensions.GetElement(ActiveInlineNoteContainerLocator));
        }

        /// <summary>
        /// Clicks Previous Inline note Button
        /// </summary>
        public EdgeViewInlineNoteDialog ClickPreviousNoteButton()
        {
            DriverExtensions.WaitForElement(PreviousNoteButtonLocator).Click();
            return new EdgeViewInlineNoteDialog(DriverExtensions.GetElement(ActiveInlineNoteContainerLocator));
        }

        /// <summary>
        /// Checks if the NavigationComponent is Displayed
        /// </summary>
        /// <returns>Whether or not the NavigationComponent is displayed</returns>
        public bool IsNavigationComponentDisplayed() => DriverExtensions.IsDisplayed(NavigationComponentContainerLocator, 5);

        /// <summary>
        /// Get text from the navigation widget
        /// </summary>
        public string GetTextFromNavigationWidget() => DriverExtensions.GetElement(NavigationComponentTextLocator).Text;

        /// <summary>
        /// Get the number of strikethrough inline note
        /// </summary>
        public string GetStrikethroughInlineNoteNumber() => DriverExtensions.GetElement(NavigationComponentContainerLocator, StrikethroughNoteNumberLocator).Text;
    }
}