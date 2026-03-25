namespace Framework.Common.UI.Products.WestLawNextMobile.Pages.RelatedInformation
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Common page for CitingReference, Fillings and NegativeTreatment pages
    /// </summary>
    public class CommonRiPage : BaseRelatedInfoPage
    {
        private const string ContentIdLctMask = "coid_relatedInfo_LinkToGuid_{0}";
        
        /// <summary>
        /// Determines if the link returns content
        /// </summary>
        /// <param name="guid"> The guid. </param>
        /// <returns> True if returned content. </returns>
        public bool IsContentByGuidDisplayed(string guid) => DriverExtensions.IsDisplayed(By.Id(string.Format(ContentIdLctMask, guid)), 5);

        /// <summary>
        /// Determines if the link returns content
        /// </summary>
        /// <param name="index"> The index. </param>
        /// <returns> True if returned content. </returns>
        public bool IsContentByIndexDisplayed(int index) => DriverExtensions.IsDisplayed(By.Id(string.Format(ContentIdLctMask, index)), 5);
    }
}
