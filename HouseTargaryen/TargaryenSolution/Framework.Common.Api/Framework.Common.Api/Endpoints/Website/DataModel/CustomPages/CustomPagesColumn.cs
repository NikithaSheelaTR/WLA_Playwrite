namespace Framework.Common.Api.Endpoints.Website.DataModel.CustomPages
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// The custom pages column.
    /// </summary>
    [DataContract]
    public class CustomPagesColumn
    {
        private List<CustomPagesSection> sections;

        /// <summary>
        /// Sets CP sections (type, label, model) (empty if there is no content)
        /// </summary>
        [DataMember(Name = "sections")]
        public List<CustomPagesSection> Sections
        {
            get
            {
                return this.sections ?? (this.sections = new List<CustomPagesSection>());
            }

            set
            {
                this.sections = value;
            }
        }
    }
}
