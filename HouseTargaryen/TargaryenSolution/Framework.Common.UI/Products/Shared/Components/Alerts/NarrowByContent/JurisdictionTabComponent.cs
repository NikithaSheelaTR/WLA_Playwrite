namespace Framework.Common.UI.Products.Shared.Components.Alerts.NarrowByContent
{
    using Framework.Common.UI.Products.Shared.Dialogs.Alerts;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Jurisdiction Tab Panel
    /// </summary>
    public class JurisdictionTabComponent : BaseTabComponent
    {
        private const string JurisdictionLctMask = "//a[contains(@id,'Jurisdiction') and text()='{0}']";
        private const string JurisdictionExpandLctMask = "//button[@class='co_facet_expand' and text()='{0}']";

        private static readonly By ContainerLocator = By.Id("keycite_alerts_lightbox");

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Jurisdiction";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Expand Jurisdiction 
        /// </summary>
        /// <param name="jurisdiction"> Jurisdiction </param>
        /// <returns>a browse page for the specified content type</returns>
        public JurisdictionTabComponent ExpandJurisdiction(string jurisdiction)
        {
            DriverExtensions.Click(By.XPath(string.Format(JurisdictionExpandLctMask, jurisdiction)));
            return this;
        }

        /// <summary>
        /// Select content type
        /// </summary>
        /// <param name="jurisdiction">
        /// Jurisdiction 
        /// </param> <param name="isSet"> True to check, false to uncheck  </param>
        /// <returns> The <see cref="NarrowByContentDialog"/>. </returns>
        public NarrowByContentDialog SetCheckboxByJurisdiction(string jurisdiction, bool isSet = true)
        {
            DriverExtensions.SetCheckbox(By.XPath(string.Format(JurisdictionLctMask, jurisdiction)), isSet);
            return new NarrowByContentDialog();
        }

    }
}
