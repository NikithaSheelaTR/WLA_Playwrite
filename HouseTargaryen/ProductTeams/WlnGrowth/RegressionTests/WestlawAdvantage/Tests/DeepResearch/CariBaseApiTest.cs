namespace WestlawAdvantage.Tests.DeepResearch
{
    using Framework.Common.Api.DataModel.Configuration;
    using Framework.Common.Api.Tests;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.DataModel.Security;
    using Framework.Core.DataModel.Security.Specialized;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CariBaseApiTest : BaseApiRegressionTest<ApiTestExecutionContext>
    {
        /// <summary>
        /// On manage credential
        /// </summary>
        protected override void OnManageCredential()
        {
            this.DefaultUserCredential = new UserDbCredential(
                             this.TestContext,
                             PasswordVertical.WlnGrowth,
                             this.GetUserPool())
            { ClientId = "Cari" };

            CredentialPool.RegisterUser(this.DefaultUserCredential.ToOnePassUserInfo());
        }

        private string GetUserPool()
        {
            return this.TestContext.Properties["Pool"] != null
                       ? this.TestContext.Properties["Pool"].ToString()
                       : this.GetPasswordPool();
        }
    }
}
