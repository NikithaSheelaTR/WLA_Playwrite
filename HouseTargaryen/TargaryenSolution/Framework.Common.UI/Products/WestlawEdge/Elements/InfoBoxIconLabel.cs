namespace Framework.Common.UI.Products.WestlawEdge.Elements
{
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils;
    using OpenQA.Selenium;

    /// <summary>
    /// Info box icon label
    /// </summary>
    public class InfoBoxIconLabel : Label
    {
        /// <inheritdoc />
        public InfoBoxIconLabel(params By[] locatorBys) : base(locatorBys)
        {
        }

        /// <inheritdoc />
        public override void Hover()
        {
            string elementName = this.GetElementName(this.GetContainer().Text);
            this.GetContainer().SeleniumHover();
            Logger.LogDebug("Hover performed on element: " + elementName);
        }
    }
}
