namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.QuickCheck
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
    /// The document analyzer security description dialog.
    /// </summary>
    public class QuickCheckSecurityDescriptionDialog : BaseModuleRegressionDialog
    {
        private static readonly By TextLocator = By.XPath(".//div[@class = 'co_overlayBox_content']");
        private static readonly By CloseButtonLocator = By.XPath(".//button[.='Close']");

        private readonly IWebElement container = DriverExtensions.GetElement(
            By.XPath(EnumPropertyModelCache.GetMap<Dialogs, WebElementInfo>()[Dialogs.DocumentAnalyzerSecurityDescriptionDialog].LocatorString));

        /// <summary>
        /// The security text label.
        /// </summary>
        public ILabel SecurityTextLabel => new Label(this.container, TextLocator);

        /// <summary>
        /// The close button.
        /// </summary>
        public IButton CloseButton => new Button(this.container, CloseButtonLocator);
    }
}