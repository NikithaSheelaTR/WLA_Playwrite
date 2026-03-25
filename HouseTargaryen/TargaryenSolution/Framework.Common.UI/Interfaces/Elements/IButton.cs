namespace Framework.Common.UI.Interfaces.Elements
{
    using Framework.Common.UI.Interfaces;

    /// <inheritdoc />
    /// <summary>
    /// The Button interface.
    /// </summary>
    public interface IButton : IBaseWebElement
    {
        /// <summary>
        /// The click.
        /// </summary>
        void Click();

        /// <summary>
        /// The click.
        /// </summary>
        /// <typeparam name="TWebObject">
        /// The desired type of page object to be created
        /// </typeparam>
        /// <returns>
        /// The <see cref="ICreatablePageObject"/>.
        /// </returns>
        TWebObject Click<TWebObject>()
            where TWebObject : ICreatablePageObject;
    }
}