namespace Framework.Common.UI.Interfaces.Elements
{
    /// <summary>
    /// The DropdownOptionItem interface.
    /// </summary>
    public interface IDropdownOptionItem
    {
        /// <summary>
        /// Gets the option text.
        /// </summary>
        string OptionText { get; }

        /// <summary>
        /// Gets a value indicating whether is selected.
        /// </summary>
        bool IsSelected { get; }
    }
}