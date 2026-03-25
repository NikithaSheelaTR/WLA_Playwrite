namespace Framework.Common.Api.Endpoints.Website.DataModel.Products.Concourse
{
    /// <summary>
    /// The permissions.
    /// </summary>
    public class Permissions
    {
        /// <summary>
        /// Gets or sets a value indicating whether add contents.
        /// </summary>
        public bool AddContents { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether delete.
        /// </summary>
        public bool Delete { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether read.
        /// </summary>
        public bool Read { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether update.
        /// </summary>
        public bool Update { get; set; }
    }
}