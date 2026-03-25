namespace Framework.Common.UI.Products.WestLawNextCanada.Pages.Quantum
{
    using System.Collections.Generic;    
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.WestLawNext.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Components.Toolbar;
    using Framework.Common.UI.Products.WestlawEdge.Components;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;

    /// <summary>
    /// Represents a page containing fields for advanced search in a quantum service.
    /// </summary>
    public class QuantumFieldsPage : CommonAdvancedSearchPage
    {
        private static readonly By InjuryLocator = By.Id("co_search_advancedSearch_PIOI");

        private static readonly By AgeLocator = By.Id("co_search_advancedSearch_AGE");

        private static readonly By JurisdictionDropdownLocator = By.Id("co_search_advancedSearch_JUR");

        private static readonly By DateDropdownListLocator = By.XPath(".//ul[contains(@class, 'a11yDateWidget-options')]//input[@type='radio']/following-sibling::label");

        private static readonly By DateWidgetDropdownArrowLocator = By.Id("co_dateWidget_DA");

        private static readonly By TypeOfContractLocator = By.CssSelector("#co_search_advancedSearch_SUJ");

        private static readonly By PrimaryCustodianLocator = By.Id("co_search_advancedSearch_CUST");

        private static readonly By IncomeOfPayorLocator = By.Id("co_search_advancedSearch_PAYR");

        private static readonly By IncomeOfPayeeLocator = By.Id("co_search_advancedSearch_PAYE");

        private static readonly By NumberOfChildrenLocator = By.Id("co_search_advancedSearch_CHLD");

        private static readonly By NatureOfPublicationLocator = By.Id("co_search_advancedSearch_PUB");

        private static readonly By LengthOfRelationshipLocator = By.Id("co_search_advancedSearch_LR");

        private static readonly By EstatesAssetsLocator = By.Id("co_search_advancedSearch_EST");

        private static readonly By ClaimantsAssetsLocator = By.Id("co_search_advancedSearch_CLMT");

        private static readonly By AggravatingFactorsLocator = By.Id("co_search_advancedSearch_AF");

        private static readonly By MitigatingFactorsLocator = By.Id("co_search_advancedSearch_MF");

        private static readonly By YearsOfEmploymentLocator = By.Id("co_search_advancedSearch_EMP");

        private static readonly By RecentDocumentsLinkLocator = By.XPath(".//li[@id='recentResearch_item_0']/div[1]");

        private static readonly By SelectAllResultsCheckBoxLocator = By.Id("co_searchHeader_selectAll");

        private static readonly By ClearAllTopLinkLocator = By.Id("co_search_advancedSearchClearLink_bottom");

        private static readonly By ContinueButtonLocator = By.XPath("//button[contains(@class, 'co_primaryBtn')]");

        /// <summary>
        /// Click on the RecentDocumentsLink
        /// </summary>
        public ILink RecentDocumentsLink => new Link(RecentDocumentsLinkLocator);

        /// <summary>
        /// Click ClearAllFieldsLink
        /// </summary>
        public ILink ClearAllFieldsLink => new Link(ClearAllTopLinkLocator);

        /// <summary>
        /// Select all checkbox
        /// </summary>
        public ICheckBox SelectAllResultsCheckBox => new CheckBox(SelectAllResultsCheckBoxLocator);

        /// <summary>
        /// Click on the ContinueButton 
        /// </summary>
        public IButton ContinueButton => new Button(ContinueButtonLocator);

        /// <summary>
        /// Delivery Widget
        /// </summary>
        public DeliveryDropdown DeliveryDropdown { get; } = new DeliveryDropdown();

        /// <summary> Toolbar component  </summary>
        public Toolbar Toolbar { get; } = new Toolbar();

        /// <summary>
        /// Header
        /// </summary>
        public new EdgeHeaderComponent Header { get; } = new EdgeHeaderComponent();

        /// <summary>
        /// Selects multiple options in a dropdown menu for TypeofContractLocator.
        /// </summary>
        /// <param name="options">An array of strings representing the options to be selected.</param>
        public void SelectTypeOfContractMultipleOptions(params string[] options)
        {
            // Find the dropdown element using the locator
            var dropdownElement = DriverExtensions.GetElement(TypeOfContractLocator);
            this.SelectDropdownMultipleOptions(dropdownElement, options);
        }

        /// <summary>
        /// Selects multiple options in a dropdown menu for lengthOfRelationship.
        /// </summary>
        /// <param name="options">An array of strings representing the options to be selected.</param>
        public void SelectNumberOfChildrenOptions(params string[] options)
        {
            // Find the dropdown element using the locator
            var dropdownElement = DriverExtensions.GetElement(NumberOfChildrenLocator);
            this.SelectDropdownMultipleOptions(dropdownElement, options);
        }

        /// <summary>
        /// Selects multiple options in a dropdown menu for lengthOfRelationship.
        /// </summary>
        /// <param name="options">An array of strings representing the options to be selected.</param>
        public void SelectPrimaryCustodianOptions(params string[] options)
        {
            // Find the dropdown element using the locator
            var dropdownElement = DriverExtensions.GetElement(PrimaryCustodianLocator);
            this.SelectDropdownMultipleOptions(dropdownElement, options);
        }

        /// <summary>
        /// Selects multiple options in a dropdown menu for lengthOfRelationship.
        /// </summary>
        /// <param name="options">An array of strings representing the options to be selected.</param>
        public void SelectNatureOfPublicationMultipleOptions(params string[] options)
        {
            // Find the dropdown element using the locator
            var dropdownElement = DriverExtensions.GetElement(NatureOfPublicationLocator);
            this.SelectDropdownMultipleOptions(dropdownElement, options);
        }

        /// <summary>
        /// Selects multiple options in a dropdown menu for lengthOfRelationship.
        /// </summary>
        /// <param name="options">An array of strings representing the options to be selected.</param>
        public void SelectLengthOfRelationshipMultipleOptions(params string[] options)
        {
            // Find the dropdown element using the locator
            var dropdownElement = DriverExtensions.GetElement(LengthOfRelationshipLocator);
            this.SelectDropdownMultipleOptions(dropdownElement, options);
        }

        /// <summary>
        /// Selects multiple options in a dropdown menu for estatesAssets.
        /// </summary>
        /// <param name="options">An array of strings representing the options to be selected.</param>
        public void SelectEstatesAssetsMultipleOptions(params string[] options)
        {
            // Find the dropdown element using the locator
            var dropdownElement = DriverExtensions.GetElement(EstatesAssetsLocator);
            this.SelectDropdownMultipleOptions(dropdownElement, options);
        }

        /// <summary>
        /// Selects multiple options in a dropdown menu for claimantsAssets.
        /// </summary>
        /// <param name="options">An array of strings representing the options to be selected.</param>
        public void SelectClaimantsAssetsMultipleOptions(params string[] options)
        {
            // Find the dropdown element using the locator
            var dropdownElement = DriverExtensions.GetElement(ClaimantsAssetsLocator);
            this.SelectDropdownMultipleOptions(dropdownElement, options);
        }

        /// <summary>
        /// Selects multiple options in a dropdown menu for aggravatingFactors.
        /// </summary>
        /// <param name="options">An array of strings representing the options to be selected.</param>
        public void SelectAggravatingFactorsMultipleOptions(params string[] options)
        {
            // Find the dropdown element using the locator
            var dropdownElement = DriverExtensions.GetElement(AggravatingFactorsLocator);
            this.SelectDropdownMultipleOptions(dropdownElement, options);
        }

        /// <summary>
        /// Selects multiple options in a dropdown menu for mitigatingFactors.
        /// </summary>
        /// <param name="options">An array of strings representing the options to be selected.</param>
        public void SelectMitigatingFactorsMultipleOptions(params string[] options)
        {
            // Find the dropdown element using the locator
            var dropdownElement = DriverExtensions.GetElement(MitigatingFactorsLocator);
            this.SelectDropdownMultipleOptions(dropdownElement, options);
        }

        /// <summary>
        /// Selects multiple options in a dropdown menu for Injuryselector.
        /// </summary>
        /// <param name="options">An array of strings representing the options to be selected.</param>
        public void SelectInjuryMultipleOptions(params string[] options)
        {
            // Find the dropdown element using the locator
            var dropdownElement = DriverExtensions.GetElement(InjuryLocator);
            this.SelectDropdownMultipleOptions(dropdownElement, options);
        }

        /// <summary>
        /// Selects multiple options in a dropdown menu for Ageselector.
        /// </summary>
        /// <param name="options">An array of strings representing the options to be selected.</param>
        public void SelectAgeMultipleOptions(params string[] options)
        {
            // Find the dropdown element using the locator
            var dropdownElement = DriverExtensions.GetElement(AgeLocator);
            this.SelectDropdownMultipleOptions(dropdownElement, options);
        }

        /// <summary>
        /// Selects multiple options in a dropdown menu for NumberOfChildren.
        /// </summary>
        /// <param name="options">An array of strings representing the options to be selected.</param>
        public void SelectNumberOfChildrenMultipleOptions(params string[] options)
        {
            // Find the dropdown element using the locator
            var dropdownElement = DriverExtensions.GetElement(NumberOfChildrenLocator);
            this.SelectDropdownMultipleOptions(dropdownElement, options);
        }

        /// <summary>
        /// Selects multiple options in a dropdown menu for incomeOfPayor.
        /// </summary>
        /// <param name="options">An array of strings representing the options to be selected.</param>
        public void SelectIncomeOfPayorMultipleOptions(params string[] options)
        {
            // Find the dropdown element using the locator
            var dropdownElement = DriverExtensions.GetElement(IncomeOfPayorLocator);
            this.SelectDropdownMultipleOptions(dropdownElement, options);
        }

        /// <summary>
        /// Selects multiple options in a dropdown menu for incomeOfPayee.
        /// </summary>
        /// <param name="options">An array of strings representing the options to be selected.</param>
        public void SelectIncomeOfPayeeMultipleOptions(params string[] options)
        {
            // Find the dropdown element using the locator
            var dropdownElement = DriverExtensions.GetElement(IncomeOfPayeeLocator);
            this.SelectDropdownMultipleOptions(dropdownElement, options);
        }

        /// <summary>
        /// Selects multiple options in a dropdown menu for yearsOfEmployment.
        /// </summary>
        /// <param name="options">An array of strings representing the options to be selected.</param>
        public void SelectYearsOfEmploymentMultipleOptions(params string[] options)
        {
            // Find the dropdown element using the locator
            var dropdownElement = DriverExtensions.GetElement(YearsOfEmploymentLocator);
            this.SelectDropdownMultipleOptions(dropdownElement, options);
        }

        /// <summary>
        /// Selects multiple options in a dropdown menu for jurisdiction.
        /// </summary>
        /// <param name="options">An array of strings representing the options to be selected.</param>
        public void SelectJurisdictionMultipleOptions(params string[] options)
        {
            var JurisdictiondropdownElement = DriverExtensions.GetElement(JurisdictionDropdownLocator);
            this.SelectDropdownMultipleOptions(JurisdictiondropdownElement, options);
        }

        /// <summary>
        /// Selects multiple options in a dropdown menu.
        /// </summary>
        protected void SelectDropdownMultipleOptions(IWebElement dropdownMenu, string[] options)
        {
            dropdownMenu.SendKeys("");

            dropdownMenu.SendKeys(options[0]);

            foreach (string option in options)
            {
                new SelectElement(dropdownMenu).SelectByText(option);
            }
        }        

        /// <summary>
        /// Selects an option from a date dropdown.
        /// </summary>
        public void SelectDateOption(params string[] dates)
        {
            if (dates.Length == 1)
            {
                this.SelectFixedDateOption(dates[0]);
            }
        }

        /// <summary>
        /// Selects an option from a FixedDatedropdown
        /// </summary>
        protected void SelectFixedDateOption(string dates)
        {

            DriverExtensions.WaitForElementDisplayed(DateWidgetDropdownArrowLocator).Click();

            IReadOnlyCollection<IWebElement> links = DriverExtensions.GetElements(DateDropdownListLocator);
            List<string> appliedFilterList = new List<string>();

            foreach (var link in links)
            {
                appliedFilterList.Add(link.Text);

                if (link.Text.Trim().Equals(dates.Trim()))
                {
                    // Use DriverExtensions to click the link
                    DriverExtensions.Click(link);
                    ContinueButton.Click();
                    break;
                    // Optionally, add a wait here if there's any dynamic content loading after the click
                }
            }
        }      
    }
}












