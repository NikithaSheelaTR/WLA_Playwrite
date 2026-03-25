namespace Framework.Common.UI.Products.WestLawNext.Components.Toolbar
{
    using System;

    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// GraphicComponent Component inside Toolbar
    /// </summary>
    public class GraphicComponent : BaseModuleRegressionComponent
    {
        private static readonly By ResetButtonLocator = By.Id("coid_graphic_reset_link");

        private static readonly By ShowHideButtonLocator = By.Id("coid_graphic_toggle_link");

        private static readonly By ZoomInButtonLocator = By.Id("coid_graphic_zoomIn_link");

        private static readonly By ZoomOutButtonLocator = By.Id("coid_graphic_zoomOut_link");

        private static readonly By ContainerLocator = By.Id("co_docPrimaryTabNavigationContainer");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Clicks Reset Button
        /// </summary>
        public void ClickResetButton() => DriverExtensions.WaitForElement(ResetButtonLocator).Click();

        /// <summary>
        /// Clicks ZoomInButton button
        /// </summary>
        public void ClickZoomInButton() => DriverExtensions.WaitForElement(ZoomInButtonLocator).Click();

        /// <summary>
        /// Gets ShowHideButton Text
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetShowHideButtonText() => DriverExtensions.WaitForElement(ShowHideButtonLocator).Text;

        /// <summary>
        /// Hide history graphic if it is shown
        /// </summary>
        public void HideHistoryGraphic()
        {
            if (this.GetShowHideButtonText().Equals("Hide"))
            {
                DriverExtensions.GetElement(ShowHideButtonLocator).Click();
                DriverExtensions.WaitForJavaScript();
            }
        }

        /// <summary>
        /// Determine if ResetButton is disabled and greyed out.
        /// </summary>
        /// <returns>True if greyed out.</returns>
        public bool IsResetButtonDisabled()
            => Convert.ToBoolean(DriverExtensions.GetElement(ResetButtonLocator).GetAttribute("aria-disabled"));

        /// <summary>
        /// Verifies if ResetButton displayed
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsResetButtonDisplayed() => DriverExtensions.WaitForElement(ResetButtonLocator).Displayed;

        /// <summary>
        /// Verifies if ShowHideButton displayed
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsShowHideButtonDisplayed() => DriverExtensions.WaitForElement(ShowHideButtonLocator).Displayed;

        /// <summary>
        /// Determine if ZoomInButton is disabled and greyed out.
        /// </summary>
        /// <returns>True if greyed out.</returns>
        public bool IsZoomInButtonDisabled()
            => Convert.ToBoolean(DriverExtensions.GetElement(ZoomInButtonLocator).GetAttribute("disabled"));

        /// <summary>
        /// Determine if ZoomInButton is displayed
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsZoomInButtonDisplayed() => DriverExtensions.WaitForElement(ZoomInButtonLocator).Displayed;

        /// <summary>
        /// Determine if ZoomOutButton is disabled and greyed out.
        /// </summary>
        /// <returns>True if greyed out.</returns>
        public bool IsZoomOutButtonDisabled()
            => Convert.ToBoolean(DriverExtensions.GetElement(ZoomOutButtonLocator).GetAttribute("disabled"));

        /// <summary>
        /// Determine if ZoomOutButton is displayed
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsZoomOutButtonDisplayed() => DriverExtensions.WaitForElement(ZoomOutButtonLocator).Displayed;

        /// <summary>
        /// Show history graphic if it is hidden
        /// </summary>
        public void ShowHistoryGraphic()
        {
            if (this.GetShowHideButtonText().Equals("Show"))
            {
                DriverExtensions.GetElement(ShowHideButtonLocator).Click();
                DriverExtensions.WaitForJavaScript();
            }
        }
    }
}