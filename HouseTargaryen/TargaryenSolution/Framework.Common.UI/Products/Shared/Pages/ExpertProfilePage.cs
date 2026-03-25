namespace Framework.Common.UI.Products.Shared.Pages
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Enums.Document;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.Shared.Pages.Document;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Page for Expert Profiles
    /// </summary>
    public class ExpertProfilePage : CommonDocumentPage
    {
        private static readonly By AreaOfExpertiseLocator = By.XPath(".//div[contains(@class,'co_areaOfExpertiseSection')]//div[@class='co_paragraph']");

        private static readonly By ContactInformationLocator = By.XPath("//div[contains(preceding-sibling::div[1]//text(),'Contact Information')]");           

        private static readonly By ProfilePhotoLocator = By.ClassName("co_profilerImage");

        private static readonly By ProfileRtgTitleLocator = By.ClassName("co_profilerTitle");

        private static readonly By ProfileTabLocator = By.Id("coid_DocumentTab_link");

        private static readonly By ProfileLinkLocator = By.XPath("//div[contains(@class,'co_document')]//*[contains(@class,'co_link')]");

        private static readonly By ReportedLineLocator = By.XPath(".//div[contains(@class,'co_areaOfExpertiseSection')]/following-sibling::div[@class='co_paragraph']");

        private static readonly By TableLocator = By.XPath("//div[contains(@class,'co_fullscreenTable')]//table");

        private static readonly By TotalNumberLocator = By.XPath("//div[contains(@class, 'Total')]/strong");
      
        private EnumPropertyMapper<DocumentTab, WebElementInfo> documentTabMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpertProfilePage"/> class.
        /// </summary>
        public ExpertProfilePage()
        {
            DriverExtensions.WaitForElement(ProfileTabLocator);
        }

        /// <summary>
        /// Gets the DocumentIcon enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<DocumentTab, WebElementInfo> DocumentTabMap
            => this.documentTabMap = this.documentTabMap ?? EnumPropertyModelCache.GetMap<DocumentTab, WebElementInfo>();

        /// <summary>
        /// Checks if the appropriate RTG values are present in the profiles page source
        /// </summary>
        /// <returns></returns>
        public bool AreRtgValuesPresent() => DriverExtensions.IsDisplayed(ProfilePhotoLocator, 3)
                                            && DriverExtensions.IsDisplayed(ProfileRtgTitleLocator, 3);

        /// <summary>
        /// Clicks tab of the expert profile document
        /// </summary>
        /// <param name="tab">The tab</param>
        /// <typeparam name="T">PageObject</typeparam>
        /// <returns>Instance of the page</returns>
        public T ClickTab<T>(DocumentTab tab) where T : ICreatablePageObject
        {           
            DriverExtensions.WaitForElement(By.Id(this.DocumentTabMap[tab].Id)).Click();
            return DriverExtensions.CreatePageInstance<T>();
        } 

        /// <summary>
        /// The get expert name.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetExpertName() => DriverExtensions.GetText(ContactInformationLocator);

        /// <summary>
        /// gets tab name.
        /// </summary>
        /// <param name="tab">The tab</param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetTabName(DocumentTab tab) => DriverExtensions.GetText(By.Id(this.DocumentTabMap[tab].Id));

        /// <summary>
        /// Gets the tab number of a profile page
        /// </summary>
        /// <param name="tab">The name of the tab</param>
        /// <returns>Tab Count of profile page</returns>
        public int GetTabNumber(DocumentTab tab) => DriverExtensions.GetElement(By.Id(this.DocumentTabMap[tab].Id)).Text.RetrieveCountFromBrackets();

        /// <summary>
        /// Gets the total number of a profile page
        /// </summary>
        /// <returns>Count of profile page</returns>
        public int GetTotalNumber() => DriverExtensions.GetText(TotalNumberLocator).ConvertCountToInt();

        /// <summary>
        /// Get reported line
        /// </summary>
        /// <returns>The text of Reported Line is displayed</returns>
        public string GetReportedLine() => DriverExtensions.GetText(ReportedLineLocator);

        /// <summary>
        /// Get Area of expertise
        /// </summary>
        /// <returns>The text of Area of Expertise is displayed</returns>
        public string GetAreaOfExpertise() => DriverExtensions.GetText(AreaOfExpertiseLocator);

        /// <summary>
        /// Checks if the page is displayed
        /// </summary>
        public bool IsPageVisible() => DriverExtensions.IsDisplayed(ProfileTabLocator);

        /// <summary>
        /// Checks if the Profile Link is displayed
        /// </summary>
        /// <returns>True if Profile Link is displayed</returns>
        public bool IsProfileLinkDisplayed() => DriverExtensions.IsDisplayed(ProfileLinkLocator);

        /// <summary>
        /// Checks if the Table Link is displayed
        /// </summary>
        /// <returns>True if Table Link is displayed</returns>
        public bool IsTableDisplayed() => DriverExtensions.IsDisplayed(TableLocator);

        /// <summary>
        /// Checks is tab active
        /// </summary>
        /// <param name="tab">The tab</param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsTabActive(DocumentTab tab) => 
            !DriverExtensions.WaitForElement(By.Id(this.DocumentTabMap[tab].Id)).GetAttribute("class").Contains("co_tabInactive");           
    }
}