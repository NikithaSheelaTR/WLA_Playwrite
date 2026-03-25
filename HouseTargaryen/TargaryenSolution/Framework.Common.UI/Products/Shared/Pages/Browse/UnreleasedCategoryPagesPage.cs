namespace Framework.Common.UI.Products.Shared.Pages.Browse
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Unreleased Category Pages Category page
    /// Containg functionality that still under the testing and wait for release or publishing
    /// </summary>
    public class UnreleasedCategoryPagesPage : CommonBrowsePage
    {
        private const string TabLctMask = @"//a[@class='co_tabLink'][contains(.,'{0}')]";

        /// <summary>
        /// Click Tab By Name
        /// </summary>
        /// <param name="name">Tab Name</param>
        /// <returns></returns>
        public UnreleasedCategoryPagesPage ClickTabByName(string name)
        {
            DriverExtensions.Click(By.XPath(string.Format(TabLctMask, name)));
            return this;
        }
    }
}
