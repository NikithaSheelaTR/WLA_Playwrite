namespace Framework.Common.UI.Products.WestLawNext.Models.Components.PatentDocument
{
	using System.Collections.Generic;

	/// <summary>
	/// Priority Information Section
	/// </summary>
	public class PriorityInformationSection
	{
		/// <summary>
		/// PCT Applications
		/// </summary>
		public string PctApplications { get; set; }

		/// <summary>
		/// PCT Publications
		/// </summary>
		public string PctPublications { get; set; }

		/// <summary>
		/// Priority Publications
		/// </summary>
		public List<string> PriorityPublications { get; set; } = new List<string>();

		/// <summary>
		/// Erliest Priority Date
		/// </summary>
		public string ErliestPriorityDate { get; set; }
	}
}