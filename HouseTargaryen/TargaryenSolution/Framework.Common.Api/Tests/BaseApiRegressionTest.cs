namespace Framework.Common.Api.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.Api.DataModel;
    using Framework.Common.Api.DataModel.Configuration;
    using Framework.Common.Api.Endpoints;
    using Framework.Common.Api.Endpoints.CobaltServices;
    using Framework.Common.Api.Utilities;
    using Framework.Core.Cobalt.Passwords;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.CommonTypes.Constants;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;
    using Framework.Core.DataModel.Security;
    using Framework.Core.DataModel.Security.Proxies;
    using Framework.Core.DataModel.Security.Specialized;
    using Framework.Core.QrtBaseTests;
    using Framework.Core.QualityChecks.Result;
    using Framework.Core.Utils;
    using Framework.Core.Utils.Configuration;
    using Framework.Core.Utils.Execution;
    using Framework.Core.Utils.Verification;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// The base API regression test (for QRT)
    /// </summary>
    /// <typeparam name="TTestContextType"></typeparam>
    [TestClass]
    public class BaseApiRegressionTest<TTestContextType> : BaseContextualTest<TTestContextType>
        where TTestContextType : ApiTestExecutionContext
    {
        /// <summary>
        /// The cobalt session manager.
        /// </summary>
        protected CobaltSessionManager CobaltSessionManager => this.cobaltSessionManager =
                                                                   this.cobaltSessionManager
                                                                   ?? new CobaltSessionManager(
                                                                       this.TestExecutionContext.TestEnvironment,
                                                                       this.DefaultCobaltProduct,
                                                                       this.DefaultUserCredential.ToOnePassUserInfo());

        /// <summary>
        /// Cobalt product under test
        /// </summary>
        protected CobaltProductInfo DefaultCobaltProduct { get; set; }

        /// <summary>
        /// Gets or sets the API execution settings.
        /// </summary>
        protected ApiExecutionFlags ApiExecutionSettings { get; set; }

        /// <summary>
        /// The default user ctedentials
        /// </summary>
        protected IUserCredential DefaultUserCredential { get; set; }

        /// <summary>
        /// Test Case Verify
        /// </summary>
        protected TestCaseVerify TestCaseVerify { get; set; }

        /// <summary>
        /// The Cobalt session manager property
        /// </summary>
        private CobaltSessionManager cobaltSessionManager;

        /// <summary>
        /// Initializes new instance of <see cref="BaseApiRegressionTest{TTestContextType}"/>
        /// </summary>
        public BaseApiRegressionTest()
        {
            this.ApiExecutionSettings = this.ApiExecutionSettings.SetFlags(
                ApiExecutionFlags.AllowCredentialManagement,
                ApiExecutionFlags.AllowAutoSignOn,
                ApiExecutionFlags.AllowAutoSignOff,
                ApiExecutionFlags.AllowContextLoggingOnSetUp,
                ApiExecutionFlags.AllowContextLoggingOnCleanUp);

            this.DefaultCobaltProduct = this.TestExecutionContext.Products.FirstOrDefault();

            string isFedRamp = this.Settings.GetValue(EnvironmentConstants.IsFedRamp);
            this.DefaultCobaltProduct = isFedRamp != null && isFedRamp.ToLower().Equals("yes")
                                            ? TestConfigurationRepository.FedRampInstance.FindProduct(
                                                CobaltProductId.WestlawNext)
                                            : this.DefaultCobaltProduct;
        }

        /// <summary>
        /// The initialize test case.
        /// </summary>
        [TestInitialize]
        public override void InitializeTestCase()
        {
            var handlers = new List<Action>();

            // Superior initialize call
            base.InitializeTestCase();

            // User account management block
            if (this.ApiExecutionSettings.IsFlagDependencyMet(ApiFlagDependencyMap.AllowCredentialManagement))
            {
                handlers.Add(this.OnManageCredential);
            }

            // Logging block
            if (this.ApiExecutionSettings.IsFlagDependencyMet(ApiFlagDependencyMap.AllowContextLoggingOnSetUp))
            {
                handlers.Add(this.LogExecutionStart);
            }

            // API precondition block
            if (this.ApiExecutionSettings.IsFlagDependencyMet(ApiFlagDependencyMap.AllowApiPreconditions))
            {
                handlers.Add(this.PerformApiPreconditions);
            }

            // Test client initialization block,
            if (this.ApiExecutionSettings.IsFlagDependencyMet(ApiFlagDependencyMap.AllowAutoSignOn))
            {
                handlers.Add(this.StartCobaltSession);
            }

            // Execution of the chained pre-condition steps.
            ExecutionResultType execResult = this.ProcessEnvironmentPreparationHandlers(handlers, true);

            // Terminating a test if at least one preparation procedure has failed unexpectedly.
            if (execResult != ExecutionResultType.Success)
            {
                this.CleanupTestCase();
            }

        }

        /// <summary>
        /// Cleans up the test case.
        /// </summary>
        [TestCleanup]
        public sealed override void CleanupTestCase()
        {
            var handlers = new List<Action>();

            // Logging block
            if (this.ApiExecutionSettings.IsFlagDependencyMet(ApiFlagDependencyMap.AllowContextLoggingOnCleanUp))
            {
                handlers.Add(this.LogExecutionEnd);
            }

            // Test client finalization block
            if (this.ApiExecutionSettings.IsFlagDependencyMet(ApiFlagDependencyMap.AllowAutoSignOff))
            {
                handlers.Add(this.SignOffSession);
            }

            // API post-condition block
            if (this.ApiExecutionSettings.IsFlagDependencyMet(ApiFlagDependencyMap.AllowApiPostconditions))
            {
                handlers.Add(this.PerformApiPostconditions);
            }

            // User account finalization block
            handlers.Add(CredentialPool.DisposeOfUsers);

            // Execution of the chained post-condition steps and recording the results.
            this.ProcessEnvironmentPreparationHandlers(handlers, false);
            this.QualityTestCase.WriteToConsole();

            // Superior clean-up call block. Finalize the checks
            base.CleanupTestCase();

        }

        /// <summary>
        /// Specifies how to reserve a user (users) for the test
        /// </summary>
        protected virtual void OnManageCredential()
        {
            this.DefaultUserCredential = new UserDbCredential(
                this.TestContext,
                PasswordVertical.WlnModuleAndFeatureRegression,
                this.GetPasswordPool());

            CredentialPool.RegisterUser(this.DefaultUserCredential.ToOnePassUserInfo());
        }

        /// <summary>
        ///  Creates Cobalt session based on test execution context.
        /// </summary>
        protected virtual void StartCobaltSession()
        {
            if (!this.TestExecutionContext.ForceRouting)
            {
                this.CobaltSessionManager.StartSession(this.DefaultUserCredential.ClientId);
            }
            else
            {
                this.CobaltSessionManager.StartSession(
                    this.Settings.GetValues("IACS_ON").ToList(),
                    this.Settings.GetValues("IACS_OFF").ToList(),
                    this.Settings.GetValues("FACS_ON").ToList(),
                    this.Settings.GetValues("FACS_OFF").ToList());
            }

        }

        /// <summary>
        /// Returns the password pool.
        /// </summary>
        /// <returns>The password pool.</returns>
        protected string GetPasswordPool()
        {
            string passwordPool = this.Settings.GetValue(EnvironmentConstants.PasswordPoolName);
            return !string.IsNullOrEmpty(passwordPool)
                       ? passwordPool
                       : (this.TestExecutionContext.TestEnvironment.IsLower
                              ? PasswordPool.WlnrGeneralPurposePreProdPool.GetEnumTextValue()
                              : PasswordPool.WlnrGeneralPurposeProdPool.GetEnumTextValue());
        }

        /// <summary>
        /// The get API client
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected T GetApiClient<T>()
            where T : BaseCobaltServiceClient
        {
            return ApiClientFactory.GetInstance<T>(
                this.DefaultUserCredential.ToOnePassUserInfo(),
                this.CobaltSessionManager.SessionInfo.SessionId,
                this.DefaultCobaltProduct,
                this.TestExecutionContext.TestEnvironment,
                this.CobaltSessionManager.SessionCookies);
        }

        /// <summary>
        /// Specifies the type of API precondition operations to perform before the test.
        /// </summary>
        protected void PerformApiPreconditions()
        {
            if (this.ApiExecutionSettings.IsFlagDependencyMet(ApiFlagDependencyMap.AllowSuperDeleteOnSetUp)
                && this.TestExecutionContext.TestEnvironment.IsLower
                && FolderingManager.IsFolderingAvailableFor(this.DefaultCobaltProduct?.Id))
            {
                SafeMethodExecutor.Execute(this.CleanUpFolders).LogDetails();
            }

            if (this.ApiExecutionSettings.IsFlagDependencyMet(ApiFlagDependencyMap.AllowApiPreconditionRoutines))
            {
                this.PerformApiPreconditionRoutines();
            }
        }

        /// <summary>
        /// Logs the test execution context  
        /// </summary>
        protected virtual void LogExecutionStart()
        {
            this.TestExecutionContext.PrintTestContext();

            // Logs info about users that are reserved by the test
            if (this.ApiExecutionSettings.HasFlag(ApiExecutionFlags.AllowCredentialManagement))
            {
                SafeMethodExecutor.Execute(this.DisplayPasswordInfo).LogDetails();
            }
        }

        /// <summary>
        /// Prints out current password info for checked out passwords to confirm that no concurrency issues have occurred.
        /// </summary>
        protected void DisplayPasswordInfo()
        {
            foreach (IUserInfo userInfo in CredentialPool.GetAllRegisteredUsers())
            {
                var userDbInfo = userInfo as UserDbCredential;
                if (userDbInfo != null)
                {
                    Logger.LogInfo("Checked out password:");
                    BaseApiRegressionTest<TTestContextType>.PrintPasswordInfo(userDbInfo.PasswordInfo);
                }
                else
                {
                    Logger.LogError("There is no required info for user: " + userInfo.UserName);
                }
            }
        }

        /// <summary>
        /// Prints the current test's session id and session info tool URL.
        /// </summary>
        protected void PrintSessionId()
        {
            var userInfo = CredentialPool.GetFirstOrDefaultUser<IOnePassUserInfo>();
            string sessionId = string.Empty;
            Logger.LogInfo(this.GetApiClient<CobaltServicesClient>().BaseEndpointURL);

            if (userInfo != null)
            {
                if (!string.IsNullOrEmpty(userInfo.UniqueKey))
                {
                    SafeMethodExecutor.Execute(
                        () =>
                            sessionId =
                                this.CobaltSessionManager.SessionInfo.SessionId);
                }
                else
                {
                    Logger.LogError($"Unable to retrieve session, Prism GUID not set for user '{userInfo.UserName}'");
                }
            }
            else
            {
                Logger.LogError("User is not defined.'");
            }

            if (!string.IsNullOrEmpty(sessionId))
            {
                Logger.LogInfo("Session ID: " + sessionId);
                Logger.LogInfo(
                    this.TestExecutionContext.TestEnvironment.Id.GetSessionInfoUrl(sessionId) + Environment.NewLine);
            }
        }

        /// <summary>
        /// Performs common API preconditions that are needed for the test,
        /// for example, creation of folders via API calls.
        /// </summary>
        protected virtual void PerformApiPreconditionRoutines()
        {
        }

        /// <summary>
        /// Logs the test execution info
        /// </summary>
        protected virtual void LogExecutionEnd()
        {
            this.PrintSessionId();
        }

        /// <summary>
        /// Specifies the API post-condition calls to perform after the test,
        /// for example, clearing some test data via API calls.
        /// </summary>
        protected virtual void PerformApiPostconditionRoutines()
        {
        }

        /// <summary>
        /// Specifies the type of API post-condition operations to perform after the test.
        /// </summary>
        protected virtual void PerformApiPostconditions()
        {
            if (this.ApiExecutionSettings.IsFlagDependencyMet(ApiFlagDependencyMap.AllowSuperDeleteOnCleanUp)
                && this.TestExecutionContext.TestEnvironment.IsLower
                && FolderingManager.IsFolderingAvailableFor(this.DefaultCobaltProduct?.Id))
            {
                SafeMethodExecutor.Execute(this.CleanUpFolders).LogDetails();
            }

            if (this.ApiExecutionSettings.IsFlagDependencyMet(ApiFlagDependencyMap.AllowApiPostconditionRoutines))
            {
                this.PerformApiPostconditionRoutines();
            }
        }

        /// <summary>
        /// Processes the chained test environment handlers in pre-condition and post-condition blocks. Failures detected are embedded into virtual quality checks.
        /// </summary>
        /// <param name="handlers">The chain of handlers to invoke.</param>
        /// <param name="stopExecutionOnFailure">Instructs whether to stop execution of the chain of handlers on a failure or to proceed.</param>
        /// <returns>The <see cref="ExecutionResultType"/>.</returns>
        private ExecutionResultType ProcessEnvironmentPreparationHandlers(
            IList<Action> handlers,
            bool stopExecutionOnFailure)
        {
            var execResult = ExecutionResultType.Success;

            for (int i = 0;
                 (!stopExecutionOnFailure || execResult == ExecutionResultType.Success) && i < handlers.Count;
                 i++)
            {
                ExecutionResult intermExecResullt = SafeMethodExecutor.Execute(handlers[i]);

                intermExecResullt.LogDetails();

                if (intermExecResullt.ResultType != ExecutionResultType.Success)
                {
                    // ReSharper disable once BitwiseOperatorOnEnumWithoutFlags
                    execResult |= intermExecResullt.ResultType;

                    this.QualityTestCase.AddQualityChecks(
                        new QualityCheck($"{this.GetType().Name}::{handlers[i].Method.Name} operation succeeded")
                        {
                            QualityCheckType = QualityCheckType.Verification,
                            Outcome = Outcome.Failed,
                            DateTime = DateTime.UtcNow,
                            Message = "Operation failed due to: " + intermExecResullt.Details
                        });
                }
            }

            return execResult;
        }

        /// <summary>
        /// Prints out password info.
        /// </summary>
        /// <param name="passwordInfo">The password info.</param>
        private static void PrintPasswordInfo(PasswordInfo passwordInfo)
        {
            if (passwordInfo == null)
            {
                Logger.LogInfo("Password information is undefined.");
            }
            else
            {
                Logger.LogInfo("OnePass Username:\t" + passwordInfo.OnePassUsername);
                Logger.LogInfo("Available:\t\t\t" + passwordInfo.Available);
                Logger.LogInfo("Checked out by:\t\t" + passwordInfo.CheckedOutBy);
                Logger.LogInfo("Checkout Date Time:\t" + passwordInfo.CheckoutDateTime);
                Logger.LogInfo("Expiration Date Time:\t" + passwordInfo.ExpirationDateTime);
                Logger.LogInfo("Test Run ID:\t\t\t" + passwordInfo.TestRunId);
                Logger.LogInfo("Test Using Password:\t" + passwordInfo.TestUsingPwd);
            }
        }

        /// <summary>
        /// The sign Off session
        /// </summary>
        private void SignOffSession()
        {
            this.CobaltSessionManager.KillSession();
        }

        /// <summary>
        /// Call SuperDelete endpoint for the user: clean up all folders and sharing invitations.
        /// </summary>
        private void CleanUpFolders()
        {
            foreach (IOnePassUserInfo user in CredentialPool.GetAllRegisteredUsers<IOnePassUserInfo>())
            {
                if (string.IsNullOrEmpty(user.PrismGuid))
                {
                    throw new ArgumentException(
                        $"The user info for user: '{user.UserName}' is not complete: PrismGUID filed is empty");
                }

                FolderingManager.PerformSuperDelete(
                    user,
                    this.TestExecutionContext.TestEnvironment,
                    this.DefaultCobaltProduct);
            }
        }

    }
}