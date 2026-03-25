namespace Framework.Common.UI.Products.WestLawNextCanada.Dialogs.QuickCheck
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using OpenQA.Selenium;
    using System.Collections.Generic;

    /// <summary>
    /// Dialog for filtering by document in Quick Check feature in Westlaw Edge Canada.
    /// </summary>
    public class FilterByDocumentDialog : BaseModuleRegressionDialog
    {
        private static readonly By TitleLabelLocator = By.XPath("//div[@id='coid_docHeadingFacetModal']//h2");
        private static readonly By SelectAllItemsCheckbox = By.XPath("//label[@class='DA-FilterDocSelected']/input");
        private static readonly By SelectAllItemsLabelLocator = By.XPath("//label[@class='DA-FilterDocSelected']/span");
        private static readonly By DocumentHeadingsCheckboxLocator = By.XPath("//td[@class='DA-FilterHeading']/input");
        private static readonly By DocumentHeadingsLabelLocator = By.XPath("//td[@class='DA-FilterHeading']/label");
        private static readonly By DocumentCountLabelLocator = By.XPath("//td[@class='DA-FilterHeading']/following-sibling::td");
        private static readonly By FilterButtonLocator = By.XPath("//button[text()='Filter']");
        private static readonly By SelectedItemsLabelLocator = By.XPath("//span[@class='DA-FilterDocSelected']");

        /// <summary>
        /// Title of the dialog.    
        /// </summary>
        public ILabel TitleLabel => new Label(TitleLabelLocator);

        /// <summary>
        /// Select All CheckBox for items in the dialog.
        /// </summary>
        public ICheckBox SelectAllCheckBox => new CheckBox(SelectAllItemsCheckbox);

        /// <summary>
        /// Label for the Select All CheckBox in the dialog.
        /// </summary>
        public ILabel SelectAllItemsLabel => new Label(SelectAllItemsLabelLocator);

        /// <summary>
        /// Collection of document links in the Navigate Headings dialog.
        /// </summary>
        public IReadOnlyCollection<ICheckBox> HeaderCheckboxesList => new ElementsCollection<CheckBox>(DocumentHeadingsCheckboxLocator);

        /// <summary>
        /// Collection of labels for document headings in the dialog.
        /// </summary>
        public IReadOnlyCollection<ILabel> HeaderLabelsList => new ElementsCollection<Label>(DocumentHeadingsLabelLocator);

        /// <summary>
        /// Collection of document count labels in the dialog.
        /// </summary>
        public IReadOnlyCollection<ILabel> DocumentCountLabelsList => new ElementsCollection<Label>(DocumentCountLabelLocator);

        /// <summary>
        /// Filter button in the dialog.
        /// </summary>
        public IButton FilterButton => new Button(FilterButtonLocator);

        /// <summary>
        /// Label for the selected items in the dialog.
        /// </summary>
        public ILabel SelectedItemsLabel => new Label(SelectedItemsLabelLocator);
    }
}