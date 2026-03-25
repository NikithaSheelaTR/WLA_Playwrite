namespace Framework.Common.UI.Products.WestLawNext.Pages.Search
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// WLN IRS Tax AST page
    /// </summary>
    public sealed class IrsTaxAstPage : CommonAdvancedSearchPage
    {
        private static readonly By IrsTaxIrcAddButtonLocator = By.Id("co_ircAddButton");

        /// <summary>
        /// Click Add Internal Revenue Code
        /// </summary>
        public void ClickAddInternalRevenueCode() => DriverExtensions.WaitForElement(IrsTaxIrcAddButtonLocator).Click();
    }
}