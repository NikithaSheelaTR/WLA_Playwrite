namespace Framework.Common.Api.Endpoints.Document.DataModel.DeliveryFoUriPathInfo
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The page dimensions.
    /// </summary>
    [DataContract]
    public class PageDimensions
    {
        /// <summary>
        /// Gets or sets the landscape.
        /// </summary>
        [DataMember(Name = "Landscape")]
        public Size Landscape { get; set; }

        /// <summary>
        /// Gets or sets the portrait.
        /// </summary>
        [DataMember(Name = "Portrait")]
        public Size Portrait { get; set; }

        /// <summary>
        /// Gets or sets the portrait dual column.
        /// </summary>
        [DataMember(Name = "PortraitDualColumn")]
        public Size PortraitDualColumn { get; set; }

        /// <summary>
        /// Gets or sets the portrait dual column right note margin.
        /// </summary>
        [DataMember(Name = "PortraitDualColumnRightNoteMargin")]
        public Size PortraitDualColumnRightNoteMargin { get; set; }

        /// <summary>
        /// Gets or sets the portrait right note margin.
        /// </summary>
        [DataMember(Name = "PortraitRightNoteMargin")]
        public Size PortraitRightNoteMargin { get; set; }
    }
}