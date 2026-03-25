namespace Framework.Common.Api.Endpoints.Website.DataModel.CustomPages
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// The custom pages section model.
    /// </summary>
    [DataContract]
    public class CustomPagesSectionModel
    {
        private List<object> uris;

        /// <summary>
        /// Sets the CP Uris (in the format Home/Cases)
        /// </summary>
        [DataMember(Name = "categoryPageUris")]
        public List<object> CategoryPageUris
        {
            get
            {
                return this.uris ?? (this.uris = new List<object>());
            }

            set
            {
                this.uris = value;
            }
        }
    }
}

