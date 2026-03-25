namespace Framework.Common.UI.Products.WestLawNext.Utils
{
    using System.Diagnostics.CodeAnalysis;

    using Framework.Common.UI.DataModel.Configuration;
    using Framework.Common.UI.Products.Shared.Enums.Core;
    using Framework.Core.DataModel.Security.Proxies;
    using Framework.Core.DataModel.Security.Specialized;

    /// <summary>
    /// The KM sign on context.
    /// </summary>
    /// <typeparam name="TUserInfo"> TUserInfo </typeparam>
    public class KmSignOnContext<TUserInfo> : WlnSignOnContext<TUserInfo>
        where TUserInfo : class, IUserInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KmSignOnContext{TUserInfo}"/> class.
        /// </summary>
        /// <param name="testExecutionContext"> The test execution context </param>
        /// <param name="userInfo"> The user info. </param>
        /// <param name="kmUserName"> The KM user name</param>
        /// <param name="authenticationMode"> The authentication mode</param>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation",
            Justification = "Reviewed. Suppression is OK here.")]
        public KmSignOnContext(
            UiTestExecutionContext testExecutionContext,
            TUserInfo userInfo,
            string kmUserName,
            KmAuthenticationMode authenticationMode)
            : base(testExecutionContext, userInfo)
        {
            this.KmAuthMode = authenticationMode;
            if (this.KmAuthMode == KmAuthenticationMode.Km)
            {
                var dbUserInfo = new UserDbCredential(kmUserName, "KmTest");
                this.KmUserInfo = dbUserInfo.ToWlnUserInfo() as TUserInfo;
            }
        }

        /// <summary>
        /// Gets the KM authentication mode.
        /// </summary>
        public KmAuthenticationMode KmAuthMode { get; private set; }

        /// <summary>
        /// Gets the KM user info.
        /// </summary>
        public TUserInfo KmUserInfo { get; private set; }
    }
}