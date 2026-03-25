using Framework.Common.UI.Interfaces.Elements;
using Framework.Common.UI.Products.Shared.Components;
using Framework.Common.UI.Products.Shared.Elements;
using Framework.Common.UI.Products.Shared.Models.EnumProperties;
using Framework.Common.UI.Products.WestlawEdgePremium.Enums.LitigationAnalytics;
using Framework.Core.Utils.Enums;
using OpenQA.Selenium;

namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.OpportunityFinderComponents
{
    /// <summary>
    /// OpportunityFinderLegalActivityComponent
    /// </summary>
    public class OpportunityFinderLegalActivityComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@class='la-OpFinder-contentColumns']");
        private static string IncludeActivityWithinTheLastLocator = "//input[@id = '{0}']";
        private static string LegalActivityTypeLocator = "//input[@type = 'radio' and @id = '{0}']";

        /// <summary>
        /// Gets the Opportunity Finder Include Activity Within The Last to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<OpportunityFinderIncludeActivityWithinTheLast, WebElementInfo> TabsMap =>
            EnumPropertyModelCache.GetMap<OpportunityFinderIncludeActivityWithinTheLast, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/WestlawEdgePremium/LitigationAnalytics");

        /// <summary>
        /// Include Activity Within The Last
        /// </summary>
        public IRadiobutton IncludeActivityWithinTheLastRadiobutton(OpportunityFinderIncludeActivityWithinTheLast parameter) => new Radiobutton(By.XPath(string.Format(IncludeActivityWithinTheLastLocator, this.TabsMap[parameter].Id)));

        /// <summary>
        /// Gets the Opportunity Finder Legal Activity Type to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<OpportunityFinderChooseOneLegalActivityType, WebElementInfo> LegalActivityTypeMap =>
            EnumPropertyModelCache.GetMap<OpportunityFinderChooseOneLegalActivityType, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/WestlawEdgePremium/LitigationAnalytics");

        /// <summary>
        /// Legal Activity Type
        /// </summary>
        public IRadiobutton LegalActivityTypeRadiobutton(OpportunityFinderChooseOneLegalActivityType parameter) =>
            new Radiobutton(By.XPath(string.Format(LegalActivityTypeLocator, this.LegalActivityTypeMap[parameter].Id)));

        /// <summary>
        /// ComponentLocator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}