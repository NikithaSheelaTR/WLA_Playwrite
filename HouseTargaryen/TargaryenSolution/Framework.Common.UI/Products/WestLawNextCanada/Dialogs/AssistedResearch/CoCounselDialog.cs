namespace Framework.Common.UI.Products.WestLawNextCanada.Dialogs.AssistedResearch
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// CoCounsel dialog
    /// </summary>
    public class CoCounselDialog : BaseModuleRegressionDialog
    {
        private static readonly By ContainerLocator = By.XPath("//*[@id='co_aiAssistantContainer']/div[@role='dialog']");
        private static readonly By TitleLabelLocator = By.XPath(".//h2");
        private static readonly By CloseButonLocator = By.XPath(".//button[@class='co_overlayBox_closeButton']");
        private static readonly By AiAssistedResearchLinkLocator = By.XPath(".//a[contains(@aria-label, 'AI-Assisted Research')]");
        private static readonly By CoCounselLinkLocator = By.XPath(".//a[contains(@aria-label, 'CoCounsel')]");
        private static readonly By CoCounselDropboxMsgLocator = By.ClassName("dropDownBox-message");
        private static readonly By CoCounselDialogSubHeadingsLocator = By.ClassName("dropDownBox-listHeading");
        private static readonly By CoCounselAlertTextLocator = By.XPath("//div[@class='saf-alert saf-alert_information']/div[contains(@class,'content')]/span");

        /// <summary>
        /// Title label
        /// </summary>
        public ILabel TitleLabel => new Label(ContainerLocator, TitleLabelLocator);

        /// <summary>
        /// Close button
        /// </summary>
        public IButton CloseButton => new Button(ContainerLocator, CloseButonLocator);

        /// <summary>
        /// AI-Assisted Research link
        /// </summary>
        public ILink AiAssistedResearchLink => new Link(ContainerLocator, AiAssistedResearchLinkLocator);

        /// <summary>
        /// Co counsel link
        /// </summary>
        public ILink CoCounselLink => new Link(ContainerLocator, CoCounselLinkLocator);

        /// <summary>
        /// CoCounsel dropbox message
        /// </summary>
        public ILabel CoCounselDropboxMessage => new Label(ContainerLocator, CoCounselDropboxMsgLocator);

        /// <summary>
        /// CoCounsel alert text
        /// </summary>
        public ILabel CoCounselAlertText => new Label(ContainerLocator, CoCounselAlertTextLocator);

        /// <summary>
        /// List of sub heading labels
        /// </summary>
        public List<Label> SubHeadingsLabelList =>
            DriverExtensions.GetElements(ContainerLocator, CoCounselDialogSubHeadingsLocator)
                    .Select(webEl => new Label(webEl)).ToList();
    }
}
