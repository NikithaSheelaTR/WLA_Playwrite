namespace Framework.Common.UI.Products.WestlawEdge.Components.RightPanel.OutlineBuilder
{
    using System.Collections.Generic;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.WestlawEdge.Elements.Judicial;
    using Framework.Common.UI.Raw.WestlawEdge.Items.Folders;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Base component of Outline's internal elements like Headings
    /// </summary>
    public class BaseOutlineInternalComponent : BaseEdgeRightPanelComponent
    {
        private const string HeadingButtonLctMask = "//div[@class='OutlineBuilder-buttonAddContainer']//li[contains(@id, 'co_heading')]";

        private static readonly By OutlineBuilderContainerLocator = By.XPath("//div[@id='co_body']");
        private static readonly By AddHeadingOrNoteButtonLocator = By.CssSelector("button.OutlineBuilder-buttonAdd");
        private static readonly By NoteButtonLocator = By.CssSelector("div.OutlineBuilder-buttonAddContainer li#co_outline_note");
        private static readonly By HeadingOrNoteTextboxLocator = By.CssSelector("textarea.OutlineBuilder-textArea");
        private static readonly By SaveHeadingButtonLocator = By.CssSelector("div.OutlineBuilder-editingContainer button.saveNode");
        private static readonly By DeleteHeadingButtonLocator = By.CssSelector("div.OutlineBuilder-editingContainer button.removeNode");
        private static readonly By ConfirmHeaderDeleteButtonLocator = By.CssSelector("button#OutlineBuilder-confirmDeleteButton");
        private static readonly By ElementInTheListOfHeadingsLocator = By.XPath(".//div[contains(@class,'OutlineBuilder-level')]//div[contains(@class,'OutlineBuilder-heading')]/..");

        /// <summary>
        /// Add Heading or Note to Outline button
        /// </summary>
        public IButton AddHeadingOrNoteButton => new CustomEdgeButton(this.ComponentLocator, AddHeadingOrNoteButtonLocator);

        /// <summary>
        /// Save Note or Heading component after editing button
        /// </summary>
        public IButton SaveHeadingButton => new Button(this.ComponentLocator, SaveHeadingButtonLocator);

        /// <summary>
        /// Delete Note or Heading component button
        /// </summary>
        public IButton DeleteHeadingButton => new Button(this.ComponentLocator, DeleteHeadingButtonLocator);

        /// <summary>
        /// Confirm Note or Heading component deletion button
        /// </summary>
        public IButton ConfirmDeleteHeadingButton => new Button(this.ComponentLocator, ConfirmHeaderDeleteButtonLocator);

        /// <summary>
        /// Add any Heading component to current Outline button
        /// </summary>       
        public IReadOnlyCollection<OutlineHeadingButton> HeadingButton => new ElementsCollection<OutlineHeadingButton>(
            this.ComponentLocator, By.XPath(HeadingButtonLctMask));

        /// <summary>
        /// Add Note component to current Outline button
        /// </summary>
        public IButton NoteButton => new CustomClickButton(this.ComponentLocator, NoteButtonLocator);

        /// <summary>
        /// Textbox for adding Headings\Notes to current Outline
        /// </summary>
        public ITextbox AddHeadingTextbox => new CustomTextbox(this.ComponentLocator, HeadingOrNoteTextboxLocator);

        /// <summary>
        /// List of existing Headings in current Outline
        /// </summary>
        public ItemsCollection<HeadingItem> ListOfHeadings => new ItemsCollection<HeadingItem>(this.ComponentLocator, ElementInTheListOfHeadingsLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => OutlineBuilderContainerLocator;

        /// <summary>
        /// Verify that component is displayed
        /// </summary>
        /// <returns> True if component is displayed, false otherwise </returns>
        public override bool IsDisplayed() => DriverExtensions.IsDisplayed(OutlineBuilderContainerLocator);
    }
}
