namespace Framework.Common.UI.Products.WestlawEdge.Components.History
{
    using System;
    using System.Collections.Generic;

    using Framework.Common.UI.Products.Shared.Components.TabPanel;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.History;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Documents preview tab panel
    /// </summary>
    public class DocumentPreviewTabPanel : TabPanel<DocumentPreviewTabs>
    {
        private static readonly By CurrentActiveTabLocator = By.XPath("//div[contains(@class, 'GH-Panel-tab-container')]//li[contains(@class,'Tab--active')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentPreviewTabPanel"/> class
        /// </summary>
        public DocumentPreviewTabPanel()
        {
            this.AllPossibleTabOptions = new Dictionary<DocumentPreviewTabs, Type>
                                             {
                                                 { DocumentPreviewTabs.MyAnnotations, typeof(DocumentPreviewMyAnnotationsTabComponent) },
                                                 { DocumentPreviewTabs.Folders, typeof(DocumentPreviewFoldersTabComponent) }
                                             };
        }

        /// <summary>
        /// History tabs map
        /// </summary>
        protected override EnumPropertyMapper<DocumentPreviewTabs, WebElementInfo> TabsMap =>
            EnumPropertyModelCache.GetMap<DocumentPreviewTabs, WebElementInfo>(
                string.Empty,
                @"Resources/EnumPropertyMaps/WestlawEdge/Document");

        /// <summary>
        /// Is tab active
        /// </summary>
        /// <param name="tab">Tab option</param>
        /// <returns>True if active, false otherwise</returns>
        public override bool IsActive(DocumentPreviewTabs tab)
            => DriverExtensions.WaitForElement(CurrentActiveTabLocator).Text.Contains(this.TabsMap[tab].Text);

        /// <summary>
        /// Is tab displayed
        /// </summary>
        /// <param name="tab">Tab option</param>
        /// <returns>True if displayed, false otherwise</returns>
        public override bool IsDisplayed(DocumentPreviewTabs tab)
            => DriverExtensions.IsDisplayed(By.XPath(this.TabsMap[tab].LocatorString));
    }
}
