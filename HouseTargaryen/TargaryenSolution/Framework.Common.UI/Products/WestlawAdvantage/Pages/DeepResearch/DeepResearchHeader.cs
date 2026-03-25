namespace Framework.Common.UI.Products.WestlawAdvantage.Components.DeepResearch
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestlawAdvantage.DropDowns.DeepResearch;
    using Framework.Common.UI.Products.WestlawEdge.DropDowns;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Deep Research Header
    /// </summary>
    public class DeepResearchHeader : BaseModuleRegressionComponent
    {
        private static readonly By HeaderContainerLocator = By.XPath("//div[contains(@class,'SingleColumnLayout-module__mainContainerHeader') or contains(@class,'Header-module__headerContainer')]");
        private static readonly By HeadingLabelLocator = By.XPath(".//*[contains(@class,'ApplicationHeader-module__heading')]");
        private static readonly By NewResearchButtonLocator = By.XPath(".//saf-button-v3[@data-testid='new-research-button']");

        /// <summary>
        /// New research button
        /// </summary>
        public IButton NewResearchButton => new Button(this.ComponentLocator, NewResearchButtonLocator);

        /// <summary>
        /// Landing page heading label
        /// </summary>
        public ILabel HeadingLabel => new Label(this.ComponentLocator, HeadingLabelLocator);

        /// <summary>
        /// Report Versions menu dropdown
        /// </summary>
        public ReportVersionsDropdown ReportVersionsMenu => new ReportVersionsDropdown(DriverExtensions.GetElement(this.ComponentLocator));

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => HeaderContainerLocator;
    }
}
