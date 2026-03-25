namespace Framework.Common.UI.Products.LawSchool.Utils
{
    using Framework.Common.UI.DataModel.Configuration;
    using Framework.Common.UI.Products.Shared.DomainObjects.SignOn;
    using Framework.Core.DataModel.Security.Proxies;

    /// <summary>
    /// LawSchoolSignOnContext sing on context
    /// </summary>
    /// <typeparam name="TUserInfo">TUserInfo</typeparam>
    public class LawSchoolSignOnContext<TUserInfo> : BaseSignOnContext<TUserInfo>
        where TUserInfo : IUserInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LawSchoolSignOnContext{TUserInfo}"/> class.  
        /// </summary>
        /// <param name="testExecutionContext">The test Execution Context.</param>
        /// <param name="userInfo">The user Info.</param>
        public LawSchoolSignOnContext(UiTestExecutionContext testExecutionContext, TUserInfo userInfo)
            : base(testExecutionContext, userInfo)
        {
            this.ForceRouting = false;
        }
    }
}