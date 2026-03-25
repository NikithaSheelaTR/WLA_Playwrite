namespace Framework.Common.UI.Products.TaxnetPro.Components.Toolbar
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Enums.Toolbars;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Taxnet Pro TermNavigationComponent inside Toolbar
    /// </summary>
    public class TaxnetProTermNavigationComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.Id("co_docToolbarTermNavigation");
        private static readonly By OptionLocator = By.XPath("//div[@class='co_dropdownBoxContentRight']/ul/li[not(contains(@class,'co_hideState'))]/button[not(contains(@class,'co_hideState'))]");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Term navigation options
        /// </summary>
        public List<SearchTermNavigationOption> Options
        {
            get
            {
                List<string> elementsClasses = DriverExtensions.GetElements(OptionLocator)
                                                               .Select(e => e.GetAttribute("class").Replace("co_selected", "").Trim()).ToList();

                return elementsClasses.Select(x => x.GetEnumValueByPropertyModel<SearchTermNavigationOption, WebElementInfo>(el => el.ClassName)).ToList();
            }
        }
    }
}