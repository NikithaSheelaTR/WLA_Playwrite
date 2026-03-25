// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Enums.cs" company="Thomson Reuters">
//   Copyright 2015: Thomson Reuters. All Rights Reserved. Proprietary
//   and Confidential information of Thomson Reuters. Disclosure, Use or
//   Reproduction without the written authorization of Thomson Reuters is
//   prohibited.
// </copyright>
// <summary>
//   All enumeration values are declared in this class
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Framework.Common.UI.Constants.Foldering
{
    using TRGR.Quality.QedArsenal.QualityLibrary.Core.Utils.Enums;

    /// <summary>
    /// All enumeration values are declared in this class
    /// </summary>
    public sealed class Enums
    {
        /// <summary>
        /// The pool.
        /// </summary>
        public enum Pool
        {
            /// <summary>
            /// The general purpose pre-Prod.
            /// </summary>
            [StringValue("General_Purpose_Pre_Prod")]
            GeneralPurposePreProd, 

            /// <summary>
            /// The general purpose pre-Prod.
            /// </summary>
            [StringValue("WLN UK DR Certification Foldering")]
            WlnUkDataRoomGeneralPurposePreProd, 

            /// <summary>
            /// The migrated firm 1.
            /// </summary>
            [StringValue("MigratedFirm1")]
            MigratedFirm1, 

            /// <summary>
            /// The migrated firm 1.
            /// </summary>
            [StringValue("WLN UK DR Certification DRUK")]
            WlnUkDataRoomMigratedFirm1, 

            /// <summary>
            /// The migrated firm 2.
            /// </summary>
            [StringValue("MigratedFirm2")]
            MigratedFirm2, 

            /// <summary>
            /// The migrated firm 3.
            /// </summary>
            [StringValue("MigratedFirm3")]
            MigratedFirm3, 

            /// <summary>
            /// The training password.
            /// </summary>
            [StringValue("TrainingPassword")]
            TrainingPassword, 

            /// <summary>
            /// The patron user.
            /// </summary>
            [StringValue("PatronUser")]
            PatronUser, 

            /// <summary>
            /// The KM user.
            /// </summary>
            [StringValue("kmuser")]
            KmUser, 

            /// <summary>
            /// No sharing.
            /// </summary>
            [StringValue("NoSharing")]
            NoSharing
        }

        /// <summary>
        /// The vertical.
        /// </summary>
        public enum Vertical
        {
            /// <summary>
            /// The wln module and feature regression.
            /// </summary>
            [StringValue("WLN Module and Feature Regression")]
            WlnModuleAndFeatureRegression, 

            /// <summary>
            /// The foldering.
            /// </summary>
            [StringValue("Foldering")]
            Foldering, 

            /// <summary>
            /// The qed testing.
            /// </summary>
            [StringValue("QED_TESTING")]
            QedTesting, 

            /// <summary>
            /// The website.
            /// </summary>
            [StringValue("Website")]
            Website, 

            /// <summary>
            /// The events.
            /// </summary>
            [StringValue("Events")]
            Events, 

            /// <summary>
            /// The online platform.
            /// </summary>
            [StringValue("OnlinePlatform")]
            OnlinePlatform, 

            /// <summary>
            /// The forms assembly.
            /// </summary>
            [StringValue("Forms Assembly")]
            FormsAssembly, 

            /// <summary>
            /// The alert.
            /// </summary>
            [StringValue("ALERT")]
            Alert, 

            /// <summary>
            /// The bankruptcy.
            /// </summary>
            [StringValue("Bankruptcy")]
            Bankruptcy, 

            /// <summary>
            /// The mobile.
            /// </summary>
            [StringValue("Mobile")]
            Mobile, 

            /// <summary>
            /// The document.
            /// </summary>
            Document, 

            /// <summary>
            /// The uds.
            /// </summary>
            Uds
        }

        /// <summary>
        /// Grid Component Columns 
        /// </summary>
        public enum Columns
        {
            /// <summary>
            /// The added by.
            /// </summary>
            AddedBy, 

            /// <summary>
            /// The client id.
            /// </summary>
            ClientId, 

            /// <summary>
            /// The content.
            /// </summary>
            Content, 

            /// <summary>
            /// The date added.
            /// </summary>
            DateAdded, 

            /// <summary>
            /// The date time.
            /// </summary>
            DateTime, 

            /// <summary>
            /// The event.
            /// </summary>
            Event, 

            /// <summary>
            /// The description.
            /// </summary>
            Description
        }

        /// <summary>
        /// The collaborator roles.
        /// </summary>
        public enum CollaboratorRoles
        {
            /// <summary>
            /// The reviewer.
            /// </summary>
            [StringValue("reviewer")]
            Reviewer, 

            /// <summary>
            /// The contributor.
            /// </summary>
            [StringValue("contributor")]
            Contributor, 

            /// <summary>
            /// The owner.
            /// </summary>
            [StringValue("owner")]
            Owner
        }

        // TODO: merge with Framework.Common.UI.Enums.Delivery.WhatToDeliver
        /// <summary>
        /// The what to deliver.
        /// </summary>
        public enum WhatToDeliver
        {
            /// <summary>
            /// The list.
            /// </summary>
            List, 

            /// <summary>
            /// The documents.
            /// </summary>
            Documents, 

            /// <summary>
            /// The document and annotations.
            /// </summary>
            DocumentAndAnnotations, 

            /// <summary>
            /// The doc original document.
            /// </summary>
            DocOriginalDocument, 

            /// <summary>
            /// The doc original version with WLN doc.
            /// </summary>
            DocOrigionalVersionWithWlnDoc
        }

        /// <summary>
        /// The content type facet.
        /// </summary>
        public enum ContentTypeFacet
        {
            /// <summary>
            /// The cases.
            /// </summary>
            [StringValue("Cases")]
            Cases, 

            /// <summary>
            /// The briefs.
            /// </summary>
            [StringValue("Briefs")]
            Briefs, 

            /// <summary>
            /// The statutes.
            /// </summary>
            [StringValue("Statutes")]
            Statutes, 

            /// <summary>
            /// The secondary sources.
            /// </summary>
            [StringValue("Secondary Sources")]
            SecondarySources, 

            /// <summary>
            /// The jury verdicts.
            /// </summary>
            [StringValue("JuryVerdicts")]
            JuryVerdicts, 

            /// <summary>
            /// The pleadings.
            /// </summary>
            [StringValue("Pleadings")]
            Pleadings, 

            /// <summary>
            /// The regulations.
            /// </summary>
            [StringValue("Regulations")]
            Regulations, 

            /// <summary>
            /// The km.
            /// </summary>
            [StringValue("KM")]
            Km, 

            /// <summary>
            /// The uploaded document.
            /// </summary>
            [StringValue("Uploaded Document")]
            UploadedDocument, 

            /// <summary>
            /// The trial court orders.
            /// </summary>
            [StringValue("Trial Court Orders")]
            TrialCourtOrders
        }
    }
}
