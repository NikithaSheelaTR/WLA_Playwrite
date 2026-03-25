namespace Framework.Common.UI.Utils.QualityLibraryFacade.Custom
{
    using System;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.CommonTypes.Constants;
    using Framework.Core.CommonTypes.Enums.Setup;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;
    using Framework.Common.UI.Utils.Browser;
    using TRGR.Quality.QedArsenal.QualityLibrary.WebDriver.Extensions;

    /// <summary>
    /// Click extension
    /// </summary>
    public static partial class CustomExtensions
    {
        /// <summary>
        /// Clicks on element even it's out of view
        /// </summary>
        ///  <summary>
        /// Settings
        /// </summary>
        public static TestSettings Settings { get; private set; }

        /// <param name="driver">driver</param>
        /// <param name="elementBys">
        /// A variable number of WebElement By identifier arguments. The last argument corresponds to the desired 
        /// WebElement while the others correspond to any containers it might have
        /// </param>
        public static void Click(this IWebDriver driver, params By[] elementBys)
        {
            try
            {                
                if (BrowserPool.CurrentBrowser.BrowserInfo.Alias.Equals(BrowserUnderTest.EDGE.ToString()))
                {
                    ClickExtensions.JavascriptClick(driver, elementBys);
                }
                else
                {
                    ClickExtensions.Click(driver, elementBys);
                }
            }
            catch (InvalidOperationException)
            {
                driver.ScrollIntoView(new ByChained(elementBys));
                ClickExtensions.Click(driver, elementBys);
            }
        }

        /// <summary>
        /// Clicks on element even it's out of view
        /// </summary>
        /// <param name="driver">driver</param>
        /// <param name="iWebElement">
        /// WebElement to click 
        /// </param>
        public static void Click(this IWebDriver driver, IWebElement iWebElement)
        {
            try
            {              
                if (BrowserPool.CurrentBrowser.BrowserInfo.Alias.Equals(BrowserUnderTest.EDGE.ToString()))
                {
                    ClickExtensions.JavascriptClick(driver, iWebElement);
                }
                else
                {
                    ClickExtensions.Click(driver, iWebElement);
                }
            }
            catch (InvalidOperationException)
            {
                driver.ScrollTo(iWebElement);
                ClickExtensions.Click(driver, iWebElement);   
            }
        }
    }
}