namespace Framework.Common.Api.Endpoints.Website.DataModel.CustomPages
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// The custom pages content.
    /// </summary>
    [DataContract]
    public class CustomPagesContent
    {
        private List<CustomPagesCategoryPages> categories;

        /// <summary>
        /// Sets CP category pages uri, label, isSearchable (empty if there is no content)
        /// </summary>
        [DataMember(Name = "categoryPages")]
        public List<CustomPagesCategoryPages> CustomPageCategoryPages
        {
            get
            {
                return this.categories ?? (this.categories = new List<CustomPagesCategoryPages>());
            }

            set
            {
                this.categories = value;
            }
        }
    }
}

