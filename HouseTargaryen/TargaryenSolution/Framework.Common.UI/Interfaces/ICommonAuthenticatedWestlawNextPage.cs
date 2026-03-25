namespace Framework.Common.UI.Interfaces
{
    /// <summary>
    /// ICommonAuthenticatedWestlawNextPage
    /// </summary>
    public interface ICommonAuthenticatedWestlawNextPage : ICreatablePageObject
    {
        /// <summary>
        /// NavigateToDocumentDirectly
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="isKm"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T NavigateToDocumentDirectly<T>(string guid, bool isKm) where T : ICommonDocumentPage;
    }
}