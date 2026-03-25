namespace Framework.Common.UI.DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Resources Object
    /// </summary>
    [DataContract]
    public class ResourcesObject
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        [DataMember(Name = "title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [DataMember(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether has count.
        /// </summary>
        [DataMember(Name = "hasCount")]
        public bool HasCount { get; set; }

        /// <summary>
        /// Gets or sets the is sortable.
        /// </summary>
        [DataMember(Name = "isSortable")]
        public bool IsSortable { get; set; }

        /// <summary>
        /// Gets or sets the sort type.
        /// </summary>
        [DataMember(Name = "sortType")]
        public string SortType { get; set; }

        /// <summary>
        /// Gets or sets the sort by option descending.
        /// </summary>
        [DataMember(Name = "sortByOptionDesc")]
        public string SortByOptionDesc { get; set; }

        /// <summary>
        /// Gets or sets the sort by option ascending.
        /// </summary>
        [DataMember(Name = "sortByOptionAsc")]
        public string SortByOptionAsc { get; set; }
    }
}
