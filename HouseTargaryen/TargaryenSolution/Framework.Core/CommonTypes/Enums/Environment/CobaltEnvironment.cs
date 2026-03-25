namespace Framework.Core.CommonTypes.Enums.Environment
{
    using Framework.Core.CommonTypes.Enums.Setup;

    /// <summary>
    /// Environments for Cobalt Products
    /// </summary>
    public class CobaltEnvironment : IEnvironment
    {
        /// <summary>
        /// Ci
        /// </summary>
        public static CobaltEnvironment CI = new CobaltEnvironment("CI", true);

        /// <summary>
        /// Demo
        /// </summary>
        public static CobaltEnvironment DEMO = new CobaltEnvironment("DEMO", true);

        /// <summary>
        /// Qed
        /// </summary>
        public static CobaltEnvironment QED = new CobaltEnvironment("QED", true);

        /// <summary>
        /// HotProd
        /// </summary>
        public static CobaltEnvironment HOTPROD = new CobaltEnvironment("HOTPROD", true);

        /// <summary>
        /// Prod
        /// </summary>
        public static CobaltEnvironment PROD = new CobaltEnvironment("PROD", false);


        /// <summary>
        /// CIAWS
        /// </summary>
        public static CobaltEnvironment CIAWS = new CobaltEnvironment("CIAWS", true);

        /// <summary>
        /// DEMOAWS
        /// </summary>
        public static CobaltEnvironment DEMOAWS = new CobaltEnvironment("DEMOAWS", true);
      
        /// <summary>
        /// QEDAWS
        /// </summary>
        public static CobaltEnvironment QEDAWS = new CobaltEnvironment("QEDAWS", true);

        /// <summary>
        /// QEDAWS
        /// </summary>
        public static CobaltEnvironment QEDAAWS = new CobaltEnvironment("QEDAAWS", true);

        /// <summary>
        /// QEDAWS
        /// </summary>
        public static CobaltEnvironment QEDBAWS = new CobaltEnvironment("QEDBAWS", true);

        /// <summary>
        /// HOTPRODAWS
        /// </summary>
        public static CobaltEnvironment HOTPRODAWS = new CobaltEnvironment("HOTPRODAWS", true);

        /// <summary>
        /// PRODAWS
        /// </summary>
        public static CobaltEnvironment PRODAWS = new CobaltEnvironment("PRODAWS", false);

        /// <summary>
        /// Name
        /// </summary>
        protected string Name;

        /// <summary>
        /// IsLowerEnv
        /// </summary>
        protected bool IsLowerEnv;

        /// <summary>
        /// CobaltEnvironment
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="isLowerEnv">The is Lower Env.</param>
        private CobaltEnvironment(string name, bool isLowerEnv)
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
    }
}