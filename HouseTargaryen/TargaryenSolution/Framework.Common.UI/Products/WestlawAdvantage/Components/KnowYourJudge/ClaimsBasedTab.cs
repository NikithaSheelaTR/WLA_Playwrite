namespace Framework.Common.UI.Products.WestlawAdvantage.Components.KnowYourJudge
{
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Products.Shared.Components.HomePage.Browse;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Claims Based Tab
    /// </summary>
    public class ClaimsBasedTab : BaseBrowseTabPanelComponent
    {
        private static readonly By TabContainerLocator = By.XPath("//div[@data-testid='claims-report-container']");
        private static readonly By OverviewLabelLocator = By.XPath("//saf-accordion-item-v3[@data-testid='section-overview']");
        private static readonly By AccordionItemLocator = By.XPath("//saf-accordion-item-v3[@saf='accordion-item']");
        private static readonly By AccordionItemHeadingLocator = By.XPath(".//span/h4");
        private const string InputAreaScript = "return(arguments[0].shadowRoot.querySelector('button'));";

        /// <summary>
        /// Tab name
        /// </summary>
        protected override string TabName => "Claims-based";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => TabContainerLocator;

        /// <summary>
        /// Overview Label
        /// </summary>
        public string OverviewLabel => DriverExtensions.GetElement(ComponentLocator, OverviewLabelLocator).Text;

        /// <summary>
        /// Click accordion arrow
        /// </summary>     
        /// <return> "return Accordion Button element" </return>
        public IWebElement ClickSummaryAccordionArrow()
        {
            IWebElement AccordionItem1 = DriverExtensions.GetElement(ComponentLocator, AccordionItemLocator);
            IWebElement AccordionButtonElement = (IWebElement)DriverExtensions.ExecuteScript(InputAreaScript, AccordionItem1);
            AccordionButtonElement.Click();
            return AccordionButtonElement;
        }

        /// <summary>
        /// Verify Accordion Item is present
        /// </summary>
        /// <param name="accordionItemNames"> accordion items to verify </param>
        public void VerifyAccordionItemIsPresent(List<string> accordionItemNames)
        {
            var accordionItemHeadings = DriverExtensions.GetElements(ComponentLocator, AccordionItemLocator)
                .SelectMany(item => item.FindElements(AccordionItemHeadingLocator));
            foreach (var itemHeading in accordionItemHeadings)
            {
                bool isPresent = accordionItemNames.Any(item => item.Equals(itemHeading.Text));
                if (!isPresent)
                {
                    throw new WebDriverException($"Accordion item with name '{itemHeading.Text}' is not expected to be present in Claims Based tab.");
                }
            }
        }
    }
}


