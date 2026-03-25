namespace Framework.Common.UI.Products.WestLawNext.Components.BusinessLawCenterPowerSearch.ContentComponents
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.WestLawNext.Pages.BusinessLawCenterPowerSearch;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The exhibits table.
    /// </summary>
    public class ExhibitsTableComponent : BaseModuleRegressionComponent
    {
        private static readonly By HeaderLocator = By.XPath("//div[@id='documentFilingHeader']/div/h2");

        private static readonly By TableItemLocator = By.XPath("//div[contains(@class, 'documentItems')]/ul/li[2]");

        private static readonly By TableOfContentLocator =
            By.XPath("//div[contains(@class, 'threeColDocument')]/div[@class='documentContainer']");

        private static readonly By ContainerLocator = By.ClassName("threeColDocument");

        /// <summary>
        /// Initializes a new instance of the <see cref="ExhibitsTableComponent"/> class. 
        /// </summary>
        public ExhibitsTableComponent()
        {
            DriverExtensions.WaitForElementDisplayed(TableItemLocator);
        }

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the table of content.
        /// </summary>
        private IList<IWebElement> TableOfContenItems
            => DriverExtensions.GetElements(DriverExtensions.WaitForElementDisplayed(TableOfContentLocator), TableItemLocator);

        /// <summary>
        /// The click on table of content row.
        /// </summary>
        /// <param name="row">The row to click, numeration starts from 0</param>
        /// <returns>The <see cref="FilingsDetailsPage"/>.</returns>
        public FilingsDetailsPage ClickOnTableOfContentRow(int row = 0)
        {
            DriverExtensions.Click(this.TableOfContenItems.ElementAt(row));
            return new FilingsDetailsPage();
        }

        /// <summary>
        /// The get filing header name.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetFilingHeaderName() => DriverExtensions.WaitForElement(HeaderLocator).Text;

        /// <summary>
        /// The get TOC item number.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        public int GetTocItemCount() => this.TableOfContenItems.Count;

        /// <summary>
        /// The get TOC item name.
        /// </summary>
        /// <param name="row">The row to click, numeration starts from 0</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetTocItemName(int row = 0) => this.TableOfContenItems.ElementAt(row).Text;
    }
}