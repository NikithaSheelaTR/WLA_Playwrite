namespace Framework.Common.UI.Products.Patron.Utils
{
    using Framework.Common.UI.DataModel.Configuration;
    using Framework.Common.UI.Products.Shared.DomainObjects.SignOn;
    using Framework.Core.DataModel.Security.Proxies;

    /// <summary>
    /// The Patron sign on context.
    /// </summary>
    /// <typeparam name="TUserInfo"> User Info type </typeparam>
    public class PatronSignOnContext<TUserInfo> : BaseSignOnContext<TUserInfo>
        where TUserInfo : IUserInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PatronSignOnContext{TUserInfo}"/> class.
        /// </summary>
        /// <param name="testExecutionContext"> The test execution context.  </param>
        /// <param name="userInfo"> The user info. </param>
        public PatronSignOnContext(UiTestExecutionContext testExecutionContext, TUserInfo userInfo)
            : base(testExecutionContext, userInfo)
        {
            this.RoutingSettingsInfo = testExecutionContext.RoutingPageSettings;
            this.AcceptAgreement = false;
        }

        /// <summary>
        /// Gets or sets a value indicating whether accept agreement or not.
        /// </summary>
        public bool AcceptAgreement { get; set; }
    }
}