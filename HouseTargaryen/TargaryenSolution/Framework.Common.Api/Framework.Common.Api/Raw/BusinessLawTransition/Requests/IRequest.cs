// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRequest.cs" company="Thomson Reuters">
//   Copyright 2015: Thomson Reuters. All Rights Reserved. Proprietary
//   and Confidential information of Thomson Reuters. Disclosure, Use or
//   Reproduction without the written authorization of Thomson Reuters is
//   prohibited. 
// </copyright>
// <summary>
//   request Interface
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Framework.Common.Api.Raw.BusinessLawTransition.Requests
{
    /// <summary>
    /// request Interface
    /// </summary>
    public interface IRequest
    {
        /// <summary>
        /// Get Request body
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string GetRequestBody();
    }
}