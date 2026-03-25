namespace Framework.Common.UI.Products.WestLawNext.Components.CaseEvaluator.TableComponents
{
    using System.Collections.Generic;

    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The date header component.
    /// </summary>
    public class DateHeaderComponent : BaseModuleRegressionComponent
    {
        private const string ArrowLctMask = "//div[@id='{0}']//*[@id='{1}']//a";

        private const string DateHeaderColumnLctMask = "//div[@id='{0}']//th[{1}]//strong";

        private const string NextArrowId = "Th1";

        private const string HeaderComponentLctMask = "//div[@id='{0}']//table[@class='co_documentReportTable']/thead/tr";

        private const string TableLctMask = "//div[@id='{0}']//table";

        private const string RowLctMask = "./tbody/tr[{0}]";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => By.XPath(string.Format(TableLctMask, this.TableId));

        private string PreviousArrowId { get; }

        private string TableId { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateHeaderComponent"/> class.
        /// </summary>
        /// <param name="tableId">The table id.</param>
        /// <param name="previousArrowId">The previous arrow id.</param>
        public DateHeaderComponent(string tableId, string previousArrowId)
        {
            this.TableId = tableId;
            this.PreviousArrowId = previousArrowId;
        }

        /// <summary>
        /// Get the Filter Date Years in the Sub Header Row
        /// </summary>
        /// <returns>list of years</returns>
        public List<string> GetDates(int rowNum=1)
        {
            // Create empty list, find first filter date column, click previous year until visible
            var dateHeadersList = new List<string>();

            By previousArrowLocator = By.XPath(string.Format(ArrowLctMask, this.TableId, this.PreviousArrowId));
            By nextArrowLocator = By.XPath(string.Format(ArrowLctMask, this.TableId, NextArrowId));
            By firstDateColumnLocator = By.XPath(string.Format(DateHeaderColumnLctMask, this.TableId, 3));
            By rowLocator = By.XPath(string.Format(RowLctMask, rowNum));
            DriverExtensions.ScrollTo(DriverExtensions.WaitForElement(ComponentLocator), rowLocator);

            while (!DriverExtensions.IsDisplayed(firstDateColumnLocator))
            {
                DriverExtensions.WaitForElement(previousArrowLocator);
                DriverExtensions.GetElement(previousArrowLocator).Click();
            }

            int i = 3;
            while (true)
            {
                By currentDateColumn = By.XPath(string.Format(DateHeaderColumnLctMask, this.TableId, i));

                if (!DriverExtensions.IsDisplayed(currentDateColumn))
                {
                    if (DriverExtensions.WaitForElement(nextArrowLocator).GetAttribute("class").Contains("co_disabled"))
                    {
                        break;
                    }

                    DriverExtensions.GetElement(nextArrowLocator).Click();
                }

                string currentDate = DriverExtensions.WaitForElement(currentDateColumn).Text;
                dateHeadersList.Add(currentDate);
                i++;
            }

            return dateHeadersList;
        }
    }
}