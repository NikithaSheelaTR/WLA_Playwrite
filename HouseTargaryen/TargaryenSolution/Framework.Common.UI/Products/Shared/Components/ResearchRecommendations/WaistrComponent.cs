namespace Framework.Common.UI.Products.Shared.Components.ResearchRecommendations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Dialogs.ResearchRecommendations;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Fields and methods for 'Why are these my results?' ('Why am I see/getting these recommendations') section
    /// </summary>
    public class WaistrComponent : BaseModuleRegressionComponent
    {
        private const string DocTitleByNumberLctMask = "//a[@id='cobalt_ra_trail_case_title{0}']";

        private static readonly By ResetButtonLocator = By.XPath("//*[@class='js-ra-reset co_defaultBtn']");

        private static readonly By ItemLocator = By.XPath("//a[contains(@id,'cobalt_ra_trail_case_title')]");

        private static readonly By DownloadIconLocator = By.XPath("//div[@id='coid_explanationBody']//span[@class='icon25 icon_rrDelivery']");

        private static readonly By ExplanationBodyLocator = By.Id("coid_explanationBody");

        private static readonly By GlassesIconLocator =
            By.XPath("//div[@id='coid_explanationBody']//*[@class='icon25 icon_glasses']");

        private static readonly By CitationLocator = By.XPath("//div[@id='coid_explanationBody']//span[3]");

        private static readonly By CourtLocator =
            By.XPath("//div[@id='coid_explanationBody']//span[1][contains(text(),'Court')]");

        private static readonly By DateLocator = By.XPath("//div[@id='coid_explanationBody']//span[2]");

        private static readonly By OpenWaistrLocator = By.XPath("//div[@id='coid_explanationHeading']/button");

        private static readonly By SaveToFolderIconLocator = By.XPath("//div[@id='coid_explanationBody']//*[@class='icon25 icon_foldered']");

        private static readonly By StatutePublicationLocator = By.XPath("//*[@id='coid_explanationBody']//span[2]");

        private static readonly By StatuteTitleLocator = By.XPath("//*[@id='coid_explanationBody']//span[3]");

        private static readonly By ContainerLocator = By.XPath("//div[@id='coid_explanationBody' or @id='coid_explanationHeading']");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Click document title link
        /// </summary>
        /// <param name="itemNumber">number of the doc to click</param>
        /// <typeparam name="T">ICreatable PageObject</typeparam>
        /// <returns>PageObject</returns>
        public T ClickDocLinkByNumber<T>(int itemNumber) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(By.XPath(string.Format(DocTitleByNumberLctMask, itemNumber - 1)), 50000).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click Reset button
        /// </summary>
        /// <returns>new instance of RaResetModal</returns>
        public ResetDialog ClickResetButton()
        {
            DriverExtensions.WaitForElementDisplayed(ResetButtonLocator).Click();
            return new ResetDialog();
        }

        /// <summary>
        /// Click 'Why are these my results?' link to open/close WAISTR section
        /// </summary>
        public void ClickWaistrLink()
        {
            DriverExtensions.Click(DriverExtensions.WaitForElementDisplayed(OpenWaistrLocator));
            DriverExtensions.WaitForElementDisplayed(ResetButtonLocator);
        }

        /// <summary>
        /// Get list of Citations displayed under document titles
        /// </summary>
        /// <returns>list of Citations</returns>
        public List<string> GetCitationsList() => DriverExtensions.GetElements(CitationLocator).Select(element => element.Text).ToList();

        /// <summary>
        /// Get list of document item titles
        /// </summary>
        /// <returns>list of document item titles</returns>
        public List<string> GetDocItemTitles() => DriverExtensions.GetElements(ItemLocator).Select(i => i.Text).ToList();

        /// <summary>
        /// Get number of docs with Court section
        /// </summary>
        /// <returns>number of docs with Court section</returns>
        public int GetDocsNumWithCourtSection() => DriverExtensions.GetElements(CourtLocator).Count;

        /// <summary>
        /// Get number of docs with Download icon
        /// </summary>
        /// <returns>number of docs with Download icon</returns>
        public int GetDownloadIconsNumber() => DriverExtensions.GetElements(DownloadIconLocator).Count;

        /// <summary>
        /// Get number of docs with Save to folder icon
        /// </summary>
        /// <returns>number of docs with Save to folder icon</returns>
        public int GetFolderedIconsNumber() => DriverExtensions.GetElements(SaveToFolderIconLocator).Count;

        /// <summary>
        /// Get number of docs with Glasses icon
        /// </summary>
        /// <returns>number of docs with Glasses icon</returns>
        public int GetGlassesIconsNumber() => DriverExtensions.GetElements(GlassesIconLocator).Count;

        /// <summary>
        /// Method returns the text found in the Publication section of the metadata
        /// </summary>
        /// <returns>List of the publications</returns>
        public List<string> GetPublicationList() => DriverExtensions.GetElements(StatutePublicationLocator).Select(element => element.Text).ToList();

        /// <summary>
        /// Method returns the text found in the Title elements in the WATMR
        /// </summary>
        /// <returns>List of the titles</returns>
        public List<string> GetTitleList() => DriverExtensions.GetElements(StatuteTitleLocator).Select(element => element.Text).ToList();

        /// <summary>
        /// Verify if Reset button is displayed
        /// </summary>
        /// <returns>true if Reset button is displayed, false otherwise</returns>
        public bool IsResetButtonDisplayed() => DriverExtensions.IsDisplayed(ResetButtonLocator, 5);

        /// <summary>
        /// Verify correct Date sections are displayed for all documents 
        /// </summary>
        /// <param name="docNumber">number of documents in WAISTR section</param>
        /// <returns>true if correct Date present for all documents, false otherwise</returns>
        public bool IsDateDisplayedForAllDocs(int docNumber)
        {
            DateTime dateResult;
            IReadOnlyCollection<IWebElement> elements = DriverExtensions.GetElements(DateLocator);
            return elements.Count >= docNumber && elements.All(element => DateTime.TryParse(element.Text, out dateResult));
        }

        /// <summary>
        /// Verify specified Document title is present and is a link
        /// </summary>
        /// <param name="itemNumber">number of the doc to verify. Starts from zero</param>
        /// <returns>true if title is present and is a link, false otherwise</returns>
        public bool IsDocTitleLinkDisplayed(int itemNumber) =>
            DriverExtensions.IsDisplayed(By.XPath(string.Format(DocTitleByNumberLctMask, itemNumber)), 5)
            && !string.IsNullOrEmpty(DriverExtensions.WaitForElementDisplayed(By.XPath(string.Format(DocTitleByNumberLctMask, itemNumber))).GetAttribute("href"));

        /// <summary>
        /// Verify if "Why are these my results?" expanded
        /// </summary>
        /// <returns>true if expanded, false otherwise</returns>
        public bool IsExplanationBodyDisplayed() => DriverExtensions.IsDisplayed(ExplanationBodyLocator, 5);

        /// <summary>
        /// Verify 'Why are these my results?' link is displayed
        /// </summary>
        /// <returns>true if 'Why are these my results?' link is displayed, false otherwise</returns>
        public bool IsWaistrLinkDisplayed() => DriverExtensions.IsDisplayed(ExplanationBodyLocator, 5);
    }
}