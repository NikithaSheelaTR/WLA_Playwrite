namespace Framework.Common.UI.Products.WestLawNext.Pages.CompanyInvestigator
{
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Enums.Reports;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The company investigator report page.
    /// </summary>
    public class CompanyInvestigatorReportPage : CommonReportDocumentPage
    {
        private const string SectionLctMask = "//*[@id='tocContainer']//a[@class='bi_sectionName' and text()='{0}']";

        private static readonly By ContentElementsLocator = By.XPath(".//div[@class='co_bi_bodyContainer']//a");

        private static readonly By ContentTableLocator = By.ClassName("co_bi_sectionContainer");

        private static readonly By ContentTitlesLocator = By.XPath(".//a[@class='expandCollapseControl']");

        private static readonly By SectionContentLocator = By.XPath(".//div[@class='co_bi_bodyContainer']");

        private static readonly By TitleLocator = By.Id("co_bi_header_title");
        
        /// <summary>
        /// Section document by index.
        /// </summary>
        /// <param name="sectionName">
        /// text of section
        /// </param>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <returns>
        /// The <see cref="CompanyInvestigatorDocumentPage"/>.
        /// </returns>
        public CompanyInvestigatorDocumentPage ClickSectionDocumentByIndex(CompanyInvestigatorReportSections sectionName, int index)
        {
            DriverExtensions.GetElements(this.GetSectionElement(sectionName), ContentElementsLocator).ElementAt(index).Click();
            DriverExtensions.WaitForPageLoad();
            return new CompanyInvestigatorDocumentPage();
        }

        /// <summary>
        /// Returns the TitleLocator of the Report
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string GetDocumentTitle() => DriverExtensions.GetText(TitleLocator);

        /// <summary>
        /// Section document by index.
        /// </summary>
        /// <param name="sectionName">
        /// text of section
        /// </param>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetDocumentTitleByIndex(CompanyInvestigatorReportSections sectionName, int index) =>
            DriverExtensions.GetElements(this.GetSectionElement(sectionName), ContentElementsLocator).ElementAt(index).Text;

        /// <summary>
        /// The go to section.
        /// </summary>
        /// <param name="sectionName">
        /// The section name.
        /// </param>
        public void GoToSection(CompanyInvestigatorReportSections sectionName) =>
            DriverExtensions.Click(By.XPath(string.Format(SectionLctMask, sectionName.GetEnumTextValue())));

        /// <summary>
        /// The subsidiaries section has document open.
        /// </summary>
        /// <param name="sectionName">
        /// text of section
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsDocumentOpenedInSection(CompanyInvestigatorReportSections sectionName)
        {
            string sectionText = DriverExtensions.WaitForElement(this.GetSectionElement(sectionName), SectionContentLocator).Text;
            return sectionText.Contains("Source") & sectionText.Contains("EXHIBIT");
        }

        private IWebElement GetSectionElement(CompanyInvestigatorReportSections sectionName) =>
            DriverExtensions.GetElements(ContentTableLocator)
            .First(e => DriverExtensions.WaitForElement(e, ContentTitlesLocator)
            .GetAttribute("title") == $"Toggle {sectionName.GetEnumTextValue()}");
    }
}