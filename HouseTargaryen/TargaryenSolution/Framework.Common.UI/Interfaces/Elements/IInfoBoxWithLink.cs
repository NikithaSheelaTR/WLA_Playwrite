namespace Framework.Common.UI.Interfaces.Elements
{
    /// <summary>
    /// The InfoBoxWithLink interface
    /// </summary>
    public interface IInfoBoxWithLink : IInfoBox
    {
        /// <summary>
        /// Gets the text link.
        /// </summary>
        ILink TextLink { get; }

        /// <summary>
        /// Gets the internal link.
        /// </summary>
        ILink InternalLink { get; }
    }
}