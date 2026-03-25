namespace Framework.Common.UI.Products.Shared.Dialogs.CustomPages.ManagePage
{
    using Framework.Common.UI.Products.Shared.Pages.CustomPages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Create New Content Section LightBox
    /// </summary>
    public class CreateNewContentSectionDialog : BaseManagePageDialog
    {
        private static readonly By NewSectionTextboxLocator = By.XPath("//input[@id='cp_addCategoryPageSection_input']");

        /// <summary>
        /// Add section
        /// </summary>
        /// <param name="sectionName"> string </param>
        /// <returns> New instance <see cref="CustomPage"/>. </returns>
        public CustomPage AddSection(string sectionName)
        {
            DriverExtensions.SetTextField(sectionName, NewSectionTextboxLocator);
            return this.ClickSaveButton<CustomPage>();
        }
    }
}