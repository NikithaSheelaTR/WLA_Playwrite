namespace Framework.Core.CommonTypes.Enums.Environment
{
    using System;
    using System.Collections.Generic;

    using Framework.Core.CommonTypes.Enums.Setup;

    /// <summary>
    /// OnePassEnvironment enum.
    /// </summary>
    public class NovusEnvironment : IEnvironment
    {
        /// <summary>
        /// Client
        /// </summary>
        public static NovusEnvironment CLIENT = new NovusEnvironment("CLIENT", true);

        /// <summary>
        /// Prod
        /// </summary>
        public static NovusEnvironment PROD = new NovusEnvironment("PROD", false);

        // ReSharper restore InconsistentNaming

        /// <summary>
        /// Collection containing all supported NovusEnvironments
        /// </summary>
        public static List<NovusEnvironment> Values = new List<NovusEnvironment>
        {
            PROD,
            CLIENT
        };

        /// <summary>
        /// Name
        /// </summary>
        protected string Name;

        /// <summary>
        /// IsLowerEnv
        /// </summary>
        protected bool IsLowerEnv;

        /// <summary>
        /// NovusEnvironment
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="isLowerEnv">The is Lower Env.</param>
        private NovusEnvironment(string name, bool isLowerEnv)
        {
            this.Name = name;
            this.IsLowerEnv = isLowerEnv;
        }

        /// <summary>
        /// GetName
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetName() => this.Name;

        /// <summary>
        /// IsLowerEnvironment
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsLowerEnvironment() => this.IsLowerEnv;

        /// <summary>
        /// Loops through all of the NovusEnvironments and returns the one that matches the specified name.
        /// </summary>
        /// <param name="name">The NovusEnvironment corresponding to the specified name.</param>
        /// <returns>Thrown if no NovusEnvironment could be found matching the specified name.</returns>
        public static NovusEnvironment FromName(string name)
        {
            foreach (var novusEnvironment in Values)
            {
                if (novusEnvironment.GetName().ToLower().Equals(name.ToLower()))
                {
                    return novusEnvironment;
                }
            }
            throw new ArgumentException("No NovusEnvironment enum could be found corresponding to the name: \'" + name + "\'.");
        }
    }
}
