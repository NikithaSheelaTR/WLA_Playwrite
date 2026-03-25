namespace Framework.Common.UI.Interfaces.Components
{
    /// <summary>
    /// Component With Links Interface
    /// </summary>
    public interface ILinksComponent
    {
        /// <summary>
        /// Get Widget title
        /// </summary>
        /// <returns>Widget Title as string</returns>
        string GetTitle();

        /// <summary>
        /// Check is widget displayed
        /// </summary>
        /// <returns>true if displayed</returns>
        bool IsDisplayed();
    }
}