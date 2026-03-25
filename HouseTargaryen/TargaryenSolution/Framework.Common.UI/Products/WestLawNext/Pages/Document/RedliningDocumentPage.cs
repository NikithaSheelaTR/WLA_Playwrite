namespace Framework.Common.UI.Products.WestLawNext.Pages.Document
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Pages.Document;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Document page
    /// </summary>
    public class RedliningDocumentPage : CommonDocumentPage
    {
        private const string WhiteColor = "background-color: white;";

        private const string RgbWhiteColor = "background-color: rgb(255, 255, 255);";

        private static readonly By AddedPartLocator = By.XPath("//ins[contains(@class,'co_ruleBookRedline')]");

        private static readonly By DeletedPartLocator = By.XPath("//del[contains(@class,'co_ruleBookRedline')]");

        /// <summary>
        /// Determine whether redlining markup is displayed
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsRedliningMarkupDisplayed()
        {
            List<string> addedPartsColor = DriverExtensions.GetElements(AddedPartLocator).Select(el => el.GetAttribute("style")).ToList();

            return (addedPartsColor.Count > 0 && addedPartsColor.TrueForAll(attr => (attr != RgbWhiteColor && attr != WhiteColor)))
                || DriverExtensions.GetElements(DeletedPartLocator).Count > 0;
        }
    }
}