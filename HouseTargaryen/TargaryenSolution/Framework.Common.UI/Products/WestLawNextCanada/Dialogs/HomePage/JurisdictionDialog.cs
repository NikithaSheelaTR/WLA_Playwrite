namespace Framework.Common.UI.Products.WestLawNextCanada.Dialogs.HomePage
{
    using Framework.Common.UI.Products.Shared.Dialogs.Alerts;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNextCanada.Enums;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;
    using System.Linq;

    /// <summary>
    /// Jurisdiction Dialog
    /// </summary>
    public class JurisdictionDialog : JurisdictionOptionsDialog
    {
        private const string JurisdictionCheckboxLocator = "//input[@title='{0}']";

        private EnumPropertyMapper<JurisdictionOptions, WebElementInfo> jurisdictionMapper;

        /// <summary>
        /// Browse Tab Tabs Mapper
        /// </summary>
        protected EnumPropertyMapper<JurisdictionOptions, WebElementInfo> JurisdictionMapper =>
           this.jurisdictionMapper = this.jurisdictionMapper
                                  ?? EnumPropertyModelCache
                                      .GetMap<JurisdictionOptions, WebElementInfo>(
                                          string.Empty,
                                          @"Resources/EnumPropertyMaps/WestlawNextCanada");

        /// <summary>
        /// Checks if given Jurisdiction option is selected or not
        /// </summary>
        /// <param name="options">Jurisdiction option to check is selected</param>
        /// <returns>True if selected otherwise false</returns>
        public bool AreJurisdictionOptionsSelected(params JurisdictionOptions[] options) =>
            options.All(option => DriverExtensions.IsCheckboxSelected(
                By.XPath(string.Format(JurisdictionCheckboxLocator, this.JurisdictionMapper[option].Text))));

        /// <summary>
        /// Selects the given Jurisdiction option
        /// </summary>
        /// <param name="options">Jurisdiction option to select</param>
        public void SelectJurisdictionOptions(params JurisdictionOptions[] options)
        {
            foreach (var option in options)
            {
                DriverExtensions.GetElement(
                    By.XPath(string.Format(JurisdictionCheckboxLocator, this.JurisdictionMapper[option].Text))).Click();
            }
        }
    }
}