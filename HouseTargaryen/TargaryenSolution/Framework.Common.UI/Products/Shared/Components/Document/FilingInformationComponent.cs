namespace Framework.Common.UI.Products.Shared.Components.Document
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Filing Information Component
    /// </summary>
    public class FilingInformationComponent : BaseModuleRegressionComponent
    {
        private static readonly By FilingInformationTitleLocator = By.Id("co_docketFilingInformation");

        private static readonly By HeadersTitlesLocator =
            By.XPath("//h2[@id='co_docketFilingInformation']/following-sibling::table[1]/tbody/tr/th");

        private static readonly By ContainerLocator
            = By.XPath("//h2[@id='co_docketFilingInformation'] | //div[@id='co_document_0']/table[@class='co_docketsTable'][2]");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets Title of the Filing information block without number of documents inside the block
        /// </summary>
        public string Title
        {
            get
            {
                string fullTitle = DriverExtensions.GetElement(FilingInformationTitleLocator).Text;
                return fullTitle.Remove(fullTitle.IndexOf("(")).Trim();
            }
        }

        /// <summary>
        /// Gets headers of the Filing Information block
        /// </summary>
        /// <returns> Headers Of The Block </returns>
        public List<string> GetHeadersOfTheBlock() => DriverExtensions.GetElements(HeadersTitlesLocator).Select(element => element.Text).ToList();
    }
}