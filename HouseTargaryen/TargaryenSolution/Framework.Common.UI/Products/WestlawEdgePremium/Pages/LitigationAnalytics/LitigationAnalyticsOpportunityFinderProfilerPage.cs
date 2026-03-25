namespace Framework.Common.UI.Products.WestlawEdgePremium.Pages.LitigationAnalytics
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.OpportunityFinderComponents;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// LitigationAnalyticsOpportunityFinderProfilerPage
    /// </summary>
    public class LitigationAnalyticsOpportunityFinderProfilerPage : EdgeCommonAuthenticatedWestlawNextPage
    {
        private static readonly By TitleLocator = By.ClassName("la-OpFinder-heading");
        private static readonly By BackButtonLocator = By.ClassName("la-OpFinder-prevButton");
        private static readonly By ContinueButtonLocator = By.ClassName("la-OpFinder-nextButton");
        private static readonly By OpportunityFinderContentHeaderLocator = By.XPath("//div[contains(@class,'la-OpFinder-contentHeader')]/h2");
        private static readonly By MySelectionsItemLocator = By.XPath("//div[@class ='la-OpFinder-selectionsList']//span[@class ='saf-chip__text']");

        ///// <summary>
        ///// Title
        ///// </summary>
        //public string Title => DriverExtensions.WaitForElement(TitleLocator).Text;

        /// <summary>
        /// Step 1 : Legal Activity Component
        /// </summary>
        public OpportunityFinderLegalActivityComponent StepFirstLegalActivityComponent => new OpportunityFinderLegalActivityComponent();
        ///<summary>
        /// Opportunity Finder Content Header
        /// </summary>
        public string OpportunityFinderContentHeader => DriverExtensions.WaitForElement(OpportunityFinderContentHeaderLocator).Text;

        ///<summary>
        /// My Selections Filters Text
        /// </summary>
        public List<string> MySelectionsFiltersText => DriverExtensions.GetElements(MySelectionsItemLocator).ToList().Select(item => item.Text).ToList();

        /// <summary>
        /// Back Button
        /// </summary>
        public IButton BackButton()
        {

            DriverExtensions.ScrollPageToBottom();
            return new Button(BackButtonLocator);
        }

        /// <summary>
        /// Continue Button
        /// </summary>
        public IButton ContinueButton()
        {
            DriverExtensions.ScrollPageToBottom();
            return new Button(ContinueButtonLocator);
        }
    }
}