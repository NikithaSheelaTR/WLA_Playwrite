namespace Framework.Common.UI.Products.Shared.Components
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Enums.Footer;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;

    /// <summary>
    /// Global footer found on almost all Westlaw next pages.
    /// </summary>
    public class WestlawNextFooterComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.Id("co_footerLinks");

        private static readonly By FooterPhoneNumberLocator = By.Id("co_footerCs");

        private static readonly By GettingStartedLocator = By.Id("coid_website_quickStartGuide");

        private static readonly By SetWelcomePreferenceLinkLocator = By.Id("coid_websiteFooter_setWelcomeScreenPreference");

        private static readonly By ThomsonReutersFooterLogoLocator = By.Id("co_trLogo_link");

        private static readonly By ThomsonReutersCopyRightLocator = By.XPath("//*[@id='co_footerCopyright']/li");

        private static readonly By ImproveWestlawReportAnErrorButtonLocator = By.XPath("//button[@id='coid_websiteFooter_pageSurvey']");

        private static readonly By PreferenceButtonLocator = By.XPath("//span[@class='icon25 icon_gear_document']");

        private EnumPropertyMapper<FooterLinks, WebElementInfo> footerLinksMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Footer Links Map
        /// </summary>
        protected EnumPropertyMapper<FooterLinks, WebElementInfo> FooterLinksMap
            => this.footerLinksMap = this.footerLinksMap ?? EnumPropertyModelCache.GetMap<FooterLinks, WebElementInfo>();

        /// <summary>
        /// Click link in Footer
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="footerlink"> Footer link </param>
        /// <returns> New instance of the page </returns>
        public T ClickLink<T>(FooterLinks footerlink) where T : ICreatablePageObject
        {
            DriverExtensions.ScrollPageToBottom();
            try
            {
                DriverExtensions.GetElement(By.LinkText(this.FooterLinksMap[footerlink].Text)).Click();
            }catch(NoSuchElementException)
            {
                DriverExtensions.GetElement(By.XPath(this.FooterLinksMap[footerlink].LocatorString)).Click();
            }
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Clicks the welcome preference link on the footer.
        /// </summary>
        public void ClickSetWelcomePreferenceLink()
            => DriverExtensions.WaitForElement(SetWelcomePreferenceLinkLocator).Click();

        /// <summary>
        /// Gets the footer copyright text.
        /// </summary>
        /// <returns> The footer copyright text. </returns>
        public string GetCopyrightText() => DriverExtensions.GetText(ThomsonReutersCopyRightLocator);

        /// <summary>
        /// Gets the footer phone number.
        /// </summary>
        /// <returns> The footer phone number. </returns>
        public string GetFooterPhoneNumber() => DriverExtensions.GetText(FooterPhoneNumberLocator);

        /// <summary>
        /// Gets the href of the "What's New" link
        /// </summary>
        /// <returns>Address</returns>
        public string GetWhatsNewLinkAddress()
            => DriverExtensions.GetAttribute("href", By.LinkText(this.FooterLinksMap[FooterLinks.WhatsNew].Text));

        /// <summary>
        /// Verifies if Getting started present.
        /// </summary>
        /// <returns> true if getting started is present, otherwise false </returns>
        public bool IsGettingStartedPresent() => DriverExtensions.IsElementPresent(GettingStartedLocator);

        /// <summary>
        /// Get Footer phone number Status
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsFooterPhoneNumberDisplayed() => DriverExtensions.IsDisplayed(FooterPhoneNumberLocator);

        /// <summary>
        /// Get Footer Display Status
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public override bool IsDisplayed() => DriverExtensions.IsDisplayed(this.ComponentLocator);

        /// <summary>
        /// Is footer link displayed
        /// </summary>
        /// <param name="footerlink"> Footer link</param>
        /// <returns> True if link is displayed, false otherwise </returns>
        public bool IsFooterLinkDisplayed(FooterLinks footerlink)
        {
          var container = DriverExtensions.GetElement(this.ComponentLocator);
          return DriverExtensions.IsDisplayed(container, By.XPath(this.FooterLinksMap[footerlink].LocatorString));        
        }

        /// <summary>
        /// Check if TR logo is displayed in the footer
        /// </summary>
        /// <returns> True if footer logo is displayed, false otherwise </returns>
        public bool IsThomsonReutersLogoDisplayed() => DriverExtensions.IsDisplayed(ThomsonReutersFooterLogoLocator);

        /// <summary>
        /// Check if TR Copyright is displayed in the footer
        /// </summary>
        /// <returns> True if copyright is displayed, false otherwise </returns>
        public bool IsCopyrightItemDisplayed() => DriverExtensions.IsDisplayed(ThomsonReutersCopyRightLocator);

        /// <summary>
        /// Check if set welcome preference link displayed in the footer
        /// </summary>
        /// <returns> True if set welcome preference link is displayed, false otherwise </returns>
        public bool IsSetWelcomePreferenceLinkDisplayed() => DriverExtensions.IsDisplayed(SetWelcomePreferenceLinkLocator);

        /// <summary>
        ///  ImproveWestLaw / ReportAnError Button
        /// </summary>
        public IButton ImproveWestLawOrReportAnErrorButton = new Button(ImproveWestlawReportAnErrorButtonLocator);

        /// <summary>
        ///  Preference Button
        /// </summary>
        public IButton PreferenceButton => new Button(this.ComponentLocator,PreferenceButtonLocator);
    }
}