namespace Framework.Core.CommonTypes.Enums.Environment
{
    using System;
    using System.Collections.Generic;

    using Framework.Core.CommonTypes.Enums.Setup;

    /// <summary>
    /// Cobalt Products
    /// </summary>
    public class CobaltProduct : IProduct
    {
        /// <summary>
        /// WestlawNext
        /// </summary>
        public static CobaltProduct WESTLAWNEXT = new CobaltProduct("WESTLAWNEXT", "WestlawNext");

        /// <summary>
        /// WestlawNext_Tax
        /// </summary>
        public static CobaltProduct WESTLAWNEXT_TAX = new CobaltProduct("WESTLAWNEXT_TAX", "NEXTTAX.WESTLAW");

        /// <summary>
        /// WestlawNext_Canada
        /// </summary>
        public static CobaltProduct WESTLAWNEXT_CANADA = new CobaltProduct("WESTLAWNEXT_CANADA", "CARSWELL.WESTLAW");

        /// <summary>
        /// TaxNet_Pro
        /// </summary>
        public static CobaltProduct TAXNET_PRO = new CobaltProduct("TAXNET_PRO", "CARSWELL.TAXNETPRO");

        /// <summary>
        /// Government_WebLinks
        /// </summary>
        public static CobaltProduct GOVERNMENT_WEBLINKS = new CobaltProduct("GOVERNMENT_WEBLINKS", "GOVT.WESTLAW");

        /// <summary>
        /// Firm_Central
        /// </summary>
        public static CobaltProduct FIRM_CENTRAL = new CobaltProduct("FIRM_CENTRAL", "SLWB.WESTLAW");

        /// <summary>
        /// Concourse
        /// </summary>
        public static CobaltProduct CONCOURSE = new CobaltProduct("CONCOURSE", "ORION.WESTLAW");

        /// <summary>
        /// Westlaw_Analytics
        /// </summary>
        public static CobaltProduct WESTLAW_ANALYTICS = new CobaltProduct("WESTLAW_ANALYTICS", "ANALYTICS.WESTLAW");

        /// <summary>
        /// Forms_Builder
        /// </summary>
        public static CobaltProduct FORM_BUILDER = new CobaltProduct("FORM_BUILDER", "Forms.Westlaw");

        /// <summary>
        /// Correctional
        /// </summary>
        public static CobaltProduct CORRECTIONAL = new CobaltProduct("CORRECTIONAL", "NEXTCORRECTIONAL.WESTLAW");

        /// <summary>
        /// ProView
        /// </summary>
        public static CobaltProduct PROVIEW = new CobaltProduct("PROVIEW", "EREADER");

        /// <summary>
        /// Cayman
        /// </summary>
        public static CobaltProduct CAYMAN = new CobaltProduct("CAYMAN", "CAYMAN.THOMSONREUTERS");

        /// <summary>
        /// Drafting
        /// </summary>
        public static CobaltProduct DRAFTING = new CobaltProduct("DRAFTING", "DRAFTING.WESTLAW");

        /// <summary>
        /// Tric
        /// </summary>
        public static CobaltProduct TRIC = new CobaltProduct("TRIC", "LFD.THOMSONREUTERS");

        /// <summary>
        /// Collection containing all supported image filetypes
        /// </summary>
        public static List<CobaltProduct> Values = new List<CobaltProduct>
        {
            WESTLAWNEXT,
            WESTLAWNEXT_TAX,
            WESTLAWNEXT_CANADA,
            TAXNET_PRO,
            GOVERNMENT_WEBLINKS,
            FIRM_CENTRAL,
            CONCOURSE,
            WESTLAW_ANALYTICS,
            FORM_BUILDER,
            CORRECTIONAL,
            PROVIEW,
            CAYMAN,
            DRAFTING,
            TRIC
        };

        /// <summary>
        /// Name
        /// </summary>
        private string Name;

        /// <summary>
        /// ProductName
        /// </summary>
        private string ProductName;

        /// <summary>
        /// CobaltProduct
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="productName">The product Name.</param>
        private CobaltProduct(string name, string productName)
        {
            this.Name = name;
            this.ProductName = productName;
        }

        /// <summary>
        /// GetName
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetName() => this.Name;

        /// <summary>
        /// GetProductName
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetProductName() => this.ProductName;

        /// <summary>
        /// Loops through all of the CobaltProducts and returns the one that matches the specified name.
        /// </summary>
        /// <param name="name">The CobaltProduct corresponding to the specified name.</param>
        /// <returns>Thrown if no CobaltProduct could be found matching the specified name.</returns>
        public static CobaltProduct FromName(string name)
        {
            foreach (var cobaltModule in Values)
            {
                if (cobaltModule.GetName().ToLower().Equals(name.ToLower()))
                {
                    return cobaltModule;
                }
            }
            throw new ArgumentException("No CobaltProduct enum could be found corresponding to the name: \'" + name + "\'.");
        }
    }
}