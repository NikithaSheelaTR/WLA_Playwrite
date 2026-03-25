namespace Framework.Common.UI.Utils
{
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// The Utils to get some security headers.
    /// </summary>
    public class SecurityHeadersUtils
    {
        /// <summary>
        /// The mask to find the Ajax Token.
        /// </summary>
        private static readonly Regex AjaxTokenMaskRegex = new Regex("(?<=\"AjaxToken\":\")[\\w]{32,64}(?=\")");

        /// <summary>
        /// The mask to find the Page Event Identifier.
        /// </summary>
        private static readonly Regex PcidMaskRegex =
            new Regex("(?<=\"PageEventIdentifier\": \"|\"PageEventIdentifier\":\")([\\w]{32})");

        /// <summary>
        /// The mask to find the Requested Identity.
        /// </summary>
        private static readonly Regex RequestedIdentityMaskRegex =
            new Regex("(?<=\"RequestedIdentity\":{\"Id\":\")[\\w]{32,64}(?=\")");

        /// <summary>
        /// Returns the Ajax Token.
        /// </summary>
        /// <param name="sourceString">The string that matches Ajax Token.</param>
        /// <returns></returns>
        public static string GetAjaxToken(string sourceString) => AjaxTokenMaskRegex.Match(sourceString).ToString();

        /// <summary>
        /// Returns the Page Event Identifier.
        /// </summary>
        /// <param name="sourceString">The string that matches Page Event Identifier.</param>
        /// <returns></returns>
        public static string GetPcid(string sourceString) => PcidMaskRegex.Match(sourceString).ToString();

        /// <summary>
        /// Returns the Parent Transaction Identifier.
        /// </summary>
        /// <returns></returns>
        public static string GetPtid() => Guid.NewGuid().ToString("N").ToUpper();

        /// <summary>
        /// Returns the Requested Identity.
        /// </summary>
        /// <param name="sourceString">The string that matches Requested Identity.</param>
        /// <returns></returns>
        public static string GetRequestedIdentity(string sourceString)
            => RequestedIdentityMaskRegex.Match(sourceString).ToString();
    }
}