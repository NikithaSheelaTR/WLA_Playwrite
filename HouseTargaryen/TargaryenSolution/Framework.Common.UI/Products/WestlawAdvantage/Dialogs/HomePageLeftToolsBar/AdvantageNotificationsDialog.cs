namespace Framework.Common.UI.Products.WestlawAdvantage.Dialogs.HomePageLeftToolsBar
{
    using System;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Advantage Notifications Dialog
    /// </summary>
    public class AdvantageNotificationsDialog : AdvantageBaseDialog
    {
        private static readonly By ContentTypeContainerLocator = By.XPath("//div[contains(@class,'__panelContent')]");
        private static readonly By AlertItemLocator = By.XPath("//saf-anchor[contains(@href, 'Alerts')]"); 
        private static readonly By AlertsContainerLocator = By.XPath(".//div[@id='co_contentWrapper']");
        private static readonly By AlertsPageTitleLabelLocator = By.XPath("//h2[@class='co_alertCenter_heading']");
        private static readonly By ViewAllItemLocator = By.XPath("//saf-anchor[contains(@href, 'All')]");
        private static readonly By PreferencesContainerLocator = By.XPath("//div[@class = 'co_overlayBox_container Preferences-modal']");
        private static readonly By PreferencesItemLocator = By.XPath("//button[contains(@class, '__buttonContent--')]");
        private static readonly By PreferencesTitleLabelLocator = By.XPath("//h2[contains(text(), 'Preferences')]");
        /// <summary>
        /// Alerts Link
        /// </summary>
        public ILink AlertsLink => new Link( AlertItemLocator);
       
        /// <summary>
        /// Preferences Link
        /// </summary>
        public ILink PreferencesLink => new Link(PreferencesItemLocator);

        /// <summary>
        /// View All Button
        /// </summary>
        public IButton ViewAll => new Button(ContentTypeContainerLocator, ViewAllItemLocator);
    }
}
