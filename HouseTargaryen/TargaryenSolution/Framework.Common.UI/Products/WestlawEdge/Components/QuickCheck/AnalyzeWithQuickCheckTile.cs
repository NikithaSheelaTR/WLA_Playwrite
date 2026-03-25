namespace Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// analyze brief tile component
    /// </summary>
    public class AnalyzeWithQuickCheckTile : BaseItem
    {
        private static readonly By CheckboxLocator = By.XPath(".//input");
        private static readonly By OptionTextLocator = By.XPath(".//h4");

        /// <summary>
        /// CheckYourWorkTile
        /// </summary>
        /// <param name="containerLocator"></param>
        public AnalyzeWithQuickCheckTile(By containerLocator)
            : base(DriverExtensions.GetElement(containerLocator))
        {
        }

        /// <summary>
        /// CheckBox
        /// </summary>
        public ICheckBox CheckBox => new CheckBox(this.Container, CheckboxLocator);

        /// <summary>
        /// option text
        /// </summary>
        public ILabel OptionNameLabel => new Label(this.Container, OptionTextLocator);
    }
}
