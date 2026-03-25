namespace WestlawPrecision.Tests.SeparateAthensFeature.ResearchOutlineBuilder
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Products.Shared.Dialogs.Document.Notes;
    using Framework.Common.UI.Products.Shared.Managers;
    using Framework.Common.UI.Products.WestlawEdge.Components.Preferences;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.HomePage;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.OutlineBuilder;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Preferences;
    using Framework.Common.UI.Utils.Browser;

    [TestClass]
    public class OutlineBuilderUserPreferenceTests : OutlineBuilderBaseTest
    {
        /// <summary>
        /// User Story 1674888: Outline Snippets honor 'Line options' of Copy w Reference preferences
        /// 1. Sign in WLP and set user pref Line Option: ref on separate line following text
        /// 2. Run unique search to get to a Case document: 490 F.3d 143 
        /// 3. Open Outline right panel and create outline: Test Preference Outline
        /// 4. Select text: Hill v. City of New York, 
        /// 5. From popup menu select: Add to Outline, and save outline
        /// 6. Check: Outline has reference info following text
        /// 7. Delete outline and run the unique search again
        /// 8. Select user pref Line Option: ref on separate line preceding text
        /// 9. Select text: Hill v. City of New York,
        /// 10.Add to Outline and save
        /// 11.Check: Outline has reference info preceding text
        /// 12.Go back to zero state by deleting outline
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        public void LineOptionsTest()
        {
            const string UniqueFindQuery = "490 F.3d 143";
            const string OutlineTitle = "Test Preference Outline";
            const string CitationLinkText = "Hill v. City of New York,";

            var preferencesDialog = this.GetHomePage<PrecisionHomePage>().Header.OpenProfileSettingsDialog().ClickWestlawPreferences<EdgePreferencesDialog>();
            var copyTabComponent = preferencesDialog.TabPanel.SetActiveTab<EdgeCopyWithReferenceTabComponent>(EdgePreferencesDialogTabs.Citations);
            copyTabComponent.SetCheckbox(EdgeCopyWithReferenceTab.AddQuotationsAroundCopiedText, false);
            copyTabComponent.ToggleRadioButton(EdgeCopyWithReferenceTab.ReferenceSeparateLineFollowingText);
            preferencesDialog.SaveButton.Click<EdgeHomePage>();

            // Go to Document by running a unique search
            var document = GetHomePage<PrecisionHomePage>().Header.EnterSearchQueryAndClickSearch<EdgeCommonDocumentPage>(UniqueFindQuery);
            document.RightPanel.Toggle.ToggleState<EdgeCommonDocumentPage>(false);

            // Create Outline 
            document.RightPanel.OutlineBuilderPanel.OutlinesPanelButton.Click<EdgeCommonDocumentPage>();
            document = CreateNewOutline(document, OutlineTitle);

            HighlightMenuDialog highlightMenu = document.SelectText(CitationLinkText);
            var addToOutlineDialog = highlightMenu.AddToOutlinesButton.Click<AddToOutlineDialog>();
            document = addToOutlineDialog.SaveOutlineButton.Click<EdgeCommonDocumentPage>();
            document.RightPanel.Toggle.ToggleState<EdgeCommonDocumentPage>(true);
            string outlineText = document.RightPanel.OutlineBuilderPanel.OutlineInternalRightPanel
                                         .OutlineSnippetNodeLabel;

            // Verify Reference on separate line following text
            this.TestCaseVerify.IsTrue("Reference on separate line following text",
                outlineText.StartsWith(CitationLinkText),
                "Reference on separate line following text is not correct");
            
            document.RightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel
                    .BackToListOfOutlinesButton.Click<EdgeCommonDocumentPage>();
            document.RightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel.ListOfOutlines.First()
                               .DeleteOutline<EdgeCommonDocumentPage>();

            // Go to Document by running a unique search
            document = GetHomePage<PrecisionHomePage>().Header.EnterSearchQueryAndClickSearch<EdgeCommonDocumentPage>(UniqueFindQuery);
            document.RightPanel.Toggle.ToggleState<EdgeCommonDocumentPage>(false);

            preferencesDialog = this.GetHomePage<PrecisionHomePage>().Header.OpenProfileSettingsDialog().ClickWestlawPreferences<EdgePreferencesDialog>();
            copyTabComponent = preferencesDialog.TabPanel.SetActiveTab<EdgeCopyWithReferenceTabComponent>(EdgePreferencesDialogTabs.Citations);
            copyTabComponent.ToggleRadioButton(EdgeCopyWithReferenceTab.ReferenceSeparateLinePrecedingText);
            preferencesDialog.SaveButton.Click<EdgeHomePage>();

            // Create Outline 
            document = CreateNewOutline(document, OutlineTitle);

            highlightMenu = document.SelectText(CitationLinkText);
            addToOutlineDialog = highlightMenu.AddToOutlinesButton.Click<AddToOutlineDialog>();
            document = addToOutlineDialog.SaveOutlineButton.Click<EdgeCommonDocumentPage>();
            document.RightPanel.Toggle.ToggleState<EdgeCommonDocumentPage>(true);
            outlineText = document.RightPanel.OutlineBuilderPanel.OutlineInternalRightPanel
                                         .OutlineSnippetNodeLabel;

            // Verify Reference on separate line preceding text
            this.TestCaseVerify.IsTrue("Reference on separate line preceding text",
                outlineText.EndsWith(CitationLinkText),
                "Reference on separate line preceding text is not correct");
        }

        /// <summary>
        /// User Stories 1674886 and 1676151: Outline Citations honor "Add" of Copy with Reference
        /// 1. Sign in WLP and set user pref Line Option: ref on separate line following text
        /// 2. Check user pref Add: Quotations around copied text
        /// 3. Run unique search to get to a Case document: 490 F.3d 143 
        /// 4. Open Outline right panel and create outline: Test Preference Outline
        /// 5. Select text: Hill v. City of New York, 
        /// 6. From popup menu select: Add to Outline, and save outline
        /// 7. Check: Quotations displayed around copied text
        /// 8. Delete outline and run the unique search again
        /// 9. Uncheck user pref Add: Quotations around copied text
        /// 10.Select text: Hill v. City of New York,
        /// 11.Add to Outline and save
        /// 12.Check: Quotations not displayed around copied text
        /// 13.Go back to zero state by deleting outline
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        public void AddQuotationsTest()
        {
            const string UniqueFindQuery = "490 F.3d 143";
            const string OutlineTitle = "AddQuotationsTest";
            const string CitationLinkText = "Hill v. City of New York,";

            BrowserPool.CurrentBrowser.Maximize();
            var preferencesDialog = this.GetHomePage<PrecisionHomePage>().Header.OpenProfileSettingsDialog().ClickWestlawPreferences<EdgePreferencesDialog>();
            var copyTabComponent = preferencesDialog.TabPanel.SetActiveTab<EdgeCopyWithReferenceTabComponent>(EdgePreferencesDialogTabs.Citations);
            copyTabComponent.SetCheckbox(EdgeCopyWithReferenceTab.AddQuotationsAroundCopiedText, true);
            copyTabComponent.ToggleRadioButton(EdgeCopyWithReferenceTab.ReferenceSeparateLineFollowingText);
            preferencesDialog.SaveButton.Click<EdgeHomePage>();

            // Go to Document by running a unique search
            var document = GetHomePage<PrecisionHomePage>().Header.EnterSearchQueryAndClickSearch<EdgeCommonDocumentPage>(UniqueFindQuery);
            document.RightPanel.Toggle.ToggleState<EdgeCommonDocumentPage>(false);

            // Create Outline 
            document.RightPanel.OutlineBuilderPanel.OutlinesPanelButton.Click<EdgeCommonDocumentPage>();
            document = CreateNewOutline(document, OutlineTitle);

            HighlightMenuDialog highlightMenu = document.SelectText(CitationLinkText);
            var addToOutlineDialog = highlightMenu.AddToOutlinesButton.Click<AddToOutlineDialog>();
            document = addToOutlineDialog.SaveOutlineButton.Click<EdgeCommonDocumentPage>();
            document.RightPanel.Toggle.ToggleState<EdgeCommonDocumentPage>(true);
            string outlineText = document.RightPanel.OutlineBuilderPanel.OutlineInternalRightPanel
                                         .OutlineSnippetNodeLabel;

            // Verify quotations displayed around copied text
            this.TestCaseVerify.IsTrue("Quotations displayed around copied text when Add is checked in Preference",
                outlineText.StartsWith("\"" + CitationLinkText),
                "Quotations not displayed around copied text");
                        
            document.RightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel
                    .BackToListOfOutlinesButton.Click<EdgeCommonDocumentPage>();
            document.RightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel.ListOfOutlines.First()
                               .DeleteOutline<EdgeCommonDocumentPage>();

            // Go to Document by running a unique search
            document = GetHomePage<PrecisionHomePage>().Header.EnterSearchQueryAndClickSearch<EdgeCommonDocumentPage>(UniqueFindQuery);
            document.RightPanel.Toggle.ToggleState<EdgeCommonDocumentPage>(false);

            preferencesDialog = this.GetHomePage<PrecisionHomePage>().Header.OpenProfileSettingsDialog().ClickWestlawPreferences<EdgePreferencesDialog>();
            copyTabComponent = preferencesDialog.TabPanel.SetActiveTab<EdgeCopyWithReferenceTabComponent>(EdgePreferencesDialogTabs.Citations);
            copyTabComponent.SetCheckbox(EdgeCopyWithReferenceTab.AddQuotationsAroundCopiedText, false);
            copyTabComponent.ToggleRadioButton(EdgeCopyWithReferenceTab.ReferenceSeparateLineFollowingText);
            preferencesDialog.SaveButton.Click<EdgeHomePage>();

            // Create Outline 
            document = CreateNewOutline(document, OutlineTitle);

            highlightMenu = document.SelectText(CitationLinkText);
            addToOutlineDialog = highlightMenu.AddToOutlinesButton.Click<AddToOutlineDialog>();
            document = addToOutlineDialog.SaveOutlineButton.Click<EdgeCommonDocumentPage>();
            document.RightPanel.Toggle.ToggleState<EdgeCommonDocumentPage>(true);
            outlineText = document.RightPanel.OutlineBuilderPanel.OutlineInternalRightPanel
                                         .OutlineSnippetNodeLabel;

            // Verify quotations not displayed around copied text
            this.TestCaseVerify.IsFalse("Quotations not displayed around copied text when Add is unchecked in Preference",
                outlineText.StartsWith("\"" + CitationLinkText),
                "Quotations should not display around copied text");
        }

        /// <summary>
        /// Bug 1756035: Precision: ROB - User preferences not honored after Citation format changes
        /// 1. Sign in WLP and go to Preferences
        /// 2. Set Copy Citation preferences: Citation format = standard; Citation style = Law reviews
        /// 3. View document by guid: I751c06ea916411ebbea4f0dc9fb69570
        /// 4. Open Outline right panel and create outline: Test Preference Outline
        /// 5. Select text: Following the European model of the General Data Protection Regulation
        /// 6. From popup menu select: Add to Outline, and save outline
        /// 7. Check: expected text in small caps: Cybaris An Intell. Prop. L. Rev.
        /// 8. Delete outline and create an outline again
        /// 9. Select the same text and click Add to Outline
        /// 10.On the Add dialog window, change Citation format to Westlaw and back to Standard again
        /// 11.Click Save and finish adding text to outline
        /// 12.Check: expected text in small caps: Cybaris An Intell. Prop. L. Rev. (after format changes step #10)
        /// 13.Go back to zero state by deleting outline
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        public void HonorPreferenceSettingWithChangedFormatTest()
        {
            const string LawReviewDoc = "I751c06ea916411ebbea4f0dc9fb69570";
            const string CitationFormatStandard = "Standard";
            const string CitationFormatWestlaw = "Westlaw";
            const string OutlineTitle = "Test Preference Outline";
            const string TextToAdd = "Following the European model of the General Data Protection Regulation";
            const string TextExpectedInSmallCaps = "Cybaris An Intell. Prop. L. Rev.";

            var preferencesDialog = this.GetHomePage<PrecisionHomePage>().Header.OpenProfileSettingsDialog().ClickWestlawPreferences<EdgePreferencesDialog>();
            var copyTabComponent = preferencesDialog.TabPanel.SetActiveTab<EdgeCopyWithReferenceTabComponent>(EdgePreferencesDialogTabs.Citations);
            copyTabComponent.SetDropdown(EdgeCopyWithReferenceTab.CitationFormat, CitationFormatStandard);
            copyTabComponent.ToggleRadioButton(EdgeCopyWithReferenceTab.LawReviews);
            preferencesDialog.SaveButton.Click<EdgeHomePage>();

            // Navigate to Document by doc guid
            var document = EdgeNavigationManager.Instance.NavigateToDocumentDirectly<EdgeCommonDocumentPage>(LawReviewDoc);
            document.RightPanel.Toggle.WaitEnabled(2000);
            document.RightPanel.Toggle.ToggleState<EdgeCommonDocumentPage>(false);

            // Create Outline 
            document.RightPanel.OutlineBuilderPanel.OutlinesPanelButton.Click<EdgeCommonDocumentPage>();
            document = CreateNewOutline(document, OutlineTitle);

            HighlightMenuDialog highlightMenu = document.SelectText(TextToAdd);
            var addToOutlineDialog = highlightMenu.AddToOutlinesButton.Click<AddToOutlineDialog>();
            document = addToOutlineDialog.SaveOutlineButton.Click<EdgeCommonDocumentPage>();
            document.RightPanel.Toggle.ToggleState<EdgeCommonDocumentPage>(true);

            // Verify Law Review style with Standard format set from Preferences
            this.TestCaseVerify.IsTrue("Check Law Review style with Standard format set from Preferences",
                document.RightPanel.OutlineBuilderPanel.OutlineInternalRightPanel.IsTextInSmallCaps(TextExpectedInSmallCaps),
                "Law Review style does not look correct when setting from Preferences");
                        
            document.RightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel
                    .BackToListOfOutlinesButton.Click<EdgeCommonDocumentPage>();
            document.RightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel.ListOfOutlines.First()
                               .DeleteOutline<EdgeCommonDocumentPage>();

            // Navigate to Document by doc guid
            document = EdgeNavigationManager.Instance.NavigateToDocumentDirectly<EdgeCommonDocumentPage>(LawReviewDoc);
            document.RightPanel.Toggle.ToggleState<EdgeCommonDocumentPage>(true);

            // Create Outline 
            document = CreateNewOutline(document, OutlineTitle);

            highlightMenu = document.SelectText(TextToAdd);
            addToOutlineDialog = highlightMenu.AddToOutlinesButton.Click<AddToOutlineDialog>();
            addToOutlineDialog = addToOutlineDialog.CitationFormatDropdown.SelectOption<AddToOutlineDialog>(CitationFormatWestlaw);
            //Some user preference settings got dropped at this point
            addToOutlineDialog = addToOutlineDialog.CitationFormatDropdown.SelectOption<AddToOutlineDialog>(CitationFormatStandard);
            document = addToOutlineDialog.SaveOutlineButton.Click<EdgeCommonDocumentPage>();
            document.RightPanel.Toggle.ToggleState<EdgeCommonDocumentPage>(true);

            // Verify Law Review style with Standard format set from Add to Outline dialog
            this.TestCaseVerify.IsTrue("Check Law Review style with Standard format set from Add dialog",
                document.RightPanel.OutlineBuilderPanel.OutlineInternalRightPanel.IsTextInSmallCaps(TextExpectedInSmallCaps),
                "Law Review style does not look correct when setting from Add dialog");
        }
    }
}

