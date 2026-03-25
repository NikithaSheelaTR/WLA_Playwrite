// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResultListResponseType.cs" company="Thomson Reuters">
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
    /// The result list response type.
    /// </summary>
    public enum ResultListResponseType
    {
        /// <summary>
        /// The business summary.
        /// </summary>
        BusinessSummary,

        /// <summary>
        /// The IntelliScore plus.
        /// </summary>
        IntelliscorePlus,

        /// <summary>
        /// The premier profile.
        /// </summary>
        PremierProfile,

        /// <summary>
        /// The error response.
        /// </summary>
        ErrorResponse
    }
}