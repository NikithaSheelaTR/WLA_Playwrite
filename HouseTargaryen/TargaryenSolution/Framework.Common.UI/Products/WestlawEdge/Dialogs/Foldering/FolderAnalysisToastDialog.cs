namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.Foldering
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Enums;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Folder analysis toast that indicates that there are new recommendations in the folder analysis slider
    /// </summary>
    public class FolderAnalysisToastDialog : BaseModuleRegressionDialog
    {
        private static readonly By NewRecommendationsLinkLocator = By.XPath("//button[text() = 'New recommendations']");
        private static readonly By CloseButtonLocator = By.XPath("//button[@id = 'co_hideToast']");

        /// <summary>
        /// New recommendations link locator
        /// </summary>
        public ILink NewRecommendationsLink => new Link(NewRecommendationsLinkLocator);

        /// <summary>
        /// Close button
        /// </summary>
        public IButton CloseButton => new Button(CloseButtonLocator);
    }
}