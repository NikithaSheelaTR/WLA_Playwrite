namespace Framework.Common.UI.Products.WestLawNext.Pages.SecondarySources
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// SecondarySourcesTablePage
    /// </summary>
    public class SecondarySourcesTablePage : BaseModuleRegressionPage
    {
        private const string TableRowValuesLctMask =
            "(//div[@class='co_fullscreenTable']//table//tr[{0}]/td)|(//div[@class='co_fullscreenTable']//table//tr[{0}]/th)";

        /// <summary>
        /// The are row values correct.
        /// </summary>
        /// <param name="rowNumber"> The row number. </param>
        /// <param name="rowValues"> The row values. </param>
        /// <returns> True if values are correct, false otherwise </returns>
        public bool AreRowValuesCorrect(int rowNumber, List<string> rowValues) =>
            DriverExtensions.GetElements(By.XPath(string.Format(TableRowValuesLctMask, rowNumber)))
                            .Select(value => value.Text).SequenceEqual(rowValues);
    }
}
