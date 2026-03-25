// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SecurityTokenRequest.cs" company="Thomson Reuters">
//   Copyright 2015: Thomson Reuters. All Rights Reserved. Proprietary
//   and Confidential information of Thomson Reuters. Disclosure, Use or
//   Reproduction without the written authorization of Thomson Reuters is
//   prohibited. 
// </copyright>
// <summary>
//   The security token request.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Framework.Common.Api.Raw.BusinessLawTransition.Requests
{
    using System;
    using System.Web.Script.Serialization;

    /// <summary>
    /// The security token request.
    /// </summary>
    public class SecurityTokenRequest : IRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityTokenRequest"/> class.
        /// </summary>
        /// <param name="userName">
        /// The _username.
        /// </param>
        /// <param name="password">
        /// The _password.
        /// </param>
        public SecurityTokenRequest(string userName = null, string password = null)
        {
            this.orchestration = new OrchestrationData
                                     {
                                         username =
                                             !string.IsNullOrEmpty(userName)
                                                 ? userName
                                                 : "845E5AFD41DFF3B11BEB046C73D54497A2D19891DCA6042C9B67D90D61A2B4D3",
                                         password =
                                             !string.IsNullOrEmpty(password)
                                                 ? password
                                                 : "50E044A008B9CBBF13E5D64BC97883C436CB03F18D980D6371505ED022BD55CE"
                                     };

            this.expiry = new Expiry { type = "temporal" /*, duration = "2015-08-31T12:00:00-05:00"*/ };
        }

        /// <summary>
        /// Gets or sets the expiry.
        /// </summary>
        public Expiry expiry { get; set; }

        /// <summary>
        /// Gets or sets the orchestration.
        /// </summary>
        public OrchestrationData orchestration { get; set; }

        /// <summary>
        /// The get request body.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetRequestBody()
        {
            return new JavaScriptSerializer().Serialize(this.orchestration);
        }

        /// <summary>
        /// The expiry.
        /// </summary>
        public class Expiry
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Expiry"/> class.
            /// </summary>
            public Expiry()
            {
                DateTime currentDateTime = DateTime.Now;

                currentDateTime = currentDateTime.AddDays(7);
                this.duration = currentDateTime.ToString("yyyy-MM-ddThh:mm:sszzz");
            }

            /// <summary>
            /// Gets or sets the duration.
            /// </summary>
            public string duration { get; set; }

            /// <summary>
            /// Gets or sets the type.
            /// </summary>
            public string type { get; set; }
        }

        /// <summary>
        /// The orchestration data.
        /// </summary>
        public class OrchestrationData
        {
            /// <summary>
            /// Gets or sets the password.
            /// </summary>
            public string password { get; set; }

            /// <summary>
            /// Gets or sets the username.
            /// </summary>
            public string username { get; set; }
        }
    }
}