namespace Framework.Common.UI.Products.Shared.DomainObjects.SignOn
{
    using System;
    using System.Collections.Generic;

    using Framework.Common.UI.Products.ANZ.Utils;
    using Framework.Common.UI.Products.CaseNotebook.Utils;
    using Framework.Common.UI.Products.Concourse.Utils;
    using Framework.Common.UI.Products.GovernmentWeblinks;
    using Framework.Common.UI.Products.LawSchool.Utils;
    using Framework.Common.UI.Products.Patron.Utils;
    using Framework.Common.UI.Products.WestlawNextCorrectional.Utils;
    using Framework.Common.UI.Products.WestLawAnalytics.Utils;
    using Framework.Common.UI.Products.WestLawNext.Utils;
    using Framework.Common.UI.Products.WestLawNextCanada.Utils;
    using Framework.Common.UI.Products.WestLawNextLinks.Utils;
    using Framework.Common.UI.Products.WestLawNextMobile.Utils;
    using Framework.Common.UI.Products.WestlawNextOpenWeb.Utils;
    using Framework.Common.UI.Products.WestLawNextTax;
    using Framework.Common.UI.Raw.WestlawEdge.Utils;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;
    using Framework.Common.UI.Products.WestlawGlobal.Utils;
    using Framework.Common.UI.Products.CheckpointGlobal.Utils;
    using Framework.Common.UI.Products.TaxnetPro.Utils;
    using Framework.Common.UI.Products.WestlawEdgePremium.Utils;

    /// <summary>
    /// Factory for creating instances for each product
    /// </summary>
    public class SignOnManagerFactory
    {
        private static readonly Dictionary<CobaltProductId, Type> SignOnManagerMap =
            new Dictionary<CobaltProductId, Type>
                {
                    { CobaltProductId.WestlawNext, typeof(WestlawSignOnManager) },
                    { CobaltProductId.WlnMobile, typeof(WestlawMobileSignOnManager) },
                    { CobaltProductId.Concourse, typeof(OrionSignOnManager) },
                    { CobaltProductId.WlAnalytics, typeof(AnalyticsSignOnManager) },
                    { CobaltProductId.WestlawEdge, typeof(EdgeSignOnManager) },
                    { CobaltProductId.WlnPatron, typeof(PatronSignOnManager) },
                    { CobaltProductId.WlnTax, typeof(WestlawNextTaxSignOnManager) },
                    { CobaltProductId.WlnLinks, typeof(WestlawLinksSignOnManager) },
                    { CobaltProductId.GovtSites, typeof(GovernmentWeblinksSignOnManager) },
                    { CobaltProductId.WlnCanada, typeof(CarswellSignOnManager) },
                    { CobaltProductId.WlnCanadaAws, typeof(WestlawCanadaAwsSignOnManager) },
                    { CobaltProductId.LawSchool, typeof(LawSchoolSignOnManager) },
                    { CobaltProductId.WlnOpenWeb, typeof(OpenWebSignOnManager) },
                    { CobaltProductId.CaseNotebook, typeof(CaseNotebookSignOnManager) },
                    { CobaltProductId.Anz, typeof(AnzSignOnManager) },
                    { CobaltProductId.WlnCorrectional, typeof(WestlawNextCorrectionalManager) },
                    { CobaltProductId.WlnGlobal, typeof(WestlawGlobalSignOnManager) },
                    { CobaltProductId.CheckpointGlobal, typeof(CheckpointGlobalSignOnManager)},
                    { CobaltProductId.TaxNetPro3, typeof(TaxnetProSignOnManager)},
                    { CobaltProductId.TaxnetPro3Aws, typeof(TaxnetProAwsSignOnManager) },
                    { CobaltProductId.WestlawPrecisionAws, typeof(WestlawPrecisionAwsSignOnManager) }
                };

        /// <summary>
        /// Retrieve sign-on manager instance
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static ISignOnManager Retrieve(CobaltProductInfo product)
        {
            ISignOnManager signOnManager;

            if (product != null && SignOnManagerMap.ContainsKey(product.Id))
            {
                signOnManager = Activator.CreateInstance(SignOnManagerMap[product.Id]) as ISignOnManager;
            }
            else
            {
                throw new ArgumentException(
                    "Unsupported product was detected: " + (product == null ? "null" : product.TagName));
            }

            return signOnManager;
        }
    }
}