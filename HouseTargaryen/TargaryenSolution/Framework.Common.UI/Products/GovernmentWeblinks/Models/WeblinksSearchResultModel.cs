namespace Framework.Common.UI.Products.GovernmentWeblinks.Models
{
    /// <summary>
    /// The weblinks search result.
    /// </summary>
    public class WeblinksSearchResultModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WeblinksSearchResultModel"/> class.
        /// </summary>
        /// <param name="index"> The index. </param>
        /// <param name="resultTitle"> The result title. </param>
        /// <param name="resultCitation"> The result citation. </param>
        /// <param name="resultLink"> The result link. </param>
        public WeblinksSearchResultModel(int index, string resultTitle, string resultCitation, string resultLink)
        {
            this.Index = index;
            this.Title = resultTitle;
            this.Citation = resultCitation;
            this.Link = resultLink;
        }

        /// <summary>
        /// Gets the citation.
        /// </summary>
        public string Citation { get; }

        /// <summary>
        /// Gets the document link.
        /// </summary>
        public string Link { get; }

        /// <summary>
        /// Gets the index.
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// Gets the title.
        /// </summary>
        public string Title { get; }
    }
}