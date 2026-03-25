
namespace Framework.Common.UI.Products.Shared.Dialogs.Document
{
    using Framework.Common.Api.Endpoints.Alerts.DataModel;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.WrapperEements.InfoBox;
    using Framework.Common.UI.Products.Shared.Enums;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Copy with Reference Modal for document dialog.
    /// </summary>
    public class CopyWithReferenceDialog : BaseModuleRegressionDialog
    {
        private static readonly By CopyWithReferenceTitleLocator = By.XPath(".//*[contains(@id, 'coid_lightboxAriaLabel')]");
        private static readonly By CitationFormatLabelLocator = By.XPath(".//*[@class='co_copyCitation-select-label']");
        private static readonly By CitationFormatDropdownLocator = By.Id("co_copyCitationFormat_select");
        private static readonly By PreviewLabelLocator = By.XPath(".//*[contains(@class, 'co_copyCitation-preview')]/label");
        private static readonly By PreviewInputLocator = By.XPath(".//div[@id = 'co_copyCitationPreview']");
        private static readonly By SelectTextLabelLocator = By.XPath(".//*[@class = 'co_referenceTextOptions']/legend");
        private static readonly By SeperateLineFollowingRadioButtonLocator = By.XPath("//*[@id='radio_includeSeparateLineFollowingText']");
        private static readonly By SeperateLineFollowingLabelLocator = By.XPath("//*[contains(@for,'radio_includeSeparateLineFollowingText')]");
        private static readonly By SeperateLinePrecedingRadioButtonLocator = By.XPath("//*[@id='radio_includeSeparateLinePrecedingText']");
        private static readonly By SeperateLinePrecedingLabelLocator = By.XPath("//*[contains(@for,'radio_includeSeparateLinePrecedingText')]");
        private static readonly By CopyReferenceOnlyRadioButtonLocator = By.XPath("//*[@id='radio_copyReferenceOnly']");
        private static readonly By CopyReferenceOnlyLabelLocator = By.XPath("//*[contains(@for,'radio_copyReferenceOnly')]");
        private static readonly By SelectedTextParentheticalRadioButtonLocator = By.XPath("//*[@id='radio_addSelectedTextAsParentheticalAfterReference']");
        private static readonly By SelectedTextParentheticalLableLocator = By.XPath("//*[contains(@for,'radio_addSelectedTextAsParentheticalAfterReference')]");
        private static readonly By AdditionsLabelLocator = By.XPath("//*[contains(@class,'co_cwReferenceAdditions')]/legend");
        private static readonly By CreateLinkCheckboxLocator = By.XPath("//*[contains(@id,'co_addHyperlinkToCitation_checkbox')]");
        private static readonly By CreateLinkLabelLocator = By.XPath("//*[contains(@for,'co_addHyperlinkToCitation_checkbox')]");
        private static readonly By QuotationsCopiedCheckBox = By.XPath("//*[contains(@id,'co_quoteText_checkbox')]");
        private static readonly By QuotationsCopiedTextLabelLocator = By.XPath("//*[contains(@for,'co_quoteText_checkbox')]");
        private static readonly By InstructionLocator = By.XPath(".//p[@class = 'co_copyCitation-instructions']");
        private static readonly By CopyButtonLocator = By.XPath(".//input[@id = 'co_copyCitation_copyButton']");
        private static readonly By CancelButtonLocator = By.XPath(".//input[@id = 'co_copyCitation_cancelButton']");
        private static readonly By InfoMessageLocator = By.XPath("//*[text()='Text is copied.']");

        /// <summary>
        /// WebElement Container for the dialog
        /// </summary>
        private IWebElement Container => DriverExtensions.WaitForElement(By.XPath("//div[@id='co_copyWithReferenceModal']"));

        /// <summary>
        /// Copy With Reference Title Locator
        /// </summary>
        public ILabel CopyWithReferenceTitle => new Label(this.Container, CopyWithReferenceTitleLocator);

        /// <summary>
        /// Citation Format Label Locator
        /// </summary>
        public ILabel CitationFormatLabel => new Label(this.Container, CitationFormatLabelLocator);

        /// <summary>
        /// Copy Citation Format Dropdown
        /// </summary>
        /// <returns></returns>
        public IDropdown<string> CopyCitationFormatDropdown => new Dropdown(CitationFormatDropdownLocator);

        /// <summary>
        /// Preview Label
        /// </summary>
        public ILabel PreviewLabel => new Label(this.Container, PreviewLabelLocator);

        /// <summary>
        /// Get text from Preview field
        /// </summary>
        /// <returns></returns>
        public string GetPreviewText() => DriverExtensions.GetElement(this.Container, PreviewInputLocator).Text;

        /// <summary>
        /// Check if text from Preview field is underlined
        /// </summary>
        /// <returns></returns>
        public bool IsPreviewTextUnderline()
        {
            IWebElement element = DriverExtensions.GetElement(this.Container, PreviewInputLocator);
            return DriverExtensions.SafeGetElement(element, By.XPath(".//u"))?.GetCssValue("text-decoration")
            .Contains("underline") ?? false;
        }

        /// <summary>
        /// Select Text Options Label
        /// </summary>
        public ILabel SelectTextOptionsLabel => new Label(this.Container, SelectTextLabelLocator);

        /// <summary>
        ///  Separate Line Following Text Label
        /// </summary>
        public ILabel SeperateLineFollowingLabel => new Label(this.Container, SeperateLineFollowingLabelLocator);

        /// <summary>
        ///  Separate Line Preceding Text Label
        /// </summary>
        public ILabel SeperateLinePrecedingLabel => new Label(this.Container, SeperateLinePrecedingLabelLocator);

        /// <summary>
        ///  Copy with Reference Only Label
        /// </summary>
        public ILabel CopyWithReferenceOnlyLabel => new Label(this.Container, CopyReferenceOnlyLabelLocator);

        /// <summary>
        ///  Add Selected Text As Parenthetical After Reference Label
        /// </summary>
        public ILabel SelectedTextParentheticalLabel => new Label(this.Container, SelectedTextParentheticalLableLocator);

        /// <summary>
        /// Text is copied Info Message
        /// </summary>
        public IInfoBox TextInfoMessage => new InfoBox(InfoMessageLocator);

        /// <summary>
        /// Selects Separate Line Following Text RadioButton Is Selected
        /// </summary>
        public void SelectSeperateLineFollowingRadioButton()
        {
            var radioButton = DriverExtensions.GetElement(this.Container, SeperateLineFollowingRadioButtonLocator);
            if (!DriverExtensions.IsRadioButtonSelected(this.Container, SeperateLineFollowingRadioButtonLocator))
            {
                radioButton.Click();
                DriverExtensions.WaitForJavaScript();
            }
        }

        /// <summary>
        /// Selects Separate Line Preceding Text RadioButton Is Selected
        /// </summary>
        public void SelectSeperateLinePrecedingRadioButton()
        {
            var radioButton = DriverExtensions.GetElement(this.Container, SeperateLinePrecedingRadioButtonLocator);
            if (!DriverExtensions.IsRadioButtonSelected(this.Container, SeperateLinePrecedingRadioButtonLocator))
            {
                radioButton.Click();
                DriverExtensions.WaitForJavaScript();
            }
        }

        /// <summary>
        /// Check If Include Reference On A Separate Line Following Text RadioButton Is Selected
        /// </summary>
        /// <returns>True if the radio button is selected, false otherwise</returns>
        public bool CheckSeperateLineFollowingRadioButton()
        {
            var isSelected = DriverExtensions.IsRadioButtonSelected(this.Container, SeperateLineFollowingRadioButtonLocator);
            DriverExtensions.WaitForJavaScript();
            return isSelected;
        }

        /// <summary>
        /// Check If Include Reference On A Separate Line Preceding Text RadioButton Is Selected
        /// </summary>
        /// <returns>True if the radio button is selected, false otherwise</returns>
        public bool CheckSeperateLinePrecedingRadioButton()
        {
            var isSelected = DriverExtensions.IsRadioButtonSelected(this.Container, SeperateLinePrecedingRadioButtonLocator);
            DriverExtensions.WaitForJavaScript();
            return isSelected;
        }

        /// <summary>
        /// Selects Copy With Reference RadioButton Is Selected
        /// </summary>
        public void SelectCopyWithReferenceOnlyRadioButton()
        {
            var radioButton = DriverExtensions.GetElement(this.Container, CopyReferenceOnlyRadioButtonLocator);
            if (!DriverExtensions.IsRadioButtonSelected(this.Container, CopyReferenceOnlyRadioButtonLocator))
            {
                radioButton.Click();
                DriverExtensions.WaitForJavaScript();
            }
        }

        /// <summary>
        /// Check If Copy Reference Only RadioButton Is Selected
        /// </summary>
        /// <returns>True if the radio button is selected, false otherwise</returns>
        public bool CheckCopyReferenceOnlyRadioButton()
        {
            var isSelected = DriverExtensions.IsRadioButtonSelected(this.Container, CopyReferenceOnlyRadioButtonLocator);
            DriverExtensions.WaitForJavaScript();
            return isSelected;
        }

        /// <summary>
        /// Check If Add Selected Text As Parenthetical After Reference Is Selected
        /// </summary>
        /// <returns>True if the radio button is selected, false otherwise</returns>
        public bool CheckSelectedTextParentheticalRadioButton()
        {
            var isSelected = DriverExtensions.IsRadioButtonSelected(this.Container, SelectedTextParentheticalRadioButtonLocator);
            DriverExtensions.WaitForJavaScript();
            return isSelected;
        }

        /// <summary>
        /// Select the Selected Text Parenthetical After Reference radio button
        /// </summary>
        public void SelectTextParentheticalRadioButton()
        {
            var radioButton = DriverExtensions.GetElement(this.Container, SelectedTextParentheticalRadioButtonLocator);
            if (!DriverExtensions.IsRadioButtonSelected(this.Container, SelectedTextParentheticalRadioButtonLocator))
            {
                radioButton.Click();
                DriverExtensions.WaitForJavaScript();
            }
        }

        /// <summary>
        /// Additions Label
        /// </summary>
        public ILabel AdditionsLabel => new Label(this.Container, AdditionsLabelLocator);

        /// <summary>
        /// Create Link From Reference Label
        /// </summary>
        public ILabel CreateLinkLabel => new Label(this.Container, CreateLinkLabelLocator);

        /// <summary>
        /// Put Quotations Around Copied Text
        /// </summary>
        public ILabel QuotationsLabel=> new Label(this.Container, QuotationsCopiedTextLabelLocator);

        /// <summary>
        /// Create Link From Reference Checkbox
        /// </summary>
        /// <param name="selected"></param>
        public void SetCreateLinkCheckbox(bool selected)
        {
            DriverExtensions.SetCheckbox(selected, this.Container, CreateLinkCheckboxLocator);
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Check If Create Link From Reference Checkbox is selected
        /// </summary>
        /// <returns>
        /// True - if the checkbox is selected <see cref="bool"/>.
        /// </returns>
        public bool IsCreateLinkCheckboxSelected() =>
            DriverExtensions.IsCheckboxSelected(this.Container, CreateLinkCheckboxLocator);

        /// <summary>
        /// Check If Quotation Checkbox is selected
        /// </summary>
        /// <returns>
        /// True - if the checkbox is selected <see cref="bool"/>.
        /// </returns>
        public bool IsQuotationCheckboxSelected() =>
            DriverExtensions.IsCheckboxSelected(this.Container, QuotationsCopiedCheckBox);

        /// <summary>
        /// Put Quotations Around Copied Text Checkbox
        /// </summary>
        /// <param name="selected">The desired checkbox state</param>
        /// <returns>True if the checkbox is selected after setting, false otherwise</returns>
        public bool SetQuotationsCheckbox(bool selected)
        {
            DriverExtensions.SetCheckbox(selected, this.Container, QuotationsCopiedCheckBox);
            DriverExtensions.WaitForJavaScript();
            return DriverExtensions.IsCheckboxSelected(this.Container, QuotationsCopiedCheckBox);
        }

        /// <summary>
        /// Additional Settings Instruction Text
        /// </summary>
        public string AdditionalSettingsInstructionText => DriverExtensions.GetText(InstructionLocator, this.Container, 0);

        /// <summary>
        /// Copy Button
        /// </summary>
        public IButton CopyButton => new Button(this.Container, CopyButtonLocator);

        /// <summary>
        /// Cancel Button
        /// </summary>
        public IButton CancelButton => new Button(this.Container, CancelButtonLocator);

    
    }
}
