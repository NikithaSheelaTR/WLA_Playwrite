namespace Framework.Common.UI.Products.Shared.Components.IpTools
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components.Document.RI;
    using Framework.Common.UI.Products.Shared.Components.IpTools.DrawingsTabComponents;

    /// <summary>
    /// Drawings RI tab
    /// </summary>
    public class IpDrawingsTabComponent : RelatedInfoTabComponent, ICreatablePageObject
    {
        /// <summary>
        /// Descriptions
        /// </summary>
        public DescriptionsComponent Descriptions => new DescriptionsComponent();

        /// <summary>
        /// Pages
        /// </summary>
        public PagesComponent Pages => new PagesComponent();
    }
}