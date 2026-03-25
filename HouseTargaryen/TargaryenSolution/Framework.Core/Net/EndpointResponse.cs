namespace Framework.Core.Net
{
    using System.Net;
    using System.Text;

    using Framework.Core.Utils;

    /// <summary>
    /// Stores information regarding a successful response from an application endpoint. This information includes:
    /// <ul>
    /// <li>a response body</li>
    /// <li>a list of headers received on the response</li>
    /// </ul>
    /// </summary>
    /// <remarks>It is recommended that an <c>EndpointResponse&lt;String&gt;</c> be used when no response body is expected but response headers are still expected.</remarks>
    /// <typeparam name="T">The type of the response body returned by an endpoint.</typeparam>
    public class EndpointResponse<T>
    {
        /// <summary>
        /// Gets or sets the response body of an endpoint response.
        /// </summary>
        /// <value>The response body of an endpoint response.</value>
        public T ResponseBody { get; set; }

        /// <summary>
        /// Gets or sets the headers of an endpoint response.
        /// </summary>
        /// <value>The headers of an endpoint response.</value>
        public WebHeaderCollection Headers { get; set; }

        /// <summary>
        /// Constructs an endpoint response.
        /// </summary>
        /// <param name="responseBody">A response body.</param>
        /// <param name="headers">A set of headers.</param>
        public EndpointResponse(T responseBody, WebHeaderCollection headers = null)
        {
            this.ResponseBody = responseBody;
            this.Headers = headers;
        }

        /// <summary>
        /// Determines whether the specified <see cref="EndpointResponse{T}"/> is equal to the current <see cref="EndpointResponse{T}"/>.
        /// </summary>
        /// <param name="aThat">The <see cref="EndpointResponse{T}"/> to compare with the current <see cref="EndpointResponse{T}"/>.</param>
        /// <returns><b>true</b> if the specified Object is equal to the current <see cref="EndpointResponse{T}"/>; otherwise, <b>false</b>.</returns>
        public override bool Equals(object aThat)
        {
            if (aThat == null || this.GetType() != aThat.GetType())
            {
                return false;
            }

            var that = (EndpointResponse<T>)aThat;
            return EqualsUtils.AreEqual(this.ResponseBody, that.ResponseBody) && EqualsUtils.AreEqual(this.Headers, that.Headers);
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>A hash code for the current <see cref="EndpointResponse{T}"/>.</returns>
        public override int GetHashCode()
        {
            int result = HashCodeUtils.Seed;
            result = HashCodeUtils.Hash(result, this.ResponseBody);
            result = HashCodeUtils.Hash(result, this.Headers);
            return result;
        }

        /// <summary>
        /// Returns a string that represents the current <see cref="EndpointResponse{T}"/>.
        /// </summary>
        /// <returns>A string that represents the current <see cref="EndpointResponse{T}"/>.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Response Body:\n" + this.ResponseBody);
            sb.AppendLine("Headers:\n" + this.Headers);
            return sb.ToString();
        }
    }
}
