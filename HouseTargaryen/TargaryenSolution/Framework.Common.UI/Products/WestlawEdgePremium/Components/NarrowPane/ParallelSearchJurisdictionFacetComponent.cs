namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.NarrowPane
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.NarrowPanel;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Parallel Search Jurisdiction Facet
    /// </summary>
    public class ParallelSearchJurisdictionFacetComponent : NewEdgeRecentFiltersFacetComponent
    {
        private static readonly By JurisdictionComponentLocator = By.XPath("//saf-facet-category[@id='jurisdiction']");
        private static readonly By FederalComponentLocator = By.XPath("//saf-facet-item[@id='Federal']");
        private const string FederalCheckboxScript = "return(arguments[0].shadowRoot.querySelector('saf-checkbox'));";
        private const string FederalClickScript = "return(arguments[0].shadowRoot.querySelector('input[id=control]'));";
        private const string FederalCountScript = "return(arguments[0].shadowRoot.querySelector('span[class=number-of-items]'));";
        private static readonly By StateComponentLocator = By.XPath("//saf-facet-item[@id='State']");
        private static readonly By SubStateLocator = By.XPath("//saf-facet-item[contains(@id, 'State|**|')]");
        private const string StateExpandScript = "return(arguments[0].shadowRoot.querySelector('saf-button'));";
        private const string StateSelectScript = "return(arguments[0].shadowRoot.querySelector('input[id=control]'));";
        private const string StateCheckboxScript = "return(arguments[0].shadowRoot.querySelector('saf-checkbox'));";
        
        ///<Summary>
        ///Jurisdiction component locator
        ///</Summary>
        protected override By ComponentLocator => JurisdictionComponentLocator;

        /// <summary>
        /// Click federal checkbox
        /// </summary>     
        public void ClickFederalCheckbox()
        {
            IWebElement federalElement = DriverExtensions.GetElement(this.ComponentLocator, FederalComponentLocator);
            IWebElement federalField = (IWebElement)DriverExtensions.ExecuteScript(FederalCheckboxScript, federalElement);
            IWebElement federalCheckbox = (IWebElement)DriverExtensions.ExecuteScript(FederalClickScript, federalField);
            federalCheckbox.Click();
        }
        
        /// <summary>
        /// Click first option under State filter and get the selected State value
        /// </summary>
        public string ClickFirstStateCheckbox()
        {
            StateFilterExpand();
            IWebElement stateElement = DriverExtensions.GetElement(this.ComponentLocator, SubStateLocator);
            IWebElement stateField = (IWebElement)DriverExtensions.ExecuteScript(StateCheckboxScript, stateElement);
            IWebElement stateCheckbox = (IWebElement)DriverExtensions.ExecuteScript(StateSelectScript, stateField);
            stateCheckbox.Click();
            return stateCheckbox.Text;
        }

        /// <summary>
        /// Expand state filter
        /// </summary>
        public void StateFilterExpand()
        {
            IWebElement stateElement = DriverExtensions.GetElement(this.ComponentLocator, StateComponentLocator);
            IWebElement stateField = (IWebElement)DriverExtensions.ExecuteScript(StateExpandScript, stateElement);
            stateField.Click();
        }

        /// <summary>
        /// Get number of federal items displaying next to federal filter
        /// </summary>
        public string FederalCount()
        {
            IWebElement federalElement = DriverExtensions.GetElement(this.ComponentLocator, FederalComponentLocator);
            IWebElement federalField = (IWebElement)DriverExtensions.ExecuteScript(FederalCountScript, federalElement);
            return federalField.Text;
        }
    }
}