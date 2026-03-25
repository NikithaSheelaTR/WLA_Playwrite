namespace Framework.Core.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Class to obtain formatted date and time
    /// </summary>
    public static class DateTimeClass
    {
        private const string CstTimeZoneId = "Central Standard Time";

        private const string TimeZoneFormat = "MM/dd/yyyy h:mm tt";

        /// <summary>
        /// Get current CST date and time
        /// </summary>
        /// <returns>Current CTS Date Time</returns>
        public static DateTime GetCstDateTime()
        {
            return DateTimeClass.GetTimeZoneDateTime(CstTimeZoneId);
        }

        /// <summary>
        /// Converts the current UTC date and time to CST and formats using the culture.
        /// en-US is default culture
        /// </summary>
        /// <param name="cultureInfo"> The culture. </param>
        /// <returns> The <see cref="string"/> . </returns>
        public static string GetCstFormattedDateTime(CultureInfo cultureInfo = null)
        {
            return DateTimeClass.GetTimeZoneFormattedDateTime(CstTimeZoneId, cultureInfo);
        }

        /// <summary>
        /// Get current date and time of time zone
        /// </summary>
        /// <param name="timeZoneId">
        /// The time Zone Id.
        /// </param>
        /// <returns>
        /// Current Date Time
        /// </returns>
        public static DateTime GetTimeZoneDateTime(string timeZoneId)
        {
            TimeZoneInfo timezoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timezoneInfo);
        }

        /// <summary>
        /// Converts the current UTC date and time to CST and formats using the culture.
        /// en-US is default culture
        /// </summary>
        /// <param name="timeZoneId">
        /// The time Zone Id.
        /// </param>
        /// <param name="cultureInfo">
        /// The culture. 
        /// </param>
        /// <returns>
        /// The <see cref="string"/> . 
        /// </returns>
        public static string GetTimeZoneFormattedDateTime(string timeZoneId, CultureInfo cultureInfo = null)
        {
            return DateTimeClass.GetTimeZoneDateTime(timeZoneId)
                                .ToString(TimeZoneFormat, cultureInfo ?? new CultureInfo("en-US"));
        }

        /// <summary>
        /// Gets the years from set interval as string collection
        /// </summary>
        /// <param name="startingDate">starting date</param>
        /// <param name="endingDate">ending date</param>
        /// <returns>The string collections of years in set interval</returns>
        public static List<string> GetYearsAsStringCollection(DateTime startingDate, DateTime endingDate)
        {
            if (startingDate == null)
            {
                throw new NullReferenceException(
                    $"Object reference not set to an instance of an object. The parameter {nameof(startingDate)} can not be 'null'");
            }

            if (endingDate == null)
            {
                throw new NullReferenceException(
                    $"Object reference not set to an instance of an object. The parameter {nameof(startingDate)} can not be 'null'");
            }

            if (startingDate > endingDate)
            {
                throw new ArgumentOutOfRangeException(
                    $@"The condition '{nameof(startingDate)}'<'{nameof(endingDate)}' must be satisfied. 
                             \nActual values are : '{nameof(startingDate)}'={startingDate}, '{nameof(endingDate)}'={endingDate}");
            }

            return DateTimeClass.GetYearsAsStringCollection(startingDate.Year, endingDate.Year);
        }

        /// <summary>
        /// Gets the years from set interval as string collection
        /// </summary>
        /// <param name="startingYear">starting year</param>
        /// <param name="endingYear">ending year</param>
        /// <returns>The string collections of years in set interval</returns>
        public static List<string> GetYearsAsStringCollection(int startingYear, int endingYear)
        {
            if (startingYear < DateTime.MinValue.Year || startingYear > DateTime.MaxValue.Year)
            {
                throw new ArgumentOutOfRangeException($@"The parameter '{nameof(startingYear)}' must be in range [{DateTime
                    .MinValue},{DateTime.MinValue}]. 
                             \nActual value : '{nameof(startingYear)}'={startingYear}");
            }

            if (endingYear < DateTime.MinValue.Year || endingYear > DateTime.MaxValue.Year)
            {
                throw new ArgumentOutOfRangeException($@"The parameter '{nameof(endingYear)}' must be in range [{DateTime
                    .MinValue},{DateTime.MinValue}]. 
                             \nActual value : '{nameof(endingYear)}'={endingYear}");
            }

            return
                Enumerable.Range(startingYear, endingYear - startingYear + 1).Select(@int => @int.ToString()).ToList();
        }

        /// <summary>
        /// Gets the years from set interval as string collection
        /// </summary>
        /// <param name="startingDate">Start date</param>
        /// <param name="intervalInYears">Interval in years, also can be negative value</param>
        /// <returns>The string collections of years in set interval</returns>
        public static List<string> GetYearsAsStringCollection(this DateTime startingDate, int intervalInYears)
        {
            if (startingDate == null)
            {
                throw new NullReferenceException(
                    $"Object reference not set to an instance of an object. The parameter {nameof(startingDate)} can not be 'null'");
            }

            int startingYear = startingDate.Year;

            int endingYear = startingDate.AddYears(intervalInYears).Year;

            if (startingYear == endingYear)
            {
                return new List<string> { startingYear.ToString() };
            }

            return startingYear > endingYear
                       ? DateTimeClass.GetYearsAsStringCollection(endingYear, startingYear)
                       : DateTimeClass.GetYearsAsStringCollection(startingYear, endingYear);
        }

        /// <summary>
        /// Check if first date time is in range of second date time.
        /// </summary>
        /// <param name="firstDateTime">
        /// The first date time.
        /// </param>
        /// <param name="secondDateTime">
        /// The second date time.
        /// </param>
        /// <param name="range">
        /// The range in seconds
        /// </param>
        /// <returns>
        /// boolean
        /// </returns>
        public static bool IsInRange(this DateTime firstDateTime, DateTime secondDateTime, int range = 60)
        {
            TimeSpan difference = (firstDateTime - secondDateTime).Duration();
            return difference.TotalSeconds < range;
        }

        /// <summary>
        /// Given a date string returns the date object. This is for times when only given a year.
        /// </summary>
        /// <param name="dateString">date</param>
        /// <returns>converted date time</returns>
        public static DateTime ReturnDate(string dateString)
        {
            if (Regex.Match(dateString, "^\\d+$").Success)
            {
                return new DateTime(int.Parse(dateString), 1, 1);
            }

            return DateTime.Parse(dateString);
        }

        /// <summary>
        /// Converts string value to Date Time.
        /// </summary>
        /// <param name="value">
        /// String value
        /// Example: 01/05/2017 11:16 AM
        /// </param>
        /// <param name="format">
        /// The format.
        /// Example: MM/dd/yyyy h:mm tt
        /// </param>
        /// <param name="cultureInfo">
        /// The culture.
        /// </param>
        /// <returns>
        /// The <see cref="DateTime"/>.
        /// </returns>
        public static DateTime ToDateTime(
            this string value,
            string format = TimeZoneFormat,
            CultureInfo cultureInfo = null)
        {
            return DateTime.ParseExact(value, format, cultureInfo ?? new CultureInfo("en-US"));
        }

        /// <summary>
        /// Converts string value to Date Time.
        /// </summary>
        /// <param name="value">String value</param>
        /// <param name="format">The format.</param>
        /// <param name="cultureInfo">The culture.</param>
        /// <param name="dateTimeStyles">Date style</param>
        /// <returns>
        /// The <see cref="DateTime"/>.
        /// </returns>
        public static DateTime ToDateTime(this string value, string[] format, CultureInfo cultureInfo = null, DateTimeStyles dateTimeStyles = DateTimeStyles.AdjustToUniversal)
        {
            return DateTime.ParseExact(value, format, cultureInfo ?? new CultureInfo("en-US"), dateTimeStyles);
        }
    }
}