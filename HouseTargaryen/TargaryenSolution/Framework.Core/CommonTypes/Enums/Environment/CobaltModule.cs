namespace Framework.Core.CommonTypes.Enums.Environment
{
    using System;
    using System.Collections.Generic;

    using Framework.Core.CommonTypes.Enums.Setup;

    /// <summary>
    /// Cobalt Modules
    /// </summary>
    public class CobaltModule : IModule
    {
        /// <summary>
        /// Foldering
        /// </summary>
        public static CobaltModule FOLDERING = new CobaltModule("FOLDERING");

        /// <summary>
        /// Search
        /// </summary>
        public static CobaltModule SEARCH = new CobaltModule("SEARCH");

        /// <summary>
        /// Document
        /// </summary>
        public static CobaltModule DOCUMENT = new CobaltModule("DOCUMENT");

        /// <summary>
        /// RelatedInfo
        /// </summary>
        public static CobaltModule RELATEDINFO = new CobaltModule("RELATEDINFO");

        /// <summary>
        /// WebSite
        /// </summary>
        public static CobaltModule WEBSITE = new CobaltModule("WEBSITE");

        /// <summary>
        /// Alerts
        /// </summary>
        public static CobaltModule ALERTS = new CobaltModule("ALERTS");

        /// <summary>
        /// DataOrchestration
        /// </summary>
        public static CobaltModule DATAORCHESTRATION = new CobaltModule("DATAORCHESTRATION");

        /// <summary>
        /// Reports
        /// </summary>
        public static CobaltModule REPORTS = new CobaltModule("REPORTS");

        /// <summary>
        /// Uds
        /// </summary>
        public static CobaltModule UDS = new CobaltModule("UDS");

        /// <summary>
        /// FormsAssembly
        /// </summary>
        public static CobaltModule FORMSASSEMBLY = new CobaltModule("FORMSASSEMBLY");

        /// <summary>
        /// Collection containing all supported image filetypes
        /// </summary>
        public static List<CobaltModule> Values = new List<CobaltModule>
        {
            FOLDERING,
            SEARCH,
            DOCUMENT,
            RELATEDINFO,
            WEBSITE,
            ALERTS,
            DATAORCHESTRATION,
            REPORTS,
            UDS,
            FORMSASSEMBLY
        };

        /// <summary>
        /// Name
        /// </summary>
        private string Name;

        /// <summary>
        /// CobaltModule
        /// </summary>
        /// <param name="name">The name.</param>
        private CobaltModule(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// GetName
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetName() => this.Name;

        /// <summary>
        /// Loops through all of the CobaltSites and returns the one that matches the specified name.
        /// </summary>
        /// <param name="name">The CobaltSite corresponding to the specified name.</param>
        /// <returns>Thrown if no CobaltSite could be found matching the specified name.</returns>
        public static CobaltModule FromName(string name)
        {
            foreach (var cobaltModule in Values)
            {
                if (cobaltModule.GetName().ToLower().Equals(name.ToLower()))
                {
                    return cobaltModule;
                }
            }
            throw new ArgumentException("No CobaltModule enum could be found corresponding to the name: \'" + name + "\'.");
        }
    }
}