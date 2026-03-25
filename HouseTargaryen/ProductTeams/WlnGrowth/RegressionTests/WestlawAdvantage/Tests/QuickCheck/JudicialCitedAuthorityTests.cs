namespace WestlawAdvantage.Tests.QuickCheck
{
    using Framework.Common.UI.Products.Shared.Managers;
    using Framework.Common.UI.Products.WestlawAdvantage.Components.Judicial.ReportTabs;
    using Framework.Common.UI.Products.WestlawEdge.Enums.QuickCheck;
    using Framework.Core.Utils.Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Judicial Cited Authority tab tests
    /// </summary>
    [TestClass]
    public class JudicialCitedAuthorityTests : WestlawAdvantageQuickCheckBaseTest
    {
        private new const string CurrentTestCategory = "WestlawAdvantageQuickCheck";

        /// <summary>
        /// Task 2235797, 2235800, 2234455
        /// Upload documents by parties
        /// Verify: Citation Issues button is displayed
        /// Verify: Citation Issues button is expanded
        /// Verify: No Citation Message is displayed for Party 1
        /// Verify: Citation issues 'Cited Authority' counting is correct
        /// Verify: Party 1 'Language Analysis' counting is correct and equals to ''Cited Authority'
        /// Verify: Party 2 'Language Analysis' counting is correct and equals to ''Cited Authority'
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        public void JudicialCitedAuthorityTabCitationIssuesTest()
        {
            const string Document1 = "Insurance Litigation - Party 1 - Northern 1 - Motion for Summary Judgment.docx";
            const string Document2 = "Insurance Litigation - Party 2 - Roberts 1 -  Opposition to MSJ and Cross Motion.docx";
            const string NoCitationMessage = "No citation issues were found in the uploaded document.";

            string checkCitationIssuesButtonDisplayed = "Verify: Citation Issues button is displayed";
            string checkStatusIsKept = "Verify: State(open/closed) is kept per tab";
            string checkCitationIssuseCitedAuthorityCounting = "Verify: Citation issues 'Cited Authority' counting is correct";
            string checkNoCitationMessageIsDisplayed = "Verify: No citation message is displayed";
            string checkFirstPartyLanguageAnalysisEqualsToCitedAuthority = "Verify: Party 1 'Language Analysis' counting is correct and equals to ''Cited Authority'";
            string checkSecondPartyLanguageAnalysisEqualsToCitedAuthority = "Verify: Party 2 'Language Analysis' counting is correct and equals to ''Cited Authority'";
            string checkCitationIssueStatusForLanguageAnalysis = "Verify Citation issue button is by default collapsed for language analysis tab";

            var assignment = new Dictionary<string, JudicialParties>
            {
                { Document1, JudicialParties.FirstParty },
                { Document2, JudicialParties.SecondParty }
            };

            var reportPage = QuickCheckUiManager.AssignDocumentsToPartiesAndGetReportInAdvantage(TestDocsPath, assignment);

            var judicialCitedAuthorityTab = reportPage.ReportTabsPanel.GetJudicialCitedAuthorityTab();

            this.TestCaseVerify.IsTrue(
                checkCitationIssuesButtonDisplayed,
                judicialCitedAuthorityTab.CitationIssuesButton.Displayed,
                "Citation Issues button is NOT displayed on the Cited Authority tab");

            this.TestCaseVerify.IsTrue(
                checkStatusIsKept,
                judicialCitedAuthorityTab.CitationIssuesButton.GetAttribute("aria-expanded").Equals("true"),
                "Citation Issues button status is not maintained in Warning for cited authority tab");

            var firstCitedAuthorityPartyBox = judicialCitedAuthorityTab.CitationIssuesComponent.PartyIssueBoxes.First();
            var secondCitedAuthorityPartyBox = judicialCitedAuthorityTab.CitationIssuesComponent.PartyIssueBoxes.Last();

            var firstCitedAuthorityPartyBoxCount = firstCitedAuthorityPartyBox.CountLabel.Text.ConvertCountToInt();
            var secondCitedAuthorityPartyBoxCount = secondCitedAuthorityPartyBox.CountLabel.Text.ConvertCountToInt();

            this.TestCaseVerify.IsTrue(
                checkCitationIssuseCitedAuthorityCounting,
                judicialCitedAuthorityTab.CitationIssuesComponent.TitleLabel.Text.ConvertCountToInt().Equals(firstCitedAuthorityPartyBoxCount + secondCitedAuthorityPartyBoxCount)
                && judicialCitedAuthorityTab.CitationIssuesButton.Text.ConvertCountToInt().Equals(firstCitedAuthorityPartyBoxCount + secondCitedAuthorityPartyBoxCount),
                "Citation issues 'Cited Authority' counting is NOT correct");

            firstCitedAuthorityPartyBox.DocumentTitleButton.Click();

            this.TestCaseVerify.IsTrue(
                checkNoCitationMessageIsDisplayed,
                firstCitedAuthorityPartyBox.NoCitationMessageLabel.Text.Equals(NoCitationMessage),
                "No citation message is NOT displayed for Party 1");

            var judicialLanguageAnalysisTab = reportPage.ReportTabsPanel.GetJudicialLanguageAnalysisTabTab().PartySwitcher.GetFirstParty();

            this.TestCaseVerify.IsTrue(
                checkCitationIssueStatusForLanguageAnalysis,
                judicialCitedAuthorityTab.CitationIssuesButton.GetAttribute("aria-expanded").Equals("false"),
                "Citation Issues button status is not collapsed in Language Analysis tab");

            judicialLanguageAnalysisTab = judicialLanguageAnalysisTab.CitationIssuesButton.Click<JudicialLanguageAnalysisTab>();

            var firstLanguageAnalysisPartyBox = judicialLanguageAnalysisTab.CitationIssuesComponent.PartyIssueBoxes.First();
            var firstLanguageAnalysisPartyBoxCount = firstLanguageAnalysisPartyBox.CountLabel.Text.ConvertCountToInt();

            this.TestCaseVerify.IsTrue(
                checkFirstPartyLanguageAnalysisEqualsToCitedAuthority,
                judicialLanguageAnalysisTab.CitationIssuesComponent.TitleLabel.Text.ConvertCountToInt().Equals(firstLanguageAnalysisPartyBoxCount)
                && judicialCitedAuthorityTab.CitationIssuesButton.Text.ConvertCountToInt().Equals(firstLanguageAnalysisPartyBoxCount)
                && firstLanguageAnalysisPartyBoxCount.Equals(firstCitedAuthorityPartyBoxCount),
                "Party 1 'Language Analysis' counting is NOT correct and doesn't equal to 'Cited Authority'");

            judicialLanguageAnalysisTab = reportPage.ReportTabsPanel.GetJudicialLanguageAnalysisTabTab().PartySwitcher.GetSecondParty();

            judicialLanguageAnalysisTab = judicialLanguageAnalysisTab.CitationIssuesButton.Click<JudicialLanguageAnalysisTab>();

            var secondLanguageAnalysisPartyBox = judicialLanguageAnalysisTab.CitationIssuesComponent.PartyIssueBoxes.First();
            var secondLanguageAnalysisPartyBoxCount = secondLanguageAnalysisPartyBox.CountLabel.Text.ConvertCountToInt();

            this.TestCaseVerify.IsTrue(
                checkSecondPartyLanguageAnalysisEqualsToCitedAuthority,
                judicialLanguageAnalysisTab.CitationIssuesComponent.TitleLabel.Text.ConvertCountToInt().Equals(secondLanguageAnalysisPartyBoxCount)
                && judicialCitedAuthorityTab.CitationIssuesButton.Text.ConvertCountToInt().Equals(secondLanguageAnalysisPartyBoxCount)
                && secondLanguageAnalysisPartyBoxCount.Equals(secondCitedAuthorityPartyBoxCount),
                "Party 2 'Language Analysis' counting is NOT correct and doesn't equal to 'Cited Authority'");
        }
    }
}
