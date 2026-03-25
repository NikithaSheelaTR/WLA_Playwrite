namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.ResearchReport
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;

    using OpenQA.Selenium;

    /// <summary>
    /// The include subfolders dialog. Appears in case of creating Research report from the folder which has subfolders
    /// </summary>
    public class IncludeSubfoldersDialog : BaseModuleRegressionDialog
    {
        private static readonly By DoNotIncludeSubfoldersButtonLocator = By.Id("excludeSubfoldersBtn");

        private static readonly By IncludeSubfoldersIfAnyButtonLocator = By.Id("includeSubfoldersBtn");

        private static readonly By CloseButtonLocator = By.Id("co_closeGenerateReportDialog");

        private static readonly By DialogTitleLocator = By.XPath("//div[@class='co_overlayBox_headline']//h2 | //div[@class='co_overlayBox_headline']//h1");

        /// <summary>
        /// Dialog title
        /// </summary>
        public ILabel DialogTitle => new Label(DialogTitleLocator);

        /// <summary>
        /// Close button
        /// </summary>
        public IButton CloseButton => new Button(CloseButtonLocator);

        /// <summary>
        /// Do not include subfolders button
        /// </summary>
        public IButton DoNotIncludeSubfoldersButton => new Button(DoNotIncludeSubfoldersButtonLocator);

        /// <summary>
        /// Include subfolders button
        /// </summary>
        public IButton IncludeSubfoldersButton => new Button(IncludeSubfoldersIfAnyButtonLocator);
    }
}