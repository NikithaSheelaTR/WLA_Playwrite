namespace Framework.Core.Cobalt.Uds
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;

    using Framework.Core.CommonTypes.Enums.Environment;
    using Framework.Core.Net;
    using Newtonsoft.Json;



    /// <summary>
    /// Provides the ability to create, retrieve, update, and delete UDS Session information.  Additionally, provides capabilities for UDS Session Bindings.
    /// </summary>
    public class SessionService
    {
        private readonly string hostUrl;

        /// <summary>
        /// Creates an instance of the service responsible for communicating with UDS <c>authsession</c> endpoints. The <c>hostUrl</c> differs by environment and by product.
        /// </summary>
        /// <param name="hostUrl">A host URL for UDS, e.g. <c>http://uds.int.next.qed.westlaw.com</c>.</param>
        public SessionService(string hostUrl)
        {
            this.hostUrl = hostUrl + "/UDS";
        }

        /// <summary>
        /// Generates a SessionServiceonnalt.Uds.Session"/> for use with methods within the <see cref="SessionService"/>. An <see cref="ArgumentNullException"/> will be thrown if any of the fields other than <c>emulatedPrismUsername</c> or <c>emulatedPrismPassword</c> are <c>null</c>. Additionally, an <see cref="ArgumentOutOfRangeException"/> will be thrown if either <c>emulatedPrismUsername</c> or <c>emulatedPrismPassword</c> is an empty or blank string. Finally, an <see cref="InvalidOperationException"/> will be thrown if only one of the emulatee fields is set. Only both of the emulatee fields should be set or none at all.
        /// </summary>
        /// <param name="site">A value of <c>A</c> or <c>B</c>, but should only be <c>B</c> in the <c>CI</c> and <c>Demo</c> environments.</param>
        /// <param name="product">A value conforming with those mentioned for the {@code x-cobalt-product-container} header on the <a href="http://nsawiki.int.westgroup.com/wiki/index.php/UDS_Product_Configuration#x-cobalt-product-container">UDS Wiki</a>.</param>
        /// <param name="userClassification">A value corresponding with the user making the request, e.g. "UDS Tester" or "Search Tester".</param>
        /// <param name="prismUsername">A Prism username.</param>
        /// <param name="prismPassword">A Prism password.</param>
        /// <param name="emulatedPrismUsername">A Prism username for the emulated of the session being created.  Defaults to <c>null</c> if not provided.</param>
        /// <param name="emulatedPrismPassword">A Prism password for the emulated of the session being created.  Defaults to <c>null</c> if not provided.</param>
        /// <returns>An initialized default <see cref="Session"/>.</returns>
        public Session GenerateDefaultSession(
            string site,
            CobaltProduct product,
            string userClassification,
            string prismUsername,
            string prismPassword,
            string emulatedPrismUsername = null,
            string emulatedPrismPassword = null)
        {
            // validate input
            if (site == null)
            {
                throw new ArgumentNullException("site");
            }

            if (site.Trim().Length == 0)
            {
                throw new ArgumentOutOfRangeException("site", "The site cannot be an empty or blank string.");
            }

            if (userClassification == null)
            {
                throw new ArgumentNullException("userClassification");
            }

            if (userClassification.Trim().Length == 0)
            {
                throw new ArgumentOutOfRangeException("userClassification", "The userClassification cannot be an empty or blank string.");
            }

            if (prismUsername == null)
            {
                throw new ArgumentNullException("prismUsername");
            }

            if (prismUsername.Trim().Length == 0)
            {
                throw new ArgumentOutOfRangeException("prismUsername", "The prismUsername cannot be an empty or blank string.");
            }

            if (prismPassword == null)
            {
                throw new ArgumentNullException("prismPassword");
            }

            if (prismPassword.Trim().Length == 0)
            {
                throw new ArgumentOutOfRangeException("prismPassword", "The prismPassword cannot be an empty or blank string.");
            }

            // validate emulation inputs
            bool isEmulatedSession = false;
            if (emulatedPrismUsername != null)
            {
                if (emulatedPrismPassword == null)
                {
                    throw new InvalidOperationException("When providing emulatedPrismUsername, an emulatedPrismPassword must also be included.");
                }

                if (emulatedPrismUsername.Trim().Length == 0)
                {
                    throw new ArgumentOutOfRangeException("emulatedPrismUsername", "The emulatedPrismUsername cannot be an empty or blank string.");
                }
            }

            if (emulatedPrismPassword != null)
            {
                if (emulatedPrismUsername == null)
                {
                    throw new InvalidOperationException("When providing emulatedPrismPassword, an emulatedPrismUsername must also be included.");
                }

                if (emulatedPrismPassword.Trim().Length == 0)
                {
                    throw new ArgumentOutOfRangeException("emulatedPrismPassword", "The emulatedPrismPassword cannot be an empty or blank string.");
                }

                isEmulatedSession = true;
            }

            string productName = product.GetProductName();

            // set supplied values
            var session = new Session
            {
                Site = site,
                ProductName = productName,
                UserClassification = userClassification
            };

            var authenticationService = new AuthenticationService(this.hostUrl);
            AuthenticationInfo authenticationInfo = authenticationService.RetrieveAuthenticationInfo(prismUsername, prismPassword);

            if (authenticationInfo.PrismAuthStatusCode != 0)
            {
                throw new InvalidOperationException("Authentication Service failure occurred during construction of a UDS Session.\n\rPrismAuthStatusCode: " + authenticationInfo.PrismAuthStatusCode
                        + "\n\rPrismAuthFailureReason: " + authenticationInfo.PrismAuthFailureReason + "\n\rPlease see http://nsawiki.int.westgroup.com/wiki/index.php/Authentication_Service for more information.");
            }

            session.PrismGuid = authenticationInfo.PrismGuid;
            session.PrismAuthToken = authenticationInfo.PrismAuthToken;

            // if needed, add emulation credentials
            if (isEmulatedSession)
            {
                AuthenticationInfo emulatedAuthenticationInfo = authenticationService.RetrieveAuthenticationInfo(prismUsername, prismPassword);

                if (emulatedAuthenticationInfo.PrismAuthStatusCode != 0)
                {
                    throw new InvalidOperationException("Authentication Service failure occurred during construction of a UDS Session.\n\rPrismAuthStatusCode: " + emulatedAuthenticationInfo.PrismAuthStatusCode
                            + "\n\rPrismAuthFailureReason: " + emulatedAuthenticationInfo.PrismAuthFailureReason + "\n\rPlease see http://nsawiki.int.westgroup.com/wiki/index.php/Authentication_Service for more information.");
                }

                session.EmulateePrismGuid = emulatedAuthenticationInfo.PrismGuid;
                session.EmulateePrismAuthToken = emulatedAuthenticationInfo.PrismAuthToken;
            }

            // set all other required values
            session.SessionId = Guid.NewGuid().ToString().Replace("-", string.Empty);
            session.SessionStatus = Session.Status.Online;
            session.FirstName = "Justin";
            session.LastName = "Morneau";
            session.EmailAddress = "justin.morneau@twins.com";
            session.OnePassUserName = "JustinsOnePassUsername";
            DateTime currentDateTimePlusOneHour = DateTime.UtcNow.AddHours(1);
            session.OrphanExpiresDateTime = currentDateTimePlusOneHour;
            session.SessionExpiresDateTime = currentDateTimePlusOneHour;
            session.ExpiresReason = Session.Reason.Maintenance;
            session.Tier = 1;
            return session;
        }

        /// <summary>
        /// Creates the provided <c>session</c> in UDS. An <see cref="ArgumentNullException"/> will be thrown if the provided <c>session</c> is <c>null</c>. Additionally, an <see cref="ArgumentOutOfRangeException"/> will be thrown if either the <c>Site</c> value of <c>session</c> or the <c>ProductName</c> value of <c>session</c> are <c>null</c>, blank, or an empty string.
        /// </summary>
        /// <param name="session">A session to be created in UDS.</param>
        /// <returns>The created session in UDS.</returns>
        public Session CreateSession(Session session)
        {
            // validate input
            if (session == null)
            {
                throw new ArgumentNullException("session");
            }

            string site = session.Site;
            if (site == null || site.Trim().Length == 0)
            {
                throw new ArgumentOutOfRangeException("session", "A Site value must be set within the session to appropriately set certain headers on the request to the endpoint.");
            }

            string productName = session.ProductName;
            if (productName == null || productName.Trim().Length == 0)
            {
                throw new ArgumentOutOfRangeException("session", "A ProductName value must be set within the session to appropriately set certain headers on the request to the endpoint.");
            }

            // construct request
            string url = this.hostUrl + "/v8/authsession";
            string requestBody = session.ToString();
            var headers = new WebHeaderCollection { { "x-cobalt-product-container", productName } };
            var cookies = new CookieCollection { new Cookie("site", site.ToLower()) };
            HttpWebRequest request = HttpUtils.ConstructHttpWebRequest(
                HttpUtils.HttpMethod.Post,
                url,
                MimeType.Application.Json,
                headers,
                cookies,
                requestBody,
                Encoding.UTF8,
                MimeType.Application.Json);

            // send request to endpoint & parse response
            using (HttpWebResponse response = HttpUtils.GetHttpWebResponse(request))
            {
                HttpStatusCode statusCode = response.StatusCode;
                if (statusCode != HttpStatusCode.Created)
                {
                    if (statusCode == HttpStatusCode.ServiceUnavailable)
                    {
                        string throttleReason = response.Headers["x-cobalt-throttle-reason"];
                        throw new HttpResponseException(statusCode, "A system capacity limit has been reached: " + throttleReason);
                    }

                    string exceptionMessage = HttpUtils.HandleHttpResponseException(response, Encoding.UTF8, request.Headers, requestBody);
                    throw new HttpResponseException(statusCode, exceptionMessage);
                }

                string sessionLongToken = response.Headers["Location"].Replace("/v8/authsession/", "");
                return this.RetrieveSession(sessionLongToken, site);
            }
        }

        /// <summary>
        /// Updates the provided <c>session</c> in UDS. Ensure that the <c>Site</c> value of <c>session</c> is set to the site on which the UDS Session was originally created. If not, a <c>409 Conflict</c> exception will occur and be thrown via an <see cref="HttpResponseException"/>. An <see cref="ArgumentNullException"/> will be thrown if the provided <c>session</c> is <c>null</c>. Additionally, an <see cref="ArgumentOutOfRangeException"/> will be thrown if the <c>Site</c> value of the session is <c>null</c>, blank, or an empty string.
        /// </summary>
        /// <param name="session">A session to be updated in UDS.</param>
        public void UpdateSession(Session session)
        {
            // validate input
            if (session == null)
            {
                throw new ArgumentNullException("session");
            }

            string site = session.Site;
            if (site == null || site.Trim().Length == 0)
            {
                throw new ArgumentOutOfRangeException("session", "A Site value must be set within the session to appropriately set certain headers on the request to the endpoint.");
            }

            // construct request
            string url = this.hostUrl + "/v8/authsession/" + session.LongToken;
            string requestBody = session.ToString();
            var cookies = new CookieCollection { new Cookie("site", site.ToLower()) };
            HttpWebRequest request = HttpUtils.ConstructHttpWebRequest(HttpUtils.HttpMethod.Post, url, MimeType.Application.Json, null, cookies, requestBody, Encoding.UTF8, MimeType.Application.Json);

            // send request to endpoint and handle response
            using (HttpWebResponse response = HttpUtils.GetHttpWebResponse(request))
            {
                HttpStatusCode statusCode = response.StatusCode;
                if (statusCode != HttpStatusCode.OK)
                {
                    string exceptionMessage = HttpUtils.HandleHttpResponseException(response, Encoding.UTF8, request.Headers, requestBody);
                    throw new HttpResponseException(statusCode, exceptionMessage);
                }
            }
        }

        /// <summary>
        /// Kills the provided <c>session</c> in UDS. Ensure that the <c>Site</c> value of <c>session</c> is set to the site on which the UDS Session was originally created. If not, a <c>409 Conflict</c> exception will occur and be thrown via an <see cref="HttpResponseException"/>. An <see cref="ArgumentNullException"/> will be thrown if the provided <c>session</c> is <c>null</c>. Additionally, an <see cref="ArgumentOutOfRangeException"/> will be thrown if the <c>site</c> value of the session is <c>null</c>, blank, or an empty string.
        /// </summary>
        /// <param name="session">A session to be killed in UDS.</param>
        public void KillSession(Session session)
        {
            // validate input
            if (session == null)
            {
                throw new ArgumentNullException("session");
            }

            session.SessionStatus = Session.Status.Killed;
            session.SessionEndedReason = Session.Reason.Maintenance;
            this.UpdateSession(session);
        }

        /// <summary>
        /// Retrieves a UDS Session via the provided <c>site</c>. If retrieving a UDS Session created on the opposite site, a <c>409 Conflict</c> exception will occur and be thrown via an <see cref="HttpResponseException"/>. An <see cref="ArgumentNullException"/> will be thrown if either parameter is <c>null</c>. Additionally, an <see cref="ArgumentOutOfRangeException"/> will be thrown if either parameter is an empty or blank string.
        /// </summary>
        /// <param name="longToken">A UDS Session long token.</param>
        /// <param name="site">A value of <c>A</c> or <c>B</c>, but should only be <c>B</c> in the <c>CI</c> and <c>Demo</c> environments.</param>
        /// <returns>A UDS Session.</returns>
        public Session RetrieveSession(string longToken, string site)
        {
            // validate input
            if (longToken == null)
            {
                throw new ArgumentNullException("longToken");
            }

            if (longToken.Trim().Length == 0)
            {
                throw new ArgumentOutOfRangeException("longToken", "The longToken cannot be an empty or blank string.");
            }

            if (site == null)
            {
                throw new ArgumentNullException("site");
            }

            if (site.Trim().Length == 0)
            {
                throw new ArgumentOutOfRangeException("site", "The site cannot be an empty or blank string.");
            }

            // construct request
            string url = this.hostUrl + "/v8/authsession/" + longToken;
            var cookies = new CookieCollection { new Cookie("site", site.ToLower()) };
            HttpWebRequest request = HttpUtils.ConstructHttpWebRequest(HttpUtils.HttpMethod.Get, url, MimeType.Application.Json, null, cookies);

            // send request to endpoint & parse response
            using (HttpWebResponse response = HttpUtils.GetHttpWebResponse(request))
            {
                HttpStatusCode statusCode = response.StatusCode;
                if (statusCode == HttpStatusCode.OK)
                {
                    Stream stream = response.GetResponseStream();
                    if (stream == null)
                    {
                        throw new IOException("No Session data was received in response from UDS.");
                    }

                    using (var streamReader = new StreamReader(stream, Encoding.UTF8))
                    {
                        return SessionService.PersistJsonToSession(streamReader);
                    }
                }

                string exceptionMessage = HttpUtils.HandleHttpResponseException(response, Encoding.UTF8, request.Headers);
                throw new HttpResponseException(statusCode, exceptionMessage);
            }
        }

        /// <summary>
        /// Retrieves a list of UDS Sessions matching the provided parameters. Any parameters provided as a <c>null</c> will be ignored. However, if no parameters are set, an <see cref="InvalidOperationException"/> will be thrown. At least one parameter <i>must</i> be set. Additionally, if any string-based parameters are provided as an empty or blank string, an <see cref="ArgumentOutOfRangeException"/> will be thrown.
        /// </summary>
        /// <param name="site">A value of <c>A</c> or <c>B</c>, but should only be <c>B</c> or <c>PC1</c> in the <c>CI</c> and <c>Demo</c> environments.</param>
        /// <param name="status">A UDS Session status.</param>
        /// <param name="expireLT">All sessions with an expires date and time before this provided date/time will be included in the result set.</param>
        /// <param name="userId">A Prism GUID associated with a user.</param>
        /// <param name="product">A WestlawNext platform product.</param>
        /// <param name="pmdDataVersion">A version of the data being used from PMD.</param>
        /// <param name="cookies">Cookies you'd like to pass on the authsession/query request (for example to target a site)</param>
        /// <returns>A list of UDS Sessions matching the query parameters.</returns>
        public IList<Session> RetrieveSessions(
            string site = null,
            Session.Status? status = null,
            DateTime? expireLT = null,
            string userId = null,
            string product = null,
            string pmdDataVersion = null,
            CookieCollection cookies = null)
        {
            // construct request
            string url = this.hostUrl + "/v8/authsession/query?" + SessionService.ConstructMultiSessionQueryParamUrl(site, status, expireLT, userId, product, pmdDataVersion);
            HttpWebRequest request = HttpUtils.ConstructHttpWebRequest(HttpUtils.HttpMethod.Get, url, MimeType.Application.Json, cookies: cookies);

            // send request to endpoint & parse response
            IList<string> sessionLongTokens;
            using (HttpWebResponse response = HttpUtils.GetHttpWebResponse(request))
            {
                HttpStatusCode statusCode = response.StatusCode;
                if (statusCode == HttpStatusCode.OK)
                {
                    using (var streamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        sessionLongTokens = SessionService.PersistJsonToSessionIdList(streamReader);
                    }
                }
                else
                {
                    string exceptionMessage = HttpUtils.HandleHttpResponseException(response, Encoding.UTF8, request.Headers);
                    throw new HttpResponseException(statusCode, exceptionMessage);
                }
            }

            // populate and return session list
            IList<Session> sessions = new List<Session>();
            if (site != null)
            {
                foreach (string sessionLongToken in sessionLongTokens)
                {
                    sessions.Add(this.RetrieveSession(sessionLongToken, site));
                }
            }
            else
            {
                foreach (string sessionLongToken in sessionLongTokens)
                {
                    try
                    {
                        sessions.Add(this.RetrieveSession(sessionLongToken, "B"));
                    }
                    catch (HttpResponseException)
                    {
                        try
                        {
                            sessions.Add(this.RetrieveSession(sessionLongToken, "A"));
                        }
                        catch (HttpResponseException)
                        {
                            sessions.Add(this.RetrieveSession(sessionLongToken, "PC1"));
                        }
                    }
                }
            }

            return sessions;
        }

        /// <summary>
        /// Retrieves a count of UDS Sessions matching the provided parameters. Any parameters provided as a <c>null</c> will be ignored. However, if no parameters are set, an <see cref="InvalidOperationException"/> will be thrown. At least one parameter <i>must</i> be set. Additionally, if any string-based parameters are provided as an empty or blank string, an <see cref="ArgumentOutOfRangeException"/> will be thrown.
        /// </summary>
        /// <param name="site">A value of <c>A</c> or <c>B</c>, but should only be <c>B</c> in the <c>CI</c> and <c>Demo</c> environments.</param>
        /// <param name="status">A UDS Session status.</param>
        /// <param name="expireLT">All sessions with an expires date/time before this provided date/time will be included in the result set.</param>
        /// <param name="userId">A Prism GUID associated with a user.</param>
        /// <param name="product">A WestlawNext platform product.</param>
        /// <param name="pmdDataVersion">A version of the data being used from PMD.</param>
        /// <returns>The number of UDS Sessions matching the query parameters.</returns>
        public int RetrieveSessionsCount(
            string site = null,
            Session.Status? status = null,
            DateTime? expireLT = null,
            string userId = null,
            string product = null,
            string pmdDataVersion = null)
        {
            // construct request
            string url = this.hostUrl + "/v8/authsession/querycount?" + SessionService.ConstructMultiSessionQueryParamUrl(site, status, expireLT, userId, product, pmdDataVersion);
            HttpWebRequest request = HttpUtils.ConstructHttpWebRequest(HttpUtils.HttpMethod.Get, url, MimeType.Application.Json);

            // send request to endpoint & parse response
            using (HttpWebResponse response = HttpUtils.GetHttpWebResponse(request))
            {
                HttpStatusCode statusCode = response.StatusCode;
                if (statusCode == HttpStatusCode.OK)
                {
                    Stream stream = response.GetResponseStream();
                    if (stream == null)
                    {
                        throw new IOException("No data was received in response from UDS.");
                    }

                    using (var streamReader = new StreamReader(stream, Encoding.UTF8))
                    {
                        return Convert.ToInt32(streamReader.ReadToEnd());
                    }
                }

                string exceptionMessage = HttpUtils.HandleHttpResponseException(response, Encoding.UTF8, request.Headers);
                throw new HttpResponseException(statusCode, exceptionMessage);
            }
        }

        /// <summary>
        /// Sets bindings for a UDS Session. An <see cref="ArgumentNullException"/> will be thrown if any of the parameters is <c>null</c>. Additionally, an <see cref="ArgumentOutOfRangeException"/> will be thrown if the <c>Site</c> value of <c>session</c> or the <c>SessionId</c> value of <c>session</c> is <c>null</c>, blank, or an empty string.
        /// </summary>
        /// <param name="session">An active session that has already been created in UDS.</param>
        /// <param name="sessionBindings">A set of bindings to be associated with <c>session</c>.</param>
        public void SetSessionBindings(Session session, SessionBindings sessionBindings)
        {
            // validate input
            if (session == null)
            {
                throw new ArgumentNullException("session");
            }

            string site = session.Site;
            if (site == null || site.Trim().Length == 0)
            {
                throw new ArgumentOutOfRangeException("session", "A Site value must be set within the session to appropriately set certain headers on the request to the endpoint.");
            }

            string sessionId = session.SessionId;
            if (sessionId == null || sessionId.Trim().Length == 0)
            {
                throw new ArgumentOutOfRangeException("session", "A SessionId value must be set within the session to appropriately make the request to the endpoint");
            }

            if (sessionBindings == null)
            {
                throw new ArgumentNullException("sessionBindings");
            }

            // construct request
            string url = this.hostUrl + "/v4/sessionbindings/" + sessionId;
            string requestBody = sessionBindings.ToString();
            var cookies = new CookieCollection { new Cookie("site", site.ToLower()) };
            HttpWebRequest request = HttpUtils.ConstructHttpWebRequest(
                HttpUtils.HttpMethod.Post,
                url,
                MimeType.Application.Json,
                null,
                cookies,
                requestBody,
                Encoding.UTF8,
                MimeType.Application.Json);

            // send request to endpoint and handle response
            using (HttpWebResponse response = HttpUtils.GetHttpWebResponse(request))
            {
                HttpStatusCode statusCode = response.StatusCode;
                if (statusCode != HttpStatusCode.Created)
                {
                    string exceptionMessage = HttpUtils.HandleHttpResponseException(response, Encoding.UTF8, request.Headers, requestBody);
                    throw new HttpResponseException(statusCode, exceptionMessage);
                }
            }
        }

        /// <summary>
        /// Constructs a query string URL containing the provided parameters. Any parameters provided as a <c>null</c> will be ignored. However, if no parameters are set, an <see cref="InvalidOperationException"/> will be thrown. At least one parameter <i>must</i> be set. Additionally, if any string-based parameters are provided as an empty or blank string, an <see cref="ArgumentOutOfRangeException"/> will be thrown.
        /// </summary>
        /// <param name="site">A value of <c>A</c> or <c>B</c>, but should only be <c>B</c> in the <c>CI</c> and <c>Demo</c> environments.</param>
        /// <param name="status">A UDS Session status.</param>
        /// <param name="expireLT">All sessions with an expires date/time before this provided date/time will be included in the result set.</param>
        /// <param name="userId">A Prism GUID associated with a user.</param>
        /// <param name="product">A WestlawNext platform product.</param>
        /// <param name="pmdDataVersion">A version of the data being used from PMD.</param>
        /// <returns>A query string URL containing the provided parameters.</returns>
        private static string ConstructMultiSessionQueryParamUrl(
            string site = null,
            Session.Status? status = null,
            DateTime? expireLT = null,
            string userId = null,
            string product = null,
            string pmdDataVersion = null)
        {
            bool isOneFieldSet = false;
            var urlBuilder = new StringBuilder();
            if (site != null)
            {
                if (site.Trim().Length == 0)
                {
                    throw new ArgumentOutOfRangeException("site", "The site cannot be an empty or blank string.");
                }

                isOneFieldSet = true;
                urlBuilder.Append("site=" + site + "&");
            }

            if (status != null)
            {
                isOneFieldSet = true;
                urlBuilder.Append("status=" + status.ToString().ToUpper() + "&");
            }

            if (expireLT != null)
            {
                isOneFieldSet = true;
                urlBuilder.Append("expireLT=" + ((DateTime)expireLT).ToString("o") + "Z" + "&");
            }

            if (userId != null)
            {
                if (userId.Trim().Length == 0)
                {
                    throw new ArgumentOutOfRangeException("userId", "The userId cannot be an empty or blank string.");
                }

                isOneFieldSet = true;
                urlBuilder.Append("userId=" + userId + "&");
            }

            if (product != null)
            {
                if (product.Trim().Length == 0)
                {
                    throw new ArgumentOutOfRangeException("product", "The product cannot be an empty or blank string.");
                }

                isOneFieldSet = true;
                urlBuilder.Append("product=" + product + "&");
            }

            if (pmdDataVersion != null)
            {
                if (pmdDataVersion.Trim().Length == 0)
                {
                    throw new ArgumentOutOfRangeException("pmdDataVersion", "The pmdDataVersion cannot be an empty or blank string.");
                }

                isOneFieldSet = true;
                urlBuilder.Append("pmdDataVersion=" + pmdDataVersion + "&");
            }

            if (isOneFieldSet == false)
            {
                throw new InvalidOperationException("At least one UDS Session related parameter must be valued.");
            }

            string url = urlBuilder.ToString();
            return url.Substring(0, url.Length - 1);
        }

        /// <summary>
        /// Deserializes a JSON string into a UDS Session. A <see cref="MissingFieldException"/> is thrown if an unexpected field is encountered in the JSON.
        /// </summary>
        /// <param name="jsonData">A serialized form of a UDS Session.</param>
        /// <returns>A UDS Session.</returns>
        private static Session PersistJsonToSession(StreamReader jsonData)
        {
            using (JsonReader jsonReader = new JsonTextReader(jsonData))
            {
                jsonReader.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                var session = new Session();
                jsonReader.Read();

                while (jsonReader.TokenType != JsonToken.EndObject)
                {
                    if (jsonReader.TokenType == JsonToken.PropertyName)
                    {
                        string fieldName = (string)jsonReader.Value;
                        jsonReader.Read();
                        switch (fieldName)
                        {
                            case "SessionId":
                                session.SessionId = (string)jsonReader.Value;
                                break;
                            case "PrismGuid":
                                session.PrismGuid = (string)jsonReader.Value;
                                break;
                            case "PrismAuthToken":
                                session.PrismAuthToken = (string)jsonReader.Value;
                                break;
                            case "Site":
                                session.Site = (string)jsonReader.Value;
                                break;
                            case "Status":
                                try
                                {
                                    session.SessionStatus = (Session.Status)Enum.Parse(typeof(Session.Status), (string)jsonReader.Value, true);
                                }
                                catch (ArgumentException)
                                {
                                    session.SessionStatus = Session.Status.Notset;
                                }

                                break;
                            case "ExpiresReason":
                                try
                                {
                                    session.ExpiresReason = (Session.Reason)Enum.Parse(typeof(Session.Reason), (string)jsonReader.Value, true);
                                }
                                catch (ArgumentException)
                                {
                                    session.ExpiresReason = Session.Reason.Notset;
                                }

                                break;
                            case "UserClassification":
                                session.UserClassification = (string)jsonReader.Value;
                                break;
                            case "FirstName":
                                session.FirstName = (string)jsonReader.Value;
                                break;
                            case "LastName":
                                session.LastName = (string)jsonReader.Value;
                                break;
                            case "EmailAddress":
                                session.EmailAddress = (string)jsonReader.Value;
                                break;
                            case "OnePassUserName":
                                session.OnePassUserName = (string)jsonReader.Value;
                                break;
                            case "CreatedDateTime":
                                session.CreatedDateTime = (DateTime)jsonReader.Value;
                                break;
                            case "ExpiresDateTime":
                                session.ExpiresDateTime = (DateTime)jsonReader.Value;
                                break;
                            case "SessionExpiresDateTime":
                                session.SessionExpiresDateTime = (DateTime)jsonReader.Value;
                                break;
                            case "OrphanExpiresDateTime":
                                session.OrphanExpiresDateTime = (DateTime)jsonReader.Value;
                                break;
                            case "ProductName":
                                session.ProductName = (string)jsonReader.Value;
                                break;
                            case "Tier":
                                session.Tier = (int)((long)jsonReader.Value);
                                break;
                            case "LongToken":
                                session.LongToken = (string)jsonReader.Value;
                                break;
                            case "SessionEndedDateTime":
                                session.SessionEndedDateTime = (DateTime)jsonReader.Value;
                                break;
                            case "SessionEndedReason":
                                try
                                {
                                    session.SessionEndedReason = (Session.Reason)Enum.Parse(typeof(Session.Reason), (string)jsonReader.Value, true);
                                }
                                catch (ArgumentException)
                                {
                                    session.SessionEndedReason = Session.Reason.Notset;
                                }

                                break;
                            case "SeamlessAuthenticationToken":
                                session.SeamlessAuthenticationToken = (string)jsonReader.Value;
                                break;
                            case "EmulateePrismGuid":
                                session.EmulateePrismGuid = (string)jsonReader.Value;
                                break;
                            case "EmulateePrismAuthToken":
                                session.EmulateePrismAuthToken = (string)jsonReader.Value;
                                break;
                            case "SessionBasedPreferences":
                                session.SessionBasedPreferences = (bool)jsonReader.Value;
                                break;
                            case "PmdDataVersion":
                                session.PmdDataVersion = (string)jsonReader.Value;
                                break;
                            case "SessionSource":
                                session.SessionSource = (string)jsonReader.Value;
                                break;
                            case "ServiceType":
                                session.ServiceType = (string)jsonReader.Value;
                                break;
                            case "PrismRegistrationKey":
                                session.PrismRegistrationKey = (string)jsonReader.Value;
                                break;
                            case "PaymentType":
                                try
                                {
                                    session.SessionPaymentType = (Session.PaymentType)Enum.Parse(typeof(Session.PaymentType), (string)jsonReader.Value, true);
                                }
                                catch (ArgumentException e)
                                {
                                    throw new ArgumentException($"Source response contains '{(string)jsonReader.Value}' value which can not be parsed to 'PaymentType' enumeration.", e);
                                }

                                break;
                            case "OnePassProductName":
                                session.OnePassProductName = (string)jsonReader.Value;
                                break;
                            case "ProductView":
                                session.ProductView = (string)jsonReader.Value;
                                break;
                            case "IpAddress":
                                session.IpAddress = (string)jsonReader.Value;
                                break;
                            case "UserCategory":
                                session.UserCategory = (string)jsonReader.Value;
                                break;
                            case "IntegrationInfo":
                                session.IntegrationInfo = (string)jsonReader.Value;
                                break;
                            case "BillingMethod":
                                session.BillingMethod = (string)jsonReader.Value;
                                break;
                            default:
                                Console.Error.WriteLine("Warning - Field cannot be parsed into a UDS Session: '" + fieldName + "'.");
                                break;
                        }
                    }

                    jsonReader.Read();
                }

                return session;
            }
        }

        /// <summary>
        /// Deserializes a JSON string into a list of UDS Session long tokens.
        /// </summary>
        /// <param name="streamReader">A serialized form of a UDS Session long token list.</param>
        /// <returns>A list of UDS Session long tokens.</returns>
        private static IList<string> PersistJsonToSessionIdList(StreamReader streamReader)
        {
            using (var jsonReader = new JsonTextReader(streamReader))
            {
                IList<string> sessionLongTokens = new List<string>();
                jsonReader.Read();
                jsonReader.Read(); // skip StartArray token

                while (jsonReader.TokenType != JsonToken.EndArray)
                {
                    sessionLongTokens.Add((string)jsonReader.Value);
                    jsonReader.Read();
                }

                return sessionLongTokens;
            }
        }
    }
}