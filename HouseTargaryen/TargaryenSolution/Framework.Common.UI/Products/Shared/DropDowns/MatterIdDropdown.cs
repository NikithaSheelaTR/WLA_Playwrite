namespace Framework.Common.UI.Products.Shared.DropDowns
{
    using System;
    using System.Collections.Generic;
	using Framework.Common.UI.Products.Shared.Dialogs.HomePage;
	using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
	using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
	using OpenQA.Selenium;

    /// <summary>
    /// Matter Id drop down
    /// </summary>
    public class MatterIdDropdown : BaseModuleRegressionCustomDropdown<string>
    {
        private static readonly By MatterIdTextBoxLocator = By.XPath("//input[contains(@class,'co_matterIDTextbox') or @id='co_matterIDTextbox']");

        /// <summary>
        /// Return Selected Matter Id
        /// </summary>
        public override string SelectedOption => DriverExtensions.GetText(MatterIdTextBoxLocator);

        /// <summary>
        /// Is not implemented as not applicable for this drop down
        /// ToDo should get options from dropdown
        /// </summary>
        protected override IEnumerable<string> OptionsFromExpandedDropdown
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Is not implemented as not applicable for this drop down
        /// ToDo dropdown element
        /// </summary>
        protected override IWebElement Dropdown
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// AutoSuggestionDialog
        /// </summary>
        /// <returns></returns>
        public AutoSuggestionDialog AutoSuggestionDialog => new AutoSuggestionDialog();

        /// <summary>
        /// Type Matter Id slowly
        /// </summary>
        /// <param name="matterId"></param>
        public void TypeMatterId(string matterId)
        {
            DriverExtensions.WaitForElement(MatterIdTextBoxLocator).Clear();
            DriverExtensions.WaitForElement(MatterIdTextBoxLocator).SendKeysSlow(matterId);
        }

        /// <summary>
        /// Is not implemented as not applicable for this drop down
        /// ToDo should verify selected option
        /// </summary>
        /// <param name="option">Option to verify</param>
        /// <returns> NotImplementedException </returns>
        public override bool IsSelected(string option)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Is not implemented as not applicable for this drop down
        /// ToDo should select the required option from dropdown
        /// </summary>
        /// <param name="option">Matter id</param>
        protected override void SelectOptionFromExpandedDropdown(string option)
        {
            throw new NotImplementedException();
        }
    }
}
