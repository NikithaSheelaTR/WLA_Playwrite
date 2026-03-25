namespace Framework.Common.Api.Raw.Search.Utils
{
    using System;
    using System.Collections.Generic;

    using Framework.Common.Api.Raw.Search.Enums;
    using Framework.Core.DataModel;
    using Framework.Core.Utils.Enums;

    using Thomson.Novus.ProductAPI;

    /// <summary>
    /// A utility class outside of the page object model, used to search Novus directly
    /// in order to preform verifications that the information displayed in the UI
    /// is actually correct.
    /// </summary>
    public class NovusSearchUtility : BaseNovusUtility
    {
        private EnumPropertyMapper<NovusField, BaseTextModel> novusFiledMap;

        /// <summary>
        /// Gets the NovusField enumeration to BaseTextModel map.
        /// </summary>
        protected EnumPropertyMapper<NovusField, BaseTextModel> NovusFiledMap
            => this.novusFiledMap = this.novusFiledMap ?? EnumPropertyModelCache.GetMap<NovusField, BaseTextModel>();
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="collectionSet"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<string> SearchNovus(string collectionSet, string query)
        {
            Novus novus = this.GetDefaultNovusObject();
            Search search = novus.GetSearch();
            search.QueryType = SearchQueryType.BOOLEAN;
            SearchCollectionAttributes searchCollectionAttributes = new SearchCollectionAttributes();
            searchCollectionAttributes.AddRankOption("RELEVANCE");
            search.AddCollectionSet(collectionSet, searchCollectionAttributes);
            search.Ranking = true;
            search.RelevanceRankElementRanges = true;
            search.QueryText = query;
            SearchResult result = this.PerformNovusSearch(search);

            List<string> resultGuids = new List<string>();

            if (result.FoundDocumentCount > 0)
            {
                string[] tempArray = search.SearchResult.GetItems();

                foreach (string tempString in tempArray)
                {
                    resultGuids.Add(tempString);
                }
            }

            return resultGuids;
        }

        /// <summary>
        /// Searches Novus to retrieve a specified field for a document
        /// </summary>
        /// <param name="collectionSet">the collection set to search</param>
        /// <param name="docGuid">the document guid to search for</param>
        /// <param name="field">the field to retrieve a value for</param>
        /// <returns>the string value of the field as returned by Novus</returns>
        public string SearchNovusFieldForGuid(string collectionSet, string docGuid, NovusField field)
        {
            string fieldName = this.NovusFiledMap[field].Text;

            Novus novus = this.GetDefaultNovusObject();
            Search search = this.GetNovusSearch(novus, fieldName, collectionSet, docGuid);
            SearchResult searchResult = this.PerformNovusSearch(search);
            try
            {
                IndexMetaField metaField = searchResult.GetMetaField(docGuid);
                string fieldVal = metaField.GetValue(fieldName);
                return fieldVal;
            }
            catch (Exception)
            {
                return "0";
            }
        }

        /// <summary>
        /// Gets a Novus Search object with the fields set to search for a specific field for a specific guid
        /// </summary>
        /// <param name="novus">the Novus object to search</param>
        /// <param name="fieldName">the novus field to search</param>
        /// <param name="collectionSet">the collection set to search</param>
        /// <param name="docGuid">the doc guid to search for</param>
        /// <returns>a Novus Search object</returns>
        private Search GetNovusSearch(Novus novus, string fieldName, string collectionSet, string docGuid)
        {
            string[] terms = { docGuid };

            Search search = novus.GetSearch();
            search.AddIndexMetaField(fieldName);
            search.AddCollectionSet(collectionSet);
            search.QueryType = SearchQueryType.BOOLEAN;
            search.Ranking = false;
            search.QueryText = "=n-document";
            RestrictionSet restrictionSet = new RestrictionSet("RS1", terms) { Field = "uuid" };
            search.RestrictionSetExpression = "RS1";
            search.AddRestrictionSet(restrictionSet);
            return search;
        }

        /// <summary>
        /// Perform the actual search of Novus
        /// </summary>
        /// <param name="search">the Search object for querying Novus</param>
        /// <returns>a Novus SearchResult object</returns>
        private SearchResult PerformNovusSearch(Search search)
        {
            Progress progress = search.Submit(true);

            while (!progress.IsComplete)
            {
                Console.WriteLine("Percent Complete: " + progress.PercentComplete);
                Console.WriteLine("Documents Found: " + progress.DocsFound);
            }

            return search.SearchResult;
        }
    }
}