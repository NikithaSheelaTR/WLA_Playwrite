namespace Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.CoCounsel
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;
    using System.Linq;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.Header;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;

    /// <summary>
    /// CoCounsel Claims Explorer jurisdiction dialog
    /// </summary>
    public class CoCounselClaimsExplorerJurisdictionDialog : EdgeJurisdictionOptionsDialog
    {
        private static readonly By ContainerLocator = By.XPath("//*[@id='jurisdictionDialog']");

        private static readonly By SaveButtonLocator = By.XPath(".//*[@data-testid='jurisdiction-dialog-confirm']");

        private static readonly By JurisdictionCheckboxesLocator = By.XPath(".//saf-checkbox");

        private const string JurisdictionSelectionLctMask = ".//saf-checkbox[@current-value='{0}']";

        private const string ContentTypeCheckboxScript = "return(arguments[0].shadowRoot.querySelector('input[id=control]'));";

        private EnumPropertyMapper<Jurisdiction, JurisdictionInfo> jurisdictionsMap;

        /// <summary>
        /// Gets the jurisdiction enumeration to JurisdictionInfo map.
        /// </summary>
        private EnumPropertyMapper<Jurisdiction, JurisdictionInfo> JurisdictionsMap
            => this.jurisdictionsMap = this.jurisdictionsMap ?? EnumPropertyModelCache.GetMap<Jurisdiction, JurisdictionInfo>();

        /// <summary>
        /// Save button
        /// </summary>
        public new IButton SaveButton => new Button(this.ComponentLocator, SaveButtonLocator);

        /// <summary>
        /// Clears all selected checkboxes
        /// </summary>
        public new void ClearAllOtherJurisdictions()
        {
            foreach (var checkbox in DriverExtensions.GetElements(this.ComponentLocator, JurisdictionCheckboxesLocator).Where(item => item.GetAttribute("current-checked").Equals("true")).ToList())
            {
                var jurisdictionCheckbox = (IWebElement)DriverExtensions.ExecuteScript(ContentTypeCheckboxScript, checkbox);
                jurisdictionCheckbox.Click();
            }
        }
            
        /// <summary>
        /// Selects the jurisdictions
        /// Important: not more than 3 jurisdiction
        /// </summary>
        /// <param name="clearAllOtherJurisdictions">
        /// True to clear the other jurisdictions other than what is passed in, false to leave them there
        /// </param>
        /// <param name="jurisdictions">List of jurisdictions to select</param>
        /// <returns>The <see cref="CoCounselClaimsExplorerJurisdictionDialog"/>.</returns>
        public new CoCounselClaimsExplorerJurisdictionDialog SelectJurisdictions(bool clearAllOtherJurisdictions, params Jurisdiction[] jurisdictions)
        {
            if (clearAllOtherJurisdictions)
            {
                this.ClearAllOtherJurisdictions();
            }

            foreach (Jurisdiction jurisdiction in jurisdictions.ToList())
            {
                var jurisdictionElement = DriverExtensions.GetElement(this.ComponentLocator, By.XPath(string.Format(JurisdictionSelectionLctMask, this.JurisdictionsMap[jurisdiction].JurisdictionName))); 
                var jurisdictionCheckbox = (IWebElement)DriverExtensions.ExecuteScript(ContentTypeCheckboxScript, jurisdictionElement);
                jurisdictionCheckbox.Click();
            }

            return this;
        }

        /// <summary>
        /// Component locator
        /// </summary>
        protected By ComponentLocator => ContainerLocator;
    }
}
