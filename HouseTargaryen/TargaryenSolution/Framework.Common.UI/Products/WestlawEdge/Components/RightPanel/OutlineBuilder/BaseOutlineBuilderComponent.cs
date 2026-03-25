namespace Framework.Common.UI.Products.WestlawEdge.Components.RightPanel.OutlineBuilder
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;    
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Outline Builder Full page panel component
    /// </summary>
    public class BaseOutlineBuilderComponent : BaseEdgeRightPanelComponent
    {
        private static readonly By OutlineBuilderContainerLocator = By.XPath("//div[@id='co_body']");
        private static readonly By CreateNewOutlineButtonLocator = By.CssSelector("button.OutlineBuilder-buttonBuild");
        private static readonly By EditOutlineTitleButtonLocator = By.CssSelector("button.OutlineBuilderEditNameButton");

        /// <summary>
        /// Create New Outline button
        /// </summary>
        public IButton CreateNewOutlineButton => new Button(this.ComponentLocator, CreateNewOutlineButtonLocator);

        /// <summary>
        /// Edit Outline title button
        /// </summary>
        public IButton EditOutlineTitleButton => new Button(this.ComponentLocator, EditOutlineTitleButtonLocator);

        /// <summary>
        /// Save Outline title button
        /// </summary>
        public virtual IButton SaveOutlineTitleButton { get; set; }

        /// <summary>
        /// Label shows current Outline's title
        /// </summary>
        public virtual ILabel CurrentOutlineTitleLabel { get; set; }

        /// <summary>
        /// Textbox changes current Outline's title
        /// </summary>
        public virtual ITextbox CurrentOutlineTextbox { get; set; }

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
