namespace Framework.Common.UI.Products.WestLawNext.Components.BusinessLawCenterPowerSearch.ContentComponents
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.WestLawNext.Utils.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;

    using OpenQA.Selenium;

    /// <summary>
    /// The base delivery and save to project section.
    /// </summary>
    public class DeliveryAndSaveToProjectComponent : BaseModuleRegressionComponent
    {
        private const string ConfirmationMessageForAddingFillingToProjectLctMask =
            "//div[@class='co_foldering_popupMessageContainer co_infoBox bottom success']//div[@class='co_infoBox_message']/strong[contains(.,{0})]";

        private const string SpecificProjectLctMask = "//div[@class='co_dropDownMenuContent']//a[.={0}]";

        private static readonly By AddFilingsButtonLocator =
            By.XPath("//div[@class='co_dropDownButton']//a[contains(@title, 'Add to Project')]");

        private static readonly By CreateANewProjectLocator =
            By.XPath("//div[@class='co_dropDownMenuContent']//a[@id='createProjectLink']");

        private static readonly By IncludeEntireFilingLocator = By.Id("includeEntireFiling");

        private static readonly By ProjectMessageLocator =
            By.XPath("//li[@class='addToProject ng-isolate-scope']//div[@class='co_infoBox_message']/strong");

        private static readonly By ContainerLocator = By.ClassName("co_navSelect");

        private static readonly By DeliveryDropDownLocator = By.XPath("//li[contains(@class, 'deliveryDropdown')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="DeliveryAndSaveToProjectComponent"/> class.
        /// </summary>
        public DeliveryAndSaveToProjectComponent()
        {
            DriverExtensions.WaitForElementDisplayed(AddFilingsButtonLocator);
        }

        /// <summary>
        /// Delivery dropdown
        /// </summary>
        public DeliveryDropdown DeliveryDropdown => new DeliveryDropdown(DeliveryDropDownLocator, "BLC");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Clicks "Add filing(s) to Project" dropdown button
        /// </summary>
        public void ClickAddFilingsToProjectDropdown()
            => DriverExtensions.WaitForElement(AddFilingsButtonLocator).Click();

        /// <summary>
        /// Clicks "Create a new project" option in the "Add filing(s) to Project" dropdown
        /// </summary>
        public void ClickCreateANewProjectLink()
            => DriverExtensions.WaitForElement(CreateANewProjectLocator).Click();

        /// <summary>
        /// Clicks on the add entire filing check box.
        /// </summary>
        public void ClickOnAddEntireFilingCheckBox()
        {
            IWebElement entireFilingCheckBox = DriverExtensions.WaitForElement(IncludeEntireFilingLocator);
            ActionExtensions.DoUntilConditionWillBecomeTrue(
                () =>
                    {
                        entireFilingCheckBox.Hover();
                        entireFilingCheckBox.Click();
                    },
                () => DriverExtensions.IsCheckboxSelected(entireFilingCheckBox));
        }

        /// <summary>
        /// The click on specific a project link.
        /// </summary>
        /// <param name="projectName">The project name.</param>
        public void ClickOnSpecificAProjectLink(string projectName)
        {
            DriverExtensions.WaitForElementDisplayed(SafeXpath.BySafeXpath(SpecificProjectLctMask, projectName)).Click();
            DriverExtensions.WaitForElementDisplayed(60000, ProjectMessageLocator);
        }

        /// <summary>
        /// The wait for project dropdown message.
        /// </summary>
        /// <param name="text">The text.</param>
        public void WaitForProjectDropdownMessage(string text)
            => DriverExtensions.WaitForElement(SafeXpath.BySafeXpath(ConfirmationMessageForAddingFillingToProjectLctMask, text));
    }
}