namespace Framework.Core.Utils.Configuration
{
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.DataModel.Configuration.Constants;

    /// <summary>
    /// EnvironmentUtils
    /// </summary>
    public static class EnvironmentUtils
    {
        /// <summary>
        /// Retrieves Cobalt Services URL.
        /// </summary>
        /// <param name="environmentUnderTest">The environment under test.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string GetCobaltServicesUrl(this EnvironmentId environmentUnderTest)
        {
            return
                TestConfigurationRepository.DefaultInstance.FindEndpoint(
                    CobaltModuleId.CobaltServices,
                    CobaltProductId.None,
                    environmentUnderTest).Uri;
        }

        /// <summary>
        /// Retrieves the base URL for the Data Orchestration Gateway service in the specified environment.
        /// </summary>
        /// <param name="environmentUnderTest">Environment under test.</param>
        /// <returns>Base URL.</returns>
        public static string GetDoGatewayUrlForEnv(this EnvironmentId environmentUnderTest)
        {
            return
                TestConfigurationRepository.DefaultInstance.FindEndpoint(
                    CobaltModuleId.DOGateway,
                    CobaltProductId.None,
                    environmentUnderTest).Uri;
        }

        /// <summary>
        /// Retrieves the base URL for the Foldering service in the specified environment.
        /// </summary>
        /// <param name="environmentUnderTest">Environment under test.</param>
        /// <returns>Base URL.</returns>
        public static string GetFolderingUrlForEnv(this EnvironmentId environmentUnderTest)
        {
            return
                TestConfigurationRepository.DefaultInstance.FindEndpoint(
                    CobaltModuleId.Foldering,
                    CobaltProductId.WestlawNext,
                    environmentUnderTest).Uri;
        }

        /// <summary>
        /// Retrieves the base URL for the Security service in the specified environment.
        /// </summary>
        /// <param name="environmentUnderTest">Environment under test.</param>
        /// <returns>Base URL.</returns>
        public static string GetSecurityUrlForEnv(this EnvironmentId environmentUnderTest)
        {
            return
                TestConfigurationRepository.DefaultInstance.FindEndpoint(
                    CobaltModuleId.Security,
                    CobaltProductId.None,
                    environmentUnderTest).Uri;
        }

        /// <summary>
        /// Runs a switch statement on the environment to determine the session info tool URL for the current session
        /// </summary>
        /// <returns>URL to the session info tool for the session</returns>
        public static string GetSessionInfoUrl(this EnvironmentId environmentUnderTest, string sessionId)
        {
            if (string.IsNullOrEmpty(sessionId))
            {
                return "Unable to generate session info URL - session ID is null or empty";
            }

            return
                string.Format(
                    TestConfigurationRepository.DefaultInstance.FindEndpoint(
                        CobaltModuleId.None,
                        CobaltProductId.CobaltSessionInfo,
                        environmentUnderTest).Uri,
                    sessionId);
        }

        /// <summary>
        /// Retrieves the base URL for the UDS service in the specified environment.
        /// </summary>
        /// <param name="environmentUnderTest">Environment under test.</param>
        /// <returns>Base URL.</returns>
        public static string GetUdsUrlForEnv(this EnvironmentId environmentUnderTest)
        {
            return
                TestConfigurationRepository.DefaultInstance.FindEndpoint(
                    CobaltModuleId.Uds,
                    CobaltProductId.WestlawNext,
                    environmentUnderTest).Uri;
        }

        /// <summary>
        /// Runs a switch statement on the environment to determine the URL for the ANZ
        /// </summary>
        /// <param name="environmentUnderTest">The environment Under Test.</param>
        /// <returns>URL to that environment</returns>
        public static string GetUrlForAnz(this EnvironmentId environmentUnderTest)
        {
            return
                TestConfigurationRepository.DefaultInstance.FindEndpoint(
                    CobaltModuleId.Website,
                    CobaltProductId.Anz,
                    environmentUnderTest).Uri;
        }

        /// <summary>
        /// Runs a switch statement on the environment to determine the URL for Case Notebook
        /// </summary>
        /// <param name="environmentUnderTest">The environment Under Test.</param>
        /// <returns>URL to that environment</returns>
        public static string GetUrlForCaseNotebook(this EnvironmentId environmentUnderTest)
        {
            return
                TestConfigurationRepository.DefaultInstance.FindEndpoint(
                    CobaltModuleId.Website,
                    CobaltProductId.CaseNotebook,
                    environmentUnderTest).Uri;
        }

        /// <summary>
        /// Runs a switch statement on the environment to determine the URL for WLN &lt;=&gt; Case Notebook integration.
        /// </summary>
        /// <param name="environmentUnderTest">The environment Under Test.</param>
        /// <returns>URL to that environment</returns>
        public static string GetUrlForCaseNotebookSearch(this EnvironmentId environmentUnderTest)
        {
            return environmentUnderTest.GetUrlForWestlawNext() + "V1/CaseNotebook/Search";
        }

        /// <summary>
        /// Runs a switch statement on the environment to determine the URL for Concourse.
        /// </summary>
        /// <param name="environmentUnderTest">The environment Under Test.</param>
        /// <returns>URL to that environment.</returns>
        public static string GetUrlForConcourse(this EnvironmentId environmentUnderTest)
        {
            return
                TestConfigurationRepository.DefaultInstance.FindEndpoint(
                    CobaltModuleId.Website,
                    CobaltProductId.Concourse,
                    environmentUnderTest).Uri;
        }

        /// <summary>
        /// Runs a switch statement on the environment to determine the URL for drafting assistant
        /// </summary>
        /// <param name="environmentUnderTest">The environment Under Test.</param>
        /// <returns>URL to that environment</returns>
        public static string GetUrlForDraftingAssistant(this EnvironmentId environmentUnderTest)
        {
            return
                TestConfigurationRepository.DefaultInstance.FindEndpoint(
                    CobaltModuleId.Website,
                    CobaltProductId.DraftingAssistant,
                    environmentUnderTest).Uri + "DraftingAssistantTest";
        }

        /// <summary>
        /// Runs a switch statement on the environment to determine the URL for Firm Central.
        /// </summary>
        /// <param name="environmentUnderTest">The environment Under Test.</param>
        /// <returns>URL to that environment.</returns>
        public static string GetUrlForFirmCentral(this EnvironmentId environmentUnderTest)
        {
            return
                TestConfigurationRepository.DefaultInstance.FindEndpoint(
                    CobaltModuleId.Website,
                    CobaltProductId.FirmCentral,
                    environmentUnderTest).Uri;
        }

        /// <summary>
        /// Runs a switch statement on the environment to determine the URL for the Government Sites site
        /// </summary>
        /// <param name="environmentUnderTest">The environment Under Test.</param>
        /// <param name="site">The government site.</param>
        /// <returns>URL to that environment.</returns>
        public static string GetUrlForGovernmentSites(this EnvironmentId environmentUnderTest, string site)
        {
            return
                TestConfigurationRepository.DefaultInstance.FindEndpoint(
                    CobaltModuleId.Website,
                    CobaltProductId.GovtSites,
                    environmentUnderTest).Uri + site;
        }

        /// <summary>
        /// Runs a switch statement on the environment to determine the URL for LawSchool.
        /// </summary>
        /// <param name="environmentUnderTest">The environment Under Test.</param>
        /// <returns>URL to that environment.</returns>
        public static string GetUrlForLawSchool(this EnvironmentId environmentUnderTest)
        {
            return
                TestConfigurationRepository.DefaultInstance.FindEndpoint(
                    CobaltModuleId.Website,
                    CobaltProductId.LawSchool,
                    environmentUnderTest).Uri;
        }

        /// <summary>
        /// Runs a switch statement on the environment to determine the URL for West KM service.
        /// </summary>
        /// <param name="environmentUnderTest">The environment Under Test.</param>
        /// <returns>URL to that environment.</returns>
        public static string GetUrlForWestKmService(this EnvironmentId environmentUnderTest)
        {
            return
                TestConfigurationRepository.DefaultInstance.FindEndpoint(
                    CobaltModuleId.Website,
                    CobaltProductId.WestKm,
                    environmentUnderTest).Uri;
        }

        /// <summary>
        /// Returns the URL for Westlaw Analytics for the given environment.
        /// </summary>
        /// <param name="environmentUnderTest">Environment under test.</param>
        /// <returns>The URL.</returns>
        public static string GetUrlForWestlawAnalytics(this EnvironmentId environmentUnderTest)
        {
            return
                TestConfigurationRepository.DefaultInstance.FindEndpoint(
                    CobaltModuleId.Website,
                    CobaltProductId.WlAnalytics,
                    environmentUnderTest).Uri;
        }

        /// <summary>
        /// Runs a switch statement on the environment to determine the URL for the full WLN site
        /// </summary>
        /// <param name="environmentUnderTest">The environment Under Test.</param>
        /// <returns>URL to that environment</returns>
        public static string GetUrlForWestlawNext(this EnvironmentId environmentUnderTest)
        {
            return
                TestConfigurationRepository.DefaultInstance.FindEndpoint(
                    CobaltModuleId.Website,
                    CobaltProductId.WestlawNext,
                    environmentUnderTest).Uri;
        }

        /// <summary>
        /// Returns the URL for Westlaw Edge Premium for the given environment.
        /// </summary>
        /// <param name="environmentUnderTest">The environment Under Test.</param>
        /// <returns>URL to that environment</returns>
        public static string GetUrlForWestlawEdgePremium(this EnvironmentId environmentUnderTest)
        {
            return
                TestConfigurationRepository.DefaultInstance.FindEndpoint(
                    CobaltModuleId.Website,
                    CobaltProductId.WestlawEdgePremium,
                    environmentUnderTest).Uri;
        }

        /// <summary>
        /// Runs a switch statement on the environment to determine the URL for the full WLN Canada site
        /// </summary>
        /// <param name="environmentUnderTest">The environment Under Test.</param>
        /// <returns>URL to that environment</returns>
        public static string GetUrlForWestlawNextCanada(this EnvironmentId environmentUnderTest)
        {
            return
                TestConfigurationRepository.DefaultInstance.FindEndpoint(
                    CobaltModuleId.Website,
                    CobaltProductId.WlnCanada,
                    environmentUnderTest).Uri;
        }

        /// <summary>
        /// Runs a switch statement on the environment to determine the URL for the full WLN Mobile
        /// </summary>
        /// <param name="environmentUnderTest">The environment Under Test.</param>
        /// <returns>URL to that environment</returns>
        public static string GetUrlForWestlawNextMobile(this EnvironmentId environmentUnderTest)
        {
            return
                TestConfigurationRepository.DefaultInstance.FindEndpoint(
                    CobaltModuleId.Website,
                    CobaltProductId.WlnMobile,
                    environmentUnderTest).Uri;
        }

        /// <summary>
        /// Runs a switch statement on the environment to determine the URL for the full WLN Tax site
        /// </summary>
        /// <param name="environmentUnderTest">The environment Under Test.</param>
        /// <returns>URL to that environment</returns>
        public static string GetUrlForWestlawNextTax(this EnvironmentId environmentUnderTest)
        {
            return
                TestConfigurationRepository.DefaultInstance.FindEndpoint(
                    CobaltModuleId.Website,
                    CobaltProductId.WlnTax,
                    environmentUnderTest).Uri;
        }

        /// <summary>
        /// Runs a switch statement on the environment to determine the URL for the full WLN Global site
        /// </summary>
        /// <param name="environmentUnderTest">The environment Under Test.</param>
        /// <returns>URL to that environment</returns>
        public static string GetUrlForWlnGlobal(this EnvironmentId environmentUnderTest)
        {
            return
                TestConfigurationRepository.DefaultInstance.FindEndpoint(
                    CobaltModuleId.Website,
                    CobaltProductId.WlnGlobal,
                    environmentUnderTest).Uri;
        }

        /// <summary>
        /// Runs a switch statement on the environment to determine the URL for the full Checkpoint Global site
        /// </summary>
        /// <param name="environmentUnderTest">The environment Under Test.</param>
        /// <returns>URL to that environment</returns>
        public static string GetUrlForCheckpointGlobal(this EnvironmentId environmentUnderTest)
        {
            return
                TestConfigurationRepository.DefaultInstance.FindEndpoint(
                    CobaltModuleId.Website,
                    CobaltProductId.CheckpointGlobal,
                    environmentUnderTest).Uri;
        }

        /// <summary>
        /// Runs a switch statement on the environment to determine the URL for the full WLN OpenWeb site
        /// </summary>
        /// <param name="environmentUnderTest">The environment Under Test.</param>
        /// <returns>URL to that environment</returns>
        public static string GetUrlForWlnOpenWeb(this EnvironmentId environmentUnderTest)
        {
            return
                TestConfigurationRepository.DefaultInstance.FindEndpoint(
                    CobaltModuleId.Website,
                    CobaltProductId.WlnOpenWeb,
                    environmentUnderTest).Uri;
        }

        /// <summary>
        /// Runs a switch statement on the environment to determine the URL for the full WLN OpenWeb site
        /// </summary>
        /// <param name="environmentUnderTest">The environment Under Test.</param>
        /// <returns>URL to that environment</returns>
        public static string GetUrlForProView(this EnvironmentId environmentUnderTest)
            =>
                TestConfigurationRepository.DefaultInstance.FindEndpoint(
                    CobaltModuleId.Website,
                    CobaltProductId.ProView,
                    environmentUnderTest).Uri;

        /// <summary>
        /// Runs a switch statement on the environment to determine the URL for the  WLN Correctional site
        /// </summary>
        /// <param name="environmentUnderTest">The environment Under Test.</param>
        /// <returns>URL to that environment</returns>
        public static string GetUrlForCorrectional(this EnvironmentId environmentUnderTest)
            =>
                TestConfigurationRepository.DefaultInstance.FindEndpoint(
                    CobaltModuleId.Website,
                    CobaltProductId.WlnCorrectional,
                    environmentUnderTest).Uri;
    }
}