namespace Framework.Common.Api.Raw.IPhoneMode.Models.Requests
{
    /// <summary>
    /// The list practice area request.
    /// </summary>
    public class ListPracticeAreaRequest
    {
        /// <summary>
        /// Gets or sets Area1
        /// </summary>
        public string Area1 { get; set; }

        /// <summary>
        /// Gets or sets Area2
        /// </summary>
        public string Area2 { get; set; }

        /// <summary>
        /// Gets or sets BeginIndex
        /// </summary>
        public int BeginIndex { get; set; }

        /// <summary>
        /// Gets or sets Content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets Count
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets Order
        /// </summary>
        public string Order { get; set; }
    }
}