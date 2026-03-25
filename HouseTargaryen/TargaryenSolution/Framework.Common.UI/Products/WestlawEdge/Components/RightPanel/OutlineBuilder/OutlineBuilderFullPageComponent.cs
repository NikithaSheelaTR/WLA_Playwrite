namespace Framework.Common.UI.Products.WestlawEdge.Components.RightPanel.OutlineBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using Framework.Common.UI.Products.Shared.Enums;
    using Framework.Common.UI.Products.WestlawEdge.DropDowns;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium.Support.PageObjects;
    using OpenQA.Selenium;

	/// <summary>
	/// Outline Builder Full Page panel component
	/// </summary>
	public class OutlineBuilderFullPageComponent : BaseOutlineBuilderComponent
    {
        private static readonly By OutlineBuilderContainerLocator = By.XPath("//div[@id='co_body']");
        private static readonly By OutlineTitleLocator = By.CssSelector("h2.OutlineBuilderTitle");
        private static readonly By OutlineTitleInputLocator = By.CssSelector("textarea#coid_OutlineBuilder-textArea");
        private static readonly By SaveOutlineTitleButtonLocator = By.XPath("//button[text()='Save']");
        private static readonly By AlignTextDropdownLocator = By.XPath("//button[@id='OutlineBuilderAlignmentButton']/..");
        private static readonly By DownloadButtonLocator = By.XPath("//button[contains(@id,'deliveryLink')]");
        private static readonly By CollapseOutlinesPanelButtonLocator = By.CssSelector("button.OutlineBuilder-togglePanelButton");
        private static readonly By PanelCollapseStatusLocator = By.CssSelector("button.OutlineBuilder-togglePanelButton span");
        private static readonly By ReturnToDocumentButtonLocator = By.CssSelector("div#co_subHeader a");
        private static readonly By OutlineContentWindowLocator = By.CssSelector("div.OutlineBuilder-outline");
        private static readonly By NumberingButtonLocator = By.CssSelector("div.SlideToggle-thumb");
        private static readonly By DeliveryButtonLocator = By.Id("deliveryDropButton2");
        private static readonly By DeliveryOptions = By.XPath("//div[@id='deliveryWidgetMenu2']//li/a");

		/// <summary>
		/// Component locator
		/// </summary>
		protected override By ComponentLocator => OutlineBuilderContainerLocator;

        /// <summary>
        /// Save Outline title button
        /// </summary>
        public new IButton SaveOutlineTitleButton => new Button(this.ComponentLocator, SaveOutlineTitleButtonLocator);

        /// <summary>
        /// Download current Outline button
        /// </summary>
        public IButton DownloadButton => new Button(this.ComponentLocator, DownloadButtonLocator);

        /// <summary>
        /// Delivery Dropdown
        /// </summary>
        public IButton DeliveryButton => new Button(this.ComponentLocator, DeliveryButtonLocator);

		/// <summary>
		/// Switch Numbering button
		/// </summary>
		public IButton NumberingButton => new Button(this.ComponentLocator, NumberingButtonLocator);

        /// <summary>
        /// Collapse Outlines panel button
        /// </summary>
        public IButton CollapseButton => new Button(this.ComponentLocator, CollapseOutlinesPanelButtonLocator);

        /// <summary>
        /// Return to document button
        /// </summary>
        public IButton ReturnToDocumentButton => new Button(this.ComponentLocator, ReturnToDocumentButtonLocator);

        /// <summary>
        /// Label shows current Outline's title
        /// </summary>
        public override ILabel CurrentOutlineTitleLabel => new Label(this.ComponentLocator, OutlineTitleLocator);

        /// <summary>
        /// Textbox changes current Outline's title
        /// </summary>
        public override ITextbox CurrentOutlineTextbox => new CustomTextbox(this.ComponentLocator, OutlineTitleInputLocator);

        /// <summary>
        /// Align Outline's text dropdown
        /// </summary>
        public IDropdown<OutlinesAlignOrderOptions> AlignTextDropdown =>
            new AlignOutlineTextDropdown(DriverExtensions.WaitForElement(AlignTextDropdownLocator));

        /// <summary>
        /// Check what kind of alignment is applied
        /// </summary>
        /// <returns> string that specifies alignment </returns>
        public string ReturnOutlineViewportTextAlignment()
        {
            var classAttribute = DriverExtensions.GetElement(OutlineContentWindowLocator).GetAttribute("class");
            return new List<string> { "left", "centered", "hierarchy" }
                          .FirstOrDefault(item => classAttribute.Contains(item)) ?? "unknown";
        }

        /// <summary>
        /// Verify that component is displayed
        /// </summary>
        /// <returns> True if component is displayed, false otherwise </returns>
        public override bool IsDisplayed()
        {
            var element = DriverExtensions.SafeGetElement(new ByChained(ComponentLocator, PanelCollapseStatusLocator));
            return element != null && !element.Text.Equals("Expand outlines");            
        }

		/// <summary>
		/// Get Delivery Options Available
		/// </summary>
		/// <returns> List of String which are the delivery options </returns>
		public List<string> GetDeliveryOptions()
		{
            this.ClickDeliveryButton();
            var options = DriverExtensions.GetElements(DeliveryOptions);
            var optionTexts = from item in options select item.GetAttribute("Title");
            return optionTexts.ToList<string>();
		}

        /// <summary>
		/// Click Delivery button
		/// </summary>
        public void ClickDeliveryButton()
        {
            var element = DriverExtensions.GetElement(DeliveryButtonLocator);
            element.Click();
        }
    }
}
