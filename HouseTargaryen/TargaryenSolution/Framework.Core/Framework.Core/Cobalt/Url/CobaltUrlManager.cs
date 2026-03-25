namespace Framework.Core.Cobalt.Url
{
    using System.Collections.Generic;

    using Framework.Core.CommonTypes.Enums.Environment;
    using Framework.Core.CommonTypes.Enums.Setup;
    using Framework.Core.CommonTypes.Settings;
    using Framework.Core.DataModel.Configuration.Settings;
    using Framework.Core.Url;

    /// <summary>
    /// CobaltUrlManager
    /// </summary>
    public class CobaltUrlManager : UrlManager
    {
        /// <summary>
        /// Default class constructor.
        /// </summary>
        public CobaltUrlManager()
        {
        }

        /// <summary>
        /// Alternate class constructor with default TestSettings.
        /// </summary>
        /// <param name="defaultSettings">the default TestSettings</param>
        public CobaltUrlManager(TestSettings defaultSettings) : base(defaultSettings)
        {
        }

        /// <summary>
        /// AddUrls
        /// </summary>
        protected override void AddUrls()
        {
            // Add the super class URLs
            base.AddUrls();

            // -- Foldering API CI -- //
            this.AddUrl
                (
                    "http://foldering.int.next.ci.westlaw.com",
                    CobaltUrlType.FOLDERING,
                    new List<TestSetting>
                    {
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.CI),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }
                );

            // -- Foldering API Demo -- //
            this.AddUrl
                (
                    "http://foldering.int.next.demo.westlaw.com",
                    CobaltUrlType.FOLDERING,
                    new List<TestSetting>
                    {
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.DEMO),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }
                );

            // -- Foldering API QED -- //
            this.AddUrl
                (
                    "http://foldering.int.next.qed.westlaw.com",
                    CobaltUrlType.FOLDERING,
                    new List<TestSetting>
                    {
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.QED),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }

                );

            // -- Foldering API HotProd -- //
            this.AddUrl
                (
                    "http://foldering.int.next.hotprod.westlaw.com",
                    CobaltUrlType.FOLDERING,
                    new List<TestSetting>
                    {
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT,
                            CobaltEnvironment.HOTPROD),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }

                );

            // -- Foldering API Prod -- //
            this.AddUrl
                (
                    "http://foldering.int.next.westlaw.com",
                    CobaltUrlType.FOLDERING,
                    new List<TestSetting>
                    {
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.PROD),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }
                );
            // -- Foldering API CI -- //
            this.AddUrl
                (
                    "https://foldering-int-next-ci-westlaw-com.30962.aws-int.thomsonreuters.com",
                    CobaltUrlType.FOLDERING,
                    new List<TestSetting>
                    {
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.CIAWS),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }
                );

            // -- Foldering API Demo -- //
            this.AddUrl
                (
                    "https://foldering-int-next-demo-westlaw-com.30962.aws-int.thomsonreuters.com",
                    CobaltUrlType.FOLDERING,
                    new List<TestSetting>
                    {
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.DEMOAWS),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }
                );

            // -- Foldering API QED -- //
            this.AddUrl
                (
                    "https://foldering-int-next-qed-westlaw-com.92615.aws-int.thomsonreuters.com",
                    CobaltUrlType.FOLDERING,
                    new List<TestSetting>
                    {
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.QEDAWS),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }

                );

            // -- Foldering API QED -- //
            this.AddUrl
                (
                    "https://foldering-int-next-qed-westlaw-com.92615.aws-int.thomsonreuters.com",
                    CobaltUrlType.FOLDERING,
                    new List<TestSetting>
                    {
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.QEDAAWS),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }

                );
            // -- Foldering API QED -- //
            this.AddUrl
                (
                    "https://foldering-int-next-qed-westlaw-com.92615.aws-int.thomsonreuters.com",
                    CobaltUrlType.FOLDERING,
                    new List<TestSetting>
                    {
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.QEDBAWS),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }

                );


            #region UDS

            // -- UDS CI -- //
            this.AddUrl
                (
                    "http://uds.int.next.ci.westlaw.com",
                    CobaltUrlType.UDS,
                    new List<TestSetting>
                    {
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.CI),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }
                );

            // -- UDS Demo -- //
            this.AddUrl
                (
                    "http://uds.int.next.demo.westlaw.com",
                    CobaltUrlType.UDS,
                    new List<TestSetting>
                    {
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.DEMO),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }
                );

            // -- UDS QED -- //
            this.AddUrl
                (
                    "http://uds.int.next.qed.westlaw.com",
                    CobaltUrlType.UDS,
                    new List<TestSetting>
                    {
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.QED),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }
                );

            // -- UDS HotProd -- //
            this.AddUrl
                (
                    "http://uds.int.next.hotprod.westlaw.com",
                    CobaltUrlType.UDS,
                    new List<TestSetting>
                    {
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT,
                            CobaltEnvironment.HOTPROD),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }
                );

            // -- UDS Prod -- //
            this.AddUrl
                (
                    "http://uds.int.next.prod.westlaw.com",
                    CobaltUrlType.UDS,
                    new List<TestSetting>
                    {
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.PROD),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }
                );

            // -- UDS CI AWS -- //
            this.AddUrl
                (
                    "https://uds-int-next-ci-westlaw-com.30962.aws-int.thomsonreuters.com",
                    CobaltUrlType.UDS,
                    new List<TestSetting>
                    {
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.CIAWS),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }
                );

            // -- UDS DemoAWS -- //
            this.AddUrl
                (
                    "https://uds-int-next-demo-westlaw-com.30962.aws-int.thomsonreuters.com",
                    CobaltUrlType.UDS,
                    new List<TestSetting>
                    {
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.DEMOAWS),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }
                );

            // -- UDS QED AWS -- //
            this.AddUrl
                (
                    "https://uds-int-next-qed-westlaw-com.92615.aws-int.thomsonreuters.com",
                    CobaltUrlType.UDS,
                    new List<TestSetting>
                    {
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.QEDAWS),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }
                );



            #endregion

            #region SessionInfo

            // -- Session Info CI -- //
            this.AddUrl
                (
                    "http://cobalttools.ci.int.westgroup.com/SessionInfo/v2/sessionguid/{0}?environment=CI",
                    CobaltUrlType.SESSION_INFO,
                    new List<TestSetting>
                    {
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.CI),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }
                );

            // -- Session Info Demo -- //
            this.AddUrl
                (
                    "http://cobalttools.demo.int.westgroup.com/SessionInfo/v2/sessionguid/{0}?environment=DEMO",
                    CobaltUrlType.SESSION_INFO,
                    new List<TestSetting>
                    {
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.DEMO),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }
                );

            // -- Session Info QED -- //
            this.AddUrl
                (
                    "http://cobalttools.qed.int.westgroup.com/SessionInfo/v2/sessionguid/{0}?environment=QED",
                    CobaltUrlType.SESSION_INFO,
                    new List<TestSetting>
                    {
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.QED),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }
                );

            // -- Session Info HotProd -- //
            this.AddUrl
                (
                    "http://cobalttools.hotprod.int.westgroup.com/SessionInfo/v2/sessionguid/{0}?environment=HOTPROD",
                    CobaltUrlType.SESSION_INFO,
                    new List<TestSetting>
                    {
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT,
                            CobaltEnvironment.HOTPROD),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }
                );

            // -- Session Info Prod -- //
            this.AddUrl
                (
                    "http://cobalttools.prod.westlan.com/SessionInfo/v2/sessionguid/{0}?environment=PROD",
                    CobaltUrlType.SESSION_INFO,
                    new List<TestSetting>
                    {
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.PROD),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }
                );

            #endregion

            # region Environment URLs

            // -- WestlawNext CI -- //
            this.AddUrl
                (
                    "http://1.next.ci.westlaw.com/",
                    CobaltUrlType.SIGN_ON_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.NONE),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.CI),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }
                );

            // -- WestlawNext CI Client-- //
            this.AddUrl
                (
                    "http://next-client.ci.westlaw.com/",
                    CobaltUrlType.SIGN_ON_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.CLIENT),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.CI),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }
                );

            // -- WestlawNext Demo -- //
            this.AddUrl
                (
                    "http://1.next.demo.westlaw.com/",
                    CobaltUrlType.SIGN_ON_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.NONE),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.DEMO),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }
                );

            // -- WestlawNext Demo B -- //
            this.AddUrl
                (
                    "http://demo-b.next.demo.westlaw.com/",
                    CobaltUrlType.SIGN_ON_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.B),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.DEMO),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }
                );

            // -- WestlawNext Demo PC1 -- //
            this.AddUrl
                (
                    "http://demo-pc1.next.demo.westlaw.com/",
                    CobaltUrlType.SIGN_ON_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.PC1),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.DEMO),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }
                );

            // -- WestlawNext Demo Client -- //
            this.AddUrl
                (
                    "http://next-client.demo.westlaw.com/",
                    CobaltUrlType.SIGN_ON_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.CLIENT),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.DEMO),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }
                );

            // -- WestlawNext QED -- //
            this.AddUrl
                (
                    "https://1.next.qed.westlaw.com/",
                    CobaltUrlType.SIGN_ON_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.NONE),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.QED),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }

                );

            // -- WestlawNext QED A -- //
            this.AddUrl
                (
                    "http://qed-a.next.qed.westlaw.com/",
                    CobaltUrlType.SIGN_ON_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.A),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.QED),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }

                );

            // -- WestlawNext QED B -- //
            this.AddUrl
                (
                    "http://qed-b.next.qed.westlaw.com/",
                    CobaltUrlType.SIGN_ON_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.B),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.QED),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }

                );

            // -- WestlawNext HotProd -- //
            this.AddUrl
                (
                    "https://1.next.hotprod.westlaw.com/",
                    CobaltUrlType.SIGN_ON_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.NONE),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT,
                            CobaltEnvironment.HOTPROD),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }

                );

            // -- WestlawNext Prod -- //
            this.AddUrl
                (
                    "https://1.next.westlaw.com/",
                    CobaltUrlType.SIGN_ON_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.NONE),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.PROD),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }
                );

            // -- WestlawNext Prod A -- //
            this.AddUrl
                (
                    "http://prod-a.next.westlaw.com/",
                    CobaltUrlType.SIGN_ON_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.A),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.PROD),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }
                );

            // -- WestlawNext Prod B -- //
            this.AddUrl
                (
                    "http://prod-b.next.westlaw.com/",
                    CobaltUrlType.SIGN_ON_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.B),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.PROD),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }
                );
            // -- WestlawNext CI AWS -- //
            this.AddUrl
                (
                    "https://region-use1.next.ci.westlaw.com/",
                    CobaltUrlType.SIGN_ON_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.NONE),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.CIAWS),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }
                );

            // -- WestlawNext Demo AWS -- //
            this.AddUrl
                (
                    "https://region-use1.next.demo.westlaw.com/",
                    CobaltUrlType.SIGN_ON_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.NONE),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.DEMOAWS),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }
                );


            // -- WestlawNext QED AWS -- //
            this.AddUrl
                (
                    "https://region-use1.next.qed.westlaw.com/",
                    CobaltUrlType.SIGN_ON_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.NONE),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.QEDAWS),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }

                );

            #endregion

            # region Routing URLs

            // -- WestlawNext CI -- //
            this.AddUrl
                (
                    "http://1.next.ci.westlaw.com/routing",
                    CobaltUrlType.ROUTING_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.NONE),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.CI),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }
                );

            // -- WestlawNext CI Client-- //
            this.AddUrl
                (
                    "http://next-client.ci.westlaw.com/routing",
                    CobaltUrlType.ROUTING_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.CLIENT),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.CI),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }
                );

            // -- WestlawNext Demo -- //
            this.AddUrl
                (
                    "http://1.next.demo.westlaw.com/routing",
                    CobaltUrlType.ROUTING_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.NONE),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.DEMO),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }
                );

            // -- WestlawNext Demo B -- //
            this.AddUrl
                (
                    "http://demo-b.next.demo.westlaw.com/routing",
                    CobaltUrlType.ROUTING_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.B),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.DEMO),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }
                );

            // -- WestlawNext Demo PC1 -- //
            this.AddUrl
                (
                    "http://demo-pc1.next.demo.westlaw.com/routing",
                    CobaltUrlType.ROUTING_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.PC1),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.DEMO),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }
                );

            // -- WestlawNext Demo Client -- //
            this.AddUrl
                (
                    "http://next-client.demo.westlaw.com/routing",
                    CobaltUrlType.ROUTING_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.CLIENT),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.DEMO),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }
                );

            // -- WestlawNext QED -- //
            this.AddUrl
                (
                    "https://1.next.qed.westlaw.com/routing",
                    CobaltUrlType.ROUTING_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.NONE),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.QED),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }

                );

            // -- WestlawNext QED A -- //
            this.AddUrl
                (
                    "http://qed-a.next.qed.westlaw.com/routing",
                    CobaltUrlType.ROUTING_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.A),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.QED),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }

                );

            // -- WestlawNext QED B -- //
           
            this.AddUrl
                (
                    "http://qed-b.next.qed.westlaw.com/routing",
                    CobaltUrlType.ROUTING_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.B),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.QED),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }

                );

            // -- WestlawNext HotProd -- //
            this.AddUrl
                (
                    "https://1.next.hotprod.westlaw.com/routing",
                    CobaltUrlType.ROUTING_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.NONE),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT,
                            CobaltEnvironment.HOTPROD),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }

                );

            // -- WestlawNext Prod -- //
            this.AddUrl
                (
                    "https://1.next.westlaw.com/routing",
                    CobaltUrlType.ROUTING_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.NONE),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.PROD),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }
                );

            // -- WestlawNext Prod A -- //
            this.AddUrl
                (
                    "http://prod-a.next.westlaw.com/routing",
                    CobaltUrlType.ROUTING_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.A),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.PROD),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }
                );

            // -- WestlawNext Prod B -- //
            this.AddUrl
                (
                    "http://prod-b.next.westlaw.com/routing",
                    CobaltUrlType.ROUTING_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.B),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.PROD),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }
                );


            // -- WestlawNext CI AWS -- //
            this.AddUrl
                (
                    "https://region-use1.next.ci.westlaw.com/routing",
                    CobaltUrlType.ROUTING_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.NONE),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.CIAWS),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }
                );


            // -- WestlawNext Demo AWS -- //
            this.AddUrl
                (
                    "https://region-use1.next.demo.westlaw.com/routing",
                    CobaltUrlType.ROUTING_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.NONE),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.DEMOAWS),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }
                );


            // -- WestlawNext QED AWS -- //
            this.AddUrl
                (
                    "https://region-use1.next.qed.westlaw.com/routing",
                    CobaltUrlType.ROUTING_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.NONE),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.QEDAWS),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.WESTLAWNEXT)
                    }

                );

            #endregion

            #region Taxnet Pro

            #region Foldering

            // -- Foldering API CI -- //
            this.AddUrl
                (
                    "http://foldering.int.v3.ci.taxnetpro.com",
                    CobaltUrlType.FOLDERING,
                    new List<TestSetting>
                    {
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.CI),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.TAXNET_PRO)
                    }
                );

            // -- Foldering API Demo -- //
            this.AddUrl
                (
                    "http://foldering.int.v3.demo.taxnetpro.com",
                    CobaltUrlType.FOLDERING,
                    new List<TestSetting>
                    {
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.DEMO),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.TAXNET_PRO)
                    }
                );

            // -- Foldering API QED -- //
            this.AddUrl
                (
                    "http://foldering.int.v3.qed.taxnetpro.com",
                    CobaltUrlType.FOLDERING,
                    new List<TestSetting>
                    {
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.QED),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.TAXNET_PRO)
                    }

                );

            // -- Foldering API HotProd -- //
            this.AddUrl
                (
                    "http://foldering.int.v3.hotprod.taxnetpro.com",
                    CobaltUrlType.FOLDERING,
                    new List<TestSetting>
                    {
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT,
                            CobaltEnvironment.HOTPROD),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.TAXNET_PRO)
                    }

                );

            // -- Foldering API Prod -- //
            this.AddUrl
                (
                    "http://foldering.int.v3.taxnetpro.com",
                    CobaltUrlType.FOLDERING,
                    new List<TestSetting>
                    {
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.PROD),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.TAXNET_PRO)
                    }
                );

            #endregion

            #region UDS

            // -- UDS CI -- //
            this.AddUrl
                (
                    "http://uds.int.v3.ci.taxnetpro.com",
                    CobaltUrlType.UDS,
                    new List<TestSetting>
                    {
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.CI),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.TAXNET_PRO)
                    }
                );

            // -- UDS Demo -- //
            this.AddUrl
                (
                    "http://uds.int.v3.demo.taxnetpro.com",
                    CobaltUrlType.UDS,
                    new List<TestSetting>
                    {
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.DEMO),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.TAXNET_PRO)
                    }
                );

            // -- UDS QED -- //
            this.AddUrl
                (
                    "http://uds.int.v3.qed.taxnetpro.com",
                    CobaltUrlType.UDS,
                    new List<TestSetting>
                    {
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.QED),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.TAXNET_PRO)
                    }
                );

            // -- UDS HotProd -- //
            this.AddUrl
                (
                    "http://uds.int.v3.hotprod.taxnetpro.com",
                    CobaltUrlType.UDS,
                    new List<TestSetting>
                    {
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT,
                            CobaltEnvironment.HOTPROD),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.TAXNET_PRO)
                    }
                );

            // -- UDS Prod -- //
            this.AddUrl
                (
                    "http://uds.int.v3.prod.taxnetpro.com",
                    CobaltUrlType.UDS,
                    new List<TestSetting>
                    {
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.PROD),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.TAXNET_PRO)
                    }
                );

            #endregion

            #region SessionInfo

            // -- Session Info CI -- //
            this.AddUrl
                (
                    "http://cobalttools-shared.ci.int.westgroup.com/SessionInfo/v2/sessionguid/{0}?environment=CI",
                    CobaltUrlType.SESSION_INFO,
                    new List<TestSetting>
                    {
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.CI),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.TAXNET_PRO)
                    }
                );

            // -- Session Info Demo -- //
            this.AddUrl
                (
                    "http://cobalttools-shared.demo.int.westgroup.com/SessionInfo/v2/sessionguid/{0}?environment=DEMO",
                    CobaltUrlType.SESSION_INFO,
                    new List<TestSetting>
                    {
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.DEMO),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.TAXNET_PRO)
                    }
                );

            // -- Session Info QED -- //
            this.AddUrl
                (
                    "http://cobalttools-shared.qed.int.westgroup.com/SessionInfo/v2/sessionguid/{0}?environment=QED",
                    CobaltUrlType.SESSION_INFO,
                    new List<TestSetting>
                    {
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.QED),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.TAXNET_PRO)
                    }
                );

            // -- Session Info HotProd -- //
            this.AddUrl
                (
                    "http://cobalttools-shared.hotprod.int.westgroup.com/SessionInfo/v2/sessionguid/{0}?environment=HOTPROD",
                    CobaltUrlType.SESSION_INFO,
                    new List<TestSetting>
                    {
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT,
                            CobaltEnvironment.HOTPROD),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.TAXNET_PRO)
                    }
                );

            // -- Session Info Prod -- //
            this.AddUrl
                (
                    "http://cobalttools-shared.prod.westlan.com/SessionInfo/v2/sessionguid/{0}?environment=PROD",
                    CobaltUrlType.SESSION_INFO,
                    new List<TestSetting>
                    {
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.PROD),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.TAXNET_PRO)
                    }
                );

            #endregion

            # region Environment URLs

            // -- Taxnet Pro CI -- //
            this.AddUrl
                (
                    "https://v3.ci.taxnetpro.com",
                    CobaltUrlType.SIGN_ON_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.NONE),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.CI),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.TAXNET_PRO)
                    }
                );

            // -- Taxnet Pro Demo -- //
            this.AddUrl
                (
                    "https://v3.demo.taxnetpro.com",
                    CobaltUrlType.SIGN_ON_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.NONE),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.DEMO),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.TAXNET_PRO)
                    }
                );

            // -- Taxnet Pro Demo B -- //
            this.AddUrl
                (
                    "http://demoshared-b.v3.demo.taxnetpro.com/",
                    CobaltUrlType.SIGN_ON_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.B),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.DEMO),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.TAXNET_PRO)
                    }
                );

            // -- Taxnet Pro Demo PC1 -- //
            this.AddUrl
                (
                    "http://demoshared-pc1.v3.demo.taxnetpro.com/",
                    CobaltUrlType.SIGN_ON_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.PC1),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.DEMO),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.TAXNET_PRO)
                    }
                );


            // -- Taxnet Pro QED -- //
            this.AddUrl
                (
                    "https://v3.qed.taxnetpro.com",
                    CobaltUrlType.SIGN_ON_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.NONE),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.QED),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.TAXNET_PRO)
                    }
                );

            // -- Taxnet Pro QED A -- //
            this.AddUrl
                (
                    "http://qedshared-a.v3.qed.taxnetpro.com/",
                    CobaltUrlType.SIGN_ON_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.A),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.QED),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.TAXNET_PRO)
                    }
                );

            // -- Taxnet Pro QED B -- //
            this.AddUrl
                (
                    "http://qedshared-b.v3.qed.taxnetpro.com/",
                    CobaltUrlType.SIGN_ON_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.B),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.QED),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.TAXNET_PRO)
                    }
                );

            // -- Taxnet Pro Hotprod -- //
            this.AddUrl
                (
                    "https://v3.hotprod.taxnetpro.com",
                    CobaltUrlType.SIGN_ON_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.NONE),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.HOTPROD),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.TAXNET_PRO)
                    }
                );

            // -- Taxnet Pro Prod -- //
            this.AddUrl
                (
                    "https://v3.taxnetpro.com",
                    CobaltUrlType.SIGN_ON_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.NONE),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.PROD),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.TAXNET_PRO)
                    }
                );

            // -- Taxnet Pro Prod A -- //
            this.AddUrl
                (
                    "http://prodshared-a.v3.taxnetpro.com/",
                    CobaltUrlType.SIGN_ON_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.A),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.PROD),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.TAXNET_PRO)
                    }
                );

            // -- Taxnet Pro Prod B -- //
            this.AddUrl
                (
                    "http://prodshared-b.v3.taxnetpro.com/",
                    CobaltUrlType.SIGN_ON_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.B),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.PROD),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.TAXNET_PRO)
                    }
                );

            #endregion

            # region DC Exit Environment URLs

            // -- Taxnet Pro Demo -- //
            this.AddUrl
                (
                    "https://region-use1.v3.demo.taxnetpro.com/",
                    CobaltUrlType.DCEXIT_TAXNETPRO,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.NONE),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.DEMO),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.TAXNET_PRO)
                    }
                );

            // -- Taxnet Pro QED -- //
            this.AddUrl
                (
                    "https://region-use1.v3.qed.taxnetpro.com/",
                    CobaltUrlType.DCEXIT_TAXNETPRO,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.NONE),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.QED),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.TAXNET_PRO)
                    }
                );

            #endregion

            # region Routing URLs

            // -- Taxnet Pro CI -- //
            this.AddUrl
                (
                    "https://v3.ci.taxnetpro.com/routing",
                    CobaltUrlType.ROUTING_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.NONE),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.CI),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.TAXNET_PRO)
                    }
                );

            // -- Taxnet Pro Demo -- //
            this.AddUrl
                (
                    "https://v3.demo.taxnetpro.com/routing",
                    CobaltUrlType.ROUTING_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.NONE),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.DEMO),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.TAXNET_PRO)
                    }
                );

            // -- Taxnet Pro Demo B -- //
            this.AddUrl
                (
                    "https://demoshared-b.v3.demo.taxnetpro.com/routing",
                    CobaltUrlType.ROUTING_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.B),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.DEMO),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.TAXNET_PRO)
                    }
                );

            // -- Taxnet Pro Demo PC1 -- //
            this.AddUrl
                (
                    "https://demoshared-pc1.v3.demo.taxnetpro.com/routing",
                    CobaltUrlType.ROUTING_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.PC1),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.DEMO),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.TAXNET_PRO)
                    }
                );


            // -- Taxnet Pro QED -- //
            this.AddUrl
                (
                    "https://v3.qed.taxnetpro.com/routing",
                    CobaltUrlType.ROUTING_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.NONE),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.QED),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.TAXNET_PRO)
                    }
                );

            // -- Taxnet Pro QED A -- //
            this.AddUrl
                (
                    "https://qedshared-a.v3.qed.taxnetpro.com/routing",
                    CobaltUrlType.ROUTING_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.A),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.QED),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.TAXNET_PRO)
                    }
                );

            // -- Taxnet Pro QED B -- //
            this.AddUrl
                (
                    "https://qedshared-b.v3.qed.taxnetpro.com/routing",
                    CobaltUrlType.ROUTING_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.B),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.QED),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.TAXNET_PRO)
                    }
                );

            // -- Taxnet Pro Hotprod -- //
            this.AddUrl
                (
                    "https://v3.hotprod.taxnetpro.com/routing",
                    CobaltUrlType.ROUTING_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.NONE),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.HOTPROD),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.TAXNET_PRO)
                    }
                );

            // -- Taxnet Pro Prod -- //
            this.AddUrl
                (
                    "https://v3.taxnetpro.com/routing",
                    CobaltUrlType.ROUTING_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.NONE),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.PROD),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.TAXNET_PRO)
                    }
                );

            // -- Taxnet Pro Prod A -- //
            this.AddUrl
                (
                    "https://prodshared-a.v3.prod.taxnetpro.com/routing",
                    CobaltUrlType.ROUTING_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.A),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.PROD),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.TAXNET_PRO)
                    }
                );

            // -- Taxnet Pro Prod B -- //
            this.AddUrl
                (
                    "http://prodshared-b.v3.prod.taxnetpro.com/routing",
                    CobaltUrlType.ROUTING_PAGE,
                    new List<TestSetting>
                    {
                        new TestSetting<ISite>(CobaltTestSettingKeys.TEST_SITE, CobaltSite.B),
                        new TestSetting<IEnvironment>(CobaltTestSettingKeys.TEST_ENVIRONMENT, CobaltEnvironment.PROD),
                        new TestSetting<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT, CobaltProduct.TAXNET_PRO)
                    }
                );

            #endregion

            #endregion

            #region CobaltServices

            // -- Cobalt Services CI -- //
            this.AddUrl
                (
                    "http://cobaltservices.ci.int.thomsonreuters.com",
                    CobaltUrlType.COBALT_SERVICES,
                    new TestSetting<CobaltEnvironment>(CobaltTestSettingKeys.TEST_COBALT_SERVICES_ENVIRONMENT,
                        CobaltEnvironment.CI)
                );

            // -- Cobalt Services Demo -- //
            this.AddUrl
                (
                    "http://cobaltservices.demo.int.thomsonreuters.com",
                    CobaltUrlType.COBALT_SERVICES,
                    new TestSetting<CobaltEnvironment>(CobaltTestSettingKeys.TEST_COBALT_SERVICES_ENVIRONMENT,
                        CobaltEnvironment.DEMO)
                );

            // -- Cobalt Services QED -- //
            this.AddUrl
                (
                    "http://cobaltservices.qed.int.thomsonreuters.com",
                    CobaltUrlType.COBALT_SERVICES,
                    new TestSetting<CobaltEnvironment>(CobaltTestSettingKeys.TEST_COBALT_SERVICES_ENVIRONMENT,
                        CobaltEnvironment.QED)
                );

            // -- Cobalt Services HotProd -- //
            this.AddUrl
                (
                    "http://cobaltservices.int.thomsonreuters.com",
                    CobaltUrlType.COBALT_SERVICES,
                    new TestSetting<CobaltEnvironment>(CobaltTestSettingKeys.TEST_COBALT_SERVICES_ENVIRONMENT,
                        CobaltEnvironment.HOTPROD)
                );

            // -- Cobalt Services Prod -- //
            this.AddUrl
                (
                    "http://cobaltservices.int.thomsonreuters.com",
                    CobaltUrlType.COBALT_SERVICES,
                    new TestSetting<CobaltEnvironment>(CobaltTestSettingKeys.TEST_COBALT_SERVICES_ENVIRONMENT,
                        CobaltEnvironment.PROD)
                );

            // -- Cobalt Services CI AWS -- //
            this.AddUrl
                (
                    "https://cobaltservices-use1-demo.dataroom-preprod.aws-int.thomsonreuters.com",
                    CobaltUrlType.COBALT_SERVICES,
                    new TestSetting<CobaltEnvironment>(CobaltTestSettingKeys.TEST_COBALT_SERVICES_ENVIRONMENT,
                        CobaltEnvironment.CIAWS)
                );

            // -- Cobalt Services Demo AWS -- //
            this.AddUrl
                (
                    "https://cobaltservices-use1-demo.dataroom-preprod.aws-int.thomsonreuters.com",
                    CobaltUrlType.COBALT_SERVICES,
                    new TestSetting<CobaltEnvironment>(CobaltTestSettingKeys.TEST_COBALT_SERVICES_ENVIRONMENT,
                        CobaltEnvironment.DEMOAWS)
                );

            // -- Cobalt Services QED AWS -- //
            this.AddUrl
                (
                    "https://cobaltservices-use1-qed.dataroom-prod.aws-int.thomsonreuters.com",
                    CobaltUrlType.COBALT_SERVICES,
                    new TestSetting<CobaltEnvironment>(CobaltTestSettingKeys.TEST_COBALT_SERVICES_ENVIRONMENT,
                        CobaltEnvironment.QEDAWS)
                );

           

            #endregion

            #region LegalServices

            // -- Legal Services CI -- //
            this.AddUrl
                (
                    "http://legalservices.ci.westlaw.com",
                    CobaltUrlType.LEGAL_SERVICES,
                    new TestSetting<CobaltEnvironment>(CobaltTestSettingKeys.TEST_LEGAL_SERVICES_ENVIRONMENT,
                        CobaltEnvironment.CI)
                );

            // -- Legal Services Demo -- //
            this.AddUrl
                (
                    "http://legalservices.demo.westlaw.com",
                    CobaltUrlType.LEGAL_SERVICES,
                    new TestSetting<CobaltEnvironment>(CobaltTestSettingKeys.TEST_LEGAL_SERVICES_ENVIRONMENT,
                        CobaltEnvironment.DEMO)
                );

            // -- Legal Services QED -- //
            this.AddUrl
                (
                    "http://legalservices.qed.westlaw.com",
                    CobaltUrlType.LEGAL_SERVICES,
                    new TestSetting<CobaltEnvironment>(CobaltTestSettingKeys.TEST_LEGAL_SERVICES_ENVIRONMENT,
                        CobaltEnvironment.QED)
                );

            // -- Legal Services HotProd -- //
            this.AddUrl
                (
                    "http://legalservices.westlaw.com",
                    CobaltUrlType.LEGAL_SERVICES,
                    new TestSetting<CobaltEnvironment>(CobaltTestSettingKeys.TEST_LEGAL_SERVICES_ENVIRONMENT,
                        CobaltEnvironment.HOTPROD)
                );

            // -- Legal Services Prod -- //
            this.AddUrl
                (
                    "http://legalservices.westlaw.com",
                    CobaltUrlType.LEGAL_SERVICES,
                    new TestSetting<CobaltEnvironment>(CobaltTestSettingKeys.TEST_LEGAL_SERVICES_ENVIRONMENT,
                        CobaltEnvironment.PROD)
                );

            #endregion

        }
    }
}