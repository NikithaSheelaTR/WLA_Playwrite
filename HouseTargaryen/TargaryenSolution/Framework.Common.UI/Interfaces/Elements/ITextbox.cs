namespace Framework.Common.UI.Interfaces.Elements
{
    /// <inheritdoc />
    /// <summary>
    /// The Textbox interface.
    /// </summary>
    public interface ITextbox : IBaseWebElement
    {
        /// <summary>
        /// The set text.
        /// </summary>
        /// <param name="textToSet">
        /// The text to set.
        /// </param>
        void SetText(string textToSet);

        /// <summary>
        /// The set text.
        /// </summary>
        /// <typeparam name="T">
        /// Type of the page
        /// </typeparam>
        /// <param name="textToSet">
        /// The text to set.
        /// </param>
        /// <returns>
        /// New instance of the page
        /// </returns>
        T SetText<T>(string textToSet) where T : ICreatablePageObject;

        /// <summary>
        /// The set text slow.
        /// </summary>
        /// <param name="textToSet">
        /// The text to set.
        /// </param>
        void SendKeysSlow(string textToSet);

        /// <summary>
        /// The set text slow.
        /// </summary>
        /// <param name="textToSet">
        /// The text to set.
        /// </param>
        /// <returns>
        /// New instance of the page
        /// </returns>
        T SendKeysSlow<T>(string textToSet) where T : ICreatablePageObject;

        /// <summary>
        /// The clear.
        /// </summary>
        void Clear();

        /// <summary>
        /// The clear.
        /// </summary>
        T Clear<T>() where T : ICreatablePageObject;

        /// <summary>
        /// The click.
        /// </summary>
        /// <typeparam name="TPageObject"> The desired page object </typeparam>
        /// <returns>
        /// The <see cref="ICreatablePageObject"/>.
        /// </returns>
        TPageObject Click<TPageObject>()
            where TPageObject : ICreatablePageObject;
    }
}