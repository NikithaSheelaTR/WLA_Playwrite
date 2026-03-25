namespace Framework.Common.UI.Utils.QualityLibraryFacade.Custom
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Execution;

    using OpenQA.Selenium;

    using TRGR.Quality.QedArsenal.QualityLibrary.WebDriver.Extensions;

    /// <summary>
    /// Custom Action extensions
    /// </summary>
    public static partial class CustomExtensions
    {
        /// <summary>
        /// Verifies if element is enabled
        /// </summary>
        /// <param name="driver">driver</param>
        /// <param name="elementBy">elementBy</param>
        /// <returns>true or false</returns>
        public static bool IsEnabled(this IWebDriver driver, By elementBy)
        {
            bool returnValue = false;
            SafeMethodExecutor.Execute(() => returnValue = driver.GetElement(elementBy).Enabled);
            return returnValue;
        }

        /// <summary>
        /// The is scrollable element in view.
        /// </summary>
        /// <param name="driver">
        /// The driver.
        /// </param>
        /// <param name="elementWithinScrollBy">
        /// The element within scroll by.
        /// </param>
        /// <param name="divWithScrollBy">
        /// The div with scroll by.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsScrollableElementInView(this IWebDriver driver, By elementWithinScrollBy, By divWithScrollBy)
        {
            IWebElement elementWithinScroll = driver.GetElement(elementWithinScrollBy);
            double elementWithinScrollTopPosition = elementWithinScroll.GetElementTopPosition();
            double divWithScrollTopPosition = driver.GetElement(divWithScrollBy).GetElementTopPosition();
            return elementWithinScroll.IsElementInView() && (elementWithinScrollTopPosition >= divWithScrollTopPosition);
        }
    }
}