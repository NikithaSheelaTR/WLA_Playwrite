namespace Framework.Common.Api.Endpoints.DataOrchestration.DataModel.Judicial.RequestModels
{
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// Current party info
    /// </summary>
    public class PartyInfoRequest
    {
        /// <summary>
        /// party name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; private set; }

        /// <summary>
        /// party id
        /// TODO get it from response when endpoint will be finished
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; private set; }

        /// <summary>
        /// Files that are uploaded to report
        /// </summary>
        [JsonProperty("files")]
        public List<FileInfoRequest> Files { get; private set; }

        /// <summary>
        /// The add name.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="PartyInfoRequest"/>.
        /// </returns>
        public PartyInfoRequest AddName(string name)
        {
            this.Name = name;
            return this;
        }

        /// <summary>
        /// Add report id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public PartyInfoRequest AddId(string id)
        {
            this.Id = id;
            return this;
        }

        /// <summary>
        /// Add file info
        /// </summary>
        /// <param name="fileName">fileName</param>
        /// <param name="fileId">fileId</param>
        /// <returns></returns>
        public PartyInfoRequest AddFileInfo(string fileName, string fileId)
        {
            this.Files = this.Files ?? new List<FileInfoRequest>();
            this.Files.Add(new FileInfoRequest() { Name = fileName, Id = fileId });
            return this;
        }
    }
}
