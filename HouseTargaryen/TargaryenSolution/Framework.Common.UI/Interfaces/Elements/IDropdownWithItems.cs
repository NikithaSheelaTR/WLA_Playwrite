namespace Framework.Common.UI.Interfaces.Elements
{
    using System.Collections.Generic;

    /// <summary>
    /// The Drop down for Items interface.
    /// </summary>
    /// <typeparam name="T"> The type of options </typeparam>
    /// <typeparam name="TItem"> The type of drop down item </typeparam>
    public interface IDropdownWithItems<T, out TItem> : IDropdown<T>
        where TItem : IDropdownOptionItem
    {
        /// <summary>
        /// Gets the option items.
        /// </summary>
        IEnumerable<TItem> OptionItems { get; }

        /// <summary>
        /// Gets selected option item in drop down
        /// </summary>
        TItem SelectedOptionItem { get; }
    }
}