namespace Framework.Common.UI.Utils
{
    using System;

    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Execution;

    /// <summary>
    /// Preference Utils
    /// </summary>
    public static class PreferenceUtils
    {
        /// <summary>
        /// Get history event date time.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetHistoryEventDateTime()
        {
            return PreferenceUtils.GetHistoryEventDateTime(DateTime.Now);
        }

        /// <summary>
        /// Get history event date time.
        /// </summary>
        /// <param name="dateTime">
        /// The date Time.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetHistoryEventDateTime(DateTime dateTime)
        {
            string timezoneName = PreferenceUtils.GetTimezoneName();
            return
                TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.FindSystemTimeZoneById(timezoneName))
                            .ToString("MM/dd/yyyy h:mm tt");
        }

        /// <summary>
        /// Get Time zone Name
        /// </summary>
        /// <returns>Time zone Name</returns>
        public static string GetTimezoneName()
        {
            string result = null;
            SafeMethodExecutor.Execute(
                () =>
                    {
                        result =
                            (string)
                            DriverExtensions.ExecuteScript(
                                "return typeof Cobalt !== 'undefined' ? Cobalt.User.Preference.Get('Global', 'TimeZone') : Cobalt_Testing_Automation.Preferences.Get('Global', 'TimeZone')");
                    });

            return result ?? string.Empty;
        }
    }
}