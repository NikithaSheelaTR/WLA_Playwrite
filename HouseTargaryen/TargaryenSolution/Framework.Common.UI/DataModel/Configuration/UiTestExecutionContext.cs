namespace Framework.Common.UI.DataModel.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.Api.Endpoints.Uds.DataModel;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.CommonTypes.Constants;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.DataModel;
    using Framework.Core.Utils;
    using Framework.Core.Utils.Execution;

    /// <summary>
    /// TestExecutionContext
    /// </summary>
    public class UiTestExecutionContext : TestExecutionContext
    {
        private RoutingSettingsInfo routingPageSettings;

        private List<Tuple<string, RoutingSettingDropdown, bool>> settingKeyToDropdownRoutingOptionMap;

        private List<Tuple<string, RoutingSettingTextbox, bool>> settingKeyToTextBoxRoutingOptionMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="UiTestExecutionContext"/> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public UiTestExecutionContext(TestSettings settings)
            : base(settings)
        {
        }

        /// <summary>
        /// Gets or sets the routing page settings.
        /// </summary>
        public RoutingSettingsInfo RoutingPageSettings
        {
            get
            {
                return this.routingPageSettings ?? (this.routingPageSettings = new RoutingSettingsInfo());
            }

            set
            {
                this.routingPageSettings = value;
            }
        }

        /// <summary>
        /// Prints the test execution context.
        /// </summary>
        public override void PrintTestContext()
        {
            base.PrintTestContext();
            Logger.LogInfo(this.RoutingPageSettings.ToString());
        }

        /// <summary>
        /// Initializes the list of actions to change the context on changes to the settings.
        /// </summary>
        protected override void GenerateSettingUpdaterMap()
        {
            base.GenerateSettingUpdaterMap();

            this.PrepareSettingToDropDownOptionMap();
            this.PrepareSettingToTextBoxOptionMap();
            this.UpdaterMap[EnvironmentConstants.NameOfEnvironmentId] += () =>
                {
                    this.InvokeTypedOptionUpdaterRoutines(
                        this.settingKeyToDropdownRoutingOptionMap,
                        this.RoutingPageSettings.RoutingDropdownSettings);
                    this.InvokeTypedOptionUpdaterRoutines(
                        this.settingKeyToTextBoxRoutingOptionMap,
                        this.RoutingPageSettings.RoutingTextboxSettings);
                };

            this.PrepareFacDelegates();
            this.PrepareSupportedFeatureDelegates();
            this.PrepareTypedOptionUpdaterDelegates(
                this.settingKeyToDropdownRoutingOptionMap,
                this.RoutingPageSettings.RoutingDropdownSettings);
            this.PrepareTypedOptionUpdaterDelegates(
                this.settingKeyToTextBoxRoutingOptionMap,
                this.RoutingPageSettings.RoutingTextboxSettings);
        }

        private void InvokeTypedOptionUpdaterRoutines<TKey, TValue>(
            IEnumerable<Tuple<string, TKey, bool>> settingToRoutingOptionMap,
            Dictionary<TKey, TValue> routingSettings) where TKey : struct
        {
            foreach (Tuple<string, TKey, bool> entry in settingToRoutingOptionMap)
            {
                this.TypedOptionUpdaterRoutine(entry.Item1, entry.Item2, entry.Item3, routingSettings);
            }
        }

        private void PrepareFacDelegates()
        {
            this.UpdaterMap.Add(
                EnvironmentConstants.FeatureAccessControlsOn,
                () =>
                    {
                        FeatureAccessControl[] facsToTurnOn =
                            this.Settings.GetValues(EnvironmentConstants.FeatureAccessControlsOn)
                                .Where(str => !string.IsNullOrEmpty(str))
                                .Select(str => str.GetEnumValueByText<FeatureAccessControl>())
                                .ToArray();

                        this.RoutingPageSettings.FeatureAccessControls.RemoveRoutingSettings(
                                (k, v) => v == FeatureSelectionOption.Grant || v == FeatureSelectionOption.Default)
                            .AppendRoutingSettings(FeatureSelectionOption.Grant, facsToTurnOn);
                    });

            this.UpdaterMap.Add(
                EnvironmentConstants.FeatureAccessControlsOff,
                () =>
                    {
                        FeatureAccessControl[] facsToTurnOff =
                            this.Settings.GetValues(EnvironmentConstants.FeatureAccessControlsOff)
                                .Where(str => !string.IsNullOrEmpty(str))
                                .Select(str => str.GetEnumValueByText<FeatureAccessControl>())
                                .ToArray();

                        this.RoutingPageSettings.FeatureAccessControls.RemoveRoutingSettings(
                                (k, v) => v == FeatureSelectionOption.Deny || v == FeatureSelectionOption.Default)
                            .AppendRoutingSettings(FeatureSelectionOption.Deny, facsToTurnOff);
                    });
        }

        private void PrepareSettingToDropDownOptionMap()
        {
            // (Option Name, Option EnumValue, Is for Lower Environment Only)
            this.settingKeyToDropdownRoutingOptionMap = new List<Tuple<string, RoutingSettingDropdown, bool>>
            {
                new Tuple<string, RoutingSettingDropdown, bool>(
                    EnvironmentConstants.IsDataRoomRegionEnabled,
                    RoutingSettingDropdown.IsDataRoomRegionEnabled,
                    false),
                new Tuple<string, RoutingSettingDropdown, bool>(
                    EnvironmentConstants.WlnDataRoomFoldersReadWriteDestination,
                    RoutingSettingDropdown.WlnDataRoomFoldersReadWriteDestination,
                    true),
                new Tuple<string, RoutingSettingDropdown, bool>(
                    EnvironmentConstants.EmulatePatronAccessUser,
                    RoutingSettingDropdown.EmulatePatronAccessUser,
                    false),
                new Tuple<string, RoutingSettingDropdown, bool>(
                    EnvironmentConstants.IphoneAppRenderingMode,
                    RoutingSettingDropdown.IPhoneAppRenderingMode,
                    false),
                new Tuple<string, RoutingSettingDropdown, bool>(
                    EnvironmentConstants.GatewayLiveExternal,
                    RoutingSettingDropdown.GatewayLiveExternal,
                    false),
                new Tuple<string, RoutingSettingDropdown, bool>(
                    EnvironmentConstants.LoggingQuantity,
                    RoutingSettingDropdown.LoggingQuantity,
                    false),
                new Tuple<string, RoutingSettingDropdown, bool>(
                    EnvironmentConstants.DataRoomReadSource,
                    RoutingSettingDropdown.DataRoomReadSource,
                    false),
                new Tuple<string, RoutingSettingDropdown, bool>(
                    EnvironmentConstants.DataRoomWriteDestination,
                    RoutingSettingDropdown.DataRoomWriteDestination,
                    true),
                new Tuple<string, RoutingSettingDropdown, bool>(
                    EnvironmentConstants.FeatureExposure,
                    RoutingSettingDropdown.FeatureExposure,
                    false),
                new Tuple<string, RoutingSettingDropdown, bool>(
                    EnvironmentConstants.DisplayDynamicMessages,
                    RoutingSettingDropdown.DisplayDynamicMessages,
                    false),
                new Tuple<string, RoutingSettingDropdown, bool>(
                    EnvironmentConstants.SkipAnonymousAuthentication,
                    RoutingSettingDropdown.SkipAnonymousAuthentication,
                    false),
                new Tuple<string, RoutingSettingDropdown, bool>(
                    EnvironmentConstants.UsageLimitOverride,
                    RoutingSettingDropdown.UsageLimitOverride,
                    false),
                new Tuple<string, RoutingSettingDropdown, bool>(
                    EnvironmentConstants.FeatureOverlay,
                    RoutingSettingDropdown.FeatureOverlay,
                    false),
                new Tuple<string, RoutingSettingDropdown, bool>(
                    EnvironmentConstants.OverrideOnlineCharges,
                    RoutingSettingDropdown.OverrideOnlineCharges,
                    false),
                new Tuple<string, RoutingSettingDropdown, bool>(
                    EnvironmentConstants.RDTypeaheadRoutingEnabled,
                    RoutingSettingDropdown.RDTypeaheadRoutingEnabled,
                    false),
                new Tuple<string, RoutingSettingDropdown, bool>(
                    EnvironmentConstants.RDSearchRoutingEnabled,
                    RoutingSettingDropdown.RDSearchRoutingEnabled,
                    false),
                new Tuple<string, RoutingSettingDropdown, bool>(
                    EnvironmentConstants.SearchDebugInfo,
                    RoutingSettingDropdown.SearchDebugInfo,
                    false),
                new Tuple<string, RoutingSettingDropdown, bool>(
                    EnvironmentConstants.UseSSOAuth,
                    RoutingSettingDropdown.UseSSOAuth,
                    false),
                new Tuple<string, RoutingSettingDropdown, bool>(
                    EnvironmentConstants.BypassExternalLLMs,
                    RoutingSettingDropdown.BypassExternalLLMs,
                    false),
                 new Tuple<string, RoutingSettingDropdown, bool>(
                    EnvironmentConstants.EnableCiam,
                    RoutingSettingDropdown.EnableCiam,
                    false),
                 new Tuple<string, RoutingSettingDropdown, bool>(
                    EnvironmentConstants.EnableGovCiam,
                    RoutingSettingDropdown.EnableGovCiam,
                    false),
                  new Tuple<string, RoutingSettingDropdown, bool>(
                    EnvironmentConstants.BlockCiam,
                    RoutingSettingDropdown.BlockCiam,
                    false)
            };
        }

        private void PrepareSettingToTextBoxOptionMap()
        {
            // (Option Name, Option EnumValue, Is for Lower Environment Only)
            this.settingKeyToTextBoxRoutingOptionMap = new List<Tuple<string, RoutingSettingTextbox, bool>>
            {
                new Tuple<string, RoutingSettingTextbox, bool>(
                    EnvironmentConstants.CategoryPageCollectionSet,
                    RoutingSettingTextbox.CategoryPageCollectionSet,
                    false),
                new Tuple<string, RoutingSettingTextbox, bool>(
                    EnvironmentConstants.PmdVersion,
                    RoutingSettingTextbox.PmdDataVersion,
                    false),
                new Tuple<string, RoutingSettingTextbox, bool>(
                    EnvironmentConstants.InfrastructureAccessControlsOn,
                    RoutingSettingTextbox.InfrastructureAccessControlsOn,
                    false),
                new Tuple<string, RoutingSettingTextbox, bool>(
                    EnvironmentConstants.InfrastructureAccessControlsOff,
                    RoutingSettingTextbox.InfrastructureAccessControlsOff,
                    false),
                new Tuple<string, RoutingSettingTextbox, bool>(
                    EnvironmentConstants.DataRoom,
                    RoutingSettingTextbox.DataRoom,
                    true),
                new Tuple<string, RoutingSettingTextbox, bool>(
                    EnvironmentConstants.DataRoomBulk,
                    RoutingSettingTextbox.DataRoomBulk,
                    true),
                new Tuple<string, RoutingSettingTextbox, bool>(
                    EnvironmentConstants.Document,
                    RoutingSettingTextbox.Document,
                    true),
                new Tuple<string, RoutingSettingTextbox, bool>(
                    EnvironmentConstants.Km,
                    RoutingSettingTextbox.Km,
                    false),
                new Tuple<string, RoutingSettingTextbox, bool>(
                    EnvironmentConstants.OnlineCharges,
                    RoutingSettingTextbox.OnlineCharges,
                    true),
                new Tuple<string, RoutingSettingTextbox, bool>(
                    EnvironmentConstants.SessionTimeoutOverride,
                    RoutingSettingTextbox.SessionTimeoutOverride,
                    false),
                new Tuple<string, RoutingSettingTextbox, bool>(
                    EnvironmentConstants.DataOrchestration,
                    RoutingSettingTextbox.DataOrchestration,
                    true),
                new Tuple<string, RoutingSettingTextbox, bool>(
                    EnvironmentConstants.SessionDeliveryLimit,
                    RoutingSettingTextbox.SessionDeliveryLimit,
                    false),
                new Tuple<string, RoutingSettingTextbox, bool>(
                    EnvironmentConstants.DailyDeliveryLimit,
                    RoutingSettingTextbox.DailyDeliveryLimit,
                    false),
                new Tuple<string, RoutingSettingTextbox, bool>(
                    EnvironmentConstants.MaximumCasesDocumentCountLimit,
                    RoutingSettingTextbox.MaximumCasesDocumentCountLimit,
                    false),
                new Tuple<string, RoutingSettingTextbox, bool>(
                    EnvironmentConstants.PamSubscriptionDeniedOverride,
                    RoutingSettingTextbox.PamSubscriptionDeniedOverride,
                    false),
                new Tuple<string, RoutingSettingTextbox, bool>(
                    EnvironmentConstants.OverridePamCaseTypeToReleased,
                    RoutingSettingTextbox.OverridePamCaseTypeToReleased,
                    false),
                new Tuple<string, RoutingSettingTextbox, bool>(
                    EnvironmentConstants.AIClaimsFinderLimit,
                    RoutingSettingTextbox.AIClaimsFinderLimit,
                    false),
                new Tuple<string, RoutingSettingTextbox, bool>(
                    EnvironmentConstants.AIGuidedResearchDailyLimit,
                    RoutingSettingTextbox.AIGuidedResearchDailyLimit,
                    false),
                new Tuple<string, RoutingSettingTextbox, bool>(
                    EnvironmentConstants.AISearchDailyLimit,
                    RoutingSettingTextbox.AISearchDailyLimit,
                    false),
                new Tuple<string, RoutingSettingTextbox, bool>(
                    EnvironmentConstants.RasConversationHost,
                    RoutingSettingTextbox.RasConversationHost,
                    true),
                new Tuple<string, RoutingSettingTextbox, bool>(
                    EnvironmentConstants.UserIpAddressOverride,
                    RoutingSettingTextbox.UserIpAddressOverride, 
                    false),
                new Tuple<string, RoutingSettingTextbox, bool>(
                    EnvironmentConstants.AITreatiseSearchDailyLimit,
                    RoutingSettingTextbox.AITreatiseSearchDailyLimit,
                    false),
                new Tuple<string, RoutingSettingTextbox, bool>(
                    EnvironmentConstants.AIJurisdictionalSurveysDailyLimit,
                    RoutingSettingTextbox.AIJurisdictionalSurveysDailyLimit,
                    false),
                new Tuple<string, RoutingSettingTextbox, bool>(
                    EnvironmentConstants.DocViewLimitCatchAllDaily,
                    RoutingSettingTextbox.DocViewLimitCatchAllDaily,
                    false),
                new Tuple<string, RoutingSettingTextbox, bool>(
                    EnvironmentConstants.DocViewLimitPracticalLawDaily,
                    RoutingSettingTextbox.DocViewLimitPracticalLawDaily,
                    false),
                new Tuple<string, RoutingSettingTextbox, bool>(
                    EnvironmentConstants.AIChatAssistantServiceConfig,
                    RoutingSettingTextbox.AIChatAssistantServiceConfig,
                    false)
            };
        }

        private void PrepareSupportedFeatureDelegates()
        {
            this.UpdaterMap.Add(
                EnvironmentConstants.SupportedFeaturesOn,
                () =>
                    {
                        SupportedFeatures[] supportedFeaturesToTurnOn =
                            this.Settings.GetValues(EnvironmentConstants.SupportedFeaturesOn)
                                .Where(str => !string.IsNullOrEmpty(str))
                                .Select(str => str.GetEnumValueByText<SupportedFeatures>())
                                .ToArray();

                        this.RoutingPageSettings.SupportedFeatureSettings.RemoveRoutingSettings((k, v) => v)
                            .AppendRoutingSettings(true, supportedFeaturesToTurnOn);
                    });
            this.UpdaterMap.Add(
                EnvironmentConstants.SupportedFeaturesOff,
                () =>
                    {
                        SupportedFeatures[] supportedFeaturesToTurnOff =
                            this.Settings.GetValues(EnvironmentConstants.SupportedFeaturesOff)
                                .Where(str => !string.IsNullOrEmpty(str))
                                .Select(str => str.GetEnumValueByText<SupportedFeatures>())
                                .ToArray();

                        this.RoutingPageSettings.SupportedFeatureSettings.RemoveRoutingSettings((k, v) => !v)
                            .AppendRoutingSettings(false, supportedFeaturesToTurnOff);
                    });
        }

        private void PrepareTypedOptionUpdaterDelegates<TKey, TValue>(
            IEnumerable<Tuple<string, TKey, bool>> settingToRoutingOptionMap,
            Dictionary<TKey, TValue> routingSettings) where TKey : struct
        {
            foreach (Tuple<string, TKey, bool> entry in settingToRoutingOptionMap)
            {
                this.UpdaterMap.Add(
                    entry.Item1,
                    () => this.TypedOptionUpdaterRoutine(entry.Item1, entry.Item2, entry.Item3, routingSettings));
            }
        }

        private void TypedOptionUpdaterRoutine<TKey, TValue>(
            string optionName,
            TKey optionEnumValue,
            bool isForLowerEnvironmentOnly,
            Dictionary<TKey, TValue> routingSettings) where TKey : struct
        {
            string valueToSet = this.Settings.GetValue(optionName);

            if (valueToSet != null && (!isForLowerEnvironmentOnly || this.TestEnvironment.IsLower))
            {
                TValue value;
                Func<string, TValue> converter = v =>
                    {
                        Type type = typeof(TValue);

                        return type.IsEnum ? (TValue)Enum.Parse(type, v, true) : (TValue)Convert.ChangeType(v, type);
                    };

                if (SafeMethodExecutor.Execute(() => converter(valueToSet), out value).ResultType
                    == ExecutionResultType.Success)
                {
                    routingSettings.Append(optionEnumValue, value, true);
                }
            }
            else if (routingSettings != null && routingSettings.ContainsKey(optionEnumValue))
            {
                routingSettings.Remove(optionEnumValue);
            }
        }
    }
}