namespace Framework.Common.UI.Products.Shared.Dialogs.Document.Notes
{
    using System;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Enums;
    using Framework.Common.UI.Products.Shared.Models.Annotations;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdge.Elements;
    using Framework.Common.UI.Products.WestlawEdge.Elements.Judicial;
    using Framework.Common.UI.Products.WestLawNext.Dialogs;
    using Framework.Common.UI.Products.WestLawNextCanada.Enums;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Dialog that appears after selecting text on the Document page
    /// </summary>
    public class HighlightMenuDialog : BaseModuleRegressionDialog
    {
        private static readonly By CopyWithReferenceDropDownLocator = By.CssSelector("#co_copyWithRefMenu > button");
        private static readonly By CopyWithReferenceItemLocator = By.Id("co_selectedTextMenuListItem_CopyWithRef");
        private static readonly By CopyWithRefListItemLocator = By.XPath("//input[@name='copywithref_options']/ancestor::label");
        private static readonly By CopyWithReferenceGearIconLocator = By.XPath("//button[@class='co_buttonCopyWithRef_gear_icon']");
        private static readonly By SaveToFolderOptionLocator = By.Id("co_selectedTextMenuListItem_AddToDock");
        private static readonly By SaveToFolderLinkLocator = By.XPath("//ul[@class='co_selectedTextMenuList']/li/button");
        private static readonly By NoteColorBoxContainerLocator = By.XPath("//li[@id='co_selectedTextMenuListItem_AddNote']");
        private static readonly By HighlightColorBoxContainerLocator = By.XPath("//li[@id='co_selectedTextMenuListItem_Highlight']");
        private static readonly By HighlightDialogContainerLocator = By.Id("co_selectedTextMenu");
        private static readonly By AddToCompareLinkLocator = By.XPath("//*[contains(@class,'co-RedlineDropdown-button')]");
        private static readonly By AddToCompareArrowLocator = By.XPath("//div[@class='co-RedlineDropdown']/a");
        private static readonly By AddToCompareOverLimitMessageLocator = By.XPath("//span[@id = 'co_document_snippetCompareOver4kLimit']");
        private static readonly By CopyWithoutReferenceItemLocator = By.Id("co_selectedTextMenuListItem_CopyWithoutRef");
        private static readonly By AddToOutlinesItemLocator = By.Id("co_selectedTextMenuListItem_AddToOutlineLink");
        private static readonly By SubmitToQuickCheckButtonLocator = By.XPath("//li[contains(@id,'ListItem_SubmitToQuickCheck')]");
        private static readonly By CopyNeutralItemLocator = By.Id("crsw_selectedTextMenuListItem_CopyNeutral");

        private EnumPropertyMapper<HighlightColor, WebElementInfo> highlightColorPickerMap;
        private EnumPropertyMapper<SidebarOptions, WebElementInfo> sidebarOptionsMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="HighlightMenuDialog"/> class. 
        /// </summary>
        public HighlightMenuDialog()
        {
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Gets the Add to Compare link.
        /// </summary>
        public ILink AddToCompareLink { get; } = new AddToCompareLink(AddToCompareLinkLocator);

        /// <summary>
        /// Gets the Add to Compare arrow.
        /// </summary>
        public IButton AddToCompareArrow { get; } = new Button(AddToCompareArrowLocator);

        /// <summary>
        /// Gets over 4k limit message
        /// </summary>
        public ILabel AddToCompareOver4KLimitMessage { get; } = new Label(AddToCompareOverLimitMessageLocator);

        /// <summary>
        /// Submit to QuickCheck button.
        /// </summary>
        public IButton SubmitToQuickCheckButton { get; } = new Button(SubmitToQuickCheckButtonLocator);

        /// <summary>
        /// Add to Outlines button.
        /// </summary>
        public IButton AddToOutlinesButton { get; } = new CustomClickButton(AddToOutlinesItemLocator);

        /// <summary>
        /// Copy Neutral + pinpoint button
        /// </summary>
        public IButton CopyNeutralOptionButton { get; } = new CustomClickButton(CopyNeutralItemLocator);

        /// <summary>
        /// AddToCompareDropdown
        /// </summary>
        public AddToCompareDropdown AddToCompareDropdown { get; private set; } = new AddToCompareDropdown(HighlightDialogContainerLocator);

        /// <summary>
        /// Gets the Flag enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<HighlightColor, WebElementInfo> HighlightColorPickerMap =>
            this.highlightColorPickerMap = this.highlightColorPickerMap
                                           ?? EnumPropertyModelCache.GetMap<HighlightColor, WebElementInfo>(
                                               "Picker");

        /// <summary>
        /// Gets the content type enumeration to ContentTypeInfo map.
        /// </summary>
        protected EnumPropertyMapper<SidebarOptions, WebElementInfo> SidebarOptionsMap
            => this.sidebarOptionsMap = this.sidebarOptionsMap ?? EnumPropertyModelCache
                                            .GetMap<SidebarOptions, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/WestlawNextCanada");

        /// <summary>
        /// The click save to folder.
        /// </summary>
        /// <returns>
        /// The <see cref="SaveToFolderListDialog"/>.
        /// </returns>
        public SaveToFolderListDialog ClickSaveToFolder()
        {
            DriverExtensions.WaitForElementDisplayed(SaveToFolderLinkLocator);
            DriverExtensions.Hover(SaveToFolderLinkLocator);
            return new SaveToFolderListDialog();
        }

        /// <summary>
        /// Clicks on highlight text 
        /// </summary>
        /// <param name="color">
        /// The color.
        /// </param>
        /// <typeparam name="T">
        /// Page type 
        /// </typeparam>
        /// <returns>
        /// New instance of the page object 
        /// </returns>
        public T AddHighlight<T>(HighlightColor color = HighlightColor.Yellow) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElementDisplayed(HighlightColorBoxContainerLocator);
            DriverExtensions.GetElement(
                HighlightColorBoxContainerLocator, 
                By.XPath(this.HighlightColorPickerMap[color].LocatorString)).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Add Note to the Selected color
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="noteText">note text</param>
        /// <param name="color">color</param>
        /// <returns>The new instance of T page</returns>
        public T AddNote<T>(string noteText, HighlightColor color = HighlightColor.Yellow) where T : ICreatablePageObject
        {
            var editNoteDialog = this.ClickColorNoteOption<EditNoteDialog>(color);
            return editNoteDialog.AddTextToNote<T>(noteText);
        }

        /// <summary>
        /// Add Yellow Note to the Selected text
        /// </summary>
        /// <param name="noteText"> note text </param>
        /// <param name="color">color</param>
        /// <returns>
        /// The <see cref="NoteItemModel"/>. </returns>
        public NoteItemModel AddNote(string noteText = null, HighlightColor color = HighlightColor.Yellow)
        {
            var editNoteDialog = this.ClickColorNoteOption<EditNoteDialog>(color);
            return editNoteDialog.AddTextToNote(noteText ?? "Making a test note at " + DateTime.Now);
        }

        /// <summary>
        /// Click CopyWithReference DropDown and open the CopyWithReference option list
        /// </summary>
        public void ClickCopyWithReferenceDropDown() => this.ClickElement(CopyWithReferenceDropDownLocator);

        /// <summary>
        /// Click CopyWithReference Gear Icon
        /// </summary>
        public IButton CopyWithReferenceGearIcon() => new Button(CopyWithReferenceGearIconLocator);

        /// <summary>
        /// Click Save To Folder Option
        /// </summary>
        /// <typeparam name="T">page object</typeparam>
        /// <returns>New page instance</returns>
        public T ClickSaveToFolderOption<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElementDisplayed(SaveToFolderOptionLocator).Click();
            DriverExtensions.WaitForJavaScript();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Clicks the option in the copy with references list
        /// </summary>
        /// <typeparam name="T">page object</typeparam>
        /// <param name="option">Option to click</param>
        /// <returns>New page instance</returns>
        public T ClickCopyWithReferenceOption<T>(string option = null) where T : ICreatablePageObject
        {
            if (!string.IsNullOrEmpty(option))
            {
                DriverExtensions.WaitForElement(CopyWithReferenceDropDownLocator).Click();
                DriverExtensions.GetElements(CopyWithRefListItemLocator).First(o => o.Text.Contains(option)).Click();
                DriverExtensions.WaitForJavaScript();
            }

            DriverExtensions.WaitForElementDisplayed(CopyWithReferenceItemLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Verify that the option is available in the copy with references list
        /// </summary>
        /// <param name="option">Option to check</param>
        /// <returns>True if option displayed, false otherwise</returns>
        public bool IsCopyWithReferenceOptionExist(string option)
            => DriverExtensions.GetElements(CopyWithRefListItemLocator).Any(o => o.Text.Contains(option));

        /// <summary>
        /// Is copy with reference option displayed
        /// </summary>
        /// <returns>True if option is displayed, false otherwise.</returns>
        public bool IsCopyWithReferenceOptionDisplayed() => DriverExtensions.IsDisplayed(CopyWithReferenceItemLocator);

        /// <summary>
        /// Is copy without reference option displayed
        /// </summary>
        /// <returns>True if option is displayed, false otherwise.</returns>
        public bool IsCopyWithoutReferenceOptionDisplayed() => DriverExtensions.IsDisplayed(CopyWithoutReferenceItemLocator);

        /// <summary>
        /// Is safe to folder option displayed
        /// </summary>
        /// <returns>True if option is displayed, false otherwise.</returns>
        public bool IsSafeToFolderOptionDisplayed() => DriverExtensions.IsDisplayed(SaveToFolderOptionLocator);

        /// <summary>
        /// Click Note Option
        /// </summary>
        /// <typeparam name="T">
        /// EditNoteDialog
        /// </typeparam>
        /// <param name="color">
        /// The color.
        /// </param>
        /// <returns>
        /// New instance of type EditNoteDialog
        /// </returns>
        public T ClickColorNoteOption<T>(HighlightColor color) where T : EditNoteDialog
        {
            DriverExtensions.WaitForElement(DriverExtensions.WaitForElementDisplayed(NoteColorBoxContainerLocator), By.XPath(this.HighlightColorPickerMap[color].LocatorString)).Click();
            return DriverExtensions.CreatePageInstance<T>(); 
        }

        /// <summary>
        /// Select sidebar for highlighted text
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option">Sidebar option to select</param>
        /// <returns>Returns Page instance</returns>
        public T AddSidebar<T>(SidebarOptions option = SidebarOptions.BothSidebar) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElementDisplayed(HighlightColorBoxContainerLocator);
            DriverExtensions.Click(
                HighlightColorBoxContainerLocator,
                By.XPath(this.SidebarOptionsMap[option].LocatorString));
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Is Add a note option displayed
        /// </summary>
        /// <returns>True if option is displayed, false otherwise.</returns>
        public bool IsAddNoteOptionDisplayed() => DriverExtensions.IsDisplayed(NoteColorBoxContainerLocator);

        /// <summary>
        /// Is Highlight color option displayed
        /// </summary>
        /// <returns>True if option is displayed, false otherwise.</returns>
        public bool IsHighlightColorOptionDisplayed() => DriverExtensions.IsDisplayed(HighlightColorBoxContainerLocator);

    /// <summary>
    /// Select sidebar for highlighted text with a note
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="option">Sidebar note option to select</param>
    /// <returns>Returns Page instance</returns>
    public T AddNoteSidebar<T>(SidebarOptions option = SidebarOptions.BothNoteSidebar) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElementDisplayed(NoteColorBoxContainerLocator);
            DriverExtensions.Click(
                NoteColorBoxContainerLocator,
                By.XPath(this.SidebarOptionsMap[option].LocatorString));
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}