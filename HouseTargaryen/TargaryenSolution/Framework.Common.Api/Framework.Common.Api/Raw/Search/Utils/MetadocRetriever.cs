namespace Framework.Common.Api.Raw.Search.Utils
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.Api.Raw.Search.Enums;
    using Framework.Core.DataModel;
    using Framework.Core.Utils.Enums;

    using Thomson.Novus.ProductAPI;

    /// <summary>
    /// A utility class outside of the page object model, used to query Metadoc directly
    /// in order to preform verifications that the information displayed in the UI
    /// is actually correct.
    /// </summary>
    public class MetadocRetriever : BaseNovusUtility
    {
        private const string DefaultDomainDescriptor = "w_wl_metadoc_cb_combo";

        private EnumPropertyMapper<MetadocField, BaseTextModel> metadocFieldMap;

        /// <summary>
        /// Gets the MetadocField enumeration to BaseTextModel map.
        /// </summary>
        protected EnumPropertyMapper<MetadocField, BaseTextModel> MetadocFieldMap
            =>
                this.metadocFieldMap =
                    this.metadocFieldMap ?? EnumPropertyModelCache.GetMap<MetadocField, BaseTextModel>();
        
        /// <summary>
        /// Retrieves a list of values for the specified Metadoc field for the specified guids
        /// </summary>
        /// <param name="field">the field in Metadoc to query</param>
        /// <param name="maps">metadoc maps</param>
        /// <param name="guids">the guids to query metadoc for</param>
        /// <returns>a dictionary mapping a guid to the information metadoc returnd for it</returns>
        public Dictionary<string, List<T>> RetrieveMetadocFieldValuesFromMetadocMap<T>(
            MetadocField field,
            IEnumerable<MetaDocMap> maps,
            params string[] guids)
        {
            Dictionary<string, List<T>> metadocValues = new Dictionary<string, List<T>>();
            string fieldName = this.MetadocFieldMap[field].Text;

            foreach (MetaDocMap map in maps)
            {
                string metadocGuid = map.MetaDocGuid;

                if (map.Keys.Contains(fieldName))
                {
                    ArrayList mapValues = map.GetValues(fieldName);
                    List<T> values = mapValues.Cast<T>().ToList();
                    metadocValues.Add(metadocGuid, values);
                }
                else
                {
                    metadocValues.Add(metadocGuid, new List<T>());
                }
            }

            return metadocValues;
        }

        /// <summary>
        /// Retrieves a list of values for the specified Metadoc field for the specified guids
        /// </summary>
        /// <param name="guids">the guids to query metadoc for</param>
        /// <returns>a dictionary mapping a guid to the information metadoc returnd for it</returns>
        public IEnumerable<MetaDocMap> RetrieveMetaDocMapsForGuids(params string[] guids)
        {
            Novus novus = this.GetDefaultNovusObject();

            IEnumerable<MetaDocMap> maps = this.RetrieveMetadataMaps(guids, novus);

            return maps;
        }

        /// <summary>
        /// Retrieves a list of values for the specified Metadoc field for the specified guids
        /// </summary>
        /// <param name="field">the field in Metadoc to query</param>
        /// <param name="guids">the guids to query metadoc for</param>
        /// <returns>a dictionary mapping a guid to the information metadoc returnd for it</returns>
        public Dictionary<string, List<T>> RetrieveValuesFromMetadocForGuids<T>(
            MetadocField field,
            params string[] guids)
        {
            Dictionary<string, List<T>> metadocValues = new Dictionary<string, List<T>>();
            string fieldName = this.MetadocFieldMap[field].Text;

            Novus novus = this.GetDefaultNovusObject();

            IEnumerable<MetaDocMap> maps = this.RetrieveMetadataMaps(guids, novus);

            foreach (MetaDocMap map in maps)
            {
                string metadocGuid = map.MetaDocGuid;

                List<T> values;
                if (map.Keys.Contains(fieldName))
                {
                    ArrayList mapValues = map.GetValues(fieldName);
                    values = mapValues.Cast<T>().ToList();
                }
                else
                {
                    values = new List<T>();
                }

                metadocValues.Add(metadocGuid, values);
            }

            return metadocValues;
        }

        /// <summary>
        /// Retrieve a Metadoc map of fields to their values for the specified guids, using an input Novus object
        /// </summary>
        /// <param name="guids">the guids to retrieve metadoc info for</param>
        /// <param name="novus">the Novus object to query</param>
        /// <returns>an array of MetadocMaps, where each entry in the array corresponds to a single guid</returns>
        private IEnumerable<MetaDocMap> RetrieveMetadataMaps(string[] guids, Novus novus)
        {
            MetaDocManager manager = novus.GetMetaDocManager();
            manager.DomainDescriptor = DefaultDomainDescriptor;
            ItemList guidsList = new ItemList(guids);
            manager.ItemList = guidsList;
            MetaDocMap[] maps = manager.RetrieveMetadata();
            return maps;
        }
    }
}