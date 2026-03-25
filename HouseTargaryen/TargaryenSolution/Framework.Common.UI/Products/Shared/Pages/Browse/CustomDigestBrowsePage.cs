namespace Framework.Common.UI.Products.Shared.Pages.Browse
{
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components.KeyNumber;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// This class represents the West Key Number System page
    /// </summary>
    public class CustomDigestBrowsePage : CheckboxBrowsePage
    {
        private static readonly By LinkLocator = By.XPath("//a[@class=' co_tocItemLink '] | //*[@id='coid_browseToc']//a");

        private static readonly By ViewHeadnotesButtonLocator = By.Id("co_viewHeadnotesButton");

        private static readonly By ScopeIconLocator = By.XPath("//*[contains(@id, 'co_scopeIcon_')]");

        /// <summary>
        /// Search For Key Numbers component
        /// </summary>
        public SearchForKeyNumbersComponent SearchForKeyNumbers { get; set; } = new SearchForKeyNumbersComponent();

        /// <summary>
        /// Title Search component
        /// </summary>
        public TitleSearchComponent TitleSearch { get; set; } = new TitleSearchComponent();

        /// <summary>
        /// Clicks a table of contents link
        /// </summary>
        /// <typeparam name="T">Page we'll end up on</typeparam>
        /// <param name="linkText">Name of the link</param>
        /// <returns>New instance of page T</returns>
        public T ClickTableOfContentsLink<T>(string linkText) where T : ICreatablePageObject
        {
            this.GetLinkItem(linkText).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click the View Head notes button
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>The new T page instance</returns>
        public T ClickViewHeadnotes<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(ViewHeadnotesButtonLocator).CustomClick();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Returns true if View Head notes button is displayed
        /// </summary>
        /// <returns> True if View Head notes button is displayed, false otherwise </returns>
        public bool IsViewHeadnotesButtonDisplayed() => DriverExtensions.IsDisplayed(ViewHeadnotesButtonLocator);

        /// <summary>
        /// Open scope dialog
        /// </summary>
        /// <param name="linkText">string link text</param>
        /// <returns>The <see cref="ScopeDialog"/>.</returns>
        public ScopeDialog OpenScopeDialogAfterHover(string linkText)
        {
            this.GetLinkItem(linkText).SeleniumHover();
            DriverExtensions.WaitForElementDisplayed(ScopeIconLocator).Click();
            return new ScopeDialog();
        }

        private IWebElement GetLinkItem(string linkText)
            => DriverExtensions.GetElements(LinkLocator).FirstOrDefault(elem => DriverExtensions.GetImmediateText(elem).Trim().Equals(linkText));
    }
}