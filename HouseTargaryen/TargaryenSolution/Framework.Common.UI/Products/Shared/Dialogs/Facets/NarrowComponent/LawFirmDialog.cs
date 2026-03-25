namespace Framework.Common.UI.Products.Shared.Dialogs.Facets.NarrowComponent
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Dialogs;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Describe Law Firm Dialog which appears after clicking on the 'Select' button near the Law Firm on the Narrow pane
    /// </summary>
    public class LawFirmDialog : BaseAvailableAndSelectedOptionsListsDialog, IHasLpaSearchLink
    {
        private static readonly By ContainerLocator = By.Id("co_facet_lawfirm_popup");

        /// <summary>
        /// Container
        /// </summary>
        protected override IWebElement Container => DriverExtensions.WaitForElementDisplayed(ContainerLocator);

        /// <summary>
        /// Enter text and click Legal Professional Authority facet search link.
        /// </summary>
        /// <param name="itemName">
        /// The item name.
        /// </param>
        /// <typeparam name="T">
        /// T
        /// </typeparam>
        /// <returns>
        /// T
        /// </returns>
        public T EnterTextAndClickLpaSearchLink<T>(string itemName) where T : ICreatablePageObject
        => this.EnterTextAndClickFacetSearchLink<T>(itemName);
    }
}