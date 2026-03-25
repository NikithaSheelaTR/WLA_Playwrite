namespace Framework.Common.UI.Products.Shared.Dialogs.Facets
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Items.Facets;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Describe dialog for Headnote Topics Facet
    /// </summary>
    public class HeadnoteTopicsDialog : BaseModuleRegressionDialog
    {
        private static readonly By CancelLinklocator = By.XPath(".//a[@class='co_overlayBox_buttonCancel']");

        private static readonly By FilterButtonLocator = By.XPath(
            ".//input[@value='Filter Results' and @type='button']");

        private static readonly By HeadnotesLocator =
            By.XPath(".//ol[@class='co_relatedInfo_ExpandedHeadnoteFacetList']/li");

        private static readonly By HeadnoteTopicsDialogLocator =
            By.XPath("//div[@class='co_overlayBox_container' and contains(@id,'expand_Group_')]");

        private static readonly By LbaleLocator = By.XPath(".//div[@class='co_overlayBox_headline']");

        private static readonly By SelectAllCheckboxLocator =
            By.XPath(".//div[@class='co_overlayBox_content']/div/span/a[@role='checkbox']");

        private static readonly By SelectButtonLocator = By.XPath(".//input[@value='Select']");

        private readonly IWebElement dialogContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="HeadnoteTopicsDialog"/> class. 
        /// </summary>
        public HeadnoteTopicsDialog()
        {
            this.dialogContainer = DriverExtensions.WaitForElement(HeadnoteTopicsDialogLocator);
        }

        /// <summary>
        /// Head Notes
        /// </summary>
        public List<HeadnoteTopicsDialogItem> HeadNotes
        {
            get
            {
                ReadOnlyCollection<IWebElement> headNotes = this.dialogContainer.FindElements(HeadnotesLocator);
                return headNotes.Select(hn => new HeadnoteTopicsDialogItem(hn)).ToList();
            }
        }

        /// <summary>
        /// Label Text
        /// </summary>
        public string LabelText => DriverExtensions.GetElement(this.dialogContainer, LbaleLocator).Text;

        /// <summary>
        /// Select Button
        /// </summary>
        public IWebElement SelectButton
        {
            get
            {
                return DriverExtensions.IsDisplayed(SelectButtonLocator, 5)
                           ? DriverExtensions.GetElement(this.dialogContainer, SelectButtonLocator)
                           : null;
            }
        }

        /// <summary>
        /// Click on the 'Cancel' link
        /// </summary>
        public void ClickOnTheCancelLink()
            => DriverExtensions.GetElement(this.dialogContainer, CancelLinklocator).Click();

        /// <summary>
        /// Click on the Filter button
        /// </summary>
        public void ClickOnTheFilterButton()
            => DriverExtensions.WaitForElement(this.dialogContainer, FilterButtonLocator).Click();

        /// <summary>
        /// Click on the 'Select All' checkbox
        /// </summary>
        public void ClickOnTheSelectAllCheckbox()
            => DriverExtensions.SetCheckbox(true, this.dialogContainer, SelectAllCheckboxLocator);

        /// <summary>
        /// Get text from 'Select All' checkbox
        /// </summary>
        /// <returns> Text from 'Select All' checkbox </returns>
        public string GetSelectAllCheckboxText()
            => DriverExtensions.GetElement(this.dialogContainer, SelectAllCheckboxLocator).Text;

        /// <summary>
        /// Verify that 'Cancel' button is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsCancelLinkDisplayed() => DriverExtensions.IsDisplayed(this.dialogContainer, CancelLinklocator);
    }
}