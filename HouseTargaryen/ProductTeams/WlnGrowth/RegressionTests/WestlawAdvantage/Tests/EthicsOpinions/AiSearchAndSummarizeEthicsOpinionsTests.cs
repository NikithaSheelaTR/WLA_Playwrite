namespace WestlawAdvantage.Tests.EthicsOpinions
{
    using System;
    using System.Globalization;
    using System.Linq;
    using Framework.Common.UI.Products.Shared.Components.HomePage.Browse;
    using Framework.Common.UI.Products.Shared.Dialogs.Delivery;
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Products.Shared.Enums.Delivery;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.Header;
    using Framework.Common.UI.Products.WestlawEdge.Pages.SearchResult.ContentTypeSearchResultPages;
    using Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.AALP;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages.AALP;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Header;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Toolbars;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.CommonTypes.Constants;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.Utils.Execution;
    using Framework.Core.Utils.Extensions;
    using Framework.Core.Utils.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Ai Search And Summarize Ethics Tests
    /// </summary>
    [TestClass]
    public class AiSearchAndSummarizeEthicsOpinionsTests : WlaBaseTest
    {
        private const string FeatureTestCategory = "AiSearchAndSummarizeEthics";
        protected const string TeamMatzekCategory = "AalpMatzekTests";

        private const string EthicsContentNameFromSecondarySources = "Ethics & Professional Responsibility";
        private const string EthicsContentNameFromAdministrativeDecisionsGuidance = "Ethics & Disciplinary Opinions";
        private const string SearchAndSummarizeEthics = "Search & Summarize Ethics";
        private const string SearchAndSummarizeEthicsLabel = "Search & SummarizeABA and State Ethics Opinions";
        private const string SearchQuery = "ABA Ethics Opinions";
        private const string CategoryPageLabelTextInToolbar = "ABA and State Ethics Opinions";

        /// <summary>
        /// Task # 2258423, 2258226, 2258224, 2258217
        /// Verify Ethics's access point from 'Secondary sources' and 'Administrative Decisions & Guidance'
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        public void AiSearchAndSummarizeEthicsCommonTest()
        {
            const string HowAiWorksDialogHeader = "How Search & Summarize Ethics works";
            const string HowAIWorksDialogDescription = "Search&SummarizeEthicsuseslargelanguagemodels-atypeofgenerativeAI-andfocusesthemodelsonlyonABAEthicsMaterialsandstateAdministrativeMaterials.Portionsfromthesedocumentsareusedintheresponsesandmayincludetheactuallanguagefromthesource.Linksareincludedtoreadthefulltextofthesourcedocuments.Evenwiththeseandotherprecautions,Search&SummarizeEthicscanoccasionallyproduceinaccuracies,soitshouldalwaysbeusedaspartofaresearchprocessinconnectionwithadditionalresearchtofullyunderstandthenuanceoftheissuesandfurtherimproveaccuracy.TheAI-generatedsummaryabovethelistofdocumentscanbeextraordinarilyusefulforgettinganoverviewoftheissuesandpointers,butitshouldneverbeusedtoadviseaclient,writeabrieformotionforacourt,orotherwisebereliedonwithoutdoingfurtherresearch.Useittoacceleratethoroughresearch.Don'tuseitasareplacementforthoroughresearch.VisittheAICourtRulespagetoreviewthecourtrules,ordersthatupdatecourtrules,proposedandadoptedlegislation,andselectordersfromindividualjudgesthataddresstheuseofartificialintelligenceinlegalresearchanddrafting.PleaseconsultallapplicablerulesofpracticepriortofilingdocumentsthatrelyonAI-basedtools.ReviewAICourtRulesandOrdersopensinanewtab";
            const string TipsForBestResultsDialogDescription = "WriteaqueryfocusedonthecontentyouwouldexpecttofindinABAandstateethicsopinionsTheethicsopinionspublishedbytheABAandstateadministrativeagenciesaretheonlycontentbeingusedtogeneratearesponsetoaquery.Therefore,avoidqueriesaboutcontentoutsideofthescopeofthatcoverage,suchasthepublisheddecisionsfromstatecourtsorstatutoryquestions.IncludejurisdictioninyourqueryIncludethespecificjurisdictionyouwanttosearchinyourquery.Youmayspecifyuptothreejurisdictions.Forexample,ifyouwanttosearchCaliforniaethicsopinions,include\"California\"inyourquery.Ifaspecificjurisdictionisnotincludedinyourquery,thenonlyABAethicsopinionswillbesearched.Query:Cananattorneysueaclientfornonpayment?Betterquery:CananattorneysueaclientfornonpaymentinCalifornia?Writeaclear,concise,andfocusedquerythatis1-2sentenceslongConsideraddinginformation,suchasmaterialfacts,theruleatissue,orlookingatspecificcontenttoprovidebettercontextforthequery.Query:Ineedtowithdrawfromrepresentingmyclientandneedtomakesuretherearenoadversematerialeffectstomyclient’scase?WhatarethereasonsIcanandcannotwithdraw?Betterquery:WhatmaterialadverseeffectspreventpermissivewithdrawalunderABAModelRule1.16(b)?ProviderelevantfactsDonotincludeextrabackgroundfactsthatarenotmaterialtothequestion.Eachterminaqueryshouldbeessentialtofindingrelevantinformation.Ifjurisdictionisimportant,provideitinthequery.Query:Myclientpaidaretainerof$5,000forfutureservicesbutIdonotknowwhataccountIhavetokeepitinandwhencanIstarttopaymyself?IamlocatedinMinnesota.Betterquery:HowtohandleanddisperseadvancedpaymentsforservicesinMinnesota?Donotwriteaqueryinthestyleofaprompt,command,orinstructionWehavedonethepromptingandinstructionworkforyouandhavedesignedSearchandSummarizetoworkbestwithlegalresearchqueriesthatclearlyidentifytheinformationyouwouldliketofind.Forexample,itisbettertoavoidincludingextrawordsorphraseslike\"findallmaterialsthat…\"whichbookendthequerybutarenotsubstantive.Instead,itisbettertoconstructaquerywithwordsorphrasesyouwouldexpecttofindinadministrativeorsecondarysourcedocuments.Query:Youareanexperiencedattorneywhoisanexpertlegalresearcher,andyouneedtofindandsummarizethebestethicsdocumentsthatdiscussdiscriminationinthejuryselectionprocess.Betterquery:Whatconstitutesdiscriminationinthejuryselectionprocess?FocusonasingleissueorquestionWhereitiseasytodistinguishseparateissues,askaboutthoseissuesinseparatequestionsdedicatedtotheissues,ratherthancombiningthem.Query:Mayanactivejudgeconcurrentlyserveonamilitarycommission,oralternatively,serveonajointcommissiononpublichealth?Betterquery:Mayanactivejudgeconcurrentlyserveonamilitarycommission?Betterquery:Mayanactivejudgeconcurrentlyserveonajointcommissiononpublichealth?AvoidincludingpropernamesofpeopleorpartiesUsingpropernamescanartificiallyfocusthesearchonresultsthatmayonlybemoderatelyrelevantbutcontainthesamename.Query:IsMaryJohnson,theownerofasmalllawfirmcalledJohnsonLawOffice,requiredtoprovideprobonoservicesinNewYork?Betterquery:AreattorneysrequiredtoprovideprobonoservicesinNewYork?";

            string checkSecondarySourcesTipsForBestResultsDialogDescription = "Verify: 'Tips For Best Results' Dialogue description is as expected when opened from Secondary Sources";
            string checkSecondarySourcesTipsForBestResultsDialogDescriptionFromToolbar = "Verify: 'Tips For Best Results' Dialogue description in the Toolbar is as expected when opened from Secondary Sources";
            string checkAiAssistantFeatureHeaderAndDescription = "Verify: 'How AI Works' Title and descrition are as expected";
            string checkAiAssistantFeatureHeaderAndDescriptionFromChatArea = "Verify: 'How AI Works' Title and description in the chat Area are as expected";
            string checkSearchSummaryButtonDisplayed = "Verify: How Search & Summarize Ethics button is displayed as expected";
            string checkSearchSummaryEthicsLandingPageContentText = "Verify: Search & Summarize Ethics Landing Page header, label and label content is correct";
            string checkSearchSummaryEthicsContentText = "Verify: Search & Summarize Ethics Category Page label text is correct";
            string checkTipsForBestResultsDialogDescription = "Verify: 'Tips For Best Results' Dialogue description is as expected";
            string checkTipsForBestResultsDialogDescriptionFromToolbar = "Verify: 'Tips For Best Results' Dialogue description in the Toolbar is as expected";

            var homePage = this.GetHomePage<PrecisionHomePage>();

            //Ethics Opinions Commentaries
            var secondarySourcesContentTypePage = homePage.BrowseTabPanel.SetActiveTab<AllContentTabPanel>(PrecisionBrowseTab.ContentTypes)
                .ClickBrowseCategory<EdgeContentTypeSearchResultPage>(ContentType.SecondarySources);

            var aiAssistantPage = secondarySourcesContentTypePage.ClickLinkByText<AiAssistedResearchPage>(SearchAndSummarizeEthics);

            //Checking the first Tips For Best Results link opened from SecondarySources under Tools & Resources
            var tipsForBestResultsDialog = aiAssistantPage.Chat.TipsForBestResultLink.Click<TipsForBestResultsDialog>();

            var v = this.RemoveNewLinesAndExtraSpaces(tipsForBestResultsDialog.DescriptionLabel.Text);

            this.TestCaseVerify.IsTrue(
                checkSecondarySourcesTipsForBestResultsDialogDescription,
                this.RemoveNewLinesAndExtraSpaces(tipsForBestResultsDialog.DescriptionLabel.Text).Equals(TipsForBestResultsDialogDescription),
                "'Tips For Best Results' Dialogue description is NOT as expected when opened from SecondarySources");

            aiAssistantPage = tipsForBestResultsDialog.CloseButton.Click<AiAssistedResearchPage>();

            //checking the second Tips For Best Results link in the Toolbar opened from SecondarySources under Tools & Resources
            tipsForBestResultsDialog = aiAssistantPage.Toolbar.TipsBestResultsButton.Click<TipsForBestResultsDialog>();

            this.TestCaseVerify.IsTrue(
                  checkSecondarySourcesTipsForBestResultsDialogDescriptionFromToolbar,
                this.RemoveNewLinesAndExtraSpaces(tipsForBestResultsDialog.DescriptionLabel.Text).Equals(TipsForBestResultsDialogDescription),
                "'Tips For Best Results' Dialogue description in the Toolbar is NOT as expected when opened from SecondarySources");

            secondarySourcesContentTypePage = BrowserPool.CurrentBrowser.GoBack<EdgeContentTypeSearchResultPage>();
            var browsePage = secondarySourcesContentTypePage.ClickLinkByText<EdgeContentTypeSearchResultPage>(EthicsContentNameFromSecondarySources)
                .ClickLinkByText<EdgeContentTypeSearchResultPage>(SearchQuery);

            this.TestCaseVerify.IsTrue(
                checkSearchSummaryButtonDisplayed,
                browsePage.Toolbar.GetToolbarElementText(EdgeToolbarElements.SearchAndSummarizeEthics).Equals(SearchAndSummarizeEthics),
                "'Search & Summarize  Ethics' button is not displayed as expected");

            //SearchAndSummarizeEthics landing page label & content text, Category page label text verification
            aiAssistantPage = browsePage.Toolbar.ClickToolbarElement<AiAssistedResearchPage>(EdgeToolbarElements.SearchAndSummarizeEthics);

            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            this.TestCaseVerify.IsTrue(
                checkSearchSummaryEthicsLandingPageContentText,
                aiAssistantPage.Toolbar.HeadingLabel.Text.Equals(SearchAndSummarizeEthics)
                && aiAssistantPage.Chat.LandingPageLabel.Text.Contains(SearchAndSummarizeEthics)
                && aiAssistantPage.Chat.LandingPageContentLabel.Text.Contains(SearchAndSummarizeEthics),
                "Search & Summarize Ethics Landing Page header, label and label content text is incorrect");

            this.TestCaseVerify.IsTrue(
                checkSearchSummaryEthicsContentText,
                aiAssistantPage.Toolbar.CategoryPageLabel.Displayed
                && aiAssistantPage.Toolbar.CategoryPageLabel.Text.Equals($"Content: {CategoryPageLabelTextInToolbar}"),
                "Search & Summarize Ethics Category Page label text is incorrect");

            var howAiWorksDialog = aiAssistantPage.Chat.HowAiWorksLink.Click<HowAiAssistedResearchWorksDialog>();

            this.TestCaseVerify.IsTrue(
                checkAiAssistantFeatureHeaderAndDescription,
                howAiWorksDialog.TitleLabel.Text.Equals(HowAiWorksDialogHeader)
                && this.RemoveNewLinesAndExtraSpaces(howAiWorksDialog.DescriptionLabel.Text).Equals(HowAIWorksDialogDescription),
                "'How AI Works' Title and description are NOT as expected");

            aiAssistantPage = howAiWorksDialog.CloseButton.Click<AiAssistedResearchPage>();

            //checking the second how AI model works link
            howAiWorksDialog = aiAssistantPage.Toolbar.HowAiWorksButton.Click<HowAiAssistedResearchWorksDialog>();

            this.TestCaseVerify.IsTrue(
                checkAiAssistantFeatureHeaderAndDescriptionFromChatArea,
                howAiWorksDialog.TitleLabel.Text.Equals(HowAiWorksDialogHeader)
                && this.RemoveNewLinesAndExtraSpaces(howAiWorksDialog.DescriptionLabel.Text).Equals(HowAIWorksDialogDescription),
                "'How AI Works' Title and description in the chat Area are NOT as expected");

            //Checking the first Tips For Best Results link
            aiAssistantPage = howAiWorksDialog.CloseButton.Click<AiAssistedResearchPage>();

            tipsForBestResultsDialog = aiAssistantPage.Chat.TipsForBestResultLink.Click<TipsForBestResultsDialog>();

            this.TestCaseVerify.IsTrue(
                checkTipsForBestResultsDialogDescription,
                this.RemoveNewLinesAndExtraSpaces(tipsForBestResultsDialog.DescriptionLabel.Text).Equals(TipsForBestResultsDialogDescription),
                "'Tips For Best Results' Dialogue description is NOT as expected");

            aiAssistantPage = tipsForBestResultsDialog.CloseButton.Click<AiAssistedResearchPage>();

            //checking the second Tips For Best Results link in the Toolbar
            tipsForBestResultsDialog = aiAssistantPage.Toolbar.TipsBestResultsButton.Click<TipsForBestResultsDialog>();

            this.TestCaseVerify.IsTrue(
                checkTipsForBestResultsDialogDescriptionFromToolbar,
                this.RemoveNewLinesAndExtraSpaces(tipsForBestResultsDialog.DescriptionLabel.Text).Equals(TipsForBestResultsDialogDescription),
                "'Tips For Best Results' Dialogue description in the Toolbar is NOT as expected");

            BrowserPool.CurrentBrowser.CloseTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(BrowsePageTab);

            //Verify Ethics under 'Administrative Decisions & Guidance'

            browsePage = BrowserPool.CurrentBrowser.GoBack<EdgeContentTypeSearchResultPage>();
            browsePage = BrowserPool.CurrentBrowser.GoBack<EdgeContentTypeSearchResultPage>();
            browsePage = BrowserPool.CurrentBrowser.GoBack<EdgeContentTypeSearchResultPage>();

            var administrativeDecisionsGuidanceContentTypePage = homePage.BrowseTabPanel.SetActiveTab<AllContentTabPanel>(PrecisionBrowseTab.ContentTypes)
                .ClickBrowseCategory<EdgeContentTypeSearchResultPage>(ContentType.AdministrativeDecisionsAndGuidance);

            browsePage = administrativeDecisionsGuidanceContentTypePage.ClickLinkByText<EdgeContentTypeSearchResultPage>(EthicsContentNameFromAdministrativeDecisionsGuidance);

            this.TestCaseVerify.IsTrue(
               checkSearchSummaryButtonDisplayed,
               browsePage.Toolbar.GetToolbarElementText(EdgeToolbarElements.SearchAndSummarizeEthics).Equals(SearchAndSummarizeEthics),
               "'Search & Summarize  Ethics' button is not displayed as expected");

            //SearchAndSummarizeEthics landing page label & content text, Category page label text verification
            aiAssistantPage = browsePage.Toolbar.ClickToolbarElement<AiAssistedResearchPage>(EdgeToolbarElements.SearchAndSummarizeEthics);

            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            this.TestCaseVerify.IsTrue(
                checkSearchSummaryEthicsLandingPageContentText,
                aiAssistantPage.Toolbar.HeadingLabel.Text.Equals(SearchAndSummarizeEthics)
                && aiAssistantPage.Chat.LandingPageLabel.Text.Contains(SearchAndSummarizeEthics)
                && aiAssistantPage.Chat.LandingPageContentLabel.Text.Contains(SearchAndSummarizeEthics),
                "Search & Summarize Ethics Landing Page header, label and label content text is incorrect");

            this.TestCaseVerify.IsTrue(
                checkSearchSummaryEthicsContentText,
                aiAssistantPage.Toolbar.CategoryPageLabel.Displayed
                && aiAssistantPage.Toolbar.CategoryPageLabel.Text.Equals($"Content: {CategoryPageLabelTextInToolbar}"),
                "Search & Summarize Ethics Category Page label text is incorrect");

            howAiWorksDialog = aiAssistantPage.Chat.HowAiWorksLink.Click<HowAiAssistedResearchWorksDialog>();

            this.TestCaseVerify.IsTrue(
                checkAiAssistantFeatureHeaderAndDescription,
                howAiWorksDialog.TitleLabel.Text.Equals(HowAiWorksDialogHeader)
                && this.RemoveNewLinesAndExtraSpaces(howAiWorksDialog.DescriptionLabel.Text).Equals(HowAIWorksDialogDescription),
                "'How AI Works' Title and description are NOT as expected");

            aiAssistantPage = howAiWorksDialog.CloseButton.Click<AiAssistedResearchPage>();

            //checking the second how AI model works link
            howAiWorksDialog = aiAssistantPage.Toolbar.HowAiWorksButton.Click<HowAiAssistedResearchWorksDialog>();

            this.TestCaseVerify.IsTrue(
                checkAiAssistantFeatureHeaderAndDescriptionFromChatArea,
                howAiWorksDialog.TitleLabel.Text.Equals(HowAiWorksDialogHeader)
                && this.RemoveNewLinesAndExtraSpaces(howAiWorksDialog.DescriptionLabel.Text).Equals(HowAIWorksDialogDescription),
                "'How AI Works' Title and description in the chat Area are NOT as expected");

            //Checking the first Tips For Best Results link
            aiAssistantPage = howAiWorksDialog.CloseButton.Click<AiAssistedResearchPage>();

            tipsForBestResultsDialog = aiAssistantPage.Chat.TipsForBestResultLink.Click<TipsForBestResultsDialog>();

            this.TestCaseVerify.IsTrue(
                checkTipsForBestResultsDialogDescription,
                this.RemoveNewLinesAndExtraSpaces(tipsForBestResultsDialog.DescriptionLabel.Text).Equals(TipsForBestResultsDialogDescription),
                "'Tips For Best Results' Dialogue description is NOT as expected");

            aiAssistantPage = tipsForBestResultsDialog.CloseButton.Click<AiAssistedResearchPage>();

            //checking the second Tips For Best Results link in the Toolbar
            tipsForBestResultsDialog = aiAssistantPage.Toolbar.TipsBestResultsButton.Click<TipsForBestResultsDialog>();

            this.TestCaseVerify.IsTrue(
                checkTipsForBestResultsDialogDescriptionFromToolbar,
                this.RemoveNewLinesAndExtraSpaces(tipsForBestResultsDialog.DescriptionLabel.Text).Equals(TipsForBestResultsDialogDescription),
                "'Tips For Best Results' Dialogue description in the Toolbar is NOT as expected");
        }

        /// <summary>
        /// Task # 2260247
        /// Verify Content name is displayed on the history page
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        public void AiSearchAndSummarizeEthicsHistoryEventsTest()
        {
            const string Question = "Is a lawyer obligated to be truthful when making statements during settlement negotiations?";

            string checkContentNameLabel = "Verify: Content name is displayed as Ethics";
            string checkHistoryPage = "Verify: Category page name is displayed as 'Search & Summarize . ABA and State Ethics Opinions' instead of 'AI-Assisted Research . All State & Federal' ";
            string checkHistoryPageLeadsToAiResearch = "Verify: Category page event from the History page leads to AI-Assisted Research page";
            string checkGoToFullistoryPageLeadsToAiResearch = "Verify: Category page event from the History page (via 'Go to full history' link) leads to Full history search page";
            string checkHistoryPageViaGoToFullHistory = "Verify: Category page name is displayed instead of jurisdiction on the History page (via 'Go to full history' link)";

            var homePage = this.GetHomePage<PrecisionHomePage>();

            var browsePage = homePage.BrowseTabPanel.SetActiveTab<AllContentTabPanel>(PrecisionBrowseTab.ContentTypes)
              .ClickBrowseCategory<EdgeContentTypeSearchResultPage>(ContentType.SecondarySources)
              .ClickLinkByText<EdgeContentTypeSearchResultPage>(EthicsContentNameFromSecondarySources)
              .ClickLinkByText<EdgeContentTypeSearchResultPage>(SearchQuery);

            var aiAssistantPage = browsePage.Toolbar.ClickToolbarElement<AiAssistedResearchPage>(EdgeToolbarElements.SearchAndSummarizeEthics);

            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            SafeMethodExecutor.WaitUntil(() => aiAssistantPage.Toolbar.CategoryPageLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkContentNameLabel,
                aiAssistantPage.Toolbar.CategoryPageLabel.Text.Equals($"Content: {CategoryPageLabelTextInToolbar}"),
                "Category page name is NOT 'Ethics'");

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            aiAssistantPage = aiAssistantPage.ConversationHistory.ExpandButton.Click<AiAssistedResearchPage>();

            // History tab
            var recentHistoryDialog = aiAssistantPage.Header.ClickHeaderTab<EdgeRecentHistoryDialog>(EdgeHeaderTabs.History);
            SafeMethodExecutor.WaitUntil(() => recentHistoryDialog.GetRecentSearchesItems().First().HistoryItemLink.Displayed);

            this.TestCaseVerify.IsTrue(
                checkHistoryPage,
                recentHistoryDialog.GetRecentSearchesItems().First().HistoryItemLink.Text.Equals(Question)
                && recentHistoryDialog.GetRecentSearchesItems().First().MetaData.Contains(SearchAndSummarizeEthicsLabel),
                "Category page name is NOT displayed as 'Search & Summarize . ABA and State Ethics Opinions' on the History page");

            aiAssistantPage = recentHistoryDialog.GetRecentSearchesItems().First().HistoryItemLink.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkHistoryPageLeadsToAiResearch,
                aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text.Remove(0, aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text.IndexOf("says:") + 7).Equals(Question)
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.Count.Equals(1)
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().AnswerLabel.Displayed
                && aiAssistantPage.Toolbar.CategoryPageLabel.Text.Equals($"Content: {CategoryPageLabelTextInToolbar}"),
                "Category page event from the History tab doesn't lead to AI-Assisted Research page");

            // History page
            recentHistoryDialog = aiAssistantPage.Header.ClickHeaderTab<EdgeRecentHistoryDialog>(EdgeHeaderTabs.History);
            var historyPage = recentHistoryDialog.ViewAllLink.Click<EdgeCommonHistoryPage>();

            this.TestCaseVerify.IsTrue(
                checkHistoryPage,
                historyPage.HistoryTable.GetGridItems().First().Title.Equals(Question)
                && historyPage.HistoryTable.GetGridItems().First().Summary.Equals(SearchAndSummarizeEthicsLabel),
                "Category page name is NOT displayed as 'Search & Summarize . ABA and State Ethics Opinions' on the History page");

            aiAssistantPage = historyPage.HistoryTable.GetGridItems().First().ClickLinkByText<AiAssistedResearchPage>(historyPage.HistoryTable.GetGridItems().First().Title);
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                 checkHistoryPageLeadsToAiResearch,
                 aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text.Remove(0, aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text.IndexOf("says:") + 7).Equals(Question)
                 && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.Count.Equals(1)
                 && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().AnswerLabel.Displayed
                 && aiAssistantPage.Toolbar.CategoryPageLabel.Text.Equals($"Content: {CategoryPageLabelTextInToolbar}"),
                 "Category page event from the History page doesn't lead to AI-Assisted Research page");

            // History page via "Go to full history" link
            aiAssistantPage = aiAssistantPage.ConversationHistory.ExpandButton.Click<AiAssistedResearchPage>();

            historyPage = aiAssistantPage.ConversationHistory.GoToFullHistoryLink.Click<EdgeCommonHistoryPage>();

            BrowserPool.CurrentBrowser.CreateTab(HistoryPageTab);
            BrowserPool.CurrentBrowser.ActivateTab(HistoryPageTab);

            SafeMethodExecutor.WaitUntil(() => historyPage.EdgeToolbar.DeliveryDropdown.IsDisplayed());

            this.TestCaseVerify.IsTrue(
                checkHistoryPageViaGoToFullHistory,
                historyPage.HistoryTable.GetGridItems().First().Title.Equals(Question)
                && historyPage.HistoryTable.GetGridItems().First().Summary.Equals(SearchAndSummarizeEthicsLabel),
                "Category page name is NOT displayed instead of jurisdiction on the History page (via 'Go to full history' link)");

            aiAssistantPage = historyPage.HistoryTable.GetGridItems().First().ClickLinkByText<AiAssistedResearchPage>(historyPage.HistoryTable.GetGridItems().First().Title);
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkGoToFullistoryPageLeadsToAiResearch,
                aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text.Remove(0, aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text.IndexOf("says:") + 7).Equals(Question)
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.Count.Equals(1)
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().AnswerLabel.Displayed
                && aiAssistantPage.Toolbar.CategoryPageLabel.Text.Equals($"Content: {CategoryPageLabelTextInToolbar}"),
                "Category page event from the History page (via 'Go to full history' link) doesn't lead to AI-Assisted Research page");
        }

        protected override void InitializeRoutingPageSettings()
        {
            base.InitializeRoutingPageSettings();

            this.Settings.AppendValues(
                EnvironmentConstants.FeatureAccessControlsOn,
                SettingUpdateOption.Append,
                FeatureAccessControl.AIResearchEthics);

            this.Settings.AppendValues(
                EnvironmentConstants.InfrastructureAccessControlsOn,
                SettingUpdateOption.Append,
                "IAC-AI-ETHICS-OPINIONS");

            this.Settings.AppendValues(
                EnvironmentConstants.CategoryPageCollectionSet,
                SettingUpdateOption.Append,
                "w_cb_catpagesqa_cs");
        }

        private string RemoveNewLinesAndExtraSpaces(string text) => text.Replace("\n", string.Empty).Replace("\r", string.Empty).Replace(" ", string.Empty);

    }
}
