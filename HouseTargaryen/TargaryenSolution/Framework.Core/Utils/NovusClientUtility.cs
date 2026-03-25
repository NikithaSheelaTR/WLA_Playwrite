namespace Framework.Core.Utils
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Xml;
    using Thomson.Novus.ProductAPI;
    using ItemList = Thomson.Novus.ProductAPI.ItemList;

	/// <summary>
	/// Novus client utility
	/// </summary>
	public class NovusClientUtility
    {
        private static NovusClientUtility instance;

		private const int Timeout = 120000;

		private readonly Novus novus;

		private Document novusDocument;

		/// <summary>
		/// The constructor
		/// </summary>
		/// <param name="novusEnvironment">Client or Prod environment</param>
		private NovusClientUtility(Enum novusEnvironment)
		{
			this.novus = new Novus(true);
			if (novusEnvironment.Equals(NovusEnvironmentEnum.Prod))
			{
				this.novus.SetQueueCriteria(null, "prod");
			}
            else if (novusEnvironment.Equals(NovusEnvironmentEnum.NovusAwsProd))
            {
                this.novus.SetQueueCriteria(null, "novusaws:prod");
            }
            else if (novusEnvironment.Equals(NovusEnvironmentEnum.Client))
			{
				this.novus.SetQueueCriteria(null, "client");
			}

			this.novus.ResponseTimeout = Timeout;
			this.novus.ProductName = "Cobalt";
			this.novus.RouteTag = "cobalt";
			this.novus.UserId = "Search Module Regression";
			this.novus.UseLatestPit();
        }

		/// <summary>
		/// Method to Get NovusClient utility instance
		/// </summary>
		/// <param name="novusEnvironment"></param>
		/// <returns></returns>
        public static NovusClientUtility GetInstance(Enum novusEnvironment)
        {
            if(instance == null)
                instance = new NovusClientUtility(novusEnvironment);
            return instance;
		}

		/// <summary>
		/// Retrieve Xml document from Novus
		/// </summary>
		/// <param name="guid">document guid</param>
		/// <returns><see cref="XmlDocument"/></returns>
		public XmlDocument GetDocumentXmlFromNovusByGuid(string guid)
		{
			var xmlDocument = new XmlDocument();
			this.novusDocument = this.GetFind().GetDocument(null, guid);
			xmlDocument.LoadXml(this.novusDocument.Text);
			return xmlDocument;
		}

		/// <summary>
		/// Retrieve MetaDoc map for given doc guids and domain
		/// See documentation https://theshare.thomsonreuters.com/sites/novus/Functions%20and%20Features/MetaDoc%20Implementation.doc
		/// </summary>
		/// <param name="domainDescriptor">The domain</param>
		/// <param name="documentGuids">Document guids array</param>
		/// <returns></returns>
		public MetaDocMap[] GetMetaDoc(string domainDescriptor, string[] documentGuids)
		{
			MetaDocManager metaDocManager = this.novus.GetMetaDocManager();
			metaDocManager.DomainDescriptor = domainDescriptor;
			metaDocManager.ItemList = new ItemList(documentGuids);
			return metaDocManager.RetrieveMetadata();
		}

		/// <summary>
	    /// Retrieve Xml document metadata from Novus
	    /// </summary>
	    /// <param name="guid">document guid</param>
	    /// <returns><see cref="XmlDocument"/></returns>
	    public XmlDocument GetDocumentMetadataFromNovusByGuid(string guid)
	    {
	        var xmlDocument = new XmlDocument();
	        this.novusDocument = this.GetFind().GetDocument(null, guid);
	        xmlDocument.LoadXml(this.novusDocument.MetaData);
	        return xmlDocument;
	    }

        /// <summary>
        /// Retrieves List of guids for specific collection name
        /// </summary>
        /// <param name="collectionName"> Collection Name </param>
        /// <param name="documentLimit"> Restrict the count of returned guids </param>
        /// <param name="searchQuery"> Document marker node </param>
        /// <returns> List of GUIDS </returns>
        public List<string> GetDocumentsGuidsFromNovusBySpecificCollection(
	        string collectionName,
	        int documentLimit = 100,
	        string searchQuery = "=n-document") => this.GetDocumentsGuidsFromNovus(
	        collectionName,
	        false,
	        documentLimit,
	        searchQuery);

	    /// <summary>
	    /// Retrieves List of guids for specific collection name
	    /// </summary>
	    /// <param name="collectionSetName"> Collection Set Name </param>
	    /// <param name="documentLimit"> Restrict the count of returned guids </param>
	    /// <param name="searchQuery"> Document marker node </param>
	    /// <returns> List of GUIDS </returns>
	    public List<string> GetDocumentsGuidsFromNovusBySpecificCollectionSet(
	        string collectionSetName,
	        int documentLimit = 100,
	        string searchQuery = "=n-document") => this.GetDocumentsGuidsFromNovus(
	        collectionSetName,
	        true,
	        documentLimit,
	        searchQuery);

	    /// <summary>
	    /// Retrieves List of Citations for specific collection name
	    /// </summary>
	    /// <param name="collectionName"> Collection Name </param>
	    /// <param name="documentLimit"> Restrict the count of returned guids </param>
	    /// <param name="searchQuery"> Document marker node </param>
	    /// <returns> List of GUIDS </returns>
	    public List<string> GetDocumentsCitationsFromNovusBySpecificCollection(
	        string collectionName,
	        int documentLimit = 100,
	        string searchQuery = "=n-document") => this
	        .GetDocumentsGuidsFromNovus(collectionName, false, documentLimit, searchQuery).Select(
	            x => this.GetDocumentMetadataFromNovusByGuid(x)?.SelectSingleNode("//md.first.line.cite")?.InnerText)
	        .ToList();

	    /// <summary>
	    /// Retrieves List of Citations for specific collection set name
	    /// </summary>
	    /// <param name="collectionSetName"> Collection Name </param>
	    /// <param name="documentLimit"> Restrict the count of returned guids </param>
	    /// <param name="searchQuery"> Document marker node </param>
	    /// <returns> List of GUIDS </returns>
	    public List<string> GetDocumentsCitationsFromNovusBySpecificCollectionSet(
	        string collectionSetName,
	        int documentLimit = 100,
	        string searchQuery = "=n-document") => this
	        .GetDocumentsGuidsFromNovus(collectionSetName, true, documentLimit, searchQuery).Select(
	            x => this.GetDocumentMetadataFromNovusByGuid(x)?.SelectSingleNode("//md.first.line.cite")?.InnerText)
	        .ToList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collectionSet"></param>
        /// <param name="searchQuery"></param>
        /// <param name="countOfItems"></param>
        /// <returns></returns>
	    public List<string> GetRandomDocumentGuidsFromNovusByCollectionSet(string collectionSet, string searchQuery = "=n-document", int countOfItems = 5)
	    {
	        List<string> testDataList = this.GetDocumentsGuidsFromNovusBySpecificCollectionSet(
	            collectionSet, searchQuery: searchQuery);
	        countOfItems = testDataList.Count > countOfItems ? countOfItems : testDataList.Count;
	        var random = new Random(testDataList.Count);
	        var guidsToTestList = new List<string>();
	        while (guidsToTestList.Count < countOfItems)
	        {
	            string candidateGuid = testDataList[random.Next(testDataList.Count)];
	            if (!guidsToTestList.Contains(candidateGuid))
	            {
	                guidsToTestList.Add(candidateGuid);
	            }
	        }

	        return guidsToTestList;
	    }

        /// <summary>
        /// Get relationships for n-relbase against following parameters
        /// </summary>
        /// <param name="legacyId">md.doc.legacy.id</param>
        /// <param name="familyUuid">md.doc.family.uuid</param>
        /// <param name="normDomain">Norm domain</param>
        /// <returns></returns>
        public Relationship[] GetCitingReferences(string legacyId, string familyUuid, string normDomain = "w_wl_KCCitingRef32")
        {
            RelationshipManager normManager = this.novus.GetRelationshipManager();
            normManager.Domain = normDomain;
            var baseItems = new ItemList(new [] { legacyId, familyUuid });
            return normManager.GetRelationships(baseItems.StringArray);
        }

        /// <summary>
        /// Get relationships for n-reltarget against following parameters
        /// </summary>
        /// <param name="legacyId">md.doc.legacy.id</param>
        /// <param name="familyUuid">md.doc.family.uuid</param>
        /// <param name="normDomain">Norm domain</param>
        /// <returns></returns>
        public Relationship[] GetReferencesCited(string legacyId, string familyUuid, string normDomain = "w_wl_KCCitingRef32")
        {
            RelationshipManager normManager = this.novus.GetRelationshipManager();
            normManager.Domain = normDomain;
            var baseItems = new ItemList(new[] { legacyId, familyUuid });
            return normManager.GetRelationshipsByTarget(baseItems.StringArray);
        }

        /// <summary>
        /// Norm relationships retriever
        /// </summary>
        /// <param name="guids"></param>
        /// <param name="normDomain"></param>
        /// <param name="byTarget"></param>
        /// <returns><see cref="Relationship"/>norm relationships</returns>
        public Relationship[] GetNormRelationships(string[] guids, string normDomain, bool byTarget = false)
        {
            RelationshipManager normManager = this.novus.GetRelationshipManager();
            normManager.Domain = normDomain;
            var baseItems = new ItemList(guids);
            return byTarget
                       ? normManager.GetRelationshipsByTarget(baseItems.StringArray)
                       : normManager.GetRelationships(baseItems.StringArray);
        }

		/// <summary>
		/// Get Find object
		/// </summary>
		/// <returns>Find</returns>
		private Find GetFind() => this.novus.GetFind();

		/// <summary>
		/// Get search
		/// </summary>
		/// <returns>Search</returns>
		private Search GetSearch() => this.novus.GetSearch();

	    private List<string> GetDocumentsGuidsFromNovus(
	        string colName,
	        bool searchByCollectionSet,
	        int documentLimit = 100,
	        string searchQuery = "=n-document")
	    {
	        Search search = this.GetSearch();

            foreach (string splitedCol in colName.Trim().Split(','))
	        {
	            if (searchByCollectionSet)
	            {
	                search.AddCollectionSet(splitedCol);
	            }
	            else
	            {
	                search.AddCollection(splitedCol);
	            }
            }

	        search.QueryType = SearchQueryType.BOOLEAN;
	        search.QueryText = searchQuery;
	        search.DocumentLimit = documentLimit;
			search.UseQueryWarnings = false;
	        search.Submit(true);
	        ContentSearchResult result = search.ContentSearchResult;
	        return result.GetItems().ToList();
	    }
	}
}