namespace Framework.Common.Api.Endpoints
{
    using System;
    using Framework.Common.Api.Endpoints.Alerts;
    using Framework.Common.Api.Endpoints.Alerts.DataModel;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.DataModel.Configuration.Proxies;
    using Framework.Core.DataModel.Security.Proxies;

    /// <summary>
    /// Alerts ui manager
    /// </summary>
    public class AlertsManager
    {
        /// <summary>
        /// The environment.
        /// </summary>
        private readonly EnvironmentInfo environment;

        /// <summary>
        /// The product.
        /// </summary>
        private readonly CobaltProductInfo product;

        /// <summary>
        /// The actual test environment.
        /// </summary>
        private EnvironmentInfo actualTestEnvironment;

        /// <summary>
        /// The current session id.
        /// </summary>
        private string currentSessionId;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlertsManager"/> class.
        /// </summary>
        /// <param name="testExecutionContext">The test execution context.</param>
        /// <param name="product">The product.</param>
        public AlertsManager(TestExecutionContext testExecutionContext, CobaltProductInfo product)
        {
            this.product = product;
            this.environment = testExecutionContext.TestEnvironment;
        }

        /// <summary>
        /// Deletes all alerts
        /// </summary>
        /// <param name="user"><see cref="IOnePassUserInfo"/></param>
        /// <returns></returns>
        public void DeleteAllAlerts(IOnePassUserInfo user)
        {
            this.UpdateCurrentCallExecutionContext(user);

            if (string.IsNullOrEmpty(user.PrismGuid))
            {
                throw new Exception("User prism guid must be not null");
            }

            var alertsClient = ApiClientFactory.GetInstance<AlertsClient>(
                user,
                this.currentSessionId,
                this.product,
                this.actualTestEnvironment);

            AlertResponse alertResponse = alertsClient.RetriveAllAlertsInfo();
            alertsClient.DeleteAllAlerts(alertResponse);
        }

        /// <summary>
        /// The get session id for user.
        /// </summary>
        /// <param name="user">The user.</param>
        private void UpdateCurrentCallExecutionContext(IOnePassUserInfo user)
        {
            this.actualTestEnvironment = this.environment;
            this.currentSessionId = new CobaltSessionManager(this.environment, this.product, user).GetSessionInfo().SessionId; 
        }
    }
}
