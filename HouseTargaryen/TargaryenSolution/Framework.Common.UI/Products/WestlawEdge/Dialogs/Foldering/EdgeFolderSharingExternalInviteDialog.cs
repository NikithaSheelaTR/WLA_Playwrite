namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.Foldering
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs.Foldering;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.WestlawEdge.Components.Folders;

    using OpenQA.Selenium;

    /// <summary>
    /// Shows up when you try and add an email to share with that hasn't accepted and invite before
    /// 'Collaborators and roles' component was added to this dialog as part of the Folder redesign
    /// </summary>
    public class EdgeFolderSharingExternalInviteDialog : FolderSharingExternalInviteDialog
    {
        private static readonly By ShareButtonLocator = By.Id("co_folderingShareFolderCommit");
        private static readonly By CancelButtonLocator = By.Id("co_folderingShareFolderCancel");
        private static readonly By AddButtonLocator = By.Id("co_folderingShareFolderCommit");

        /// <summary>
        /// Collaborators and roles component
        /// </summary>
        public EdgeCollaboratorsAndRolesComponent CollaboratorsAndRolesComponent { get; } = new EdgeCollaboratorsAndRolesComponent();

        /// <summary>
        /// Share button
        /// </summary>
        public IButton ShareButton => new Button(ShareButtonLocator);

        /// <summary>
        /// Share button
        /// </summary>
        public IButton AddButton => new Button(AddButtonLocator);

        /// <summary>
        /// Cancel button
        /// </summary>
        public IButton CancelButton => new Button(CancelButtonLocator);
    }
}
