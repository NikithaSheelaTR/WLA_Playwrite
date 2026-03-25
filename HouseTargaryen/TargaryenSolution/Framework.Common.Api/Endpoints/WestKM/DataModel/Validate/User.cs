namespace Framework.Common.Api.Endpoints.WestKM.DataModel.Validate
{
    /// <summary>
    /// User
    /// </summary>
    public class User
    {
        /// <summary>
        /// Data Guid
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// MachineId
        /// </summary>
        public string MachineId { get; set; }

        /// <summary>
        /// MachineName
        /// </summary>
        public string MachineName { get; set; }

        /// <summary>
        /// Resource
        /// </summary>
        public string Resource { get; set; }

        /// <summary>
        /// RegKey
        /// </summary>
        public string RegKey { get; set; }
    }
}
