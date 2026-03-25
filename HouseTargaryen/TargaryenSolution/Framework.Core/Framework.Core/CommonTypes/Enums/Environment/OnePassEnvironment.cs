namespace Framework.Core.CommonTypes.Enums.Environment
{
    using System;
    using System.Collections.Generic;

    using Framework.Core.CommonTypes.Enums.Setup;

    /// <summary>
    /// OnePassEnvironment enum.
    /// </summary>
    public class OnePassEnvironment : IEnvironment
    {
        /// <summary>
        /// Dev
        /// </summary>
        public static OnePassEnvironment DEV = new OnePassEnvironment("DEV", true);

        /// <summary>
        /// Test
        /// </summary>
        public static OnePassEnvironment TEST = new OnePassEnvironment("TEST", true);

        /// <summary>
        /// Qa
        /// </summary>
        public static OnePassEnvironment QA = new OnePassEnvironment("QA", true);

        /// <summary>
        /// Prod
        /// </summary>
        public static OnePassEnvironment PROD = new OnePassEnvironment("PROD", false);

        // ReSharper restore InconsistentNaming

        /// <summary>
        /// Collection containing all supported OnePassEnvironments
        /// </summary>
        public static List<OnePassEnvironment> Values = new List<OnePassEnvironment>
        {
            PROD,
            QA,
            DEV,
            TEST
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
        /// OnePassEnvironment
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="isLowerEnv">The is Lower Env.</param>
        private OnePassEnvironment(string name, bool isLowerEnv)
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
        /// Loops through all of the CobaltProductViews and returns the one that matches the specified name.
        /// </summary>
        /// <param name="name">The CobaltProductView corresponding to the specified name.</param>
        /// <returns>Thrown if no CobaltProductView could be found matching the specified name.</returns>
        public static OnePassEnvironment FromName(string name)
        {
            foreach (var onePassEnvironment in Values)
            {
                if (onePassEnvironment.GetName().ToLower().Equals(name.ToLower()))
                {
                    return onePassEnvironment;
                }
            }
            throw new ArgumentException("No OnePassEnvironment enum could be found corresponding to the name: \'" + name + "\'.");
        }
    }
}
