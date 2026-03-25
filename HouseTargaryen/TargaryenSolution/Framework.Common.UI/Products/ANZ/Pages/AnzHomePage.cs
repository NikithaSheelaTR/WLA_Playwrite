namespace Framework.Common.UI.Products.ANZ.Pages
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.ANZ.Components;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using OpenQA.Selenium;
    using System.Collections.Generic;

    /// <summary>
    /// ANZ Home Page
    /// </summary>
    public class AnzHomePage : EdgeHomePage
    {
        private static readonly By AnzHomePageAllWidgets = By.XPath("//*[@id='coid_browseTabs']/li | //*[@class='Tab-list']/li | //*[@class='co_genericBoxHeader'] | //*[@class='Additional-product']/h2");

        private static readonly By NewsFromReutersSectionLocator = By.XPath("//div[@class='co_genericBox co_expandedState co_dataFeedWidget styled']");

        private static readonly By ResumeSession = By.XPath("//*[@value=\"Resume session\"]");

        private static readonly By ResumeSessionOverlayLocator = By.Id("co_suspendBilling");

        private static readonly By ReturntoWestlaw = By.XPath("//*[@id=\"coid_website_signBackOnButton\"]");

        private static readonly By Logo = By.XPath("//*[@id=\"co_logo\"]/img");

        /// <summary>
        /// All Widget Elements
        /// </summary>
        public IReadOnlyCollection<ILabel> AllWidgetElements => new ElementsCollection<Label>(AnzHomePageAllWidgets);

        /// <summary>
        /// NewsFromReutersSection
        /// </summary>
        public ILabel NewsFromReutersSection => new Label(NewsFromReutersSectionLocator);

        /// <summary>
        /// Tracker Analytics Component
        /// </summary>
        public AnzHomeTrackerAnalyticsComponent AnzHomeTrackerAnalyticsComponent { get; } = new AnzHomeTrackerAnalyticsComponent();

        /// <summary>
        /// Key Number System component
        /// </summary>
        public AnzKeyNumberSystemComponent AnzKeyNumberSystem { get; } = new AnzKeyNumberSystemComponent();


        /// <summary>
        /// Footer component
        /// </summary>
        public ANZFooterComponents ANZFooterElements { get; } = new ANZFooterComponents();

        /// <summary>
        /// Click Resume Session Button
        /// </summary>
        public AnzHomePage ClickResumeSession()
        {
            DriverExtensions.WaitForCondition(a => DriverExtensions.IsDisplayed(ResumeSessionOverlayLocator, 10000));
            DriverExtensions.WaitForElement(ResumeSession).Click();
            return this;
        }

        /// <summary>
        /// Verify the logo
        /// </summary>
        public AnzHomePage VerifyLogo()
        {
            var TRlogo = DriverExtensions.WaitForElement(Logo);
            TRlogo.IsDisplayed();
            TRlogo.GetText();
            return this;
        }

        /// <summary>
        /// Click Return to Westlaw Button
        /// </summary>
        public AnzHomePage ClickReturntoWestlaw()
        {
            DriverExtensions.ClickUsingJavaScript(ReturntoWestlaw);
            return this;
        }
    }
}
