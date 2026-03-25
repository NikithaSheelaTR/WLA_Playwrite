namespace Framework.Common.UI.Products.WestLawNext.Pages.BusinessLawCenterPowerSearch
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Framework.Common.UI.Products.WestLawNext.Dialogs.BusinessLawCenterPowerSearch;
    using Framework.Common.UI.Products.WestLawNext.Utils.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The project page.
    /// </summary>
    public class ProjectPage : BaseProjectPage
    {
        private const string FilingCheckboxStringMask = "//div[@class='listItems ng-scope'][{0}]/ul/li/label/input";

        private static readonly By DeliveryDropDownLocator = By.XPath("//span[@title='Select Delivery Method']");

        private static readonly By DeliveryDropDownDownLoadLocator = By.XPath("//a[@title='Download items']");

        private static readonly By DownloadDialogHeaderLocator =
            By.XPath("//*[contains(@class, 'co_overlayBox_headline')]//*[text()='Download Documents']");

        private static readonly By EditLocator = By.XPath("//ul[@class='projectControls']/li/a[text()='Edit']");

        private static readonly By EditProjectDescriptionLocator = By.XPath("//div[@class='projectDescription']/textarea");

        private static readonly By EditProjectNameLocator = By.XPath("//div[@class='projectName']/input");

        private static readonly By FillingNamesLocator =
            By.XPath("//div[@class='sixCol searchResults']//div[@class='listItems ng-scope']/ul/li/a");

        private static readonly By ItemLocator = By.XPath("//div[@class='listItems ng-scope']");

        private static readonly By ItemCountLocator = By.XPath("//ul[@class='itemCount']/li/strong");

        private static readonly By MessageItemWereSuccessfullyDeletedLocator =
            By.XPath("//div[@class='co_foldering_popupMessageContainer co_infoBox bottom success']//div[@class='co_infoBox_message' and contains (.,'Item(s) successfully deleted.')]");

        private static readonly By ProjectDescriptionResultsHeaderLocator = By.XPath(
            "//div[@class='projectDescription']//span");

        private static readonly By ProjectNameResultsHeaderLocator = By.XPath("//div[@id='rightPane']/div/h2");

        private static readonly Random Randomizer = new Random();

        private static readonly By SaveEditLocator = By.XPath("//ul[@class='projectControls']/li/a[text()='Save']");

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectPage"/> class. 
        /// Constructor to create the Projects Page.
        /// </summary>
        public ProjectPage()
        {
            DriverExtensions.WaitForElementDisplayed(EditLocator);
        }


        /// <summary>
        /// The are filing items listed in specific order.
        /// </summary>
        /// <param name="expectedFilingItems">The are filing items in expected order.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool AreFilingItemsListedInSpecificOrder(IEnumerable<string> expectedFilingItems)
        {
            IEnumerable<string> actualFilingItems =
                DriverExtensions.GetElements(FillingNamesLocator).Select(el => el.Text).ToList();
            return actualFilingItems.SequenceEqual(expectedFilingItems);
        }

        /// <summary>
        /// The clear project description.
        /// </summary>
        public void ClearProjectDescription()
        {
            ActionExtensions.DoUntilConditionWillBecomeTrue(
                DriverExtensions.WaitForElement(EditLocator).Click,
                () => DriverExtensions.IsDisplayed(EditProjectDescriptionLocator), 5);

            DriverExtensions.WaitForElementDisplayed(EditProjectDescriptionLocator).Clear();
        }

        /// <summary>
        /// The click on delivery dropdown download.
        /// </summary>
        /// <returns>The <see cref="BlcPowerSearchDownloadDeliveryDialog"/>.</returns>
        public BlcPowerSearchDownloadDeliveryDialog ClickOnDeliveryDropDownDownLoad()
        {
            ActionExtensions.DoUntilConditionWillBecomeTrue(
                () => DriverExtensions.JavascriptClick(DeliveryDropDownLocator),
                () => DriverExtensions.IsDisplayed(DeliveryDropDownDownLoadLocator), 5);

            ActionExtensions.DoUntilConditionWillBecomeTrue(
                () => DriverExtensions.JavascriptClick(DeliveryDropDownDownLoadLocator),
                () => DriverExtensions.IsDisplayed(DownloadDialogHeaderLocator), 5);

            return new BlcPowerSearchDownloadDeliveryDialog();
        }

        /// <summary>
        /// The click on random items.
        /// </summary>
        /// <param name="number">The number.</param>
        public void ClickOnRandomItems(int number)
        {
            int count = this.GetNumberOfItems();
            if (number < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(number),
                    $"Invalid argument '{nameof(number)}'. Actual value: {number}, should be >=0");
            }

            if (number > count)
            {
                for (int i = 1; i <= count; i++)
                {
                    DriverExtensions.WaitForElement(By.XPath(string.Format(FilingCheckboxStringMask, i))).Click();
                }
            }
            else
            {
                var uniqueIntCollection = Randomizer.GetCollectionOfUniqueNumbers(1, count, number);
                foreach (int num in uniqueIntCollection)
                {
                    DriverExtensions.WaitForElement(By.XPath(string.Format(FilingCheckboxStringMask, num))).Click();
                }
            }
        }

        /// <summary>
        /// Clicks the Remove button
        /// </summary>
        public void DeleteSelectedItemsFromProject()
        {
            this.DeleteSelectedItems(MessageItemWereSuccessfullyDeletedLocator);
        }

        /// <summary>
        /// Gets the number of the items in the project
        /// </summary>
        /// <returns>number of items in the project</returns>
        public int GetNumberOfItems()
        {
            this.WaitforSpinnerToGo();
            DriverExtensions.WaitForElementDisplayed(ToggleAllLocator);
            DriverExtensions.WaitForPageLoad();
            string text = DriverExtensions.WaitForElement(ItemCountLocator).Text;
            string[] digits = Regex.Split(text, @"\D+");
            int itemsCount = int.Parse(digits[0]);
            int items = DriverExtensions.GetElements(ItemLocator).Count;
            if (items == itemsCount)
            {
                return itemsCount;
            }

            throw new Exception("Check for error: item counter does not match the number of items");
        }

        /// <summary>
        /// GetProjectDescriptionResultsHeader - Get the selected project name for edit from Projects Page.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetProjectDescriptionResultsHeader()
            => DriverExtensions.WaitForElement(ProjectDescriptionResultsHeaderLocator).GetAttribute("textContent");

        /// <summary>
        /// GetProjectNameResultsHeader - Get the selected project name for edit from Projects Page.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetProjectNameResultsHeader()
            => DriverExtensions.WaitForElement(ProjectNameResultsHeaderLocator).GetAttribute("textContent");

        /// <summary>
        /// SaveProjectEdit - Select save button on edit light box.
        /// </summary>
        public void SaveProjectEdit() => DriverExtensions.WaitForElement(SaveEditLocator).Click();

        /// <summary>
        /// Update of the Project. 
        /// </summary>
        /// <param name="newProjectDescription">The new project description.</param>
        public void TypeInEditProjectDescription(string newProjectDescription)
        {
            ActionExtensions.DoUntilConditionWillBecomeTrue(
                () => DriverExtensions.JavascriptClick(EditLocator),
                () => DriverExtensions.IsDisplayed(EditProjectDescriptionLocator, 3));

            DriverExtensions.SetTextField(newProjectDescription, EditProjectDescriptionLocator);
        }

        /// <summary>
        /// Update of the Project Name
        /// </summary>
        /// <param name="newProjectName">The new project name.</param>
        public void TypeInEditProjectName(string newProjectName)
        {
            ActionExtensions.DoUntilConditionWillBecomeTrue(
                DriverExtensions.WaitForElement(EditLocator).Click,
                () => DriverExtensions.IsDisplayed(EditProjectNameLocator), 5);

            DriverExtensions.SetTextField(newProjectName, EditProjectNameLocator);
        }
    }
}