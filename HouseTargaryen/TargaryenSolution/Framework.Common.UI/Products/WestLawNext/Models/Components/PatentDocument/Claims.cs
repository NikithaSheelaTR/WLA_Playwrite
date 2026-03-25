namespace Framework.Common.UI.Products.WestLawNext.Models.Components.PatentDocument
{
	using System.Collections.Generic;

	/// <summary>
	/// Claims
	/// </summary>
	public class Claims
	{
		/// <summary>
		/// Number of Claims
		/// </summary>
		public string NumberOfClaims { get; set; }

		/// <summary>
		/// Claims
		/// </summary>
		public List<string> ClaimList { get; set; } = new List<string>();
	}
}