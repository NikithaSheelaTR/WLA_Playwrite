namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.MultipleSearchWithin
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Enums;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;

    /// <summary>
    /// Search within from recent history dialog
    /// </summary>
    public class SearchWithinFromHistoryDialog : BaseModuleRegressionDialog
    {
        private static readonly By ContentMessageLocator = By.XPath(".//div[@class = 'co_overlayBox_content']");
        private static readonly By ContinueButtonLocator = By.XPath(".//input[@value = 'Continue']");
        private static readonly By CancelButtonLocator = By.XPath(".//a[@class = 'co_overlayBox_buttonCancel']");

        /// <summary>
        /// Continue button
        /// </summary>
        public IButton ContinueButton => new Button(this.Container, ContinueButtonLocator);

        /// <summary>
        /// Cancel button
        /// </summary>
        public IButton CancelButton => new Button(this.Container, CancelButtonLocator);

        /// <summary>
        /// Content message label
        /// </summary>
        public ILabel ContentMessageLabel => new Label(this.Container, ContentMessageLocator);

        /// <summary>
        /// Dialog container element
        /// </summary>
        private IWebElement Container => DriverExtensions.GetElement(
            By.XPath(EnumPropertyModelCache.GetMap<Dialogs, WebElementInfo>()[Dialogs.SearchWithinFromHistoryDialog].LocatorString));

    }
}
