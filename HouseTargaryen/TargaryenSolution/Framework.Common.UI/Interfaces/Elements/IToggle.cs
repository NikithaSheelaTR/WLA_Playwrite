namespace Framework.Common.UI.Interfaces.Elements
{
    /// <summary>
    /// The IToggle interface
    /// </summary>
    public interface IToggle : IBaseWebElement
    {
        /// <summary>
        /// Gets the State
        /// </summary>
        bool State { get; }

        /// <summary>
        /// The toggle.
        /// </summary>
        /// <param name="state">
        /// The state.
        /// </param>
        void ToggleState(bool state);

        /// <summary>
        /// The Toggle.
        /// </summary>
        /// <param name="state">
        /// The state.
        /// </param>
        /// <typeparam name="TPageObject"> The desired page object </typeparam>
        /// <returns>
        /// The <see cref="ICreatablePageObject"/>.
        /// </returns>
        TPageObject ToggleState<TPageObject>(bool state)
            where TPageObject : ICreatablePageObject;
    }
}