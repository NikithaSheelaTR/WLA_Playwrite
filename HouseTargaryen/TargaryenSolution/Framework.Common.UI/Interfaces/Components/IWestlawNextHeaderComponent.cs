namespace Framework.Common.UI.Interfaces.Components
{
    using Framework.Common.UI.Products.Shared.Dialogs;

    /// <summary>
    /// IWestlawNextHeaderComponent
    /// </summary>
    public interface IWestlawNextHeaderComponent
    {
        /// <summary>
        /// Clicks the search button
        /// </summary>
        /// <typeparam name="T">
        /// Object
        /// </typeparam>
        /// <returns>
        /// A new instance of the search results page
        /// </returns>
        T ClickSearchButton<T>() where T : ICreatablePageObject;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="sendSlow"></param>
        /// <param name="clearFirst"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T EnterSearchQuery<T>(string query, bool sendSlow = false, bool clearFirst = true)
            where T : BaseModuleRegressionDialog;
    }
}