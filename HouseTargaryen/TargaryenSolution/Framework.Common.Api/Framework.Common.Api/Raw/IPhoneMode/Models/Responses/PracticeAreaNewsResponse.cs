namespace Framework.Common.Api.Raw.IPhoneMode.Models.Responses
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// PracticeAreaNews class
    /// </summary>
    public class PracticeAreaNewsResponse
    {
        /// <summary>
        /// Gets or sets the entries.
        /// </summary>
        public List<Entry> Entries { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The entry.
        /// </summary>
        public class Entry
        {
            /// <summary>
            /// Gets or sets the date.
            /// </summary>
            public string Date { get; set; }

            /// <summary>
            /// Gets or sets the id.
            /// </summary>
            public string Id { get; set; }

            /// <summary>
            /// Gets or sets the image URI.
            /// </summary>
            public string ImageUri { get; set; }

            /// <summary>
            /// Gets or sets the last modified date.
            /// </summary>
            public long LastModifiedDate { get; set; }

            /// <summary>
            /// Gets or sets the link data.
            /// </summary>
            public string LinkData { get; set; }

            /// <summary>
            /// Gets or sets the link data type.
            /// </summary>
            public string LinkDataType { get; set; }

            /// <summary>
            /// Gets or sets the metadata.
            /// </summary>
            public Metadata Metadata { get; set; }

            /// <summary>
            /// Gets or sets the publication.
            /// </summary>
            public string Publication { get; set; }

            /// <summary>
            /// Gets or sets the royalty ids.
            /// </summary>
            public string RoyaltyIds { get; set; }

            /// <summary>
            /// Gets or sets the source.
            /// </summary>
            public string Source { get; set; }

            /// <summary>
            /// Gets or sets the state.
            /// </summary>
            public string State { get; set; }

            /// <summary>
            /// Gets or sets the summary.
            /// </summary>
            public string Summary { get; set; }

            /// <summary>
            /// Gets or sets the summary char count.
            /// </summary>
            public int SummaryCharCount { get; set; }

            /// <summary>
            /// Gets or sets the thumbnail URI.
            /// </summary>
            public string ThumbnailUri { get; set; }

            /// <summary>
            /// Gets or sets the title.
            /// </summary>
            public string Title { get; set; }

            /// <summary>
            /// Gets or sets the type.
            /// </summary>
            public string Type { get; set; }

            /// <summary>
            /// Gets or sets the version id.
            /// </summary>
            public string VersionId { get; set; }
        }

        /// <summary>
        /// The metadata.
        /// </summary>
        public class Metadata
        {
            /// <summary>
            /// Gets or sets the area id.
            /// </summary>
            public string AreaId { get; set; }

            /// <summary>
            /// Gets or sets the collection set.
            /// </summary>
            [JsonProperty("collection-set")]
            public string CollectionSet { get; set; }

            /// <summary>
            /// Gets or sets the group.
            /// </summary>
            public string Group { get; set; }

            /// <summary>
            /// Gets or sets the image alt.
            /// </summary>
            public string ImageAlt { get; set; }

            /// <summary>
            /// Gets or sets the important multiplier.
            /// </summary>
            [JsonProperty("important-multiplier")]
            public string ImportantMultiplier { get; set; }

            /// <summary>
            /// Gets or sets the rank.
            /// </summary>
            public int Rank { get; set; }
        }
    }
}