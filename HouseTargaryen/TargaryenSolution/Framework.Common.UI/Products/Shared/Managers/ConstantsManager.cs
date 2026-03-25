namespace Framework.Common.UI.Products.Shared.Managers
{
    using Framework.Common.Ui.Products.Shared.Models.TestConstants;
    using Framework.Common.UI.Products.GovernmentWeblinks.Enums;
    using Framework.Common.UI.Products.GovernmentWeblinks.Models;
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Products.Shared.Enums.TestConstants;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.Shared.Models.TestConstants;
    using Framework.Core.Utils.Enums;

    /// <summary>
    /// Constants Manager
    /// </summary>
    public static class ConstantsManager
    {
        private const string TestConstantsPath = @"Resources/EnumPropertyMaps/TestConstants";

        private static EnumPropertyMapper<Documents, DocumentInfoModel> documentsInfosMap;

        private static EnumPropertyMapper<Search, SearchInfoModel> searchInfosMap;

        private static EnumPropertyMapper<WebLinks, GovermentLinksModel> webLinksInfoMap;

        private static EnumPropertyMapper<Jurisdiction, JurisdictionInfo> jurisdictionsInfoMap;

        private static EnumPropertyMapper<Funds, BlcFundInfoModel> fundsInfoMap;

        /// <summary>
        /// Gets the Documents enumeration to DocumentInfoModel map.
        /// </summary>
        public static EnumPropertyMapper<Documents, DocumentInfoModel> Documents
            => documentsInfosMap = documentsInfosMap ?? EnumPropertyModelCache.GetMap<Documents, DocumentInfoModel>(string.Empty, TestConstantsPath);

        /// <summary>
        /// Gets the Search enumeration to SearchInfoModel map.
        /// </summary>
        public static EnumPropertyMapper<Search, SearchInfoModel> Search
            => searchInfosMap = searchInfosMap ?? EnumPropertyModelCache.GetMap<Search, SearchInfoModel>(string.Empty, TestConstantsPath);

        /// <summary>
        /// Gets the Weblinks enumeration to GovermentLinksInfoModel map.
        /// </summary>
        public static EnumPropertyMapper<WebLinks, GovermentLinksModel> WebLinks
            => webLinksInfoMap = webLinksInfoMap ?? EnumPropertyModelCache.GetMap<WebLinks, GovermentLinksModel>(string.Empty, TestConstantsPath);

        /// <summary>
        /// Gets the Jurisdiction enumeration to JurisdictionInfo map.
        /// </summary>
        public static EnumPropertyMapper<Jurisdiction, JurisdictionInfo> Jurisdictions
            => jurisdictionsInfoMap = jurisdictionsInfoMap ?? EnumPropertyModelCache.GetMap<Jurisdiction, JurisdictionInfo>();

        /// <summary>
        /// Gets the Fund enumeration to FundsInfo map.
        /// </summary>
        public static EnumPropertyMapper<Funds, BlcFundInfoModel> Funds =>
            fundsInfoMap = fundsInfoMap ?? EnumPropertyModelCache.GetMap<Funds, BlcFundInfoModel>(string.Empty, TestConstantsPath);
    }
}
