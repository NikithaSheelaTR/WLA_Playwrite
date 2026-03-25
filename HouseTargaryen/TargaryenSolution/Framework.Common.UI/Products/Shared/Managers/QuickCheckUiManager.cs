namespace Framework.Common.UI.Products.Shared.Managers
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.WestlawAdvantage.Pages.Judicial;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.QuickCheck;
    using Framework.Common.UI.Products.WestlawEdge.Enums.QuickCheck;
    using Framework.Common.UI.Products.WestlawEdge.Items.Judicial;
    using Framework.Common.UI.Products.WestlawEdge.Pages.Judicial;
    using Framework.Common.UI.Products.WestlawEdge.Pages.QuickCheck;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages.AALP.ComplaintAnalyzer;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Execution;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The document analyzer manager.
    /// </summary>
    public static class QuickCheckUiManager
    {
        private const string CheckWorkUrl = "/QuickCheck/CheckWork/Upload?contextData=(sc.Default)&transitionType=Default";
        private const string OpponentWorkUrl = "/QuickCheck/Opponent/Upload?contextData=(sc.Default)&transitionType=Default";
        private const string JudicialUrl = "/QuickCheck/Judicial/Upload?contextData=(sc.Default)&transitionType=Default";
        private const string LandingUrl = "/QuickCheck?transitionType=Default&contextData=%28sc.Default%29";
        private const string ComplaintAnalyserUrl = "/ComplaintAnalyzer/Home?contextData=(sc.Default)&transitionType=Default";

        /// <summary>
        /// The go to doc analyzer upload page.
        /// </summary>
        /// <param name="uploadOption">
        /// The upload Option.
        /// </param>
        /// <returns>
        /// The <see cref="QuickCheckUploadPage"/>.
        /// </returns>
        public static QuickCheckUploadPage GoToDocAnalyzerUploadPage(WhatYouWouldLikeToDoOptions uploadOption = WhatYouWouldLikeToDoOptions.CheckYourWork)
        {
            QuickCheckUiManager.NavigateToDocAnalyzerEntryPoint(uploadOption);
            return new QuickCheckUploadPage();
        }

        /// <summary>
        /// Navigate to do analyzer alternate page
        /// </summary>
        /// <returns> The <see cref="QuickCheckAlternateHomePage"/>. </returns>
        public static QuickCheckAlternateHomePage GoToDocAnalyzerAlternatePage() =>
            EdgeNavigationManager.Instance.GoToQuickCheckPage<QuickCheckAlternateHomePage>();

            /// <summary>
        /// Navigate to do analyzer alternate page
        /// </summary>
        /// <returns> The <see cref="QuickCheckAlternateHomePage"/>. </returns>
        public static QuickCheckLandingPage GoToLandingPage(bool closeWelcomeDialog = true)
        {
            BrowserPool.CurrentBrowser.GoToUrl($"{QuickCheckUiManager.GetDomain()}{LandingUrl}");
            var page = new QuickCheckLandingPage();

            if (closeWelcomeDialog)
            {
                page.CloseWelcomeVideoDialogIfDisplayed();
            }

            return page;
        }

        /// <summary>
        /// The upload file.
        /// </summary>
        /// <param name="filePath"> The file path. </param>
        /// <param name="uploadOption"> The upload option. </param>
        /// <typeparam name="TPage"> The type of page </typeparam>
        /// <returns> The TPage</returns>
        public static TPage UploadFile<TPage>(
            string filePath,
            WhatYouWouldLikeToDoOptions uploadOption = WhatYouWouldLikeToDoOptions.CheckYourWork)
            where TPage : ICreatablePageObject
        {
            QuickCheckUploadPage uploadPage = QuickCheckUiManager.GoToDocAnalyzerUploadPage(uploadOption);
            var uploadDialog = uploadPage.UploadFile<QuickCheckFileUploadDialog>(filePath);
            uploadDialog.WaitUntilFileUpload();
            return DriverExtensions.CreatePageInstance<TPage>();
        }

        /// <summary>
        /// The upload file.
        /// </summary>
        /// <param name="folderPath"> The folder path. </param>
        /// <param name="fileName"> The file name. </param>
        /// <param name="uploadOption"> The upload option. </param>
        /// <typeparam name="TPage"> The type of page</typeparam> 
        /// <returns> The new page </returns>
        public static TPage UploadFile<TPage>(
            string folderPath,
            string fileName,
            WhatYouWouldLikeToDoOptions uploadOption = WhatYouWouldLikeToDoOptions.CheckYourWork)
            where TPage : ICreatablePageObject => QuickCheckUiManager.UploadFile<TPage>($@"{folderPath}/{fileName}", uploadOption);

        /// <summary>
        /// The enter text with cited authority.
        /// </summary>
        /// <param name="legalIssue"> The legal issue. </param>
        /// <param name="text"> The text. </param>
        /// <param name="uploadOption"> The upload option. </param>
        /// <typeparam name="TPage"> The type of page </typeparam>
        /// <returns> The TPage </returns>
        public static TPage EnterTextWithCitedAuthority<TPage>(
            string legalIssue,
            string text,
            WhatYouWouldLikeToDoOptions uploadOption = WhatYouWouldLikeToDoOptions.CheckYourWork)
            where TPage : ICreatablePageObject
        {
            QuickCheckUploadPage uploadPage = QuickCheckUiManager.GoToDocAnalyzerUploadPage(uploadOption);
            uploadPage.HeadingTextbox.SetText(legalIssue);
            uploadPage.PlainTextTextbox.SetText(text);
            SafeMethodExecutor.WaitUntil(() => uploadPage.StartAnalysisButton.Enabled);
            uploadPage.StartAnalysisButton.Click<QuickCheckFileUploadDialog>().WaitUntilFileUpload();
            return DriverExtensions.CreatePageInstance<TPage>();
        }

        /// <summary>
        /// The click enter plain text.
        /// </summary>
        /// <param name="uploadOption">
        /// The upload Option.
        /// </param>
        /// <returns>
        /// The <see cref="TextWithCitedAuthorityDialog"/>. 
        /// </returns>
        public static TextWithCitedAuthorityDialog OpenPlainTextDialog(WhatYouWouldLikeToDoOptions uploadOption = WhatYouWouldLikeToDoOptions.CheckYourWork)
        {
            QuickCheckUploadPage uploadPage = QuickCheckUiManager.GoToDocAnalyzerUploadPage(uploadOption);
            return uploadPage.EnterPlainTextLink.Click<TextWithCitedAuthorityDialog>();
        }

        /// <summary>
        /// Navigate to Judicial file upload page
        /// </summary>
        /// <returns>
        /// The <see cref="JudicialBaseUploadPage"/>. </returns>
        public static JudicialBaseUploadPage GoToJudicialUploadPage()
        {
            BrowserPool.CurrentBrowser.GoToUrl($"{QuickCheckUiManager.GetDomain()}{JudicialUrl}");

            return new JudicialBaseUploadPage();
        }

        /// <summary>
        /// Upload files to judicial and get report
        /// </summary>
        /// <param name="filePath">File path</param>
        /// <param name="fileNames">File names</param>
        /// <returns>Report Page</returns>
        public static JudicialRecommendationsPage UploadFilesAndGetReport(string filePath, params string[] fileNames)
        {
            JudicialAssignDocumentToPartyPage assignDocumentToPartiesPage =
                QuickCheckUiManager.UploadFilesToJudicial(filePath, fileNames);
            QuickCheckUiManager.AssignDocumentsToPartiesByDefault();
            return assignDocumentToPartiesPage.AssignDocumentsToPartiesComponent.GetReport();
        }

        /// <summary>
        /// Upload files to judicial
        /// </summary>
        /// <param name="filePath">File path</param>
        /// <param name="fileNames">File names</param>
        /// <returns>Report Page</returns>
        public static JudicialAssignDocumentToPartyPage UploadFilesToJudicial(string filePath, params string[] fileNames) =>
            QuickCheckUiManager.GoToJudicialUploadPage().UploadFiles(filePath, fileNames);

        /// <summary>
        /// Assign uploaded documents to parties
        /// 1 doc -> 1 party 1 slot
        /// </summary>
        public static void AssignDocumentsToPartiesByDefault()
        {
            int docSlotsPerParty = 3;
            int i = 0;
            var assignToPartyPage = new JudicialAssignDocumentToPartyPage();
            assignToPartyPage.UploadedFileItems.ForEach(
                el =>
                {
                    if (i < docSlotsPerParty)
                    {
                        el.AssignToByDragAndDrop(
                            assignToPartyPage.AssignDocumentsToPartiesComponent.GetAssignmentSlotsForParty(
                                JudicialParties.FirstParty)[i]);
                    }
                    else
                    {
                        el.AssignToByDragAndDrop(
                            assignToPartyPage.AssignDocumentsToPartiesComponent.GetAssignmentSlotsForParty(
                                JudicialParties.SecondParty)[i % docSlotsPerParty]);
                    }

                    i++;
                });
        }

        /// <summary>
        /// The assign documents to parties.
        /// </summary>
        /// <param name="filePath">
        /// The file path.
        /// </param>
        /// <param name="assignment">
        /// The assignment.
        /// </param>
        /// <param name="firstPartyName">The first party</param>
        /// <param name="secondPartyName">The second party</param>
        /// <returns>
        /// The <see cref="JudicialRecommendationsPage"/>.
        /// </returns>
        public static JudicialRecommendationsPage AssignDocumentsToPartiesAndGetReport(
            string filePath,
            Dictionary<string, JudicialParties> assignment,
            string firstPartyName = "",
            string secondPartyName = "")
        {
            string[] files = assignment.Keys.ToArray();

            JudicialAssignDocumentToPartyPage assignDocumentToPartiesPage =
                QuickCheckUiManager.UploadFilesToJudicial(filePath, files);

            foreach (KeyValuePair<string, JudicialParties> assignmentPair in assignment)
            {
                JudicialUploadedDocumentItem freeSlot = assignDocumentToPartiesPage
                                                        .AssignDocumentsToPartiesComponent
                                                        .GetAssignmentSlotsForParty(assignmentPair.Value)
                                                        .First(slot => !slot.Assigned);

                List<JudicialUploadedDocumentItem> uploadedItems = assignDocumentToPartiesPage.UploadedFileItems;

                uploadedItems.First(item => item.Name.Equals(assignmentPair.Key)).AssignToByDragAndDrop(freeSlot);
            }

            if (string.Empty != firstPartyName)
            {
                assignDocumentToPartiesPage.AssignDocumentsToPartiesComponent.FirstPartyNameTextBox.SetText(firstPartyName);
            }

            if (string.Empty != secondPartyName)
            {
                assignDocumentToPartiesPage.AssignDocumentsToPartiesComponent.SecondPartyNameTextBox.SetText(secondPartyName);
            }

            return assignDocumentToPartiesPage.AssignDocumentsToPartiesComponent.GetReport();
        }

        /// <summary>
        /// The assign documents to parties.
        /// </summary>
        /// <param name="filePath">
        /// The file path.
        /// </param>
        /// <param name="assignment">
        /// The assignment.
        /// </param>
        /// <param name="firstPartyName">The first party</param>
        /// <param name="secondPartyName">The second party</param>
        /// <returns>
        /// The <see cref="AdvantageJudicialRecommendationsPage"/>.
        /// </returns>
        public static AdvantageJudicialRecommendationsPage AssignDocumentsToPartiesAndGetReportInAdvantage(
            string filePath,
            Dictionary<string, JudicialParties> assignment,
            string firstPartyName = "",
            string secondPartyName = "")
        {
            string[] files = assignment.Keys.ToArray();

            JudicialAssignDocumentToPartyPage assignDocumentToPartiesPage =
                QuickCheckUiManager.UploadFilesToJudicial(filePath, files);

            foreach (KeyValuePair<string, JudicialParties> assignmentPair in assignment)
            {
                JudicialUploadedDocumentItem freeSlot = assignDocumentToPartiesPage
                                                        .AssignDocumentsToPartiesComponent
                                                        .GetAssignmentSlotsForParty(assignmentPair.Value)
                                                        .First(slot => !slot.Assigned);

                List<JudicialUploadedDocumentItem> uploadedItems = assignDocumentToPartiesPage.UploadedFileItems;

                uploadedItems.First(item => item.Name.Equals(assignmentPair.Key)).AssignToByDragAndDrop(freeSlot);
            }

            if (string.Empty != firstPartyName)
            {
                assignDocumentToPartiesPage.AssignDocumentsToPartiesComponent.FirstPartyNameTextBox.SetText(firstPartyName);
            }

            if (string.Empty != secondPartyName)
            {
                assignDocumentToPartiesPage.AssignDocumentsToPartiesComponent.SecondPartyNameTextBox.SetText(secondPartyName);
            }

            return assignDocumentToPartiesPage.AssignDocumentsToPartiesComponent.GetAdvantageReport();
        }

        /// <summary>
        /// Assign single document to party by drag and drop
        /// </summary>
        /// <param name="docName">Document name</param>
        /// <param name="party">Party name</param>
        /// <param name="index">Index of slot</param>
        public static void AssignSingleDocumentToPartyByDragAndDrop(string docName, JudicialParties party, int index)
        {
            var assignDocumentToPartyPage = new JudicialAssignDocumentToPartyPage();
            assignDocumentToPartyPage.UploadedFileItems.First(el => el.Name.Equals(docName)).AssignToByDragAndDrop(
                assignDocumentToPartyPage
                    .AssignDocumentsToPartiesComponent
                    .GetAssignmentSlotsForParty(party)[index]);
        }

        /// <summary>
        /// Assign single document to party by click
        /// todo: remove  from here
        /// </summary>
        /// <param name="docName">Document name</param>
        /// <param name="party">Party name</param>
        /// <param name="index">Index of slot</param>
        public static void AssignSingleDocumentToPartyByClick(string docName, JudicialParties party, int index)
        {
            var assignDocumentToPartyPage = new JudicialAssignDocumentToPartyPage();
            assignDocumentToPartyPage.UploadedFileItems.First(el => el.Name.Equals(docName)).AssignToByClick(
                assignDocumentToPartyPage
                    .AssignDocumentsToPartiesComponent
                    .GetAssignmentSlotsForParty(party)[index]);
        }

        /// <summary>
        /// Upload westlaw document to the Quick check
        /// </summary>
        /// <param name="docGuid">westlaw document guid</param>
        /// <param name="option">upload option</param>
        /// <returns>report page</returns>
        public static QuickCheckRecommendationsPage UploadWestlawDocumentToQuickCheck(string docGuid, WhatYouWouldLikeToDoOptions option = WhatYouWouldLikeToDoOptions.CheckYourWork)
        {
            var documentPage =
                EdgeNavigationManager.Instance.NavigateToDocumentDirectly<EdgeCommonDocumentPage>(docGuid);

            return documentPage.Toolbar.SubmitToQuickCheckToolbarComponent.QuickCheckButton
                        .Click<AnalyzeWithQuickCheckDialog>().UploadToQuickCheck(option);
        }

        private static void NavigateToDocAnalyzerEntryPoint(WhatYouWouldLikeToDoOptions uploadOption)
        {
            string targetUrl;

            switch (uploadOption)
            {
                case WhatYouWouldLikeToDoOptions.CheckYourWork:
                    targetUrl = $"{QuickCheckUiManager.GetDomain()}{CheckWorkUrl}";
                    break;
                case WhatYouWouldLikeToDoOptions.AnalyzeOpponents:
                    targetUrl = $"{QuickCheckUiManager.GetDomain()}{OpponentWorkUrl}";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(uploadOption), uploadOption, $"upload option {uploadOption} not supported");
            }

            BrowserPool.CurrentBrowser.GoToUrl(targetUrl);
        }

        /// <summary>
        /// Navigate to Complaint analyser landing page
        /// </summary>
        /// <returns>
        /// The <see cref="AiAnalyzeAComplaintPage"/>. </returns>
        public static AiAnalyzeAComplaintPage GoToComplaintAnalyserPage()
        {
            BrowserPool.CurrentBrowser.GoToUrl($"{QuickCheckUiManager.GetDomain()}{ComplaintAnalyserUrl}");

            return new AiAnalyzeAComplaintPage();
        }

        private static string GetDomain() =>
            BrowserPool.CurrentBrowser.Url.Substring(
                0,
                BrowserPool.CurrentBrowser.Url.IndexOf("/", 9, StringComparison.Ordinal));
    }
}