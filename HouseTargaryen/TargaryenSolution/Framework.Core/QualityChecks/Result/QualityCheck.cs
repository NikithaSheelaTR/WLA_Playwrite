namespace Framework.Core.QualityChecks.Result
{
    using System;
    using System.Globalization;
    using System.Text;
    using System.Xml;

    using Framework.Core.Utils;
    using Framework.Core.Utils.Enums;

    /// <summary>
    /// Provides a means of storing information resulting from calls to Assertion and Verification methods.
    /// </summary>
    /// <remarks>
    /// The following Quality Check types are provided:
    /// <ul>
    /// <li><c>Assertion</c></li>
    /// <li><c>Verification</c></li>
    /// <li><c>NotApplicable</c></li>
    /// </ul>
    /// The following Quality Check outcomes are provided:
    /// <ul>
    /// <li><c>Passed</c></li>
    /// <li><c>Failed</c></li>
    /// <li><c>Inconclusive</c></li>
    /// </ul>
    /// A Quality Check is initialized to have a type NotApplicable and an outcome of Inconclusive, but will change based on Assertion method calls or Verification method calls. A Quality Check should only be associated with the Quality Check list of a single <see cref="QualityTestCase"/> instance. If associated with more than one instance, the Order value of the Quality Check is not guaranteed to be correct.
    /// </remarks>
    public class QualityCheck : IComparable<QualityCheck>
    {
        private string _name;

        /// <summary>
        /// Gets or sets the name of the Quality Check.
        /// </summary>
        /// <value>The name of the Quality Check.</value>
        /// <exception cref="ArgumentNullException">Thrown if the provided name is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the provided name is an empty string after being trimmed or its length exceeds 256 characters.</exception>
        public string Name
        {
            get { return this._name; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                string trimmedName = value.Trim();
                if (trimmedName.Length == 0)
                {
                    throw new ArgumentOutOfRangeException("value", "Name of QualityCheck cannot be an empty or blank string.");
                }
                if (trimmedName.Length > 256)
                {
                    throw new ArgumentOutOfRangeException("value", "Name of QualityCheck cannot exceed 256 characters.");
                }
                this._name = value;
            }
        }

        /// <summary>
        /// Gets or sets the type of the Quality Check.
        /// </summary>
        /// <value>The type of the Quality Check.</value>
        public QualityCheckType QualityCheckType { get; set; }

        /// <summary>
        /// Gets or sets the outcome of the Quality Check.
        /// </summary>
        /// <value>The outcome of the Quality Check.</value>
        public Outcome Outcome { get; set; }

        /// <summary>
        /// Gets or sets the date and time the Quality Check was asserted or verified.
        /// </summary>
        /// <value>The date and time the Quality Check was asserted or verified.</value>
        public DateTime? DateTime { get; set; }

        /// <summary>
        /// Gets or sets the message of the Quality Check that results from an Assertion method call or a Verification method call.
        /// </summary>
        /// <value>The message of the Quality Check that results from an Assertion method call or a Verification method call.</value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the order of the Quality Check when contained in a list of Quality Checks.
        /// </summary>
        /// <value>The order of the Quality Check when contained in a list of Quality Checks.</value>
        public int Order { get; set; }

        /// <summary>
        /// Initializes a new instance of the QualityCheck class and specifies the name.
        /// </summary>
        /// <param name="name">A name for the Quality Check.</param>
        /// <exception cref="ArgumentNullException">Thrown if the provided name is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the provided name is an empty string after being trimmed or its length exceeds 256 characters.</exception>
        public QualityCheck(string name)
        {
            Name = name;
            QualityCheckType = QualityCheckType.NotApplicable;
            Outcome = Outcome.Inconclusive;
        }

        /// <summary>
        /// Determines whether the specified <see cref="QualityCheck"/> is equal to the current <see cref="QualityCheck"/>.
        /// </summary>
        /// <param name="aThat">The <see cref="QualityCheck"/> to compare with the current <see cref="QualityCheck"/>.</param>
        /// <returns><b>true</b> if the specified Object is equal to the current <see cref="QualityCheck"/>; otherwise, <b>false</b>.</returns>
        public override bool Equals(Object aThat)
        {
            if (aThat == null || this.GetType() != aThat.GetType())
            {
                return false;
            }
            var that = (QualityCheck)aThat;
            return EqualsUtils.AreEqual(this.Name, that.Name) && EqualsUtils.AreEqual(this.QualityCheckType, that.QualityCheckType) &&
                EqualsUtils.AreEqual(this.Outcome, that.Outcome) && EqualsUtils.AreEqual(this.DateTime, that.DateTime) &&
                EqualsUtils.AreEqual(this.Order, that.Order);
        }


        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>A hash code for the current <see cref="QualityCheck"/>.</returns>
        public override int GetHashCode()
        {
            var result = HashCodeUtils.Seed;
            result = HashCodeUtils.Hash(result, this.Name);
            result = HashCodeUtils.Hash(result, this.QualityCheckType);
            result = HashCodeUtils.Hash(result, this.Outcome);
            result = HashCodeUtils.Hash(result, this.DateTime);
            result = HashCodeUtils.Hash(result, this.Message);
            result = HashCodeUtils.Hash(result, this.Order);
            return result;
        }


        /// <summary>
        /// Returns a string that represents the current <see cref="QualityCheck"/>.
        /// </summary>
        /// <returns>A string that represents the current <see cref="QualityCheck"/>.</returns>
        public override string ToString()
        {
            // write the list of checks out to XML
            var sb = new StringBuilder();
            var xmlSettings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "\t",
                OmitXmlDeclaration = true
            };
            using (var xmlWriter = XmlWriter.Create(sb, xmlSettings))
            {
                xmlWriter.WriteStartDocument();
                this.ToString(xmlWriter);
                xmlWriter.WriteEndDocument();
            }
            return sb.ToString();
        }


        /// <summary>
        /// Serializes this <see cref="QualityCheck"/> to XML.
        /// </summary>
        /// <param name="xmlWriter">An XmlWriter.</param>
        /// <exception cref="ArgumentNullException">Thrown if xmlWriter has not been initialized or is provided as null.</exception>
        /// <remarks>Ensure that the provided XmlWriter has already written the start of the XML document via <see cref="XmlWriter.WriteStartDocument()"/>, as this action is not taken as part of this method. Correspondingly, be sure to write the end of the XML document via <see cref="XmlWriter.WriteEndDocument()"/> after calling this method.</remarks>
        internal void ToString(XmlWriter xmlWriter)
        {
            if (xmlWriter == null)
            {
                throw new ArgumentNullException("xmlWriter");
            }

            xmlWriter.WriteStartElement("QualityCheck");
            xmlWriter.WriteElementString("Name", this.Name);
            xmlWriter.WriteElementString("Type", StringEnumUtils.GetStringValue(this.QualityCheckType));
            xmlWriter.WriteElementString("Outcome", this.Outcome.ToString());
            if (this.DateTime.HasValue)
            {
                xmlWriter.WriteElementString("DateTime", this.DateTime.Value.ToUniversalTime().ToString("o"));
            }
            xmlWriter.WriteElementString("Message", this.Message);
            xmlWriter.WriteElementString("Order", this.Order.ToString(CultureInfo.InvariantCulture));
            xmlWriter.WriteEndElement();
        }

        /// <summary>
        /// Compares this Quality Check to another Quality Check, ordering by <c>DateTime</c> ascending.
        /// </summary>
        /// <param name="aThat">The other <see cref="QualityCheck"/> against which to compare.</param>
        /// <returns>A signed number indicating the relative values of this instance and <c>aThat</c>.</returns>
        /// <remarks>
        /// A Quality Check with a <c>DateTime</c> value will be ordered ahead of a Quality Check without a <c>DateTime</c> value. If neither of the Quality Checks being compared has a <c>DateTime</c> value, ordering is determined by the <c>Order</c> values of the Quality Checks.
        /// </remarks>
        public int CompareTo(QualityCheck aThat)
        {
            const int before = -1;
            const int equal = 0;
            const int after = 1;

            var thatDateTime = aThat.DateTime;
            var thatOrder = aThat.Order;

            // first check has no DateTime
            if (this.DateTime == null)
            {
                // both checks have no DateTime, so return in the order they were created
                if (thatDateTime == null)
                {
                    if (this.Order < thatOrder)
                    {
                        return before;
                    }
                    return this.Order == thatOrder ? equal : after;
                }
                // second check has a DateTime
                return after;
            }
            // first check has a DateTime, but the second check does NOT have a DateTime
            if (thatDateTime == null)
            {
                return before;
            }
            // both checks have a DateTime, so compare DateTime's
            return System.DateTime.Compare((DateTime)this.DateTime, (DateTime)thatDateTime);
        }


        /// <summary>
        /// Sets the pre-condition values of this Quality Check.
        /// </summary>
        internal void SetPreConditionValues(QualityCheckType type)
        {
            this.QualityCheckType = type;
            this.DateTime = System.DateTime.UtcNow;
        }


        /// <summary>
        /// Sets this Quality Check to have an Outcome of Passed.
        /// </summary>
        internal void SetSuccessfulValues()
        {
            this.Outcome = Outcome.Passed;
        }


        /// <summary>
        /// Sets this Quality Check to have an Outcome of Failed.
        /// </summary>
        /// <param name="message">A message associated with the failure of this Quality Check.</param>
        internal void SetFailedValues(string message)
        {
            this.Outcome = Outcome.Failed;
            this.Message = message;
        }
    }
}
