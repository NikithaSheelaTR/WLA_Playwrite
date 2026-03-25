namespace Framework.Common.UI.Products.WestLawNext.Dialogs.PortalManagerSearch
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// POM for the dialog that is displayed when the user clicks on a custom page link in the custom page section on the global search form page.
    /// </summary>
    public class CustomPageSectionDialog : BaseModuleRegressionDialog
    {
        private static readonly By CloseButtonLocator = By.Id("showCustomPage");

        private static readonly By CustomPageContentSectionLocator =
            By.XPath("//div[@id='showCustomPageLightbox']//ul[@class='ui-sortable']/li");
        
        /// <summary>
        /// Click on the close button
        /// </summary>
        /// <typeparam name="T">Page Object</typeparam>
        /// <returns>The page instance.</returns>
        public T ClickClose<T>() where T : BaseModuleRegressionPage => this.ClickElement<T>(CloseButtonLocator);

        /// <summary>
        /// Return all the content items text as a list
        /// </summary>
        /// <returns>The List of values</returns>
        public List<string> GetContentSections()
        {
            DriverExtensions.WaitForElement(CustomPageContentSectionLocator);
            return DriverExtensions.GetElements(CustomPageContentSectionLocator).Select(item => item.Text).ToList();
        }
    }
}