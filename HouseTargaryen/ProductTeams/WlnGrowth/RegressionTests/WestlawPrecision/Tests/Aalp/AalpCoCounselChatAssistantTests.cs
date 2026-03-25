namespace WestlawPrecision.Tests.Aalp
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Products.Shared.Managers;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.Header;
    using Framework.Common.UI.Products.WestlawEdge.Pages.QuickCheck;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP.CoCounsel;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP.ComplaintAnalyzer;
    using Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.CoCounsel;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums.AALP;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums.AALP.ComplaintAnalyzer;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums.CoCounsel;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages.AALP;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages.AALP.ComplaintAnalyzer;
    using Framework.Common.UI.Products.WestLawNext.Pages;
    using Framework.Common.UI.Products.WestLawNext.Utils;
    using Framework.Common.UI.Products.WestLawNextCanada.Pages.Documents;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Header;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Raw.WestlawEdge.Pages.RelatedInfo;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.CommonTypes.Constants;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.CommonTypes.Enums.Environment;
    using Framework.Core.DataModel.Security;
    using Framework.Core.DataModel.Security.Proxies;
    using Framework.Core.DataModel.Security.Specialized;
    using Framework.Core.Utils.Execution;
    using Framework.Core.Utils.Extensions;
    using Framework.Core.Utils.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium;
    using WestlawPrecision.Utilities;
    using DeliveryOptions = Framework.Common.UI.Products.WestlawEdgePremium.Enums.CoCounsel.DeliveryOptions;

    /// <summary>
    /// CoCounsel Chat Assistant tests
    /// </summary>
    [TestClass]
    public class AalpCoCounselChatAssistantTests : AalpBaseTest
    {
        private const string FeatureTestCategory = "CoCounselChatAssistant";
        private const string CoCounselCommonTestCategory = "CoCounselCommon";
        private const string ClaimsExplorerSkillTestCategory = "CoCounselClaimsExplorer";
        private const string AIAssistantResearchSkillTestCategory = "CoCounselAIAssistantResearch";
        private const string QuickCheckSkillTestCategory = "CoCounselQuickCheck";
        private const string ComplaintAnalysisSkillTestCategory = "CoCounselComplaintAnalysis";

        [TestMethod]
        [TestCategory(CoCounselCommonTestCategory)]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamMatzekCategory)]      
        public void AiCoCounselTest()
        {
            string checkOpenInWestlawExpandsCoCounselChatAssistant = "Verify: Clicking 'Open in Westlaw' button expands 'CoCounsel' chat assistant";
            string checkOpenInPracticalLawExpandsCoCounselChatAssistant = "Verify: Clicking 'Open in Practical Law' button expands 'CoCounsel' chat assistant";

            var homePage = this.GetHomePage<PrecisionHomePage>();

            var coCounselChatAssistantDialog = homePage.Header.ClickHeaderTab<CoCounselChatAssistantDialog>(EdgeHeaderTabs.CoCounsel);

            this.TestCaseVerify.IsTrue(
                checkOpenInWestlawExpandsCoCounselChatAssistant,
                coCounselChatAssistantDialog.GetAttribute("class").Contains("expanded"),
                "Clicking 'Open in Westlaw' button doesn't expand 'CoCounsel' chat assistant");

            var practicalLawPage = homePage.Header.CompartmentDropdown.SelectOption<PracticalLawPage>(CompartmentType.PracticalLaw);

            coCounselChatAssistantDialog = practicalLawPage.Header.ClickHeaderTab<CoCounselChatAssistantDialog>(EdgeHeaderTabs.CoCounsel);

            this.TestCaseVerify.IsTrue(
                checkOpenInPracticalLawExpandsCoCounselChatAssistant,
                coCounselChatAssistantDialog.GetAttribute("class").Contains("expanded"),
                "Clicking 'Open in Practical Law' button doesn't expand 'CoCounsel' chat assistant");
        }

        /// <summary>
        /// Verify Claims Explorer, Keycite falg, meta data info, Datetime, Disclaimer Info in the CoCounsel dialog 
        /// Test case: 2128451, 2134458, 2134460, 2134461, 2147732
        /// </summary>
        [TestMethod]
        [TestCategory(ClaimsExplorerSkillTestCategory)]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        public void AiCoCounselClaimsExplorerCommonTest()
        {
            const string Question = "A sheriff has a personal vandetta against a former deputy who reported criminal activity within the department. The deputy has been trying to get a new job in law enforcement, but every time he applies for a position at a police department in the county, the sheriff heads down to meet with the police chief, and says that if the deputy is hired, the sheriff's department will withhold resources from the police department. Does the deputy have a cause of action?";
            const string DisclaimerInformation = "Claims Explorer results use generative AI and should be verified for accuracy.";

            string checkFederalTabIsNotEmpty = "Verify: Federal tab is not empty";
            string checkStateTabIsNotEmpty = "Verify: State tab is not empty";
            string checkKeyCiteFlag = "Verify: Keycite Falg is displayed";
            string checkMetadataInfo = "Verify: Meta data info is displayed";
            string checkDateTime = "Verify: DateTime is displayed";
            string checkDisclaimerInfo = "Verify: Disclaimer information is disaplyed";

            var coCounselChatAssistantDialog = new CoCounselChatAssistantDialog();

            coCounselChatAssistantDialog = coCounselChatAssistantDialog.MaximizeButton.Click<CoCounselChatAssistantDialog>();

            SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.Chat.ResearchActionButton.Displayed);

            coCounselChatAssistantDialog.Chat.QuestionTextbox.SendKeys(Question);
            coCounselChatAssistantDialog = coCounselChatAssistantDialog.Chat.SubmitButton.Click<CoCounselChatAssistantDialog>();

            SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.Chat.JurisdictionButton.Displayed);

            var aiJurisdictionDialog = coCounselChatAssistantDialog.Chat.JurisdictionButton.Click<CoCounselClaimsExplorerJurisdictionDialog>();

            SafeMethodExecutor.WaitUntil(() => aiJurisdictionDialog.SaveButton.Displayed);

            coCounselChatAssistantDialog = aiJurisdictionDialog.SelectJurisdictions(true, Jurisdiction.NewYork, Jurisdiction.Federal).SaveButton.Click<CoCounselChatAssistantDialog>();

            coCounselChatAssistantDialog = coCounselChatAssistantDialog.Chat.SubmitButton.Click<CoCounselChatAssistantDialog>();

            SafeMethodExecutor.WaitUntil(() => !coCounselChatAssistantDialog.Chat.ProgressLabel.Displayed);
            SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.Chat.ClaimsExplorerQuestionAndAnswerItem.AnswerTabPanel.IsDisplayed(ClaimsExplorerAnswerTab.Federal));

            var federalTab = coCounselChatAssistantDialog.Chat.ClaimsExplorerQuestionAndAnswerItem.AnswerTabPanel.SetActiveTab<FederalTabComponent>(ClaimsExplorerAnswerTab.Federal);

            coCounselChatAssistantDialog.Chat.DisclaimerInformationLabel.ScrollToElement();

            this.TestCaseVerify.IsTrue(
                checkDisclaimerInfo,
                coCounselChatAssistantDialog.Chat.DisclaimerInformationLabel.Text.Equals(DisclaimerInformation),
                "Disclaimer information is not displayed correctly");

            this.TestCaseVerify.IsTrue(
                checkFederalTabIsNotEmpty,
                federalTab.Headings.Any(item => item.DocumentLink.Displayed),
                "Federal tab is empty");

            this.TestCaseVerify.IsTrue(
                checkKeyCiteFlag,
                federalTab.Headings.Any(doc => doc.KeyCiteFlagLink.Displayed),
                "KeyCite Falg is not displayed");

            this.TestCaseVerify.IsTrue(
                checkMetadataInfo,
                federalTab.Headings.Any(doc => doc.LastAmendedLabel.Displayed),
                "Last Amended meta data info is not displayed");

            this.TestCaseVerify.IsTrue(
                checkDateTime,
                coCounselChatAssistantDialog.Chat.DateTimeLabel.Displayed,
                "Datetime info is not displayed");

            var stateTab = coCounselChatAssistantDialog.Chat.ClaimsExplorerQuestionAndAnswerItem.AnswerTabPanel.SetActiveTab<StateTabComponent>(ClaimsExplorerAnswerTab.State);

            coCounselChatAssistantDialog.Chat.DisclaimerInformationLabel.ScrollToElement();

            this.TestCaseVerify.IsTrue(
                checkStateTabIsNotEmpty,
                stateTab.Headings.Any(item => item.DocumentLink.Displayed),
                "State tab is empty");
        }

        /// <summary>
        /// Verify Claims Explorer Out of Plan in the CoCounsel dialog
        /// Test case: 2133535
        /// </summary>
        [TestMethod]
        [TestCategory(ClaimsExplorerSkillTestCategory)]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        [TestProperty("OutOfPlanUser", "On")]
        public void AiCoCounselClaimsExplorerOutOfPlanTest()
        {
            const string Question = "Plaintiff is a former employee of defendant whose father suffered a stroke that left him with permanent disabilities, and plaintiff spends a lot of time taking care of him. During the pandemic, plaintiff’s father’s doctor advised he stay home and that plaintiff stay home to help him without going out so he didn’t get infected. Plaintiff was told he’d have to use accumulated sick leave but that taking the time off would “reflect poorly” on him and could lead to a pay decrease or termination. When he took time off, he began to hear about negative comments made about him, and he was eventually laid off. He complained to human resources, but was still terminated.";

            string checkOutOfPlanBanners = "Verify: CoCounsel Claims Explorer supporting materials contain OOP banners";

            var coCounselChatAssistantDialog = new CoCounselChatAssistantDialog();

            coCounselChatAssistantDialog = coCounselChatAssistantDialog.MaximizeButton.Click<CoCounselChatAssistantDialog>();

            SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.Chat.ResearchActionButton.Displayed);

            coCounselChatAssistantDialog.Chat.QuestionTextbox.SendKeys(Question);
            coCounselChatAssistantDialog = coCounselChatAssistantDialog.Chat.SubmitButton.Click<CoCounselChatAssistantDialog>();

            SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.Chat.JurisdictionButton.Displayed);

            var aiJurisdictionDialog = coCounselChatAssistantDialog.Chat.JurisdictionButton.Click<CoCounselClaimsExplorerJurisdictionDialog>();
            coCounselChatAssistantDialog = aiJurisdictionDialog.SelectJurisdictions(true, Jurisdiction.California, Jurisdiction.Federal).SaveButton.Click<CoCounselChatAssistantDialog>();

            coCounselChatAssistantDialog = coCounselChatAssistantDialog.Chat.SubmitButton.Click<CoCounselChatAssistantDialog>();

            SafeMethodExecutor.WaitUntil(() => !coCounselChatAssistantDialog.Chat.ProgressLabel.Displayed);

            coCounselChatAssistantDialog.Chat.DisclaimerInformationLabel.ScrollToElement();

            var federalTab = coCounselChatAssistantDialog.Chat.ClaimsExplorerQuestionAndAnswerItem.AnswerTabPanel.SetActiveTab<FederalTabComponent>(ClaimsExplorerAnswerTab.Federal);

            var isFederalOop = federalTab.Headings.Any(item => item.OutOfPlanLabel.Displayed);

            var stateTab = coCounselChatAssistantDialog.Chat.ClaimsExplorerQuestionAndAnswerItem.AnswerTabPanel.SetActiveTab<StateTabComponent>(ClaimsExplorerAnswerTab.State);

            var isStateOop = stateTab.Headings.Any(item => item.OutOfPlanLabel.Displayed);

            this.TestCaseVerify.IsTrue(
               checkOutOfPlanBanners,
               isFederalOop
               || isStateOop,
               "CoCounsel Claims Explorer supporting materials don't contain OOP banners");
        }

        /// <summary>
        /// Verify filtering on federal and state tabs in CoCounsel
        /// Test case: 2135486, 2136165
        /// </summary>
        [TestMethod]
        [TestCategory(ClaimsExplorerSkillTestCategory)]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        public void AiCoCounselClaimsExplorerPillsFilteringTest()
        {
            const string Question = "sheriff has a personal vandetta against a former deputy who reported criminal activity within the department. The deputy has been trying to get a new job in law enforcement, but every time he applies for a position at a police department in the county, the sheriff heads down to meet with the police chief, and says that if the deputy is hired, the sheriff's department will withhold resources from the police department. Does the deputy have a cause of action?";

            string checkCounterBadges = "Verify: Counter badges numbers are equal to 'All' pills numbers";
            string checkFiltersOrder = "Verify: Filters order is correct";
            string checkAllByDefault = "Verify: 'All' filter pill is selected by default, items count equals to 'All' pill number";
            string checkNumbersSum = "Verify: 'All' number equals to the sum of 'Supported' and 'Additional facts needed'";
            string checkFiltersAreIndependent = "Verify: Filters are independent of tabs and can be applied independently";
            string checkSupportedCount = "Verify: 'Supported' items count equals to 'Supported' pill number";
            string checkSupportedOnly = "Verify: Only 'Supported' items are displayed for 'Supported' filter";
            string checkAllContainsSupported = "Verify: 'All' items contains all 'Supported' items";
            string checkAdditionalFactsCount = "Verify: 'Additional facts needed' items count equals to 'Additional facts needed' pill number";
            string checkAdditionalFactsOnly = "Verify: Only 'Additional facts needed' items are displayed for 'Additional facts needed' filter";
            string checkAllContainsAdditionalFacts = "Verify: 'All' items contains all 'Additional facts needed' items";

            var coCounselChatAssistantDialog = new CoCounselChatAssistantDialog();

            coCounselChatAssistantDialog = coCounselChatAssistantDialog.MaximizeButton.Click<CoCounselChatAssistantDialog>();

            SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.Chat.ResearchActionButton.Displayed);

            coCounselChatAssistantDialog.Chat.QuestionTextbox.SendKeys(Question);
            coCounselChatAssistantDialog = coCounselChatAssistantDialog.Chat.SubmitButton.Click<CoCounselChatAssistantDialog>();

            SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.Chat.JurisdictionButton.Displayed);

            var aiJurisdictionDialog = coCounselChatAssistantDialog.Chat.JurisdictionButton.Click<CoCounselClaimsExplorerJurisdictionDialog>();
            coCounselChatAssistantDialog = aiJurisdictionDialog.SelectJurisdictions(true, Jurisdiction.California, Jurisdiction.Federal).SaveButton.Click<CoCounselChatAssistantDialog>();

            coCounselChatAssistantDialog = coCounselChatAssistantDialog.Chat.SubmitButton.Click<CoCounselChatAssistantDialog>();

            SafeMethodExecutor.WaitUntil(() => !coCounselChatAssistantDialog.Chat.ProgressLabel.Displayed);

            coCounselChatAssistantDialog.Chat.DisclaimerInformationLabel.ScrollToElement();

            var stateTabComponent = coCounselChatAssistantDialog.Chat.ClaimsExplorerQuestionAndAnswerItem.AnswerTabPanel.SetActiveTab<StateTabComponent>(ClaimsExplorerAnswerTab.State);
            var stateAllPillNumber = stateTabComponent.AllFilterButton.Text.ConvertCountToInt();

            var federalTabComponent = coCounselChatAssistantDialog.Chat.ClaimsExplorerQuestionAndAnswerItem.AnswerTabPanel.SetActiveTab<FederalTabComponent>(ClaimsExplorerAnswerTab.Federal);

            var allItems = federalTabComponent.Headings
                .SelectMany((item, index) =>
                {
                    var subHeadingTexts = new List<string>();

                    if (index.Equals(0))
                    {
                        subHeadingTexts.AddRange(item.SubHeadings.Select(subItem => subItem.HeadingLabel.Text));
                        item.HeadingAccordionButton.Click();
                    }
                    else
                    {
                        item.HeadingAccordionButton.Click();
                        item.SubHeadings.First().BadgeLabel.ScrollToElement();
                        subHeadingTexts.AddRange(item.SubHeadings.Select(subItem => subItem.HeadingLabel.Text));
                        item.HeadingAccordionButton.Click();
                    }

                    return subHeadingTexts;
                }).ToList();

            allItems.RemoveAll(item => string.IsNullOrWhiteSpace(item));

            var allButtons = federalTabComponent.FilterButtons.Select(button => button.Text).ToList();
            var federalAllPillNumber = federalTabComponent.AllFilterButton.Text.ConvertCountToInt();
            var supportedPillNumber = federalTabComponent.SupportedFilterButton.Text.ConvertCountToInt();
            var additionalFactsPillNumber = federalTabComponent.AdditionalFactsNeededFilterButton.Text.ConvertCountToInt();

            this.TestCaseVerify.IsTrue(
                checkCounterBadges,
                coCounselChatAssistantDialog.Chat.ClaimsExplorerQuestionAndAnswerItem.AnswerTabPanel.TabHeaderLabel(ClaimsExplorerAnswerTab.Federal).Text.ConvertCountToInt().Equals(federalAllPillNumber)
                && coCounselChatAssistantDialog.Chat.ClaimsExplorerQuestionAndAnswerItem.AnswerTabPanel.TabHeaderLabel(ClaimsExplorerAnswerTab.State).Text.ConvertCountToInt().Equals(stateAllPillNumber),
                "Counter badges numbers are NOT equal to 'All' pills numbers");

            var expectedFiltersOrder = new List<string>
            {
                $"All ({federalAllPillNumber})",
                $"Supported ({supportedPillNumber})" ,
                $"Additional facts needed ({additionalFactsPillNumber})"
            };

            this.TestCaseVerify.IsTrue(
                checkFiltersOrder,
                allButtons.SequenceEqual(expectedFiltersOrder),
                "Filters order is NOT correct");

            this.TestCaseVerify.IsTrue(
                checkAllByDefault,
                allItems.ToList().TrueForAll(item => item.Contains("Supported") || item.Contains("Additional facts needed"))
                && allItems.Count.Equals(federalAllPillNumber),
                "'All' filter pill is NOT selected by default, items count doesn't equal to 'All' pill number");

            this.TestCaseVerify.IsTrue(
                checkNumbersSum,
                federalAllPillNumber.Equals(supportedPillNumber + additionalFactsPillNumber),
                "'All' number doesn't equal to the sum of 'Supported' and 'Additional facts needed'");

            coCounselChatAssistantDialog.Chat.DisclaimerInformationLabel.ScrollToElement();

            federalTabComponent = federalTabComponent.SupportedFilterButton.Click<FederalTabComponent>();

            stateTabComponent = coCounselChatAssistantDialog.Chat.ClaimsExplorerQuestionAndAnswerItem.AnswerTabPanel.SetActiveTab<StateTabComponent>(ClaimsExplorerAnswerTab.State);

            this.TestCaseVerify.IsTrue(
                checkFiltersAreIndependent,
                stateTabComponent.SupportedFilterButton.GetAttribute("aria-selected").Equals("false"),
                "Filters are NOT independent of tabs and can NOT be applied independently");

            coCounselChatAssistantDialog.Chat.DisclaimerInformationLabel.ScrollToElement();

            federalTabComponent = coCounselChatAssistantDialog.Chat.ClaimsExplorerQuestionAndAnswerItem.AnswerTabPanel.SetActiveTab<FederalTabComponent>(ClaimsExplorerAnswerTab.Federal);

            var supportedItems = federalTabComponent.Headings
                .SelectMany((item, index) =>
                {
                    var subHeadingTexts = new List<string>();

                    item.HeadingAccordionButton.Click();
                    item.SubHeadings.First().HeadingLabel.ScrollToElement();
                    subHeadingTexts.AddRange(item.SubHeadings.Select(subItem => subItem.HeadingLabel.Text));
                    item.HeadingAccordionButton.Click();

                    return subHeadingTexts;
                }).ToList();

            supportedItems.RemoveAll(item => string.IsNullOrWhiteSpace(item));

            this.TestCaseVerify.IsTrue(
                checkSupportedCount,
                supportedItems.Count.Equals(supportedPillNumber),
                "'Supported' items count doesn't equal to 'Supported' pill number");

            this.TestCaseVerify.IsTrue(
                checkSupportedOnly,
                supportedItems.TrueForAll(item => item.Contains("Supported")),
                "NOT only 'Supported' items are displayed for 'Supported' filter");

            this.TestCaseVerify.IsTrue(
                checkAllContainsSupported,
                allItems.Where(item => item.Contains("Supported")).ToList().SequenceEqual(supportedItems),
                "'All' items doesn't contain all 'Supported' items");

            federalTabComponent = federalTabComponent.AdditionalFactsNeededFilterButton.Click<FederalTabComponent>();

            var additionalFactsNeededItems = federalTabComponent.Headings
                .SelectMany((item, index) =>
                {
                    var subHeadingTexts = new List<string>();

                    item.HeadingAccordionButton.Click();
                    item.SubHeadings.First().HeadingLabel.ScrollToElement();
                    subHeadingTexts.AddRange(item.SubHeadings.Select(subItem => subItem.HeadingLabel.Text));
                    item.HeadingAccordionButton.Click();

                    return subHeadingTexts;
                }).ToList();

            additionalFactsNeededItems.RemoveAll(item => string.IsNullOrWhiteSpace(item));

            this.TestCaseVerify.IsTrue(
                checkAdditionalFactsCount,
                additionalFactsNeededItems.Count.Equals(additionalFactsPillNumber),
                "'Additional facts needed' items count doesn't equal to 'Additional facts needed' pill number");

            this.TestCaseVerify.IsTrue(
                checkAdditionalFactsOnly,
                additionalFactsNeededItems.TrueForAll(item => item.Contains("Additional facts needed")),
                "NOT only 'Additional facts needed' items are displayed for 'Additional facts needed' filter");

            this.TestCaseVerify.IsTrue(
                checkAllContainsAdditionalFacts,
                allItems.Where(item => item.Contains("Additional facts needed")).ToList().SequenceEqual(additionalFactsNeededItems),
                "'All' items doesn't contain all 'Additional facts needed' items");
        }

        /// <summary>
        /// Verify tray dropdown
        /// </summary>
        [TestMethod]
        [TestCategory(CoCounselCommonTestCategory)]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        public void AiCoCounselTrayTest()
        {
            string checkDialogPositionAcrossSessions = "Verify: Dialog position is saved across sessions";
            string checkMenuItemsWhenNotExpanded = "Verify: Menu has 2 items when not expanded";
            string checkMenuItemsWhenExpanded = "Verify: Menu has 2 items when expanded";
            string checkMenuItemsWhenMaximized = "Verify: Menu has only 1 'Hide' item when maximized";
            string checkNotMaximizedOnAnotherTab = "Verify: Dialog default position is changed to 'Maximized' due to the previous tab changes";
            string checkNotCollapasedOnAnotherTab = "Verify: Dialog default position is changed to 'Collapsed' due to the previous tab changes";

            var homePage = this.GetHomePage<PrecisionHomePage>();

            var coCounselChatAssistantDialog = new CoCounselChatAssistantDialog();

            SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.MenuDropdown.IsDisplayed());

            if (coCounselChatAssistantDialog.GetAttribute("class").Contains("leftAligned"))
            {
                coCounselChatAssistantDialog = coCounselChatAssistantDialog.MenuDropdown.SelectOption<CoCounselChatAssistantDialog>(TrayOptions.MoveToRight);
                SafeMethodExecutor.WaitUntil(() => !coCounselChatAssistantDialog.GetAttribute("class").Contains("leftAligned"));
            }

            coCounselChatAssistantDialog = coCounselChatAssistantDialog.MenuDropdown.SelectOption<CoCounselChatAssistantDialog>(TrayOptions.MoveToLeft);

            this.DefaultSignOnManager.SignOff();
            homePage = this.SignOnBack().ClickContinueButton<PrecisionHomePage>();

            this.TestCaseVerify.IsTrue(
                checkDialogPositionAcrossSessions,
                coCounselChatAssistantDialog.GetAttribute("class").Contains("leftAligned"),
                "Dialog position is NOT saved across sessions");

            coCounselChatAssistantDialog = coCounselChatAssistantDialog.MenuDropdown.SelectOption<CoCounselChatAssistantDialog>(TrayOptions.MoveToRight);

            SafeMethodExecutor.WaitUntil(() => !coCounselChatAssistantDialog.GetAttribute("class").Contains("leftAligned"));

            this.TestCaseVerify.IsTrue(
                checkMenuItemsWhenNotExpanded,
                coCounselChatAssistantDialog.MenuDropdown.Options.ToList().Any(item => item.Equals(TrayOptions.MoveToLeft))
                && coCounselChatAssistantDialog.MenuDropdown.Options.ToList().Any(item => item.Equals(TrayOptions.Hide)),
                "Menu don't have 2 items when not expanded");

            coCounselChatAssistantDialog = coCounselChatAssistantDialog.ExpandButton.Click<CoCounselChatAssistantDialog>();

            SafeMethodExecutor.WaitUntil(() => !coCounselChatAssistantDialog.ExpandButton.GetAttribute("aria-expanded").Contains("false"));
            SafeMethodExecutor.WaitUntil(() => !coCounselChatAssistantDialog.ExpandButton.Displayed);
            SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.CollapseButton.Displayed);

            this.TestCaseVerify.IsTrue(
                checkMenuItemsWhenExpanded,
                coCounselChatAssistantDialog.MenuDropdown.Options.ToList().Any(item => item.Equals(TrayOptions.MoveToLeft))
                && coCounselChatAssistantDialog.MenuDropdown.Options.ToList().Any(item => item.Equals(TrayOptions.Hide)),
                "Menu don't have 2 items when expanded");

            coCounselChatAssistantDialog = coCounselChatAssistantDialog.MaximizeButton.Click<CoCounselChatAssistantDialog>();

            SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.GetAttribute("class").Contains("maximized"));
            SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.MinimizeButton.Displayed);

            this.TestCaseVerify.IsTrue(
                checkMenuItemsWhenMaximized,
                coCounselChatAssistantDialog.MenuDropdown.Options.ToList().Count.Equals(1)
                && coCounselChatAssistantDialog.MenuDropdown.Options.ToList().Any(item => item.Equals(TrayOptions.Hide)),
                "Menu don't have only 1 'Hide' item when maximized");

            BrowserTabManager.Instance.SetCurrentTabName("FirstTab");

            BrowserTabManager.Instance.OpenUrlInNewTab("SecondTab", BrowserPool.CurrentBrowser.Url);
            BrowserTabManager.Instance.SetTabActive("SecondTab");

            SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.MaximizeButton.Displayed);

            this.TestCaseVerify.IsTrue(
                checkNotMaximizedOnAnotherTab,
                !coCounselChatAssistantDialog.GetAttribute("class").Contains("leftAligned")
                && coCounselChatAssistantDialog.GetAttribute("class").Contains("minimized"),
                "Dialog default state is changed to 'Maximized' due to the previous tab changes");

            coCounselChatAssistantDialog = coCounselChatAssistantDialog.MaximizeButton.Click<CoCounselChatAssistantDialog>();

            BrowserTabManager.Instance.SetTabActive("FirstTab");

            coCounselChatAssistantDialog = coCounselChatAssistantDialog.CollapseButton.Click<CoCounselChatAssistantDialog>();

            BrowserTabManager.Instance.SetTabActive("SecondTab");

            this.TestCaseVerify.IsTrue(
                checkNotCollapasedOnAnotherTab,
                coCounselChatAssistantDialog.GetAttribute("class").Contains("maximized"),
                "Dialog default state is changed to 'Collapsed' due to the previous tab changes");

            BrowserTabManager.Instance.SetTabActive("FirstTab");

            coCounselChatAssistantDialog = coCounselChatAssistantDialog.MaximizeButton.Click<CoCounselChatAssistantDialog>();
            coCounselChatAssistantDialog = coCounselChatAssistantDialog.MinimizeButton.Click<CoCounselChatAssistantDialog>();

            BrowserTabManager.Instance.SetTabActive("SecondTab");

            this.TestCaseVerify.IsTrue(
                checkNotMaximizedOnAnotherTab,
                coCounselChatAssistantDialog.GetAttribute("class").Contains("maximized"),
                "Dialog default state is changed to 'Minimized' due to the previous tab changes");
        }

        //[TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        public void AiCoCounselChatTest()
        {
            const string Question = "For paid family leave, are siblings of employees covered as family members?";

            //string checkInlineKeyCiteFlagLink = "Verify: Inline KC flag opens negative treatment page on the same browser tab";
            //string checkInlineTitleLink = "Verify: Inline title opens document page on the same browser tab";

            var homePage = this.GetHomePage<PrecisionHomePage>();

            var coCounselChatAssistantDialog = new CoCounselChatAssistantDialog();

            coCounselChatAssistantDialog = coCounselChatAssistantDialog.MaximizeButton.Click<CoCounselChatAssistantDialog>();

            coCounselChatAssistantDialog.Chat.QuestionTextbox.SendKeys(Question);
            coCounselChatAssistantDialog = coCounselChatAssistantDialog.Chat.SubmitButton.Click<CoCounselChatAssistantDialog>();

            SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.Chat.AiAssistedResearchRadiobutton.Displayed);

            coCounselChatAssistantDialog.Chat.AiAssistedResearchRadiobutton.Select();

            coCounselChatAssistantDialog = coCounselChatAssistantDialog.Chat.StartAiAssistedResearchButton.Click<CoCounselChatAssistantDialog>();

            SafeMethodExecutor.WaitUntil(() => !coCounselChatAssistantDialog.Chat.StartAiAssistedResearchButton.Displayed);
            SafeMethodExecutor.WaitUntil(() => !coCounselChatAssistantDialog.Chat.CoCounselQuestionAndAnswerItems.First().LoadingAnswerLabel.Displayed);

            var negativeTreatmentPage = coCounselChatAssistantDialog.Chat.CoCounselQuestionAndAnswerItems.First().InlineTitlesKeyCiteFlagsLinks.First().Click<EdgeNegativeTreatmentPage>();

            /*            this.TestCaseVerify.IsTrue(
                            checkInlineKeyCiteFlagLink,
                            negativeTreatmentPage.RiTabs.GetSelectedTab().Equals(RiTab.NegativeTreatment)
                            || negativeTreatmentPage.RiTabs.GetSelectedTab().Equals(RiTab.History),
                            "Inline KC flag doesn't open negative treatment page on the same browser tab");*/

            homePage = BrowserPool.CurrentBrowser.GoBack<PrecisionHomePage>();

            coCounselChatAssistantDialog = coCounselChatAssistantDialog.MaximizeButton.Click<CoCounselChatAssistantDialog>();

            //SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.GetAttribute("class").Contains("maximized"));
            //SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.Chat.CoCounselAnswerItems.First().LoadingAnswerLabel.Displayed);
            //SafeMethodExecutor.WaitUntil(() => !coCounselChatAssistantDialog.Chat.CoCounselAnswerItems.First().LoadingAnswerLabel.Displayed);

            var documentPage = coCounselChatAssistantDialog.Chat.CoCounselQuestionAndAnswerItems.First().InlineTitlesLinks.First().Click<PrecisionCommonDocumentPage>();

            /*            this.TestCaseVerify.IsTrue(
                            checkInlineTitleLink,
                            documentPage.IsDocumentLoaded(),
                            "Inline title doesn't open document page on the same browser tab");*/
        }

        /// <summary>
        /// Test case: 2086563
        /// Verify delivered information
        /// </summary>
        [TestMethod]
        [TestCategory(AIAssistantResearchSkillTestCategory)]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        [TestCategory(TeamMatzekAdvantageSmokeCategory)]
        public void AiCoCounselDeliveryTest()
        {
            const string Question = "What length should a counter should be in public places?";

            string checkDelivery = "Verify: Delivered document contains all the required information";

            var homePage = this.GetHomePage<PrecisionHomePage>();

            homePage = homePage.Header.OpenJurisdictionDialog().SelectDefaultJurisdiction().SaveButton.Click<PrecisionHomePage>();

            var coCounselChatAssistantDialog = new CoCounselChatAssistantDialog();

            coCounselChatAssistantDialog = coCounselChatAssistantDialog.MaximizeButton.Click<CoCounselChatAssistantDialog>();

            SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.Chat.QuestionTextbox.Displayed);

            coCounselChatAssistantDialog.Chat.QuestionTextbox.SendKeys(Question);
            coCounselChatAssistantDialog = coCounselChatAssistantDialog.Chat.SubmitButton.Click<CoCounselChatAssistantDialog>();

            SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.Chat.AiAssistedResearchRadiobutton.Displayed);

            coCounselChatAssistantDialog.Chat.AiAssistedResearchRadiobutton.Select();

            coCounselChatAssistantDialog = coCounselChatAssistantDialog.Chat.StartAiAssistedResearchButton.Click<CoCounselChatAssistantDialog>();

            SafeMethodExecutor.WaitUntil(() => !coCounselChatAssistantDialog.Chat.StartAiAssistedResearchButton.Displayed);
            SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.Chat.ViewAllCitedSourcesButton.Present);

            coCounselChatAssistantDialog.Chat.ViewAllCitedSourcesButton.ScrollToElement();

            coCounselChatAssistantDialog = coCounselChatAssistantDialog.Chat.ViewAllCitedSourcesButton.Click<CoCounselChatAssistantDialog>();

            var actualQuestion = this.CleanTextForCompare(coCounselChatAssistantDialog.Chat.CoCounselQuestionAndAnswerItems.First().QuestionLabel.Text);

            var headingTitles = new List<string>() { "Sources" };

            headingTitles.AddRange(coCounselChatAssistantDialog.CoCounselAllCitedSources.Headings.Select(item => item.HeadingButton.Text).ToList());

            var documentTitles = coCounselChatAssistantDialog.CoCounselAllCitedSources.SupportingMaterialsItems.Select(item => item.DocumentTitleLink.Text).ToList();

            coCounselChatAssistantDialog.Chat.CoCounselQuestionAndAnswerItems.First().QuestionLabel.ScrollToElement();

            coCounselChatAssistantDialog = coCounselChatAssistantDialog.Chat.CoCounselQuestionAndAnswerItems.First().CoCounselDeliveryDropdown.SelectOption<CoCounselChatAssistantDialog>(DeliveryOptions.WordDocument);

            var fileName = $"cocounsel_aalr_{DateTime.Now.ToString("yyyy_MM_dd")}.docx";
            FileUtil.WaitForFileDownload(FolderToSave, fileName);
            var text = this.CleanTextForCompare(DocxTextExtractor.ExtractTextFromWord(Path.Combine(FolderToSave, fileName)));

            this.TestCaseVerify.IsTrue(
                checkDelivery,
                text.Contains($"QuestionPresented{actualQuestion}")
                && text.Contains("Response:")
                && text.Contains(".Itshouldbeverifiedforaccuracy.")
                && headingTitles.TrueForAll(item => text.Contains(this.CleanTextForCompare(item)))
                && documentTitles.TrueForAll(item => text.Contains(this.CleanTextForCompare(item))),
                "Delivered document doesn't contain all the required information");
        }

        /// <summary>
        /// Test case: 2113114
        /// Verify Cited Sources, Verify Feedback buttons
        /// </summary>
        [TestMethod]
        [TestCategory(AIAssistantResearchSkillTestCategory)]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        public void AiCoCounselCitedSourcesTest()
        {
            const string Question = "What length should toilet paper be in public places?";

            var expectedHeadingTitlesList = new List<string> { "Relevant resources and snippets", "Administrative decisions and guidance", "Practical Law", "Additional secondary sources", "Current awareness" };

            string checkAllCitedSourcesHeadings = "Verify: All Cited Sources headings are as expected";
            string checkJumpLinkScrollsToTheDocument = "Verify: Jump link scrolls to the right document";
            string checkSupportingMaterialsMetadata = "Verify: Supporting materials metadata reflects the right jurisdictions";
            string checkFeedbackButtons = "Verify: Feedback buttons yes/no are displayed";
            //string checkDocumentTitleLeadsToRightEnvironment = "Verify: Document supporting materials title leads to the right environment";

            var homePage = this.GetHomePage<PrecisionHomePage>();

            homePage = homePage.Header.OpenJurisdictionDialog().SelectJurisdictions(true, Jurisdiction.Michigan, Jurisdiction.Minnesota).SaveButton.Click<PrecisionHomePage>();

            var coCounselChatAssistantDialog = new CoCounselChatAssistantDialog();

            coCounselChatAssistantDialog = coCounselChatAssistantDialog.MaximizeButton.Click<CoCounselChatAssistantDialog>();

            SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.Chat.QuestionTextbox.Displayed);

            coCounselChatAssistantDialog.Chat.QuestionTextbox.SendKeys(Question);
            coCounselChatAssistantDialog = coCounselChatAssistantDialog.Chat.SubmitButton.Click<CoCounselChatAssistantDialog>();

            SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.Chat.AiAssistedResearchRadiobutton.Displayed);

            coCounselChatAssistantDialog.Chat.AiAssistedResearchRadiobutton.Select();

            coCounselChatAssistantDialog = coCounselChatAssistantDialog.Chat.StartAiAssistedResearchButton.Click<CoCounselChatAssistantDialog>();

            SafeMethodExecutor.WaitUntil(() => !coCounselChatAssistantDialog.Chat.StartAiAssistedResearchButton.Displayed);
            SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.Chat.ViewAllCitedSourcesButton.Present);

            coCounselChatAssistantDialog.Chat.ViewAllCitedSourcesButton.ScrollToElement();

            this.TestCaseVerify.IsTrue(
                checkFeedbackButtons,
                coCounselChatAssistantDialog.FeedbackYesButton.Displayed
                && coCounselChatAssistantDialog.FeedbackNoButton.Displayed,
                "Feedback Yes/No buttons are not displayed");

            coCounselChatAssistantDialog = coCounselChatAssistantDialog.Chat.ViewAllCitedSourcesButton.Click<CoCounselChatAssistantDialog>();

            var headingTitles = new List<string>() { coCounselChatAssistantDialog.CoCounselAllCitedSources.DialogHeaderLabel.Text };

            headingTitles.AddRange(coCounselChatAssistantDialog.CoCounselAllCitedSources.Headings.Select(item => item.HeadingButton.Text).ToList());

            this.TestCaseVerify.IsTrue(
                checkAllCitedSourcesHeadings,
                expectedHeadingTitlesList.SequenceEqual(headingTitles),
                "All Cited Sources headings are NOT as expected");

            coCounselChatAssistantDialog.CoCounselAllCitedSources.ClosePanelButton.ScrollToElement();

            coCounselChatAssistantDialog = coCounselChatAssistantDialog.CoCounselAllCitedSources.ClosePanelButton.Click<CoCounselChatAssistantDialog>();

            coCounselChatAssistantDialog = coCounselChatAssistantDialog.Chat.CoCounselQuestionAndAnswerItems.First().JumpLinks.First().Click<CoCounselChatAssistantDialog>();

            this.TestCaseVerify.IsTrue(
                checkJumpLinkScrollsToTheDocument,
                coCounselChatAssistantDialog.CoCounselAllCitedSources.SupportingMaterialsItems.First().DocumentTitleLink.IsInView,
                "Jump link doesn't scroll to the right document");

            var metadataLabels = coCounselChatAssistantDialog.CoCounselAllCitedSources.SupportingMaterialsItems.Select(item => item.MetadataLabel.Text).ToList();

            this.TestCaseVerify.IsTrue(
                checkSupportingMaterialsMetadata,
                metadataLabels.TrueForAll(item => item.Contains("MI") || item.Contains("MN") || item.Contains("Minnesota") || item.Contains("Michigan") || item.Contains("FRCP")),
                "Supporting materials metadata doesn't relfect the right jurisdictions");

            // Uncomment when Bug 2088250 is fixed
            //var documentPage = coCounselChatAssistantDialog.CoCounselAllCitedSources.SupportingMaterialsItems.First().DocumentTitleLink.Click<PrecisionCommonDocumentPage>();

            //this.TestCaseVerify.IsTrue(
            //    checkDocumentTitleLeadsToRightEnvironment,
            //    BrowserPool.CurrentBrowser.Url.Contains(this.TestExecutionContext.TestEnvironment.Name.ToLower()),
            //    "Document supporting materials title doesn't lead to the right environment");
        }

        /// <summary>
        /// Test case: 2080248
        /// Verify CoCounsel history items
        /// </summary>
        [TestMethod]
        [TestCategory(AIAssistantResearchSkillTestCategory)]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        public void AiCoCounselHistoryTest()
        {
            const string Question = "What length should a counter should be in public places?";

            string checkRecentSearchEvent = "Verify: Recent search event contains question, event type and jurisdiction";
            string checkRecentSearchEventOpensConversation = "Verify: Recent search event click opens the recent conversation";
            string checkHistoryTabEvent = "Verify: History event is displayed on the History tab";
            string checkConversationHistoryEvent = "Verify: Conversation (Left Rail) history event is displayed";
            string checkAiResearchFacet = "Verify: History event facet is applied";
            string checkHistoryPageEvent = "Verify: History page event contains question, event type and jurisdiction";

            var homePage = this.GetHomePage<PrecisionHomePage>();

            homePage = homePage.Header.OpenJurisdictionDialog().SelectDefaultJurisdiction().SaveButton.Click<PrecisionHomePage>();

            var coCounselChatAssistantDialog = new CoCounselChatAssistantDialog();

            coCounselChatAssistantDialog = coCounselChatAssistantDialog.MaximizeButton.Click<CoCounselChatAssistantDialog>();

            SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.Chat.QuestionTextbox.Displayed);

            coCounselChatAssistantDialog.Chat.QuestionTextbox.SendKeys(Question);
            coCounselChatAssistantDialog = coCounselChatAssistantDialog.Chat.SubmitButton.Click<CoCounselChatAssistantDialog>();

            SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.Chat.AiAssistedResearchRadiobutton.Displayed);

            coCounselChatAssistantDialog.Chat.AiAssistedResearchRadiobutton.Select();

            coCounselChatAssistantDialog = coCounselChatAssistantDialog.Chat.StartAiAssistedResearchButton.Click<CoCounselChatAssistantDialog>();

            SafeMethodExecutor.WaitUntil(() => !coCounselChatAssistantDialog.Chat.StartAiAssistedResearchButton.Displayed);
            SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.Chat.ViewAllCitedSourcesButton.Present);

            coCounselChatAssistantDialog.Chat.ViewAllCitedSourcesButton.ScrollToElement();

            coCounselChatAssistantDialog = coCounselChatAssistantDialog.CollapseButton.Click<CoCounselChatAssistantDialog>();

            var clientIdPage = new CommonClientIdPage();

            var isDemoEnvironment = this.DefaultSignOnContext.TestEnvironment.Name.ToLower().Contains(CobaltEnvironment.DEMO.GetName().ToLower());

            if (isDemoEnvironment)
            {
                this.DefaultSignOnManager.SignOff();
                this.Settings.AppendValues(EnvironmentConstants.NameOfEnvironmentId, SettingUpdateOption.Overwrite, CobaltEnvironment.QED.GetName());
                clientIdPage = this.SignOnBack();
            }
            else
            {
                this.DefaultSignOnManager.SignOff();
                clientIdPage = this.SignOnBack();
            }

            var eventDescription = clientIdPage.RecentResearchPane.RecentResearchList.First(item => item.Text.Contains(Question)).Text.Replace(" ", string.Empty);

            this.TestCaseVerify.IsTrue(
                checkRecentSearchEvent,
                eventDescription.Equals($"{Question.Replace(" ", string.Empty)}\r\nAI-AssistedResearchAllState&FederalCoCounsel"),
                "Recent search event DOESN'T contain question, event type and jurisdiction");

            var aiAssistantPage = clientIdPage.RecentResearchPane.RecentResearchList.First(item => item.Text.Contains(Question)).ClickTitleLink<AiAssistedResearchPage>();

            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkRecentSearchEventOpensConversation,
                aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text.Remove(0, aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text.IndexOf("says:") + 7).Equals(Question)
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.Count.Equals(1)
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().AnswerLabel.Displayed,
                "Recent search event click DOESN'T open the recent conversation");

            // History tab
            var recentHistoryDialog = aiAssistantPage.Header.ClickHeaderTab<EdgeRecentHistoryDialog>(EdgeHeaderTabs.History);

            this.TestCaseVerify.IsTrue(
                checkHistoryTabEvent,
                recentHistoryDialog.GetRecentSearchesItems().First().HistoryItemLink.Text.Equals(Question)
                && recentHistoryDialog.GetRecentSearchesItems().First().MetaData.Replace(" ", string.Empty).Contains($"AI-AssistedResearchAllState&FederalCoCounsel"),
                "History event is NOT displayed on the History tab");

            aiAssistantPage = aiAssistantPage.ConversationHistory.ExpandButton.Click<AiAssistedResearchPage>();

            // Conversations (Left Rail) history
            var lastConversationEvent = aiAssistantPage.ConversationHistory.Conversations.First().ConversationButton.Text;

            this.TestCaseVerify.IsTrue(
                checkConversationHistoryEvent,
                lastConversationEvent.Contains(Question)
                && isDemoEnvironment ? lastConversationEvent.Contains("CoCounsel") : lastConversationEvent.Contains("Active\r\nCoCounsel") // Bug 2144583
                && lastConversationEvent.Contains($"{DateTime.Now.ToString("MMM d, yyyy", CultureInfo.InvariantCulture)}"),
                "Conversation (Left Rail) history event is NOT displayed");

            // History page
            var historyPage = aiAssistantPage.ConversationHistory.GoToFullHistoryLink.Click<EdgeCommonHistoryPage>();

            BrowserPool.CurrentBrowser.CreateTab(HistoryPageTab);
            BrowserPool.CurrentBrowser.ActivateTab(HistoryPageTab);

            this.TestCaseVerify.IsTrue(
                checkAiResearchFacet,
                historyPage.NarrowPane.Filter.HistoryEventFacet.GetSelectedOptions().First().Equals("AI Research"),
                "History event facet is NOT applied");

            this.TestCaseVerify.AreEqual(
                checkHistoryPageEvent,
                $"{Question}AI-AssistedResearchAllState&FederalCoCounsel".Replace(" ", string.Empty),
                $"{historyPage.HistoryTable.GetGridItems().First().Title}{historyPage.HistoryTable.GetGridItems().First().Summary}".Replace(" ", string.Empty),
                "History page event DOESN'T contains question, event type and jurisdiction");
        }

        /// <summary>
        /// Test case: 2123327
        /// Verify footnote highlights
        /// </summary>
        [TestMethod]
        [TestCategory(AIAssistantResearchSkillTestCategory)]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        public void AiCoCounselDocumentFootnoteHighlightsInCanvasTest()
        {
            const string DocGuid = "I01b06ee05e9811e8a6608077647c238b";

            const string Question = "what were the class criteria?";
            const string FollowUpQuestion = "can you tell me about the settlement?";

            string checkFootnoteHighlighted = "Verify: Clicking on footnote link highlights the section in document";
            string checkNextFootnoteHighlighted = "Verify: Clicking on another footnote link highlights the different section in document";
            string CheckNextFootnoteHighlightedIsNotSameAsBeforeFootnote = "Verify: Another footnote link click removes before highlighted section";

            var documentPage = EdgeNavigationManager.Instance.NavigateToDocumentDirectly<PrecisionCommonDocumentPage>(DocGuid);
            var coCounselChatAssistantDialog = new CoCounselChatAssistantDialog();
            coCounselChatAssistantDialog = coCounselChatAssistantDialog.ExpandButton.Click<CoCounselChatAssistantDialog>();

            SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.GetAttribute("class").Contains("expanded"));
            SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.Chat.AiAssistedResearchSkillButton.Displayed);

            coCounselChatAssistantDialog.Chat.QuestionTextbox.SendKeys(Question);
            coCounselChatAssistantDialog = coCounselChatAssistantDialog.Chat.SubmitButton.Click<CoCounselChatAssistantDialog>();

            SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.Chat.CoCounselQuestionAndAnswerItems.Count > 0);
            SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.Chat.CoCounselQuestionAndAnswerItems.First().ViewAllFootnotesButton.Displayed);

            documentPage = coCounselChatAssistantDialog.Chat.CoCounselQuestionAndAnswerItems.First().FootnoteLinks.First().Click<PrecisionCommonDocumentPage>();

            this.TestCaseVerify.IsTrue(
                checkFootnoteHighlighted,
                documentPage.FootNotesHighlightedLabel.Displayed,
                "First question first Footnote click is not highlighted in canvas area");

            var highlightedText = documentPage.FootNotesHighlightedLabel.Text;

            coCounselChatAssistantDialog.Chat.QuestionTextbox.SendKeys(FollowUpQuestion);
            coCounselChatAssistantDialog = coCounselChatAssistantDialog.Chat.SubmitButton.Click<CoCounselChatAssistantDialog>();

            SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.Chat.CoCounselQuestionAndAnswerItems.Count > 1);

            documentPage = coCounselChatAssistantDialog.Chat.CoCounselQuestionAndAnswerItems.ElementAt(1).FootnoteLinks.First().Click<PrecisionCommonDocumentPage>();

            this.TestCaseVerify.IsTrue(
                checkNextFootnoteHighlighted,
                documentPage.FootNotesHighlightedLabel.Displayed,
                "Second question first Footnote click is not highlighted in canvas area");

            this.TestCaseVerify.IsFalse(
                CheckNextFootnoteHighlightedIsNotSameAsBeforeFootnote,
                documentPage.FootNotesHighlightedLabel.Text.Equals(highlightedText),
                "Second question first Footnote click has not removed the first highlight and not moved to correct position in canvas area");
        }

        /// <summary>
        /// Verify selected Jurisdiction of Claims Explorer Question in the CoCounsel Chat Assistant 
        /// Test case: 2166167
        /// </summary>
        [TestMethod]
        [TestCategory(ClaimsExplorerSkillTestCategory)]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        public void AiCoCounselClaimsExplorerSelectedJurisdictionTest()
        {
            const string JurisdictionQuestion = "My employer refused to allow me to serve as an election judge, and even docked my pay when I used vacation to do so. What claims can I bring in New York?.";

            string checkSelectedJurisdiction = "Verify: Selected Jurisdiction is correct";

            var coCounselChatAssistantDialog = new CoCounselChatAssistantDialog();

            coCounselChatAssistantDialog = coCounselChatAssistantDialog.MaximizeButton.Click<CoCounselChatAssistantDialog>();

            SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.Chat.ResearchActionButton.Displayed);

            coCounselChatAssistantDialog.Chat.QuestionTextbox.SendKeys(JurisdictionQuestion);
            coCounselChatAssistantDialog = coCounselChatAssistantDialog.Chat.SubmitButton.Click<CoCounselChatAssistantDialog>();

            SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.Chat.SelectedJurisdictionLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkSelectedJurisdiction,
                coCounselChatAssistantDialog.Chat.SelectedJurisdictionLabel.Text.Equals("NY"),
                "Selected Jurisdiction is incorrect");
        }

        ///// <summary>
        ///// Verify 'Find Defenses' links behavior for 2K characters in CoCounsel Claims Explorer
        ///// Test Case: 2146973
        ///// </summary>
        //[TestMethod]
        //[TestCategory(CurrentTestCategory)]
        //[TestCategory(FeatureTestCategory)]
        //[TestCategory(TeamMatzekCategory)]
        //[TestProperty(EnvironmentConstants.NameOfEnvironmentId, "QED")]
        //public void CoCounselClaimsExplorerFindDefensesLink2KTest()
        //{
        //    const string Question2000Characters = "My employer refused to allow me to serve as an election judge, and even docked my pay when I used vacation to do so"; // Ensure this is 2000 characters
        //                                                                                                                                                                 //const string BuildedQuery2000CharactersActionableDataPattern = "I have a potential claim under {0} which is actionable under {1}. What are some potential defenses for this claim?";
        //                                                                                                                                                                 //const string BuildedQuery2000CharactersNoActionableDataPattern = "I have a potential claim {0} {1}. What are some potential defenses for this claim?";
        //                                                                                                                                                                 //const string BuildedQuery2000CharactersCommonLawPattern = "I have a potential claim for {0}. What are some potential defenses to this claim?";

        //    //string checkStatutoryOver2000BuildedNoActionableDataQuery = "Verify: Statutory builded query without actionable data over 2000 characters is as expected";
        //    string checkActionableDataLinkOpensADocumentPage = "Verify: Actionable data link opens a document page";
        //    //string checkStatutoryOver2000BuildedWithActionableDataQuery = "Verify: Statutory builded query with actionable data over 2000 characters is as expected";
        //    //string checkCommonLawOver2000BuildedQuery = "Verify: Common Law builded query over 2000 characters is as expected";
        //    //string checkConstitutionalOver2000BuildedQuery = "Verify: Constitutional builded query over 2000 characters is as expected";

        //    var coCounselChatAssistantDialog = new CoCounselChatAssistantDialog();

        //    coCounselChatAssistantDialog = coCounselChatAssistantDialog.MaximizeButton.Click<CoCounselChatAssistantDialog>();

        //    SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.Chat.ResearchActionButton.Displayed);

        //    coCounselChatAssistantDialog.Chat.QuestionTextbox.SendKeys(Question2000Characters);
        //    coCounselChatAssistantDialog = coCounselChatAssistantDialog.Chat.SubmitButton.Click<CoCounselChatAssistantDialog>();

        //    SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.Chat.JurisdictionButton.Displayed);

        //    var aiJurisdictionDialog = coCounselChatAssistantDialog.Chat.JurisdictionButton.Click<CoCounselClaimsExplorerJurisdictionDialog>();
        //    coCounselChatAssistantDialog = aiJurisdictionDialog.SelectJurisdictions(true, Jurisdiction.California, Jurisdiction.Federal).SaveButton.Click<CoCounselChatAssistantDialog>();

        //    coCounselChatAssistantDialog = coCounselChatAssistantDialog.Chat.SubmitButton.Click<CoCounselChatAssistantDialog>();
        //    SafeMethodExecutor.WaitUntil(() => !coCounselChatAssistantDialog.Chat.ProgressLabel.Displayed);

        //    var stateTabComponent = coCounselChatAssistantDialog.Chat.ClaimsExplorerQuestionAndAnswerItem.AnswerTabPanel.SetActiveTab<StateTabComponent>(ClaimsExplorerAnswerTab.State);

        //    var itemNoActionableData = stateTabComponent.Headings.First(item => !item.ActionableDataLink.Displayed);

        //    var title = itemNoActionableData.SubHeadings.First().HeadingLabel.Text;
        //    itemNoActionableData.FindDefensesLink.ScrollToElement();

        //    var aiAssistantPage = itemNoActionableData.FindDefensesLink.Click<AiAssistedResearchPage>();

        //    BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
        //    BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

        //    DriverExtensions.WaitForJavaScript();

        //    //Bug 2148890: CoCounsel Chat Assistant: Claims Explorer Skill: Incorrect citation mentioned for find defenses from CoCounsel
        //    //this.TestCaseVerify.IsTrue(
        //    //    checkStatutoryOver2000BuildedNoActionableDataQuery,
        //    //    this.ExtractQuestion(aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text).Equals(string.Format(BuildedQuery2000CharactersNoActionableDataPattern, "under", title)),
        //    //    "Statutory builded query without actionable data over 2000 characters is NOT as expected");

        //    BrowserPool.CurrentBrowser.CloseTab(AiAssistedResearchTab);
        //    BrowserPool.CurrentBrowser.ActivateTab(HomePageTab);

        //    foreach (var headingItem in stateTabComponent.Headings)
        //    {
        //        headingItem.HeadingAccordionButton.ScrollToElement();
        //        headingItem.HeadingAccordionButton.Click();
        //        if (headingItem.ActionableDataLink.Displayed)
        //        {
        //            break;
        //        }
        //    }

        //    var itemWithActionableData = stateTabComponent.Headings.First(item => item.ActionableDataLink.Displayed);
        //    var actionableDataTitle = itemWithActionableData.ActionableDataLink.Text;

        //    itemWithActionableData.ActionableDataLink.ScrollToElement();

        //    var documentPage = itemWithActionableData.ActionableDataLink.Click<EdgeCommonDocumentPage>();

        //    BrowserPool.CurrentBrowser.CreateTab(DocumentTab);
        //    BrowserPool.CurrentBrowser.ActivateTab(DocumentTab);

        //    this.TestCaseVerify.IsTrue(
        //        checkActionableDataLinkOpensADocumentPage,
        //        documentPage.IsDocumentLoaded(),
        //        "Actionable data link doesn't open a document page");

        //    BrowserPool.CurrentBrowser.CloseTab(DocumentTab);
        //    BrowserPool.CurrentBrowser.ActivateTab(HomePageTab);

        //    itemWithActionableData.FindDefensesLink.ScrollToElement();

        //    aiAssistantPage = aiAssistantPage = itemWithActionableData.FindDefensesLink.Click<AiAssistedResearchPage>();
        //    BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
        //    BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

        //    DriverExtensions.WaitForJavaScript();

        //    //Bug 2148890
        //    //this.TestCaseVerify.IsTrue(
        //    //    checkStatutoryOver2000BuildedWithActionableDataQuery,
        //    //    this.ExtractQuestion(aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text).Equals(string.Format(BuildedQuery2000CharactersActionableDataPattern, title, actionableDataTitle)),
        //    //    "Statutory builded query with actionable data over 2000 characters is NOT as expected");

        //    BrowserPool.CurrentBrowser.CloseTab(AiAssistedResearchTab);
        //    BrowserPool.CurrentBrowser.ActivateTab(HomePageTab);

        //    // Bug 2154719
        //    //aiAssistantPage = stateTabComponent.Headings.First(item => item.HeadingAccordionButton.Text.Equals("Common law")).HeadingAccordionButton.Click<AiAssistedResearchPage>();
        //    //title = stateTabComponent.Headings.First(item => item.HeadingAccordionButton.Text.Equals("Common law")).SubHeadings.First().HeadingLabel.Text;
        //    //aiAssistantPage =stateTabComponent.Headings.First(item => item.HeadingAccordionButton.Text.Equals("Common law")).FindDefensesLink.Click<AiAssistedResearchPage>();
        //    //BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
        //    //BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

        //    //DriverExtensions.WaitForJavaScript();

        //    //this.TestCaseVerify.IsTrue(
        //    //    checkCommonLawOver2000BuildedQuery,
        //    //    this.ExtractQuestion(aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text).Equals(string.Format(BuildedQuery2000CharactersCommonLawPattern, title)),
        //    //    "Common Law builded query over 2000 characters is NOT as expected");

        //    //BrowserPool.CurrentBrowser.CloseTab(AiAssistedResearchTab);
        //    //BrowserPool.CurrentBrowser.ActivateTab(HomePageTab);

        //    //SafeMethodExecutor.WaitUntil(() => stateTabComponent.Headings.First(item => item.HeadingAccordionButton.Text.Equals("Constitutional")).SubHeadings.First().HeadingLabel.Displayed);

        //    //aiAssistantPage = stateTabComponent.Headings.First(item => item.HeadingAccordionButton.Text.Equals("Constitutional")).HeadingAccordionButton.Click<AiAssistedResearchPage>();
        //    //title = stateTabComponent.Headings.First(item => item.HeadingAccordionButton.Text.Equals("Constitutional")).SubHeadings.First().HeadingLabel.Text;
        //    //aiAssistantPage = stateTabComponent.Headings.First(item => item.HeadingAccordionButton.Text.Equals("Constitutional")).FindDefensesLink.Click<AiAssistedResearchPage>();
        //    //BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
        //    //BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

        //    //DriverExtensions.WaitForJavaScript();

        //    //this.TestCaseVerify.IsTrue(
        //    //    checkConstitutionalOver2000BuildedQuery,
        //    //    this.ExtractQuestion(aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text).Equals(string.Format(BuildedQuery2000CharactersNoActionableDataPattern, "under", title)),
        //    //    "Constitutional builded query over 2000 characters is NOT as expected");
        //}

        ///// <summary>
        ///// Verify 'Find Defenses' links behavior for under 2K characters in CoCounsel Claims Explorer
        ///// Test Case: 2146972
        ///// </summary>
        //[TestMethod]
        //[TestCategory(CurrentTestCategory)]
        //[TestCategory(FeatureTestCategory)]
        //[TestCategory(TeamMatzekCategory)]
        //[TestProperty(EnvironmentConstants.NameOfEnvironmentId, "QED")]
        //public void CoCounselClaimsExplorerFindDefensesLinkUnder2KTest()
        //{
        //    const string QuestionUnder2000Characters = "My employer refused to allow me to serve as an election judge, and even docked my pay when I used vacation to do so My employer refused to allow me to serve as an election judge, and even docked my pay when I used vacation to do so My employer refused to allow me to serve as an election judge, and even docked my pay when I used vacation to do so My employer refused to allow me to serve as an election judge, and even docked my pay when I used vacation to do so My employer refused to allow me to serve as an election judge, and even docked my pay when I used vacation to do so My employer refused to allow me to serve as an election judge, and even docked my pay when I used vacation to do soMy employer refused to allow me to serve as an election judge, and even docked my pay when I used vacation to do soMy employer refused to allow me to serve as an election judge, and even docked my pay when I used vacation to do so My employer refused to allow me to serve as an election judge, and even docked my pay when I used vacation to do soMy employer refused to allow me to serve as an election judge, and even docked my pay when I used vacation to do soMy employer refused to allow me to serve as an election judge, and even docked my pay when I used vacation to do so My employer refused to allow me to serve as an election judge, and even docked my pay when I used vacation to do soMy employer refused to allow me to serve as an election judge, and even docked my pay when I used vacation to do so My employer refused to allow me to serve as an election judge, and even docked my pay when I used vacation to do so My employer refused to allow me to serve as an election judge, and even docked my pay when I used vacation to do soMy employer refused to allow me to serve as an election judge, and even docked my pay when I used vacation to do soMy employer refused to allow me to serve as an election judge, and even docked my pay when I used vacation to do soMy employer refused to allow me to serve as an election judge, and even docked my pay when I used vacation to do so.";
        //    //const string BuildedQueryUnder2000CharactersActionableDataPattern = "I have a potential claim under {0} based on this fact pattern: {1}. What are some potential defenses based on these facts? {2} is actionable under {3}, so please also include any defenses based on {4}.";
        //    //const string BuildedQueryUnder2000CharactersNoActionableDataPattern = "I have a potential claim {0} {1} based on this fact pattern: {2}. What are some potential defenses based on these facts?";

        //    //string checkStatutoryUnder2000BuildedNoActionableDataQuery = "Verify: Statutory builded query without actionable data under 2000 characters is as expected";
        //    //string checkStatutoryUnder2000BuildedWithActionableDataQuery = "Verify: Statutory builded query with actionable data under 2000 characters is as expected";
        //    //string checkCommonLawUnder2000BuildedQuery = "Verify: Common Law builded query under 2000 characters is as expected";
        //    //string checkConstitutionalUnder2000BuildedQuery = "Verify: Constitutional builded query under 2000 characters is as expected";
        //    string checkLimitConcurrentSearchesWarning = "Verify: Limit concurrent searches warning message is displayed for >3 parallel searches";

        //    var coCounselChatAssistantDialog = new CoCounselChatAssistantDialog();

        //    coCounselChatAssistantDialog = coCounselChatAssistantDialog.MaximizeButton.Click<CoCounselChatAssistantDialog>();

        //    SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.Chat.ResearchActionButton.Displayed);

        //    coCounselChatAssistantDialog.Chat.QuestionTextbox.SendKeys(QuestionUnder2000Characters);
        //    coCounselChatAssistantDialog = coCounselChatAssistantDialog.Chat.SubmitButton.Click<CoCounselChatAssistantDialog>();

        //    SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.Chat.JurisdictionButton.Displayed);

        //    var aiJurisdictionDialog = coCounselChatAssistantDialog.Chat.JurisdictionButton.Click<CoCounselClaimsExplorerJurisdictionDialog>();
        //    coCounselChatAssistantDialog = aiJurisdictionDialog.SelectJurisdictions(true, Jurisdiction.California, Jurisdiction.Federal).SaveButton.Click<CoCounselChatAssistantDialog>();

        //    coCounselChatAssistantDialog = coCounselChatAssistantDialog.Chat.SubmitButton.Click<CoCounselChatAssistantDialog>();
        //    SafeMethodExecutor.WaitUntil(() => !coCounselChatAssistantDialog.Chat.ProgressLabel.Displayed);

        //    var stateTabComponent = coCounselChatAssistantDialog.Chat.ClaimsExplorerQuestionAndAnswerItem.AnswerTabPanel.SetActiveTab<StateTabComponent>(ClaimsExplorerAnswerTab.State);

        //    var itemNoActionableData = stateTabComponent.Headings.First(item => !item.ActionableDataLink.Displayed);

        //    var title = itemNoActionableData.SubHeadings.First().HeadingLabel.Text;
        //    var aiAssistantPage = itemNoActionableData.FindDefensesLink.Click<AiAssistedResearchPage>();

        //    BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
        //    BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

        //    DriverExtensions.WaitForJavaScript();

        //    SafeMethodExecutor.WaitUntil(() => aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Displayed);

        //    //Bug 2148890: CoCounsel Chat Assistant: Claims Explorer Skill: Incorrect citation mentioned for find defenses from CoCounsel
        //    //this.TestCaseVerify.IsTrue(
        //    //    checkStatutoryUnder2000BuildedNoActionableDataQuery,
        //    //    this.ExtractQuestion(aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text).Equals(string.Format(BuildedQueryUnder2000CharactersNoActionableDataPattern, "under", title)),
        //    //    "Statutory builded query without actionable data over 2000 characters is NOT as expected");

        //    BrowserPool.CurrentBrowser.CloseTab(AiAssistedResearchTab);
        //    BrowserPool.CurrentBrowser.ActivateTab(HomePageTab);

        //    foreach (var headingItem in stateTabComponent.Headings)
        //    {
        //        headingItem.HeadingAccordionButton.ScrollToElement();
        //        headingItem.HeadingAccordionButton.Click();
        //        if (headingItem.ActionableDataLink.Displayed)
        //        {
        //            break;
        //        }
        //    }

        //    var itemWithActionableData = stateTabComponent.Headings.First(item => item.ActionableDataLink.Displayed);
        //    title = itemWithActionableData.SubHeadings.First().HeadingLabel.Text;

        //    var actionableDataTitle = itemWithActionableData.ActionableDataLink.Text;
        //    itemWithActionableData.FindDefensesLink.ScrollToElement();

        //    aiAssistantPage = itemWithActionableData.FindDefensesLink.Click<AiAssistedResearchPage>();
        //    BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
        //    BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

        //    DriverExtensions.WaitForJavaScript();

        //    //Bug 2148890
        //    //this.TestCaseVerify.IsTrue(
        //    //    checkStatutoryUnder2000BuildedWithActionableDataQuery,
        //    //    this.ExtractQuestion(aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text).Equals(string.Format(BuildedQueryUnder2000CharactersActionableDataPattern,title, QuestionUnder2000Characters, title, actionableDataTitle, actionableDataTitle)),
        //    //    "Statutory builded query with actionable data over 2000 characters is NOT as expected");

        //    BrowserPool.CurrentBrowser.CloseTab(AiAssistedResearchTab);
        //    BrowserPool.CurrentBrowser.ActivateTab(HomePageTab);

        //    // BUG 2154719
        //    //aiAssistantPage = stateTabComponent.Headings.First(item => item.HeadingAccordionButton.Text.Equals("Common law")).HeadingAccordionButton.Click<AiAssistedResearchPage>();
        //    //title = stateTabComponent.Headings.First(item => item.HeadingAccordionButton.Text.Equals("Common law")).SubHeadings.First().HeadingLabel.Text;
        //    //aiAssistantPage = stateTabComponent.Headings.First(item => item.HeadingAccordionButton.Text.Equals("Common law")).FindDefensesLink.Click<AiAssistedResearchPage>();
        //    //BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
        //    //BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

        //    //DriverExtensions.WaitForJavaScript();

        //    //this.TestCaseVerify.IsTrue(
        //    //    checkCommonLawUnder2000BuildedQuery,
        //    //    this.ExtractQuestion(aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text).Equals(string.Format(BuildedQueryUnder2000CharactersNoActionableDataPattern, title)),
        //    //    "Common Law builded query over 2000 characters is NOT as expected");

        //    //BrowserPool.CurrentBrowser.CloseTab(AiAssistedResearchTab);
        //    //BrowserPool.CurrentBrowser.ActivateTab(HomePageTab);

        //    //aiAssistantPage = stateTabComponent.Headings.First(item => item.HeadingAccordionButton.Text.Equals("Constitutional")).HeadingAccordionButton.Click<AiAssistedResearchPage>();
        //    //title = stateTabComponent.Headings.First(item => item.HeadingAccordionButton.Text.Equals("Constitutional")).SubHeadings.First().HeadingLabel.Text;
        //    //aiAssistantPage = stateTabComponent.Headings.First(item => item.HeadingAccordionButton.Text.Equals("Constitutional")).FindDefensesLink.Click<AiAssistedResearchPage>();
        //    //BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
        //    //BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

        //    //DriverExtensions.WaitForJavaScript();

        //    //this.TestCaseVerify.IsTrue(
        //    //    checkConstitutionalUnder2000BuildedQuery,
        //    //    this.ExtractQuestion(aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text).Equals(string.Format(BuildedQueryUnder2000CharactersNoActionableDataPattern, "under", title)),
        //    //    "Constitutional builded query over 2000 characters is NOT as expected");

        //    //BrowserPool.CurrentBrowser.CloseTab(AiAssistedResearchTab);
        //    //BrowserPool.CurrentBrowser.ActivateTab(HomePageTab);

        //    stateTabComponent.Headings.First().HeadingAccordionButton.Click();
        //    aiAssistantPage = stateTabComponent.Headings.First().FindDefensesLink.Click<AiAssistedResearchPage>();
        //    BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
        //    BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);
        //    DriverExtensions.WaitForJavaScript();
        //    BrowserPool.CurrentBrowser.CloseTab(AiAssistedResearchTab);
        //    BrowserPool.CurrentBrowser.ActivateTab(HomePageTab);

        //    stateTabComponent.Headings.ElementAt(1).HeadingAccordionButton.Click();
        //    aiAssistantPage = stateTabComponent.Headings.ElementAt(1).FindDefensesLink.Click<AiAssistedResearchPage>();
        //    BrowserPool.CurrentBrowser.CreateTab($"{AiAssistedResearchTab}Second");
        //    BrowserPool.CurrentBrowser.ActivateTab($"{AiAssistedResearchTab}Second");
        //    DriverExtensions.WaitForJavaScript();
        //    BrowserPool.CurrentBrowser.CloseTab($"{AiAssistedResearchTab}Second");
        //    BrowserPool.CurrentBrowser.ActivateTab(HomePageTab);

        //    stateTabComponent.Headings.ElementAt(2).HeadingAccordionButton.Click();
        //    aiAssistantPage = stateTabComponent.Headings.ElementAt(2).FindDefensesLink.Click<AiAssistedResearchPage>();
        //    BrowserPool.CurrentBrowser.CreateTab($"{AiAssistedResearchTab}Third");
        //    BrowserPool.CurrentBrowser.ActivateTab($"{AiAssistedResearchTab}Third");
        //    DriverExtensions.WaitForJavaScript();
        //    BrowserPool.CurrentBrowser.CloseTab($"{AiAssistedResearchTab}Third");
        //    BrowserPool.CurrentBrowser.ActivateTab(HomePageTab);

        //    stateTabComponent.Headings.ElementAt(3).HeadingAccordionButton.Click();
        //    aiAssistantPage = stateTabComponent.Headings.ElementAt(3).FindDefensesLink.Click<AiAssistedResearchPage>();
        //    BrowserPool.CurrentBrowser.CreateTab($"{AiAssistedResearchTab}Fourth");
        //    BrowserPool.CurrentBrowser.ActivateTab($"{AiAssistedResearchTab}Fourth");
        //    DriverExtensions.WaitForJavaScript();

        //    this.TestCaseVerify.AreEqual(
        //        checkLimitConcurrentSearchesWarning,
        //        "You can submit this query after one of your last AI searches receives a response.",
        //        stateTabComponent.Headings.ElementAt(3).FindDefensesConcurrentSearchesLimitInfobox.Text,
        //        "Limit concurrent searches warning message is NOT displayed for >3 parallel searches");
        //}

        /// <summary>
        /// Verify CoCounsel Chat Assistant: Quick Check Skill: MFE: Quotations: Cutover to real data, Clicking on the Document link opens the document page
        /// Test Case: 2157960, 2179669
        /// </summary>
        [TestMethod]
        [TestCategory(QuickCheckSkillTestCategory)]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        public void AiCoCounselQuickCheckCommonUploadDocumentLinkTest()
        {
            const string Question = "I want to check for quotations in a brief for accuracy?";
            const string FirstDocumentName = "FS59779.docx";
            const string SecondDocumentName = "2015_WL_3402577.docx";
            const string MoreThan30MbDocumentName = "PDFGreaterThan30LessThan35.pdf";
            const string DataSecurityAndPrivacyDialogContent = "Thomson Reuters is committed to maintaining the security of your data and employs appropriate security measures.\r\nThe provided file and the Identifying Mischaracterizations report are encrypted both in transit using HTTPS and at rest.\r\nThe analyzed file is solely processed by the Identifying Mischaracterizations skill engine to generate a report and is not used for any other purpose.\r\nThe provided file and the Identifying Mischaracterizations report are governed by CoCounsel 2.0 retention policies with the user maintaining full control over data deletion.\r\nThe data processed by the generative AI model is done using a zero-retention API and is labeled as Thomson Reuters, thus not revealing the user's identity.";

            string checkDataSecurityAndPrivacyDialog = "Verify: 'Data security and privacy' is displayed";
            string checkMoreThanOneFileError = "Verify: Error message for > 1 files is displayed";
            string checkMoreThan30MbFileError = "Verify: Error message for > 30 MB file is displayed";
            string checkPotentialMischaracterizationResults = "Verify: Potential mischaracterization results > 0";
            string checkQuickCheckReportCorrectlyDisplayed = "Verify: Quick check report is displayed";
            string checkQuickCheckTabTitle = "Verify: Quick check tab title is as per the option selected";
            string checkDocumentTitleLink = "Verify: Document title link click opens document page";
            string checkDocumentName = "Verify: Clicked Document is opened correclty";

            var coCounselChatAssistantDialog = new CoCounselChatAssistantDialog();

            coCounselChatAssistantDialog = coCounselChatAssistantDialog.MaximizeButton.Click<CoCounselChatAssistantDialog>();

            SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.Chat.QuestionTextbox.Displayed);

            coCounselChatAssistantDialog.Chat.QuestionTextbox.SendKeys(Question);
            coCounselChatAssistantDialog = coCounselChatAssistantDialog.Chat.SubmitButton.Click<CoCounselChatAssistantDialog>();

            SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.Chat.UploadDocumentButton.Displayed);

            var coCounselDataRetentionDialog = coCounselChatAssistantDialog.Chat.LearnAboutDataRetenionLink.Click<CoCounselDataRetentionDialog>();

            this.TestCaseVerify.AreEqual(
                checkDataSecurityAndPrivacyDialog,
                DataSecurityAndPrivacyDialogContent,
                coCounselDataRetentionDialog.ContentLabel.Text,
                "'Data security and privacy' is NOT displayed");

            coCounselChatAssistantDialog = coCounselDataRetentionDialog.CloseButton.Click<CoCounselChatAssistantDialog>();

            coCounselChatAssistantDialog.Chat.SelectUploadPath(QuickCheckOptions.CheckYourWork);

            var coCounselQuickCheckUploadDocumentDialog = coCounselChatAssistantDialog.Chat.UploadDocumentButton.Click<CoCounselQuickCheckUploadDocumentDialog>();

            coCounselQuickCheckUploadDocumentDialog = coCounselQuickCheckUploadDocumentDialog.UploadFile<CoCounselQuickCheckUploadDocumentDialog>(Path.Combine(TestDocsFolderPath, FirstDocumentName));

            SafeMethodExecutor.WaitUntil(() => coCounselQuickCheckUploadDocumentDialog.UploadFileTextbox.Enabled);
            SafeMethodExecutor.WaitUntil(() => coCounselQuickCheckUploadDocumentDialog.DoneButton.Enabled);

            coCounselQuickCheckUploadDocumentDialog = coCounselQuickCheckUploadDocumentDialog.UploadFile<CoCounselQuickCheckUploadDocumentDialog>(Path.Combine(TestDocsFolderPath, SecondDocumentName));

            var errorMessage = coCounselChatAssistantDialog.Chat.QuickCheckUploadErrorLabel.Text;

            this.TestCaseVerify.AreEqual(
                checkMoreThanOneFileError,
                "Only one file can be analyzed at a time. Remove all but the desired file.",
                errorMessage.Remove(errorMessage.IndexOf("\r")),
                "Error message for > 1 files is NOT displayed");

            SafeMethodExecutor.WaitUntil(() => coCounselQuickCheckUploadDocumentDialog.UploadFileTextbox.Enabled);
            SafeMethodExecutor.WaitUntil(() => coCounselQuickCheckUploadDocumentDialog.DoneButton.Enabled);

            coCounselChatAssistantDialog = coCounselQuickCheckUploadDocumentDialog.DoneButton.Click<CoCounselChatAssistantDialog>();
            
            coCounselChatAssistantDialog.Chat.CoCounselQuickCheckFileChipItems.ToList().ForEach(item => item.RemoveButton.Click());

            coCounselQuickCheckUploadDocumentDialog = coCounselChatAssistantDialog.Chat.UploadDocumentButton.Click<CoCounselQuickCheckUploadDocumentDialog>();

            coCounselQuickCheckUploadDocumentDialog = coCounselQuickCheckUploadDocumentDialog.UploadFile<CoCounselQuickCheckUploadDocumentDialog>(Path.Combine(TestDocsFolderPath, MoreThan30MbDocumentName));

            SafeMethodExecutor.WaitUntil(() => coCounselQuickCheckUploadDocumentDialog.UploadFileTextbox.Enabled);
            SafeMethodExecutor.WaitUntil(() => coCounselQuickCheckUploadDocumentDialog.DoneButton.Enabled);

            coCounselChatAssistantDialog = coCounselQuickCheckUploadDocumentDialog.DoneButton.Click<CoCounselChatAssistantDialog>();

            this.TestCaseVerify.AreEqual(
                checkMoreThan30MbFileError,
                "PDF file size exceeds 30 MB limit",
                coCounselChatAssistantDialog.Chat.QuickCheckFileErrorStatusLabel.GetAttribute("message"),
                "Error message for > 30 MB file is NOT displayed");

            coCounselChatAssistantDialog.Chat.CoCounselQuickCheckFileChipItems.ToList().ForEach(item => item.RemoveButton.Click());

            coCounselQuickCheckUploadDocumentDialog = coCounselChatAssistantDialog.Chat.UploadDocumentButton.Click<CoCounselQuickCheckUploadDocumentDialog>();

            coCounselQuickCheckUploadDocumentDialog = coCounselQuickCheckUploadDocumentDialog.UploadFile<CoCounselQuickCheckUploadDocumentDialog>(Path.Combine(TestDocsFolderPath, FirstDocumentName));

            SafeMethodExecutor.WaitUntil(() => coCounselQuickCheckUploadDocumentDialog.UploadFileTextbox.Enabled);
            SafeMethodExecutor.WaitUntil(() => coCounselQuickCheckUploadDocumentDialog.DoneButton.Enabled);

            coCounselChatAssistantDialog = coCounselQuickCheckUploadDocumentDialog.DoneButton.Click<CoCounselChatAssistantDialog>();

            coCounselChatAssistantDialog = coCounselChatAssistantDialog.Chat.StartAiAssistedResearchButton.Click<CoCounselChatAssistantDialog>();

            SafeMethodExecutor.WaitUntil(() => !coCounselChatAssistantDialog.Chat.QuickCheckProgressSpinnerLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkPotentialMischaracterizationResults,
                coCounselChatAssistantDialog.Chat.CoCounselQuickCheckAnswerItems.Count > 0,
                "Potential mischaracterization results equal 0");

            coCounselChatAssistantDialog.Chat.QuickCheckReportLink.ScrollToElement();
            var reportPage = coCounselChatAssistantDialog.Chat.QuickCheckReportLink.Click<QuickCheckRecommendationsPage>();

            if (this.DefaultSignOnContext.TestEnvironment.Name.ToLower().Contains(CobaltEnvironment.DEMO.GetName().ToLower()))
            {
                BrowserPool.CurrentBrowser.CreateTab(HomePageTab);
                BrowserPool.CurrentBrowser.ActivateTab(HomePageTab);

                this.Settings.AppendValues(EnvironmentConstants.NameOfEnvironmentId, SettingUpdateOption.Overwrite, CobaltEnvironment.QED.GetName());
                this.SignOnBack().ClickContinueButton<PrecisionHomePage>();

                BrowserPool.CurrentBrowser.CloseTab(HomePageTab);
                BrowserPool.CurrentBrowser.ActivateTab(HomePageTab);

                reportPage = coCounselChatAssistantDialog.Chat.QuickCheckReportLink.Click<QuickCheckRecommendationsPage>();
            }

            BrowserPool.CurrentBrowser.CreateTab(QuickCheckTab);
            BrowserPool.CurrentBrowser.ActivateTab(QuickCheckTab);

            DriverExtensions.WaitForAnimation();

            this.TestCaseVerify.IsTrue(
                checkQuickCheckTabTitle,
                BrowserPool.CurrentBrowser.Title.Contains("Quick Check Your Work Results"),
                "Quick check tab is incorrect");

            this.TestCaseVerify.IsTrue(
                checkQuickCheckReportCorrectlyDisplayed,
                reportPage.ReportTabsPanel.ReportPageLabel.Displayed,
                "Quick check report is not displayed while navigating from CoCounsel chat");

            BrowserPool.CurrentBrowser.CloseTab(QuickCheckTab);
            BrowserPool.CurrentBrowser.ActivateTab(HomePageTab);
            
            coCounselChatAssistantDialog.Chat.CoCounselQuickCheckAnswerItems.First().AnswerItemExpandButton.Click();
            var documentName = coCounselChatAssistantDialog.Chat.CoCounselQuickCheckAnswerItems.First().DocumentLink.Text;
            var documentPage = coCounselChatAssistantDialog.Chat.CoCounselQuickCheckAnswerItems.First().DocumentLink.Click<PrecisionCommonDocumentPage>();

            BrowserPool.CurrentBrowser.CreateTab(DocumentTab);
            BrowserPool.CurrentBrowser.ActivateTab(DocumentTab);

            this.TestCaseVerify.IsTrue(
                checkDocumentTitleLink,
                documentPage.IsDocumentLoaded(),
                "Click on Document link doesn't open document page");

            this.TestCaseVerify.IsTrue(
                checkDocumentName,
                documentName.Contains(documentPage.GetDocumentTitle()),
                "Document is not opened correctly");
        }

        /// <summary>
        /// Verify zero state message for CoCounsel integrated Complaint analyser
        /// Test case: 2239913
        /// </summary>
        [TestMethod]
        [TestCategory(ComplaintAnalysisSkillTestCategory)]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestProperty("AIChatAssistantServiceConfig", "DEV5")]
        public void AiCoCounselComplaintAnalyserZeroStateTest()
        {
            const string TriggerQuestion = "I would like to analyse the complaint";
            const string ZeroComplaintDocument = "Complaint-Zero State.docx";

            string checkZeroStateMessage = "Verify: Zero state message is displayed when document has no complaints";

            var coCounselChatAssistantDialog = new CoCounselChatAssistantDialog();

            coCounselChatAssistantDialog = coCounselChatAssistantDialog.MaximizeButton.Click<CoCounselChatAssistantDialog>();

            SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.Chat.QuestionTextbox.Displayed);

            coCounselChatAssistantDialog.Chat.QuestionTextbox.SendKeys(TriggerQuestion);
            coCounselChatAssistantDialog = coCounselChatAssistantDialog.Chat.SubmitButton.Click<CoCounselChatAssistantDialog>();

            SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.Chat.UploadDocumentButton.Displayed);

            var coCounselQuickCheckUploadDocumentDialog = coCounselChatAssistantDialog.Chat.UploadDocumentButton.Click<CoCounselQuickCheckUploadDocumentDialog>();

            coCounselQuickCheckUploadDocumentDialog = coCounselQuickCheckUploadDocumentDialog.UploadFile<CoCounselQuickCheckUploadDocumentDialog>(Path.Combine(TestDocsFolderPath, ZeroComplaintDocument));

            SafeMethodExecutor.WaitUntil(() => coCounselQuickCheckUploadDocumentDialog.UploadFileTextbox.Enabled);
            SafeMethodExecutor.WaitUntil(() => coCounselQuickCheckUploadDocumentDialog.DoneButton.Enabled);

            coCounselChatAssistantDialog = coCounselQuickCheckUploadDocumentDialog.DoneButton.Click<CoCounselChatAssistantDialog>();

            coCounselChatAssistantDialog = coCounselChatAssistantDialog.Chat.StartAnalysisButton.Click<CoCounselChatAssistantDialog>();

            SafeMethodExecutor.WaitUntil(() => !coCounselChatAssistantDialog.Chat.ComplaintAnalysisProgressLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkZeroStateMessage,
                coCounselChatAssistantDialog.Chat.ZeroStateMessageLabel.Displayed,
                "Zero state message for no complaints is not displayed");

        }

        /// <summary>
        /// Task # 2234381, 2236822, 2238574
        /// Verify complaint analysis skill
        /// Verify uploaded document is giving results from the uploaded document
        /// Verify existing complaint from CoCounsel chat history is loading the earlier results
        /// </summary>
        [TestMethod]
        [TestCategory(ComplaintAnalysisSkillTestCategory)]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        public void AiCoCounselComplaintAnalysisTest()
        {
            const string Question = "I would like to analyze the complaint";
            const string SecondQuestion = "I would like to run Identifying Citation Issues.";
            string checkStartAnalysisButtonDisplayed = "Verify: Start Analysis Button is Displayed";
            string checkStartAnalysisProgress = "Verify: Start Analysis Button click is progressing to load the results";
            string checkSummaryDetails = "Verify: Summary tab displays information as per uploaded document";
            string checkClaimsTabCardHeadingsAfterDocumentUpload = "Verify: All claims tab cards contains expected section headings";
            string checkEventsTabSectionsForDocumentUpload = "Verify: Document Upload - Events tab cards contains all sections";
            string checkDefensesTabSectionsForDocumentUpload = "Verify: Document Upload - Defenses tab cards contains all sections";

            const string ComplaintDocumentName = "2023 WL 9108365.docx";
            var documentPath = Path.Combine(TestDocsFolderPath, ComplaintDocumentName);

            var expectedHeadingTitlesList = new List<string> { "Citation:", "Description:", "Parties involved:", "Related facts:", "Relief sought:" };

            var coCounselChatAssistantDialog = new CoCounselChatAssistantDialog();

            coCounselChatAssistantDialog = coCounselChatAssistantDialog.MaximizeButton.Click<CoCounselChatAssistantDialog>();

            SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.Chat.QuestionTextbox.Displayed);

            coCounselChatAssistantDialog.Chat.QuestionTextbox.SendKeys(Question);
            coCounselChatAssistantDialog = coCounselChatAssistantDialog.Chat.SubmitButton.Click<CoCounselChatAssistantDialog>();

            SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.Chat.UploadDocumentButton.Displayed);

            var coCounselComplaintAnalysisUploadDocumentDialog = coCounselChatAssistantDialog.Chat.UploadDocumentButton.Click<CoCounselComplaintAnalysisUploadDocumentDialog>();

            coCounselComplaintAnalysisUploadDocumentDialog = coCounselComplaintAnalysisUploadDocumentDialog.UploadFile<CoCounselComplaintAnalysisUploadDocumentDialog>(documentPath);

            //Get text from the uploaded file for comparison
            Clipboard.SetText(DocxTextExtractor.ExtractTextFromWord(documentPath));
            var fileTextForCompare = this.CleanText(Clipboard.GetText());

            SafeMethodExecutor.WaitUntil(() => coCounselComplaintAnalysisUploadDocumentDialog.UploadFileTextbox.Enabled);
            SafeMethodExecutor.WaitUntil(() => coCounselComplaintAnalysisUploadDocumentDialog.DoneButton.Enabled);

            coCounselChatAssistantDialog = coCounselComplaintAnalysisUploadDocumentDialog.DoneButton.Click<CoCounselChatAssistantDialog>();

            this.TestCaseVerify.IsTrue(
                checkStartAnalysisButtonDisplayed,
                coCounselChatAssistantDialog.Chat.StartAnalysisButton.Displayed,
                "Start Analysis Button is NOT Displayed");

            coCounselChatAssistantDialog = coCounselChatAssistantDialog.Chat.StartAnalysisButton.Click<CoCounselChatAssistantDialog>();
           
            this.TestCaseVerify.IsTrue(
                checkStartAnalysisProgress,
                coCounselChatAssistantDialog.Chat.ComplaintAnalysisProgressLabel.Displayed,
                "Start Analysis Button click is NOT progressing to load the results");

            SafeMethodExecutor.WaitUntil(() => !coCounselChatAssistantDialog.Chat.ComplaintAnalysisProgressLabel.Displayed);

            //Verify uploaded document is giving results from the uploaded document
            var summaryTabPanel = coCounselChatAssistantDialog.Chat.CoCounselComplaintAnalysisResultItem.ComplaintAnalyzerResultTabPanel.SetActiveTab<SummaryTab>(ComplaintAnalyzerResultTabs.Summary);

            foreach(IButton button in summaryTabPanel.ChatResultExpandButtons)
            {
                button.Click();
            }

            summaryTabPanel.FillingInfoCardCaseNumberLabel.ScrollToElement();

            var caseNumber = summaryTabPanel.FillingInfoCardCaseNumberLabel.Text.Replace("Case Number: ", string.Empty).ToLower().ToString();
            var caption = this.CleanText(summaryTabPanel.FillingInfoCardCaptionLabel.Text);

            var plantiffFullString = summaryTabPanel.PartiesCardPlaintifLabel.Text.Split('(');
            var plaintiff = this.CleanText(plantiffFullString[0]);
            var defendantsList = summaryTabPanel.PartiesCardDefendantsLabels.Select(label => this.CleanText(label.Text)).ToList();

            var keyClaimsList = summaryTabPanel.KeyClaimsLabels.Select(label => this.CleanText(label.Text)).ToList();

            this.TestCaseVerify.IsTrue(
                checkSummaryDetails,
                fileTextForCompare.Contains(caseNumber)
                && fileTextForCompare.Contains(caption)
                && fileTextForCompare.Contains(plaintiff)
                && defendantsList.All(item => fileTextForCompare.Contains(item))
                && keyClaimsList.Count > 0,
                "Summary tab does NOT contain summary of the uploaded file");

            var claimsTab = coCounselChatAssistantDialog.Chat.CoCounselComplaintAnalysisResultItem.ComplaintAnalyzerResultTabPanel.SetActiveTab<ClaimsTab>(ComplaintAnalyzerResultTabs.Claims);

            foreach (IButton button in claimsTab.ChatResultExpandButtons)
            {
                button.Click();
            }

            claimsTab.ChatResultExpandButtons.First().ScrollToElement();

            var claimCardSectionTitles = claimsTab.ResultList.ClaimsCardSectionLabels.Select(item => item.Text).ToList();

            this.TestCaseVerify.IsTrue(
                checkClaimsTabCardHeadingsAfterDocumentUpload,
                claimCardSectionTitles.Distinct().All(items => expectedHeadingTitlesList.Contains(items))
                && claimsTab.ResultList.ClaimsCardPlantiffLabels.Count > 0
                && claimsTab.ResultList.ClaimsCardDefendantLabels.Count > 0
                && claimsTab.ResultList.RelatedFactsShortDescriptionLabels.Count > 0
                && claimsTab.ResultList.RelatedFactsLongDescriptionLabels.Count > 0
                && !claimsTab.ResultList.ReliefSoughtLabels.Any(item => item.Text.Contains("§")),
                "Claims tab cards does NOT contain all sections");

            var eventTabPanel = coCounselChatAssistantDialog.Chat.CoCounselComplaintAnalysisResultItem.ComplaintAnalyzerResultTabPanel.SetActiveTab<EventTimelineTab>(ComplaintAnalyzerResultTabs.EventTimeline);

            foreach (IButton button in eventTabPanel.ChatResultExpandButtons)
            {
                button.Click();
            }

            eventTabPanel.ChatResultExpandButtons.First().ScrollToElement();

            this.TestCaseVerify.IsTrue(
                checkEventsTabSectionsForDocumentUpload,
                eventTabPanel.ResultList.EventsCardPlantiffLabels.Count > 0
                && eventTabPanel.ResultList.EventsCardDefendantLabels.Count > 0,
                "Document Upload - Events tab cards does NOT contain all sections");

            var defensesTabPanel = coCounselChatAssistantDialog.Chat.CoCounselComplaintAnalysisResultItem.ComplaintAnalyzerResultTabPanel.SetActiveTab<DefensesTab>(ComplaintAnalyzerResultTabs.Defenses);

            foreach (IButton button in defensesTabPanel.ChatResultExpandButtons)
            {
                button.Click();
            }

            defensesTabPanel.ChatResultExpandButtons.First().ScrollToElement();

            this.TestCaseVerify.IsTrue(
                checkDefensesTabSectionsForDocumentUpload,
                defensesTabPanel.ResultList.DefensesCardLabels.Count > 0
                && defensesTabPanel.ResultList.DefensesCardTableLabels.Count > 0
                && defensesTabPanel.ResultList.DefensesCardOverviewSectionLabels.Count > 0,
                "Document Upload - Defenses tab cards does NOT contain all sections");

            // Verify existing complaint from CoCounsel chat history is loading the earlier results
            coCounselChatAssistantDialog = coCounselChatAssistantDialog.MenuButton.Click<CoCounselChatAssistantDialog>();

            coCounselChatAssistantDialog = coCounselChatAssistantDialog.CoCounselRecentChatHistory.NewChatDropdown.SelectOption<CoCounselChatAssistantDialog>(NewChatOptions.NewChat);

            SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.Chat.QuestionTextbox.Displayed);

            coCounselChatAssistantDialog.Chat.QuestionTextbox.SendKeys(SecondQuestion);
            coCounselChatAssistantDialog = coCounselChatAssistantDialog.Chat.SubmitButton.Click<CoCounselChatAssistantDialog>();

            SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.Chat.UploadDocumentButton.Displayed);

            coCounselChatAssistantDialog = coCounselChatAssistantDialog.CoCounselRecentChatHistory.CoCounselRecentChatItems.First(item => item.ChatLabel.Text.Contains("Complaint")).RecentChatButton.Click<CoCounselChatAssistantDialog>();

            SafeMethodExecutor.WaitUntil(() => !coCounselChatAssistantDialog.Chat.ComplaintAnalysisProgressLabel.Displayed);

            //Verify uploaded document is giving results from the uploaded document
            summaryTabPanel = coCounselChatAssistantDialog.Chat.CoCounselComplaintAnalysisResultItem.ComplaintAnalyzerResultTabPanel.SetActiveTab<SummaryTab>(ComplaintAnalyzerResultTabs.Summary);

            foreach (IButton button in summaryTabPanel.ChatResultExpandButtons)
            {
                button.Click();
            }

            this.TestCaseVerify.IsTrue(
                checkSummaryDetails,
                fileTextForCompare.Contains(caseNumber)
                && fileTextForCompare.Contains(caption)
                && fileTextForCompare.Contains(plaintiff)
                && defendantsList.All(item => fileTextForCompare.Contains(item))
                && keyClaimsList.Count > 0,
                "Summary tab does NOT contain summary of the uploaded file - From Chat History");
        }

        protected override void OnManageCredential()
        {
            if (this.TestContext.Properties["OutOfPlanUser"] != null && this.TestContext.Properties["OutOfPlanUser"].Equals("On"))
            {
                var userCredential = new UserDbCredential(
                    this.TestContext,
                    PasswordVertical.WlnGrowth,
                    "IndigoPremiumOOP")
                { ClientId = "OopAalpTest" };

                CredentialPool.RegisterUser(userCredential);

                this.DefaultSignOnContext = new WlnSignOnContext<IUserInfo>(this.TestExecutionContext, userCredential.ToWlnUserInfo());
            }
            else
            {
                base.OnManageCredential();
            }
        }

        protected override void InitializeRoutingPageSettings()
        {
            base.InitializeRoutingPageSettings();

            this.Settings.AppendValues(
                EnvironmentConstants.FeatureAccessControlsOn,
                SettingUpdateOption.Append,
                FeatureAccessControl.PLCoCounselAIAssistant);

            if (this.TestContext.Properties["AIChatAssistantServiceConfig"] != null && this.TestContext.Properties["AIChatAssistantServiceConfig"].Equals("On"))
            {
                this.Settings.AppendValues(
                    EnvironmentConstants.AIChatAssistantServiceConfig,
                    SettingUpdateOption.Append,
                    "DEV10");
            }

            if (this.TestContext.Properties["AIChatAssistantServiceConfig"] != null)
            {
                this.Settings.AppendValues(
                    EnvironmentConstants.AIChatAssistantServiceConfig,
                    SettingUpdateOption.Append,
                    this.TestContext.Properties["AIChatAssistantServiceConfig"]);
            }
            
            if (this.TestContext.TestName.Equals("AiCoCounselComplaintAnalysisTest"))
            {
                this.Settings.AppendValues(
                    EnvironmentConstants.AIChatAssistantServiceConfig,
                    SettingUpdateOption.Append,
                    "DEV5");
            }
            
            if (this.TestContext.Properties[EnvironmentConstants.NameOfEnvironmentId] != null && this.TestContext.Properties[EnvironmentConstants.NameOfEnvironmentId].Equals("QED"))
            {
                this.Settings.AppendValues(
                    EnvironmentConstants.NameOfEnvironmentId,
                    SettingUpdateOption.Overwrite,
                    CobaltEnvironment.QED.GetName());
            }
        }

        private string CleanText(string text)
        {
            text = text.Replace("dba:", string.Empty).Replace("violations", "violation").Replace("commonlaw", string.Empty).Replace("violationof", string.Empty).Replace("-lethalnegligence", string.Empty).Replace("-non", string.Empty).Replace(" ", string.Empty).Replace(")", string.Empty).Replace("\r\n", string.Empty).Replace("(", string.Empty).Replace(")", string.Empty).Replace(".", string.Empty).Replace(",", string.Empty).ToLower();

            return text;
        }

        private string CleanTextForCompare(string text) => text.Replace(" ", string.Empty).Replace(" ", string.Empty).Replace("(", string.Empty).Replace(")", string.Empty).Replace(" ", string.Empty).Replace("\r\n", string.Empty).Replace("’", string.Empty).Replace("'", string.Empty).Replace("§ ", string.Empty);
    }
}