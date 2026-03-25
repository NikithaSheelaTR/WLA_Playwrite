namespace Framework.Core.DataModel.Configuration.Constants
{
    using System;

    /// <summary>
    /// Represents a Cobalt Product ID.
    /// </summary>
    [Flags]
    public enum CobaltProductId : uint
    {
        /// <summary>
        /// The ANZ.
        /// </summary>
        Anz = 0x020000000,

        /// <summary>
        /// A stub to represent an entity which is not a product or product view or whose identity is not significant.
        /// </summary>
        None = 0x00000000,

        /// <summary>
        /// The Case Notebook.
        /// </summary>
        CaseNotebook = 0x00000001,

        /// <summary>
        /// The Case Notebook.
        /// </summary>
        Cayman = 0x00000002,

        /// <summary>
        /// The Checkpoint Global.
        /// </summary>
        CheckpointGlobal = 0x040000000,

        /// <summary>
        /// The Session Info tool.
        /// </summary>
        CobaltSessionInfo = 0x00000004,

        /// <summary>
        /// The Shared Session Info tool.
        /// </summary>
        CobaltSharedSessionInfo = 0x00000008,

        /// <summary>
        /// The Concourse.
        /// </summary>
        Concourse = 0x00000010,

        /// <summary>
        /// The Drafting Assistant.
        /// </summary>
        DraftingAssistant = 0x00000020,

        /// <summary>
        /// The Firm Central.
        /// </summary>
        FirmCentral = 0x00000040,

        /// <summary>
        /// The Government Sites.
        /// </summary>
        GovtSites = 0x00000080,

        /// <summary>
        /// The Shared Session Info tool.
        /// </summary>
        LawSchool = 0x00000100,

        /// <summary>
        /// The internal Legal Services fictitious product.
        /// </summary>
        LegalServices = 0x00000200,

        /// <summary>
        /// The Pro View product.
        /// </summary>
        ProView = 0x00000400,

        /// <summary>
        /// The TaxNetPro 3.
        /// </summary>
        TaxNetPro3 = 0x00000800,

        /// <summary>
        /// The TaxnetPro 3 Aws.
        /// </summary>
        TaxnetPro3Aws = 0x08000000,

        /// <summary>
        /// The West KM service
        /// </summary>
        WestKm = 0x00001000,

        /// <summary>
        /// The Westlaw Next.
        /// </summary>
        WestlawNext = 0x00002000,

        /// <summary>
        /// The Westlaw Analytics.
        /// </summary>
        WlAnalytics = 0x00004000,

        /// <summary>
        /// The Westlaw Form Builder.
        /// </summary>
        WlFormBuilder = 0x00008000,

        /// <summary>
        /// The Westlaw Next Campus.
        /// </summary>
        WlnCampus = 0x00010000,

        /// <summary>
        /// The Westlaw Next Canada (Carswell).
        /// </summary>
        WlnCanada = 0x00020000,

        /// <summary>
        /// The Westlaw Next Canada Aws.
        /// </summary>
        WlnCanadaAws = 0x04000000,

        /// <summary>
        /// The Westlaw Next Correctional.
        /// </summary>
        WlnCorrectional = 0x00040000,

        /// <summary>
        /// The Westlaw Indigo
        /// </summary>
        WestlawEdge = 0x00080000,

        /// <summary>
        /// The Westlaw Indigo Premium
        /// </summary>
        WestlawEdgePremium = 0x00160000,

        /// <summary>
        /// The Westlaw Precision Aws
        /// </summary>
        WestlawPrecisionAws = 0x00320000,

        /// <summary>
        /// The Westlaw Global.
        /// </summary>
        WlnGlobal = 0x030000000,

        /// <summary>
        /// The Westlaw Next Links.
        /// </summary>
        WlnLinks = 0x00100000,

        /// <summary>
        /// The Westlaw Next Mobile.
        /// </summary>
        WlnMobile = 0x00200000,

        /// <summary>
        /// The WLN OpenWeb
        /// </summary>
        WlnOpenWeb = 0x00400000,

        /// <summary>
        /// The Westlaw Next Patron.
        /// </summary>
        WlnPatron = 0x00800000,

        /// <summary>
        /// The Westlaw Next Tax.
        /// </summary>
        WlnTax = 0x010000000
    }
}