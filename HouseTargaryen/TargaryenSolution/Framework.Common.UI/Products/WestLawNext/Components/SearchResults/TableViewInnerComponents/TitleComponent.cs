namespace Framework.Common.UI.Products.WestLawNext.Components.SearchResults.TableViewInnerComponents
{
	using Framework.Common.UI.Interfaces;
	using Framework.Common.UI.Products.Shared.Components;
	using Framework.Common.UI.Products.Shared.Enums.Document;
	using Framework.Common.UI.Products.Shared.Models.EnumProperties;
	using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
	using Framework.Core.Utils.Enums;

	using OpenQA.Selenium;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Title Component
    /// </summary>
    public sealed class TitleComponent : BaseModuleRegressionComponent
	{
		private static readonly By CheckboxLocator = By.XPath(".//input");

		private static readonly By TitleLinkLocator = By.XPath(".//a[@docguid]");

        private static readonly By DocIconsLocator = By.XPath(".//ul[@class='co_documentIcons']//li");

        /// <inheritdoc />
        public TitleComponent(IWebElement parentWebElement)
		{
			this.ContainerWebElement = DriverExtensions.GetElement(parentWebElement, this.ComponentLocator);
		}

		/// <inheritdoc />
		protected override By ComponentLocator { get; } = By.XPath(".//div[input]");

		/// <summary>
		/// Gets the DocumentIcon enumeration to WebElementInfo map.
		/// </summary>
		private EnumPropertyMapper<DocumentIcon, WebElementInfo> DocIconsMap { get; } = EnumPropertyModelCache.GetMap<DocumentIcon, WebElementInfo>();

		/// <summary>
		/// Container WebElement
		/// </summary>
		private IWebElement ContainerWebElement { get; }

		/// <summary>
		/// Get Link Title text
		/// </summary>
		/// <returns></returns>
		public string GetLinkTitle() => DriverExtensions.GetTextSafe(this.ContainerWebElement, TitleLinkLocator);

		/// <summary>
		/// Retrieve Item Guid
		/// </summary>
		/// <returns></returns>
		public string GetItemGuid() => DriverExtensions.GetAttribute("docguid", this.ContainerWebElement, TitleLinkLocator);

		/// <summary>
		/// Is checkbox Selected
		/// </summary>
		/// <returns>
		/// The <see cref="bool"/>True if selected</returns>
		public bool IsCheckboxSelected() => DriverExtensions.IsCheckboxSelected(this.ContainerWebElement, CheckboxLocator);

		/// <summary>
		/// Set checkbox state
		/// </summary>
		/// <param name="selected">Desired state</param>
		public void SetCheckBox(bool selected = true) => DriverExtensions.SetCheckbox(selected, this.ContainerWebElement, CheckboxLocator);


        /// <summary>
        /// Returns document icons
        /// </summary>
        /// <returns></returns>
        public List<DocumentIcon> GetDocumentIconsList()
        {
            IEnumerable<string> pathsToSrc = DriverExtensions
                .GetElements(this.ContainerWebElement, DocIconsLocator)
                .Where(elem => elem.Displayed)
                .Select(elem => elem.GetAttribute("class"));

            return pathsToSrc.Any()
                      ? this.DocIconsMap.Where(
                                pair => pathsToSrc.Any(path => pair.Value.LocatorString.Contains(path)))
                            .Select(pair => pair.Key).ToList()
                      : new List<DocumentIcon>();
        }

        /// <summary>
        /// Check if Document Status Icon Displayed
        /// </summary>
        /// <param name="iconType">
        /// Document icon Type.
        /// </param> <returns> true if icon present in the Document </returns>
        public bool IsStatusIconDisplayed(DocumentIcon iconType) => DriverExtensions.IsDisplayed(
			this.ContainerWebElement,
			By.XPath(this.DocIconsMap[iconType].LocatorString));

		/// <summary>
		/// Click on document link
		/// </summary>
		/// <typeparam name="T">Page type</typeparam>
		/// <returns> The Page Object</returns>
		public T ClickOnTitleLink<T>()
			where T : ICreatablePageObject
		{
			DriverExtensions.Click(this.ContainerWebElement, TitleLinkLocator);
			return DriverExtensions.CreatePageInstance<T>();
		}
	}
}