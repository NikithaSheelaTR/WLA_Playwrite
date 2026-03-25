namespace Framework.Common.UI.Products.WestlawEdge.Components.Folders
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// EdgeAddNoteToItemComponent
    /// </summary>
    public class EdgeAddNoteToItemComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath(".//div[@class = 'Foldering--NoteContainer'] | //div[@class = 'FileListItem--NoteContainer']");
        private static readonly By TextboxLocator = By.XPath(".//textarea[contains(@class,'NoteEdit')]");
        private static readonly By SaveButtonLocator = By.XPath(".//button[@class ='co_primaryBtn Button--small']");
        private static readonly By CancelButtonLocator = By.XPath(".//button[@class ='co_secondaryBtn Button--small']");
        
        /// <summary>
        /// Initializes a new instance of the <see cref="EdgeAddNoteToItemComponent"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        internal EdgeAddNoteToItemComponent(IWebElement container) =>
            this.Container = DriverExtensions.WaitForElement(container, ContainerLocator);

        /// <summary>
        /// Add note textbox
        /// </summary>
        public ITextbox AddNoteTextbox => new Textbox(this.Container, TextboxLocator);

        /// <summary>
        /// Save button
        /// </summary>
        public IButton SaveButton => new Button(this.Container, SaveButtonLocator);

        /// <summary>
        /// Cancel button
        /// </summary>
        public IButton CancelButton => new Button(this.Container, CancelButtonLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        private IWebElement Container { get; }
    }
}