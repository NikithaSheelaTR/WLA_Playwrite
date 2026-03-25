namespace Framework.Common.UI.Products.Concourse.Dialogs
{
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;

    using OpenQA.Selenium;

    /// <summary>
    /// Edit matter
    /// </summary>
    public class EditMatterDialog : BaseModuleRegressionDialog
    {
        private const string ParticipantItemLctMask = "//ul[@class='participantRows']/li[.//h4[contains(text(),{0})]]";

        private static readonly By AddParticipantsTabLocator = By.XPath("//li[contains(@class,'matterParticipants')]");

        private static readonly By CloseButtonLocator = By.XPath("//button[contains(text(),'Close Matter')]");

        private static readonly By SaveButtonLocator = By.XPath("//button[contains(@class, 'save')]");

        private static readonly By YesButtonLocator = By.XPath("//button[contains(text(),'Yes')]");

        private static readonly By DeleteParticipantButtonLocator = By.XPath(".//a[@class='deleteParticipant']");

        private static readonly By OptionsLocator = By.XPath(".//span[@class='large_icon icon_options']");

        /// <summary>
        /// ClickOnAddParticipantsTab
        /// </summary>
        public void ClickOnAddParticipantsTab()
        {
            if (!DriverExtensions.WaitForElement(AddParticipantsTabLocator).GetAttribute("class").Contains("active"))
            {
                DriverExtensions.WaitForElement(AddParticipantsTabLocator).Click();
            }
        }

        /// <summary>
        /// ClickOnSaveButton
        /// </summary>
        public void ClickOnSaveButton() => this.ClickElement(SaveButtonLocator);

        /// <summary>
        /// CloseMatter
        /// </summary>
        public void CloseMatter()
        {
            this.ClickElement(CloseButtonLocator);
            this.ClickElement(YesButtonLocator);
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Remove participant from matter
        /// </summary>
        /// <param name="name">Participant name</param>
        public void RemoveParticipant(string name)
        {
            this.ClickOnAddParticipantsTab();
            var participant = DriverExtensions.WaitForElement(SafeXpath.BySafeXpath(ParticipantItemLctMask, name));
            DriverExtensions.WaitForElement(participant, OptionsLocator).Click();
            DriverExtensions.WaitForJavaScript();
            DriverExtensions.WaitForElement(participant, DeleteParticipantButtonLocator).Click();
            DriverExtensions.WaitForJavaScript();
        }
    }
}