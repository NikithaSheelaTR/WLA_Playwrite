// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EbcGetDocumentContract.cs" company="Thomson Reuters">
//   Copyright 2015: Thomson Reuters. All Rights Reserved. Proprietary
//   and Confidential information of Thomson Reuters. Disclosure, Use or
//   Reproduction without the written authorization of Thomson Reuters is
//   prohibited. 
// </copyright>
// <summary>
//   The ebc get document contract.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Framework.Common.Api.Raw.BusinessLawTransition.Contracts.ExperianBusinessCreditContracts
{
    using System;
    using System.Collections.Generic;
    using System.Xml;

    /// <summary>
    /// The ebc get document contract.
    /// </summary>
    public class EbcGetDocumentContract : IContract
    {
        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        public string createdDate { get; set; }

        /// <summary>
        /// Gets or sets the expiry.
        /// </summary>
        public Expiry expiry { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Gets or sets the orchestration.
        /// </summary>
        public string orchestration { get; set; }

        /// <summary>
        /// Gets or sets the section count.
        /// </summary>
        public int sectionCount { get; set; }

        /// <summary>
        /// Gets or sets the sections.
        /// </summary>
        public List<Section> sections { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        public string state { get; set; }

        /// <summary>
        /// Gets or sets the updated date.
        /// </summary>
        public string updatedDate { get; set; }

        /// <summary>
        /// The is valid.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsValid()
        {
            return this.Validations().Valid();
        }

        /// <summary>
        /// The validations.
        /// </summary>
        /// <returns>
        /// The <see cref="ContractValidations"/>.
        /// </returns>
        public ContractValidations Validations()
        {
            // verify at least one section was returned
            var validations = new ContractValidations();
            validations.Add("Section count is 1 ", this.sectionCount == 1);

            // verify valid xml document recieved
            var xmlDoc = new XmlDocument();
            bool validXML = false;
            if ((this.sections.Count > 0) && (this.sections[0].Data.Count > 0))
            {
                string xmlData = this.sections[0].Data[0].orchestrationData.xml;
                try
                {
                    xmlDoc.LoadXml(xmlData);
                    validXML = true;
                }
                catch (Exception)
                {
                    validXML = false;
                }
            }

            validations.Add("Recieved valid xml document ", validXML);

            return validations;
        }

        /// <summary>
        /// The datum.
        /// </summary>
        public class Datum
        {
            /// <summary>
            /// Gets or sets the created date.
            /// </summary>
            public string createdDate { get; set; }

            /// <summary>
            /// Gets or sets the id.
            /// </summary>
            public string id { get; set; }

            /// <summary>
            /// Gets or sets the index.
            /// </summary>
            public int index { get; set; }

            /// <summary>
            /// Gets or sets the name.
            /// </summary>
            public string name { get; set; }

            /// <summary>
            /// Gets or sets the orchestration data.
            /// </summary>
            public OrchestrationData orchestrationData { get; set; }

            /// <summary>
            /// Gets or sets the royalty data.
            /// </summary>
            public List<object> royaltyData { get; set; }

            /// <summary>
            /// Gets or sets the size.
            /// </summary>
            public int size { get; set; }

            /// <summary>
            /// Gets or sets the updated date.
            /// </summary>
            public string updatedDate { get; set; }
        }

        /// <summary>
        /// The expiry.
        /// </summary>
        public class Expiry
        {
            /// <summary>
            /// Gets or sets the duration.
            /// </summary>
            public string duration { get; set; }

            /// <summary>
            /// Gets or sets the type.
            /// </summary>
            public string type { get; set; }
        }

        /// <summary>
        /// The orchestration data.
        /// </summary>
        public class OrchestrationData
        {
            /// <summary>
            /// Gets or sets the xml.
            /// </summary>
            public string xml { get; set; }
        }

        /// <summary>
        /// The section.
        /// </summary>
        public class Section
        {
            /// <summary>
            /// Gets or sets the created date.
            /// </summary>
            public string createdDate { get; set; }

            /// <summary>
            /// Gets or sets the data.
            /// </summary>
            public List<Datum> Data { get; set; }

            /// <summary>
            /// Gets or sets the id.
            /// </summary>
            public string id { get; set; }

            /// <summary>
            /// Gets or sets the index.
            /// </summary>
            public int index { get; set; }

            /// <summary>
            /// Gets or sets the name.
            /// </summary>
            public string name { get; set; }

            /// <summary>
            /// Gets or sets the size.
            /// </summary>
            public int size { get; set; }

            /// <summary>
            /// Gets or sets the updated date.
            /// </summary>
            public string updatedDate { get; set; }
        }
    }
}