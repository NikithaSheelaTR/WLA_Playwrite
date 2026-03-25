namespace Framework.Common.UI.Products.WestLawNextCanada.Items
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Items;
    using OpenQA.Selenium;
    using System.Collections.Generic;

    /// <summary>
    /// Supporting Materials Item
    /// </summary>
    public class SupportingMaterialsItem : BaseItem
    {
        private static readonly By DocumentPassageLinkLocator = By.XPath(".//a[contains(@href, 'Document') and contains(@class, 'co_snippet_link')]");

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="containerElement"></param>
        public SupportingMaterialsItem(IWebElement containerElement) : base(containerElement)
        {
        }

        /// <summary>
        /// Document passages links
        /// </summary>
        public IReadOnlyCollection<ILink> DocumentPassageLinks => 
            new ElementsCollection<Link>(this.Container, DocumentPassageLinkLocator);
    }
}