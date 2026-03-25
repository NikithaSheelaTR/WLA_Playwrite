namespace Framework.Common.UI.Products.WestLawNextCanada.Dialogs.NewTypeAhead
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Dialogs.CustomPages.ManagePage;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Add Content to Content Section Dialog
    /// </summary>
    public class CanadaAddContentToContentSectionDialog : BaseManagePageDialog
    {
        private static readonly By SearchContentLocator = By.Id("cp_selectContent_search_input");

        /// <summary>
        /// Type here to find and select content by name or database
        /// </summary>
        /// <typeparam name="T">The component type</typeparam>
        /// <param name="searchtext">Search Text</param>
        /// <returns>The specified type</returns>
        public T EnterSearchTextToFindContent<T>(string searchtext)
            where T : ICreatablePageObject
        {
            DriverExtensions.GetElement(SearchContentLocator).SendKeys(searchtext);
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}
