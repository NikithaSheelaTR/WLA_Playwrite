namespace Framework.Common.Api.Endpoints
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Net;

    using Framework.Common.Api.Endpoints.Alerts;
    using Framework.Common.Api.Endpoints.CARI;
    using Framework.Common.Api.Endpoints.CaseNoteBook;
    using Framework.Common.Api.Endpoints.CobaltServices;
    using Framework.Common.Api.Endpoints.DataOrchestration;
    using Framework.Common.Api.Endpoints.DocPersist;
    using Framework.Common.Api.Endpoints.Document;
    using Framework.Common.Api.Endpoints.DoGateway;
    using Framework.Common.Api.Endpoints.Foldering;
    using Framework.Common.Api.Endpoints.Nlu;
    using Framework.Common.Api.Endpoints.Omr;
    using Framework.Common.Api.Endpoints.OnePass;
    using Framework.Common.Api.Endpoints.Report;
    using Framework.Common.Api.Endpoints.Search;
    using Framework.Common.Api.Endpoints.SearchSpellChecker;
    using Framework.Common.Api.Endpoints.TypeAhead;
    using Framework.Common.Api.Endpoints.Uds;
    using Framework.Common.Api.Endpoints.Website;
    using Framework.Common.Api.Endpoints.WestKM;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;
    using Framework.Core.DataModel.Security.Proxies;

    /// <summary>
    /// The API client factory.
    /// </summary>
    public static class ApiClientFactory
    {
        /// <summary>
        /// The map for API client implementation to Cobalt module Id.
        /// </summary>
        private static readonly Dictionary<Type, CobaltModuleId> MapForApiServices =
            new Dictionary<Type, CobaltModuleId>
            {
                { typeof(CobaltServicesClient), CobaltModuleId.CobaltServices },
                { typeof(DocPersistClient), CobaltModuleId.DocPersist },
                { typeof(DocumentClient), CobaltModuleId.Document },
                { typeof(FolderingClient), CobaltModuleId.Foldering },
                { typeof(UdsSessionClient), CobaltModuleId.Uds },
                { typeof(WebsiteClient), CobaltModuleId.Website },
                { typeof(SearchClient), CobaltModuleId.Search },
                { typeof(WestKmClient), CobaltModuleId.Website },
                { typeof(CaseNoteBookClient), CobaltModuleId.CaseNotebook },
                { typeof(NluClient), CobaltModuleId.Nlu },
                { typeof(DataOrchestrationClient), CobaltModuleId.DataOrchestration },             
                { typeof(TypeAheadClient), CobaltModuleId.TypeAhead },
                { typeof(ReportClient), CobaltModuleId.Report },
                { typeof(OnePassWebClient), CobaltModuleId.OnePassWeb},
                { typeof(AlertsClient), CobaltModuleId.Alerts },
                { typeof(OmrClient), CobaltModuleId.Omr },
                { typeof(OnePassV3Client), CobaltModuleId.OnePassV3 },
                { typeof(DoGatewayClient), CobaltModuleId.DOGateway },
                {typeof(SearchSpellCheckerClient), CobaltModuleId.SearchSpellChecker },
                {typeof(CariClient), CobaltModuleId.Cari }
            };

        /// <summary>
        /// The get instance.
        /// </summary>
        /// <param name="userInfo">
        /// The user info.
        /// </param>
        /// <param name="sessionId">
        /// The session id.
        /// </param>
        /// <param name="product">
        /// The product.
        /// </param>
        /// <param name="environment">
        /// The environment.
        /// </param>
        /// <param name="cookies">
        /// The cookies.
        /// </param>
        /// <typeparam name="TClient">
        /// </typeparam>
        /// <returns>
        /// The TClient.
        /// </returns>
        public static TClient GetInstance<TClient>(
            IOnePassUserInfo userInfo,
            string sessionId,
            CobaltProductInfo product,
            EnvironmentInfo environment,
            CookieContainer cookies = null) where TClient : BaseCobaltServiceClient
        {
            Type clientType = typeof(TClient);
            ApiClientFactory.AssertIsCompatibleClient(clientType);

            NameValueCollection securityHeaders = SecurityHeaderFactory.GetSecurityHeaders(
                MapForApiServices[clientType],
                userInfo,
                environment,
                product,
                sessionId);

            return ApiClientFactory.GetInstance<TClient>(securityHeaders, product, environment, cookies);
        }

        /// <summary>
        /// The get instance.
        /// </summary>
        /// <param name="securityHeaders">
        /// The security headers.
        /// </param>
        /// <param name="product">
        /// The product.
        /// </param>
        /// <param name="environment">
        /// The environment.
        /// </param>
        /// <param name="cookies">
        /// The cookies.
        /// </param>
        /// <typeparam name="TClient">
        /// </typeparam>
        /// <returns>
        /// The type for API client
        /// </returns>
        public static TClient GetInstance<TClient>(
            NameValueCollection securityHeaders,
            CobaltProductInfo product,
            EnvironmentInfo environment,
            CookieContainer cookies = null) where TClient : BaseCobaltServiceClient
        {
            ApiClientFactory.AssertIsCompatibleClient(typeof(TClient));

            return (TClient)Activator.CreateInstance(typeof(TClient), environment, product.Id, cookies, securityHeaders);
        }

        private static void AssertIsCompatibleClient(Type type)
        {
            if (MapForApiServices.Keys.Contains(type))
            {
                return;
            }

            throw new TypeAccessException(
                "Specified client type is not supported, you need to update the ApiClientFactory class");
        }
    }
}