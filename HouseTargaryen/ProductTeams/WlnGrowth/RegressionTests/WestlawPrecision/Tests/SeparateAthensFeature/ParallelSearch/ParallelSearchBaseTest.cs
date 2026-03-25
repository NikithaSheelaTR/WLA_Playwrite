namespace WestlawPrecision.Tests.SeparateAthensFeature.ParallelSearch
{
    using Framework.Common.UI.DataModel;
    using Framework.Common.UI.Products.WestLawNext.Utils;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.CommonTypes.Constants;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.DataModel.Security.Proxies;
    using Framework.Core.DataModel.Security.Specialized;
    using Framework.Core.DataModel.Security;
    using Framework.Core.Utils.Extensions;
    using System.IO;

    public class ParallelSearchBaseTest : BaseWestlawTest
    {
        protected const string CurrentTestCategory = "Aalp";
        protected const string SmokeTestCategory = "AalpSmoke";
        protected const string ParallelSearchTab = "Parallel Search page";
        protected const string FolderToSaveConst = @"C:\Temp\Aalp";
        protected const string DeliveryDateFormat = "MM-dd-yyyy";

        /// <summary>
        ///Initializes a new instance of the ParallelSearchBaseTest class
        /// </summary>
        public ParallelSearchBaseTest()
        {
            //Provide PASSWORD_POOL value in LocalTestConfig file
            //WLP: IndigoPremium WLA: WestlawAdvantage
            this.UiExecutionSettings = this.UiExecutionSettings.SetFlags(UiExecutionFlags.AllowUiPreconditionRoutines);
        }

        /// <summary>
        /// The folder to save.
        /// </summary>
        protected override string FolderToSave => Path.Combine(FolderToSaveConst, this.TestContext.TestName);

        /// <summary>
        /// On Manage Credential
        /// </summary>
        protected override void OnManageCredential()
        {
            var userCredential = new UserDbCredential(
                    this.TestContext,
                    PasswordVertical.WlnGrowth,
                    this.GetPasswordPool())
            { ClientId = "ParallelSearchTest" };
            CredentialPool.RegisterUser(userCredential);

            this.DefaultSignOnContext = new WlnSignOnContext<IUserInfo>(this.TestExecutionContext, userCredential.ToWlnUserInfo());
        }

        /// <summary>
        /// The perform ui postcondition routines.
        /// </summary>
        protected override void PerformUiPostconditionRoutines()
        {
            if (Directory.Exists(FolderToSave))
            {
                Directory.Delete(FolderToSave, true);
            }

            base.PerformUiPostconditionRoutines();
        }
    }
}

