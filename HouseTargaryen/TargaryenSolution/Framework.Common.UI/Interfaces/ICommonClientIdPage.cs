namespace Framework.Common.UI.Interfaces
{
    /// <summary>
    /// ICommonClientIdPage
    /// </summary>
    public interface ICommonClientIdPage : ICreatablePageObject, IEnvironmentCheckPage
    {
        /// <summary>
        /// EnterClientIdAndClickContinue
        /// </summary>
        /// <param name="clientid"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T EnterClientIdAndClickContinue<T>(string clientid) where T : ICreatablePageObject;
    }
}