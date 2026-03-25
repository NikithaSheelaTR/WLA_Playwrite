namespace Framework.Common.UI.Products.Concourse.Pages.Base
{
    using Framework.Common.UI.Products.Concourse.Dialogs;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;
    using Framework.Core.Utils;

    using OpenQA.Selenium;

    /// <summary>
    /// The base matter page.
    /// </summary>
    public class BaseMatterPage : BaseConcoursePage
    {
        private const string MatterLctMask = "//a[@class='matterLink' and text()={0}]";

        private static readonly By NewMatterButtonLocator = By.XPath("//button[contains(text(),'New Matter')]");

        /// <summary>
        /// The click on new matter button.
        /// </summary>
        /// <returns>The instance of <see cref="CreateMatterDialog"/>.</returns>
        public CreateMatterDialog ClickOnNewMatterButton()
        {
            DriverExtensions.WaitForElement(NewMatterButtonLocator).Click();
            return new CreateMatterDialog();
        }

        /// <summary>
        /// OpenMatter
        /// </summary>
        /// <param name="matterName">Matter Name</param>
        /// <returns>The new instance of MatterDetailsPage</returns>
        public MatterDetailsPage ClickMatterByName(string matterName)
        {
            Logger.LogDebug("Open matter: " + matterName);
            DriverExtensions.WaitForElement(SafeXpath.BySafeXpath(MatterLctMask, matterName)).Click();
            return new MatterDetailsPage();
        }
    }
}