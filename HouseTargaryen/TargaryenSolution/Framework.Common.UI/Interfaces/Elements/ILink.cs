namespace Framework.Common.UI.Interfaces.Elements
{
    /// <summary>
    /// The Link interface.
    /// </summary>
    public interface ILink : IButton
    {
        /// <summary>
        /// Gets the link url.
        /// </summary>
        string LinkUrl { get; }
    }
}