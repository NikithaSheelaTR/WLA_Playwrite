namespace Framework.Common.UI.Products.WestlawEdge.Components.Document
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Form Families Tab Component
    /// </summary>
    public class FormFamiliesTabComponent : BaseTabComponent
    {
        private static readonly By ContainerLocator = By.Id("panel_formFamiliesTabId");

        private static readonly By FormBuilderComponentLocator = By.XPath(".//a[text()='Build with Form Builder']");

        /// <summary>
        /// Tab Name
        /// </summary>
        protected override string TabName => "Form families";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Get Form Families Text
        /// </summary>
        /// <returns>links text</returns>
        public string GetFormFamiliesText() => DriverExtensions.GetElement(this.ComponentLocator).Text;

        /// <summary>
        /// Click Form Families Link
        /// </summary>
        /// <typeparam name="T"> T page </typeparam>
        /// <param name="linkName"> link Name </param>
        /// <returns> page instance </returns>
        public T ClickFormFamiliesLink<T>(string linkName) where T : ICreatablePageObject
        {
            DriverExtensions.Click(By.LinkText(linkName));
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Verifies that the form builder component is displayed in the Form families tab.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if form builder component is displayed in the Form families tab. </returns>
        public bool IsFormBuilderComponentDisplayed() => DriverExtensions.IsDisplayed(this.ComponentLocator, FormBuilderComponentLocator);
    }
}
