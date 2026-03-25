namespace Framework.Common.UI.Products.WestLawNext.DropDowns
{
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.WestLawNext.Enums.BusinessLawCenter;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// RedlineViewModeDropdown on Redline Toolbar
    /// </summary>
    public class RedlineViewModeDropdown : Dropdown<ViewModeOptions>
    {
        private const string OptionLctMask = "//select[@data-ng-model='selectedMode']//option[contains(.,'{0}')]";

        /// <summary>
        /// Initializes a new instance of the <see cref="RedlineViewModeDropdown"/> class.
        /// </summary>
        /// <param name="locatorBys">
        /// The locator bys.
        /// </param>
        public RedlineViewModeDropdown(params By[] locatorBys) : base(locatorBys)
        {
        }

        /// <summary>
        /// Select option
        /// </summary>
        /// <param name="option"> Option to select </param>
        public override void SelectOption(ViewModeOptions option)
        {
            string optionText = this.GetOptionText(option);
            this.GetContainer().Click();
            DriverExtensions.WaitForElement(By.XPath(string.Format(OptionLctMask, optionText))).Click();
        }

        /// <summary>
        /// Get option by name
        /// </summary>
        /// <param name="text"> Option's text </param>
        /// <returns> Option </returns>
        protected override ViewModeOptions GetOptionByText(string text)
            => text.GetSubstringBeforeSpecialCharacter('(').Trim().GetEnumValueByText<ViewModeOptions>();
    }
}
