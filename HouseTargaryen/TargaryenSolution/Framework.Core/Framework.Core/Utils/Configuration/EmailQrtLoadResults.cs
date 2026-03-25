namespace Framework.Core.Utils.Configuration
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// Holds values associated with the <c>emailQrtLoadResults</c> field in a <c>QrtTestConfig.xml</c> file.
    /// </summary>
    [Serializable]
    public sealed class EmailQrtLoadResults
    {
        /// <summary>
        /// Gets or sets a semi-colon delimited list of email addresses to which the the result of a QRT load will be sent.
        /// </summary>
        /// <value>A semi-colon delimited list of email addresses to which the the result of a QRT load will be sent.</value>
        [XmlText]
        public string EmailAddresses { get; set; }

        /// <summary>
        /// Gets or sets an indication whether the result of a QRT load will be only be emailed if the load to QRT fails.
        /// </summary>
        /// <value>An indication whether the result of a QRT load will be only be emailed if the load to QRT fails.</value>
        [XmlAttribute("onlyOnFailure")]
        public bool OnlyOnFailure { get; set; }

        /// <summary>
        /// Determines whether the specified <see cref="EmailQrtLoadResults"/> is equal to the current <see cref="EmailQrtLoadResults"/>.
        /// </summary>
        /// <param name="aThat">The <see cref="EmailQrtLoadResults"/> to compare with the current <see cref="EmailQrtLoadResults"/>.</param>
        /// <returns><b>true</b> if the specified Object is equal to the current <see cref="EmailQrtLoadResults"/>; otherwise, <b>false</b>.</returns>
        public override bool Equals(object aThat)
        {
            if (aThat == null || this.GetType() != aThat.GetType())
            {
                return false;
            }

            var that = (EmailQrtLoadResults)aThat;
            return EqualsUtils.AreEqual(this.OnlyOnFailure, that.OnlyOnFailure)
                   && EqualsUtils.AreEqual(this.EmailAddresses, that.EmailAddresses);
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>A hash code for the current <see cref="EmailQrtLoadResults"/>.</returns>
        public override int GetHashCode()
        {
            var result = HashCodeUtils.Seed;
            result = HashCodeUtils.Hash(result, this.OnlyOnFailure);
            result = HashCodeUtils.Hash(result, this.EmailAddresses);
            return result;
        }

        /// <summary>
        /// Returns a string that represents the current <see cref="EmailQrtLoadResults"/>.
        /// </summary>
        /// <returns>A string that represents the current <see cref="EmailQrtLoadResults"/>.</returns>
        public override string ToString()
        {
            var serializer = new XmlSerializer(typeof(EmailQrtLoadResults));

            using (var memoryStream = new MemoryStream())
            {
                using (var xmlWriter = XmlWriter.Create(memoryStream))
                {
                    serializer.Serialize(xmlWriter, this);
                }

                return Encoding.UTF8.GetString(memoryStream.ToArray());
            }
        }
    }
}