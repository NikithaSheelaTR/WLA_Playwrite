namespace Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo.StatuteHistoryPages
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// EditorsAndRevisorsNotesPage
    /// </summary>
    public class EditorsAndRevisorsNotesPage : TabPage
    {
        private static readonly By EditorAndRevisorsContentContainerLocator =
            By.Id("coid_website_relatedInformationContent");

        /// <summary>
        /// Get Editor And Revisors Contentext Text
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetEditorAndRevisorsContenText()
            => DriverExtensions.WaitForElement(EditorAndRevisorsContentContainerLocator).Text;
    }
}
