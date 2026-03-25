namespace Framework.Common.UI.Products.WestlawEdge.Components.Toolbar
{
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Toolbars;
    using Framework.Core.Utils.Enums;

    /// <summary>
    /// Statute History Toolbar Component
    /// </summary>
    public class StatuteHistoryToolbarComponent : EdgeToolbarComponent
    {
        /// <summary>
        /// Toolbar Options Map
        /// </summary>
        protected override EnumPropertyMapper<EdgeToolbarElements, WebElementInfo> ToolbarMap =>
            EnumPropertyModelCache.GetMap<EdgeToolbarElements, WebElementInfo>(
                                  "StatuteHistory",
                                  @"Resources/EnumPropertyMaps/WestlawEdge/Toolbars");
    }
}