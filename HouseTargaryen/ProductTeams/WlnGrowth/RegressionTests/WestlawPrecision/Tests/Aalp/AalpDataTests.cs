namespace WestlawPrecision.Tests.Aalp
{
    using ClosedXML.Excel;
    using Framework.Common.UI.Products.Shared.Dialogs.Alerts;
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.AALP;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages.AALP;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Header;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Core.Utils.Execution;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// AALP data tests
    /// </summary>
    [TestClass]
    public class AalpDataTests : AalpBaseTest
    {
        const string Path = @"\\cobalttesttools.int.thomsonreuters.com\cobalttesttools$\QED\WestlawPrecision\AiAssistant";

        /// <summary>
        /// Comparing expected answers for invalid questions from the Excel file
        /// </summary>
        //[TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void AiAssistantInvalidQuestionsTest()
        {
            var coCounselHeaderDialog = this.GetHomePage<PrecisionHomePage>().Header.ClickHeaderTab<CoCounselDialog>(EdgeHeaderTabs.CoCounsel);
            var aiAssistantPage = coCounselHeaderDialog.AiAssistedResearchLink.Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            var workbook = new XLWorkbook($@"{Path}\TestData\AalpInvalidQuestions.xlsx");
            var sheet = workbook.Worksheet(1);
            var range = this.GetUsedRangeOfExcelSheet(sheet);

            var questions = range.AsTable().DataRange.Rows().Select(question => question.Field("QUESTIONS").GetString()).ToList();
            var answers = range.AsTable().DataRange.Rows().Select(answer => answer.Field("ANSWERS").GetString()).ToList();

            var questionsAndAnswers = questions.Zip(answers, (key, value) => new { key, value }).ToDictionary(item => item.key, item => item.value);

            foreach (var question in questionsAndAnswers.Keys)
            {
                aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(question);
                aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
                SafeMethodExecutor.ExecuteUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

                this.TestCaseVerify.AreEqual(
                    $"Verify: Answer for {question} is {questionsAndAnswers[question]}",
                    questionsAndAnswers[question],
                    aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ErrorAnswerLabel.Text,
                    $"Answer for {question} is NOT {questionsAndAnswers[question]}");
            }
        }

        /// <summary>
        /// Collecting answers and writing them to Excel file
        /// </summary>
        //[TestMethod]
        [TestCategory("QuestionsAndAnswersCollector")]
        public void AiAssistantAnswersCollectorTest()
        {
            const string TimeFormat = "dd MMMM HH mm ss";
            const string DateFormat = "MM/dd/yyyy";

            var filename = $"Ai_{this.TestExecutionContext.TestEnvironment.Id}_{DateTime.Now.ToString(TimeFormat)}.xlsx";

            var workbook = new XLWorkbook($@"{Path}\TestData\AalpQuestions.xlsx");
            var sheet = workbook.Worksheet(1);
            var range = this.GetUsedRangeOfExcelSheet(sheet);

            var questions = range.AsTable().DataRange.Rows().Select(userQuote => userQuote.Field("QUESTIONS").GetString()).Take(10).ToList();
            var jurisdictions = range.AsTable().DataRange.Rows().Select(userQuote => userQuote.Field("JURISDICTIONS").GetString().Replace(" ", string.Empty).Split(',').ToList()).Take(10).ToList()
                .Select(list => list.Select(item => (Jurisdiction)Enum.Parse(typeof(Jurisdiction), item)).ToArray()).ToList();

            var questionsAndJurisdictions = questions.Zip(jurisdictions, (key, value) => new { key, value }).ToDictionary(item => item.key, item => item.value);

            var coCounselHeaderDialog = this.GetHomePage<PrecisionHomePage>().Header.ClickHeaderTab<CoCounselDialog>(EdgeHeaderTabs.CoCounsel);
            var aiAssistantPage = coCounselHeaderDialog.AiAssistedResearchLink.Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            var answers = new List<string>();

            foreach (var question in questionsAndJurisdictions.Keys)
            {
                aiAssistantPage = aiAssistantPage.Toolbar.JurisdictionButton.Click<JurisdictionOptionsDialog>().SelectJurisdictions(true, questionsAndJurisdictions[question]).ClickSelectButton<AiAssistedResearchPage>();
                aiAssistantPage.QueryBox.QuestionTextbox.SetText(question);
                aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
                var startTime = DateTime.Now;
                SafeMethodExecutor.ExecuteUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed, timeoutFromSec: 15);

                if (aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed)
                {
                    var responseTime = (DateTime.Now - startTime).TotalSeconds.ToString();
                    this.WriteToExcelFile(Path, filename, question, $"NO_ANSWER", responseTime, DateTime.Now.ToString(DateFormat));
                }
                else if (aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ErrorAnswerLabel.Displayed)
                {
                    var responseTime = (DateTime.Now - startTime).TotalSeconds.ToString();
                    this.WriteToExcelFile(Path, filename, question, $"{aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ErrorAnswerLabel.Text}", responseTime, DateTime.Now.ToString(DateFormat));
                }
                else
                {
                    var responseTime = (DateTime.Now - startTime).TotalSeconds.ToString();
                    this.WriteToExcelFile(Path, filename, question, aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().AnswerLabel.Text.Equals(string.Empty)
                        ? $"NO_ANSWER"
                        : aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().AnswerLabel.Text, responseTime, DateTime.Now.ToString(DateFormat));
                }

                aiAssistantPage = aiAssistantPage.Toolbar.NewResearchButton.Click<AiAssistedResearchPage>();
            }
        }

        private void WriteToExcelFile(string path, string filename, string question, string answer, string responseTime, string date)
        {
            var pathToFile = $@"{path}\{filename}";

            var files = new DirectoryInfo(path).GetFiles("*.*");

            var latestfile = string.Empty;
            var lastModified = DateTime.MinValue;

            foreach (var file in files)
            {
                if (file.CreationTime < DateTime.Now.AddDays(-7) && file.Name.Contains("Ai_"))
                {
                    file.Delete();
                }

                if (file.LastWriteTime > lastModified && file.Name.Contains($"Ai_{this.TestExecutionContext.TestEnvironment.Id}"))
                {
                    lastModified = file.LastWriteTime;
                    latestfile = file.Name;
                }
            }

            var latestFilePath = $@"{path}\{latestfile}";

            var workbook = new XLWorkbook(latestFilePath);
            var sheet = workbook.Worksheet(1);
            var range = this.GetUsedRangeOfExcelSheet(sheet);

            var headers = range.AsTable().DataRange.FirstRowUsed().RowUsed().RowAbove().Cells().Select(item => item.Value.ToString()).ToList();

            sheet.Cell(range.RowCount() + 1, headers.IndexOf("QUESTIONS") + 1).Value = question;
            sheet.Cell(range.RowCount() + 1, headers.IndexOf("ANSWERS") + 1).Value = answer;
            sheet.Cell(range.RowCount() + 1, headers.IndexOf("RESPONSE TIME") + 1).Value = responseTime;
            sheet.Cell(range.RowCount() + 1, headers.IndexOf("DATE") + 1).Value = date;

            if (File.Exists(pathToFile))
            {
                workbook.Save();
            }
            else
            {
                workbook.SaveAs(pathToFile);
            }
        }

        private IXLRange GetUsedRangeOfExcelSheet(IXLWorksheet sheet)
        {
            var firstPossibleAddress = sheet.Row(sheet.FirstRowUsed().RowUsed().RowNumber()).FirstCell().Address;
            var lastPossibleAddress = sheet.LastCellUsed().Address;

            return sheet.Range(firstPossibleAddress, lastPossibleAddress).RangeUsed();
        }
    }
}
