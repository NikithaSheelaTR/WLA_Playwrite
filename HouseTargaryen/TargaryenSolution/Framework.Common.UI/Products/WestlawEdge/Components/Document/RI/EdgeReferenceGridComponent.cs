namespace Framework.Common.UI.Products.WestlawEdge.Components.Document.RI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components.Document.RI;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// ReferenceGrid
    /// </summary>
    public class EdgeReferenceGridComponent : ReferenceGridComponent
    {
        private static readonly By ImpliedOverrulingIconLocator = By.XPath(".//img[contains(@src, 'caution_orange_small.png') or contains(@src, 'caution_orange_small.svg')]");

        private static readonly By KeyCiteOverrulingRiskIconLinkLocator =
            By.XPath("//a[contains(@id,'coid_relatedInfo_impliedOverrulings')]");
        
        private readonly string gridTableLocator;

        /// <summary>
        /// Initializes a new instance of the <see cref="EdgeReferenceGridComponent"/> class. 
        /// </summary>
        /// <param name="gridTableLocator"> Locator to identify the table element for the grid </param>
        public EdgeReferenceGridComponent(string gridTableLocator) : base(gridTableLocator)
        {
            this.gridTableLocator = gridTableLocator;
        }

        /// <summary>
        /// Verify that grid row contains implied overruling icon
        /// </summary>
        /// <param name="gridRowIndex"> Row index in the results grid </param>
        /// <returns> True if icon is displayed, false otherwise </returns>
        public bool IsGridRowContainsImpliedOverrulingIcon(int gridRowIndex)
        {
            IWebElement gridRow = this.GetGridRows()[gridRowIndex];
            return this.IsGridRowContainsElement(gridRow);
        }

        /// <summary>
        /// Click on the KeyCiteOverrulingRisk image in the random row.
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>The new instance of T page</returns>
        public T ClickGridRowOverrulingRiskFlag<T>() where T : ICreatablePageObject
        {
            var gridRows = this.GetGridRows();
            var randomRowIndex = new Random().Next(0, gridRows.Count);
           while(true)
            {
                if (this.IsGridRowContainsImpliedOverrulingIcon(randomRowIndex))
                {
                    DriverExtensions.GetElement(this.GetGridRows()[randomRowIndex], ImpliedOverrulingIconLocator).JavascriptClick();
                    break;
                }
                else
                    randomRowIndex = new Random().Next(0, gridRows.Count);
            }
            return DriverExtensions.CreatePageInstance<T>();
        }


        private bool IsGridRowContainsElement(IWebElement gridRow)
        {
            IWebElement icon;
            DriverExtensions.TryGetElement(gridRow, ImpliedOverrulingIconLocator, out icon);
            return icon != null;
        }

        /// <summary>
        /// Get the rows from the results grid.
        /// </summary>
        /// <returns> Elements list from the grid </returns>
        private List<IWebElement> GetGridRows()
        {
            string xPath = $"{this.gridTableLocator}/tbody//tr";
            By itemLocator = By.XPath(xPath);
            DriverExtensions.WaitForElementDisplayed(itemLocator);
            return DriverExtensions.GetElements(itemLocator).ToList();
        }
    }
}
