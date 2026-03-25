namespace Framework.Common.UI.Interfaces.Elements
{
    /// <summary>
    /// The IIndeterminateCheckBox interface.
    /// </summary>
    public interface IIndeterminateCheckBox : ICheckBox
    {
        /// <summary>
        /// Gets a value indicating whether is 'partially selected'
        /// </summary>
        bool PartiallySelected { get; }
    }
}
