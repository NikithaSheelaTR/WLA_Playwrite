namespace Framework.Core.Utils.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;

    using Framework.Core.Utils.Execution;

    /// <summary>
    /// The string extensions.
    /// </summary>
    public static class StringExtensions
    {
        private const int InvalidCountValue = -1;

        /// <summary>
        /// The alphabetic characters regular expression.
        /// </summary>
        private const string LettersRegex = @"[a-z]|[A-Z]";

        /// <summary>
        /// The multiple spaces regular expression.
        /// </summary>
        private const string MultipleSpacesRegex = @"\s+";

        /// <summary>
        /// The only numbers regular expression.
        /// </summary>
        private const string NumbersInBracketsRegex = @"\(-?\d+([0-9,\.,\,,\s])*\)";

        /// <summary>
        /// The only numbers regular expression.
        /// </summary>
        private const string NumbersRegex = @"-?\d+([0-9,\.,\,,\s])*";

        /// <summary>
        /// The words regular expression.
        /// </summary>
        private const string RetainTextRegex = @"^[\s^\d\.]*(.+[A-z]+\)?)";

        /// <summary>
        /// To replace - (dash with whitespace),'()
        /// </summary>
        private const string NormalizeString = @"(-\s)?[',():]";

        /// <summary>
        /// To replace -- (one or two dashes)
        /// </summary>
        private const string NormalizeStringDash = @"-{1,2}";

        /// <summary>
        /// The only ellipsis regular expression
        /// </summary>
        private const string EllipsisRegex = @"\...";

        /// <summary>
        /// The westlaw guid regular expression
        /// </summary>
        private const string NegativeWestlawGuidRegex = "[^0-9a-zA-Z]";

        /// <summary>
        /// The positive westlaw guid regex.
        /// </summary>
        private const string PositiveWestlawGuidRegex = "[0-9a-zA-Z]{33}";

        /// <summary>
        /// The key.
        /// </summary>
        private const string Key = "6Xad7WDvoes=";

        /// <summary>
        /// The iv.
        /// </summary>
        private const string Iv = "XiuV4sdKjNI=";

        /// <summary>
        /// The random.
        /// </summary>
        private static readonly Random Randomizer = new Random();

        /// <summary>
        /// The contains.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="comparisonType">
        /// The comparison type.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool Contains(this string source, string value, StringComparison comparisonType) => source.IndexOf(value, comparisonType) >= 0;

        /// <summary>
        /// Returns true if original string contains any of items 
        /// </summary>
        /// <param name="inputStr">The input Str.</param>
        /// <param name="items">The items.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool ContainsAnyItem(this string inputStr, IEnumerable<string> items)
        {
            foreach (string str in items)
            {
                if (inputStr.Contains(str))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Extracts a count from a string as follows:
        /// FR-CA: 1 234 567 890,0
        /// EN-GB: 1,234,567,890.0
        /// EN-US: 1,234,567,890.0
        /// FE-CA: -1 234 567 890,0
        /// EN-GB: -1,234,567,890.0
        /// EN-US: -1,234,567,890.0
        /// Year: 1984
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>
        /// The converted Int32 value or -1 if conversion fails<see cref="int"/>.
        /// </returns>
        public static int ConvertCountToInt(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return InvalidCountValue;
            }

            Match match = Regex.Match(value, NumbersRegex);
            if (match.Success)
            {
                int countValue;
                if (int.TryParse(match.Value.Replace(" ",""), NumberStyles.Number, Thread.CurrentThread.CurrentCulture, out countValue))
                {
                    return countValue;
                }
            }

            return InvalidCountValue;
        }

        /// <summary>
        /// Gets the value of string type converted to desired.
        /// </summary>
        /// <param name="inputValue"> The input value. </param>
        /// <typeparam name="T"> The target reference type to convert the string setting value to. </typeparam>
        /// <returns> The desired type </returns>
        public static T ConvertToRefType<T>(this string inputValue) where T : class, IConvertible
        {
            T result = default(T);

            if (inputValue != null)
            {
                if (typeof(T) == typeof(string))
                {
                    result = (T)(object)inputValue;
                }
                else
                {
                    SafeMethodExecutor.Execute(() => result = (T)Convert.ChangeType(inputValue.Trim(), typeof(T)));
                }
            }

            return result;
        }

        /// <summary>
        /// Converts the value of string type to <see cref="Nullable{T}"/>.
        /// </summary>
        /// <param name="inputValue"> The input value. </param>
        /// <typeparam name="T"> The target value type to convert the string setting value to. </typeparam>
        /// <returns> The <see cref="Nullable{T}"/>. </returns>
        public static T? ConvertToValueType<T>(this string inputValue) where T : struct, IConvertible
        {
            T? result = default(T?);

            if (inputValue != null)
            {
                Type type = typeof(T);

                inputValue = inputValue.Trim();

                SafeMethodExecutor.Execute(
                    () =>
                        result =
                            type.IsEnum
                                ? (T)Enum.Parse(type, inputValue, true)
                                : (T)Convert.ChangeType(inputValue, type));
            }

            return result;
        }

        /// <summary>
        /// Generates a string of length 10 with the first 4 letters uppercase, 
        /// the next 4 letters numbers, and the last 2 letters as lowercase.
        /// </summary>
        /// <returns>string</returns>
        public static string GenerateRandomAlphaNumericString()
        {
            var builder = new StringBuilder();

            builder.Append(StringExtensions.RandomString(4, false));
            builder.Append(Randomizer.Next(0, 99999));
            builder.Append(StringExtensions.RandomString(2, true));
            return builder.ToString();
        }

        /// <summary>
        /// Generates a string from a random integer value.
        /// </summary>
        /// <returns>string</returns>
        public static string GenerateRandomNumericString() => Randomizer.Next(1, 99999).ToString();

        /// <summary>
        /// Removes letters.
        /// </summary>
        /// <param name="value">value.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string RemoveLetters(this string value) => Regex.Replace(value, LettersRegex, string.Empty);

        /// <summary>
        /// Removes multiple spaces
        /// </summary>
        /// <param name="value">value.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string RemoveMultipleSpaces(this string value) => Regex.Replace(value, MultipleSpacesRegex, " ");

        /// <summary>
        /// Retain only Text
        /// Example: References Cited (406) -> References Cited
        /// Example: 2. An apparatus, comprising: a reconstruction -> An apparatus, comprising: a reconstruction
        /// </summary>
        /// <param name="value">value.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string RetainText(this string value)
        {
            string result = string.Empty;
            Match match = Regex.Match(value, RetainTextRegex);

            if (match.Groups.Count > 1)
            {
                result = match.Groups[1].Value;
            }

            return result;
        }

        /// <summary>
        /// Retrieves a number embraced in brackets from the string and tries convert it to Int32.
        /// string "50 State Surveys (188)" returns 188
        /// </summary>
        /// <param name="value">
        /// a string containing a number in brackets.
        /// </param>
        /// <returns>
        /// The converted Int32 value or -1 if conversion fails. <see cref="int"/>.
        /// </returns>
        public static int RetrieveCountFromBrackets(this string value)
        {
            Match match = Regex.Match(value, NumbersInBracketsRegex);
            return match.Success ? match.Value.ConvertCountToInt() : InvalidCountValue;
        }

        /// <summary>
        /// Returns the text before ellipsis
        /// </summary>
        /// <param name="value">The text that should be split</param>
        /// <returns> Text before ellipsis </returns>
        public static string GetTextBeforeEllipsis(this string value) => Regex.Split(value, EllipsisRegex).First();

        /// <summary>
        /// Returns the text after ellipsis
        /// </summary>
        /// <param name="value">The text that should be split</param>
        /// <returns> Text after ellipsis </returns>
        public static string GetTextAfterEllipsis(this string value) => Regex.Split(value, EllipsisRegex).Last();

        /// <summary>
        /// Convert currency value to decimal format
        /// $11,789.67 => 11789.67
        /// </summary>
        /// <param name="value"> string to convert</param>
        /// <returns> Value in decimal format </returns>
        public static decimal ConvertCurrencyToDecimal(this string value) => Convert.ToDecimal(value.Replace("$", string.Empty));

        /// <summary>
        /// Convert currency value to file name format
        /// Bombardier Recreational Products, Inc. v. Arctic Cat, Inc. => Bombardier Recreational Products Inc v Arctic Cat Inc + '.' + extension
        /// </summary>
        /// <param name="value"> string to convert</param>
        /// <param name="extension"> file extension </param>
        /// <returns> Value in decimal format </returns>
        public static string ConvertToFileNameFormat(this string value, string extension) => new string(value.Where(ch => char.IsLetterOrDigit(ch) || char.IsWhiteSpace(ch)).ToArray()) + "." + extension;

        /// <summary>
        /// Encrypt the supplied clearText.
        /// </summary>
        /// <param name="text">String to be encrypted.</param>
        /// <returns>Encrypted string.</returns>
        public static string DesEncrypt(this string text)
        {
            var crypto = new DESCryptoServiceProvider
            {
                Key = Convert.FromBase64String(Key),
                IV = Convert.FromBase64String(Iv)
            };

            ICryptoTransform trans = crypto.CreateEncryptor();
            byte[] clearBytes = Encoding.UTF8.GetBytes(text);
            byte[] cypherBytes = trans.TransformFinalBlock(clearBytes, 0, clearBytes.Length);

            return Convert.ToBase64String(cypherBytes);
        }

        /// <summary>
        /// Generates a random string of the given length.
        /// </summary>
        /// <param name="size">Size of the string.</param>
        /// <param name="lowerCase">If true, generate lowercase string.</param>
        /// <returns>Random string.</returns>
        public static string RandomString(int size, bool lowerCase)
        {
            var builder = new StringBuilder();

            for (int i = 0; i < size; i++)
            {
                char ch = Convert.ToChar(Randomizer.Next(65, 91));

                builder.Append(ch);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }

        /// <summary>
        /// Normalize the Document Name for Trillium Result List
        /// for sorting publications on Library page(trillium) is used xml tag "md.contentstitle", from UI we do not have this value
        /// the following rules will give us this value from publication name
        /// firstly need to replace -with space and then only single -
        /// firstly need to replace "--" and then only "-" otherwise "--" will be replaced by two spaces instead of one
        /// </summary>
        /// <param name="docTitle">The document title</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string NormalizeDocumentName(this string docTitle)
            => Regex.Replace(Regex.Replace(docTitle, NormalizeString, string.Empty), NormalizeStringDash, " ").Replace("&", "and").Trim();

        /// <summary>
        /// Removes the articles from the Document Name for Trillium Result List
        /// </summary>
        /// <param name="docTitle">The document title</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string RemoveArticlesFromDocumentName(this string docTitle)
        {
            var articlesList = new List<string>() { "A ", "An ", "The " };
            articlesList.ForEach(article => docTitle = docTitle.StartsWith(article) ? docTitle.Replace(article, string.Empty) : docTitle);
            return docTitle;
        }

        /// <summary>
        /// Get substring before special characters
        /// input string: "Shared Annotations\r\nAddNote"; special character: "\r"; result: "Shared Annotations"
        /// </summary>
        /// <param name="text">Text</param>
        /// <param name="symbol">The symbols</param>
        /// <returns> Substring </returns>
        public static string GetSubstringBeforeSpecialCharacter(this string text, char symbol)
            => text.Contains(symbol) ? text.Substring(0, text.IndexOf(symbol)) : text;

        /// <summary>
        /// Determines whether string is westlaw guid.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>
        ///   <c>true</c> if string is westlaw guid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsWestlawGuid(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            // Technically, guids only allow 0-9a-fA-F, but to keep this simple and open for new guids in the future, we'll accept 0-9, a-z, A-Z
            bool containsBadGuidCharacters = Regex.IsMatch(input, NegativeWestlawGuidRegex);
            
            // guid length is 33 chars, so we should make sure that input string has that length
            return input.Length == 33 && !containsBadGuidCharacters;
        }

        /// <summary>
        /// The extract westlaw guid.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ExtractWestlawGuid(this string input)
        {
            var regex = new Regex(PositiveWestlawGuidRegex);
            MatchCollection matches = regex.Matches(input);
            return matches.Count > 0 ? matches[0].Value : string.Empty;
        }

        /// <summary>
        /// Creates a random name 
        /// </summary>
        /// <param name="tag">The name of the object that is being created (e.g. "Course", "Quiz", "Topic", etc.)</param>
        /// <returns>A string containing "Automation " + parameter + CurrentDate &amp; Time</returns>
        //  public static string CreateRandomName(string tag) => "Automation " + tag + " " + DateTime.Now.ToString("MM-dd-yyyy HH-mm-ss-ffff");
        public static string CreateRandomName(string tag) => "Automation " + tag + " " + DateTime.Now.ToString("HH-mm-ss");

    }
}