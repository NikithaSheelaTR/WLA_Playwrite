namespace Framework.Common.UI.Products.WestLawNext.Models.Components.PatentDocument
{
	/// <summary>
	/// Classification Information section
	/// </summary>
	public class ClassificationInformation
	{
		/// <summary>
		/// International Classes(IPC 8)
		/// </summary>
		public string InternationalClassesIpc8 { get; set; }

		/// <summary>
		/// International Classes (IPC 1-7):
		/// </summary>
		public string InternationalClassesIpc1To7 { get; set; }

		/// <summary>
		/// Language
		/// </summary>
		public string Language { get; set; }
	}
}