namespace Framework.Core.DataModel.Configuration.Constants
{
    using System;

    /// <summary>
    /// Represents a Cobalt Module ID.
    /// </summary>
    [Flags]
    public enum CobaltModuleId : uint
    {
        /// <summary>
        /// A stub to represent an entity which is not a module or whose module identity is not significant.
        /// </summary>
        None = 0x00000000,

        /// <summary>
        /// The Active Dashboard Container.
        /// </summary>
        Adc = 0x00000001,

        /// <summary>
        /// The Alerts.
        /// </summary>
        Alerts = 0x00000002,

        /// <summary>
        /// The Alert Product Service.
        /// </summary>
        AlertProductService = 0x00000003,

        /// <summary>
        /// The Alerts Engine.
        /// </summary>
        AlertsEngine = 0x00000004,

        /// <summary>
        /// The cobalt services.
        /// </summary>
        CobaltServices = 0x00000008,

        /// <summary>
        /// The case notebook.
        /// </summary>
        CaseNotebook = 0x00100000,

        /// <summary>
        /// The Data Orchestration.
        /// </summary>
        DataOrchestration = 0x00000010,

        /// <summary>
        /// The Document.
        /// </summary>
        Document = 0x00000020,

        /// <summary>
        /// The Document.
        /// </summary>
        DocPersist = 0x00000030,

        /// <summary>
        /// The Entity ID.
        /// </summary>
        EntityId = 0x00000040,

        /// <summary>
        /// The Research Organizer.
        /// </summary>
        Foldering = 0x00000080,

        /// <summary>
        /// The Forms Assembly.
        /// </summary>
        FormsAssembly = 0x00000100,

        /// <summary>
        /// The DoGateway.
        /// </summary>
        DOGateway = 0x00000200,

        /// <summary>
        /// The Gateway.
        /// </summary>
        Gateway = 0x00000202,

        /// <summary>
        /// The Image.
        /// </summary>
        Image = 0x00000400,

        /// <summary>
        /// The Link Resolver.
        /// </summary>
        LinkResolver = 0x00000800,

        /// <summary>
        /// The Nlu
        /// </summary>
        Nlu = 0x00200000,

        /// <summary>
        /// The Related Information.
        /// </summary>
        RelatedInformation = 0x00001000,

        /// <summary>
        /// The Report.
        /// </summary>
        Report = 0x00002000,

        /// <summary>
        /// The Search.
        /// </summary>
        Search = 0x00004000,

        /// <summary>
        /// The Security.
        /// </summary>
        Security = 0x00080000,

        /// <summary>
        /// The Static Content.
        /// </summary>
        StaticContent = 0x00008000,

        /// <summary>
        /// The TypeAhead
        /// </summary>
        TypeAhead = 0x00400000,

        /// <summary>
        /// The UDS.
        /// </summary>
        Uds = 0x00010000,

        /// <summary>
        /// The Web Content.
        /// </summary>
        WebContent = 0x00020000,

        /// <summary>
        /// The Website.
        /// </summary>
        Website = 0x00040000,

        /// <summary>
        /// The OnePassWeb
        /// </summary>
        OnePassWeb = 0x00800000,

        /// <summary>
        /// The Omr
        /// </summary>
        Omr = 0x00800002,

        /// <summary>
        /// The OnePassV3
        /// </summary>
        OnePassV3 = 0x00800004,

        /// <summary>
        /// The SearchSpellChecker
        /// </summary>
        SearchSpellChecker = 0x00800008,

        /// <summary>
        /// The Cari
        /// </summary>
        Cari = 0x00800016
    }
}