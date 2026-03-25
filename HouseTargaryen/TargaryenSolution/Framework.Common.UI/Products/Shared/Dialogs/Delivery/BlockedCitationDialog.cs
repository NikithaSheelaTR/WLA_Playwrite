
namespace Framework.Common.UI.Products.Shared.Dialogs.Delivery
{
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Blocked Citations dialog
    /// </summary>
    public class BlockedCitationDialog : BaseModuleRegressionDialog
    {
        private readonly static By ContainerLocator = By.Id("blockedCitationsLightbox");

        private readonly static By BlockedCitationHeaderLocator = By.Id("co_headerMessage");

        private readonly static By BlockedCitations = By.XPath("//p[@id='blockedCitationsMessage']/parent::div/ul//li");

        private readonly static By NextButtonLocator = By.Id("co_next");

        private readonly static By BlockedCitationMessageLocator = By.Id("blockedCitationsMessage");

        /// <summary>
        /// Get the Blocked citations header text
        /// /// </summary>
        /// <returns> Blocked Citations header text </returns>
        public ILabel BlockedCitationsHeader => new Label(ContainerLocator, BlockedCitationHeaderLocator);

        /// <summary>
        /// Get list of Citations in the dialog
        /// </summary>
        public IReadOnlyCollection<Label> BlockedCitationsList =>
            new ElementsCollection<Label>(ContainerLocator, BlockedCitations);

        /// <summary>
        /// NextButton
        /// </summary>
        public IButton NextButton => new Button(ContainerLocator, NextButtonLocator);

        /// <summary>
        /// Blocked citation message paragraph
        /// </summary>
        public ILabel BlockedCitationMessage => new Label(ContainerLocator, BlockedCitationMessageLocator);
    }
}
