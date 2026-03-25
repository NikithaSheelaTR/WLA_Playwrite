namespace Framework.Common.UI.Products.Concourse.Dialogs
{
    using Framework.Common.UI.Products.Concourse.Pages;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Enums.Foldering;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils;

    using OpenQA.Selenium;

    /// <summary>
    /// CreateMatterDialog
    /// </summary>
    public class CreateMatterDialog : BaseModuleRegressionDialog
    {
        private const string ParticipantLctMask = "//li[@class='ui-menu-item']//h3[contains(text(),'{0}')]";

        private static readonly By AddParticipantsTabLocator = By.XPath("//button[contains(@class, 'switchToParticipants')]");

        private static readonly By AddParticipantTexboxLocator = By.XPath("//input[@name='participant-query']");

        private static readonly By MatterAccessLocator = By.XPath("//select[@class='restrictedMatter']");

        private static readonly By MatterNameErrorMessageLocator = By.XPath("//div[@data-errormessageid='ShortName']");

        private static readonly By MatterNameLocator = By.Id("ShortName");

        private static readonly By ParticipantRoleDropdownLocator = By.XPath("//select[@class='participantRole']");

        private static readonly By SaveButtonLocator = By.XPath("//button[contains(text(),'Save')]");

        /// <summary>
        /// Adding permissions on the matter
        /// </summary>
        /// <param name="collaborator">Collaborator User</param>
        /// <param name="collaboratorRoles">Role of the collaborator</param>
        public void AddPermissions(string collaborator, SharingRoles collaboratorRoles)
        {
            // Adding permissions to created matter-Click on the Add Participants
            DriverExtensions.WaitForElement(AddParticipantsTabLocator).Click();

            // add the participants
            DriverExtensions.WaitForElement(AddParticipantTexboxLocator).SendKeys(collaborator);
            DriverExtensions.WaitForElement(By.XPath(string.Format(ParticipantLctMask, collaborator))).Click();

            if (collaboratorRoles == SharingRoles.Reviewer)
            {
                DriverExtensions.SelectElementInListByText(ParticipantRoleDropdownLocator, "Reviewer");
            }
        }

        /// <summary>
        /// Click on SaveButton
        /// </summary>
        /// <returns>Instance of the MatterDetailsPage or null if matter was not created</returns>
        public MatterDetailsPage ClickSaveButton()
        {
            var matterDetailsPage = this.ClickElement<MatterDetailsPage>(SaveButtonLocator);

            IWebElement errorMsg;
            var result = false;

            if (DriverExtensions.TryGetElement(MatterNameErrorMessageLocator, out errorMsg))
            {
                result = errorMsg.Text.Contains("already exists. Please enter a unique matter name");
            }

            return result ? null : matterDetailsPage;
        }

        /// <summary>
        /// EnterMatterName
        /// </summary>
        /// <param name="name">Matter name</param>
        public void EnterMatterName(string name)
            => DriverExtensions.WaitForElement(MatterNameLocator).SendKeys(name);

        /// <summary>
        /// Select MatterAccess
        /// </summary>
        /// <param name="matterAccessValue">Matter access value</param>
        public void SelectMatterAccess(string matterAccessValue)
        {
            if (DriverExtensions.IsElementPresent(MatterAccessLocator))
            {
                DriverExtensions.SelectElementInListByText(MatterAccessLocator, matterAccessValue);
            }
            else
            {
                Logger.LogDebug("Matter access dropdown is not present");
            }
        }
    }
}