namespace Framework.Common.UI.Products.WestlawEdgePremium.Dialogs
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// This class models the Right panel tools dialog.
    /// </summary>
    public class PrecisionRightPanelToolsDialog : BaseModuleRegressionDialog
    {
        private static readonly By ContainerLocator = By.XPath("//*[@class='Panel-right']");
        private static readonly By CloseButtonLocator = By.XPath(".//*[@class='Panel-close']");
        private static readonly By GoToSelectedTextButtonLocator = By.XPath(".//span[contains(text(), 'Go to selected text')]");
        private static readonly By NotesPanelButtonLocator = By.XPath(".//button/span[text()='Notes']");
        private static readonly By OutlinesPanelButtonLocator = By.XPath(".//button/span[text()='Outlines']");
        private static readonly By QuickCheckPanelButtonLocator = By.XPath(".//button/span[text()='Quick Check']");
        private static readonly By FullPageButtonLocator = By.XPath(".//button/span[text()='Full page']");

        /// <summary>
        /// Close button
        /// </summary>
        public IButton CloseButton => new Button(ContainerLocator, CloseButtonLocator);

        /// <summary>
        /// Go to selected button
        /// </summary>
        public IButton GoToSelectedTextButton => new Button(ContainerLocator, GoToSelectedTextButtonLocator);

        /// <summary>
        /// Notes panel button
        /// </summary>
        public IButton NotesPanelButton => new Button(ContainerLocator, NotesPanelButtonLocator);

        /// <summary>
        /// Outlines panel button
        /// </summary>
        public IButton OutlinesPanelButton => new Button(ContainerLocator, OutlinesPanelButtonLocator);

        /// <summary>
        /// Quick check button
        /// </summary>
        public IButton QuickCheckPanelButton => new Button(ContainerLocator, QuickCheckPanelButtonLocator);

        /// <summary>
        /// Full page button
        /// </summary>
        public IButton FullPageButton => new Button(ContainerLocator, FullPageButtonLocator);

        /// <summary>
        /// Verify that tools menu is displayed
        /// </summary>
        /// <returns> True if tools menu is displayed, false otherwise </returns>
        public bool IsDisplayed() => DriverExtensions.IsDisplayed(ContainerLocator);
    }
}
