namespace Framework.Common.UI.Interfaces
{
    /// <summary>
    /// ICommonSignOnPage
    /// </summary>
    public interface ICommonSignOnPage : ICreatablePageObject, IEnvironmentCheckPage
    {
        /// <summary>
        /// EnterUserIdPasswordAndClickSignOn
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="password"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T EnterUserIdPasswordAndClickSignOn<T>(string userid, string password) where T : ICreatablePageObject;
    }
}