namespace Framework.Common.UI.Interfaces.Elements
{

    /// <inheritdoc />
    /// <summary>
    /// The InfoBox interface.
    /// </summary>
    public interface IInfoBox : IBaseWebElement
    {
        /// <summary>
        /// Gets the X button.
        /// </summary>
        IButton CloseButton { get; }
    }
}
