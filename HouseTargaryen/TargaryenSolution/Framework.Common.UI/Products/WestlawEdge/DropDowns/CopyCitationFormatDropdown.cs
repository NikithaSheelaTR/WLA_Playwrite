namespace Framework.Common.UI.Products.WestlawEdge.DropDowns
{
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// CopyCitationFormatDropdown
    /// </summary>
    public class CopyCitationFormatDropdown : BaseModuleRegressionCustomDropdown<string>
    {
        private static readonly By CitationFormatDropdown = By.Id("co_copyCitationFormat_select");
        private static readonly By FormatOptionLocator = By.XPath(".//option[contains(@id,'co_citationFormat')]");

        /// <summary>
        /// Selected Option
        /// </summary>
        public override string SelectedOption => DriverExtensions
                                             .GetElements(this.Dropdown, FormatOptionLocator)
                                             .First(format => format.Selected).Text;

        /// <summary>
        /// Is Selected
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public override bool IsSelected(string option) =>
            DriverExtensions
                .GetElements(this.Dropdown, FormatOptionLocator)
                .First(format => format.Text.Equals(option)).Selected;

        /// <summary>
        /// Select Option From Expanded Dropdown
        /// </summary>
        /// <param name="option"></param>
        protected override void SelectOptionFromExpandedDropdown(string option)
        {
            DriverExtensions.GetElements(this.Dropdown, FormatOptionLocator).First(format => format.Text.Equals(option)).Click();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// OptionsFromExpandedDropdown
        /// </summary>
        protected override IEnumerable<string> OptionsFromExpandedDropdown => DriverExtensions
            .GetElements(this.Dropdown, FormatOptionLocator).Select(format => format.Text).ToList();

        /// <summary>
        /// Dropdown
        /// </summary>
        protected override IWebElement Dropdown => DriverExtensions.GetElement(CitationFormatDropdown);

        /// <summary>
        /// ClickDropdownArrow
        /// </summary>
        protected override void ClickDropdownArrow() => this.Dropdown.Click();
    }
}
