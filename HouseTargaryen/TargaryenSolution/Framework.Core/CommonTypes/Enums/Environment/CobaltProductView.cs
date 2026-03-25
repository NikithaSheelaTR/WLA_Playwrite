namespace Framework.Core.CommonTypes.Enums.Environment
{
    using System;
    using System.Collections.Generic;

    using Framework.Core.CommonTypes.Enums.Setup;

    /// <summary>
    /// CobaltProductView
    /// </summary>
    public class CobaltProductView : IProductView
    {
        /// <summary>
        /// None
        /// </summary>
        public static CobaltProductView NONE = new CobaltProductView("");

        /// <summary>
        /// WestlawNext_Patron
        /// </summary>
        public static CobaltProductView WESTLAWNEXT_PATRON = new CobaltProductView("WESTLAWNEXT_PATRON");

        /// <summary>
        /// WestlawNext_Campus
        /// </summary>
        public static CobaltProductView WESTLAWNEXT_CAMPUS = new CobaltProductView("WESTLAWNEXT_CAMPUS");

        /// <summary>
        /// WestlawNext_Mobile
        /// </summary>
        public static CobaltProductView WESTLAWNEXT_MOBILE = new CobaltProductView("WESTLAWNEXT_MOBILE");

        /// <summary>
        /// WestlawNext_OpenWeb
        /// </summary>
        public static CobaltProductView WESTLAWNEXT_OPENWEB = new CobaltProductView("WESTLAWNEXT_OPENWEB");

        /// <summary>
        /// TaxNetPro_Patron
        /// </summary>
        public static CobaltProductView TAXNETPRO_PATRON = new CobaltProductView("TAXNETPRO_PATRON");

        /// <summary>
        /// TaxNetPro_Mass_Emailer
        /// </summary>
        public static CobaltProductView TAXNETPRO_MASS_EMAILER = new CobaltProductView("TAXNETPRO_MASS_EMAILER");

        /// <summary>
        /// FormBuilder_Client_Portal
        /// </summary>
        public static CobaltProductView FORMBUILDER_CLIENT_PORTAL = new CobaltProductView("FORMBUILDER_CLIENT_PORTAL");

        /// <summary>
        /// FormBuilder_Client_Portal_Uk
        /// </summary>
        public static CobaltProductView FORMBUILDER_CLIENT_PORTAL_UK = new CobaltProductView("FORMBUILDER_CLIENT_PORTAL_UK");

        /// <summary>
        /// Carswel_Forms
        /// </summary>
        public static CobaltProductView CARSWELL_FORMS = new CobaltProductView("CARSWELL_FORMS");

        /// <summary>
        /// Collection containing all supported image filetypes
        /// </summary>
        public static List<CobaltProductView> Values = new List<CobaltProductView>
        {
            NONE,
            WESTLAWNEXT_PATRON,
            WESTLAWNEXT_CAMPUS,
            WESTLAWNEXT_MOBILE,
            WESTLAWNEXT_OPENWEB,
            TAXNETPRO_PATRON,
            TAXNETPRO_MASS_EMAILER,
            FORMBUILDER_CLIENT_PORTAL,
            FORMBUILDER_CLIENT_PORTAL_UK,
            CARSWELL_FORMS
        };

        /// <summary>
        /// Name
        /// </summary>
        private string Name;

        /// <summary>
        /// CobaltProductView
        /// </summary>
        /// <param name="name">The name.</param>
        private CobaltProductView(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// GetName
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetName() => this.Name;

        /// <summary>
        /// Loops through all of the CobaltProductViews and returns the one that matches the specified name.
        /// </summary>
        /// <param name="name">The CobaltProductView corresponding to the specified name.</param>
        /// <returns>Thrown if no CobaltProductView could be found matching the specified name.</returns>
        public static CobaltProductView FromName(string name)
        {
            foreach (var cobaltProductView in Values)
            {
                if (((name == null || name.Equals("")) && cobaltProductView.Equals(NONE)) ||
                    cobaltProductView.GetName().ToLower().Equals(name.ToLower()))
                {
                    return cobaltProductView;
                }
            }
            throw new ArgumentException("No CobaltProductView enum could be found corresponding to the name: \'" + name + "\'.");
        }
    }
}
