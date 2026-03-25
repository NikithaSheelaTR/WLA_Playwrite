namespace Framework.Common.Api.Endpoints.AlertProductService.DataModel
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    using Framework.Common.Api.Endpoints.CobaltServices.DataModel.Alert;

    /// <summary>
    /// Destination data model
    /// </summary>
    [DataContract]
    public class DeliveryDestination
    {
        /// <summary>
        /// Properties
        /// </summary>
        [DataMember(Name = "properties")]
        public Properties Properties { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        [DataMember(Name = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Message Body
        /// </summary>
        [DataMember(Name = "messageBody")]
        public string MessageBody { get; set; }

        /// <summary>
        /// To
        /// </summary>
        [DataMember(Name = "to")]
        public List<string> To { get; set; }

        /// <summary>
        /// Subject
        /// </summary>
        [DataMember(Name = "subject")]
        public string Subject { get; set; }

        /// <summary>
        /// From
        /// </summary>
        [DataMember(Name = "from")]
        public string From { get; set; }

        /// <summary>
        /// To String
        /// </summary>
        [DataMember(Name = "toString")]
        public string toString { get; set; }

        /// <summary>
        /// Reply To
        /// </summary>
        [DataMember(Name = "replyTo")]
        public string ReplyTo { get; set; }
    }
}
