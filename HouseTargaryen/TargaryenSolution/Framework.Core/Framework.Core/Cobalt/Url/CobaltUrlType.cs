namespace Framework.Core.Cobalt.Url
{
    using Framework.Core.Url;

    /// <summary>
    /// Cobalt URL Types
    /// </summary>
    public class CobaltUrlType : IUrlType
    {
        /// <summary>
        /// Default
        /// </summary>
        public static CobaltUrlType DEFAULT = new CobaltUrlType("DEFAULT");

        /// <summary>
        /// Host
        /// </summary>
        public static CobaltUrlType HOST = new CobaltUrlType("HOST");

        /// <summary>
        /// Alias
        /// </summary>
        public static CobaltUrlType ALIAS = new CobaltUrlType("ALIAS");

        /// <summary>
        /// Alias_Routing
        /// </summary>
        public static CobaltUrlType ALIAS_ROUTING = new CobaltUrlType("ALIAS_ROUTING");

        /// <summary>
        /// Routing_Page
        /// </summary>
        public static CobaltUrlType ROUTING_PAGE = new CobaltUrlType("ROUTING_PAGE");

        /// <summary>
        /// Sign_On_Page
        /// </summary>
        public static CobaltUrlType SIGN_ON_PAGE = new CobaltUrlType("SIGN_ON_PAGE");

        /// <summary>
        /// Foldering
        /// </summary>
        public static CobaltUrlType FOLDERING = new CobaltUrlType("FOLDERING");

        /// <summary>
        /// Uds
        /// </summary>
        public static CobaltUrlType UDS = new CobaltUrlType("UDS");

        /// <summary>
        /// Session_Info
        /// </summary>
        public static CobaltUrlType SESSION_INFO = new CobaltUrlType("SESSION_INFO");

        /// <summary>
        /// Cobalt_Services
        /// </summary>
        public static CobaltUrlType COBALT_SERVICES = new CobaltUrlType("COBALT_SERVICES");

        /// <summary>
        /// Legal_Services
        /// </summary>
        public static CobaltUrlType LEGAL_SERVICES = new CobaltUrlType("LEGAL_SERVICES");

        /// <summary>
        /// Uk
        /// </summary>
        public static CobaltUrlType UK = new CobaltUrlType("UK");

        /// <summary>
        /// Uk_Routing
        /// </summary>
        public static CobaltUrlType UK_ROUTING = new CobaltUrlType("UK_ROUTING");


        /// <summary>
        /// DcExit_TaxnetPro
        /// </summary>
        public static CobaltUrlType DCEXIT_TAXNETPRO = new CobaltUrlType("DCEXIT_TAXNETPRO");

        /// <summary>
        /// Name
        /// </summary>
        protected string Name;

        private CobaltUrlType(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// GetName
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetName() => this.Name;
    }
}
