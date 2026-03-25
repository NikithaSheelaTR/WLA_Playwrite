namespace Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo
{
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// RelatedOpinionPage
    /// </summary>
    public class RelatedOpinionPage : TabPage
    {
        private static readonly By RowLinkElementLocator =
            By.XPath("//div[@id='coid_relatedinfo_detailsContainer']//a[contains(@class, 'co_relatedInfo_grid_documentLink')]");

        /// <summary>
        /// Click on the first row link in results list
        /// </summary>
        /// <typeparam name="T"> T page </typeparam>
        /// <param name="index"> The index. </param> 
        /// <returns> new instance of CommonDocumentPage </returns>
        public T ClickResultRowLinkElement<T>(int index) where T : ICreatablePageObject
        {
            DriverExtensions.GetElements(RowLinkElementLocator).ElementAt(index).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}