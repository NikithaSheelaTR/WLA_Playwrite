namespace Framework.Common.UI.Products.WestlawEdge.Pages.QuickCheck
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Document analyzer alternative homepage 
    /// </summary>
    public class QuickCheckAlternateHomePage : QuickCheckBasePage
    {
        private static readonly By LearnMoreButtonLocator = By.XPath("//*[@class='DA-MarketingHomePage-LearnMore']/a");

        private static readonly By RightImageTextLocator = By.XPath("//*[@class='DA-MarketingHomePage-RightImage']/span");

        private static readonly By TitleLocator = By.ClassName("DA-MarketingHomePage-SubTitle");

        private static readonly By InfoBoxTexLabelLocator = By.XPath("//*[@class='co_infoBox warning']/div[@class='co_infoBox_message']");

        /// <summary>
        /// Learn more button
        /// </summary>
        public IButton LearnMoreButton => new Button(LearnMoreButtonLocator);

        /// <summary>
        /// Picture text label
        /// </summary>
        public ILabel RightImageLabel => new Label(RightImageTextLocator);

        /// <summary>
        /// Title label
        /// </summary>
        public ILabel TitleLabel => new Label(TitleLocator);

        /// <summary>
        /// Info box label
        /// </summary>
        public ILabel InfoBoxLabel => new Label(InfoBoxTexLabelLocator);    
    }
}