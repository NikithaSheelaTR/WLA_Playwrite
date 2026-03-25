namespace Framework.Common.UI.Raw.WestlawEdge.Items.Folders
{
    using System;
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Enums.Foldering;
    using Framework.Common.UI.Products.Shared.Items.FolderHistory;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdge.Components.Folders;
    using Framework.Common.UI.Products.WestlawEdge.DropDowns;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Edge Folder Grid Item for Edge Research Organizer page
    /// </summary>
    public class EdgeFolderGridItem : FolderGridItem
    {
        private static readonly By NoteLocator = By.XPath(".//div[@class = 'Foldering--Note']");
        private static readonly By SocialIconLocator = By.XPath(".//span[contains(@class, 'icon_folderShared')]");
        private static readonly By FolderNameLinkLocator = By.XPath(".//button[@class = 'co_linkBlue']");
        private static readonly By HighlightedLabelLocator = By.XPath(".//div[contains(@class, 'highlighted-label')]");
        private static readonly By NewTagLocator = By.XPath(".//span[contains(@class,'Badge badge--blue co_foldering_newContent')]");

        /// <summary>
        /// Constructor
        /// Initializes a new instance of the <see cref="EdgeFolderGridItem"/> class. 
        /// </summary>
        /// <param name="tableEntryContainer"></param>
        public EdgeFolderGridItem(IWebElement tableEntryContainer) : base(tableEntryContainer)
        {
        }

        /// <summary>
        /// Folder name after applying Search within
        /// </summary>
        public IReadOnlyCollection<ILink> FolderNameLink => 
            new ElementsCollection<Link>(this.Container, By.XPath(this.ColumnsMap[Columns.Folder].LocatorString), FolderNameLinkLocator);
        
        /// <summary>
        /// ActionsMenu dropdown 
        /// </summary>
        public ActionsMenuDropdown ActionsMenu => new ActionsMenuDropdown(this.Container);

        /// <summary>
        /// Add note to item component 
        /// </summary>
        public EdgeAddNoteToItemComponent AddNoteToItem => new EdgeAddNoteToItemComponent(this.Container);
        
        /// <summary>
        /// Added By
        /// </summary>
        public ILabel AddedBy => new Label(this.Container, By.XPath(this.ColumnsMap[Columns.AddedBy].LocatorString));
        
        /// <summary>
        /// Note
        /// </summary>
        public ILabel Note => new Label(this.Container, NoteLocator);

        /// <summary>
        /// Social icon
        /// </summary>
        public ILabel SocialIcon => new Label(this.Container, SocialIconLocator);

        /// <summary>
        /// Highlighted label indicates selected item in folder grid after applying search within
        /// </summary>
        public ILabel HighlightedLabel => new Label(this.Container, HighlightedLabelLocator);

        /// <summary>
        /// New tag
        /// </summary>
        public ILabel NewTag => new Label(this.Container, NewTagLocator);

        /// <summary>
        /// Gets the Flag enumeration to WebElementInfo map.
        /// </summary>
        protected override EnumPropertyMapper<KeyCiteFlag, WebElementInfo> KeyCiteFlagsMap =>
            EnumPropertyModelCache.GetMap<KeyCiteFlag, WebElementInfo>(String.Empty, @"Resources/EnumPropertyMaps/WestlawEdge/FoldersPopUp");
    }
}