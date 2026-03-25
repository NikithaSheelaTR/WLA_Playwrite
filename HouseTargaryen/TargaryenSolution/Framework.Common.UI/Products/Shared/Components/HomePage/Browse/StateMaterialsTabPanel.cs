namespace Framework.Common.UI.Products.Shared.Components.HomePage.Browse
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Core.DataModel;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// State Materials Tab Panel
    /// </summary>
    public class StateMaterialsTabPanel : BaseBrowseTabPanelComponent
    {
        private static readonly By ContainerLocator = By.Id("co_browseWidgetTabPanel3");

        private EnumPropertyMapper<Jurisdiction, BaseTextModel> jurisdictionsMap;

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "State Materials";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the content type enumeration to ContentTypeInfo map.
        /// </summary>
        protected EnumPropertyMapper<Jurisdiction, BaseTextModel> JurisdictionsMap
            => this.jurisdictionsMap = this.jurisdictionsMap ?? EnumPropertyModelCache.GetMap<Jurisdiction, BaseTextModel>();

        /// <summary>
        /// Clicks the category page link for a specified content type
        /// </summary>
        /// <typeparam name="T">the type of the page to return</typeparam>
        /// <param name="jurisdiction">the content type to navigate to</param>
        /// <returns>a browse page for the specified content type</returns>
        public T ClickBrowseCategory<T>(Jurisdiction jurisdiction) where T : ICreatablePageObject
            => this.ClickBrowseCategory<T>(this.JurisdictionsMap[jurisdiction].Text);
    }
}