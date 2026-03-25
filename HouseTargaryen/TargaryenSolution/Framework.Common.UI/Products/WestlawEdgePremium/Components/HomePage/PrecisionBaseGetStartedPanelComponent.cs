namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.HomePage
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.WestlawEdgePremium.Dialogs;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Base Get Started panel component
    /// </summary>
    public abstract class PrecisionBaseGetStartedPanelComponent : BaseTabComponent
    {
        private const string CheckboxLctMask = ".//*[contains(@class,'PrecisionSearch-labelText') and text()='{0}']/../input";

        private static readonly By BackToTopButtonLocator = By.Id("co_backToTop");
        private static readonly By ZeroStateLabelLocator = By.XPath(".//*[@class='PrecisionSearchModal-zeroState']");
        private static readonly By OptionLabelLocator = By.XPath(".//*[@class='PrecisionSearch-label PrecisionSearch-labelText']");
        private static readonly By GetStartedToolsOptionsLocator = By.XPath("//div[@id='panel_Tools']//label[contains(@class,'PrecisionSearch-labelText')]");

        /// <summary>
        /// Back to top button
        /// </summary>
        public IButton BackToTopButton => new Button(BackToTopButtonLocator);

        /// <summary>
        /// Zero state label
        /// </summary>
        public ILabel ZeroStateLabel => new Label(this.ComponentLocator, ZeroStateLabelLocator);

        /// <summary>
        /// Get a list Get Started Tools link options
        /// </summary>
        /// <returns>A list of Get Started options</returns>
        public List<string> GetStartedToolsOptionsLinks =>
            DriverExtensions
                .GetElements(new ByChained(this.ComponentLocator, GetStartedToolsOptionsLocator))
                .Select(e => (e.Text ?? string.Empty).Trim())
                .Where(t => !string.IsNullOrWhiteSpace(t))
                .Distinct()
                .ToList();

        /// <summary>
        /// Select Get Started tab's option
        /// </summary>
        /// <param name="option">the checkbox to select</param>
        /// <param name="isSet">True to check, false to uncheck</param>
        public PrecisionGetStartedDialog SetCheckboxByOptionName(string option, bool isSet = true)
        {
            DriverExtensions.SetCheckbox(new ByChained(this.ComponentLocator, By.XPath(string.Format(CheckboxLctMask, option))), isSet);
            return DriverExtensions.CreatePageInstance<PrecisionGetStartedDialog>();
        }

        /// <summary>
        /// Returns all option (checkbox) display texts in the current Get Started tab.
        /// Filters out headings by requiring a sibling checkbox input.
        /// </summary>
        /// <param name="onlyVisible">If true, include only displayed elements.</param>
        /// <returns>Distinct list of option names.</returns>
        public IList<string> GetAllOptionNames(bool onlyVisible = true)
        {
            var labels = DriverExtensions.GetElements(new ByChained(this.ComponentLocator, OptionLabelLocator));
            var results = new List<string>();

            foreach (var label in labels)
            {
                if (onlyVisible && !label.Displayed)
                    continue;

                // Ensure associated checkbox exists as sibling
                var checkboxes = label.FindElements(By.XPath("./../input[@type='checkbox']"));
                if (!checkboxes.Any())
                    continue;

                var text = (label.Text ?? string.Empty).Trim();
                if (string.IsNullOrWhiteSpace(text))
                    continue;

                if (!results.Contains(text))
                    results.Add(text);
            }

            return results;
        }        
    }
}

