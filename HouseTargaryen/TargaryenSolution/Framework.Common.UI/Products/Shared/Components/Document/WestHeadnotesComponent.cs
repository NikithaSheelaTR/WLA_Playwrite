namespace Framework.Common.UI.Products.Shared.Components.Document
{
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Products.WestlawEdge.Enums;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;

    /// <summary>
    /// West Headnotes Component for Headnotes Documents
    /// </summary>
    public class WestHeadnotesComponent : BaseModuleRegressionComponent
    {
        private const string HeadnoteLctMask = "co_anchor_headNote_{0}";

        private const string CitingReferenceKeyLctMask =
            "//div[@id='co_expandedHeadnotes']//div[contains(@id, 'co_anchor')][{0}]//div[contains(@class, 'co_headnoteTopics')]";

        private const string CitingReferenceTextLctMask =
            "//div[@id='co_expandedHeadnotes']//div[contains(@id, 'co_anchor')][{0}]//div[contains(@class, 'co_headnoteCitedCaseRef')]//a";

        private static readonly By ContainerLocator = By.Id("co_headnotes");

        private static readonly By ChangeViewModeLocator = By.Id("co_headnotesShowOrHideFancy");

        private static readonly By HeadnoteKeyIconLocator = By.XPath("//img[contains(@alt, 'Display Key')]");

        private static readonly By HeadnoteKeyTextLocator = By.ClassName("co_lastKeyText");

        private static readonly By HeadnoteNumberLocator = By.ClassName("co_headnoteNumber");

        private static readonly By HeadnoteTextLocator = By.ClassName("co_paragraphText");

        private static readonly By HeadnoteTitleLocator = By.ClassName("co_headnoteNode");

        private static readonly List<string> ProhibitedPhraseList = new List<string>
                                                                        {
                                                                            "in general",
                                                                            "other particular matters",
                                                                            "other particular cases and contexts",
                                                                            "particular matters"
                                                                        };

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the HeadnotesViewModes enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<HeadnotesViewModes, WebElementInfo> HeadnotesViewMap
            => EnumPropertyModelCache.GetMap<HeadnotesViewModes, WebElementInfo>(
                                                   string.Empty,
                                                   @"Resources/EnumPropertyMaps/WestlawEdge");

        /// <summary>
        /// Change View Mode Link
        /// </summary>
        public ILink ChangeViewModeLink => new Link(ChangeViewModeLocator);

        /// <summary>
        /// Verifies if List View mode selected for wln
        /// </summary>
        /// <returns> Boolean value </returns>
        public bool IsListViewModeSelected() =>
            DriverExtensions.GetAttribute("class", ComponentLocator).Contains("co_fancyHeadnotes");

        /// <summary>
        /// Is Headnotes View Mode selected
        /// </summary>
        /// /// <param name="mode">HeadnotesViewModes</param>
        /// <returns>true if Headnotes mode is selected </returns>
        public bool IsViewModeSelected(HeadnotesViewModes mode) => DriverExtensions.GetElement(By.XPath(
                this.HeadnotesViewMap[mode].LocatorString)).GetAttribute("class").Contains("co_selected");

        /// <summary>
        /// Change view
        /// </summary>
        /// <param name="mode">HeadnotesViewModes</param>
        public void ChangeView(HeadnotesViewModes mode) => DriverExtensions.GetElement(By.XPath(
                this.HeadnotesViewMap[mode].LocatorString)).Click();

        /// <summary>
        /// Click on a specific Key Note by number
        /// </summary>
        /// <param name="numberOfKeyIcon">Number of Key Icon</param>
        public void ClickKeyNoteByIndex(int numberOfKeyIcon)
        {
            if (false == this.IsKeyNoteDisplayed(numberOfKeyIcon))
            {
                this.GetKeyNoteByIndex(numberOfKeyIcon).ScrollToElement();
            }
            this.GetKeyNoteByIndex(numberOfKeyIcon).Click();
        }

        /// <summary>
        /// Get text of citing reference by index
        /// </summary>
        /// <param name="index">
        /// Number of citing reference
        /// </param>
        /// <returns>
        /// Text of citing reference
        /// </returns>
        public string GetTextOfCitingReferenceByIndex(int index) => DriverExtensions
            .WaitForElement(By.XPath(string.Format(CitingReferenceTextLctMask, index))).Text;

        /// <summary>
        /// Headnote key text list.
        /// </summary>
        /// <returns>The List of values.</returns>
        public List<string> GetHeadnoteKeyTextList() => this.GetElementListByLocator(HeadnoteKeyTextLocator);

        /// <summary>
        /// Headnote number list.
        /// </summary>
        /// <returns>The List of values.</returns>
        public List<string> GetHeadnoteNumberList() => this.GetElementListByLocator(HeadnoteNumberLocator);

        /// <summary>
        /// Headnote text list.
        /// </summary>
        /// <returns>The List of values.</returns>
        public List<string> GetHeadnoteTextList() => this.GetElementListByLocator(HeadnoteTextLocator);

        /// <summary>
        /// Headnote title list.
        /// </summary>
        /// <returns>The List of values.</returns>
        public List<string> GetHeadnoteTitleList() => this.GetElementListByLocator(HeadnoteTitleLocator);

        /// <summary>
        /// Verify class name information for the pop up corresponding to the given number of Key Icon
        /// </summary>
        /// <param name="numberOfKeyIcon">
        /// Number of Key Icon (countdown starts from 1)
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public bool IsKeyNoteDialogDisplayed(int numberOfKeyIcon) =>
            DriverExtensions.WaitForElement(By.XPath(string.Format(CitingReferenceKeyLctMask, numberOfKeyIcon)))
                            .GetAttribute("class").Contains("co_showUp co_fancyHeadnotesActive");

        /// <summary>
        /// Is Key Note displayed
        /// </summary>
        /// <param name="numberOfKeyIcon">Number of Key Icon</param>
        /// <returns>True - if it is displayed, false - otherwise</returns>
        public bool IsKeyNoteDisplayed(int numberOfKeyIcon) =>
            this.GetKeyNoteByIndex(numberOfKeyIcon).IsDisplayed();

        /// <summary>
        /// Checks if any of the Prohibited Phrases are present in the current document
        /// </summary>
        /// <returns>Boolean Value</returns>
        public bool IsHeadnoteKeyTextContainsProhibitedPhrase()
            => DriverExtensions.GetElements(HeadnoteKeyTextLocator).Select(elem => elem.Text).ToList()
                .Intersect(ProhibitedPhraseList).Any();

        /// <summary>
        /// Checks if specified headnote is scrolled into view
        /// </summary>
        /// <param name="index"> Index start with 1 </param>
        /// <returns>True if headnote is scrolled into view, false otherwise</returns>
        public bool IsHeadnoteScrolledIntoView(int index)
            => DriverExtensions.WaitForElement(By.Id(string.Format(HeadnoteLctMask, index))).IsElementInView();

        private List<string> GetElementListByLocator(By locator)
            => DriverExtensions.GetElements(DriverExtensions.WaitForElement(this.ComponentLocator), locator)
                            .Select(li => li.Text).ToList();

        /// <summary>
        /// Get key note by number of Key Icon 
        /// </summary>
        /// <param name="numberOfKeyIcon">
        /// Number of Key Icon (countdown starts from 1)
        /// </param>
        /// <returns>
        /// The <see cref="IWebElement"/>.
        /// </returns>
        private IWebElement GetKeyNoteByIndex(int numberOfKeyIcon) => DriverExtensions.GetElements(HeadnoteKeyIconLocator).ElementAt(numberOfKeyIcon - 1);
    }
}