namespace Framework.Core.Utils.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;

    using Framework.Core.DataModel.Security.Specialized;

    /// <summary>
    /// Class that loads in the credentials and then stores them
    /// </summary>
    [Serializable]
    [XmlRoot(ElementName = "credentials", Namespace = "", IsNullable = false)]
    public class UserFileCredentialRetriever
    {
        [XmlIgnore]
        private static readonly Random Randomiser = new Random();

        /// <summary>
        /// List of users read from the xml file
        /// </summary>
        [XmlElement(ElementName = "user")]
        public List<UserCredential> Users { get; set; }

        /// <summary>
        /// Reads in the users from the XML file and saves them in a list. 
        /// </summary>
        /// <param name="path">Location of the XML file</param>
        /// <returns>A new instance of the WlnCredentials class with a filled list of users from an XML file.</returns>
        public static UserFileCredentialRetriever LoadCredentials(string path)
        {
            UserFileCredentialRetriever credentials;

            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            {
                throw new FileNotFoundException("Credentials file name is invalid or the file does not exist.", path);
            }

            using (var reader = new StreamReader(path))
            {
                credentials = ObjectSerializer.DeserializeObject<UserFileCredentialRetriever>(reader.BaseStream);
                reader.Close();
            }

            return credentials;
        }

        /// <summary>
        /// Gets a Random user by the specified tag.
        /// </summary>
        /// <param name="tag">The tag to identify a user.</param>
        /// <returns>A user object that has the specified tag.</returns>
        public UserCredential GetRandomUserByTag(string tag)
        {
            List<UserCredential> userList = this.Users.FindAll(u => u.Tag != null && u.Tag == tag);
            return userList[Randomiser.Next(userList.Count)];
        }

        /// <summary>
        /// Gets a user by the specified tag.
        /// </summary>
        /// <param name="tag">The tag to identify a user.</param>
        /// <returns>A user object that has the specified tag.</returns>
        public UserCredential GetUserByTag(string tag)
        {
            return this.Users == null ? null : this.Users.FirstOrDefault(u => u.Tag != null && u.Tag == tag);
        }
    }
}