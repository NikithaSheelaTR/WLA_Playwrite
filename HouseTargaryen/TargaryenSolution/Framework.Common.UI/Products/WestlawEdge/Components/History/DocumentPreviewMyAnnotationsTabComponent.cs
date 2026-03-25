namespace Framework.Common.UI.Products.WestlawEdge.Components.History
{
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// My Annotations Tab Component in Document Preview
    /// </summary>
    public class DocumentPreviewMyAnnotationsTabComponent : BaseTabComponent
    {
        private static readonly By ContainerLocator = By.Id("tab_co_annotationsPanel");

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => DriverExtensions.GetElement(this.ComponentLocator).Text;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}