namespace Framework.Common.UI.Products.GovernmentWeblinks.Pages.Standard
{
    using Framework.Common.UI.Products.GovernmentWeblinks.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Weblinks Standard Home page
    /// </summary>
    public class StandardHomePage : WeblinksHomePage
    {
        private static readonly By MostRecentLinkLocator = By.XPath("//header[@id='co_headerWrapper']//a[contains(text(), 'Updates')]");

        private static readonly By NyregLinkLocator = By.CssSelector("a[href$='/Browse/Index']");

        /// <summary>
        /// Header component
        /// </summary>
        public new WeblinksStandardHeaderComponent Header { get; } = new WeblinksStandardHeaderComponent();

        /// <summary>
        /// Toc component
        /// </summary>
        public WeblinksTocComponent TocComponent { get; } = new WeblinksTocComponent();

        /// <summary>
        /// Goes to Toc Page. Only for NYCRR site!
        /// </summary>
        public void ClickNyregPage() => DriverExtensions.WaitForElement(NyregLinkLocator).Click();

        /// <summary>
        /// Verifies is most recent link displayed
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        public bool IsMostRecentLinkDisplayed() => DriverExtensions.IsDisplayed(MostRecentLinkLocator);
    }
}
