namespace Framework.Common.UI.Products.Shared.Components.Preferences
{
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Test Tab Component
    /// </summary>
    public class TestTabComponent : BaseTabComponent
    {
        private static readonly By DisplayCountOver10KLocator = By.Id("coid_userSettingsShowFoundDocumenCountOption");

        private static readonly By ContainerLocator = By.Id("coid_userSettingsTab9Link");

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Test";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Returns true if the specified element on the test tab is not checked 
        /// </summary>
        /// <returns>false if selected, true otherwise</returns>
        public bool IsTestTabDisplayCountOver10KSelected() => DriverExtensions.GetElement(DisplayCountOver10KLocator).Selected;

        /// <summary>
        /// Sets the specified checkbox option on the test tab to the specified value.
        /// </summary>
        /// <param name="setTo"> What to set the checkbox to. True for checked, false for unchecked. </param>
        /// <returns>
        /// The <see cref="TestTabComponent"/>TestTabComponent </returns>
        public TestTabComponent SetTestTabDisplayCountOver10KCheckbox(bool setTo)
        {
            DriverExtensions.SetCheckbox(setTo, DisplayCountOver10KLocator);
            return this;
        }
    }
}