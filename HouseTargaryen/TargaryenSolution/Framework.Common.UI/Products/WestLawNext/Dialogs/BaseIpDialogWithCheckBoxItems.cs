namespace Framework.Common.UI.Products.WestLawNext.Dialogs
{
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using Framework.Common.UI.Products.WestLawNext.Items.IpItems;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// The base dialog with check box tree component
    /// </summary>
    public abstract class BaseIpDialogWithCheckBoxItems: BaseModuleRegressionDialog
    {
        private static readonly By CancelButtonLocator = By.XPath(".//a[@class='co_overlayBox_buttonCancel']");

        private static readonly By ClassItemContainerLocator = By.XPath(".//ul[@id='co_treeSearch_list' or contains(@id, 'facet_hierarchy_children')]");

        private static readonly By ClassItemLocator = By.XPath(".//li");

        private static readonly By FilterButtonLocator = By.XPath("//input[@id='co_facet_ipClassInternational_filterButton']");

        private static readonly By SearchTextBoxLocator = By.XPath(".//input[@id ='co_docTocOverlay_searchInput']");

        private static readonly By SearchButtonLocator = By.XPath(".//input[@id ='coid_ipTreeSearchButtonId']");

        /// <summary>
        /// Cancel Button
        /// </summary>
        public IButton CancelButton => new Button(this.Container, CancelButtonLocator);

        /// <summary>
        /// Filter Button
        /// </summary>
        public IButton FilterButton => new Button(this.Container, FilterButtonLocator);

        /// <summary>
        /// Gets Items with checkboxes
        /// </summary>
        public IList<BaseClassItem> ClassItems => DriverExtensions.GetElements(this.Container, ClassItemContainerLocator, ClassItemLocator).Where(el => el.Displayed).Select(element=> new BaseClassItem(element)).ToList();

        /// <summary>
        /// Search Button
        /// </summary>
        public IButton SearchButton => new Button(this.Container, SearchButtonLocator);

        /// <summary>
        /// Search Input TextBox
        /// </summary>
        public ITextbox SearchTextBox => new Textbox(this.Container, SearchTextBoxLocator);

        /// <summary>
        /// Element Container of dialog
        /// </summary>
        protected abstract IWebElement Container { get; }
    }
}
