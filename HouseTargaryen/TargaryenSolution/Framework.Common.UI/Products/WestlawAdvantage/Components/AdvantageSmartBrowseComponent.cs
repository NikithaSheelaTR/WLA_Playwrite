namespace Framework.Common.UI.Products.WestlawAdvantage.Pages
{
    using System;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.Alerts.NarrowByContent;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Elements.Toggles;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.WestlawAdvantage.Components.HomePage;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    /// <summary>
    /// Advantage Smart Browse Component page
    /// </summary>
    public class AdvantageSmartBrowseComponent : SearchWithinResultsTabComponent
    {
        private static readonly By ContainerLocator = By.Id("co_fermiSearchResult_data");
        private static readonly By SearchResultsContainerLocator = By.XPath("//div[@class='co_searchContent co_athens_searchContent']");
        private static readonly By GlobalSearchResultsPageHeadingLocator = By.Id("co_resultsPageLabel");
        private static readonly By StatutesAndCourtRulesLinkLocator = By.XPath("//a[@id='co_search_contentNav_link_STATUTE']");
        private static readonly By ContentTypesTabLocator = By.Id("tab-content-types");
        private static readonly By ToolbarContainerLocator = By.Id("co_docToolbar");
        private static readonly By SmartBrowseToggleLocator = By.XPath("//saf-switch[@aria-label = 'Toggle Smart Browse']");
        private static readonly By CloseGuideAndReturnToWestlawLocator = By.XPath("//button[contains(@id, 'pendo-close-guide')]");
        private static readonly By SmartBrowseEnabledTextLocator = By.XPath("//p[contains(text(), 'Smart Browse uses AI-generated highlighting')]");
        
        /// <summary>
        /// Westlaw Advantage Global Search Results Page Heading Label
        /// </summary>
        public ILabel GlobalSearchResultsPageHeading => new Label(GlobalSearchResultsPageHeadingLocator);

        /// <summary>
        /// Statutes and court rules link
        /// </summary>
        public ILink StatutesAndCourtRulesLink => new Link(StatutesAndCourtRulesLinkLocator);

        /// <summary>
        /// Global Research Question and Answer items list
        /// </summary>
        /// <returns>List of user questions and answers</returns>
        public ItemsCollection<AdvantageQuestionAndAnswerItem> WlaSearchQuestionAndAnswerItems => new ItemsCollection<AdvantageQuestionAndAnswerItem>(ContainerLocator, SearchResultsContainerLocator);

        /// <summary>
        /// Content types tab
        /// </summary>
        public IButton ContentTypesTab => new Button(ContentTypesTabLocator);

        /// <summary>
        /// Smart browse toggle status
        /// </summary>
        public IToggle SmartBrowseToggleStatus => new Toggle(ToolbarContainerLocator, SmartBrowseToggleLocator);

        /// <summary>
        /// Smart browse toggle
        /// </summary>
        public IWebElement SmartBrowseToggle => (IWebElement)DriverExtensions.ExecuteScript(
                          "return(arguments[0].shadowRoot.querySelector('div'));",
                           DriverExtensions.GetElement(ToolbarContainerLocator, SmartBrowseToggleLocator));

        /// <summary>
        /// Close guide and return to westlaw pop up button
        /// </summary>
        public IButton CloseGuideAndReturnToWestLaw => new Button(CloseGuideAndReturnToWestlawLocator);

        /// <summary>
        /// Close guide and return to westlaw
        /// </summary>
        public void CloseGuidAndReturnToWestlaw()
        {
            try
            {
                if (CloseGuideAndReturnToWestLaw.Displayed && CloseGuideAndReturnToWestLaw.Enabled)
                {
                    CloseGuideAndReturnToWestLaw.Click();
                }
            }
            catch (NoSuchElementException)
            {
                // Guide button not present, continue with toggle click
            }
        }
        
        /// <summary>
        /// Click smart browse toggle
        /// </summary>
        public void ClickSmartBrowseToggle()
        {    
            CloseGuidAndReturnToWestlaw();                
            SmartBrowseToggle.JavascriptClick();
        }
    
        /// <summary>
        /// Checks if Smart Browse toggle is checked (inside shadow DOM)
        /// </summary>
        public bool GetSmartBrowseToggleState()
        {
            CloseGuidAndReturnToWestlaw();
            var smartBrowseToggleStatus = DriverExtensions.GetElement(ToolbarContainerLocator, SmartBrowseToggleLocator);
            var smartBrowseToggleAttribute = smartBrowseToggleStatus.GetAttribute("aria-checked");

            var isToggleChecked = smartBrowseToggleAttribute?.ToString();
            return isToggleChecked.Equals("true", StringComparison.OrdinalIgnoreCase) == true;       
        }

        /// <summary>
        /// Smart Browse Toggle ON Text
        /// </summary>
        public ILabel SmartBrowseEnabledText => new Label(SmartBrowseEnabledTextLocator);
    }
}