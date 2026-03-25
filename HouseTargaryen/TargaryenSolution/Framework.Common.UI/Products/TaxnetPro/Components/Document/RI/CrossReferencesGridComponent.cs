namespace Framework.Common.UI.Products.TaxnetPro.Components.Document.RI
{
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestlawEdge.Components.Document.RI;

    using OpenQA.Selenium;

    /// <summary>
    /// Reference Grid Component
    /// </summary>
    public class CrossReferencesGridComponent : EdgeReferenceGridComponent
    {
        private static readonly By ItemNumberLocator = By.CssSelector(".co_detailsTable_rowNum > strong");

        private readonly string gridTableLocator;

        /// <summary>
        /// Initializes a new instance of the <see cref="EdgeReferenceGridComponent"/> class. 
        /// </summary>
        /// <param name="gridTableLocator"> Locator to identify the table element for the grid </param>
        public CrossReferencesGridComponent(string gridTableLocator) : base(gridTableLocator)
        {
            this.gridTableLocator = gridTableLocator;
        }

        /// <summary>
        /// Item number label list
        /// </summary>
        public IReadOnlyCollection<ILabel> ItemNumberLabelList => new ElementsCollection<Label>(ItemNumberLocator);
    }
}
