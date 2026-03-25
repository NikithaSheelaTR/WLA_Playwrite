namespace Framework.Common.UI.Products.Shared.Models.CustomPages
{
    /// <summary>
    /// Set of options for sharing Custom page
    /// </summary>
    public struct CustomPageSharingOptions
    {
        private readonly bool makeStartPage;

        private readonly bool makeNonBillableZone;

        private readonly bool makeELibrary;

        private readonly string zoneName;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomPageSharingOptions"/> struct.
        /// </summary>
        /// <param name="makeStartPage">
        /// The make start page.
        /// </param>
        /// <param name="makeNonBillableZone">
        /// The make non billable zone.
        /// </param>
        /// <param name="makeELibrary">
        /// The make e library.
        /// </param>
        /// <param name="zoneName">
        /// The zone name.
        /// </param>
        public CustomPageSharingOptions(
            bool makeStartPage,
            bool makeNonBillableZone,
            bool makeELibrary,
            string zoneName)
        {
            this.makeStartPage = makeStartPage;
            this.makeNonBillableZone = makeNonBillableZone;
            this.makeELibrary = makeELibrary;
            this.zoneName = zoneName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomPageSharingOptions"/> struct.
        /// </summary>
        /// <param name="makeStartPage">
        /// The make start page.
        /// </param>
        /// <param name="makeNonBillableZone">
        /// The make non billable zone.
        /// </param>
        /// <param name="makeELibrary">
        /// The make e library.
        /// </param>
        public CustomPageSharingOptions(bool makeStartPage, bool makeNonBillableZone, bool makeELibrary)
            : this(makeStartPage, makeNonBillableZone, makeELibrary, null)
        {
        }

        /// <summary>
        /// Gets a value indicating whether make start page.
        /// </summary>
        public bool MakeStartPage
        {
            get
            {
                return this.makeStartPage;
            }
        }

        /// <summary>
        /// Make NonBillable Zone option
        /// </summary> 
        public bool MakeNonBillableZone
        {
            get
            {
                return this.makeNonBillableZone;
            }
        }

        /// <summary>
        /// Make E-library option.
        /// </summary>
        public bool MakeELibrary
        {
            get
            {
                return this.makeELibrary;
            }
        }

        /// <summary>
        /// Client Id name for non billable zone
        /// </summary>
        public string ZoneName
        {
            get
            {
                return this.zoneName;
            }
        }
    }
}