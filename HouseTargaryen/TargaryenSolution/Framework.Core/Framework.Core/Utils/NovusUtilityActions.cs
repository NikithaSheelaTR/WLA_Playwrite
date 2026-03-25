namespace Framework.Core.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Thomson.Novus.ProductAPI;


    /// <summary>
    /// 
    /// </summary>
    public static class NovusUtilityActions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="novusClient"></param>
        /// <param name="docGuidList"></param>
        /// <returns></returns>
        public static Dictionary<string, int> GetDocumentGuidCitingReferencesCountMap(
            NovusClientUtility novusClient,
            List<string> docGuidList) => GetDocumentGuidReferencesCountMap(
            novusClient,
            docGuidList,
            novusClient.GetCitingReferences);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="novusClient"></param>
        /// <param name="docGuidList"></param>
        /// <returns></returns>
        public static Dictionary<string, int> GetDocumentGuidReferencesCitedCountMap(
            NovusClientUtility novusClient,
            List<string> docGuidList) => GetDocumentGuidReferencesCountMap(
            novusClient,
            docGuidList,
            novusClient.GetReferencesCited,
            applyCrefTypeRestriction: true);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="novusClient"></param>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetActualTrademarkItemFields(
            NovusClientUtility novusClient,
            string guid)
        {
            var resultDictionary = new Dictionary<string, string>();
            MetaDocMap[] allMetadocFields = novusClient.GetMetaDoc("w_wl_metadoc2_cb_all", new[] { guid });
            resultDictionary.Add(
                "Status",
                allMetadocFields.Where(x => x.Keys.Contains("status.trademarks"))
                                .Select(x => x.Get("status.trademarks").ToString()).FirstOrDefault() ?? string.Empty);
            resultDictionary.Add("Design Type", allMetadocFields.Where(x => x.Keys.Contains("doc.type.des")).Select(x => x.Get("doc.type.des").ToString()).FirstOrDefault() ?? string.Empty);
            resultDictionary.Add("Owner", allMetadocFields.Where(x => x.Keys.Contains("owner")).Select(x => x.Get("owner").ToString()).FirstOrDefault() ?? string.Empty);
            resultDictionary.Add("International Class", string.Join("; ", allMetadocFields.Where(x => x.Keys.Contains("class.intl")).Select(x => x.GetValues("class.intl")).FirstOrDefault()?.ToArray()));
            try
            {
                resultDictionary.Add(
                    "Filed Date",
                    DateTime.Parse(
                                allMetadocFields.Where(x => x.Keys.Contains("document.date"))
                                                .Select(x => x.Get("document.date")?.ToString()).FirstOrDefault())
                            .ToString("MMMM dd, yyyy"));
            }
            catch (Exception)
            {
                resultDictionary.Add("Filed Date", string.Empty);
            }

            try
            {
                resultDictionary.Add(
                    "Registration Date",
                    DateTime.Parse(
                                allMetadocFields.Where(x => x.Keys.Contains("date.issue"))
                                                .Select(x => x.Get("date.issue")?.ToString()).FirstOrDefault())
                            .ToString("MMMM dd, yyyy"));
            }
            catch (Exception)
            {
                resultDictionary.Add(
                    "Registration Date", string.Empty);
            }

            return resultDictionary;
        }

        private static Dictionary<string, int> GetDocumentGuidReferencesCountMap(
            NovusClientUtility novusClient,
            List<string> docGuidList, Func<string, string, string, Relationship[]> referencesResolver,
            bool applyCrefTypeRestriction = false)
        {
            var documentIdList = docGuidList.Select(guid => new
            {
                Guid = guid,
                Uuid = novusClient.GetDocumentMetadataFromNovusByGuid(guid).SelectSingleNode("//md.doc.family.uuid")?.InnerText,
                LegacyId = novusClient.GetDocumentMetadataFromNovusByGuid(guid).SelectSingleNode("//md.legacy.id")?.InnerText
            }).ToList();

            var citingReferencesMap = documentIdList.Select(guid => new
            {
                guid.Guid,
                CitingReferencesArray = referencesResolver.Invoke(string.IsNullOrEmpty(guid.LegacyId) ? guid.Guid : guid.LegacyId, guid.Uuid, "w_wl_KCCitingRef32")
            }).ToList();

            Dictionary<string, int> referencesCountMap = citingReferencesMap
                .Select(
                    rel => new
                    {
                        rel.Guid,
                        CitingRefCount =
                        applyCrefTypeRestriction
                            ? rel.CitingReferencesArray?.Where(kv => kv.Type == "CREF")?.Count() ?? 0
                            : rel.CitingReferencesArray?.Count() ?? 0
                    }).ToDictionary(
                    guidCountpair => guidCountpair.Guid,
                    guidCountpair => guidCountpair.CitingRefCount);
            return referencesCountMap;
        }
    }
}
