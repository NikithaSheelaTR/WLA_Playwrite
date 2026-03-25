namespace Framework.Common.UI.Products.WestLawNext.Components.BrowsePage
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Pages.Browse;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;

    using OpenQA.Selenium;

    /// <summary>
    /// Component representing the select content/search all content widget/bar that appears at the top of
    /// several types of browse pages
    /// </summary>
    public class BrowsePageCheckboxComponent : BaseModuleRegressionComponent
    {
        private const string CheckBoxLctMask = "//input[@id='co_{0}']";

        private const string TableOfContentsCheckboxLctMask = "//span[@class='co_treeItemSelection' and ancestor::li[contains(.,{0})]]";

        private const string TocItemByTitleLctMask = "//a[@class=' co_tocItemLink ' ][contains(translate(.,'ABCDEFGHJIKLMNOPQRSTUVWXYZ','abcdefghjiklmnopqrstuvwxyz'),translate({0},'ABCDEFGHJIKLMNOPQRSTUVWXYZ','abcdefghjiklmnopqrstuvwxyz'))]";

        private static readonly By CheckBoxElementLocator = By.CssSelector("input.co_showState");

        private static readonly By BrowseTocLocator = By.XPath("//div[@id='coid_browseToc' or @id='co_categoryPageCheckboxContainer']");

        private static readonly By ClearSelectionLinkLocator = By.Id("co_itemsClearAnchor");

        private static readonly By CountOfSelectedItemsLocator = By.CssSelector("span#coid_selectedCount");

        private static readonly By FederalCheckboxLocator = By.XPath("//input[@type='checkbox' and @id='co_Federal']");

        private static readonly By SelectAllContentCheckboxLocator = By.Id("coid_browseSelectAllCheckboxInput");

        private static readonly By SpecifyContentToSearchLocator = By.XPath("//input[@type='checkbox' and @id='coid_browseShowCheckboxes'] | //span[contains(@id,'checkbox')]");

        private static readonly By StateCheckBoxLocator = By.XPath("//input[@type='checkbox' and @id='co_State-Included']");

        private static readonly By SuperBrowseIconImagesListLocator = By.CssSelector("img.co_websiteTableOfContentsDocumentLinkImage");

        private static readonly By SuperBrowseIconLinksListLocator = By.CssSelector(".co_link_wrapper>a.co_tocImageLink");

        private static readonly By SuperBrowseLinksListLocator = By.XPath("//div[@class='co_link_wrapper']/a[not(@class='co_tocImageLink')]");

        private static readonly By TaxLinksLocator = By.CssSelector(".co_2Column a");

        private static readonly By ContainerLocator = By.XPath("//*[@Id='coid_website_browseMainColumn'] | //*[@class='mat-checkbox-inner-container']");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Click on Clear Selection link
        /// </summary>
        public void ClearSelection() => DriverExtensions.Click(ClearSelectionLinkLocator);

        /// <summary>
        /// The click item by name.
        /// </summary>
        /// <returns>New page object </returns>
        public TPage ClickItemByName<TPage>(string itemName) where TPage : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(SafeXpath.BySafeXpath(TocItemByTitleLctMask, itemName)).Click();
            return DriverExtensions.CreatePageInstance<TPage>();
        }

        /// <summary>
        /// Get All Bold Links for Tax pages
        /// </summary>
        /// <returns>list of bold links </returns>
        public List<string> GetAllBoldLinks(string fontWeight = "700") => DriverExtensions
            .GetElements(TaxLinksLocator)
            .Where(el => (el.GetCssValue("font-weight").Equals(fontWeight) || el.GetCssValue("font-weight").Equals("bold")))
            .Select(el => el.Text).ToList();

        /// <summary>
        ///  Get All Not Bolded Links for Tax pages
        /// </summary>
        /// <returns>list of normal links</returns>
        public List<string> GetAllNotBoldedLinks() => DriverExtensions
            .GetElements(TaxLinksLocator)
            .Where(el => (el.GetCssValue("font-weight").Equals("400") || el.GetCssValue("font-weight").Equals("normal")))
            .Select(el => el.Text).ToList();

        /// <summary>
        /// Get count of selected checkboxes
        /// </summary>
        /// <returns></returns>
        public int GetCountOfSelectedCheckBoxes() => DriverExtensions
            .GetElements(CheckBoxElementLocator)
            .Count(element => DriverExtensions.IsCheckboxSelected(element));

        /// <summary>
        /// Get count of selected items from the inscription "... items selected"
        /// </summary>
        /// <returns></returns>
        public int GetCountOfSelectedItemsFromInscription()
        {
            string text = DriverExtensions.GetText(CountOfSelectedItemsLocator);
            string[] parts = text.Split(' ');
            int result;
            return int.TryParse(parts[0], out result) ? result : 0;
        }
        /// <summary>
        /// Get count of Super Browse icons (links)
        /// </summary>
        /// <returns>count of Super Browse icon links</returns>
        public int GetCountOfSuperBrowseIconLinks() => DriverExtensions.GetElements(SuperBrowseIconLinksListLocator).Count;

        /// <summary>
        /// Get count of Super Browse icons (images)
        /// </summary>
        /// <returns>count of Super Browse icons</returns>
        public int GetCountOfSuperBrowseIcons() => DriverExtensions.GetElements(SuperBrowseIconImagesListLocator).Count;

        /// <summary>
        /// Get count of Super Browse items
        /// </summary>
        /// <returns>count of items</returns>
        public int GetCountOfSuperBrowseLinks() => DriverExtensions.GetElements(SuperBrowseLinksListLocator).Count;

        /// <summary>
        /// Gets item text by title
        /// ToDo: Need to update name of behaviour 
        /// </summary>
        /// <param name="documentTitle"> Document Title </param>
        /// <returns> The <see cref="string"/>. </returns>
        public string GetTocItemByTitle(string documentTitle)
            => DriverExtensions.WaitForElement(SafeXpath.BySafeXpath(TocItemByTitleLctMask, documentTitle)).Text; // case insensitive xpath search

        /// <summary>
        /// Returns true if Clear selection link is present
        /// </summary>
        /// <returns></returns>
        public bool IsClearSelectionLinkDisplayed() => DriverExtensions.IsDisplayed(ClearSelectionLinkLocator);

        /// <summary>
        /// IsFederalCheckboxDisplayed
        /// </summary>
        /// <returns>True if Federal Checkbox is displayed, otherwise - false</returns>
        public bool IsFederalCheckboxDisplayed() => DriverExtensions.WaitForElement(FederalCheckboxLocator).Displayed;

        /// <summary>
        /// Returns true if Select All Content checkbox is present
        /// </summary>
        /// <returns>true if Select All Content checkbox is present</returns>
        public bool IsSelectAllContentCheckboxPresent() => DriverExtensions.WaitForElement(SelectAllContentCheckboxLocator).Displayed;

        /// <summary>
        /// Returns true if Specify Content to Search checkbox is present
        /// </summary>
        /// <returns>true if Specify Content to Search checkbox is present</returns>
        public bool IsSpecifyContentToSearchCheckboxPresent() => DriverExtensions.WaitForElement(SpecifyContentToSearchLocator).Displayed;

        /// <summary>
        /// StateCheckBoxLocator
        /// </summary>
        /// <returns>True if State Checkbox is displayed</returns>
        public bool IsStateCheckboxDisplayed() => DriverExtensions.WaitForElement(StateCheckBoxLocator).Displayed;

        /// <summary>
        /// Select a Browse page checkbox
        /// </summary>
        /// <typeparam name="T"> T </typeparam>
        /// <param name="label"> the checkbox label text </param>
        /// <returns> the current page </returns>
        public T SelectCheckbox<T>(string label) where T : IBrowseCategoryPage
        {
            DriverExtensions.WaitForElementDisplayed(this.ComponentLocator);
            DriverExtensions.WaitForElement(By.XPath(string.Format(CheckBoxLctMask, label))).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Select a checkbox
        /// </summary>
        /// <typeparam name="T"> t </typeparam>
        /// <param name="label"> the checkbox label text </param>
        /// <returns> the current page </returns>
        public T SelectRightColumnCheckbox<T>(string label) where T : IBrowseCategoryPage
        {
            DriverExtensions.WaitForElement(By.XPath(string.Format(CheckBoxLctMask, label))).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Select a Table of Content browse page checkbox
        /// </summary>
        /// <typeparam name="T"> t </typeparam>
        /// <param name="label"> the checkbox label text </param>
        /// <returns> the current page </returns>
        public T SelectTableOfContentsCheckbox<T>(string label) where T : IBrowseCategoryPage
        {
            DriverExtensions.WaitForElement(BrowseTocLocator);
            //DriverExtensions.Click(
            //    BrowseTocLocator,
            //    By.Id(DriverExtensions.GetAttribute("for", SafeXpath.BySafeXpath(TableOfContentsCheckboxLctMask, label))));

            DriverExtensions.Click(
                BrowseTocLocator, SafeXpath.BySafeXpath(TableOfContentsCheckboxLctMask, label));

            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Verify that checkboxIsDisplayed
        /// </summary>
        /// <param name="contentType"> Content Type </param>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsCheckboxDisplayed(string contentType)
        {
            return DriverExtensions.IsDisplayed(
               BrowseTocLocator, SafeXpath.BySafeXpath(TableOfContentsCheckboxLctMask, contentType));

            //IWebElement labelElement =
            //    DriverExtensions.SafeGetElement(SafeXpath.BySafeXpath(TableOfContentsCheckboxLctMask, contentType));
            //return labelElement != null && DriverExtensions.IsDisplayed(By.Id(labelElement.GetAttribute("for")));
        }

        /// <summary>
        /// Verify that label is displayed
        /// </summary>
        /// <param name="label"> Label </param>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsLabelPresent(string label)
            => DriverExtensions.IsElementPresent(SafeXpath.BySafeXpath(TableOfContentsCheckboxLctMask, label));

        /// <summary>
        /// Sets the Select All Content checkbox
        /// </summary>
        /// <param name="showCheckboxes">true if the checkbox should be checked, false if it should be unchecked</param>
        public void SetSelectAllContentCheckbox(bool showCheckboxes)
            => DriverExtensions.SetCheckbox(SelectAllContentCheckboxLocator, showCheckboxes);

        /// <summary>
        /// Verify that 'Select All' checkbox is selected
        /// </summary>
        /// <returns> True if selected, false otherwise </returns>
        public bool IsSelectAllCheckboxSelected() => DriverExtensions.IsCheckboxSelected(SelectAllContentCheckboxLocator);

        /// <summary>
        /// Used to select either the Search All Content or Specify Content to Search radio buttons
        /// </summary>
        /// <param name="showCheckboxes">true to select Specify Content to Search, false for Search All Content</param>
        public void SetShowCheckboxes(bool showCheckboxes)
            => DriverExtensions.SetCheckbox(SpecifyContentToSearchLocator, showCheckboxes);

        /// <summary>
        /// Set Specified TOK Areas
        /// </summary>
        /// <param name="selectedArea"> selected Area </param>
        /// <returns> <see cref="TableOfContentsBrowsePage"/>Table Of Contents Browse Page </returns>
        public TableOfContentsBrowsePage SetSpecifiedTocAreas(List<string> selectedArea)
        {
            this.SetShowCheckboxes(true);

            foreach (string area in selectedArea)
            {
                this.SelectTableOfContentsCheckbox<TableOfContentsBrowsePage>(area);
            }

            return new TableOfContentsBrowsePage();
        }
    }
}