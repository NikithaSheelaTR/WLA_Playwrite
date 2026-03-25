namespace Framework.Common.UI.Products.WestlawAdvantage.Pages
{
    using System.Collections.Generic;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.WestlawAdvantage.Components.HomePage;
    using OpenQA.Selenium;

    /// <summary>
    /// New F1 Home page
    /// </summary>
    public class AdvantageNewHomePage : ICreatablePageObject
    {
        private static readonly By ModifyButtonLocator = By.Id("_r_f_");
        private static readonly By QuickLinksButtonLocator = By.XPath("//div[contains(@class,'__btnsRow')]/*[contains(@class, '__btn')]");
        
        /// <summary>
        /// ComponentLocator
        /// </summary>
        protected By ComponentLocator => By.Id("homePageContainer");
        
        /// <summary>
        /// Modify button for Quick links
        /// </summary>
        public IButton ModifyButton => new Button(ModifyButtonLocator);

        /// <summary>
        /// Advantage Left Toolbar
        /// </summary>
        public AdvantageLeftToolbar AdvantageLeftToolbar => new AdvantageLeftToolbar();

        /// <summary>
        /// Advantage Search Tab Panel
        /// </summary>
        public AdvantageSearchTabPanel SearchTabPanel { get; } = new AdvantageSearchTabPanel();
               
        /// <summary>
        /// Quick Links
        /// </summary>
        public IReadOnlyCollection<IButton> QuickLinks => new ElementsCollection<Button>(QuickLinksButtonLocator);
    }
}
