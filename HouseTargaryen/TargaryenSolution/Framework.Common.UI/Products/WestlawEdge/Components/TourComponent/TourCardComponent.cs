namespace Framework.Common.UI.Products.WestlawEdge.Components.TourComponent
{
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;
    using Framework.Core.Utils;

    /// <summary>
    /// Tour card component
    /// </summary>
    public class TourCardComponent<T> : BaseModuleRegressionComponent where T : struct
    {
        private static readonly By CardHomePageLocator = By.XPath(
            "//div[contains(@class,'hopscotch-bubble animated tour')]");

        private static readonly By CardNextBtnLocator =
            By.XPath("//button[contains(@class,'hopscotch-nav-button next hopscotch-next')] | //button[@class='co_primaryBtn']");

        private static readonly By CardFinishBtnLocator =
            By.XPath("//button[contains(@class,'hopscotch-nav-button next hopscotch-cta')]");

        private static readonly By CardBackBtnLocator =
            By.XPath("//button[contains(@class,'hopscotch-nav-button prev hopscotch-prev')]");

        private static readonly By CardContentLocator = By.XPath("//div[contains(@class,'bubble-content') or contains(@class,'TourBox_inner')] | //*[@class='co_overlayBox_content']");

        private static readonly By ContainerLocator = By.XPath("//div[contains(@class,'tour-LegalAnalyticsStep') or @id='coid_lightboxOverlay']");

        private static readonly By CloseButtonLocator = By.XPath("//*[contains(@class, 'hopscotch-bubble-close')]");

        private static readonly By LearnMoreButtonLocator = By.XPath("//button[@class='co_linkBlue' and contains(text(), 'Westlaw Precision')]");

        /// <summary>
        /// Tour Cards Map
        /// </summary>
        private EnumPropertyMapper<T, WebElementInfo> tourCardMap;

        /// <summary>
        /// Gets the TourCards enumeration
        /// </summary>
        private EnumPropertyMapper<T, WebElementInfo> TourCardMap =>
            this.tourCardMap = this.tourCardMap ?? EnumPropertyModelCache.GetMap<T, WebElementInfo>(
                                   string.Empty,
                                   @"Resources/EnumPropertyMaps/WestlawEdge/Tours");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Learn more button
        /// </summary>
        public IButton LearnMoreButton { get; } = new Button(LearnMoreButtonLocator);

        /// <summary>
        /// Close the card button
        /// </summary>
        public IButton CloseButton { get; } = new Button(CloseButtonLocator);

        /// <summary>
        /// tour - get card content
        /// </summary>
        /// <returns></returns>
        public string GetCardContent()
        {
            DriverExtensions.WaitForElementDisplayed(CardContentLocator);
            return DriverExtensions.GetElement(CardContentLocator).Text;
        }

        /// <summary>
        /// tour - click on Back or previous button on the card
        /// </summary>
        public void ClickOnBack(bool isWaitNeeded = false) => this.ClickTourButton(CardBackBtnLocator, isWaitNeeded);

        /// <summary>
        /// tour - click on finish on tour card
        /// </summary>
        public void ClickOnFinish()
        {
            DriverExtensions.WaitForElement(CardFinishBtnLocator);
            this.ClickTourButton(CardFinishBtnLocator);
        }

        /// <summary>
        /// tour - click on next on tour card
        /// </summary>
        /// <returns></returns>
        public bool ClickOnNext(int numberOfClicks = 1, bool isWaitNeeded = false)
        {
            bool isOperationSuccessfull= false;
            for (int i = 0; i < numberOfClicks; i++)
            {                
                try
                {
                    this.ClickTourButton(CardNextBtnLocator, isWaitNeeded);
                    //DriverExtensions.WaitForElementDisplayed(CardNextBtnLocator);
                    isOperationSuccessfull = true;
                }
                catch (WebDriverTimeoutException)
                {
                    Logger.LogInfo("Tour button 'Next' not found!");
                    isOperationSuccessfull = false;
                    break;
                }               
            }
            return isOperationSuccessfull;
        }

        /// <summary>
        /// Is tour card displayed
        /// </summary>
        /// <param name="tourCardName">Verified card name</param>
        /// <returns></returns>
        public bool IsCardDisplayed(T tourCardName)
        {
            DriverExtensions.WaitForPageLoad();
            return DriverExtensions.GetElements(CardContentLocator).First(item => item.IsDisplayed()).Text.Replace("’", "'")
                            .Contains(this.TourCardMap[tourCardName].Text.Replace("’", "'"));
        }

        /// <summary>
        /// Is it Tour card displayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool IsDisplayed() => DriverExtensions.IsDisplayed(CardContentLocator, 5);

        /// <summary>
        /// Clicks tour card button by locator
        /// </summary>
        /// <param name="buttonLocator">Buttons locator</param>
        /// <param name="isWaitNeeded">Is wait needed</param>
        private void ClickTourButton(By buttonLocator, bool isWaitNeeded = true)
        {
            if (isWaitNeeded)
            {
                DriverExtensions.WaitForElementPresent(CardHomePageLocator);
                DriverExtensions.Click(buttonLocator);
                DriverExtensions.WaitForAnimation();
            }
            else
            {
                DriverExtensions.Click(buttonLocator);
                DriverExtensions.WaitForAnimation();
            }            
        }
    }
}
