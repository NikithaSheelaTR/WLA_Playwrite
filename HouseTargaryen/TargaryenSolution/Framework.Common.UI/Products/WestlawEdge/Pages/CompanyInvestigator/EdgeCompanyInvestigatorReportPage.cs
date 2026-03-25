namespace Framework.Common.UI.Products.WestlawEdge.Pages.CompanyInvestigator
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;
    using System.Collections.Generic;    

    /// <summary>
    /// The company investigator report page.
    /// </summary>
    public class EdgeCompanyInvestigatorReportPage : EdgeCommonReportDocumentPage
    {
        private const string SectionLctMask = "//*[@id='tocContainer']//a[@class='bi_sectionName' and text()='{0}']";

        private const string ContentTableLctMask = "//div[@class='co_bi_sectionContainer' and .//a[@class='expandCollapseControl' and @title='Toggle {0}']]";

        private static readonly By ContentElementsLocator = By.XPath("//div[@class='co_bi_bodyContainer']//a");

        private static readonly By TitleLocator = By.XPath("//div[@id='co_bi_header_title']//h1");

        private static readonly By SectionContentLocator = By.XPath(".//div[@class='co_bi_bodyContainer']");

        /// <summary>
        /// Section document by name.
        /// </summary>
        /// <param name="sectionName">
        /// text of section
        /// </param>
        /// <returns>
        /// The <see cref="EdgeCompanyInvestigatorDocumentPage"/>.
        /// </returns>
        public EdgeCompanyInvestigatorDocumentPage ClickSectionDocumentByName(string sectionName)
        {
            DriverExtensions.GetElement(this.GetSectionElementByName(sectionName), ContentElementsLocator).CustomClick();
            return new EdgeCompanyInvestigatorDocumentPage();
        }

        /// <summary>
        /// The subsidiaries section has document open.
        /// </summary>
        /// <param name="sectionName">
        /// text of section
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsDocumentOpenedInSection(string sectionName)
        {
            string sectionText = DriverExtensions.WaitForElement(this.GetSectionElementByName(sectionName), SectionContentLocator).Text;
            return sectionText.Contains("Source") & sectionText.Contains("EXHIBIT");
        }

        /// <summary>
        /// Section document by name.
        /// </summary>
        /// <param name="sectionName">
        /// text of section
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetDocumentTitleByName(string sectionName) =>
            DriverExtensions.GetElement(this.GetSectionElementByName(sectionName), ContentElementsLocator).Text;

        /// <summary>
        /// The go to section.
        /// </summary>
        /// <param name="sectionName">
        /// The section name.
        /// </param>
        public void GoToSection(string sectionName) =>
            DriverExtensions.Click(By.XPath(string.Format(SectionLctMask, sectionName)));


        /// <summary>
        /// Returns the TitleLocator of the Report
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string GetDocumentTitle() => DriverExtensions.GetText(TitleLocator);

        /// <summary>
        /// Getting section element by name
        /// </summary>
        /// <param name="sectionName">Section name</param>
        /// <returns></returns>
        private IWebElement GetSectionElementByName(string sectionName) =>
            DriverExtensions.GetElement(By.XPath(string.Format(ContentTableLctMask, sectionName)));

        /// <summary>
        /// Document passages links
        /// </summary>
        public IReadOnlyCollection<ILink> DocumentPassageLinks => new ElementsCollection<Link>(ContentElementsLocator);
    }
}