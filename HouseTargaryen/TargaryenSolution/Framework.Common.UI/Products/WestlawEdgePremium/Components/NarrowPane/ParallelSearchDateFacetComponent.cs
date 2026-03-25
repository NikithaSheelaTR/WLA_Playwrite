namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.NarrowPane
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.NarrowPanel;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using OpenQA.Selenium;

    /// <summary>
    /// Parallel Search Date Facet
    /// </summary>
    public class ParallelSearchDateFacetComponent : NewEdgeRecentFiltersFacetComponent
    {
        private static readonly By DateComponentLocator = By.XPath("//saf-facet-category[@id='date']");
        private static readonly By DateRangeLocator = By.XPath("//saf-radio[@current-value='dateRange']");
        private static readonly By AllLocator = By.XPath("//saf-radio[@current-value='all']");
        private const string DateRangeRadioScript = "return(arguments[0].shadowRoot.querySelector('input[id=control]'));";
        private static readonly By StartDatePickerLocator = By.Id("date-picker-start");
        private static readonly By EndDatePickerLocator = By.Id("date-picker-end");
        private const string MaskedInputScript = "return(arguments[0].shadowRoot.querySelector('saf-date-masked-input'));";
        private const string TextFieldScript = "return(arguments[0].shadowRoot.querySelector('saf-text-field'));";
        private const string StartDateInputScript = "return(arguments[0].shadowRoot.querySelector('input[id=date-picker-start]'));";
        private const string EndDateInputScript = "return(arguments[0].shadowRoot.querySelector('input[id=date-picker-end]'));";
        private static readonly By DoneButtonLocator = By.Id("datePickerApplyButton");

        /// <summary>
        /// Done button
        /// </summary>     
        public IButton DoneButton => new Button(this.ComponentLocator, DoneButtonLocator);

        /// <summary>
        /// Date All radio button
        /// </summary>     
        public IRadiobutton DateAllRadio => new Radiobutton(this.ComponentLocator, AllLocator);

        ///<Summary>
        ///Component locator
        ///</Summary>
        protected override By ComponentLocator => DateComponentLocator;

        /// <summary>
        /// Date Range radio button
        /// </summary>     
        public void ClickDateRangeRadioButton()
        {
            IWebElement DateRangeElement = DriverExtensions.GetElement(this.ComponentLocator, DateRangeLocator); 
            IWebElement DateRange = (IWebElement)DriverExtensions.ExecuteScript(DateRangeRadioScript, DateRangeElement);
            DateRange.Click();
        }

        /// <summary>
        /// Enter start date
        /// </summary>
        public void EnterStartDate(string startDate)
        {
            IWebElement DatePickerElement = DriverExtensions.GetElement(this.ComponentLocator, StartDatePickerLocator);
            IWebElement InputElement = (IWebElement)DriverExtensions.ExecuteScript(MaskedInputScript, DatePickerElement);
            IWebElement TextFieldElement = (IWebElement)DriverExtensions.ExecuteScript(TextFieldScript, InputElement);
            IWebElement StartDate = (IWebElement)DriverExtensions.ExecuteScript(StartDateInputScript, TextFieldElement);
            StartDate.SendKeys(Keys.ArrowLeft);
            StartDate.SendKeys(Keys.ArrowLeft);
            StartDate.SendKeys(startDate);
        }

        /// <summary>
        /// Enter end date
        /// </summary>
        public void EnterEndDate(string endDate)
        {
            IWebElement DatePickerElement = DriverExtensions.GetElement(this.ComponentLocator, EndDatePickerLocator);
            IWebElement InputElement = (IWebElement)DriverExtensions.ExecuteScript(MaskedInputScript, DatePickerElement);
            IWebElement TextFieldElement = (IWebElement)DriverExtensions.ExecuteScript(TextFieldScript, InputElement);
            IWebElement StartDate = (IWebElement)DriverExtensions.ExecuteScript(EndDateInputScript, TextFieldElement);
            StartDate.SendKeys(Keys.ArrowLeft);
            StartDate.SendKeys(Keys.ArrowLeft);
            StartDate.SendKeys(endDate);
        }
    }
}
