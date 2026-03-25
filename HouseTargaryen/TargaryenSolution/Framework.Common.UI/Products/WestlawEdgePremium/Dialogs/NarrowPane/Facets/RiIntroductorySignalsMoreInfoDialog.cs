namespace Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.NarrowPane.Facets
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Enums;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Ri Citing Proximity More Info Dialog
    /// </summary>
    public class RiIntroductorySignalsMoreInfoDialog : BaseModuleRegressionDialog
    {
        private static readonly By CloseButtonLocator = By.XPath(".//button[@class='co_overlayBox_closeButton co_iconBtn']");

        /// <summary>
        /// Close button
        /// </summary>
        public IButton CloseButton => new Button(this.Container, CloseButtonLocator);

        private IWebElement Container =>
            DriverExtensions.WaitForElement(
                By.XPath(
                    EnumPropertyModelCache.GetMap<Dialogs, WebElementInfo>()[Dialogs.IntroductorySignalsMoreInfoDialog]
                                          .LocatorString));
    }
}