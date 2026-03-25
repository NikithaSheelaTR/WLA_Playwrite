namespace Framework.Common.Api.Raw.IPhoneMode.Models.Requests
{
    /// <summary>
    /// request Interface
    /// </summary>
    public interface IRequest
    {
        /// <summary>
        /// Get Request Body
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string GetRequestBody();
    }
}