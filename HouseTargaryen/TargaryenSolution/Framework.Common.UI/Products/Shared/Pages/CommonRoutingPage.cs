namespace Framework.Common.UI.Products.Shared.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Framework.Common.Api.Endpoints.Uds.DataModel;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.Utils;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// CommonRouting page
    /// </summary>
    public class CommonRoutingPage : BaseModuleRegressionPage, ICommonRoutingPage, IEnvironmentCheckPage
    {
        private static readonly By SupportedFeaturesPaneLocator = By.CssSelector("div.co_website_SupportedFeatures");

        private static readonly By ToggleSupportedFeaturesButtonLocator = By.CssSelector("button.co_website_SupportedFeatures_button");

        private static readonly By SaveButtonLocator = By.Id("coid_website_routingSaveButton");

        private static readonly By ShowFeatureSelectionButtonLocator = By.Id("co_website_resourceInfoTypeLink");

        private static readonly By FeatureSlectionContainerLocator = By.ClassName("co_website_resourceInfoTypeConfiguration");

        private static readonly By ShowDocumentViewLimitslinkLocator = By.Id("co_website_documentViewLimitsLink");

        private static readonly By DocumentViewLimitsContainerLocator = By.ClassName("co_website_documentViewLimitConfiguration");

        private static readonly By CloseOrAcceptAllCookiesButton = By.XPath("//div[@id='onetrust-close-btn-container']/button | //button[@aria-label='Close'] | //button[text()='Accept All Cookies']");

        private static readonly EnumPropertyMapper<FeatureAccessControl, WebElementInfo> FacMap =
            EnumPropertyModelCache.GetMap<FeatureAccessControl, WebElementInfo>();

        private static readonly EnumPropertyMapper<RoutingSettingDropdown, WebElementInfo> RoutingSettingsDropdownMap =
            EnumPropertyModelCache.GetMap<RoutingSettingDropdown, WebElementInfo>();

        private static readonly EnumPropertyMapper<RoutingSettingTextbox, WebElementInfo> RoutingSettingsTextboxMap =
            EnumPropertyModelCache.GetMap<RoutingSettingTextbox, WebElementInfo>();

        private static readonly EnumPropertyMapper<SupportedFeatures, WebElementInfo> SupportedFeaturesMap =
            EnumPropertyModelCache.GetMap<SupportedFeatures, WebElementInfo>();

        /// <summary>
        /// Initializes a new instance of the <see cref="CommonRoutingPage"/> class. 
        /// </summary>
        /// <param name="baseUrl">base URL for environment under test</param>
        /// <param name="routingUrlValues">The URL values to append</param>
        public CommonRoutingPage(string baseUrl, Dictionary<string, string> routingUrlValues)
        {
            string url = baseUrl + "routing";
            var queryStringParmetersString = new StringBuilder();

            if (routingUrlValues.Any())
            {
                queryStringParmetersString.Append("?");

                foreach (KeyValuePair<string, string> pair in routingUrlValues)
                {
                    queryStringParmetersString.Append(pair.Key + "=" + pair.Value).Append("&");
                }

                url += queryStringParmetersString.Remove(queryStringParmetersString.Length - 1, 1);
            }

            BrowserPool.CurrentBrowser.GoToUrl(url);
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForJavaScript();

            if (DriverExtensions.IsDisplayed(CloseOrAcceptAllCookiesButton, 30))
            {
                DriverExtensions.Click(CloseOrAcceptAllCookiesButton);
            }
        }

        /// <summary>
        ///  Implements a routing option setter for the <see cref="RoutingSettingDropdown"/> type of routing control.
        /// </summary>
        /// <param name="routingSetting">A routing page option to set.</param>
        /// <param name="routingSettingOption">The value of the routing option to set.</param>
        public static void SetRoutingOption(
            RoutingSettingDropdown routingSetting,
            RoutingSettingDropdownOption routingSettingOption)
        {
            By routingSettingDropDownLocator = By.Id(RoutingSettingsDropdownMap[routingSetting].Id);

            DriverExtensions.SetDropdown(routingSettingOption.ToString(), routingSettingDropDownLocator);
            DriverExtensions.WaitForJavaScript();

            Console.WriteLine($"Set Routing Option: {RoutingSettingsDropdownMap[routingSetting].Text} - {DriverExtensions.GetSelectedDropdownOptionText(routingSettingDropDownLocator)}");
        }

        /// <summary>
        ///  Implements a routing option setter for the <see cref="RoutingSettingTextbox"/> type of routing control.
        /// </summary>
        /// <param name="routingSetting">A routing page option to set.</param>
        /// <param name="routingSettingOption">The value of the routing option to set.</param>
        public static void SetRoutingOption(RoutingSettingTextbox routingSetting, string routingSettingOption)
        {
            if (!DriverExtensions.IsDisplayed(DocumentViewLimitsContainerLocator))
            {
                DriverExtensions.Click(ShowDocumentViewLimitslinkLocator);
            }

            // Using the By.Name mechanism as a workaround for KM text box on PROD.
            By routingSettingTextBoxLocator = By.Name(RoutingSettingsTextboxMap[routingSetting].Id);

            DriverExtensions.SetTextField(routingSettingOption, routingSettingTextBoxLocator);
            DriverExtensions.WaitForJavaScript();

            Console.WriteLine($"Set Routing Option: {RoutingSettingsTextboxMap[routingSetting].Text} - {DriverExtensions.GetText(routingSettingTextBoxLocator)}");
        }

        /// <summary>
        ///  Implements a routing option setter for the <see cref="FeatureAccessControl"/> type of routing control.
        /// </summary>
        /// <param name="fac">A feature access control to set</param>
        /// <param name="value">The value to set the feature access control to.</param>
        public static void SetRoutingOption(FeatureAccessControl fac, FeatureSelectionOption value)
        {
            if (!DriverExtensions.IsDisplayed(FeatureSlectionContainerLocator))
            {
                DriverExtensions.Click(ShowFeatureSelectionButtonLocator);
            }

            DriverExtensions.SetDropdown(value.ToString(), By.Id(FacMap[fac].Id));
            Logger.LogInfo("Set FAC: " + fac + " - " + value);
        }

        /// <summary>
        ///  Implements a routing option setter for the <see cref="SupportedFeatures"/> type of routing control.
        /// </summary>
        /// <param name="supportedFeature">A supported feature to toggle.</param>
        /// <param name="value">The value to set the feature selection to.</param>
        public static void SetRoutingOption(SupportedFeatures supportedFeature, bool value)
        {
            if (!DriverExtensions.IsDisplayed(SupportedFeaturesPaneLocator))
            {
                DriverExtensions.Click(ToggleSupportedFeaturesButtonLocator);
            }

            DriverExtensions.WaitForElement(By.Id(SupportedFeaturesMap[supportedFeature].Id)).SetCheckbox(value);
            Logger.LogInfo("Set Supported Feature: " + supportedFeature + " - " + value);
        }

        /// <summary>
        /// Sets routing options using the specified routing options setter. 
        /// </summary>
        /// <typeparam name="TKey">Type of a routing option name.</typeparam>
        /// <typeparam name="TValue">Type of a routing option value.</typeparam>
        /// <param name="routingSettings">A dictionary mapping routing page options to the values they should be set to.</param>
        /// <param name="optionSetter">The option Setter routine.</param>
        public static void SetRoutingOptions<TKey, TValue>(
            Dictionary<TKey, TValue> routingSettings,
            Action<TKey, TValue> optionSetter)
        {
            DriverExtensions.WaitForElementDisplayed(SaveButtonLocator);
            if (routingSettings != null && routingSettings.Any() && optionSetter != null)
            {
                foreach (KeyValuePair<TKey, TValue> setting in routingSettings)
                {
                    optionSetter(setting.Key, setting.Value);
                }
            }
        }

        /// <summary>
        ///  Clicks the save button and returns user to sign on page (Temporary save method)
        /// </summary>
        /// <typeparam name="T">class for object of type ICommonSignOnPage</typeparam>
        /// <returns>object of type T</returns>
        public T ClickSaveButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.Click(DriverExtensions.WaitForElementDisplayed(SaveButtonLocator));
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Checks if there is any error on the page
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsEnvironmentErrorsOnPage() => this.IsEnvironmentErrorMessageDisplayed();
    }
}