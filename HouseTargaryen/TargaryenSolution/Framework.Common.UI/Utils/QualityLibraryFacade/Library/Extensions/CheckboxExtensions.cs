namespace Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions
{
    using Framework.Common.UI.Utils.Browser;

    using OpenQA.Selenium;

    using TRGR.Quality.QedArsenal.QualityLibrary.WebDriver.Extensions;

    /// <summary>
    /// The driver extensions.
    /// </summary>
    public static partial class DriverExtensions
    {
        /// <summary>
        /// Determines if the checkbox is selected
        /// </summary>
        /// <param name="container"></param>
        /// <param name="elementBys"></param>
        /// <returns></returns>
        public static bool IsCheckboxSelected(IWebElement container, params By[] elementBys)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.IsCheckboxSelected(container, elementBys));
        }

        /// <summary>
        /// Determines if the checkbox is selected
        /// </summary>
        /// <param name="elementBys">elementBy</param>
        /// <returns>true or false</returns>
        public static bool IsCheckboxSelected(params By[] elementBys)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.IsCheckboxSelected(elementBys));
        }

        /// <summary>
        /// Selects or deselects the checkbox
        /// </summary>
        /// <param name="elementBys">
        /// A variable number of WebElement By identifier arguments. The last argument corresponds to the desired 
        /// WebElement while the others correspond to any containers it might have
        /// </param>
        /// <param name="selected">
        /// The desired value of the checkbox (True for selected and false for deselected)
        /// </param>
        public static void SetCheckbox(By elementBys, bool selected)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.SetCheckbox(selected, elementBys));
        }

        /// <summary>
        /// Selects or deselects the checkbox
        /// </summary>
        /// <param name="selected">The desired value of the checkbox (True for selected and false for deselected)</param>
        /// <param name="value">The "value" HTML attribute or the text of the checkbox you want to select</param>
        public static void SetCheckbox(bool selected, string value)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.SetCheckbox(selected, value));
        }

        /// <summary>
        /// Selects or deselects the checkbox
        /// </summary>
        /// <param name="selected">The desired value of the checkbox (True for selected and false for deselected)</param>
        /// <param name="elementBys">
        /// A variable number of WebElement By identifier arguments. The last argument corresponds to the desired 
        /// WebElement while the others correspond to any containers it might have</param>
        public static void SetCheckbox(bool selected, params By[] elementBys)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.SetCheckbox(selected, elementBys));
        }

        /// <summary>
        /// Selects or deselects the checkbox
        /// </summary>
        /// <param name="selected">The desired value of the checkbox (True for selected and false for deselected)</param>
        /// <param name="container"></param>
        /// <param name="elementBys">
        /// A variable number of WebElement By identifier arguments. The last argument corresponds to the desired 
        /// WebElement while the others correspond to any containers it might have</param>
        public static void SetCheckbox(bool selected, IWebElement container, params By[] elementBys)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.SetCheckbox(selected, container, elementBys));
        }
    }
}