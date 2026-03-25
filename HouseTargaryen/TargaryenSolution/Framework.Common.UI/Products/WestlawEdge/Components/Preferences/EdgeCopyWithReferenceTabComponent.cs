namespace Framework.Common.UI.Products.WestlawEdge.Components.Preferences
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Preferences;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Copy  with reference Tab Component
    /// </summary>
    public class EdgeCopyWithReferenceTabComponent : BaseTabComponent
    {
        private static readonly By ContainerLocator = By.Id("panel_Citations");

        /// <summary>
        /// Tab name
        /// </summary>
        protected override string TabName => " ";

        private EnumPropertyMapper<EdgeCopyWithReferenceTab, WebElementInfo> copyWithRefTabMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the copy with reference enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<EdgeCopyWithReferenceTab, WebElementInfo> CopyWithTabMap =>
                   this.copyWithRefTabMap = this.copyWithRefTabMap
                                            ?? EnumPropertyModelCache.GetMap<EdgeCopyWithReferenceTab, WebElementInfo>(
                                                string.Empty,
                                                @"Resources/EnumPropertyMaps/WestlawEdge/Preferences");

        /// <summary>
        /// Returns the value of the specified dropdown
        /// </summary>
        /// <param name="deliveryTabOption">the dropdown to look for</param>
        /// <returns>the selected dropdown option</returns>
        public string GetSelectedDropdownValue(EdgeCopyWithReferenceTab deliveryTabOption)
            => DriverExtensions.GetSelectedDropdownOptionText(By.XPath(this.CopyWithTabMap[deliveryTabOption].LocatorString));

        /// <summary>
        /// Sets the specified dropdown on the copy with reference tab to the specified value.
        /// </summary>
        /// <param name="tabOption"> the option to look for </param>
        /// <param name="option"> What to select from the dropdown. </param>
        /// <returns> The <see cref="EdgeCopyWithReferenceTabComponent"/>copy with reference </returns>
        public EdgeCopyWithReferenceTabComponent SetDropdown(EdgeCopyWithReferenceTab tabOption, string option)
        {
            DriverExtensions.SetDropdown(option, By.XPath(this.CopyWithTabMap[tabOption].LocatorString));
            return this;
        }

        /// <summary>
        /// Selects the specified radio-button option on the copy with reference tab.
        /// </summary>
        /// <param name="tabOption"> the option to look for </param>
        /// <returns> The <see cref="EdgeCopyWithReferenceTabComponent"/>copy with reference tab</returns>
        public EdgeCopyWithReferenceTabComponent ToggleRadioButton(EdgeCopyWithReferenceTab tabOption)
        {
            DriverExtensions.GetElement(By.XPath(this.CopyWithTabMap[tabOption].LocatorString)).Click();
            return this;
        }

        /// <summary>
        /// Sets the specified checkbox option on the copy with reference tab to the specified value.
        /// </summary>
        /// <param name="tabOption"> the option to look for </param>
        /// <param name="setTo"> What to set the checkbox to. True for checked, false for unchecked. </param>
        /// <returns> The <see cref="EdgeCopyWithReferenceTabComponent"/>copy with reference tab </returns>
        public EdgeCopyWithReferenceTabComponent SetCheckbox(EdgeCopyWithReferenceTab tabOption, bool setTo)
        {
            DriverExtensions.SetCheckbox(setTo, By.XPath(this.CopyWithTabMap[tabOption].LocatorString));
            return this;
        }

        /// <summary>
        /// Returns true if the specified element on the copy with reference tab is selected (checked for checkboxes)
        /// </summary>
        /// <param name="tabOption">the option to look for</param>
        /// <returns>true if selected, false otherwise</returns>
        public bool IsCheckboxSelected(EdgeCopyWithReferenceTab tabOption)
            => DriverExtensions.GetElement(By.XPath(this.CopyWithTabMap[tabOption].LocatorString)).Selected;

        /// <summary>
        /// Gets line options radio button element for the specified option
        /// </summary>
        /// <param name="lineOption">the line option to look for</param>
        /// <returns>the line options radio button element</returns>
        public IRadiobutton RadioButtonByLineOptions(EdgeCopyWithReferenceTab lineOption)
            => new Radiobutton(By.XPath(this.CopyWithTabMap[lineOption].LocatorString));

        /// <summary>
        /// Gets label element for the specified line option
        /// </summary>
        /// <param name="lineOption">the line option to look for</param>
        /// <returns>the label element</returns>
        public ILabel LabelByLineOptions(EdgeCopyWithReferenceTab lineOption)
            => new Label(By.XPath($"{this.CopyWithTabMap[lineOption].LocatorString}/parent::label/span"));

        /// <summary>
        /// Gets add checkbox element for the specified option
        /// </summary>
        /// <param name="addOption">the add option to look for</param>
        /// <returns>the add checkbox element</returns>
        public ICheckBox CheckboxByAdd(EdgeCopyWithReferenceTab addOption)
            => new CheckBox(By.XPath(this.CopyWithTabMap[addOption].LocatorString));

        /// <summary>
        /// Gets label element for the specified add option
        /// </summary>
        /// <param name="addOption">the add option to look for</param>
        /// <returns>the label element</returns>
        public ILabel LabelByAdd(EdgeCopyWithReferenceTab addOption)
            => new Label(By.XPath($"{this.CopyWithTabMap[addOption].LocatorString}/parent::label/span"));

        /// <summary>
        /// Gets the add section header element
        /// </summary>
        /// <returns>the add section header element</returns>
        public ILabel LabelByAddHeader()
            => new Label(By.XPath(this.CopyWithTabMap[EdgeCopyWithReferenceTab.AddHeader].LocatorString));

        /// <summary>
        /// Gets the line options section header element
        /// </summary>
        /// <returns>the line options section header element</returns>
        public ILabel LabelByLineOptionsHeader()
            => new Label(By.XPath(this.CopyWithTabMap[EdgeCopyWithReferenceTab.LineOptionsHeader].LocatorString));
    }
}