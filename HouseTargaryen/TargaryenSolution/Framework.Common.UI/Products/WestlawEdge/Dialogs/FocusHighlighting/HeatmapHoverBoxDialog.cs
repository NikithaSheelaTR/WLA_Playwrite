namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.FocusHighlighting
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Enums;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Heatmap section component
    /// </summary>
    public class HeatmapHoverBoxDialog : BaseModuleRegressionDialog
    {
        private static readonly By HoverBoxHeadingLocator = By.XPath("//div[@class = 'HoverBoxHeading']");
        private static readonly By TermTextLocator = By.XPath(".//span[not(contains(@class, 'Box')) and @class]");
        private static readonly By TermColorLocator = By.XPath(".//span[contains(@class, 'Box')]");
        private static readonly By NumberOfTermsLocator = By.XPath(".//span[not(@class)]");

        private IWebElement Container => DriverExtensions.WaitForElement(By.XPath(
            EnumPropertyModelCache.GetMap<Dialogs, WebElementInfo>()[Dialogs.HeatmapHoverBox].LocatorString));

        /// <summary>
        /// Title
        /// </summary>
        public string Title => DriverExtensions.GetText(HoverBoxHeadingLocator);

        /// <summary>
        /// Are terms displayed
        /// </summary>
        /// <returns>True all terms are displayed, false otherwise.</returns>
        public bool AreColorsDisplayed()
        {
            var elements = DriverExtensions.GetElements(this.Container, TermColorLocator).ToList();
            return elements.Count() > 0 ? elements.TrueForAll(term => term.Displayed) : false;
        }

        /// <summary>
        /// Are number of terms displayed
        /// </summary>
        /// <returns>True if all terms numbers are displayed, false otherwise.</returns>
        public bool AreNumberOfTermsDisplayed()
        {
            var elements = DriverExtensions.GetElements(this.Container, NumberOfTermsLocator).ToList();
            return elements.Count() > 0 ? elements.TrueForAll(term => term.Displayed) : false;
        }
        /// <summary>
        /// Get terms numbers
        /// </summary>
        public List<string> GetTermsNumbers() => DriverExtensions.GetElements(this.Container, NumberOfTermsLocator)
            .Select(termNumber => termNumber.Text).ToList();

        /// <summary>
        /// Get terms titles
        /// </summary>
        public List<string> GetTermsTitles() => DriverExtensions.GetElements(this.Container, TermTextLocator).Select(x => x.Text).ToList();

        /// <summary>
        /// Is term bold
        /// </summary>
        /// <param name="termText">Term text.</param>
        /// <returns>True if term is bold, false otherwise.</returns>
        public bool IsTermBold(string termText)
        {
            IWebElement element = DriverExtensions.GetElements(this.Container, TermTextLocator).First(term => term.Text.Equals(termText));

            return element != null ? element.GetAttribute("class").Contains("bold") : false;
        }
    }
}