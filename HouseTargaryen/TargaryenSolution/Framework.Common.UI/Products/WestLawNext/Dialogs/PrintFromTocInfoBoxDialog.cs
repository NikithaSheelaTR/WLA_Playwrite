namespace Framework.Common.UI.Products.WestLawNext.Dialogs
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Enums;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Print from toc info box dialog
    /// </summary>
    public class PrintFromTocInfoBoxDialog : BaseModuleRegressionDialog
    {
        private static readonly By CloseIconLocator = By.XPath(".//a[@class='co_infoBox_closeButton']");
        private static readonly By MessageLocator = By.XPath(".//div[@class='co_infoBox_message']");

        private readonly IWebElement infoBoxContainer = DriverExtensions.GetElement(
            By.XPath(EnumPropertyModelCache.GetMap<Dialogs, WebElementInfo>()[Dialogs.PrintFromTocInfoBox].LocatorString));

        /// <summary>
        /// Click close icon.
        /// </summary>
        /// <typeparam name="T">
        /// T
        /// </typeparam>
        /// <returns>
        /// The <see cref="EdgeCommonDocumentPage"/>. 
        /// </returns>
        public T ClickCloseIcon<T>() where T : ICreatablePageObject 
            => this.ClickElement<T>(this.infoBoxContainer, CloseIconLocator);

        /// <summary>
        /// Get message text
        /// </summary>
        /// <returns>Message text</returns>
        public string GetMessageText() => DriverExtensions.GetElement(this.infoBoxContainer, MessageLocator).Text;
    }
}
