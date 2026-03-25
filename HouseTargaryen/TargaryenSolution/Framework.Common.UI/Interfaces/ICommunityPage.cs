namespace Framework.Common.UI.Interfaces
{
    /// <summary>
    /// CommunityPage interface
    /// </summary>
    public interface ICommunityPage : ICreatablePageObject
    {
        /// <summary>
        /// Is Community page displayed
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        bool IsCommunityPageSignInDisplayed();
    }
}