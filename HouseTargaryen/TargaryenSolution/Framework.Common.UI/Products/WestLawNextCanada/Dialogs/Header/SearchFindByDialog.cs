namespace Framework.Common.UI.Products.WestLawNextCanada.Dialogs.Header
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Search Find By dialog
    /// </summary>
    public class SearchFindByDialog : BaseModuleRegressionDialog
    {
        private static readonly By ContainerLocator = By.Id("co_queryBuilderWidgetContainer");
        private static readonly By FindByCitationLocator = By.Id("co_queryBuilderWidget_CI_citationFindBy");
        private static readonly By FindByCitationBtnLocator = By.Id("button_citationFindBy");
        private static readonly By FindByCaseTextboxLocator = By.Id("co_queryBuilderWidget_findLaRefCaseByNameValue");

        /// <summary>
        /// Find By Citation button
        /// </summary>
        public IButton FindByCitationButton => new Button(ContainerLocator, FindByCitationBtnLocator);

        /// <summary>
        /// Find By Case Textbox
        /// </summary>
        public ITextbox FindByCaseTextbox => new Textbox(ContainerLocator, FindByCaseTextboxLocator);

        /// <summary>
        /// Enter citation in the Find By dialog
        /// </summary>
        /// <param name="citation"></param>
        public void EnterCitation(string citation) => 
            DriverExtensions.WaitForElement(FindByCitationLocator).SendKeys(citation);
    }
}