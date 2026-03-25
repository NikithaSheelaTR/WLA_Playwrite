namespace Framework.Common.UI.Products.WestLawNext.Components.SearchResults
{
	using System.Collections.Generic;

	using Framework.Common.UI.Interfaces;
	using Framework.Common.UI.Products.Shared.Components;
	using Framework.Common.UI.Products.WestLawNext.Components.SearchResults.TableViewInnerComponents;
	using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

	using OpenQA.Selenium;

	/// <summary>
	/// The Table View Component
	/// </summary>
	public class TableViewComponent : BaseModuleRegressionComponent, ICreatablePageObject
	{
		/// <summary>
		/// The Table Heading component
		/// </summary>
		public TableHeadingRowComponent TableHeadingRow;

		/// <summary>
		/// Table rows
		/// </summary>
		public List<TableRowComponent> TableRows;

		private static readonly By RowsLocator = By.XPath(".//tr[@data-guid]");

		/// <summary>
		/// Table View Component Constructor
		/// </summary>
		public TableViewComponent()
		{
			this.InitializeTableViewInnerComponents();
		}

		/// <summary>
		/// Component locator
		/// </summary>
		protected override By ComponentLocator { get; } = By.XPath("//table[contains(@class,'co_searchResult_list')]");

		private IWebElement tableViewContainerWebElement;

		private void InitializeTableViewInnerComponents()
		{
			DriverExtensions.WaitForPageLoad(60000);
			DriverExtensions.WaitForJavaScript();
			this.tableViewContainerWebElement = DriverExtensions.SafeGetElement(this.ComponentLocator);
			if (this.tableViewContainerWebElement != null)
			{
				this.TableHeadingRow = new TableHeadingRowComponent(this.tableViewContainerWebElement);
				this.TableHeadingRow.OnHeadingClick += this.InitializeTableViewInnerComponents;
				this.TableRows = new List<TableRowComponent>();
				foreach (IWebElement rowWebElement in DriverExtensions.GetElements(this.tableViewContainerWebElement, RowsLocator))
				{
					this.TableRows.Add(new TableRowComponent(rowWebElement));
				}
			}
		}
	}
}