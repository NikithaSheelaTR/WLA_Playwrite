namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// AI Jurisdictional Surveys Content Type component
    /// </summary>
    public class AiJurisdictionalSurveysContentTypeComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContentTypeContainerLocator = By.XPath("//div[contains(@class, 'contentTypeCard')]");
        
        private const string ContentTypeLctMask = "saf-checkbox[current-value='{0}']";
        private const string ContentTypeCheckboxScript = "return(arguments[0].shadowRoot.querySelector('input[id=control]'));";
        private By JurisdictionContentTypesLocator = By.CssSelector("saf-checkbox:not([id *= 'fiftyStates-selectAll'])");

        /// <summary>
        /// Select content type(s)
        /// </summary>
        /// <param name="contentTypes">list of content types</param>
        public void SelectContentType(params string[] contentTypes)
        {
            foreach (string contentType in contentTypes.ToList())
            {
                IWebElement contentTypeElement = DriverExtensions.GetElement(By.CssSelector(string.Format(ContentTypeLctMask, contentType)));
                IWebElement contentTypeCheckbox = (IWebElement)DriverExtensions.ExecuteScript(ContentTypeCheckboxScript, contentTypeElement);
                contentTypeCheckbox.Click();
            }
        }



        /// <summary>
        /// list content type(s)
        /// </summary>
        public List<string> JurisdictionContentTypes => DriverExtensions.GetElements(JurisdictionContentTypesLocator).Select(element => element.GetAttribute("current-value")).ToList();

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContentTypeContainerLocator;
    }
}

