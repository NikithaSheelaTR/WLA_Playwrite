namespace Framework.Common.UI.Products.WestlawEdge.Components.RightPanel.OutlineBuilder
{
    using System;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.CommonTypes.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Outline's internal Right Panel component
    /// </summary>
    public class OutlineInternalRightPanelComponent : BaseOutlineInternalComponent
    {
        private static readonly By OutlineBuilderContainerLocator = By.XPath("//*[@id='co_rightColumn']");
        private static readonly By OutlineSnippetNodeLocator = By.CssSelector("div.outlineSnippetNode");
        private static readonly By HeadingOrNoteTextboxLocator = By.CssSelector("textarea.OutlineBuilder-textArea");
        private static readonly By ErrorMessage = By.CssSelector("div.InlineError-message");
        private static readonly By KeyCiteFlagLocator = By.CssSelector("a.icon25.icon_flag-customFive.QuickAccess-icon");
        private const string SmallCapLctMask = "//span[contains(@style, 'small-caps') and text() ='{0}']";

        /// <summary>
        /// Label shows Snippet node inside any Outline
        /// </summary>
        public string OutlineSnippetNodeLabel => DriverExtensions.GetElement(this.ComponentLocator, OutlineSnippetNodeLocator).Text;        

        /// <summary>
        /// Add Heading\Node textbox 
        /// </summary>
        public ITextbox HeadingOrNodeTextbox => new CustomTextbox(ComponentLocator, HeadingOrNoteTextboxLocator);

        /// <summary>
        /// Verified that error message displayed
        /// </summary>
        /// returns True if displayed False otherwise
        public bool IsErrorMessageDisplayed() => DriverExtensions.IsDisplayed(ErrorMessage);

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
        /// Gets the key cite flag.
        /// </summary>
        public KeyCiteFlag KeyCiteFlag
        {
            get
            {
                if (DriverExtensions.IsDisplayed(KeyCiteFlagLocator))
                {
                    string flagClass = DriverExtensions.GetAttribute("class", KeyCiteFlagLocator);
                    return flagClass.GetEnumValueByPropertyModel<KeyCiteFlag, WebElementInfo>(
                        model => model.ClassName,
                        String.Empty,
                        @"Resources/EnumPropertyMaps/WestlawEdge/Folders");
                }

                return KeyCiteFlag.NoFlag;
            }
        }

        /// <summary>
        /// Verify if text in small caps displayed
        /// </summary>
        /// returns True if displayed False otherwise
        public bool IsTextInSmallCaps(string text) => DriverExtensions.IsDisplayed(By.XPath(string.Format(SmallCapLctMask, text)));
    }
}
