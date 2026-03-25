namespace Framework.Core.DataModel.Security.Proxies
{
    /// <summary>
    /// The UserInfo interface.
    /// </summary>
    public interface IUserInfo
    {
        /// <summary>
        /// Gets a value indicating whether is disposed.
        /// </summary>
        bool IsDisposed { get; }

        /// <summary>
        /// Unique key
        /// </summary>
        string UniqueKey { get; }

        /// <summary>
        /// User name
        /// </summary>
        string UserName { get; }
    }
}