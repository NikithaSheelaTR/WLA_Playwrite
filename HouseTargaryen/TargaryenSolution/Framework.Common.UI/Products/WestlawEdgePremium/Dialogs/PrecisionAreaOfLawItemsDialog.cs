namespace Framework.Common.UI.Products.WestlawEdgePremium.Dialogs
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using OpenQA.Selenium;
    using System.Collections.Generic;

    /// <summary>
    /// Area of law items dialog
    /// </summary>
    public class PrecisionAreaOfLawItemsDialog : BaseModuleRegressionDialog
    {
        private static readonly By DialogContainerLocator = By.XPath("//*[@class='a11yDropdown-menu']");
        private static readonly By CheckboxLocator = By.XPath(".//input[@type='checkbox'] | .//li[contains(@class,'a11yDropdown-item')]");
        private static readonly By AntitrustCheckboxLocator = By.XPath(".//input[@value='Antitrust'] | .//*[text()='Antitrust']/parent::li[not(contains(@role, 'menuitem'))]");
        private static readonly By CommercialLawCheckboxLocator = By.XPath(".//input[@value='Commercial Law'] | .//*[text()='Commercial Law']/parent::li[not(contains(@role, 'menuitem'))]");
        private static readonly By FederalCivilProcedureCheckboxLocator = By.XPath(".//input[@value='Federal Civil Procedure'] | .//*[text()='Federal Civil Procedure']/parent::li[not(contains(@role, 'menuitem'))]");

        /// <summary>
        /// Area of law checkboxes
        /// </summary>
        public IReadOnlyCollection<ICheckBox> AreaOfLawCheckboxes => new ElementsCollection<CheckBox>(DialogContainerLocator, CheckboxLocator);

        /// <summary>
        /// Antitrust checkbox
        /// </summary>
        public ICheckBox AntitrustCheckBox => new CheckBox(DialogContainerLocator, AntitrustCheckboxLocator);

        /// <summary>
        /// Commercial Law checkbox
        /// </summary>
        public ICheckBox CommercialLawCheckBox => new CheckBox(DialogContainerLocator, CommercialLawCheckboxLocator);

        /// <summary>
        /// Federal Civil Procedure checkbox
        /// </summary>
        public ICheckBox FederalCivilProcedureCheckBox => new CheckBox(DialogContainerLocator, FederalCivilProcedureCheckboxLocator);
    }
}
