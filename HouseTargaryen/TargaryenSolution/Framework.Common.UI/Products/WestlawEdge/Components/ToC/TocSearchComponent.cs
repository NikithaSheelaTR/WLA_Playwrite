namespace Framework.Common.UI.Products.WestlawEdge.Components.ToC
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.WrapperEements.InfoBox;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// ToC Search Component
    /// </summary>
    public class TocSearchComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.Id("co_searchTocWidgetInnerContent");

        private static readonly By SearchButtonLocator = By.Id("co_searchTocDoSearchButton");

        private static readonly By InfoMessageLocator = By.ClassName("co_infoBox_message");

        private static readonly By SearchTextboxLocator = By.Id("co_searchTocTextInput");

        private static readonly By ResultsCountLocator = By.Id("co_searchTocWidgetStats");

        private static readonly By ErrorMessageInfoBoxLocator = By.Id("co_searchWithinErrorMessageBox");

        private static readonly By SearchTocCloseButtonLocator = By.Id("co_searchTocClose");

        /// <summary>
        /// Search Button
        /// </summary>
        public IButton SearchButton => new Button(SearchButtonLocator);

        /// <summary>
        /// Search TOC Close Button
        /// </summary>
        public IButton SearchTocCloseButton => new Button(SearchTocCloseButtonLocator);

        /// <summary>
        /// Search Textbox
        /// </summary>
        public ITextbox SearchTextbox => new Textbox(SearchTextboxLocator);

        /// <summary>
        /// Info Message
        /// </summary>
        public IInfoBox InfoMessage => new InfoBox(DriverExtensions.GetElement(ContainerLocator, InfoMessageLocator));

        /// <summary>
        /// Info Box
        /// </summary>
        public IInfoBox ErrorMessageInfoBox => new InfoBox(DriverExtensions.GetElement(ErrorMessageInfoBoxLocator));

        /// <summary>
        /// Results Count Label
        /// </summary>
        public ILabel ResultsCountLabel => new Label(ResultsCountLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}
