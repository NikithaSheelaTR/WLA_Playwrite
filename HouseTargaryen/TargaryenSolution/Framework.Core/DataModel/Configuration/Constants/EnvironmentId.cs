namespace Framework.Core.DataModel.Configuration.Constants
{
    /// <summary>
    /// Environment type IDs.
    /// </summary>
    public enum EnvironmentId : ushort
    {
        /// <summary>
        /// Identifies a non-existing environment.
        /// </summary>
        None,

        /// <summary>
        /// Cobalt CI/Shared CI environment.
        /// </summary>
        Ci,

        /// <summary>
        /// Cobalt CI Client/Shared CI Client environment.
        /// </summary>
        CiClient,

        /// <summary>
        /// Cobalt DEMO/Shared DEMO environment.
        /// </summary>
        Demo,

        /// <summary>
        /// Cobalt DEMO Client environment.
        /// </summary>
        DemoClient,

        /// <summary>
        /// Cobalt DEMO-B/Shared DEMO-B environment.
        /// </summary>
        DemoB,

        /// <summary>
        /// Cobalt DEMO-PC1/Shared DEMO-PC1 environment.
        /// </summary>
        DemoPc1,

        /// <summary>
        /// Cobalt QED/Shared QED environment.
        /// </summary>
        Qed,

        /// <summary>
        /// Cobalt QED-A/Shared QED-A environment.
        /// </summary>
        QedA,

        /// <summary>
        /// Cobalt QED-B/Shared QED-B environment.
        /// </summary>
        QedB,

        /// <summary>
        /// Cobalt HotProd/Shared HotProd environment with LS and CS pointing to QED.
        /// </summary>
        HotProdQa,

        /// <summary>
        /// Cobalt HotProd/Shared HotProd environment.
        /// </summary>
        HotProd,

        /// <summary>
        /// Cobalt HotProd/Shared HotProd environment.
        /// </summary>
        HotProdB,

        /// <summary>
        /// Cobalt Prod/Shared Prod environment.
        /// </summary>
        Prod,

        /// <summary>
        /// Cobalt Prod-A/Shared Prod-A environment.
        /// </summary>
        ProdA,

        /// <summary>
        /// Cobalt Prod-B/Shared Prod-B environment.
        /// </summary>
        ProdB,

        /// <summary>
        /// OnePassWeb QA environment.
        /// </summary>
        OnePassQa,

        /// <summary>
        /// OnePassWeb PROD environment.
        /// </summary>
        OnePassProd,

        /// <summary>
        /// Cobalt CI AWS/Shared CI environment.
        /// </summary>
        CiAWS,

        /// <summary>
        /// Cobalt DEMO/ Cloud environment.
        /// </summary>
        DemoAWS,

        /// <summary>
        /// Cobalt DEMO-B/ Cloud environment.
        /// </summary>
        DemoBAWS,

        /// <summary>
        /// Cobalt DEMO-PC1/ Cloud environment.
        /// </summary>
        DemoPc1AWS,

        /// <summary>
        /// Cobalt QED/ Cloud environment.
        /// </summary>
        QedAWS,

        /// <summary>
        /// Cobalt QED-A/ Cloud environment.
        /// </summary>
        QedAAWS,

        /// <summary>
        /// Cobalt QED-B/ Cloud environment.
        /// </summary>
        QedBAWS,

        /// <summary>
        /// Cobalt HotProd/Shared HotProd  cloud environment.
        /// </summary>
        HotProdAWS,

        /// <summary>
        /// Cobalt HotProd/Shared HotProd environment.
        /// </summary>
        HotProdBAWS,

        /// <summary>
        /// Cobalt Prod/ Cloud environment.
        /// </summary>
        ProdAWS,
        /// <summary>
        /// Cobalt Prod/A Cloud environment.
        /// </summary>
        ProdAAWS,

        /// <summary>
        /// Cobalt Prod/ B Cloud environment.
        /// </summary>
        ProdBAWS,

        /// <summary>
        /// Cobalt Prod/ Cloud environment.
        /// </summary>
        ProdAWS2,
        /// <summary>
        /// Cobalt Prod/A Cloud environment.
        /// </summary>
        ProdAAWS2,

        /// <summary>
        /// Cobalt Prod/ B Cloud environment.
        /// </summary>
        ProdBAWS2,

        /// <summary>
        /// Cobalt QED/ Cloud environment.
        /// </summary>
        QedAWS2,

        /// <summary>
        /// Cobalt QED-A/ Cloud environment.
        /// </summary>
        QedAAWS2,

        /// <summary>
        /// Cobalt QED-B/ Cloud environment.
        /// </summary>
        QedBAWS2
    }
}