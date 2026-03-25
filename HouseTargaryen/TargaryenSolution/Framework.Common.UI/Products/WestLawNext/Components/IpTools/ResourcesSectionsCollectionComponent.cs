namespace Framework.Common.UI.Products.WestLawNext.Components.IpTools
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.WestLawNext.Components.BrowsePage;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Contains collection of Intellectual Property Resources sections
    /// Administrative, Secondary Sources, Litigation, Legislative and Regulatory
    /// </summary>
    public sealed class ResourcesSectionsCollectionComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[contains(@class,'co_tabShow')]//div[@class = 'co_browsePageSectionWidget']");
        private static readonly By TitleLocator = By.XPath("//div[contains(@class,'co_tabShow')]//div[@class='co_browsePageSectionWidget']/h1");

        /// <summary>
        /// component locator 
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Get Resources section title text
        /// </summary>
        /// <returns></returns>
        public string GetTitle()
        {
            return DriverExtensions.GetText(TitleLocator);
        }

        /// <summary>
        /// Administrative Resources section
        /// </summary>
        public ResourcesCheckboxSectionComponent AdministrativeResources { get; } = new ResourcesCheckboxSectionComponent("Administrative");

        /// <summary>
        /// Secondary Sources Resources section
        /// </summary>
        public ResourcesCheckboxSectionComponent SecondarySourcesResources { get; } = new ResourcesCheckboxSectionComponent("Secondary Sources");

        /// <summary>
        /// Litigation Resources section
        /// </summary>
        public ResourcesCheckboxSectionComponent LitigationResources { get; } = new ResourcesCheckboxSectionComponent("Litigation");

        /// <summary>
        /// Legislative And Regulatory Resources section 
        /// </summary>
        public ResourcesCheckboxSectionComponent LegislativeAndRegulatoryResources { get; } = new ResourcesCheckboxSectionComponent("Legislative & Regulatory");
    }
}
