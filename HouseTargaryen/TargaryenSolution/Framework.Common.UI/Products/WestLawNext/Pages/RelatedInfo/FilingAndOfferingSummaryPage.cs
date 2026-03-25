namespace Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// FillingPage, Offering Summary  (Agreements BLC)
    /// </summary>
    public class FilingAndOfferingSummaryPage : TabPage
    {
        private static readonly By ViewRelationshipsToThisDocumentLinkLocator = By.XPath("//*[contains(text(),'View relationships')]");
        
        /// <summary>
        /// Click 'View Relationships To This Document' link
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickViewRelationshipsToThisDocumentLink<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(ViewRelationshipsToThisDocumentLinkLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}
