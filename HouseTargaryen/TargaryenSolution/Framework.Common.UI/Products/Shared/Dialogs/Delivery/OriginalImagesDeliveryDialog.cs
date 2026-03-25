namespace Framework.Common.UI.Products.Shared.Dialogs.Delivery
{
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// OriginalImagesDeliveryDialog
    /// </summary>
    public class OriginalImagesDeliveryDialog : BaseModuleRegressionDialog
    {
        private static readonly By ContainerLocator = By.Id("reporterImagesLightbox");

        private static readonly By CitationBlockLocator = By.XPath(".//div[@class='co_overlayBox_content']");

        private static readonly By CitationLinkLocator = By.XPath(".//ul//a");

        private static readonly By NextButtonLocator = By.Id("co_next");

        private static readonly By ReporterImagesLocator = By.Id("co_downloadReporterImages");

        private static readonly By CloseButtonLocator = By.Id("co_deliveryCloseButton");

        private static readonly By CancelLinkLocator = By.Id("co_deliveryCancelButton");

        private static readonly By RemainingDeliveryNoteLocator = By.XPath("//p//strong");

        /// <summary>
        /// NextButton
        /// </summary>
        public IButton NextButton => new Button(NextButtonLocator);

        /// <summary>
        /// Citation Links
        /// </summary>
        public IReadOnlyCollection<ILink> CitationLinks =>
            new ElementsCollection<Link>(ContainerLocator, CitationBlockLocator, CitationLinkLocator);

        /// <summary>
        /// Note on original delivery dialog for remaining citations
        /// </summary>
        public ILabel RemainingDeliveryNoteLabel => new Label(ContainerLocator, CitationBlockLocator, RemainingDeliveryNoteLocator);

        /// <summary>
        /// Reporter images Link
        /// </summary>
        public ILink ReporterImagesLink => new Link(ReporterImagesLocator);

        /// <summary>
        /// Close button
        /// </summary>
        public IButton CloseButton => new Button(CloseButtonLocator);

        /// <summary>
        /// Cancel button
        /// </summary>
        public ILink CancelLink => new Link(CancelLinkLocator);

        /// <summary>
        /// click next button
        /// </summary>
        /// <typeparam name="TPage"> Page type </typeparam>
        /// <returns>The type of page.</returns>
        public TPage ClickNext<TPage>() where TPage : ICreatablePageObject
        {
            DriverExtensions.ScrollTo(NextButtonLocator);
            DriverExtensions.WaitForElement(NextButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<TPage>();
        }
    }
}
