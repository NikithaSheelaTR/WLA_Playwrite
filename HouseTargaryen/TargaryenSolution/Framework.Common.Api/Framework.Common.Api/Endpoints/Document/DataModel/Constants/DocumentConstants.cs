namespace Framework.Common.Api.Endpoints.Document.DataModel.Constants
{
    /// <summary>
    /// Constants used throughout the gunmetal tests
    /// </summary>
    public static class DocumentConstants
    {
        /// <summary>
        /// The batch document formatted reference.
        /// </summary>
        public const string BatchDocumentFormattedReference = "Document/v1/Batch/DocumentFormattedReference";

        /// <summary>
        /// The default timeout 30 sec.
        /// </summary>
        public const int DefaultTimeout30Sec = 30000;

        /// <summary>
        /// The default timeout 90 sec.
        /// </summary>
        public const int DefaultTimeout90Sec = 180000;

        /// <summary>
        /// The delivery v 1.
        /// </summary>
        public const string DeliveryInfoV1 = "Document/v1/deliveryInfo";

        /// <summary>
        /// The document formatted reference.
        /// </summary>
        public const string DocumentFormattedReference = "Document/v1/DocumentFormattedReference";

        /// <summary>
        /// The document link v 1.
        /// </summary>
        public const string DocumentLinkV1 = "Link/v1/guid?";

        /// <summary>
        /// The document persist path.
        /// </summary>
        public const string DocumentPersistPath = "/document/v1/Persist";

        /// <summary>
        /// The fo uri path.
        /// </summary>
        public const string FoUriPath = "Document/v1/GetFoUris";

        /// <summary>
        /// The full text path.
        /// </summary>
        public const string FullTextPath = "Document/v2/fulltext/";

        /// <summary>
        /// The full text required parameters.
        /// </summary>
        public const string FullTextRequiredParameters = "clientid=GUNMETALTEST&originalcontext=userenteredcitation";

        /// <summary>
        /// The meta info path v 2.
        /// </summary>
        public const string MetaInfoPathV2 = "Document/v2/metainfo/";

        /// <summary>
        /// The offline full text path.
        /// </summary>
        public const string OfflineFullTextPath = "Document/v1/Offline/FullText/";

        /// <summary>
        /// The persist xml path.
        /// </summary>
        public const string PersistXmlPath = "DocPersist/v1/persisteddoc/";
    }
}