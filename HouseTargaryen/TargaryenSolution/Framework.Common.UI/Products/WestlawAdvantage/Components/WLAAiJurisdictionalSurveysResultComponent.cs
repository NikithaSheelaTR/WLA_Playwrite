namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP
{
    using System;
    using System.Collections.Generic;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Components.HomePage.Browse;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.WestlawEdgePremium.Items.AALP;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    /// <summary>
    /// WLA AI Jurisdictional Surveys result component
    /// </summary>
    public class WLAAiJurisdictionalSurveysResultComponent : AiJurisdictionalSurveysResultComponent
    {
        private static readonly By ResultsContainerLocator = By.XPath("//div[contains(@class, '__results')]");
        private static readonly By ResultItemLocator = By.XPath(".//*[contains(@class, '__resultJurisdiction')]");
        private static readonly By IncludeCasesInStateJurisdictionCheckboxLocator = By.XPath("//saf-checkbox");
        private static readonly By ContentTypeContainerLocator = By.XPath(".//div[contains(@class, '__questionCard')]");
        
        /// <summary>
        /// Survey results items
        /// </summary>
        /// <returns>List of survey result items</returns>
        public ItemsCollection<WLAAiJurisdictionalSurveysResultItem> WLAAjsResultItems => new ItemsCollection<WLAAiJurisdictionalSurveysResultItem>(this.ComponentLocator, new ByChained(this.ComponentLocator, ResultItemLocator));

        /// <summary>
        /// Click Include Cases Checkbox
        /// </summary>
        public void DisableIncludeCasesCheckbox()
        {
            var includeCasesInStateJurisdictionCheckbox = (IWebElement)DriverExtensions.ExecuteScript(
                       "return(arguments[0].shadowRoot.querySelector(\"div input#control\"));",
                        DriverExtensions.GetElement(ContentTypeContainerLocator, IncludeCasesInStateJurisdictionCheckboxLocator));

            // Only click if enabled and currently checked (to disable)
            if (includeCasesInStateJurisdictionCheckbox.Selected)
            {
                includeCasesInStateJurisdictionCheckbox.JavascriptClick();
            }
            else
            {
                Console.WriteLine("The include cases checkbox is already unchecked");
            }
        }

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ResultsContainerLocator;
    }
}