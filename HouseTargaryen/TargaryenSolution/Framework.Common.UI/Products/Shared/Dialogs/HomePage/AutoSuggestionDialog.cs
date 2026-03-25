namespace Framework.Common.UI.Products.Shared.Dialogs.HomePage
{
	using Framework.Common.UI.Interfaces.Elements;
	using Framework.Common.UI.Products.Shared.Elements;
	using Framework.Common.UI.Products.Shared.Elements.Links;
	using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
	using OpenQA.Selenium;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// Auto suggestion dialog for ClientId, MatterId, etc.
	/// </summary>
	public class AutoSuggestionDialog : BaseModuleRegressionDialog
	{
		private static readonly By DialogContainerLocator = By.XPath("//div[contains(@class,'ac_results')]/ul");
		private static readonly By SuggestedLabelsLocator = By.XPath(".//li/span[1]");
		private IWebElement container;

		/// <summary>
		/// The component uses shared HTML layout,
		/// however only one instance is available per time
		/// </summary>
		public AutoSuggestionDialog()
		{
			DriverExtensions.WaitForCondition((By) => DriverExtensions.GetElements(DialogContainerLocator).Any(webEl=>webEl.Displayed), 10);
			var candidates = DriverExtensions.GetElements(DialogContainerLocator);
			this.container = candidates.FirstOrDefault(webEl => webEl.Displayed);
		}

		/// <summary>
		/// Get auto-suggestions
		/// </summary>
		/// <returns></returns>
		public IReadOnlyCollection<ILink> GetAutosuggestionOptions => new ElementsCollection<Link>(this.container, SuggestedLabelsLocator);
	}
}
