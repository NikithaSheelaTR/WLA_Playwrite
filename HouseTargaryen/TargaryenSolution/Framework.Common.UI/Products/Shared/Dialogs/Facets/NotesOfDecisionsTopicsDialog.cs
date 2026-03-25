namespace Framework.Common.UI.Products.Shared.Dialogs.Facets
{
    using Framework.Common.UI.Products.Shared.Dialogs.Facets.NarrowComponent;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Notes Of Decisions Dialog for Notes Of Decisions Facet
    /// </summary>
    public class NotesOfDecisionsTopicsDialog : BaseAvailableAndSelectedOptionsListsDialog
    {
        private static readonly By ContainerLocator = By.CssSelector("#co_facet_nod_popup, #co_facet_NODTopics_popup");

        /// <summary>
        /// Container
        /// </summary>
        protected override IWebElement Container =>
            DriverExtensions.WaitForElementDisplayed(ContainerLocator);
    }
}