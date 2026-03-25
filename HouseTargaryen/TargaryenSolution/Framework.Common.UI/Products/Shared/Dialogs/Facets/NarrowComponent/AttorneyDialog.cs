namespace Framework.Common.UI.Products.Shared.Dialogs.Facets.NarrowComponent
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Dialogs;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    ///  Describe Attorney dialog which appears after clicking on the 'Select' button near the Judge on the Narrow pane
    /// </summary>
    public class AttorneyDialog : BaseAvailableAndSelectedOptionsListsDialog, IHasLpaSearchLink
    {
        private static readonly By ContainerLocator = By.Id("co_facet_attorney_popup");

        /// <summary>
        /// Container
        /// </summary>
        protected override IWebElement Container => DriverExtensions.WaitForElementDisplayed(ContainerLocator);

        /// <summary>
        ///  Enter text and click Legal Professional Authority facet search link.
        /// </summary>
        /// <typeparam name="T">page</typeparam>
        /// <param name="itemName">name of item</param>
        /// <returns>new T page instance</returns>
        public T EnterTextAndClickLpaSearchLink<T>(string itemName) where T : ICreatablePageObject
        => this.EnterTextAndClickFacetSearchLink<T>(itemName);
    }
}