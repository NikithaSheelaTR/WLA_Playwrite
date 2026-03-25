namespace Framework.Common.UI.Interfaces
{
    /// <summary>
    /// ICommonSignOffPage
    /// </summary>
    public interface ICommonSignOffPage : ICreatablePageObject
    {
        /// <summary>
        /// ClickSignOn
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T ClickSignOn<T>() where T : ICreatablePageObject;
    }
}