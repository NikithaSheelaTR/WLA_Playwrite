namespace Framework.Common.UI.Products.GovernmentWeblinks.Pages
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Weblinks home page class
    /// </summary>
    public class WeblinksHomePage : BaseGovernmentWeblinksPage
    {
        private static readonly By ContentLinkLocator = By.XPath("//div[@id='co_contentColumn']//p/a");

        private static readonly By ContentLocator = By.XPath("//div[@id='co_contentColumn']//p");

        /// <summary>
        /// Gets Content links count
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        public int GetContentLinksCount() => DriverExtensions.GetElements(ContentLinkLocator).Count;

        /// <summary>
        /// Gets Content link href
        /// </summary>
        /// <param name="index">Starts from zero</param>
        /// <returns>The <see cref="string"/> href.</returns>
        public string GetContentLink(int index = 0) => DriverExtensions.GetElements(ContentLinkLocator).ElementAt(index).GetAttribute("href");

        /// <summary>
        /// Gets Content link text
        /// </summary>
        /// <param name="index">Starts from zero</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetContentLinkText(int index = 0) => DriverExtensions.GetElements(ContentLinkLocator).ElementAt(index).Text;

        /// <summary>
        /// Gets Content text
        /// </summary>
        /// <returns>The <see cref="List{T}"/> href.</returns>
        public List<string> GetContentText() => DriverExtensions.GetElements(ContentLocator).Select(e => e.Text).ToList();

        /// <summary>
        /// Verifies Content link
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsContentLinkDisplayed() => DriverExtensions.IsDisplayed(ContentLinkLocator, 5);
    }
}
