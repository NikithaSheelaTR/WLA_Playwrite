namespace Framework.Common.UI.Products.Shared.Dialogs.HomePage
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Enums.HomePage;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Dialog that pops up for customizing home page
    /// </summary>
    public class CustomizeHomepageDialog : BaseModuleRegressionDialog
    {
        private const string CustomizationCheckboxLctMask =
            "id('coid_homePersonalizationList')//input[@type = 'checkbox' and ../label[text()=\"{0}\"]]";

        private static readonly By CustomizationCheckboxesLocator =
            By.XPath("id('coid_homePersonalizationList')//input[@type = 'checkbox']");

        private static readonly By SaveButtonLocator = By.Id("coid_homePersonalizationSave");

        private EnumPropertyMapper<HomePageCustomization, WebElementInfo> homePageCustomizationMap;

        /// <summary>
        /// Gets the EnumType enumeration to Framework.Common.UI.Products.Shared.Models.EnumProperties.WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<HomePageCustomization, WebElementInfo> HomePageCustomizationMap
            =>
                this.homePageCustomizationMap =
                    this.homePageCustomizationMap
                    ?? EnumPropertyModelCache.GetMap<HomePageCustomization, WebElementInfo>();

        /// <summary>
        ///  Clicks the save button and returns user to sign on page
        /// </summary>
        /// <typeparam name="T">class for object of type Base page</typeparam>
        /// <returns>object of type T</returns>
        public T ClickSaveButton<T>() where T : ICreatablePageObject => this.ClickElement<T>(SaveButtonLocator);

        /// <summary>
        /// Sets specified Customization Checkboxes
        /// </summary>
        /// <param name="customizationCheckboxes">The customization checkboxes</param>
        /// <param name="setTo">What to set the checkbox to</param>
        public void SetCustomizationCheckboxes(bool setTo, params HomePageCustomization[] customizationCheckboxes)
        {
            IEnumerable<IWebElement> checkboxesToSelect =
                customizationCheckboxes.Select(
                    enumItem =>
                        DriverExtensions.GetElement(
                            By.XPath(
                                string.Format(
                                    CustomizationCheckboxLctMask,
                                    this.HomePageCustomizationMap[enumItem].Text))));

            if (setTo)
            {
                checkboxesToSelect.Intersect(
                    DriverExtensions.GetElements(CustomizationCheckboxesLocator)
                                    .Where(x => !x.Selected)).ToList().ForEach(elem => elem.Click());
            }
            else
            {
                checkboxesToSelect.Where(x => x.Selected).ToList().ForEach(elem => elem.Click());
            }
        }
    }
}