namespace Framework.Common.UI.Interfaces.Elements
{
    using System.Collections.Generic;

    /// <summary>
    /// The Dropdown interface.
    /// </summary>
    /// <typeparam name="T">The type of options inside</typeparam>
    public interface IDropdown<T>
    {
        /// <summary>
        /// Gets the Options
        /// </summary>
        /// <returns>The list of options</returns>
        IEnumerable<T> Options { get; }

        /// <summary>
        /// Gets selected option in dropdown
        /// </summary>
        T SelectedOption { get; }

        /// <summary>
        /// Gets the selected option text.
        /// </summary>
        string SelectedOptionText { get; }

        /// <summary>
        /// The Is selected. Check if desired option is selected
        /// </summary>
        /// <param name="option">The option to check</param>
        /// <returns>True is desired option is selected</returns>
        bool IsSelected(T option);

        /// <summary>
        /// The is enabled.
        /// </summary>
        /// <param name="option">The option.</param>
        /// <returns> The <see cref="bool"/>.</returns>
        bool IsEnabled(T option);

        /// <summary>
        /// The is displayed.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool IsDisplayed();

        /// <summary>
        /// The select option. Selects desired option and returns new page
        /// </summary>
        /// <param name="option">Desired option</param>
        /// <typeparam name="TPage">The type of page to return</typeparam>
        /// <returns>The new instance of desired page</returns>
        TPage SelectOption<TPage>(T option) where TPage : ICreatablePageObject;

        /// <summary>
        /// Selects desired option
        /// </summary>
        /// <param name="option">Desired option</param>
        void SelectOption(T option);
    }
}