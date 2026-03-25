namespace Framework.Common.UI.Products.WestLawNext.Pages.BusinessLawCenterPowerSearch
{
    using Framework.Common.UI.Products.WestLawNext.Utils.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The project list page.
    /// </summary>
    public class ProjectListPage : BaseProjectPage
    {
        private const string ProjectCheckboxLctMask =
            "//label/input[../span[contains(@class,'checkBoxLabel') and text()='{0}']]";

        private const string ProjectDescriptionLctMask =
            "//div[contains (@class, 'listItems') and ./ul/li/a[text()={0}]]/div[contains(@class,'snippet')]";

        private const string ProjectNameLctMask =
            "//li[@class='projectNameLink']/a[text()='{0}']";

        private static readonly By CreateNewProjectLinkLocator = By.LinkText("Create a New Project");

        private static readonly By MessageProjectsWereSuccessfullyDeletedLocator =
            By.XPath("//div[@class='co_infoBox_message' and contains (.,'Project(s) successfully deleted.')]");

        private static readonly By NumberOfProjectsReturnedLocator = By.XPath("//span[@class='searchResultNumber ng-binding']");

        private static readonly By ProjectDescriptionLocator = By.Id("projectDescription");

        private static readonly By ProjectHeadlineLocator =
            By.XPath("//h2[@class='companyResultsTableHeader' and text()='Projects']");

        private static readonly By ProjectNameLocator = By.Id("projectName");

        private static readonly By SaveProjectButtonLocator =
            By.XPath("//div[@class='co_overlayBox_optionsBottom']//input[@role='button' and @value='Save']");

        private static readonly By SpinnerLocator = By.XPath("//div[@class='co_loading']");

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectListPage"/> class. 
        /// Constructor to create the Projects List Page.
        /// </summary>
        public ProjectListPage()
        {
            DriverExtensions.WaitForElementDisplayed(ProjectHeadlineLocator);
        }

        /// <summary>
        /// CreateNewProject - Selects "Create a New Project" button.
        /// </summary>
        /// <param name="projectName">The project Name.</param>
        /// <param name="projectDescription">The project Description.</param>
        /// <param name="clickLink">The click Link.</param>
        public void CreateNewProject(string projectName, string projectDescription, bool clickLink = true)
        {
            if (clickLink)
            {
                ActionExtensions.DoUntilConditionWillBecomeTrue(
                    DriverExtensions.WaitForElement(CreateNewProjectLinkLocator).Click,
                    () => DriverExtensions.IsDisplayed(SaveProjectButtonLocator, 5));
            }

            IWebElement saveButton = DriverExtensions.WaitForElementDisplayed(SaveProjectButtonLocator);
            DriverExtensions.SetTextField(projectName, ProjectNameLocator);
            DriverExtensions.SetTextField(projectDescription, ProjectDescriptionLocator);
            saveButton.JavascriptClick();
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForJavaScript();

            this.WaitforSpinnerToGo();
        }

        /// <summary>
        /// Delete all existing project
        /// </summary>
        public void DeleteProjectsIfAny()
        {
            int numberOfProjects = this.GetTotalNumberOfProjects();
            if (numberOfProjects <= 0)
            {
                return;
            }

            this.SelectAllItems();
            this.DeleteSelectedProjects();
        }

        /// <summary>
        /// DeleteSelectedProjects - Select Delete button on Projects page.
        /// </summary>
        public void DeleteSelectedProjects() => this.DeleteSelectedItems(MessageProjectsWereSuccessfullyDeletedLocator);

        /// <summary>
        /// To check whether the particular project Exists
        /// </summary>
        /// <param name="projectName">project name</param>
        /// <returns>True if Project exists, otherwise - false</returns>
        public bool IsProjectDisplayed(string projectName) 
            => DriverExtensions.IsDisplayed(By.XPath(string.Format(ProjectNameLctMask, projectName)), 2);

        /// <summary>
        /// The get project description.
        /// </summary>
        /// <param name="projectName">The name project.</param>
        /// <returns>The project's description</returns>
        public string GetProjectDescription(string projectName)
            => DriverExtensions.WaitForElement(SafeXpath.BySafeXpath(ProjectDescriptionLctMask, projectName)).Text;

        /// <summary>
        /// Returns the total number of Projects listed on Project page
        /// </summary>
        /// <returns>number of Projects</returns>
        public int GetTotalNumberOfProjects()
        {
            DriverExtensions.WaitForElementNotDisplayed(SpinnerLocator);
            DriverExtensions.WaitForElementDisplayed(NumberOfProjectsReturnedLocator);
            return
                DriverExtensions.WaitForElement(NumberOfProjectsReturnedLocator)
                                .GetAttribute("textContent")
                                .RetrieveCountFromBrackets();
        }

        /// <summary>
        /// SelectProjectFromList - Select the input project from project list.
        /// </summary>
        /// <param name="projectName">The project Name.</param>
        /// <returns>The <see cref="ProjectPage"/>.</returns>
        public ProjectPage SelectProjectFromList(string projectName)
        {
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForElement(By.LinkText(projectName)).Click();
            DriverExtensions.WaitForJavaScript();
            DriverExtensions.WaitForPageLoad();

            return new ProjectPage();
        }

        /// <summary>
        /// The toggle project from list.
        /// </summary>
        /// <param name="projectName">The name project.</param>
        public void ToggleProjectFromList(string projectName)
        {
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForJavaScript();

            IWebElement projectCheckbox =
                DriverExtensions.WaitForElement(By.XPath(string.Format(ProjectCheckboxLctMask, projectName)));

            ActionExtensions.DoUntilConditionWillBecomeTrue(
                projectCheckbox.Click,
                () => DriverExtensions.IsCheckboxSelected(projectCheckbox));
        }
    }
}