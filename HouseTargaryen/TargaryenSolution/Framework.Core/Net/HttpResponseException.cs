namespace Framework.Core.Net
{
    using System;
    using System.Net;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    /// <summary>
    /// An exception to be thrown when an HTTP response from a URI contains an unexpected status code.
    /// </summary>
    [Serializable]
    public class HttpResponseException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResponseException"/> class.
        /// </summary>
        public HttpResponseException()
        {
            // default constructor
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResponseException"/> class with a specified HTTP status code and error message.
        /// </summary>
        /// <param name="statusCode">The HTTP status code unexpectedly received in response from a URI.</param>
        /// <param name="message">The message that describes the error.</param>
        public HttpResponseException(HttpStatusCode statusCode, string message)
            : base(message)
        {
            this.StatusCode = statusCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResponseException"/> class with a specified HTTP status code, error message, and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="statusCode">The HTTP status code unexpectedly received in response from a URI.</param>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<b>Nothing</b> in Visual Basic) if no inner exception is specified.</param>
        public HttpResponseException(HttpStatusCode statusCode, string message, Exception innerException)
            : base(message, innerException)
        {
            this.StatusCode = statusCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResponseException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination. </param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected HttpResponseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.StatusCode = (HttpStatusCode)info.GetValue("StatusCode", typeof(HttpStatusCode));
        }

        /// <summary>
        /// Gets or sets the HTTP status code associated with a response from a URI.
        /// </summary>
        /// <value>The HTTP status code associated with a response from a URI.</value>
        public HttpStatusCode StatusCode { get; private set; }

        /// <summary>
        /// Sets the <see cref="SerializationInfo"/> with information about the exception.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown. </param>
        /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination. </param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // check input
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            // set values
            info.AddValue("StatusCode", this.StatusCode);
            base.GetObjectData(info, context);
        }
    }
}
