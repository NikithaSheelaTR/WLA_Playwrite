namespace Framework.Common.UI.Products.WestlawEdge.Components.RightPanel.OutlineBuilder
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using Framework.Common.UI.Products.Shared.Enums;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.WestlawEdge.DropDowns;
    using Framework.Common.UI.Raw.WestlawEdge.Items.Folders;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Outline Builder Right panel component
    /// </summary>
    public class OutlineBuilderRightPanelComponent : BaseOutlineBuilderComponent
    {
        private const string OutlineCitationLinkLctMask = "//*[@id='co_outlinePanelContainer']//a[@class='OutlineBuilder-keyciteLink' and contains(text(),'{0}')]";

        private static readonly By OutlineBuilderContainerLocator = By.XPath("//*[@id='co_rightColumn']");
        private static readonly By OutlineTitleLocator = By.CssSelector("div.OutlineBuilder-heading h4.OutlineBuilder-headingText");
        private static readonly By OutlineTitleInputLocator = By.CssSelector("input.OutlineBuilderUpdateName");
        private static readonly By SortOutlinesDropdownLocator = By.CssSelector("div.OutlineBuilder-SortTool");
        private static readonly By OutlinePanelHeaderLocator = By.CssSelector("div.OutlineBuilder-heading h3");
        private static readonly By SaveOutlineTitleButtonLocator = By.CssSelector("button.OutlineBuilderSaveName");
        private static readonly By ElementInTheListOfOutlinesLocator = By.CssSelector("ul.OutlineBuilder-outlineList li");
        private static readonly By FullPageButtonLocator = By.CssSelector("a.OutlineBuilder-fullPageLink");
        private static readonly By OutlineTitleBackButtonLocator = By.CssSelector("button.OutlineBuilderBackButton");
        private static readonly By OutlineResponsiveCloseButtonLocator = By.XPath(".//div[@id='co_outlineBuilderPanel']//button[@aria-label='Close dialog']");
        private static readonly By OutlineResponsiveHeadingToolsMenuButtonLocator = By.XPath(".//div[@id='co_outlineBuilderPanel']//button[contains(@class,'DocumentPanel-headingTools')]");

        /// <summary>
        /// Save Outline title button
        /// </summary>
        public new IButton SaveOutlineTitleButton => new Button(this.ComponentLocator, SaveOutlineTitleButtonLocator);

        /// <summary>
        /// Goto Full page mode button
        /// </summary>
        public IButton FullPageModeButton => new Button(this.ComponentLocator, FullPageButtonLocator);

        /// <summary>
        /// Back to List of Outlines button
        /// </summary>
        public IButton BackToListOfOutlinesButton => new Button(this.ComponentLocator, OutlineTitleBackButtonLocator);

        /// <summary>
        /// Label shows current Outline's title
        /// </summary>
        public override ILabel CurrentOutlineTitleLabel => new Label(this.ComponentLocator, OutlineTitleLocator);

        /// <summary>
        /// Label shows Outline Builder's header
        /// </summary>
        public ILabel OutlinePanelHeaderLabel => new Label(this.ComponentLocator, OutlinePanelHeaderLocator);

        /// <summary>
        /// Textbox changes current Outline's title
        /// </summary>
        public override ITextbox CurrentOutlineTextbox => new CustomTextbox(this.ComponentLocator, OutlineTitleInputLocator);

        /// <summary>
        /// List of existing Outlines
        /// </summary>
        public ItemsCollection<OutlineItem> ListOfOutlines => new ItemsCollection<OutlineItem>(this.ComponentLocator, ElementInTheListOfOutlinesLocator);

        /// <summary>
        /// Sort Outlines by orders dropdown
        /// </summary>
        public IDropdown<OutlinesSortOrderOptions> SortOutlinesByDropdown =>
            new SortOutlinesDropdown(DriverExtensions.WaitForElement(SortOutlinesDropdownLocator));

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => OutlineBuilderContainerLocator;

        /// <summary>
        /// Verify that component is displayed
        /// </summary>
        /// <returns> True if component is displayed, false otherwise </returns>
        public override bool IsDisplayed() => DriverExtensions.IsDisplayed(OutlineBuilderContainerLocator);

        /// <summary>
        /// Citation link on outline containing citation text
        /// </summary>
        /// <param name="citationText">The citation text or partial text</param>
        public ILink GetOutlineCitationLink(string citationText) => new Link(this.ComponentLocator, By.XPath(string.Format(OutlineCitationLinkLctMask, citationText)));

        /// <summary>
        /// Close Outline right panel button in Responsive mode
        /// </summary>
        public IButton OutlineResponsiveCloseButton => new Button(this.ComponentLocator, OutlineResponsiveCloseButtonLocator);

        /// <summary>
        /// Outline Heading Tools Menu button in Responsive mode
        /// </summary>
        public IButton OutlineResponsiveHeadingToolsMenuButton => new Button(this.ComponentLocator, OutlineResponsiveHeadingToolsMenuButtonLocator);
    }
}
