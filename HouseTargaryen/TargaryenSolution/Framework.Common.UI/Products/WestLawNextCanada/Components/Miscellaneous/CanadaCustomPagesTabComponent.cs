namespace Framework.Common.UI.Products.WestLawNextCanada.Components.Miscellaneous
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Dialogs.CustomPages;
    using Framework.Common.UI.Products.Shared.Pages.CustomPages;
    using Framework.Common.UI.Products.WestlawEdge.Components.Miscellaneous;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;
    using OpenQA.Selenium;

    /// <summary>
    ///  Canada Custom Pages tab component
    /// </summary>
    public class CanadaCustomPagesTabComponent : CustomPagesTabComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@aria-label='My links menu']");

        private static readonly By CreateCustomPageButtonLocator = By.XPath("//*[text()='Create Custom Page']");

        private const string CustomPageLinkLctMask = "//div[@class='FrequentFavorites-topCustomPages']//a[text()={0}]";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Click 'Create Custom Page' button
        /// </summary>
        /// <returns>new instance of CreateCustomPageDialog</returns>
        public new CreateCustomPageDialog ClickCreateCustomPage()
        {
            DriverExtensions.WaitForElement(CreateCustomPageButtonLocator).Click();
            return new CreateCustomPageDialog();
        }

        /// <summary>
        /// Click Custom page link by name
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="linkName">
        /// Link name 
        /// </param>
        /// <returns>
        /// The <see cref="CustomPage"/>. 
        /// </returns>
        public new T ClickCustomPageLinkByName<T>(string linkName) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(SafeXpath.BySafeXpath(CustomPageLinkLctMask, linkName)).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}
