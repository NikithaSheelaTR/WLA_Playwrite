namespace Framework.Common.UI.Products.WestlawAdvantage.Components.AiJurisdictionalSurveys
{
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// WLA AI Jurisdictional Surveys Query box component
    /// </summary>
    public class WlaAjsQueryBoxComponent : AiJurisdictionalSurveysQueryBoxComponent
    {
        private static readonly By IncludeCasesCheckboxLocator = By.XPath(".//saf-checkbox[contains(@class,'checkbox--YwWgEE4YlyhJDHoZZlD8')]");
        private const string IncludeCasesCheckedScript = "return arguments[0].shadowRoot.querySelector('input[id=control]').getAttribute('aria-checked');";
        private const string IncludeCasesClickScript = "arguments[0].shadowRoot.querySelector('input[id=control]').click();";


        /// <summary>
        /// Check if Include cases in state juris is selected
        /// </summary>
        /// <returns>True if selected, else false</returns>
        public bool IsIncludeCasesSelected()
        {
            IWebElement includeCasesElement = DriverExtensions.GetElement(IncludeCasesCheckboxLocator);
            var ariaChecked = (string)DriverExtensions.ExecuteScript(IncludeCasesCheckedScript, includeCasesElement);

            return ariaChecked != null && ariaChecked.Contains("true");
        }

        /// <summary>
        /// Select Include cases in state jurisdictions along with statutes and regulations (if it's not already selected)
        /// </summary>
        public void SelectIncludeCases()
        {
            if (!IsIncludeCasesSelected())
            {
                IWebElement includeCasesElement = DriverExtensions.GetElement(IncludeCasesCheckboxLocator);
                DriverExtensions.ExecuteScript(IncludeCasesClickScript, includeCasesElement);
            }
        }
    }
}

