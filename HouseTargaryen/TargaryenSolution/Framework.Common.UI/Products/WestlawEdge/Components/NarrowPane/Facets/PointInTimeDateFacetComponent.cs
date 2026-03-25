namespace Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using OpenQA.Selenium;

    /// <summary>
    /// Point In Time Date Facet Component
    /// </summary>
    public class PointInTimeDateFacetComponent : EdgeBaseFacetComponent
    {
        private static readonly By ContainerLocator = By.Id("facet_div_legislationPointInTime");

        private static readonly By RemoveLinkLocator = By.XPath("//*[contains(@class,'button') and text()='Remove']");

        private static readonly By InputFieldLocator = By.Id("SearchFacetPopupEntry-legislationPointInTimeInput");

        private static readonly By GoButtonLocator = By.XPath(".//button[contains(@class,'SearchFacet-buttonSubmit')]");

        private static readonly By AppliedOptionTextLocator = By.ClassName("SearchFacet-outputTextValue");

        /// <summary>
        /// Remove Link
        /// </summary>
        public ILink RemoveLink => new Link(RemoveLinkLocator);

        /// <summary>
        /// Go Button
        /// </summary>
        public IButton GoButton => new Button(ContainerLocator, GoButtonLocator);

        /// <summary>
        /// Date input
        /// </summary>
        public ITextbox DateInput => new Textbox(ContainerLocator, InputFieldLocator);

        /// <summary>
        /// Applied option text
        /// </summary>
        public ILabel AppliedOption => new Label(ContainerLocator, AppliedOptionTextLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Appy facet
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="date">Date</param>
        /// <returns>Instance of T page</returns>
        public T ApplyFacet<T>(string date) where T : ICreatablePageObject
        {
            this.ExpandFacet();
            this.DateInput.SetText(date);
            return this.GoButton.Click<T>();
        }
    }
}
