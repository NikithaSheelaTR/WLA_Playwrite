namespace Framework.Common.Api.Raw.IPhoneMode.Models.Requests
{
    /// <summary>
    /// The generic request.
    /// </summary>
    public class GenericRequest : IRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRequest"/> class. 
        /// </summary>
        /// <param name="body">
        /// body
        /// </param>
        public GenericRequest(string body)
        {
            this.Body = body;
        }

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Get Request Body
        /// </summary>
        /// <returns> body </returns>
        public string GetRequestBody()
        {
            return this.Body;
        }
    }
}