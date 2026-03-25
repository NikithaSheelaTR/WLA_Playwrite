namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.FocusHighlighting
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Raw.WestlawEdge.Enums.FocusHighlighting;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Change colors dialog 
    /// </summary>
    public class ChangeColorsDialog : BaseFocusHighlightingDialog
    {
        private const string ColorsListLctMask =
            @"//div[@class = 'co_focusHighlightTermListChangeColor']//li[descendant::span[text() = '{0}']]//div[@class = 'co_focusHighlightColorList']//button";

        private static readonly By ReturnToSelectionsLinkLocator = By.XPath("//li[@class = 'co_focusHighlightReturnToTerms']//a");

        private static readonly By SelectedTermLocator = By.XPath("//div[@class =  'ColorSelectorContainer']//span");

        /// <summary>
        /// List of selected Terms
        /// </summary>
        protected override List<IWebElement> ListOfTerms => DriverExtensions.GetElements(SelectedTermLocator).ToList();

        /// <summary>
        /// Clicks 'Return to selections' link 
        /// </summary>
        /// <returns>
        /// Dialog <see cref="SelectionDialog"/>.
        /// </returns>
        public SelectionDialog ClickReturnToSelectionsLink()
        {
            this.ClickElement(ReturnToSelectionsLinkLocator);
            return new SelectionDialog();
        }

        /// <summary>
        /// Returns term color by term name
        /// </summary>
        /// <param name="termName">Term name</param>
        /// <returns>Term color</returns>
        public override TermColors GetTermColor(string termName) =>
            this.GetColorTypeByCode(this.ListOfTerms.First(x => x.Text.Equals(termName)).GetCssValue("background-color"));

        /// <summary>
        /// Is 'Return to selection' link displayed
        /// </summary>
        /// <returns>True - if link is displayed</returns>
        public bool IsReturnToSelectionsLinkDisplayed() => DriverExtensions.GetElement(ReturnToSelectionsLinkLocator).IsDisplayed();

        /// <summary>
        /// Sets term color
        /// </summary>
        /// <param name="termName">Term name</param>
        /// <param name="termColor">Term color</param>
        public void SetTermColor(string termName, TermColors termColor)
            =>
                DriverExtensions.GetElements(By.XPath(string.Format(ColorsListLctMask, termName)))
                                .First(
                                    x =>
                                        x.GetCssValue("background-color")
                                         .Equals(TermColorMap[termColor].BackgroundColorCode))
                                .CustomClick();
    }
}