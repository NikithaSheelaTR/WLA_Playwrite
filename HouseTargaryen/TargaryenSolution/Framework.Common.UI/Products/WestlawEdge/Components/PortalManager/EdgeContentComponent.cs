namespace Framework.Common.UI.Products.WestlawEdge.Components.PortalManager
{
    using Framework.Common.UI.Products.Shared.Dialogs.Alerts;
    using Framework.Common.UI.Products.WestLawNext.Components.PortalManager;
    using Framework.Common.UI.Products.WestLawNext.Models.EnumProperties;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Content;
    using Framework.Common.UI.Raw.WestlawEdge.Pages.PortalManager;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Indigo WestLawNext Content Component
    /// </summary>
    public class EdgeContentComponent : WlnContentComponent
    {
        private static readonly By CasesLocator = By.XPath("id('co_wizardStep_left_Home_Cases')/i");

        private EnumPropertyMapper<ContentTypeEdge, ContentTypeInfo> contentTypeMap;

        /// <summary>
        /// Gets the content type enumeration to ContentTypeInfo map.
        /// </summary>
        protected new EnumPropertyMapper<ContentTypeEdge, ContentTypeInfo> ContentTypeMap =>
            this.contentTypeMap = this.contentTypeMap
                                  ?? EnumPropertyModelCache.GetMap<ContentTypeEdge, ContentTypeInfo>(
                                      string.Empty,
                                      @"Resources/EnumPropertyMaps/WestlawEdge/Content");

        /// <summary>
        /// Selects the Cases - All Federal and All States, Key Numbers and Secondary Sources content in the WLN Content section
        /// </summary>
        public new void SelectContentPredefined()
        {
            DriverExtensions.Click(CasesLocator);
            var jurisdictionDialog = new JurisdictionOptionsDialog();
            jurisdictionDialog.SelectDefaultJurisdiction().SaveButton.Click<EdgeGlobalSearchFormPage>();

            this.SelectItemsFromPath(
                string.Empty,
                this.ContentTypeMap[ContentTypeEdge.TopicsAndKeynumbers].Text,
                this.ContentTypeMap[ContentTypeEdge.SecondarySources].Text);
        }
    }
}