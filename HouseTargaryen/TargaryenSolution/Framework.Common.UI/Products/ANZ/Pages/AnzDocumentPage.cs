namespace Framework.Common.UI.Products.ANZ.Pages
{
    using Framework.Common.UI.Products.ANZ.DropDowns;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// ANZ Document Page
    /// </summary>
    public class AnzDocumentPage : EdgeCommonDocumentPage
    {       
        private static readonly By EditorsNotesLocator = By.XPath("//h2[@id='co_headnoteHeader']/preceding-sibling::div/div");
        private const string CaseVersionLocator = "//div[@class='co_cites' and text()='{0}']";

        /// <summary>
        /// Other Versions dropdown
        /// </summary>
        public EdgeOtherVersionsDropdown EdgeOtherVersions { get; } = new EdgeOtherVersionsDropdown();

        /// <summary>
        /// The is Case Version Doc displayed.
        /// </summary>
        /// <param name="caseVersionName">
        /// The caseVersionName.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsWestlawVersionLinkDisplayed(string caseVersionName) => DriverExtensions.IsDisplayed(By.XPath(string.Format(CaseVersionLocator, caseVersionName)));

        /// <summary>
        /// Verifies whether the part from the Editor's Notes is displayed in document
        /// </summary>
        /// <returns> true if the tab part is present </returns>
        public bool IsEditorsNoteDisplayed() => DriverExtensions.IsDisplayed(EditorsNotesLocator);    
    }
}
