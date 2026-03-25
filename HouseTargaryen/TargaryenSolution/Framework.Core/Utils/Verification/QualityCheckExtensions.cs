namespace Framework.Core.Utils.Verification
{
    using System;
    using System.Globalization;
    using System.Linq;

    using Framework.Core.QualityChecks.Result;

    /// <summary>
    /// The quality check extensions.
    /// </summary>
    public static class QualityCheckExtensions
    {
        /// <summary>
        /// The to readable string.
        /// </summary>
        /// <param name="check">The check.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string ToReadableString(this QualityCheck check)
        {
            string result = string.Empty;

            if (check != null)
            {
                string[] values =
                    {
                        check.Order.ToString(CultureInfo.InvariantCulture),
                        check.QualityCheckType.ToString(),
                        check.Outcome.ToString(),
                        check.Name,
                        check.Message
                    };

                result = string.Join(" | ", values);
            }

            return result;
        }

        /// <summary>
        /// The write to console.
        /// </summary>
        /// <param name="qualityTestCase">The quality test case.</param>
        public static void WriteToConsole(this QualityTestCase qualityTestCase)
        {
            if (qualityTestCase != null && qualityTestCase.QualityChecks != null && qualityTestCase.QualityChecks.Any())
            {
                // Write the QC info to StdOut for backward compatibility with HTML QRT Report
                Console.WriteLine(
                    string.Join(Environment.NewLine, qualityTestCase.QualityChecks.Select(ch => ch.ToReadableString())));

                // Write the QC info to StdErr for backward compatibility with QReport
                Console.Error.WriteLine(qualityTestCase.ToString());
            }
        }
    }
}