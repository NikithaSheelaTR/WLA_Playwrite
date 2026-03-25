namespace Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck
{
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Delivery tray dialog
    /// </summary>
    public class QuickCheckReportTrayComponent : ItemsCollection<QuickCheckTrayItem>
    {
        private static readonly By ReportItemLocator = By.XPath("//li[contains(@class, 'co_deliveryQueue')]");
        private static readonly By CollapseIconLocator = By.ClassName("co_widget_collapseIcon");
        private static readonly By DocAnalyzerReportTrayLocator = By.XPath("//div[contains(@class,'DA-ProgressTrayContainer')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="QuickCheckReportTrayComponent"/> class.
        /// </summary>
        public QuickCheckReportTrayComponent() : base(DocAnalyzerReportTrayLocator, ReportItemLocator)
        {
        }

        /// <summary>
        /// Collapse current dialog
        /// </summary>
        public void Collapse() => DriverExtensions.WaitForElement(CollapseIconLocator).Click();

        /// <summary>
        /// Expand Tray component
        /// </summary>
        /// <returns>Current dialog</returns>
        public QuickCheckReportTrayComponent Expand()
        {
            if (!DriverExtensions.IsElementPresent(CollapseIconLocator))
            {
                DriverExtensions.GetElement(DocAnalyzerReportTrayLocator).Click();
            }

            DriverExtensions.WaitForCondition(condition => base.GetItems().ToList().TrueForAll(el => el.DocumentNameLabel.Displayed));
            return this;
        }

        /// <summary>
        /// The wait until file upload.
        /// </summary>
        public void WaitUntilFileUpload() => DriverExtensions.WaitForCondition(condition =>   base.GetItems().First().ReportStatusLabel.Text.Equals("Complete"), 60);
    }
}