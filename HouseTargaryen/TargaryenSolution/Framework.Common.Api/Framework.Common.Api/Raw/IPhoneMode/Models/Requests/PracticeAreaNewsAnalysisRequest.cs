namespace Framework.Common.Api.Raw.IPhoneMode.Models.Requests
{
    /// <summary>
    /// PracticeAreaNewsAnalysisRequest
    /// </summary>
    public class PracticeAreaNewsAnalysisRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PracticeAreaNewsAnalysisRequest"/> class. 
        /// </summary>
        /// <param name="practiceArea"> practice Area  </param>
        /// <param name="order"> order  </param>
        /// <param name="count"> count  </param>
        /// <param name="index"> index  </param>
        public PracticeAreaNewsAnalysisRequest(string practiceArea, string order, int count, int index)
        {
            this.PracticeArea = practiceArea;
            this.Order = order;
            this.Count = count;
            this.Index = index;
        }

        /// <summary>
        /// Gets or sets Count
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets Index
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Gets or sets Order
        /// </summary>
        public string Order { get; set; }

        /// <summary>
        /// Gets or sets Practice Area
        /// </summary>
        public string PracticeArea { get; set; }
    }
}