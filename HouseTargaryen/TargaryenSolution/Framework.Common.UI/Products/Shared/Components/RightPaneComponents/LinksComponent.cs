namespace Framework.Common.UI.Products.Shared.Components.RightPaneComponents
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Components;
    using Framework.Common.UI.Products.Shared.Enums.Widgets;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Component With Links:
    ///  - For KPMG/PwC Pages -- Find AND KeyCite A Tax Document
    ///                       -- PwC Customized Libraries
    ///                       -- KPMG Customized Libraries
    /// </summary>
    public abstract class LinksComponent : BaseModuleRegressionComponent, ILinksComponent
    {
        private const string ContainerLocator =
            "//div[contains(@class,'co_genericBox')][contains(./h2/span/text(), '{0}')]";

        /// <summary>
        /// Links Component Name / Title
        /// </summary>
        protected string ComponentName { get; set; }

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => By.XPath(string.Format(ContainerLocator, this.ComponentName));

        /// <summary>
        /// Initializes a new instance of the <see cref="LinksComponent"/> class. 
        /// </summary>
        /// <param name="linkName"> Links component name  </param>
        protected LinksComponent(LinksNames linkName)
        {
            this.ComponentName = linkName.GetEnumTextValue();
        }

        /// <summary>
        /// Click Link By Name
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="linkText"> Link Text </param>
        /// <returns> New instance of the page </returns>
        public T ClickLinkByName<T>(string linkText) where T : ICreatablePageObject
        {
            this.GetLinksElements().First(l => l.Text.Equals(linkText)).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Get Links Names
        /// </summary>
        /// <returns> List with links names </returns>
        public List<string> GetLinks() => this.GetLinksElements().Select(el => el.Text).ToList();

        /// <summary>
        /// Get Links Component title
        /// </summary>
        /// <returns> Links Component Title as string </returns>
        public string GetTitle()
            => DriverExtensions.GetElement(this.ComponentLocator, By.CssSelector("h3>span")).Text;

        /// <summary>
        /// Check is component displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public override bool IsDisplayed() => DriverExtensions.IsDisplayed(this.ComponentLocator, 5);

        private IList<IWebElement> GetLinksElements()
            => DriverExtensions.GetElements(this.ComponentLocator, By.TagName("a"));
    }
}