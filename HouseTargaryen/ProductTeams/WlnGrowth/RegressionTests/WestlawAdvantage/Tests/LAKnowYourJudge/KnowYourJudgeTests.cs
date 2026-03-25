namespace WestlawAdvantage.Tests.LAKnowYourJudge
{
    using Framework.Common.UI.Products.WestlawAdvantage.Pages;
    using Framework.Common.UI.Products.WestlawEdge.Components.LegalAnalytics;
    using Framework.Common.UI.Raw.WestlawEdge.Pages.LegalAnalaytics;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Microsoft.VisualStudio.TestTools.UnitTesting;    
    using Framework.Common.UI.Raw.WestlawEdge.Enums.LegalAnalytics;
    using Framework.Common.UI.Products.WestlawAdvantage.Components.LitigationAnalytics;
    using Framework.Core.Utils.Execution;
    using Framework.Common.UI.Products.WestlawAdvantage.Components.KnowYourJudge;
    using Framework.Common.UI.Products.WestlawAdvantage.Enums;
    using Framework.Common.UI.Products.WestlawAdvantage.Dialogs.KnowYourJudge;
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Threading;
    using Framework.Common.UI.Products.Shared.Managers;
    using System.Linq;
    using Framework.Common.UI.Products.Shared.Enums.Foldering;

    [TestClass]
    public class KnowYourJudgeTests : KnowYourJudgeBaseTest
    {
        /// <summary>
        /// Test Know your judge smoke test
        /// User story: 2229920, 2230063, 2259863 TC: 2256977
        /// 1. Sign in WL Advantage with KYJ access
        /// 2. Check: verify Know your judge card displayed
        /// 3. Check: verify Know your judge card content displayed
        /// 4. Click Know your Judge card under Key Features on home page
        /// 5. Check: Verify enhanced badge is displayed next to the judge tab
        /// 6. On Judges tab, search for a non district judge and open profile page
        /// 7. Check: Verify Know your judge tab disabled for non district judge
        /// 8. Click Litigation Analytics card under Key Features on home page
        /// 9. On Judges tab, search for a district judge and open profile page
        /// 10. Check: Verify Know your judge tab enabled for district judge
        /// 11. Click Know your judge tab
        /// 12. Check: Verify landing page for the judge displayed
        /// 13. Enter claims in input field 
        /// 14. Check: Verify claim input field contains user input
        /// 15. Click continue button and wait for progress bar to disappear
        /// 16. Check: Verify refine analysis page for the judge displayed
        /// 17. Click continue button and wait for progress bar to disappear
        /// 18. Check: Verify report page for the judge displayed
        /// 19. On report page, click Claims Based tab
        /// 20. Check: Verify Overview label displayed on report page
        /// 21. Navigate to history page
        /// 22. Check: Verify Know your judge entry, event, metadata displayed in History
        /// 23. Check: Verify Document View event, metadata with same judge displayed in History
        /// 24. Click on Event facet and filter with Know your Judge
        /// 25. Check: Verify count displayed correctly after applying Know your judge filter
        /// 26. Remove the filter
        /// 27. Check: Verify total count displayed correctly after removing Know your judge filter
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        public void KnowYourJudgeSmokeTest()
        {
            const string FederalDistrictJudgeName = "Reyes , Ana C.";
            const string NonFederalDistrictJudgeName = "Odom , John";
            const string KYJCardContent = "Get insights into how federal district court judges have ruled on cases with claims and facts similar to yours.";
            const string LandingPageTitle = "Analyze rulings from";
            const string ClaimsInput = "breach of contract";
            const string RefineAnalysisPageTitle = "Refine the analysis";

            var homePage = this.GetHomePage<AdvantageHomePage>();

            this.TestCaseVerify.IsTrue(
                "Know your judge card displayed",
                homePage.KeyFeaturesIncludedPanel.GetWidgetLinkByTitle(KnowYourJudgeCardTitle).Displayed,
                "Know your judge feature card not displayed");
            this.TestCaseVerify.IsTrue(
                "Verify Know your judge card content displayed",
                homePage.KeyFeaturesIncludedPanel.GetWidgetContentElement(KYJCardContent).Displayed,
                "Know your judge feature card content not displayed");

            homePage.KeyFeaturesIncludedPanel.GetWidgetLinkByTitle(KnowYourJudgeCardTitle).Click();
            var documentPage = new EdgeCommonDocumentPage();
            documentPage.CheckAndClosePendoDialog();

            var judgeTab= new JudgeTabComponent();
            this.TestCaseVerify.IsTrue(
                "Verify Enhanced badge is displayed next to the judge tab",
                judgeTab.EnhancedBadgeLabel.Text.Contains("Enhanced"),
                "Enhanced badge is not displayed next to the judge tab");

            var judgeProfilePage = judgeTab.EnterSearchQueryAndClickSearch<EdgeCommonSearchResultPage>(NonFederalDistrictJudgeName)
                           .ClickLinkByText<AnalyticsProfilerPage>(NonFederalDistrictJudgeName);

            this.TestCaseVerify.IsTrue(
                "Verify Know your judge tab disabled for non federal district judge",
                judgeProfilePage.IsKnowYourJudgeTabEnabled,
                "Know your judge tab enabled for non federal district judge");

            judgeProfilePage = OpenProfilePageForJudge(FederalDistrictJudgeName);
            this.TestCaseVerify.IsFalse(
                "Verify Know your judge tab enabled for federal district judge",
                judgeProfilePage.IsKnowYourJudgeTabEnabled,
                "Know your judge tab disabled for federal district judge");

            var knowYourJudgeTabComponent= judgeProfilePage.ProfileTabPanel.SetActiveTab<KnowYourJudgeTabComponent>(ProfileComponentTab.KnowYourJudge);
            
            this.TestCaseVerify.IsTrue(
                "Verify landing page for the judge displayed",
                knowYourJudgeTabComponent.LandingComponent.LandingPageTitle.Contains(LandingPageTitle),
                "Landing page for the judge not displayed");

            knowYourJudgeTabComponent.LandingComponent.EnterClaims(ClaimsInput);
            this.TestCaseVerify.IsTrue(
                "Verify claim input field contains user input",
                knowYourJudgeTabComponent.LandingComponent.ClaimsTextbox.GetAttribute("current-value").Equals(ClaimsInput),
                "Claim input field doesn't contain user input");

            knowYourJudgeTabComponent.LandingComponent.ContinueButton.ScrollToElement();
            knowYourJudgeTabComponent.LandingComponent.ContinueButton.Click();
            
            SafeMethodExecutor.WaitUntil(() => !knowYourJudgeTabComponent.LandingComponent.ProgressBarLabel.Displayed);
            knowYourJudgeTabComponent.RefineAnalysisComponent.ClearSelectionButton.ScrollToElement();
            this.TestCaseVerify.IsTrue(
                "Verify Refine Analysis page for the judge displayed",
                knowYourJudgeTabComponent.RefineAnalysisComponent.RefineAnalysisTitle.Contains(RefineAnalysisPageTitle),
                "Refine analysis page for the judge not displayed");

            knowYourJudgeTabComponent.RefineAnalysisComponent.ContinueButton.ScrollToElement();
            knowYourJudgeTabComponent.RefineAnalysisComponent.ContinueButton.Click();

            SafeMethodExecutor.WaitUntil(() => !knowYourJudgeTabComponent.RefineAnalysisComponent.ProgressBarLabel.Displayed);

            SafeMethodExecutor.WaitUntil(() => !knowYourJudgeTabComponent.ReportComponent.LoadingResultsLabel.Displayed);
            this.TestCaseVerify.IsTrue(
                "Verify report page for the judge displayed",
                knowYourJudgeTabComponent.ReportComponent.ReportContentElement.Displayed,
                "Report page for the judge not displayed");

            var claimsTab= knowYourJudgeTabComponent.ReportComponent.knowYourJudgeReportTabPanel.SetActiveTab<ClaimsBasedTab>(KnowYourJudgeReportTabs.ClaimBased);
            this.TestCaseVerify.IsTrue(
                "Verify Overview label displayed on report page",
                claimsTab.OverviewLabel.Contains("Overview"),
                "Overview label not displayed on report page");

            var historyPage = EdgeNavigationManager.Instance.GoToHistoryPage<EdgeCommonHistoryPage>();
            var firstHistoryItem = historyPage.HistoryTable.GetGridItems().First();
            var secondHistoryItem = historyPage.HistoryTable.GetGridItems().ElementAt(1);
            
            this.TestCaseVerify.IsTrue(
            "Verify Know your judge entry displayed in History",
               firstHistoryItem.Title.Contains(FederalDistrictJudgeName.Split(',')[0].Trim()) && firstHistoryItem.Title.Contains("Hon."),
               "Know your judge entry not displayed in History");
            
            this.TestCaseVerify.IsTrue(
            "Verify Know your judge event displayed in History",
               firstHistoryItem.Event.Equals(Events.KnowYourJudge),
               "Know your judge event not displayed in History. Actual event is: " + firstHistoryItem.Event);

            this.TestCaseVerify.IsTrue(
            "Verify Know your judge metadata displayed in History",
               firstHistoryItem.Summary.Contains("Litigation Analytics") && firstHistoryItem.Summary.Contains("Know Your Judge"),
               "Know your judge metadata not displayed in History");

            this.TestCaseVerify.IsTrue(
            "Verify Document View event with same judge displayed in History",
               secondHistoryItem.Title.Contains(FederalDistrictJudgeName.Split(',')[0].Trim()) && secondHistoryItem.Title.Contains("Hon."),
               "Document View event with same judge not displayed in History");

            this.TestCaseVerify.IsTrue(
            "Verify Document View event displayed in History",
               secondHistoryItem.Event.Equals(Events.DocumentView),
               "Document View event not displayed in History, Actual event is: " + secondHistoryItem.Event);

            this.TestCaseVerify.IsTrue(
            "Verify Document View event with metadata displayed in History",
               secondHistoryItem.Summary.Contains("Litigation AnalyticsJudgeKnow your judge"),
               "Document View event with metadata not displayed in History. Actual summary is: "+ secondHistoryItem.Summary);

            var historyEventFacet = historyPage.NarrowPane.Filter.HistoryEventFacet;
            historyEventFacet.ExpandFacetItemByName("Event");
            historyEventFacet.SetCheckboxForFacetByName<EdgeCommonHistoryPage>(true, "Know Your Judge");

            int itemcount = historyEventFacet.GetItemCountByName("Know Your Judge");
            int kyjCount = historyPage.HistoryTable.GetGridItems().Where(h => h.Event.Equals(Events.KnowYourJudge)).Count();

            this.TestCaseVerify.IsTrue(
            "Verify count displayed correctly after applying Know your judge filter",
               itemcount.Equals(historyPage.HistoryTable.GetGridItems().Count()) && itemcount.Equals(kyjCount),
               "count displayed correctly after applying Know your judge filter, Actual count is: " + itemcount);

            historyEventFacet.CheckedOption.Click();
            Thread.Sleep(2000);
            int totalCount = historyPage.HistoryTable.GetGridItems().Count();
            this.TestCaseVerify.IsTrue(
            "Verify total count displayed correctly after removing Know your judge filter",
               totalCount > itemcount,
               $"Total count not displayed correctly after removing Know your judge filter. Total count is {totalCount} and KYJ count is {itemcount}");
        }

        /// <summary>
        /// Test Know your judge Landing Page test
        /// User story:2253854, 2249164 Task:2265022 TC: 2265885
        /// 1. Sign in WL Advantage with KYJ access
        /// 2. Click on Know your Judge card under Key Features on home page
        /// 3. On Judges tab, search for a district judge and open profile page
        /// 4. Click Know your judge tab
        /// 5. Check: Verify Know your judge header displayed
        /// 6. Check: Verify new query button is disabled
        /// 7. Check: Verify landing page for the judge displayed
        /// 8. Check: Verify Tips for writing queries displayed
        /// 9. Check: Verify case details header is displayed
        /// 10. Check: Verify case details message is displayed
        /// 11. Click Tips for writing queries button
        /// 12. Check: Verify tips modal header displayed
        /// 13. Close tips modal
        /// 14. Enter max limit input in claims, facts and specific focus fields
        /// 15. Check: Verify max limit error message is displayed for each field
        /// 16. Click clear button after each field input
        /// 17. Click continue button without any input
        /// 18. Check: Verify Input error message is displayed
        /// 19. Click precedent summary switch
        /// 20. Check: Verify Precedent summary switch is enabled
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        public void KnowYourJudgeLandingPageTest()
        {
            const string FederalDistrictJudgeName = "Doty , David S.";
            const string LandingPageTitle = "Analyze rulings from";
            const string TipsForWritingQueries = "Tips for writing your queries";
            const string TipsModalHeader = "Tips for writing Know Your Judge queries";
            const string CaseDetailsHeader = "Case details";
            const string CaseDetailsMessage = "* At least one input box must be filled in.";
            string MaxLengthInput = File.ReadAllText(Environment.CurrentDirectory + @"\MaxInput500.txt");
            const string MaxCharLimitErrorMessage = "Character maximum reached. Please shorten your input.";
            const string InputErrorMessage = "You must enter at least 3 characters in the text box to continue.";

            var homePage = this.GetHomePage<AdvantageHomePage>();

            homePage.KeyFeaturesIncludedPanel.GetWidgetLinkByTitle(KnowYourJudgeCardTitle).Click();            
            var judgeTab = new JudgeTabComponent();
            var judgeProfilePage = judgeTab.EnterSearchQueryAndClickSearch<EdgeCommonSearchResultPage>(FederalDistrictJudgeName)
                           .ClickLinkByText<AnalyticsProfilerPage>(FederalDistrictJudgeName);
            var knowYourJudgeTabComponent = judgeProfilePage.ProfileTabPanel.SetActiveTab<KnowYourJudgeTabComponent>(ProfileComponentTab.KnowYourJudge);
                        
            this.TestCaseVerify.IsTrue(
                "Verify know your judge header displayed",
                knowYourJudgeTabComponent.KnowYourJudgeHeaderLabel.Text.Equals("Know your judge"),
                "Know your judge header not displayed");  
            
            this.TestCaseVerify.IsTrue(
                "Verify new query button is disabled",
                knowYourJudgeTabComponent.NewQueryButton.GetAttribute("class").Equals("disabled"),
                "New query button is enabled");

            this.TestCaseVerify.IsTrue(
                "Verify landing page for the judge displayed",
                knowYourJudgeTabComponent.LandingComponent.LandingPageTitle.Contains(LandingPageTitle),
                "Landing page for the judge not displayed");

            this.TestCaseVerify.IsTrue(
                 "Verify Tips for writing queries displayed",
                  knowYourJudgeTabComponent.LandingComponent.TipsButton.Text.Equals(TipsForWritingQueries),
                  "Tips for writing queries not displayed");

            this.TestCaseVerify.IsTrue(
                 "Verify case details header is displayed",
                  knowYourJudgeTabComponent.LandingComponent.CaseDetailsHeaderLabel.Text.Equals(CaseDetailsHeader),
                  "Case details header not displayed");

            this.TestCaseVerify.IsTrue(
                 "Verify case details message is displayed",
                  knowYourJudgeTabComponent.LandingComponent.CaseDetailsNoteLabel.Text.Equals(CaseDetailsMessage),
                  "Case details message not displayed");

            var tipsForWritingYourQueriesDialog= knowYourJudgeTabComponent.LandingComponent.TipsButton.Click<TipsForWritingYourQueriesDialog>();
            this.TestCaseVerify.IsTrue(
                 "Verify Tips Modal Header is displayed after clicking on Tips button",
                  tipsForWritingYourQueriesDialog.GetTipsModalHeader().Equals(TipsModalHeader),
                  "Tips Modal Header is not displayed after clicking on Tips button");
            tipsForWritingYourQueriesDialog.CloseButton.Click();

            knowYourJudgeTabComponent.LandingComponent.EnterClaims(MaxLengthInput);
            this.TestCaseVerify.IsTrue(
                 "Verify max limit error message is displayed for claims",
                  knowYourJudgeTabComponent.LandingComponent.ReportErrorLabel.Text.Equals(MaxCharLimitErrorMessage),
                  "Max limit error message not displayed for claims");
            knowYourJudgeTabComponent.LandingComponent.ClearButton.Click();

            knowYourJudgeTabComponent.LandingComponent.EnterFacts(MaxLengthInput);
            this.TestCaseVerify.IsTrue(
                 "Verify max limit error message is displayed for facts",
                  knowYourJudgeTabComponent.LandingComponent.ReportErrorLabel.Text.Equals(MaxCharLimitErrorMessage),
                  "Max limit error message not displayed for facts");
            knowYourJudgeTabComponent.LandingComponent.ClearButton.Click();

            knowYourJudgeTabComponent.LandingComponent.EnterSpecificFocus(MaxLengthInput);
            this.TestCaseVerify.IsTrue(
                 "Verify max limit error message is displayed for specific focus",
                  knowYourJudgeTabComponent.LandingComponent.ReportErrorLabel.Text.Equals(InputErrorMessage),
                  "Max limit error message not displayed for specific focus");
            knowYourJudgeTabComponent.LandingComponent.ClearButton.Click();

            knowYourJudgeTabComponent.LandingComponent.ContinueButton.ScrollToElement();
            knowYourJudgeTabComponent.LandingComponent.ContinueButton.Click();
            this.TestCaseVerify.IsTrue(
                 "Verify Input error message is displayed",
                  knowYourJudgeTabComponent.LandingComponent.ReportErrorLabel.Text.Equals(InputErrorMessage),
                  "Input error message not displayed");

            knowYourJudgeTabComponent.LandingComponent.PrecedentSummarySwitchButton.Click();
            this.TestCaseVerify.IsTrue(
                 "Verify Precedent summary switch is enabled",
                  knowYourJudgeTabComponent.LandingComponent.PrecedentSummarySwitchButton.GetAttribute("current-checked").Equals("true"),
                  "Precedent summary switch is not enabled");
        }

        /// <summary>
        /// Test Know your judge Generation report test
        /// User story:2230065,2230061 Task:2284168 TC: 2266041
        /// 1. Sign in WL Advantage with KYJ access
        /// 2. Click on Know your Judge card under Key Features on home page
        /// 3. On Judges tab, search for a district judge and open profile page
        /// 4. Click Know your judge tab
        /// 5. Enter claims in input field and click continue button 
        /// 6. Check: Verify refine analysis page for the judge displayed
        /// 7. On Refine analysis page, click clear selection button 
        /// 8. expand analysis section 
        /// 9. select checkboxes from all sections and click continue button
        /// 10. Check: Verify Know your judge for the judge name text displayed in report generation page
        /// 11. Click on report heading button and
        /// 12. Check: Verify Summary of analysis details section displayed in report generation page
        /// 13. Check: Verify Email me button displayed in report generation page and click on it
        /// 14. Check: Verify Email me text displayed after clicking on Email me button
        /// 15. Wait until loading results label is not displayed
        /// 16. Click on Claims Based tab and verify all accordion label displayed
        /// 17. Click on summary Accordion button
        /// 18. Check: Verify Summary is expanded
        /// 19. Click on Order Summary tab
        /// 20. Check: Verify Verify Judicial order summary, Key Citations, claims text displayed on Order Summary tab
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        public void KnowYourJudgeGenerateReportTest()
        {
            const string FederalDistrictJudgeName = "Doty , David S.";
            const string ClaimsInput = "FLSA Violation";
            const string RefineAnalysisPageTitle = "Refine the analysis";
            List<string> CheckBoxOptions = new List<string>{ "Motion to Compel", "Violation + Motion to Compel Expansion" };
            List<string> ClaimBasedAccordionItems = new List<string> { "Overview", "Common claim patterns", "Influential claim patterns", "Analysis of your claims",
                "Outcome patterns for your claims", "How this judge reasons", "Evidence & procedural requirements", "Strategic recommendations" };

            var homePage = this.GetHomePage<AdvantageHomePage>();

            homePage.KeyFeaturesIncludedPanel.GetWidgetLinkByTitle(KnowYourJudgeCardTitle).Click();
            var judgeTab = new JudgeTabComponent();
            var judgeProfilePage = judgeTab.EnterSearchQueryAndClickSearch<EdgeCommonSearchResultPage>(FederalDistrictJudgeName)
                           .ClickLinkByText<AnalyticsProfilerPage>(FederalDistrictJudgeName);
            Thread.Sleep(3000);
            var knowYourJudgeTabComponent = judgeProfilePage.ProfileTabPanel.SetActiveTab<KnowYourJudgeTabComponent>(ProfileComponentTab.KnowYourJudge);
            Thread.Sleep(3000);
            knowYourJudgeTabComponent.LandingComponent.EnterClaims(ClaimsInput);

            knowYourJudgeTabComponent.LandingComponent.ContinueButton.ScrollToElement();
            knowYourJudgeTabComponent.LandingComponent.ContinueButton.Click();

            SafeMethodExecutor.WaitUntil(() => !knowYourJudgeTabComponent.LandingComponent.ProgressBarLabel.Displayed, 60, 100);

            knowYourJudgeTabComponent.RefineAnalysisComponent.ClearSelectionButton.ScrollToElement();
            this.TestCaseVerify.IsTrue(
                "Verify Refine Analysis page for the judge displayed",
                knowYourJudgeTabComponent.RefineAnalysisComponent.RefineAnalysisTitle.Contains(RefineAnalysisPageTitle),
                "Refine analysis page for the judge not displayed");
            knowYourJudgeTabComponent.RefineAnalysisComponent.ClearSelectionButton.Click();
            knowYourJudgeTabComponent.RefineAnalysisComponent.ExpandAnalysisSwitchButton.Click();
                       
            knowYourJudgeTabComponent.RefineAnalysisComponent.selectCheckbox(CheckBoxOptions);

            knowYourJudgeTabComponent.RefineAnalysisComponent.ContinueButton.ScrollToElement();
            knowYourJudgeTabComponent.RefineAnalysisComponent.ContinueButton.Click();
            SafeMethodExecutor.WaitUntil(() => !knowYourJudgeTabComponent.RefineAnalysisComponent.ProgressBarLabel.Displayed, 60, 100);

            knowYourJudgeTabComponent.ReportComponent.ReportHeadingButton.ScrollToElement();
            var ReportHeading = knowYourJudgeTabComponent.ReportComponent.ReportHeadingLabel.Text;
            
            this.TestCaseVerify.IsTrue(
                "Verify Know your judge for the judge name text displayed",
                ReportHeading.Contains("Know your judge for") && ReportHeading.Contains(FederalDistrictJudgeName.Split(',')[0].Trim()),
                "Know your judge for the judge name text not displayed");
            knowYourJudgeTabComponent.ReportComponent.ReportHeadingButton.Click();
            this.TestCaseVerify.IsTrue(
                "Verify Summary of analysis details section displayed",
                knowYourJudgeTabComponent.ReportComponent.SummaryDetailsLabel.Text.Contains("Summary of analysis details"),
                "Summary of analysis details section not displayed");
           
            this.TestCaseVerify.IsTrue(
                "Verify Email me button displayed",
                knowYourJudgeTabComponent.ReportComponent.EmailMeButton.Text.Equals("Email me when results are ready"),
                "Email me button not displayed");
            knowYourJudgeTabComponent.ReportComponent.EmailMeButton.Click();
            this.TestCaseVerify.IsTrue(
                "Verify Email me text displayed",
                knowYourJudgeTabComponent.ReportComponent.EmailMeText.Text.Contains("You'll receive an email at"),
                "Email me text not displayed");

            SafeMethodExecutor.WaitUntil(() => !knowYourJudgeTabComponent.ReportComponent.LoadingResultsLabel.Displayed);

            var claimsTab = knowYourJudgeTabComponent.ReportComponent.knowYourJudgeReportTabPanel.SetActiveTab<ClaimsBasedTab>(KnowYourJudgeReportTabs.ClaimBased);
            claimsTab.VerifyAccordionItemIsPresent(ClaimBasedAccordionItems);
            
            var arrowElement= claimsTab.ClickSummaryAccordionArrow();            
            this.TestCaseVerify.IsTrue(
                "Verify Summary is expanded after clicking on summary accordion arrow",
                arrowElement.GetAttribute("aria-expanded").Equals("true"),
                "Summary is not expanded after clicking on summary accordion arrow");
            
            var OrderSummaryTab = knowYourJudgeTabComponent.ReportComponent.knowYourJudgeReportTabPanel.SetActiveTab<OrderSummaryTab>(KnowYourJudgeReportTabs.OrderSummary);
            Thread.Sleep(3000);
            this.TestCaseVerify.IsTrue(
                "Verify Judicial order summary text displayed in order summary Tab",
                OrderSummaryTab.OrderSummaryTextElements[0].Text.Equals("Judicial order summary"),
                "Judicial order summary text not displayed in order summary Tab");
            this.TestCaseVerify.IsTrue(
                "Verify Key Citations text displayed in order summary Tab",
                OrderSummaryTab.OrderSummaryTextElements[1].Text.Equals("Key Citations"),
                "Key Citations text not displayed in order summary Tab");
            this.TestCaseVerify.IsTrue(
                "Verify Claims text displayed in order summary Tab",
                OrderSummaryTab.OrderSummaryTextElements[2].Text.Contains("Claims"),
                "Claims text not displayed in order summary Tab");
        }
    }
}

