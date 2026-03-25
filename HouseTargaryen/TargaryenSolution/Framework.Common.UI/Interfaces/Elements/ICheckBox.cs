namespace Framework.Common.UI.Interfaces.Elements
{
    using Framework.Common.UI.Interfaces;

    /// <inheritdoc />
    /// <summary>
    /// The CheckBox interface.
    /// </summary>
    public interface ICheckBox : IBaseWebElement
    {
        /// <summary>
        /// Gets a value indicating whether selected.
        /// </summary>
        bool Selected { get; }

        /// <summary>
        /// The set.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        void Set(bool value);

        /// <summary>
        /// The set.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <typeparam name="TWebObject">
        /// The desired type of page object to be created
        /// </typeparam>
        /// <returns>
        /// The <see cref="ICreatablePageObject"/>.
        /// </returns>
        TWebObject Set<TWebObject>(bool value)
            where TWebObject : ICreatablePageObject;
    }
}