namespace Framework.Common.UI.Products.Shared.Components.IpTools.DrawingsTabComponents
{
    using System.Linq;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Navigation component through pages situated on the top
    /// </summary>
    public class PagesTopNavigationComponent : BaseModuleRegressionComponent
    {
        private static readonly By SelectAllCheckboxLocator =
            By.XPath(".//input[contains(@id,'IPDrawings-selectAllCheckbox')]");

        private static readonly By SelectionLabelLocator = By.XPath(".//li[@class='co_navItemsSelected']");

        private static readonly By ClearAllLinkLocator = By.XPath(".//a[@id='IPDrawings-clearSelected']");

        private static readonly By NextArrowButtonLocator = By.XPath("//li[@id='IPDrawings-next-arrow']/a");
        private static readonly By PreviousArrowButtonLocator = By.XPath("//li[@id='IPDrawings-previous-arrow']/a");
        private static readonly By PagesDisplayCountLabelLocator = By.XPath("//li[@id='IPDrawings-display-count']");

        /// <inheritdoc />
        protected override By ComponentLocator => By.XPath("//div[@class='IPDrawings-toolbar']/ul[@class='co_navOptions']");

        /// <summary>
        /// select all checkbox
        /// </summary>
        public ICheckBox SelectAllCheckBox => new CheckBox(this.ComponentLocator, SelectAllCheckboxLocator);

        /// <summary>
        /// Selection Label
        /// </summary>
        public ILabel SelectionLabel => new Label(this.ComponentLocator, SelectionLabelLocator);

        /// <summary>
        /// select all checkbox
        /// </summary>
        public ILabel PagesDisplayedLabel => new Label(PagesDisplayCountLabelLocator);

        /// <summary>
        /// Clear all link
        /// </summary>
        public ILink ClearAllLink =>
            DriverExtensions.GetElements(this.ComponentLocator, ClearAllLinkLocator).Any()
                ? new Link(this.ComponentLocator, ClearAllLinkLocator)
                : null;

        /// <summary>
        /// Next arrow button
        /// </summary>
        public IButton NextArrowButton =>
            DriverExtensions.GetElements(NextArrowButtonLocator).Any()
                ? new Button(NextArrowButtonLocator)
                : null;
        /// <summary>
        /// Previous arrow button
        /// </summary>
        public IButton PreviousArrowButton =>
            DriverExtensions.GetElements(PreviousArrowButtonLocator).Any()
                ? new Button(PreviousArrowButtonLocator)
                : null;

    }
}