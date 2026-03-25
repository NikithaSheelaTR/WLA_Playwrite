namespace Framework.Common.Api.Raw.IPhoneMode.Models.Requests
{
    /// <summary>
    /// Notification Converter Request
    /// </summary>
    public class NotificationConverterRequest : IRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationConverterRequest"/> class. 
        /// </summary>
        /// <param name="body"> Request body  </param>
        public NotificationConverterRequest(string body)
        {
            this.Body = body;
        }

        /// <summary>
        /// Gets or sets Request Body
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Get Request Body
        /// </summary>
        /// <returns> Request body </returns>
        public string GetRequestBody()
        {
            return this.Body;
        }
    }
}