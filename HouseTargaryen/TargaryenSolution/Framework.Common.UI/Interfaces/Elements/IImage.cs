namespace Framework.Common.UI.Interfaces.Elements
{
    /// <summary>
    /// The Image interface.
    /// </summary>
    public interface IImage : IBaseWebElement
    {
        /// <summary>
        /// Gets the source.
        /// </summary>
        string Source { get; }

        /// <summary>
        /// The click.
        /// </summary>
        void Click();

        /// <summary>
        /// The click.
        /// </summary>
        /// <typeparam name="TPageObject">The desired page object</typeparam>
        /// <returns>
        /// The <see cref="ICreatablePageObject"/>.
        /// </returns>
        TPageObject Click<TPageObject>() where TPageObject : ICreatablePageObject;
    }
}