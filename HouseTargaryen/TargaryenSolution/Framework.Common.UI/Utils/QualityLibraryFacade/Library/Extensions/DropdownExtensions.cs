namespace Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions
{
    using System.Collections.Generic;

    using Framework.Common.UI.Utils.Browser;

    using OpenQA.Selenium;

    using TRGR.Quality.QedArsenal.QualityLibrary.WebDriver.Extensions;

    /// <summary>
    /// DriverExtensions
    /// </summary>
    public static partial class DriverExtensions
    {
        /// <summary>
        /// GetDropdownOptionElements
        /// </summary>
        /// <param name="elementBys">
        /// A variable number of WebElement By identifier arguments. The last argument corresponds to the desired 
        /// select dropdown WebElement while the others correspond to any containers it might have</param>
        /// <returns></returns>
        public static IList<IWebElement> GetDropdownOptionElements(params By[] elementBys)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.GetDropdownOptionElements(elementBys));
        }

        /// <summary>
        /// Gets Dropdown Option Text
        /// </summary>
        /// <param name="elementBys">
        /// A variable number of WebElement By identifier arguments. The last argument corresponds to the desired 
        /// select dropdown WebElement while the others correspond to any containers it might have
        /// </param>
        /// <returns>
        /// The List
        /// </returns>
        public static IList<string> GetDropdownOptionTexts(params By[] elementBys)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.GetDropdownOptionTexts(elementBys));
        }

        /// <summary>
        /// Gets Dropdown Option Values
        /// </summary>
        /// <param name="elementBys">
        /// A variable number of WebElement By identifier arguments. The last argument corresponds to the desired 
        /// select dropdown WebElement while the others correspond to any containers it might have
        /// </param>
        /// <returns>
        /// The List
        /// </returns>
        public static IList<string> GetDropdownOptionValues(params By[] elementBys)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.GetDropdownOptionValues(elementBys));
        }

        /// <summary>
        /// Gets Selected Dropdown Option Text
        /// </summary>
        /// <param name="elementBys">
        /// A variable number of WebElement By identifier arguments. The last argument corresponds to the desired 
        /// select dropdown WebElement while the others correspond to any containers it might have
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetSelectedDropdownOptionText(params By[] elementBys)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.GetSelectedDropdownOptionText(elementBys));
        }

        /// <summary>
        /// Gets the displayed text of the dropdown's selected option
        /// </summary>
        /// <param name="by">
        /// how to find the element
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetSelectElementSelectedText(By by)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.GetSelectElementSelectedText(by));
        }

        /// <summary>
        /// Selects the option corresponding to the specified text from the specified Select List
        /// </summary>
        /// <param name="by">
        /// how to find the element
        /// </param>
        /// <param name="textToSelect">
        /// the item to click from the select element
        /// </param>
        public static void SelectElementInListByText(By by, string textToSelect)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.SelectElementInListByText(by, textToSelect));
        }

        /// <summary>
        /// Sets the selected value of the specified dropdown to the specified option
        /// </summary>
        /// <param name="option">
        /// The option that should be selected (Can either be the displayed text of the option or the value attribute)
        /// </param>
        /// <param name="elementBys">
        /// A variable number of WebElement By identifier arguments. The last argument corresponds to the desired 
        /// select dropdown WebElement while the others correspond to any containers it might have
        /// </param>
        public static void SetDropdown(string option, params By[] elementBys)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.SetDropdown(option, elementBys));
        }
    }
}