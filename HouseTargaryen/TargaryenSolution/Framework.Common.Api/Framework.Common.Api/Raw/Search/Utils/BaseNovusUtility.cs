namespace Framework.Common.Api.Raw.Search.Utils
{
    using System.IO;

    using Framework.Common.Api.Raw.Search.Enums;

    using Thomson.Novus.ProductAPI;

    /// <summary>
    /// A utility class outside of the page object model, used to query Novus directly
    /// in order to preform verifications that the information displayed in the UI
    /// is actually correct.
    /// </summary>
    public class BaseNovusUtility
    {
        private const NovusEnvironment DefaultNovusEnvironment = NovusEnvironment.Prod;

        private const string DefaultProductName = "Cobalt";

        private const string DefaultRouteTag = "cobalt";

        private const string DefaultUserId = "Search Module Regression";

        private const bool DefaultUseXms = true;

        /// <summary>
        /// Gets a Novus object using default values; provided for convenience
        /// </summary>
        /// <returns>a Novus object created using default values specifed above</returns>
        protected Novus GetDefaultNovusObject()
        {
            return this.GetNovusObject(
                DefaultUseXms,
                DefaultNovusEnvironment,
                DefaultProductName,
                DefaultRouteTag,
                DefaultUserId);
        }

        /// <summary>
        /// Returns a valid value to specify for Novus Environment when calling Novus
        /// </summary>
        /// <param name="env">the NovusEnvironment to use when calling Novus</param>
        /// <returns>the correct string version to use when calling Novus</returns>
        protected string GetNovusEnvironment(NovusEnvironment env)
        {
            string novusEnv;
            switch (env)
            {
                case NovusEnvironment.Prod:
                    novusEnv = "novusaws:prod";
                    break;
                default:
                    throw new InvalidDataException("NovusEnvironment " + env + " is not supported yet");
            }

            return novusEnv;
        }

        /// <summary>
        /// Gets a Novus object, which is needed for retrieving info from Novus
        /// </summary>
        /// <param name="useXms">should always be true</param>
        /// <param name="novusEnv">the novus environment to query</param>
        /// <param name="productName">the product name to query as</param>
        /// <param name="routeTag">the correct route tag to use</param>
        /// <param name="userId">the user to call as</param>
        /// <returns>a valid Novus object that can be used to retrieve info from Novus</returns>
        protected Novus GetNovusObject(
            bool useXms,
            NovusEnvironment novusEnv,
            string productName,
            string routeTag,
            string userId)
        {
            Novus novus = new Novus(useXms);
            string novusEnviroment = this.GetNovusEnvironment(novusEnv);
            novus.SetQueueCriteria(null, novusEnviroment);
            novus.ProductName = productName;
            novus.RouteTag = routeTag;
            novus.UserId = userId;
            novus.UseLatestPit();
            return novus;
        }
    }
}