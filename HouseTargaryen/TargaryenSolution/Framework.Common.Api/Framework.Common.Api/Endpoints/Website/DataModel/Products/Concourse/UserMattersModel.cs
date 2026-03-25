namespace Framework.Common.Api.Endpoints.Website.DataModel.Products.Concourse
{
    using System.Collections.Generic;

    /// <summary>
    /// The user matters model.
    /// </summary>
    public class UserMattersModel
    {
        /// <summary>
        /// Gets or sets the filters.
        /// </summary>
        public IList<string> Filters { get; set; }

        /// <summary>
        /// Gets or sets the matters.
        /// </summary>
        public IList<Matter> Matters { get; set; }

        /// <summary>
        /// Gets or sets the total count.
        /// </summary>
        public int TotalCount { get; set; }
    }
}