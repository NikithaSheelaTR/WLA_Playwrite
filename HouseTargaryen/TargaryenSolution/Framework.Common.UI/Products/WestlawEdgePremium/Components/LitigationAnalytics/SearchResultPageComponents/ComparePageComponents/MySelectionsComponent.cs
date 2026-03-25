namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.SearchResultPageComponents.ComparePageComponents
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// MySelectionsComponent
    /// </summary>
    public class MySelectionsComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.Id("my-selections");

        private static readonly By CreateReportButtonLocator = By.XPath(".//button[contains(@class, 'Button-primary')]");
        private static readonly By ClearAllSelectionsLocator = By.XPath(".//button[contains(@class, 'Button-secondary')]");
        private static readonly By MyLawFirmsSelectionItemsLocator = By.XPath("//span[@class ='la-selections-companyName']");
        private static readonly By SelectTwoFirmsMessageLocator = By.XPath(".//h3[@class = 'la-selections-zeroText-bold']");

        /// <summary>
        /// CreateReportButton
        /// </summary>
        public IButton CreateReportButton => new Button(this.ComponentLocator, CreateReportButtonLocator);

        /// <summary>
        /// ClearAllSelectionsButton
        /// </summary>
        public IButton ClearAllSelectionsButton => new Button(this.ComponentLocator, ClearAllSelectionsLocator);

        /// <summary>
        /// SelectTwoFirmsMessage
        /// </summary>
        public ITextbox SelectTwoFirmsMessage => new Textbox(this.ComponentLocator, SelectTwoFirmsMessageLocator);

        /// <summary>
        /// Law Firms selection items
        /// </summary>
        public List<string> SelectedCompanyNames => DriverExtensions.GetElements(ComponentLocator, MyLawFirmsSelectionItemsLocator).Select(element => element.Text).ToList();

        /// <summary>
        /// ComponentLocator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}