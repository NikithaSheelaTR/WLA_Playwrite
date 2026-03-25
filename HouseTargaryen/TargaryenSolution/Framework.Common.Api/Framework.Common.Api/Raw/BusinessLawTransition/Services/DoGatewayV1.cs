// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DOGatewayV1.cs" company="Thomson Reuters">
//   Copyright 2015: Thomson Reuters. All Rights Reserved. Proprietary
//   and Confidential information of Thomson Reuters. Disclosure, Use or
//   Reproduction without the written authorization of Thomson Reuters is
//   prohibited. 
// </copyright>
// <summary>
//   Defines the DOGatewayV1 type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Framework.Common.Api.Raw.BusinessLawTransition.Services
{
    using System;
    using System.Net;

    using Framework.Core.DataModel.Configuration.Proxies;
    using Framework.Core.Utils.Configuration;

    /// <summary>
    /// The Data Orchestration Gateway v1.
    /// </summary>
    public class DoGatewayV1
    {
        /// <summary>
        /// Gets or sets the environment.
        /// </summary>
        protected EnvironmentInfo Environment { get; set; }

        /// <summary>
        /// Gets or sets the headers.
        /// </summary>
        protected WebHeaderCollection Headers { get; set; }

        /// <summary>
        /// Gets or sets the host url.
        /// </summary>
        protected string HostUrl { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DoGatewayV1"/> class. 
        /// Constructs the object
        /// </summary>
        /// <param name="environment">Environment under test.</param>
        /// <param name="token">The token.</param>
        public DoGatewayV1(EnvironmentInfo environment, string token = null)
        {
            this.Environment = environment;
            this.HostUrl = this.Environment.Id.GetDoGatewayUrlForEnv() + "/DOGateway/v1";

            this.Headers = new WebHeaderCollection
                               {
                                   { "X-Cobalt-Security-UDS", this.Environment.Id.GetUdsUrlForEnv() },
                                   {
                                       "x-cobalt-security-sessionid",
                                       Guid.NewGuid().ToString().Replace("-", string.Empty)
                                   },
                                   { "x-cobalt-security-userguid", "test-page-user" },
                                   { "x-cobalt-product-container", "test" },
                                   { "x-trmr-product", "test" },
                                   { "x-trmr-businessunit", "test" },
                                   { "pwd", "F8p9getb" }
                               };

            // FMR where do we get this?

            // Headers.Add("Content-Type", "application/json");
            if (!string.IsNullOrEmpty(token))
            {
                this.Headers.Add("Security-Token", token);
            }

            // FMR what are these for?
            this.Headers.Add("x-cobalt-ptid", "professionals-integration-test-ptid");
        }
    }
}