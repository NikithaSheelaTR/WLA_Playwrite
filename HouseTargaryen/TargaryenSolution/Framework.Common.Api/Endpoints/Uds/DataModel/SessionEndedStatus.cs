namespace Framework.Common.Api.Endpoints.Uds.DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The session status.
    /// </summary>
    [DataContract]
    public class SessionEndedStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SessionEndedStatus"/> class.
        /// </summary>
        /// <param name="errorItems">
        /// The error items.
        /// </param>
        public SessionEndedStatus(ErrorItems errorItems)
        {
            this.Error = errorItems;
        }

        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        [DataMember(Name = "error")]
        public ErrorItems Error { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is session ended.
        /// </summary>
        [DataMember(Name = "IsSessionEnded")]
        public bool IsSessionEnded { get; set; }
    }

    /// <summary>
    /// The error items.
    /// </summary>
    [DataContract]
    public class ErrorItems
    {
        /// <summary>
        /// Gets or sets the one pass.
        /// </summary>
        [DataMember(Name = "onePass")]
        public string OnePass { get; set; }
    }
}