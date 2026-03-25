namespace Framework.Common.UI.Products.Concourse.Pages
{
    using Framework.Common.UI.Products.Concourse.Dialogs;
    using Framework.Common.UI.Products.Concourse.Pages.Base;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// MatterDetailsPage
    /// </summary>
    public class MatterDetailsPage : BaseConcoursePage
    {
        private static readonly By EditMatterButtonLocator = By.Id("editMatter");
        
        /// <summary>
        /// ClickOnEditMatterButton
        /// </summary>
        /// <returns>EditMatterDialog object</returns>
        public EditMatterDialog ClickOnEditMatterButton()
        {
            DriverExtensions.Click(DriverExtensions.WaitForElement(EditMatterButtonLocator));
            return new EditMatterDialog();
        }
    }
}