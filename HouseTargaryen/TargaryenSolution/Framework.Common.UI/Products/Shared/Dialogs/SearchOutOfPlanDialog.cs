namespace Framework.Common.UI.Products.Shared.Dialogs
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using OpenQA.Selenium;

    /// <summary>
    /// Dialog that pops up when you view an search out of plan document
    /// </summary>
    public class SearchOutOfPlanDialog : BaseModuleRegressionDialog
    {
        private static readonly By ClientIdTextBoxLocator = By.ClassName("co_clientIDInline_recent");

        private static readonly By ViewSearchResultsButtonLocator = By.Id("co_WarnViewOkButton");

        /// <summary>
        /// View search results button
        /// </summary>
        public IButton ViewSearchResultsButton => new Button(ViewSearchResultsButtonLocator);

        /// <summary>
        /// Client id textbox
        /// </summary>
        public ITextbox ClientIdTexbox => new Textbox(ClientIdTextBoxLocator);
    }
}
