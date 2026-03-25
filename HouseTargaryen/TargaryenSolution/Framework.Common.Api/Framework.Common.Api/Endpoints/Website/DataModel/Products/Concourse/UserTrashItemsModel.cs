namespace Framework.Common.Api.Endpoints.Website.DataModel.Products.Concourse
{
    using System.Collections.Generic;

    /// <summary>
    /// The user trash items model.
    /// </summary>
    public class UserTrashItemsModel
    {
        /// <summary>
        /// Gets or sets the total count.
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Gets or sets the trash items.
        /// </summary>
        public IList<TrashItem> TrashItems { get; set; }
    }
}