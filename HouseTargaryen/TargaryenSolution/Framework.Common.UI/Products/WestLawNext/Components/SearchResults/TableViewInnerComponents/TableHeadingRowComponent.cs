namespace Framework.Common.UI.Products.WestLawNext.Components.SearchResults.TableViewInnerComponents
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

	using OpenQA.Selenium;

	/// <summary>
	/// Table header
	/// </summary>
	public sealed class TableHeadingRowComponent : BaseInnerRowComponent
    {
        private Func<IWebElement, string> getHiddenTextFunc = webElement =>
            DriverExtensions.GetElement(webElement, By.XPath("./div")).GetHiddenText();

		/// <inheritdoc />
		public TableHeadingRowComponent(IWebElement rootComponentWebElement)
			: base(rootComponentWebElement)
		{
			this.ColumnTitleList = this.RowLineWebElementsList
			                           .Select(this.getHiddenTextFunc).ToList();
		}

		/// <summary>
		/// The on delete action method container.
		/// </summary>
		internal delegate void OnHeadingClickAction();

		/// <summary>
		/// Event, raised on delete method.
		/// </summary>
		internal event OnHeadingClickAction OnHeadingClick;

		/// <summary>
		/// ColumnTitleList
		/// </summary>
		public List<string> ColumnTitleList { get; }

		/// <summary>
		/// Component locator
		/// </summary>
		protected sealed override By ComponentLocator { get; } = By.XPath("//tr[contains(@class,'Heading')]");

		/// <summary>
		/// Click on title heading
		/// </summary>
		/// <exception cref="NullReferenceException">Throws if element is not visible on page.</exception>
		/// <param name="title">Heading title</param>
		public void ClickOnColumnTitle(string title)
		{
			DriverExtensions.Click(this.RowLineWebElementsList.FirstOrDefault(x => x.Text == title));
			this.OnHeadingClick?.Invoke();
		}

        /// <summary>
        /// True if desired header is displayed
        /// </summary>
        /// <param name="columnHeaderName"></param>
        /// <returns></returns>
        public bool IsColumnHeaderDisplayed(string columnHeaderName) =>
			this.RowLineWebElementsList.FirstOrDefault(header => this.getHiddenTextFunc(header) == columnHeaderName).Displayed;

        /// <summary>
        /// True if sorted by ascending
        /// </summary>
        /// <param name="columnHeaderName"></param>
        /// <returns></returns>
        public bool IsColumnSortedByAscending(string columnHeaderName) =>
            this.RowLineWebElementsList.FirstOrDefault(header => this.getHiddenTextFunc(header) == columnHeaderName).GetAttribute("class")
                .Contains("ascending");

        /// <summary>
        /// True if sorted by descending
        /// </summary>
        /// <param name="columnHeaderName"></param>
        /// <returns></returns>
        public bool IsColumnSortedByDescending(string columnHeaderName) =>
            this.RowLineWebElementsList.FirstOrDefault(header => this.getHiddenTextFunc(header) == columnHeaderName).GetAttribute("class")
                .Contains("descending");

		/// <summary>
		/// Gets 
		/// </summary>
		/// <param name="descending">Descending if true, ascending if false</param>
		/// <returns></returns>
		public string GetColumnTitleSortedBy(bool descending)
		{
			string attributeValue = descending? "descending" : "ascending";
			return this.RowLineWebElementsList.FirstOrDefault(x => x.GetAttribute("class").Contains(attributeValue))?.Text;
		}
	}
}