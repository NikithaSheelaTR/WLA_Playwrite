namespace Framework.Common.UI.Products.Shared.DomainObjects.SignOn
{
    using Framework.Common.UI.DataModel;
    using Framework.Common.UI.DataModel.Configuration;
    using Framework.Core.DataModel;
    using Framework.Core.DataModel.Configuration.Proxies;
    using Framework.Core.DataModel.Security.Proxies;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TUserInfo"></typeparam>
    public abstract class BaseSignOnContext<TUserInfo> : ISignOnContext<TUserInfo>
        where TUserInfo : IUserInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSignOnContext{TUserInfo}"/> class. 
        /// </summary>
        /// <param name="testExecutionContext">
        /// testExecutionContext
        /// </param>
        /// <param name="userInfo">
        /// userInfo
        /// </param>
        protected BaseSignOnContext(UiTestExecutionContext testExecutionContext, TUserInfo userInfo)
        {
            this.UserInfo = userInfo;
            this.TestEnvironment = testExecutionContext.TestEnvironment;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSignOnContext{TUserInfo}"/> class. 
        /// </summary>
        protected BaseSignOnContext()
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether force navigate.
        /// </summary>
        public bool ForceNavigate { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether force routing.
        /// </summary>
        public bool ForceRouting { get; set; } = true;

        /// <summary>
        /// Gets or sets the routing settings info.
        /// </summary>
        public RoutingSettingsInfo RoutingSettingsInfo { get; set; }

        /// <summary>
        /// Gets or sets the test environment.
        /// </summary>
        public EnvironmentInfo TestEnvironment { get; set; }

        /// <summary>
        /// Gets the user info.
        /// </summary>
        public TUserInfo UserInfo { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether closing welcome dialog
        /// </summary>
        public bool CloseWelcomeDialog { get; set; } = false;
    }
}