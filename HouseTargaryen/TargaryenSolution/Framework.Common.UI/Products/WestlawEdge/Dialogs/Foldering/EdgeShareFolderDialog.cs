namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.Foldering
{
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs.Foldering;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Elements.Toggles;
    using Framework.Common.UI.Products.WestlawEdge.Components.Folders;
    using OpenQA.Selenium;

    /// <summary>
    /// EdgeShareFolderDialog
    /// </summary>
    public class EdgeShareFolderDialog : ShareFolderDialog
    {
        private static readonly By ExpandContactsToggleLocator = By.XPath("//button[@class='ShareModalContactToggleButton']");
        private static readonly By ToggleStateLocator = By.XPath("./span");
        private static readonly By AddedContactAndGroupLink = By.XPath("//li[contains(@class,'co_contacts_addedContacts')]/a");

        /// <summary>
        /// Contacts button
        /// </summary>
        public IToggle ContactsToggle { get; } = new Toggle(ExpandContactsToggleLocator, ToggleStateLocator, "class", "co_hideState");

        /// <summary>
        /// Contacts component
        /// </summary>
        public EdgeContactsComponent ContactsComponent { get; } = new EdgeContactsComponent();

        /// <summary>
        /// List of added Contact and group 
        /// </summary>
        public IReadOnlyCollection<ILink> AddedContactAndGroupList =>
            new ElementsCollection<Link>(AddedContactAndGroupLink);
    }
}