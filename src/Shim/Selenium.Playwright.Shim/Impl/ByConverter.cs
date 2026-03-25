using OpenQA.Selenium;

namespace Selenium.Playwright.Shim.Impl
{
    internal static class ByConverter
    {
        public static string ToPlaywrightLocator(By by)
        {
            switch (by.Mechanism)
            {
                case "id":
                    return $"#{EscapeCss(by.Value)}";
                case "cssSelector":
                    return by.Value;
                case "xpath":
                    return $"xpath={by.Value}";
                case "className":
                    return $".{EscapeCss(by.Value)}";
                case "tagName":
                    return by.Value;
                case "name":
                    return $"[name='{EscapeAttr(by.Value)}']";
                case "linkText":
                    return $"a:has-text('{EscapeText(by.Value)}')";
                case "partialLinkText":
                    return $"a:has-text('{EscapeText(by.Value)}')";
                default:
                    throw new WebDriverException($"Unsupported By mechanism: {by.Mechanism}");
            }
        }

        private static string EscapeCss(string value)
        {
            // Basic CSS escaping for IDs and class names with special characters
            return value.Replace(":", "\\:").Replace(".", "\\.").Replace("[", "\\[").Replace("]", "\\]");
        }

        private static string EscapeAttr(string value)
        {
            return value.Replace("'", "\\'");
        }

        private static string EscapeText(string value)
        {
            return value.Replace("'", "\\'");
        }
    }
}
