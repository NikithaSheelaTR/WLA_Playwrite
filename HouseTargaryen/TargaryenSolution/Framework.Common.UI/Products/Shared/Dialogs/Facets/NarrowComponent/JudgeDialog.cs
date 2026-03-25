namespace Framework.Common.UI.Products.Shared.Dialogs.Facets.NarrowComponent
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Dialogs;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    ///  Describe Judge dialog which appears after clicking on the 'Select' button near the Judge on the Narrow pane
    /// </summary>
    public class JudgeDialog : BaseAvailableAndSelectedOptionsListsDialog, IHasLpaSearchLink
    {
        private static readonly By ContainerLocator = By.Id("co_facet_judge_popup");

        /// <summary>
        /// Container
        /// </summary>
        protected override IWebElement Container => DriverExtensions.WaitForElementDisplayed(ContainerLocator);

        /// <summary>
        /// Enter text and click facet Legal Professional Authority search link.
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