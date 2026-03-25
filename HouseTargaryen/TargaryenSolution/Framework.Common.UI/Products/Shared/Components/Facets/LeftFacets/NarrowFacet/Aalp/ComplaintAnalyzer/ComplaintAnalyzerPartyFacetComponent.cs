namespace Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet.AALP.ComplaintAnalyzer
{
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Party Facet Component
    /// </summary>
    public class ComplaintAnalyzerPartyFacetComponent : BaseLinkFacetComponent
    {
        private static readonly By FilterButtonLocator = By.XPath(".//saf-faceted-filter-v3[@data-testid='claims-filters'] | .//saf-faceted-filter[@data-testid='claims-filters']");
        private static readonly By FiltersCheckBoxesLabelLocator = By.XPath(".//*[contains(@class, 'ClaimsFilters-module__claimFilter')]//saf-checkbox-v3//input | .//*[contains(@class, 'ClaimsFilters-module__claimFilter')]//saf-checkbox//input");
        private static readonly By FiltersCheckBoxesLocator = By.XPath(".//*[contains(@class, 'ClaimsFilters-module__claimFilter')]//saf-checkbox-v3 | .//*[contains(@class, 'ClaimsFilters-module__claimFilter')]//saf-checkbox");
        
        private const string PartyFilterTypeCheckboxScript = "return(arguments[0].shadowRoot.querySelector('input[id=control]'));";
        private const string FilterTypeLctMask = "*[current-value='{0}']";
        
        /// <summary>
        /// Complaint Analyser Filter Facet
        /// </summary>
        public ComplaintAnalyzerPartyFacetComponent(By componentLocator)
        {
            Container = componentLocator;
        }

        /// <summary>
        /// Filter button web element
        /// </summary>
        public IButton FilterButton => new Button(ComponentLocator, FilterButtonLocator);

        /// <summary>
        /// Get list of Filter Check Boxes
        /// </summary>
        public IReadOnlyCollection<ICheckBox> FiltersCheckBoxes => new ElementsCollection<CheckBox>(ComponentLocator, FiltersCheckBoxesLocator);

        /// <summary>
        /// Get list of Filter Check Labels
        /// </summary>
        public IReadOnlyCollection<ICheckBox> FiltersCheckBoxesLabel => new ElementsCollection<CheckBox>(ComponentLocator, FiltersCheckBoxesLabelLocator);

        /// <summary>
        /// Clears all selected checkboxes
        /// </summary>
        public void ClearAllPartySelection()
        {
            foreach (var checkbox in DriverExtensions.GetElements(ComponentLocator, FiltersCheckBoxesLabelLocator).Where(item => item.GetAttribute("current-checked").Equals("true")).ToList())
            {
                var partyCheckbox = (IWebElement)DriverExtensions.ExecuteScript(PartyFilterTypeCheckboxScript, checkbox);
                partyCheckbox.Click();
            }
        }

        /// <summary>
        /// Selects the parties in claims or event tabs
        ///</summary>
        /// <param name="clearAllOtherParties">
        /// True to clear the other party other than what is passed in, false to leave them there
        /// </param>
        /// <param name="option">content value of checkbox to be selected.Starts from 0</param>
        /// <returns>The <see cref="ComplaintAnalyzerPartyFacetComponent"/>.</returns>
        public ComplaintAnalyzerPartyFacetComponent SelectParty(bool clearAllOtherParties, string option)
        {
            if (clearAllOtherParties == true)
            {
                ClearAllPartySelection();
            }

            IWebElement filterOptionElement = null;
            
            filterOptionElement = DriverExtensions.GetElement(ComponentLocator, By.CssSelector(string.Format(FilterTypeLctMask, option)));
            
            IWebElement partyFilterTypeCheckbox = (IWebElement)DriverExtensions.ExecuteScript(PartyFilterTypeCheckboxScript, filterOptionElement);
            partyFilterTypeCheckbox.Click();

            return this;
        }

        /// <summary>
        /// Complaint Analyser Filter Facet
        /// </summary>
        protected override By ComponentLocator => Container;

        private readonly By Container;
    }
}
