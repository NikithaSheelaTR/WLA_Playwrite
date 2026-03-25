namespace Framework.Common.Api.Endpoints.Document.DataModel
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The fo options. Class to store options for our fo JSON
    /// </summary>
    public class FoOptions
    {
        private string coverPageComment;

        private string footnotesFormat;

        private string headnotesLayout;

        private bool includeAnnotations;

        private bool kmFlag;

        /// <summary>
        /// Create a FoOptions object with a doc guid
        /// </summary>
        /// <param name="docGuid">the document guid</param>
        public FoOptions(string docGuid)
        {
            this.DocGuid = docGuid;
            this.DocGuids = new List<string> { docGuid };

            this.SetDefaults();
        }

        /// <summary>
        /// Create a FoOptions object with a doc guid
        /// </summary>
        /// <param name="docGuids">the list of document guids</param>
        public FoOptions(List<string> docGuids)
        {
            this.DocGuids = docGuids;
            this.SetDefaults();
            this.ContextView = "SearchResultList";
        }

        /// <summary>
        /// Deliver with annotations
        /// </summary>
        public string AnnotationsSelection { get; private set; }

        /// <summary>
        /// Gets or sets the context view.
        /// </summary>
        public string ContextView { get; set; }

        /// <summary>
        /// Deliver a cover page
        /// </summary>
        public bool CoverPage { get; set; }

        /// <summary>
        /// Add a cover page comment
        /// </summary>
        public string CoverPageComment
        {
            get
            {
                return this.coverPageComment;
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.CoverPage = true;
                }

                this.coverPageComment = value;
            }
        }

        /// <summary>
        /// The document guid
        /// </summary>
        public string DocGuid { get; private set; }

        /// <summary>
        /// The document guid
        /// </summary>
        public List<string> DocGuids { get; set; }

        /// <summary>
        /// Deliver the document in dual column mode
        /// </summary>
        public bool DualColumn { get; set; }

        /// <summary>
        /// Gets or sets the footnotes format.
        /// </summary>
        /// <exception cref="Exception">
        /// </exception>
        public string FootnotesFormat
        {
            get
            {
                return this.footnotesFormat;
            }

            set
            {
                if (value.ToLower().Equals("endofdocument") || value.ToLower().Equals("inline"))
                {
                    this.footnotesFormat = value;
                }
                else
                {
                    throw new Exception(
                        "Footnotes layout for delivery is not one of the accepted values: EndOfDocument or Inline");
                }
            }
        }

        /// <summary>
        /// The format of the delivery request "pdf", "rtf", "wlx"
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// Gets or sets the headnotes layout.
        /// </summary>
        /// <exception cref="Exception">
        /// </exception>
        public string HeadnotesLayout
        {
            get
            {
                return this.headnotesLayout;
            }

            set
            {
                if (value.ToLower().Equals("none") || value.ToLower().Equals("collapsedkeynumbers")
                    || value.ToLower().Equals("expandedkeynumbers"))
                {
                    this.headnotesLayout = value;
                }
                else
                {
                    throw new Exception(
                        "Headnotes layout for delivery is not one of the accepted values: None, CollapsedKeyNumbers, ExpandedKeyNumbers");
                }
            }
        }

        /// <summary>
        /// Include notes in the delivery request
        /// </summary>
        public bool IncludeAnnotations
        {
            get
            {
                return this.includeAnnotations;
            }

            set
            {
                this.includeAnnotations = value;
                this.AnnotationsSelection = value ? "DocumentAndPersonalAnnotations" : "DocumentOnly";
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether key cite treatment.
        /// </summary>
        public bool KeyCiteTreatment { get; set; }

        /// <summary>
        /// Add the KM icon to the cover page
        /// </summary>
        public bool KmFlag
        {
            get
            {
                return this.kmFlag;
            }

            set
            {
                if (value)
                {
                    this.CoverPage = true;
                }

                this.kmFlag = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether original image link.
        /// </summary>
        public bool OriginalImageLink { get; set; }

        /// <summary>
        /// Deliver only pages wth highlighted terms
        /// </summary>
        public bool PagesWithTerms { get; set; }

        /// <summary>
        /// Add space on the right side of the docs for notes
        /// </summary>
        public bool RightNoteMargin { get; set; }

        /// <summary>
        /// The search terms to highlight
        /// </summary>
        public List<string> SearchTerms { get; set; }

        /// <summary>
        /// Turn on term highlighting
        /// </summary>
        public bool TermHiglighting { get; set; }

        /// <summary>
        /// Set the default fo options
        /// </summary>
        private void SetDefaults()
        {
            this.TermHiglighting = false;
            this.RightNoteMargin = false;
            this.DualColumn = false;
            this.CoverPage = false;
            this.KmFlag = false;
            this.CoverPageComment = string.Empty;
            this.PagesWithTerms = false;
            this.SearchTerms = new List<string>();
            this.IncludeAnnotations = false;
            this.Format = "pdf";
            this.HeadnotesLayout = "CollapsedKeyNumbers";
            this.ContextView = "SingleDocument";
            this.OriginalImageLink = true;
            this.KeyCiteTreatment = true;
            this.FootnotesFormat = "EndOfDocument";
        }
    }
}