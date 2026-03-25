namespace Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions
{
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Custom;

    using OpenQA.Selenium;

    using TRGR.Quality.QedArsenal.QualityLibrary.Core.Enums.WebDriver;

    /// <summary>
    /// The driver extensions.
    /// </summary>
    public partial class DriverExtensions
    {
        /// <summary>
        /// Press key
        /// </summary>
        /// <param name="key"> Key </param>
        public static void PressKey(string key)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.PressKey(key));
        }

        /// <summary>
        /// Press tab key
        /// </summary>
        public static void PressTabKey(int count=1)
        {
            for(int i = 0; i < count; i++)
            {
                PressKey(Keys.Tab);
            }
        }

        /// <summary>
        /// Press enter key
        /// </summary>
        public static void PressEnterKey()
        {
            PressKey(Keys.Enter);
        }
    }
}