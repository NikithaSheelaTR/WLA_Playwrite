namespace Framework.Common.UI.Products.WestLawNext.Models.BusinessObjects
{
	using Framework.Common.UI.Products.WestLawNext.Models.Components.PatentDocument;

	/// <summary>
	/// Patent Document Model
	/// </summary>
	public class PatentDocumentModel
	{
		/// <summary>
		/// Title
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Citation
		/// </summary>
		public string Citation { get; set; }

		/// <summary>
		/// Abstract
		/// </summary>
		public string Abstract { get; set; }

		/// <summary>
		/// Patent Section
		/// </summary>
		public PatentSection PatentSection { get; set; }

		/// <summary>
		/// Priority Information Section
		/// </summary>
		public PriorityInformationSection PriorityInformationSection { get; set; }

		/// <summary>
		/// Classification Information
		/// </summary>
		public ClassificationInformation ClassificationInformation { get; set; }

		/// <summary>
		/// Claims section
		/// </summary>
		public Claims Claims { get; set; }

		/// <summary>
		/// Specification
		/// </summary>
		public Specification Specification { get; set; }

		/// <summary>
		/// References Cited
		/// </summary>
		public ReferencesCited ReferencesCited { get; set; }
	}
}