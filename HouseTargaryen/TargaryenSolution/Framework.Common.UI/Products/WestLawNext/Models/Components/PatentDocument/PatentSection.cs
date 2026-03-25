namespace Framework.Common.UI.Products.WestLawNext.Models.Components.PatentDocument
{
	using System.Collections.Generic;

	/// <summary>
	/// Patent Section
	/// </summary>
	public class PatentSection
	{
		/// <summary>
		/// Title
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Published Application Number
		/// </summary>
		public string PublishedApplicationNumber { get; set; }

	    /// <summary>
	    /// Published Application Date
	    /// </summary>
	    public string PublishedApplicationDate { get; set; }

        /// <summary>
        /// Application Number
        /// </summary>
        public string ApplicationNumber { get; set; }

		/// <summary>
		/// Application Date
		/// </summary>
		public string ApplicationDate { get; set; }

		/// <summary>
		/// Inventors
		/// </summary>
		public List<string> Inventors { get; set; } = new List<string>();

        /// <summary>
        /// Examiners
        /// </summary>
        public List<string> Examiners { get; set; } = new List<string>();

        /// <summary>
        /// Primary Examiner
        /// </summary>
        public string PrimaryExaminer { get; set; }

        /// <summary>
        /// Assignees
        /// </summary>
        public List<string> Assignees { get; set; } = new List<string>();

		/// <summary>
		/// AttorneyOrAgents
		/// </summary>
		public List<string> AttorneyOrAgents { get; set; } = new List<string>();

		/// <summary>
		/// GrantedPatentNumber
		/// </summary>
		public string GrantedPatentNumber { get; set; }

		/// <summary>
		/// GrantedDate
		/// </summary>
		public string GrantedDate { get; set; }
	}
}