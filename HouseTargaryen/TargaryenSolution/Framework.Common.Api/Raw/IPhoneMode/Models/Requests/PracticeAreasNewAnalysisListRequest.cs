namespace Framework.Common.Api.Raw.IPhoneMode.Models.Requests
{
    using System;

    /// <summary>
    /// PracticeAreasNewAnalysisListRequest
    /// </summary>
    public class PracticeAreasNewAnalysisListRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PracticeAreasNewAnalysisListRequest"/> class.  
        /// </summary>
        /// <param name="practiceareas"> practice areas  </param>
        /// <param name="count"> count  </param>
        /// <param name="index"> index  </param>
        /// <param name="order"> order  </param>
        public PracticeAreasNewAnalysisListRequest(
            Tuple<string, string> practiceareas,
            int count,
            int index,
            string order)
        {
            this.Area1 = practiceareas.Item1;
            this.Area2 = practiceareas.Item2;
            this.Count = count;
            this.Index = index;
            this.Order = order;
        }

        /// <summary>
        /// Gets or sets the area 1.
        /// </summary>
        public string Area1 { get; set; }

        /// <summary>
        /// Gets or sets the area 2.
        /// </summary>
        public string Area2 { get; set; }

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        public string Order { get; set; }
    }
}