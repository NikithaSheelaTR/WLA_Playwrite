namespace Framework.Common.UI.Products.Shared.Components.Preferences
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.Shared.Enums.Preferences;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Citations Tab Component
    /// </summary>
    public class CitationsTabComponent : BaseTabComponent
    {
        private static readonly By ContainerLocator = By.Id("coid_userSettingsTabPanel7");

        private EnumPropertyMapper<CitationsTab, WebElementInfo> citationsTabMap;

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Citations";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the CitationsTab enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<CitationsTab, WebElementInfo> CitationsTabMap
            => this.citationsTabMap = this.citationsTabMap ?? EnumPropertyModelCache.GetMap<CitationsTab, WebElementInfo>();

        /// <summary>
        /// Returns the value of the specified dropdown
        /// </summary>
        /// <param name="deliveryTabOption">the dropdown to look for</param>
        /// <returns>the selected dropdown option</returns>
        public string GetCitationsTabDropdownSelectedValue(CitationsTab deliveryTabOption)
            => DriverExtensions.GetSelectedDropdownOptionText(By.Id(this.CitationsTabMap[deliveryTabOption].Id));

        /// <summary>
        /// Returns true if the specified element on the Citations tab is selected (checked for checkboxes)
        /// </summary>
        /// <param name="tabOption">the option to look for</param>
        /// <returns>true if selected, false otherwise</returns>
        public bool IsCitationsTabOptionSelected(CitationsTab tabOption) => DriverExtensions.GetElement(By.Id(this.CitationsTabMap[tabOption].Id)).Selected;

        /// <summary>
        /// Selects the specified radio-button option on the Citations tab.
        /// </summary>
        /// <param name="tabOption"> the option to look for </param>
        /// <returns> The <see cref="CitationsTabComponent"/>CitationsTabComponent</returns>
        public CitationsTabComponent SelectCitationsTabOptionRadioButton(CitationsTab tabOption)
        {
            DriverExtensions.GetElement(By.Id(this.CitationsTabMap[tabOption].Id)).Click();
            return this;
        }

        /// <summary>
        /// Sets the specified checkbox option on the Citations tab to the specified value.
        /// </summary>
        /// <param name="tabOption"> the option to look for </param>
        /// <param name="setTo"> What to set the checkbox to. True for checked, false for unchecked. </param>
        /// <returns> The <see cref="CitationsTabComponent"/>CitationsTabComponent </returns>
        public CitationsTabComponent SetCitationsTabOptionCheckbox(CitationsTab tabOption, bool setTo)
        {
            DriverExtensions.SetCheckbox(setTo, By.Id(this.CitationsTabMap[tabOption].Id));
            return this;
        }

        /// <summary>
        /// Sets the specified dropdown on the citations tab to the specified value.
        /// </summary>
        /// <param name="tabOption"> the option to look for </param>
        /// <param name="option"> What to select from the dropdown. </param>
        /// <returns> The <see cref="CitationsTabComponent"/>CitationsTabComponent </returns>
        public CitationsTabComponent SetCitationsTabOptionDropdown(CitationsTab tabOption, string option)
        {
            DriverExtensions.SetDropdown(option, By.Id(this.CitationsTabMap[tabOption].Id));
            return this;
        }

        /// <summary>
        /// Gets citation format dropdown element for the specified option
        /// </summary>
        /// <param name="citationFormatOption">the citation format option to look for</param>
        /// <returns>the citation format dropdown element</returns>
        public IDropdown<string> DropdownByCitationFormat(CitationsTab citationFormatOption)
            => new Dropdown(By.XPath(this.CitationsTabMap[citationFormatOption].LocatorString));

        /// <summary>
        /// Gets citation style dropdown element for the specified option
        /// </summary>
        /// <param name="citationStyleOption">the citation style option to look for</param>
        /// <returns>the citation style dropdown element</returns>
        public IDropdown<string> DropdownByCitationStyle(CitationsTab citationStyleOption)
            => new Dropdown(By.XPath(this.CitationsTabMap[citationStyleOption].LocatorString));

        /// <summary>
        /// Gets citation style radio button element for the specified option
        /// </summary>
        /// <param name="citationStyleOption">the citation style option to look for</param>
        /// <returns>the citation style radio button element</returns>
        public IRadiobutton RadioButtonByCitationStyle(CitationsTab citationStyleOption)
            => new Radiobutton(By.XPath(this.CitationsTabMap[citationStyleOption].LocatorString));

        /// <summary>
        /// Gets parallel citations radio button element for the specified option
        /// </summary>
        /// <param name="parallelCitationsOption">the parallel citations option to look for</param>
        /// <returns>the parallel citations radio button element</returns>
        public IRadiobutton RadioButtonByParallelCitations(CitationsTab parallelCitationsOption)
            => new Radiobutton(By.XPath(this.CitationsTabMap[parallelCitationsOption].LocatorString));

        /// <summary>
        /// Gets selected text options radio button element for the specified option
        /// </summary>
        /// <param name="selectedTextOption">the selected text option to look for</param>
        /// <returns>the selected text options radio button element</returns>
        public IRadiobutton RadioButtonBySelectedTextOptions(CitationsTab selectedTextOption)
            => new Radiobutton(By.XPath(this.CitationsTabMap[selectedTextOption].LocatorString));

        /// <summary>
        /// Gets citation formatting checkbox element for the specified option
        /// </summary>
        /// <param name="citationFormattingOption">the citation formatting option to look for</param>
        /// <returns>the citation formatting checkbox element</returns>
        public ICheckBox CheckboxByCitationFormatting(CitationsTab citationFormattingOption)
            => new CheckBox(By.XPath(this.CitationsTabMap[citationFormattingOption].LocatorString));

        /// <summary>
        /// Gets hyperlink checkbox element for the specified option
        /// </summary>
        /// <param name="hyperlinkOption">the hyperlink option to look for</param>
        /// <returns>the hyperlink checkbox element</returns>
        public ICheckBox CheckboxByHyperLink(CitationsTab hyperlinkOption)
            => new CheckBox(By.XPath(this.CitationsTabMap[hyperlinkOption].LocatorString));

        /// <summary>
        /// Gets additions checkbox element for the specified option
        /// </summary>
        /// <param name="additionsOption">the additions option to look for</param>
        /// <returns>the additions checkbox element</returns>
        public ICheckBox CheckboxByAdditions(CitationsTab additionsOption)
            => new CheckBox(By.XPath(this.CitationsTabMap[additionsOption].LocatorString));

        /// <summary>
        /// Gets new icon button element for the specified option
        /// </summary>
        /// <param name="newIconOption">the new icon option to look for</param>
        /// <returns>the new icon button element</returns>
        public IButton ButtonByNewIcon(CitationsTab newIconOption)
            => new Button(By.XPath(this.CitationsTabMap[newIconOption].LocatorString));

        /// <summary>
        /// Gets label element for the specified citation format option
        /// </summary>
        /// <param name="citationFormatOption">the citation format option to look for</param>
        /// <returns>the label element</returns>
        public ILabel LabelByCitationFormat(CitationsTab citationFormatOption)
            => this.GetLabelText(citationFormatOption);

        /// <summary>
        /// Gets citation format dropdown header label element
        /// </summary>
        /// <returns>the citation format dropdown header label element</returns>
        public ILabel LabelByCitationFormatDropdownHeader()
            => new Label(By.XPath(this.CitationsTabMap[CitationsTab.CitationFormatDropdownHeader].LocatorString));

        /// <summary>
        /// Gets the citation format header element
        /// </summary>
        /// <returns>the citation format header element</returns>
        public ILabel LabelByCitationFormatHeader()
            => new Label(By.XPath(this.CitationsTabMap[CitationsTab.CitationFormatHeader].LocatorString));

        /// <summary>
        /// Gets label element for the specified citation style option
        /// </summary>
        /// <param name="citationStyleOption">the citation style option to look for</param>
        /// <returns>the label element</returns>
        public ILabel LabelByCitationStyle(CitationsTab citationStyleOption)
            => this.GetLabelText(citationStyleOption);

        /// <summary>
        /// Gets citation style dropdown header label element
        /// </summary>
        /// <returns>the citation style dropdown header label element</returns>
        public ILabel LabelByCitationStyleDropdownHeader()
            => new Label(By.XPath(this.CitationsTabMap[CitationsTab.CitationStyleDropdownHeader].LocatorString));

        /// <summary>
        /// Gets the citation style header element
        /// </summary>
        /// <returns>the citation style header element</returns>
        public ILabel LabelByCitationStyleHeader()
            => new Label(By.XPath(this.CitationsTabMap[CitationsTab.CitationStyleHeader].LocatorString));

        /// <summary>
        /// Gets label element for the specified citation formatting option
        /// </summary>
        /// <param name="citationFormattingOption">the citation formatting option to look for</param>
        /// <returns>the label element</returns>
        public ILabel LabelByCitationFormatting(CitationsTab citationFormattingOption)
            => this.GetLabelText(citationFormattingOption);

        /// <summary>
        /// Gets citation formatting checkbox header label element
        /// </summary>
        /// <returns>the citation formatting checkbox header label element</returns>
        public ILabel LabelByCitationFormattingCheckboxHeader()
            => new Label(By.XPath(this.CitationsTabMap[CitationsTab.CitationFormattingCheckboxHeader].LocatorString));

        /// <summary>
        /// Gets citation formatting header element
        /// </summary>
        /// <returns>the citation formatting header element</returns>
        public ILabel LabelByCitationFormattingHeader()
            => new Label(By.XPath(this.CitationsTabMap[CitationsTab.CitationFormattingHeader].LocatorString));

        /// <summary>
        /// Gets label element for the specified parallel citations option
        /// </summary>
        /// <param name="parallelCitationsOption">the parallel citations option to look for</param>
        /// <returns>the label element</returns>
        public ILabel LabelByParallelCitations(CitationsTab parallelCitationsOption)
            => this.GetLabelText(parallelCitationsOption);

        /// <summary>
        /// Gets parallel citations radio button header label element
        /// </summary>
        /// <returns>the parallel citations radio button header label element</returns>
        public ILabel LabelByParallelCitationsRadioButtonHeader()
            => new Label(By.XPath(this.CitationsTabMap[CitationsTab.ParallelCitationsRadioButtonHeader].LocatorString));

        /// <summary>
        /// Gets parallel citations header element
        /// </summary>
        /// <returns>the parallel citations header element</returns>
        public ILabel LabelByParallelCitationsHeader()
            => new Label(By.XPath(this.CitationsTabMap[CitationsTab.ParallelCitationsHeader].LocatorString));

        /// <summary>
        /// Gets the hyperlink section header element
        /// </summary>
        /// <returns>the hyperlink section header element</returns>
        public ILabel LabelByHyperlinkHeader()
            => new Label(By.XPath(this.CitationsTabMap[CitationsTab.HyperlinkHeader].LocatorString));

        /// <summary>
        /// Gets label element for the specified hyperlink option
        /// </summary>
        /// <param name="hyperlinkOption">the hyperlink option to look for</param>
        /// <returns>the label element</returns>
        public ILabel LabelByHyperLink(CitationsTab hyperlinkOption)
            => this.GetLabelText(hyperlinkOption);

        /// <summary>
        /// Gets label element for the specified additions option
        /// </summary>
        /// <param name="additionsOption">the additions option to look for</param>
        /// <returns>the label element</returns>
        public ILabel LabelByAdditions(CitationsTab additionsOption)
            => new Label(By.XPath($"{this.CitationsTabMap[additionsOption].LocatorString}/parent::label/span"));

        /// <summary>
        /// Gets the additions section header element
        /// </summary>
        /// <returns>the additions section header element</returns>
        public ILabel LabelByAdditionsHeader()
            => new Label(By.XPath(this.CitationsTabMap[CitationsTab.AdditionsHeader].LocatorString));

        /// <summary>
        /// Gets label element for the specified selected text option.
        /// Returns the first span element which contains the main text, excluding any subsequent spans that may contain "New" badges.
        /// </summary>
        /// <param name="selectedTextOption">the selected text option to look for</param>
        /// <returns>the label element</returns>
        public ILabel LabelBySelectedTextOptions(CitationsTab selectedTextOption)
        {
            return new Label(By.XPath($"{this.CitationsTabMap[selectedTextOption].LocatorString}/parent::label/span[1]"));
        }

        /// <summary>
        /// Gets the selected text options section header element
        /// </summary>
        /// <returns>the selected text options section header element</returns>
        public ILabel LabelBySelectedTextOptionsHeader()
            => new Label(By.XPath(this.CitationsTabMap[CitationsTab.SelectedTextOptionsHeader].LocatorString));

        /// <summary>
        /// Gets label element for the specified new icon option
        /// </summary>
        /// <param name="newIconOption">the new icon option to look for</param>
        /// <returns>the label element</returns>
        public ILabel LabelByNewIcon(CitationsTab newIconOption)
            => new Label(By.XPath(this.CitationsTabMap[newIconOption].LocatorString));

        /// <summary>
        /// Gets the citation tab title element
        /// </summary>
        /// <returns>the citation tab title element</returns>
        public ILabel LabelByCitationTabTitle()
            => new Label(By.XPath(this.CitationsTabMap[CitationsTab.CitationTabTitle].LocatorString));

        /// <summary>
        /// Gets label text element that supports both HTML structures (direct text in label or text within span element).
        /// </summary>
        /// <param name="option">the option to look for</param>
        /// <returns>the label text element</returns>
        private ILabel GetLabelText(CitationsTab option)
        {
            // Use union operator to support both HTML structures: span element within label or direct text in label
            return new Label(By.XPath($"({this.CitationsTabMap[option].LocatorString}/parent::label/span | {this.CitationsTabMap[option].LocatorString}/parent::label)"));
        }
    }
}