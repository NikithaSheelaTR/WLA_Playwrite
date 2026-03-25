namespace Framework.Common.UI.Products.TaxnetPro.Dialog
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;

    using OpenQA.Selenium;
    using System.Collections.Generic;

    /// <summary>
    /// Dialog that pops up when search from Table Of Content hits 10,000 result
    /// </summary>
    public class TableOfContentsSearchWarningDialog : BaseModuleRegressionDialog
    {
        private static readonly By ContainerLocator = By.Id("coid_tocSearchWarningLightbox");

        private static readonly By OkButtonLocator = By.Id("coid_tocSearchWarningConfirmButton");

        private static readonly By CancelButtonLocator = By.Id("coid_tocSearchWarningCancelButton");

        private static readonly By TitleLocator = By.Id("coid_lightboxAriaLabel_2");

        private static readonly By TextLocator = By.CssSelector(".co_overlayBox_content>div");

        /// <summary>
        /// Ok button
        /// </summary>
        public IButton OkButton => new Button(ContainerLocator, OkButtonLocator);

        /// <summary>
        /// Cancel button
        /// </summary>
        public IButton CancelButton => new Button(ContainerLocator, CancelButtonLocator);

        /// <summary>
        /// Dialog title label
        /// </summary>
        public ILabel TitleLabel => new Label(ContainerLocator, TitleLocator);

        /// <summary>
        /// Dialog text label list
        /// </summary>
        public IReadOnlyCollection<ILabel> TextLabelList => new ElementsCollection<Label>(ContainerLocator, TextLocator);
    }
}
