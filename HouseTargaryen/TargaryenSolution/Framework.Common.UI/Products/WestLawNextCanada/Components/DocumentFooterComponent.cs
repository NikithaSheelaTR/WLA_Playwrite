namespace Framework.Common.UI.Products.WestLawNextCanada.Components
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    /// <summary>
    /// Document Page
    /// </summary>
    public class DocumentFooterComponent
    {
        private static readonly By FootNoteSectionLocator = By.XPath(".//*[@class='crsw_compositeDocumentSectionHeaders']");

        private static readonly By FootNoteSectionTitleLocator = By.XPath("//*[@class='co_footnoteSectionTitle co_printHeading']");

        /// <summary>
        /// Section Header from Document
        /// </summary>
        /// <returns> Summary Text </returns>
        public IReadOnlyCollection<IWebElement> SectionHeaders => DriverExtensions.GetElements(FootNoteSectionLocator);

        /// <summary>
        /// Gets FootNote Section Tittle Text from Document 
        /// </summary>
        /// <returns> Summary Text </returns>
        public IReadOnlyCollection<IWebElement> FootNoteSectionTitles => DriverExtensions.GetElements(FootNoteSectionTitleLocator);
    }
}
