namespace Framework.Common.UI.Interfaces
{
    /// <summary>
    /// The CommonRoutingPage interface.
    /// </summary>
    public interface ICommonRoutingPage : ICreatablePageObject
    {
        /// <summary>
        /// The click save.
        /// </summary>
        /// <typeparam name="T">The type of a target page.</typeparam>
        /// <returns>The an instance of the target page.</returns>
        T ClickSaveButton<T>() where T : ICreatablePageObject;
    }
}