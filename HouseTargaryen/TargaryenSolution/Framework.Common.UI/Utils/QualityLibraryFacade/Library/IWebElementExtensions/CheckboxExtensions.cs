namespace Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions
{
    using Framework.Common.UI.Utils.Browser;
    using Framework.Core.DataModel.Configuration.Constants;

    using OpenQA.Selenium;

    using TRGR.Quality.QedArsenal.QualityLibrary.WebDriver.Extensions;

    /// <summary>
    /// The IWebElement Extensions
    /// </summary>
    public static partial class ElementExtensions
    {
        /// <summary>
        /// Selects or deselects the specified checkbox based on the specified checked value
        /// </summary>
        /// <param name="checkbox">The desired checkbox IWebElement</param>
        /// <param name="selected">The desired value of the checkbox (True for selected and false for deselected)</param>
        public static void SetCheckbox(this IWebElement checkbox, bool selected)
        {
            if (BrowserPool.CurrentBrowser.BrowserInfo.Family == TestClientFamily.MicrosoftEdge)
            {
                //Temporary solution before implementing new approach
                checkbox.SetCheckboxUsingClick(selected);
            }
            else
            {
                BrowserPool.CurrentBrowser.InvokeAction(wd => wd.SetCheckbox(selected, checkbox));
            }
        }

        /// <summary>
        /// Selects or deselects the specified checkbox using elements click. It`s needed for IE tests
        /// </summary>
        /// <param name="checkbox">The desired checkbox IWebElement</param>
        /// <param name="selected">The desired value of the checkbox (True for selected and false for deselected)</param>
        public static void SetCheckboxUsingClick(this IWebElement checkbox, bool selected)
        {
            if (selected == !checkbox.Selected)
            {
                checkbox.CustomClick();
            }
        }
    }
}
