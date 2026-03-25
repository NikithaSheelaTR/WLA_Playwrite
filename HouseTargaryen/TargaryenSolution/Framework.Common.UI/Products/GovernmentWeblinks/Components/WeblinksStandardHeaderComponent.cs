namespace Framework.Common.UI.Products.GovernmentWeblinks.Components
{
    using Framework.Common.UI.Products.GovernmentWeblinks.Enums;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Weblinks extended header component for standard pages
    /// </summary>
    public class WeblinksStandardHeaderComponent : WeblinksHeaderComponent
    {
        /// <summary>
        /// Gets href of the link
        /// </summary>
        /// <param name="link">WeblinksHeaderLinks</param>
        /// <returns>The href of a header link</returns>
        public string GetLink(StandardHeaderLinks link)
            => DriverExtensions.WaitForElement(By.XPath(this.StandardHeaderLinksMap[link].LocatorString)).GetAttribute("href");

        /// <summary>
        /// Verifies is weblinks header link displayed
        /// </summary>
        /// <param name="link">WeblinksHeaderLinks</param>
        /// <returns>True if displayed, false otherwise</returns>
        public bool IsLinkDisplayed(StandardHeaderLinks link)
            => DriverExtensions.IsDisplayed(By.XPath(this.StandardHeaderLinksMap[link].LocatorString), 5);
    }
}
