namespace Framework.Common.UI.Products.WestLawNext.Models.Components.PatentDocument
{
	using System.Collections.Generic;

	/// <summary>
	/// References Cited section
	/// </summary>
	public class ReferencesCited
	{
		/// <summary>
		/// Patents and Applications
		/// </summary>
		public List<string> PatentsAndApplications { get; set; } = new List<string>();

	}
}