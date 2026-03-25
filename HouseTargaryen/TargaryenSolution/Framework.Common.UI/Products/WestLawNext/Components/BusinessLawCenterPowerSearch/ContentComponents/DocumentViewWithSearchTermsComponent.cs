namespace Framework.Common.UI.Products.WestLawNext.Components.BusinessLawCenterPowerSearch.ContentComponents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The document view with search terms.
    /// </summary>
    public class DocumentViewWithSearchTermsComponent : DocumentViewComponent
    {
        private static readonly By IweFTSSearchTermNextButtonLocator = By.XPath("//div[@class='co_navTools']/div[contains(@class, 'searchTermNavigation')]/a[@class='co_next']");

        private static readonly By SerchResultItemLocator = By.XPath("//span[@class='co_searchTerm']");

        private static readonly By ContainerLocator = By.Id("documentContent");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// The IWebElement for the Next button of the full text search Term navigation.
        /// </summary>
        private IWebElement IweFTSSearchTermNextButton
            => DriverExtensions.WaitForElementDisplayed(IweFTSSearchTermNextButtonLocator);

        /// <summary>
        /// Is corresponding search term displayed during full text search term navigation.
        /// TODO 3/12 RA: Improve test logic
        /// </summary>
        /// <param name="testSearchTerm">
        /// The test search term.
        /// </param>
        /// Note: RA 3/12 : it looks like the page is chunked, i.e. the number of the terms is per currently displayed chunk.
        /// <param name="chunks">
        /// The times.
        /// </param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsCorresondingSearchTermDisplayedDuringFtsSearchTermNavigation(string testSearchTerm, int chunks)
        {
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForJavaScript();

            bool isEveryCorrespondingSearchTermDislayedDuringNavigation = true;

            const int SetUpCount = 2;
            for (int i = 0; i < SetUpCount; i++)
            {
                DriverExtensions.Click(this.IweFTSSearchTermNextButton);
                DriverExtensions.WaitForJavaScript();
            }

            for (int i = 0; i < chunks; i++)
            {
                IList<IWebElement> presentHighligtedSearchTerms =
                    DriverExtensions.GetElements(SerchResultItemLocator).Where(elem => elem.IsElementInView()).ToList();
                if (!presentHighligtedSearchTerms.Any())
                {
                    return false;
                }

                isEveryCorrespondingSearchTermDislayedDuringNavigation &=
                    presentHighligtedSearchTerms.Any(
                        el => string.Equals(el.Text, testSearchTerm, StringComparison.InvariantCultureIgnoreCase));

                DriverExtensions.Click(this.IweFTSSearchTermNextButton);
                DriverExtensions.WaitForJavaScript();
            }

            return isEveryCorrespondingSearchTermDislayedDuringNavigation;
        }
    }
}