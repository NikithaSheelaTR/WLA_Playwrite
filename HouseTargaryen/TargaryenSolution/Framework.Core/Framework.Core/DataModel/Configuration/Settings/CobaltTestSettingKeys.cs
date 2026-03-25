namespace Framework.Core.DataModel.Configuration.Settings
{
    using Framework.Core.CommonTypes.Enums.Environment;
    using Framework.Core.CommonTypes.Enums.Setup;

    using Framework.Core.CommonTypes.Settings;

    /// <summary>
    /// A class representing a collection of SettingField objects used by Cobalt products.
    /// </summary>
    public class CobaltTestSettingKeys : TestSettingKeys
    {
        /// <summary>
        /// Test_Product
        /// </summary>
        public static TestSettingKey<IProduct> TEST_PRODUCT = TestSettingKeys.AddKey<IProduct>("TEST_PRODUCT", CobaltProduct.WESTLAWNEXT);

        /// <summary>
        /// Test_Product_View
        /// </summary>
        public static TestSettingKey<IProductView> TEST_PRODUCT_VIEW = TestSettingKeys.AddKey<IProductView>("TEST_PRODUCT_VIEW", CobaltProductView.NONE);

        /// <summary>
        /// Test_Site
        /// </summary>
        public static TestSettingKey<ISite> TEST_SITE = TestSettingKeys.AddKey<ISite>("TEST_SITE", CobaltSite.NONE);

        /// <summary>
        /// User_Password_Pool
        /// </summary>
        public static TestSettingKey<string> USER_PASSWORD_POOL = TestSettingKeys.AddKey("USER_PASSWORD_POOL", "GENERAL_PURPOSE");

        /// <summary>
        /// User_Password_Vertical
        /// </summary>
        public static TestSettingKey<string> USER_PASSWORD_VERTICAL = TestSettingKeys.AddKey("USER_PASSWORD_VERTICAL", "QED_TESTING");

        /// <summary>
        /// Test_Client_Id
        /// </summary>
        public static TestSettingKey<string> TEST_CLIENT_ID = TestSettingKeys.AddKey("TEST_CLIENT_ID", "RegressionTest");

        /// <summary>
        /// Test_Onepass_Environment
        /// </summary>
        public static TestSettingKey<OnePassEnvironment> TEST_ONEPASS_ENVIRONMENT = TestSettingKeys.AddKey("TEST_ONEPASS_ENVIRONMENT", OnePassEnvironment.QA);

        /// <summary>
        /// Test_Cobalt_Services_Environment
        /// </summary>
        public static TestSettingKey<CobaltEnvironment> TEST_COBALT_SERVICES_ENVIRONMENT = TestSettingKeys.AddKey<CobaltEnvironment>("TEST_COBALT_SERVICES_ENVIRONMENT", null);

        /// <summary>
        /// Test_Legal_Services_Environment
        /// </summary>
        public static TestSettingKey<CobaltEnvironment> TEST_LEGAL_SERVICES_ENVIRONMENT = TestSettingKeys.AddKey<CobaltEnvironment>("TEST_LEGAL_SERVICES_ENVIRONMENT", null);

        /// <summary>
        /// Test_Novus_Environment
        /// </summary>
        public static TestSettingKey<NovusEnvironment> TEST_NOVUS_ENVIRONMENT = TestSettingKeys.AddKey<NovusEnvironment>("TEST_NOVUS_ENVIRONMENT", null);

        /// <summary>
        /// User_Prism_Guid
        /// </summary>
        public static TestSettingKey<string> USER_PRISM_GUID = TestSettingKeys.AddKey<string>("USER_PRISM_GUID", null);

        /// <summary>
        /// User_Prism_UserName
        /// </summary>
        public static TestSettingKey<string> USER_PRISM_USERNAME = TestSettingKeys.AddKey<string>("USER_PRISM_USERNAME", null);

        /// <summary>
        /// User_Prism_Password
        /// </summary>
        public static TestSettingKey<string> USER_PRISM_PASSWORD = TestSettingKeys.AddKey<string>("USER_PRISM_PASSWORD", null);

        /// <summary>
        /// Needed to get around issues with how exactly the static key objects are initialized (especially since the core
        /// test settings functionality makes use of reflection) this helps ensure they have always been initialized by the 
        /// time they are referenced elsewhere in the code.
        /// </summary>
        public override void InitKeys()
        {
            base.InitKeys();
            TEST_PRODUCT = TestSettingKeys.AddKey<IProduct>("TEST_PRODUCT", CobaltProduct.WESTLAWNEXT);
            TEST_PRODUCT_VIEW = TestSettingKeys.AddKey<IProductView>("TEST_PRODUCT_VIEW", CobaltProductView.NONE);
            TEST_SITE = TestSettingKeys.AddKey<ISite>("TEST_SITE", CobaltSite.NONE);
            USER_PASSWORD_POOL = TestSettingKeys.AddKey("USER_PASSWORD_POOL", "GENERAL_PURPOSE");
            USER_PASSWORD_VERTICAL = TestSettingKeys.AddKey("USER_PASSWORD_VERTICAL", "QED_TESTING");
            TEST_CLIENT_ID = TestSettingKeys.AddKey("TEST_CLIENT_ID", "RegressionTest");
            TEST_ONEPASS_ENVIRONMENT = TestSettingKeys.AddKey("TEST_ONEPASS_ENVIRONMENT", OnePassEnvironment.QA);
            TEST_COBALT_SERVICES_ENVIRONMENT = TestSettingKeys.AddKey<CobaltEnvironment>("TEST_COBALT_SERVICES_ENVIRONMENT", null);
            TEST_LEGAL_SERVICES_ENVIRONMENT = TestSettingKeys.AddKey<CobaltEnvironment>("TEST_LEGAL_SERVICES_ENVIRONMENT", null);
            TEST_NOVUS_ENVIRONMENT = TestSettingKeys.AddKey<NovusEnvironment>("TEST_NOVUS_ENVIRONMENT", null);
            USER_PRISM_GUID = TestSettingKeys.AddKey<string>("USER_PRISM_GUID", null);
            USER_PRISM_USERNAME = TestSettingKeys.AddKey<string>("USER_PRISM_USERNAME", null);
            USER_PRISM_PASSWORD = TestSettingKeys.AddKey<string>("USER_PRISM_PASSWORD", null);
        }
    }
}
