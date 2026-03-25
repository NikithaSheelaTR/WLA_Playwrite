namespace Framework.Common.Api.Interfaces
{
    using Framework.Common.Api.Utilities;

    using RestSharp;

    /// <summary>
    /// The RequestBuilder interface.
    /// </summary>
    public interface IRequestBuilder
    {
        /// <summary>
        /// The build request.
        /// </summary>
        /// <param name="arguments">
        /// The arguments.
        /// </param>
        /// <returns>
        /// The <see cref="IRestRequest"/>.
        /// </returns>
        IRestRequest BuildRequest(RequestArguments arguments);
    }
}
