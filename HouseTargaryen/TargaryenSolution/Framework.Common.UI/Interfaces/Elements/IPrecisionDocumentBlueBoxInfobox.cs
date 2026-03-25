namespace Framework.Common.UI.Interfaces.Elements
{
    /// <summary>
    /// The IPrecisionDocumentBlueBoxInfobox interface
    /// </summary>
    public interface IPrecisionDocumentBlueBoxInfobox: IInfoBox
    {
        /// <summary>
        /// Gets the MLT button.
        /// </summary>
        IButton MoreLikeThisButton { get; }
    }
}
