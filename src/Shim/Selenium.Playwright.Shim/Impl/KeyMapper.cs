using System.Collections.Generic;
using OpenQA.Selenium;

namespace Selenium.Playwright.Shim.Impl
{
    internal static class KeyMapper
    {
        private static readonly Dictionary<string, string> KeyMap = new Dictionary<string, string>
        {
            { Keys.Enter, "Enter" },
            { Keys.Return, "Enter" },
            { Keys.Tab, "Tab" },
            { Keys.Escape, "Escape" },
            { Keys.Backspace, "Backspace" },
            { Keys.Delete, "Delete" },
            { Keys.Space, " " },
            { Keys.Control, "Control" },
            { Keys.LeftControl, "Control" },
            { Keys.Shift, "Shift" },
            { Keys.LeftShift, "Shift" },
            { Keys.Alt, "Alt" },
            { Keys.LeftAlt, "Alt" },
            { Keys.Home, "Home" },
            { Keys.End, "End" },
            { Keys.PageUp, "PageUp" },
            { Keys.PageDown, "PageDown" },
            { Keys.Up, "ArrowUp" },
            { Keys.ArrowUp, "ArrowUp" },
            { Keys.Down, "ArrowDown" },
            { Keys.ArrowDown, "ArrowDown" },
            { Keys.Left, "ArrowLeft" },
            { Keys.ArrowLeft, "ArrowLeft" },
            { Keys.Right, "ArrowRight" },
            { Keys.ArrowRight, "ArrowRight" },
            { Keys.Insert, "Insert" },
            { Keys.F1, "F1" },
            { Keys.F2, "F2" },
            { Keys.F3, "F3" },
            { Keys.F4, "F4" },
            { Keys.F5, "F5" },
            { Keys.F6, "F6" },
            { Keys.F7, "F7" },
            { Keys.F8, "F8" },
            { Keys.F9, "F9" },
            { Keys.F10, "F10" },
            { Keys.F11, "F11" },
            { Keys.F12, "F12" },
            { Keys.Meta, "Meta" },
        };

        public static string MapKeys(string seleniumKeys)
        {
            if (string.IsNullOrEmpty(seleniumKeys)) return seleniumKeys;

            // Handle compound keys like Control+a
            var result = new System.Text.StringBuilder();
            foreach (char c in seleniumKeys)
            {
                string key = c.ToString();
                if (KeyMap.TryGetValue(key, out string mapped))
                {
                    if (result.Length > 0) result.Append("+");
                    result.Append(mapped);
                }
                else
                {
                    result.Append(c);
                }
            }
            return result.ToString();
        }
    }
}
