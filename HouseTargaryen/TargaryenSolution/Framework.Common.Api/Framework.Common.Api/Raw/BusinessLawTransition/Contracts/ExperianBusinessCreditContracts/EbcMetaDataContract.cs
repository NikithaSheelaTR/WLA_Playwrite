// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EbcMetaDataContract.cs" company="Thomson Reuters">
//   Copyright 2015: Thomson Reuters. All Rights Reserved. Proprietary
//   and Confidential information of Thomson Reuters. Disclosure, Use or
//   Reproduction without the written authorization of Thomson Reuters is
//   prohibited. 
// </copyright>
// <summary>
//   The ebc meta data contract.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Framework.Common.Api.Raw.BusinessLawTransition.Contracts.ExperianBusinessCreditContracts
{
    using System.Collections.Generic;

    /// <summary>
    /// The ebc meta data contract.
    /// </summary>
    public class EbcMetaDataContract : IContract
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
            validations.Add("Meta Data Status is, " + this.state, this.state.Equals("COMPLETE"));
            return validations;
        }

        /// <summary>
        /// The datum.
        /// </summary>
        public class Datum
        {
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
            public List<Datum> data { get; set; }

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