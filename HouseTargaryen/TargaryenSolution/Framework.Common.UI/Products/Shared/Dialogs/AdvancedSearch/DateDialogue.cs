namespace Framework.Common.UI.Products.Shared.Dialogs.AdvancedSearch
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Enums;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;
    using OpenQA.Selenium;
    using System;
    using System.Linq;

    /// <summary>
    /// Describe dialog for Date Options
    /// </summary>
    public class DateDialogue : BaseModuleRegressionDialog
    {
        private static readonly By ContainerLocator = By.XPath("//div[@class='co_overlayBox_container'][@id='coid_lightboxDateWidget']");
        private static readonly By ContinueLocator = By.XPath(".//button[contains(@class, 'a11yDateWidget-submit') and contains(text(), 'Continue')]");

        private EnumPropertyMapper<DateRangeOptions, DateRangeOptionsModel> dateRangeOptionsMap;

        /// <summary>
        /// Gets the date options type enumeration to DateRangeOptionsModel map.
        /// </summary>
        protected EnumPropertyMapper<DateRangeOptions, DateRangeOptionsModel> DateRangeOptionsMap =>
            this.dateRangeOptionsMap = this.dateRangeOptionsMap
                                       ?? EnumPropertyModelCache.GetMap<DateRangeOptions, DateRangeOptionsModel>();

        /// <summary>
        /// Date option radiobutton
        /// </summary>
        public IRadiobutton DateOptionRadiobutton(DateRangeOptions option) => new Radiobutton(ContainerLocator, By.XPath(string.Format(this.DateRangeOptionsMap[option].LocatorString)));

        /// <summary>
        /// ApplyButton button
        /// </summary>
        public IButton ApplyButton => new Button(ContainerLocator, ContinueLocator);

        /// <summary>
        /// Select and fill option
        /// <param name="option"> option </param>
        /// <param name="firstDate"> the first date </param>
        /// <param name="secondDate"> the second date </param>
        /// <returns> New instance of the page</returns>>
        /// </summary>
        public T SelectDateOption<T>(DateRangeOptions option, string firstDate = null, string secondDate = null) where T : ICreatablePageObject
        {
            this.DateOptionRadiobutton(option).Select();

            string[] DateCompare = { "All Dates Before", "All Dates After", "Specific Date", "Date Range" };

            if (DateCompare.Any(item => item.Equals(this.dateRangeOptionsMap[option].Text, StringComparison.OrdinalIgnoreCase)))
            {
                if (firstDate != null)
                {
                    DriverExtensions.SetTextField(firstDate, By.XPath(string.Format(this.dateRangeOptionsMap[option].FirstFieldLocatorString)));
                }
                if (secondDate != null)
                {
                    DriverExtensions.SetTextField(secondDate, By.XPath(string.Format(this.dateRangeOptionsMap[option].SecondFieldLocatorString)));
                }
            }
            this.ApplyButton.Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Get the count for a specific date option
        /// </summary>
        /// <param name="option"> date range to get the count for</param>
        /// <returns> The <see cref="int"/>. </returns>
        public int GetDateRangeCount(DateRangeOptions option)
        {

            int count = DriverExtensions.WaitForElement(By.XPath(this.DateRangeOptionsMap[option].OptionCountLocator)).Text.ConvertCountToInt();

            return count;
        }
    }
}

