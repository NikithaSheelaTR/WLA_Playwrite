
namespace Framework.Common.UI.Products.Shared.Components.Alerts.NarrowByContent
{
    using Framework.Common.UI.Products.Shared.Dialogs.Alerts;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Search Within Results Tab
    /// </summary>
    public class SearchWithinResultsTabComponent : BaseTabComponent
    {
        private static readonly By ContainerLocator = By.Id("keycite_alerts_lightbox");
        private static readonly By SearchWithinTextboxLocator = By.Id("co_searchWithinWidget_textArea");

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Search Within Results";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// EnterSearchTerms 
        /// </summary>
        /// <param name="searchTerm"> The search Term. </param>
        /// <returns> The <see cref="NarrowByContentDialog"/>. </returns>
        public NarrowByContentDialog EnterSearchTerms(string searchTerm)
        {
            DriverExtensions.SetTextField(searchTerm, SearchWithinTextboxLocator);
            return new NarrowByContentDialog();
        }
    }
}
