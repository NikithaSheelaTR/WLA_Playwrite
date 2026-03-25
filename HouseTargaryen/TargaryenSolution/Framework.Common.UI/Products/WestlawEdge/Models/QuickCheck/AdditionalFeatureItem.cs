namespace Framework.Common.UI.Products.WestlawEdge.Models.QuickCheck
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// AdditionalFeatureModel
    /// </summary>
    public class AdditionalFeatureItem : BaseItem
    {
        /// <summary>
        /// AdditionalFeatureModel constructor
        /// </summary>
        /// <param name="containerLocator"></param>
        public AdditionalFeatureItem(By containerLocator)
            : base(DriverExtensions.GetElement(containerLocator))
        {
        }

        /// <summary>
        /// AdditionalFeatureModel
        /// </summary>
        /// <param name="container"></param>
        public AdditionalFeatureItem(IWebElement container)
            : base(container)
        {
        }

        /// <summary>
        /// Get model text
        /// </summary>
        public string Text => this.Container.Text;

        /// <summary>
        /// CheckBox for Item
        /// </summary>
        public ICheckBox CheckBox => new CheckBox(this.Container, By.XPath(".//input[@type='checkbox']"));
    }
}
