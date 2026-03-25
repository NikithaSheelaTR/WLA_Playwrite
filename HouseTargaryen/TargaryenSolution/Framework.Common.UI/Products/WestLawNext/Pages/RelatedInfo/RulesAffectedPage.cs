namespace Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo
{
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Pages.Document;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Court Orders items Rules Affected tab
    /// </summary>
    public class RulesAffectedPage : TabPage
    {
        private static readonly By RulesItemLocator = By.XPath("//div[@class='co_relatedInfo_HistoryItem_TitleArea']/a[@guid]");

        /// <summary>
        /// Open Rule By Index
        /// </summary>
        /// <param name="index"> index </param>
        /// <returns> <see cref="CommonDocumentPage"/> </returns>
        public CommonDocumentPage OpenRuleByIndex(int index)
        {
            DriverExtensions.GetElements(RulesItemLocator).ElementAt(index).Click();
            return new CommonDocumentPage();
        }
    }
}