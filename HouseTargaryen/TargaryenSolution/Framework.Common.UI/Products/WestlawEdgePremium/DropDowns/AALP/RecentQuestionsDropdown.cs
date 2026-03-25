namespace Framework.Common.UI.Products.WestlawEdgePremium.DropDowns.AALP
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Products.Shared.Elements.Labels;

    /// <summary>
    /// Recent questions dropdown
    /// </summary>
    public class RecentQuestionsDropdown : BaseModuleRegressionCustomDropdown<string>
    {
        private static readonly By DropdownLocator = By.XPath("//div[contains(@class, 'CS-recent-search-dropdown-container')]");
        private static readonly By RecentQuestionLocator = By.XPath(".//*[@class='CS-recent-search-dropdown']//li");
        private static readonly By RecentQuestionsButtonLocator = By.XPath(".//button[contains(@class,'CS-recent-search-dropdown-button')]");
        private static readonly By ZeroStateLabelLocator = By.XPath(".//*[@class='CS-recent-search-dropdown-zeroState']");

        /// <summary>
        /// Throw exception, because there is no selected option
        /// </summary>
        public override string SelectedOption => throw new NotImplementedException("Can't get selected item for the Recent questions dropdown");

        /// <summary>
        /// Zero state label
        /// </summary>
        public ILabel ZeroStateLabel => new Label(this.Dropdown, ZeroStateLabelLocator);

        /// <summary>
        /// Dropdown element
        /// </summary>
        protected override IWebElement Dropdown => DriverExtensions.SafeGetElement(DropdownLocator);

        /// <summary>
        /// Get recent questions
        /// </summary>
        protected override IEnumerable<string> OptionsFromExpandedDropdown => DriverExtensions.GetElements(this.Dropdown, RecentQuestionLocator).Select(el => el.GetText()).ToList();

        /// <summary>
        /// Recent questions button
        /// </summary>
        private IButton RecentQuestionsButton => new Button(this.Dropdown, RecentQuestionsButtonLocator);

        /// <summary>
        /// Throw exception, because there is no selected option
        /// </summary>
        /// <param name="option"></param>
        /// <returns>Throw exception, because there is no selected option</returns>
        public override bool IsSelected(string option) => throw new NotImplementedException("Can't get selected item for the Recent questions dropdown");

        /// <summary>
        /// Is dropdown enabled
        /// </summary>
        /// <returns>True - if it is enabled, false - otherwise</returns>
        public bool IsDropdownEnabled() => !this.RecentQuestionsButton.GetAttribute("class").Contains("disabled");

        /// <summary>
        /// Is dropdown displayed
        /// </summary>
        /// <returns>True - if it is displayed, false - otherwise</returns>
        public override bool IsDisplayed() => this.RecentQuestionsButton.Displayed;

        /// <summary>
        /// Select option from expanded dropdown
        /// </summary>
        /// <param name="option"> The option. </param>
        protected override void SelectOptionFromExpandedDropdown(string option) =>
            DriverExtensions.GetElements(this.Dropdown, RecentQuestionLocator).FirstOrDefault(link => link.GetText().Equals(option)).Click();

        /// <summary>
        /// Click Recent questions button
        /// </summary>
        protected override void ClickDropdownArrow()
        {
            this.RecentQuestionsButton.Click();      
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Is dropdown expanded
        /// </summary>
        /// <returns>True - if it is expanded, false - otherwise</returns>
        protected override bool IsDropdownExpanded()
        {
            string dropdownClass = this.RecentQuestionsButton.GetAttribute("aria-expanded");
            return dropdownClass.Contains("true");
        }
    }
}
