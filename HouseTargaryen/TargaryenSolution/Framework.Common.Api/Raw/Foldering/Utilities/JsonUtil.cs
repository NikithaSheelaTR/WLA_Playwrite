// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JsonUtil.cs" company="Thomson Reuters">
//   Copyright 2015: Thomson Reuters. All Rights Reserved. Proprietary
//   and Confidential information of Thomson Reuters. Disclosure, Use or
//   Reproduction without the written authorization of Thomson Reuters is
//   prohibited.
// </copyright>
// <summary>
//   Utility to work with JSon
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Framework.Common.Api.Raw.Foldering.Utilities
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Web.Script.Serialization;

    /// <summary>
    /// Utility to work with JSon
    /// </summary>
    public static class JsonUtil
    {
        /// <summary>
        /// put the Json into array
        /// </summary>
        /// <param name="readOnlyCollection">
        /// Collection to put into array
        /// </param>
        /// <returns>
        ///  </returns>
        public static string[] JsReturnToArray(this ReadOnlyCollection<object> readOnlyCollection)
        {
            return readOnlyCollection.Select(o => o.ToString()).ToArray();
        }

        /// <summary>
        /// serialize the object to string
        /// </summary>
        /// <param name="obj">
        /// object
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ToJson(this object obj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(obj);
        }
    }
}