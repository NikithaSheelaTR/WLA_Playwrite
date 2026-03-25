namespace Framework.Common.UI.Products.WestLawNextCanada.Dialogs.QuickCheck
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using OpenQA.Selenium;
    using System.Collections.Generic;

    /// <summary>
    /// Dialog for navigating headings in Quick Check feature in Westlaw Edge Canada.
    /// </summary>
    public class NavigateHeadingsDialog : BaseModuleRegressionDialog
    {
        private static readonly By HeadingDialogTitleLocator = By.XPath("//div[@id='coid_outline_lightbox']//h2");
        private static readonly By CancelButtonLocator = By.ClassName("co_overlayBox_buttonCancel");
        private static readonly By DocumentLinksLocator = By.XPath("//div[@class='DA-outline']//li/a");

        /// <summary>
        /// Heading Dialog Title label
        /// </summary>
        public ILabel HeadingDialogTitleLabel => new Label(HeadingDialogTitleLocator);

        /// <summary>
        /// Cancel Button
        /// </summary>
        public IButton CancelButton => new Button(CancelButtonLocator);

        /// <summary>
        /// Collection of document links in the Navigate Headings dialog.
        /// </summary>
        public IReadOnlyCollection<ILink> DocumentLinks => new ElementsCollection<Link>(DocumentLinksLocator);
    }
}