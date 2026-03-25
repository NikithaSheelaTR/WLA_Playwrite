namespace Framework.Core.DataModel.Configuration.Sections
{
    /// <summary>
    /// The ProxyEmittable interface.
    /// </summary>
    /// <typeparam name="T">The type of a proxy object to emit.</typeparam>
    internal interface IProxyEmittable<T>
    {
        /// <summary>
        /// Gets a proxy object.
        /// </summary>
        T ProxyObject { get; }
    }
}