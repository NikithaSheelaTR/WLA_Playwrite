namespace Framework.Core.DataModel.Security.Proxies
{
    /// <summary>
    /// The OnePassUserInfo interface.
    /// </summary>
    public interface IOnePassUserInfo : IUserInfo
    {
        /// <summary>
        /// Password
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// User GUID
        /// </summary>
        string PrismGuid { get; set; }

        /// <summary>
        /// The dispose.
        /// </summary>
        void Dispose();
    }
}