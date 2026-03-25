namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.OutlineBuilder
{
    using System;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Raw.WestlawEdge.Items.Folders;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.Shared.Elements.WrapperEements.InfoBox;
    using Framework.Common.UI.Products.Shared.Enums;
    using Framework.Common.UI.Products.WestlawEdge.DropDowns;
    using OpenQA.Selenium;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.CommonTypes.Extensions;
    using OpenQA.Selenium.Support.PageObjects;

    /// <summary>
    /// Add Outline Dialog
    /// </summary>
    public class AddToOutlineDialog : BaseModuleRegressionDialog
    {
        private static readonly By OutlineBuilderContainerLocator = By.XPath("//div[@id='coid_OutlineBuilderModalLightbox']");
        private static readonly By SortOutlinesDropdownLocator = By.CssSelector("div.OutlineBuilder-SortTool");
        private static readonly By CreateNewOutlineButtonLocator = By.CssSelector("button.OutlineBuilder-buttonBuild");
        private static readonly By SaveOutlineButtonLocator = By.CssSelector("button.co_primaryBtn.OutlineBuilderModalSave");
        private static readonly By DialogTitleLocator = By.XPath(".//h2[contains(@id,'coid_lightboxAriaLabel')]");
        private static readonly By OutlineCancelButtonLocator = By.CssSelector("button.OutlineBuilderModalCancel");
        private static readonly By ElementInTheListOfOutlinesLocator = By.CssSelector("ul.OutlineBuilder-outlineList li");       
        private static readonly By InfoBoxLocator = By.XPath("//div[@class= 'co_infoBox success top OutlineBuilder-widgetMessage']//div[@class= 'co_infoBox_container']");
        private static readonly By InfoBoxMessageLocator = By.XPath(".//div[@class = 'co_infoBox_message']");
        private static readonly By InfoBoxCloseButtonLocator = By.XPath(".//button[@class='co_infoBox_closeButton']");
        private static readonly By KeyCiteFlagLocator = By.XPath(".//a[@class = 'icon25 icon_flag-customFive QuickAccess-icon']");
        private static readonly By CitationFormatDropdownLocator = By.Id("citeFormatSelect");

        /// <summary>
        /// Browse Component
        /// </summary>
        public AddToOutlineTabPanel AddToOutlinePanelComponent { get; } = new AddToOutlineTabPanel();

        /// <summary>
        /// Click on existing Outline in the list method
        /// </summary>
        public AddToUntitledDialog ClickOnOutlineByTitle(string title) => ListOfOutlines.First(item => item.TitleButton.Text == title)
            .TitleButton.Click<AddToUntitledDialog>();

        /// <summary>
        /// Create New Outline button
        /// </summary>
        public IButton CreateNewOutlineButton => new Button(this.ComponentLocator, CreateNewOutlineButtonLocator);

        /// <summary>
        /// Save Outline button
        /// </summary>
        public IButton SaveOutlineButton => new Button(this.ComponentLocator, SaveOutlineButtonLocator);

        /// <summary>
        /// Cancel Outline modal button
        /// </summary>
        public IButton OutlineModalCancelButton => new Button(this.ComponentLocator, OutlineCancelButtonLocator);

        /// <summary>
        /// Sort Outlines by orders dropdown
        /// </summary>
        public IDropdown<OutlinesSortOrderOptions> SortOutlinesByDropdown =>
            new SortOutlinesDropdown(DriverExtensions.WaitForElement(SortOutlinesDropdownLocator));

        /// <summary>
        /// List of existing Outlines
        /// </summary>
        public ItemsCollection<OutlineItem> ListOfOutlines => new ItemsCollection<OutlineItem>(this.ComponentLocator, ElementInTheListOfOutlinesLocator);
        
        /// <summary>
        /// InfoBox
        /// </summary>
        public IInfoBox InfoBox => new InfoBox(InfoBoxLocator, InfoBoxMessageLocator, InfoBoxCloseButtonLocator);
        
        /// <summary>
        /// Create New Outline modal title
        /// </summary>
        public ILabel DialogModalTitle => new Label(this.ComponentLocator, DialogTitleLocator);

        /// <summary>
        /// Add to Outline Citation Format Dropdown
        /// </summary>
        /// <returns></returns>
        public IDropdown<string> CitationFormatDropdown => new Dropdown(CitationFormatDropdownLocator);

        /// <summary>
        /// Gets the key cite flag.
        /// </summary>
        public KeyCiteFlag KeyCiteFlag
        {
            get
            {
                if (DriverExtensions.IsDisplayed(KeyCiteFlagLocator))
                {
                    string flagClass = DriverExtensions.GetAttribute("class",this.ComponentLocator, KeyCiteFlagLocator);
                    return flagClass.GetEnumValueByPropertyModel<KeyCiteFlag, WebElementInfo>(
                        model => model.ClassName,
                        String.Empty,
                        @"Resources/EnumPropertyMaps/WestlawEdge/Folders");
                }

                return KeyCiteFlag.NoFlag;
            }
        }

        /// <summary>
        /// Component locator
        /// </summary>
        protected By ComponentLocator => OutlineBuilderContainerLocator;

        /// <summary>
        /// Wait for List to populate
        /// </summary>
        public void WaitForListOfOutlines() => DriverExtensions.WaitForElement(new ByChained(this.ComponentLocator, ElementInTheListOfOutlinesLocator));
    }
}
