namespace Framework.Common.UI.Products.Shared.Models.GridModels
{
    using System;
    using System.Collections.Generic;

    using Framework.Core.CommonTypes.Enums;

    /// <summary>
    /// contains the string of the title and the summary underneath it
    /// </summary>
    public class GridObjectModel
    {
        /// <summary>
        /// Gets or sets the client id (added by for folders page).
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets the list of citations, if displayed
        /// </summary>
        public List<string> Citations { get; set; }

        /// <summary>
        /// date/time or 'date added' for folders page of the grid object model
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// details of the grid object model
        /// </summary>
        public string Details { get; set; }

        /// <summary>
        /// The type of the key cite flag
        /// </summary>
        public KeyCiteFlag Flag { get; set; }

        /// <summary>
        /// title of the grid object model
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// summary of the grid object model
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// title (description for search) of the grid object model
        /// </summary>
        public string Title { get; set; }
    }
}