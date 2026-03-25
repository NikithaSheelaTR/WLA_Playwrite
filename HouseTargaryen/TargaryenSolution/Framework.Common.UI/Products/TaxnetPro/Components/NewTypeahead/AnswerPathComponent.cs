namespace Framework.Common.UI.Products.TaxnetPro.Components.NewTypeahead
{
    using Framework.Common.UI.Products.TaxnetPro.Enums.NewTypeahead;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Answer Path Component
    /// </summary>
    public class AnswerPathComponent : ContentTypeDetailsBaseComponent
    {
        /// <summary>
        /// Container Locator
        /// </summary>
        private static readonly By ContainerLocator = By.Id("typeAheadContentTypesDiv");

        private const string ButtonLocator = "//button[@id='{0}' or text()='{0}']";

        /// <summary>
        /// Default constructor
        /// </summary>
        public AnswerPathComponent()
        {
            
        }

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Is More link displayed
        /// </summary>
        /// <param name="tabs">
        /// The category
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool IsMoreLinkDisplayed(TNPNewTypeaheadContentType tabs)
            => DriverExtensions.IsDisplayed(By.XPath(string.Format(ButtonLocator, this.TabsMap[tabs].Id)));

        /// <summary>
        /// Is Tab disabled
        /// </summary>
        /// <param name="tabs">
        /// The category
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsTabDisabled(TNPNewTypeaheadContentType tabs)
            => DriverExtensions.GetElement(By.XPath(string.Format(ButtonLocator, this.TabsMap[tabs].Text))).GetAttribute("disabled") != null;

        /// <summary>
        /// Click on More Link in category
        /// </summary>
        /// <param name="tabs">
        /// The category.
        /// </param>
        public override void ClickMoreLink(TNPNewTypeaheadContentType tabs)
            => DriverExtensions.GetElement(By.XPath(string.Format(ButtonLocator, this.TabsMap[tabs].Id))).Click();
    }
}
