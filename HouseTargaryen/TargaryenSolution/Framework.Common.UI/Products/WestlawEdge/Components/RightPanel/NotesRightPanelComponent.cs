namespace Framework.Common.UI.Products.WestlawEdge.Components.RightPanel
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Interfaces.Elements;

    using OpenQA.Selenium;

    /// <summary>
    /// Notes Right panel component
    /// </summary>
    public class NotesRightPanelComponent : BaseEdgeRightPanelComponent
    {
        private static readonly By NotesContainerLocator = By.XPath("//div[@id='co_annotationsPanel']");
        private static readonly By NotesZeroStateMessageLocator = By.XPath("//div[@id='co_annotationsPanel']//div[@class='Notes-zeroState']");
        private static readonly By EditButtonLocator= By.XPath(".//div[@class='co_viewNote']//button[@class='co_noteEditButton']");
        private static readonly By DeleteButtonLocator = By.XPath(".//button[@class='co_noteDeleteButton']");
        private static readonly By CloseLinkLocator = By.CssSelector(".co_notes_closeLink");

        /// <summary>
        /// Notes zero state message label
        /// </summary>
        public ILabel ZeroStateLabel => new Label(NotesZeroStateMessageLocator);

        /// <summary>
        /// Verify that component is displayed
        /// </summary>
        /// <returns> True if component is displayed, false otherwise </returns>
        public override bool IsDisplayed() => DriverExtensions.IsDisplayed(this.ComponentLocator);

        /// <summary>
        /// Edit button
        /// </summary>
        public IButton EditButton => new Button(EditButtonLocator);

        /// <summary>
        /// Delete button
        /// </summary>
        public IButton DeleteButton => new Button(DeleteButtonLocator);

        /// <summary>
        /// close link
        /// </summary>
        public ILink CloseLink => new Link(CloseLinkLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => NotesContainerLocator;
    }
}
