namespace Framework.Common.UI.Products.WestLawNext.Pages.IphoneMode
{
    using System;

    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// IPhone Mode Search ResultPage class 
    /// </summary>
    public class IphoneModeSearchResultPage : BaseModuleRegressionPage
    {
        private const string SearchResultCategoryLctMask = ".//*[@id='cobalt_search_{0}_results_header']/a";

        /// <summary>
        /// Gets the unique identifier.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns> GUID </returns>
        public string GetGuid(string content)
        {
            string str = DriverExtensions
                .WaitForElement(By.XPath(string.Format(SearchResultCategoryLctMask, content.ToLower())))
                .GetAttribute("href");
            return str.Substring(str.IndexOf("querySubmissionGuid=", StringComparison.Ordinal) + 20, 33);
        }
    }
}