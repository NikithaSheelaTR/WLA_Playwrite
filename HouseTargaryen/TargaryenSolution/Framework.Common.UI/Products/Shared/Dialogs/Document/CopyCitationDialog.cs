namespace Framework.Common.UI.Products.Shared.Dialogs.Document
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.WrapperEements.InfoBox;
    using Framework.Common.UI.Products.Shared.Enums;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Copy citation for the document dialog
    /// </summary>
    public class CopyCitationDialog : BaseModuleRegressionDialog
    {     
        private static readonly By TitleLocator = By.XPath(".//*[contains(@id, 'coid_lightboxAriaLabel')]");
        private static readonly By PreviewInputLocator = By.XPath(".//div[@id = 'co_copyCitationPreview']");
        private static readonly By AddCitationCheckboxLocator = By.XPath(".//input[@id = 'co_addHyperlinkToCitation_checkbox']");
        private static readonly By AddCitationCheckboxTextLocator = By.XPath(".//label[@for = 'co_addHyperlinkToCitation_checkbox']");
        private static readonly By CopyCitationInstructionLocator = By.XPath(".//p[@class = 'co_copyCitation-instructions']");
        private static readonly By CloseButtonLocator = By.XPath(".//button[@id = 'co_copyCitationLightbox_closeLink']");
        private static readonly By CopyButtonLocator = By.XPath(".//input[@id = 'co_copyCitation_copyButton']");
        private static readonly By CancelButtonLocator = By.XPath(".//input[@id = 'co_copyCitation_cancelButton']");
        private static readonly By InfoMessageLocator = By.XPath(".//div[@class ='co_infoBox_message']");
        private static readonly By CopyCitationFormatDropdownLocator = By.Id("co_copyCitationFormat_select");

        /// <summary>
        /// Copy Citation Format Dropdown
        /// </summary>
        /// <returns></returns>
        public IDropdown<string> CopyCitationFormatDropdown => new Dropdown(CopyCitationFormatDropdownLocator);

        /// <summary>
        /// Title
        /// </summary>
        public ILabel Title => new Label(this.Container, TitleLocator);

        /// <summary>
        /// Add citation checkbox text 
        /// </summary>
        public string AddCitationCheckboxText => DriverExtensions.GetText(AddCitationCheckboxTextLocator, this.Container, 0);

        /// <summary>
        /// Instruction Text
        /// </summary>
        public string InstructionText => DriverExtensions.GetText(CopyCitationInstructionLocator, this.Container, 0);

        /// <summary>
        /// InfoBox
        /// </summary>        
        public IInfoBox InfoBox => new InfoBox(DriverExtensions.WaitForElement(this.Container, InfoMessageLocator));

        private IWebElement Container => DriverExtensions.WaitForElement(
            By.XPath(
                EnumPropertyModelCache.GetMap<Dialogs, WebElementInfo>()[Dialogs.CopyCitation].LocatorString));

        /// <summary>
        /// Enter Text in Preview Input
        /// </summary>
        /// <param name="text">Text for preview box</param>
        public void EnterTextInPreviewInput(string text) => DriverExtensions.SetTextField(text, this.Container, PreviewInputLocator);

        /// <summary>
        /// Get text from Preview field
        /// </summary>
        /// <returns></returns>
        public string GetPreviewText() => DriverExtensions.GetElement(this.Container, PreviewInputLocator).Text;

        /// <summary>
        /// Get text from Preview field
        /// </summary>
        /// <returns></returns>
        public bool IsPreviewTextUnderline()
        {
            IWebElement element = DriverExtensions.GetElement(this.Container, PreviewInputLocator);
            return DriverExtensions.SafeGetElement(element, By.XPath(".//u"))?.GetCssValue("text-decoration")
                                   .Contains("underline") ?? false;
        }

        /// <summary>
        /// Get text from Preview field
        /// </summary>
        /// <returns></returns>
        public bool IsPreviewTextItalic()
        {
            IWebElement element = DriverExtensions.GetElement(this.Container, PreviewInputLocator);
            return DriverExtensions.SafeGetElement(element, By.XPath(".//i"))?.GetCssValue("font-style")
                                   .Contains("italic") ?? false;
        }

        /// <summary>
        /// Set Add hyperlink to citation Checkbox
        /// </summary>
        /// <param name="selected"></param>
        public void SetAddCitationCheckbox(bool selected)
        {
            DriverExtensions.SetCheckbox(selected, this.Container, AddCitationCheckboxLocator);
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Set Add hyperlink to citation Checkbox
        /// </summary>
        /// <returns>
        /// True - if the checkbox is selected <see cref="bool"/>.
        /// </returns>
        public bool IsAddCitationCheckboxSelected() =>
            DriverExtensions.IsCheckboxSelected(this.Container, AddCitationCheckboxLocator);

        /// <summary>
        /// Click Copy Button
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickCopyButton<T>() where T : ICreatablePageObject
        {
            return this.ClickElement<T>(this.Container, CopyButtonLocator);
        }

        /// <summary>
        /// Click Cancel Button
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickCancelButton<T>() where T : ICreatablePageObject
        {
            return this.ClickElement<T>(this.Container, CancelButtonLocator);
        }

        /// <summary>
        /// Click Close Button
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T CloseDialog<T>() where T : ICreatablePageObject
        {
            return this.ClickElement<T>(this.Container, CloseButtonLocator);
        }
    }
}
