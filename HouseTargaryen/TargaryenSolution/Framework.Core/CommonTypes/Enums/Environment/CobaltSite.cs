namespace Framework.Core.CommonTypes.Enums.Environment
{
    using System;
    using System.Collections.Generic;

    using Framework.Core.CommonTypes.Enums.Setup;

    /// <summary>
    /// CobaltSite
    /// </summary>
    public class CobaltSite : ISite
    {
        /// <summary>
        /// None
        /// </summary>
        public static CobaltSite NONE = new CobaltSite("");

        /// <summary>
        /// A
        /// </summary>
        public static CobaltSite A = new CobaltSite("A");

        /// <summary>
        /// B
        /// </summary>
        public static CobaltSite B = new CobaltSite("B");

        /// <summary>
        /// Pc1
        /// </summary>
        public static CobaltSite PC1 = new CobaltSite("PC1");

        /// <summary>
        /// Client
        /// </summary>
        public static CobaltSite CLIENT = new CobaltSite("Client");

        /// <summary>
        /// Collection containing all supported image filetypes
        /// </summary>
        public static List<CobaltSite> Values = new List<CobaltSite>
        {
            NONE,
            A,
            B,
            PC1,
            CLIENT
        };

        /// <summary>
        /// Name
        /// </summary>
        private string Name;

        /// <summary>
        /// CobaltSite
        /// </summary>
        /// <param name="name">The name.</param>
        private CobaltSite(string name)
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
        public static CobaltSite FromName(string name)
        {
            foreach (var cobaltSite in Values)
            {
                if (((name == null || name.Equals("")) && cobaltSite.Equals(NONE)) ||
                    cobaltSite.GetName().ToLower().Equals(name.ToLower()))
                {
                    return cobaltSite;
                }
            }
            throw new ArgumentException("No CobaltSite enum could be found corresponding to the name: \'" + name + "\'.");
        }
    }
}
