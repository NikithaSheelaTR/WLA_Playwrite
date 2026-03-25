namespace Framework.Common.UI.Products.WestLawNextCanada.Components.Miscellaneous
{
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdge.Components.Miscellaneous;
    using Framework.Common.UI.Raw.WestlawEdge.Enums;
    using Framework.Core.Utils.Enums;

    /// <summary>
    /// Miscellaneous Tab Panel
    /// </summary>
    public class CanadaMiscellaneousTabPanel : MiscellaneousTabPanel
    {
        /// <summary>
        /// Miscellaneous tabs map
        /// </summary>
        protected override EnumPropertyMapper<MiscellaneousTabs, WebElementInfo> TabsMap =>
            EnumPropertyModelCache.GetMap<MiscellaneousTabs, WebElementInfo>(
                string.Empty,
                @"Resources/EnumPropertyMaps/WestlawNextCanada");
    }
}
