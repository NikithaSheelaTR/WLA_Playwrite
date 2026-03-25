namespace Framework.Common.UI.Products.Shared.Items.ResultList
{
	using OpenQA.Selenium;

	/// <summary>
	/// Result list item for Trademarks
	/// </summary>
	public class TrademarksResultItem : ResultListItem
	{
		private const string ItemLctMask = ".//div[contains(@class,'co_search_detailLevel')]/b[text()='{0}']/parent::*";
		private static readonly By DesignTypeLocator = By.XPath(string.Format(ItemLctMask, "Design Type:"));
		private static readonly By StatusLocator = By.XPath(string.Format(ItemLctMask, "Status:"));
		private static readonly By FiledDateLocator = By.XPath(string.Format(ItemLctMask, "Filed Date:"));
		private static readonly By RegistrationDateLocator = By.XPath(string.Format(ItemLctMask, "Registration Date:"));
		private static readonly By InternationalClassLocator = By.XPath(string.Format(ItemLctMask, "International Class:"));
		private static readonly By OwnerLocator = By.XPath(string.Format(ItemLctMask, "Owner:"));

		/// <inheritdoc />
		public TrademarksResultItem(IWebElement containerElement)
			: base(containerElement)
		{
		}

		/// <summary>
		/// Owner
		/// </summary>
		public string DesignType => this.TryGetText(DesignTypeLocator).Replace("Design Type:", string.Empty);

		/// <summary>
		/// Owner
		/// </summary>
		public string Status => this.TryGetText(StatusLocator).Replace("Status:", string.Empty);

		/// <summary>
		/// Owner
		/// </summary>
		public string FiledDate => this.TryGetText(FiledDateLocator).Replace("Filed Date:", string.Empty);

		/// <summary>
		/// Owner
		/// </summary>
		public string RegistrationDate => this.TryGetText(RegistrationDateLocator).Replace("Registration Date:", string.Empty);

		/// <summary>
		/// Owner
		/// </summary>
		public string InternationalClass => this.TryGetText(InternationalClassLocator).Replace("International Class:", string.Empty);

		/// <summary>
		/// Owner
		/// </summary>
		public string Owner => this.TryGetText(OwnerLocator).Replace("Owner:", string.Empty);
	}
}