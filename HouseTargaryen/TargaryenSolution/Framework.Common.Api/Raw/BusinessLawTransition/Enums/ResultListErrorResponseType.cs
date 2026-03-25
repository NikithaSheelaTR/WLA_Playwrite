// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResultListErrorResponseType.cs" company="Thomson Reuters">
//   Copyright 2015: Thomson Reuters. All Rights Reserved. Proprietary
//   and Confidential information of Thomson Reuters. Disclosure, Use or
//   Reproduction without the written authorization of Thomson Reuters is
//   prohibited. 
// </copyright>
// <summary>
//   All Enum values are declared in this class
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Framework.Common.Api.Raw.BusinessLawTransition.Enums
{
    /// <summary>
    /// The result list error response type.
    /// </summary>
    public enum ResultListErrorResponseType
    {
        /// <summary>
        /// The missing company name.
        /// </summary>
        MissingCompanyName,

        /// <summary>
        /// The missing city.
        /// </summary>
        MissingCity,

        /// <summary>
        /// The missing state.
        /// </summary>
        MissingState,

        /// <summary>
        /// The missing zip.
        /// </summary>
        MissingZip,

        /// <summary>
        /// The missing report type.
        /// </summary>
        MissingReportType
    }
}