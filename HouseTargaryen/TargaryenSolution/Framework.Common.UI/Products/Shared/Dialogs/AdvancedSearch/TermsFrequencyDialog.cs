namespace Framework.Common.UI.Products.Shared.Dialogs.AdvancedSearch
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    /// <summary>
    /// Terms Frequency dialog
    /// </summary>
    public class TermsFrequencyDialog : BaseModuleRegressionDialog
    {
        private const string FrequencyTermDropDownLctMask =
            "//td[@class='co_termFrequency_frequency' and .//span[contains(text(),{0})]]//select";

        private const string RequireTermLctMask = "//div[@class='co_termFrequency_require']/input[contains(@value,'{0}')]";

        private static readonly By OkButtonLocator = By.Id("co_advancedSearch_termFrequencySave");

        /// <summary>
        /// Ok button
        /// </summary>
        public IButton OkButton => new Button(OkButtonLocator);

        /// <summary>
        /// Select All RequireTerm Checkbox
        /// </summary>
        public ICheckBox RequireTermCheckbox(string requiredTerm) => new CheckBox(By.XPath(string.Format(RequireTermLctMask, requiredTerm)));

        /// <summary>
        /// The apply many required term and frequency.
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="requiredTermsAndFrequency">
        /// The required terms and frequency.
        /// </param>
        /// <returns> New instance of the page </returns>
        public T ApplyManyRequiredTermAndFrequency<T>(Dictionary<string, int> requiredTermsAndFrequency)
            where T : ICreatablePageObject
        {
            requiredTermsAndFrequency.ToList().ForEach(x => this.SetRequiredTermAndFrequency(x.Key, x.Value));
            return this.OkButton.Click<T>();
        }

        /// <summary>
        /// The apply require term and frequency.
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="requiredTerm"> The require term. </param>
        /// <param name="frequency"> The frequency. </param>
        /// <returns> New instance of the page </returns>
        public T ApplyRequiredTermAndFrequency<T>(string requiredTerm, int frequency = 1) where T : ICreatablePageObject
        {
            this.SetRequiredTermAndFrequency(requiredTerm, frequency);
            return this.OkButton.Click<T>();
        }

        /// <summary>
        /// The get frequency drop-down.
        /// </summary>
        /// <param name="requiredTerm"> The required term. </param>
        /// <returns> The <see cref="SelectElement"/>. </returns>
        private SelectElement GetFrequencyDropdown(string requiredTerm) =>
            new SelectElement(DriverExtensions.GetElement(SafeXpath.BySafeXpath(FrequencyTermDropDownLctMask, requiredTerm)));

        /// <summary>
        /// The set required term and frequency.
        /// </summary>
        /// <param name="requiredTerm"> The required term. </param>
        /// <param name="frequency"> The frequency. </param>
        private void SetRequiredTermAndFrequency(string requiredTerm, int frequency)
        {
            this.RequireTermCheckbox(requiredTerm).Set(true);
            this.GetFrequencyDropdown(requiredTerm).SelectByText(frequency.ToString());
        }
    }
}