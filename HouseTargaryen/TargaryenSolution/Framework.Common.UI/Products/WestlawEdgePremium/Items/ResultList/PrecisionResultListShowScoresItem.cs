namespace Framework.Common.UI.Products.WestlawEdgePremium.Items.ResultList
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Precision result list item with score data.
    /// </summary>
    public class PrecisionResultListShowScoresItem : PrecisionResultListItem
    {
        private static readonly By ContainerScoreDataLocator = By.XPath(".//div[@class='Athens-browseBox-question']");

        /// <summary>
        /// Initializes a new instance of the <see cref="PrecisionResultListShowScoresItem"/> class. 
        /// </summary>
        /// <param name="containerElement"> container </param>
        public PrecisionResultListShowScoresItem(IWebElement containerElement) : base(containerElement)
        {
        }

        /// <summary>
        /// Gets the list of score data .
        /// </summary>
        /// <returns>The list of score data</returns>
        /// 
        public IReadOnlyCollection<string> scoreData => DriverExtensions.GetElements(ContainerScoreDataLocator).Select(e => e.Text).ToList();
    }
}
