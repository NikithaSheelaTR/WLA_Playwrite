namespace Framework.Common.UI.Raw.WestlawEdge.Items.Folders
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Enums.Foldering;
    using Framework.Common.UI.Products.Shared.Items;

    using OpenQA.Selenium;

    /// <summary>
    /// Collaborators and roles grid items
    /// </summary>
    public class CollaboratorsAndRolesGridItem : BaseItem
    {
        private static readonly By PendingLabelLocator = By.XPath(".//i[@class='pending']");
        private static readonly By SharedWithLabelLocator = By.XPath("./div[1]");
        private static readonly By RolesDropdownLocator = By.XPath(".//select[contains(@id, 'roleSelect')]");
        
        /// <summary>
        /// Initializes a new instance of the <see cref="CollaboratorsAndRolesGridItem"/> class
        /// </summary>
        /// <param name="container">Container</param>
        public CollaboratorsAndRolesGridItem(IWebElement container)
            : base(container)
        {
        }

        /// <summary>
        /// Roles dropdown 
        /// </summary>
        public IDropdown<SharingRoles> RolesDropdown => new Dropdown<SharingRoles>(this.Container, RolesDropdownLocator);

        /// <summary>
        /// Shared with label
        /// </summary>
        public ILabel SharedWith => new Label(this.Container, SharedWithLabelLocator);

        /// <summary>
        /// Pending label
        /// </summary>
        public ILabel PendingLabel => new Label(this.Container, PendingLabelLocator);
    }
}