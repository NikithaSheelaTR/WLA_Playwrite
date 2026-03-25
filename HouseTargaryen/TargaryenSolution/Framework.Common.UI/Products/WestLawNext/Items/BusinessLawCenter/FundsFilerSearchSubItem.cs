namespace Framework.Common.UI.Products.WestLawNext.Items.BusinessLawCenter
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.WestLawNext.Enums.BusinessLawCenter;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Funds Filer Search Sub Item
    /// </summary>
    public class FundsFilerSearchSubItem : BaseFilerSearchItem
    {
        private static readonly By FilerSearchTitleLocator = By.XPath(".//a[@class='ng-binding']");

        /// <summary>
        /// Initializes a new instance of the <see cref="FundsFilerSearchSubItem" /> class. 
        /// </summary>
        /// <param name="container">Container</param> 
        public FundsFilerSearchSubItem(IWebElement container)
            : base(container, "FundsSub")
        {
            this.TitleLocator = FilerSearchTitleLocator;
        }

        /// <summary>
        /// Is Series Link Highlighted
        /// </summary>
        /// <param name="seriesName">Series Name</param>
        /// <param name="option">Option</param>
        /// <returns>True - if it is highlighted, false - otherwise</returns>
        public bool VerifyHighlightSeriesLink(string seriesName, BlcItemOptions option) =>
            DriverExtensions.IsElementPresent(
                DriverExtensions.WaitForElement(
                    this.Container,
                    By.XPath(string.Format(this.Map[option].LocatorMask, seriesName))));

        /// <summary>
        /// Is Series Class Highlighted
        /// </summary>
        /// <param name="classId">Class Id</param>
        /// <param name="option">Option</param>
        /// <returns>True - if it is highlighted, false - otherwise</returns>
        public bool VerifyHighlightSeriesClass(string classId, BlcItemOptions option) =>
            DriverExtensions.IsElementPresent(
                DriverExtensions.WaitForElement(
                    this.Container,
                    By.XPath(string.Format(this.Map[option].LocatorMask, classId))));

        /// <summary>
        /// Is Ticker Highlighted
        /// </summary>
        /// <param name="classId">Class Id</param>
        /// <param name="ticker">Ticker</param>
        /// <param name="option">Option</param>
        /// <returns>True - if it is highlighted, false - otherwise</returns>
        public bool VerifyHighlightTicker(string classId, string ticker, BlcItemOptions option) =>
            DriverExtensions.IsElementPresent(
                DriverExtensions.WaitForElement(
                    this.Container,
                    By.XPath(string.Format(this.Map[option].LocatorMask, classId, ticker))));

        /// <summary>
        /// Get Series Former Names
        /// </summary>
        /// <param name="sid">SID</param>
        /// <param name="checkHighlight">CheckHighlight</param>
        /// <returns>List of Series Former Names</returns>
        public List<string> GetSeriesFormerNames(string sid, bool checkHighlight)
        {
            List<string> formerNames = this.GetListOfSeriesFormerNames(sid);

            string highlightedSeriesFormerName = this.GetHighlightedText(sid);

            bool isHighlightDisplayed = !string.IsNullOrEmpty(highlightedSeriesFormerName);

            if (!checkHighlight)
            {
                if (isHighlightDisplayed)
                {
                    formerNames.Remove(highlightedSeriesFormerName);
                }
            }
            else
            {
                if (!isHighlightDisplayed)
                {
                    formerNames.Add(string.Empty);
                }
            }

            return formerNames;
        }

        /// <summary>
        /// Is Fund Series Former Name Displayed
        /// </summary>
        /// <param name="seriesFormerNameToCheck">Series Former Name To Check</param>
        /// <param name="sid">SID</param>
        /// <param name="checkHighlighted">CheckHighlighted</param>
        /// <returns>True - if it is displayed, false - otherwise</returns>
        public bool IsFundSeriesFormerNameDisplayed(string seriesFormerNameToCheck, string sid, bool checkHighlighted) =>
            checkHighlighted
                ? this.GetHighlightedText(sid).Equals(seriesFormerNameToCheck)
                : this.GetSeriesFormerNames(sid, checkHighlighted).Contains(seriesFormerNameToCheck);

        private string GetHighlightedText(string sid)
        {
            IWebElement highlightedSeriesFormerName = DriverExtensions.SafeGetElement(
                this.Container,
                By.XPath(string.Format(this.Map[BlcItemOptions.HighlightedSeriesFormerNames].LocatorMask, sid)));
            return highlightedSeriesFormerName != null
                       ? highlightedSeriesFormerName.GetText()
                       : string.Empty;
        }

        private List<string> GetListOfSeriesFormerNames(string sid)
        {
            IWebElement fundFormerName = DriverExtensions.SafeGetElement(
                this.Container,
                By.XPath(string.Format(this.Map[BlcItemOptions.SeriesFormerNames].LocatorMask, sid)));
            return fundFormerName.Displayed
                       ? new List<string>(
                           fundFormerName.GetText()
                                           .Substring(fundFormerName.GetText().IndexOf(':') + 1).Trim()
                                           .Split(',').Select(str => str.Trim()))
                       : new List<string>();
        }
    }
}