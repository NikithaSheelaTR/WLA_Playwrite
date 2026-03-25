namespace Framework.Common.UI.Products.Shared.DomainObjects.SignOn
{
    using Framework.Common.UI.DataModel;
    using Framework.Core.DataModel;
    using Framework.Core.DataModel.Configuration.Proxies;
    using Framework.Core.DataModel.Security.Proxies;

    /// <summary>
    /// The SignOnContext interface.
    /// </summary>
    /// <typeparam name="TUserInfo">
    /// </typeparam>
    public interface ISignOnContext<TUserInfo>
        where TUserInfo : IUserInfo
    {
        /// <summary>
        /// Gets or sets a value indicating whether force navigate.
        /// </summary>
        bool ForceNavigate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether force routing.
        /// </summary>
        bool ForceRouting { get; set; }

        /// <summary>
        /// Gets or sets the routing settings info.
        /// </summary>
        RoutingSettingsInfo RoutingSettingsInfo { get; set; }

        /// <summary>
        /// Gets or sets the test environment.
        /// </summary>
        EnvironmentInfo TestEnvironment { get; set; }

        /// <summary>
        /// Gets the user info.
        /// </summary>
        TUserInfo UserInfo { get; }

        /// <summary>
        /// Gets or sets a value indicating whether closing welcome dialog
        /// </summary>
        bool CloseWelcomeDialog { get; set; }
    }
}