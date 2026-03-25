namespace Framework.Common.Api.Utilities
{
    using System;
    using System.Linq;
    using System.Text;

    using Framework.Common.Api.Endpoints.Document.DataModel;

    /// <summary>
    /// The delivery utilities.
    /// </summary>
    public static class DeliveryUtilities
    {
        /// <summary>
        /// The assemble fo request.
        /// </summary>
        /// <param name="uriRequestJson">
        /// The uri request json.
        /// </param>
        /// <param name="foPrivateData">
        /// The fo private data.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string AssembleFoRequest(string uriRequestJson, string foPrivateData)
        {
            string foRequestJson = @"{
            ""DeliveryDescription"":" + uriRequestJson + @", 
            ""DocumentDescription"":{""DocumentTitle"": """ + string.Empty + @""", 
            ""CoverPageHeaderFooterFO"":{""Portrait"":""<!-- header --><fo:static-content flow-name=\""xsl-region-after\"" font-family=\""Arial, Helvetica\"" font-size=\""10px\"" margin-left=\""36px\"" margin-right=\""36px\"" margin-bottom=\""43px\""><fo:table table-layout=\""fixed\"" margin-top=\""0px\"" space-before=\""0px\"" margin-right=\""0px\"" margin-bottom=\""0px\"" space-after=\""0px\"" margin-left=\""0px\"" font-size=\""100%\""><fo:table-column column-width=\""465px\""/><fo:table-column column-width=\""35px\""/><fo:table-body start-indent=\""0pt\"" end-indent=\""0pt\"" text-indent=\""0pt\"" last-line-end-indent=\""0pt\"" text-align=\""start\""><fo:table-row><fo:table-cell padding-top=\""20px\"" padding-bottom=\""3px\"" margin-top=\""0px\"" space-before=\""0px\"" space-after=\""0px\"" display-align=\""after\"" border-bottom-width=\""1px\"" border-bottom-style=\""solid\"" border-bottom-color=\""#AAAAAA\"" border=\""none\""><fo:block>INSERTED HEADER<fo:inline color=\""#555555\"" font-weight=\""bold\"">Boumediene v. Bush, 128 S.Ct. 2229</fo:inline></fo:block></fo:table-cell></fo:table-row></fo:table-body></fo:table></fo:static-content><!-- end of header --><!-- footer --><fo:static-content flow-name=\""xsl-region-after\"" font-family=\""Arial, Helvetica\"" font-size=\""9px\"" margin-left=\""36px\"" margin-right=\""36px\"" margin-bottom=\""43px\""><fo:table table-layout=\""fixed\"" margin-top=\""0px\"" space-before=\""0px\"" margin-right=\""0px\"" margin-bottom=\""0px\"" space-after=\""0px\"" margin-left=\""0px\"" font-size=\""100%\""><fo:table-column column-width=\""465px\""/><fo:table-column column-width=\""35px\""/><fo:table-body start-indent=\""0pt\"" end-indent=\""0pt\"" text-indent=\""0pt\"" last-line-end-indent=\""0pt\"" text-align=\""start\""><fo:table-row><fo:table-cell padding-top=\""3px\"" margin-top=\""0px\"" space-before=\""0px\"" space-after=\""43px\"" display-align=\""after\"" border-top-width=\""1px\"" border-top-style=\""solid\"" border-top-color=\""#AAAAAA\"" border=\""none\""><fo:block color=\""#AAAAAA\"">INSERTED FOOTER<fo:external-graphic src=\""url('http://images.ci.westlaw.com/StaticContent_7.5.359/images/v2/small_logoForDeliveredDocument.gif')\"" role=\""Westlaw Logo\"" content-width=\""78px\"" content-height=\""10px\""/>&#169; 2009 Thomson Reuters/West. No Claim to Orig. US Gov. Works.</fo:block></fo:table-cell><fo:table-cell padding-top=\""3px\"" margin-top=\""0px\"" space-before=\""0px\"" text-align=\""right\"" border-top-width=\""1px\"" border-top-style=\""solid\"" border-top-color=\""#AAAAAA\"" border=\""none\""><fo:block color=\""#AAAAAA\""><fo:page-number/></fo:block></fo:table-cell></fo:table-row></fo:table-body></fo:table></fo:static-content><!-- end of footer -->""},
            ""HeaderFooterFO"":{""Portrait"":""<!-- header --><fo:static-content flow-name=\""xsl-region-after\"" font-family=\""Arial, Helvetica\"" font-size=\""10px\"" margin-left=\""36px\"" margin-right=\""36px\"" margin-bottom=\""43px\""><fo:table table-layout=\""fixed\"" margin-top=\""0px\"" space-before=\""0px\"" margin-right=\""0px\"" margin-bottom=\""0px\"" space-after=\""0px\"" margin-left=\""0px\"" font-size=\""100%\""><fo:table-column column-width=\""465px\""/><fo:table-column column-width=\""35px\""/><fo:table-body start-indent=\""0pt\"" end-indent=\""0pt\"" text-indent=\""0pt\"" last-line-end-indent=\""0pt\"" text-align=\""start\""><fo:table-row><fo:table-cell padding-top=\""20px\"" padding-bottom=\""3px\"" margin-top=\""0px\"" space-before=\""0px\"" space-after=\""0px\"" display-align=\""after\"" border-bottom-width=\""1px\"" border-bottom-style=\""solid\"" border-bottom-color=\""#AAAAAA\"" border=\""none\""><fo:block>INSERTED HEADER<fo:inline color=\""#555555\"" font-weight=\""bold\"">Boumediene v. Bush, 128 S.Ct. 2229</fo:inline></fo:block></fo:table-cell></fo:table-row></fo:table-body></fo:table></fo:static-content><!-- end of header --><!-- footer --><fo:static-content flow-name=\""xsl-region-after\"" font-family=\""Arial, Helvetica\"" font-size=\""9px\"" margin-left=\""36px\"" margin-right=\""36px\"" margin-bottom=\""43px\""><fo:table table-layout=\""fixed\"" margin-top=\""0px\"" space-before=\""0px\"" margin-right=\""0px\"" margin-bottom=\""0px\"" space-after=\""0px\"" margin-left=\""0px\"" font-size=\""100%\""><fo:table-column column-width=\""465px\""/><fo:table-column column-width=\""35px\""/><fo:table-body start-indent=\""0pt\"" end-indent=\""0pt\"" text-indent=\""0pt\"" last-line-end-indent=\""0pt\"" text-align=\""start\""><fo:table-row><fo:table-cell padding-top=\""3px\"" margin-top=\""0px\"" space-before=\""0px\"" space-after=\""43px\"" display-align=\""after\"" border-top-width=\""1px\"" border-top-style=\""solid\"" border-top-color=\""#AAAAAA\"" border=\""none\""><fo:block color=\""#AAAAAA\"">INSERTED FOOTER<fo:external-graphic src=\""url('http://images.ci.westlaw.com/StaticContent_7.5.359/images/v2/small_logoForDeliveredDocument.gif')\"" role=\""Westlaw Logo\"" content-width=\""78px\"" content-height=\""10px\""/>&#169; 2009 Thomson Reuters/West. No Claim to Orig. US Gov. Works.</fo:block></fo:table-cell><fo:table-cell padding-top=\""3px\"" margin-top=\""0px\"" space-before=\""0px\"" text-align=\""right\"" border-top-width=\""1px\"" border-top-style=\""solid\"" border-top-color=\""#AAAAAA\"" border=\""none\""><fo:block color=\""#AAAAAA\""><fo:page-number/></fo:block></fo:table-cell></fo:table-row></fo:table-body></fo:table></fo:static-content><!-- end of footer -->""}}, "
                                   + foPrivateData.Trim(new[] { '{', '}' }) + "}";
            return foRequestJson;
        }

        /// <summary>
        /// The get uri request json.
        /// </summary>
        /// <param name="options">
        /// The options.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetUriRequestJson(FoOptions options)
        {
            // some of these options are used for document features, but we don't use them in our tests
            const bool ListAsFullText = true;
            const bool IncludeDocWithRiLists = false;
            string parentDocumentGuid = string.Empty;

            string selectedItems = options.DocGuids.Aggregate(
                string.Empty,
                (current, d) =>
                    current
                    + (@"{""ContentType"":""Unknown"",""DocumentGuid"":""" + d
                       + @""",""NovusSearchHandle"":"""",""ItemType"":""CobaltDocument"",""RenderType"":""FullText""},"));

            selectedItems = selectedItems.TrimEnd(new[] { ',' });

            string terms =
                options.SearchTerms.Aggregate(string.Empty, (current, s) => current + ("\"" + s + "\","))
                       .TrimEnd(new[] { ',' });

            var sb = new StringBuilder();

            sb.Append(@"{
                ""Version"": 8,
                    ""PageDimensions"": {
                      ""Portrait"": {
                        ""PageHeight"": 1056,
                        ""PageWidth"": 816,
                        ""TopMargin"": 96,
                        ""BottomMargin"": 96,
                        ""LeftMargin"": 96,
                        ""RightMargin"": 96
                      },
                      ""PortraitRightNoteMargin"": {
                        ""PageHeight"": 1056,
                        ""PageWidth"": 816,
                        ""TopMargin"": 96,
                        ""BottomMargin"": 96,
                        ""LeftMargin"": 96,
                        ""RightMargin"": 144
                      },
                      ""PortraitDualColumn"": {
                        ""PageHeight"": 1056,
                        ""PageWidth"": 816,
                        ""TopMargin"": 96,
                        ""BottomMargin"": 96,
                        ""LeftMargin"": 96,
                        ""RightMargin"": 96
                      },
                      ""PortraitDualColumnRightNoteMargin"": {
                        ""PageHeight"": 1056,
                        ""PageWidth"": 816,
                        ""TopMargin"": 96,
                        ""BottomMargin"": 96,
                        ""LeftMargin"": 96,
                        ""RightMargin"": 144
                      },
                      ""Landscape"": {
                        ""PageHeight"": 816,
                        ""PageWidth"": 1056,
                        ""TopMargin"": 96,
                        ""BottomMargin"": 96,
                        ""LeftMargin"": 96,
                        ""RightMargin"": 96
                      }
                    },
                    ""Metadata"":{
                      ""Version"": 2,
                      ""UserGuid"": ""ia74483420000011939ebd10d4dc3e282"",
                      ""ClientId"": ""fakeClientId"",
                      ""OriginatorEmail"": ""fakeEmail"",
                      ""SenderFirstName"": ""fakeSenderFirstName"",
                      ""SenderLastName"": ""fakeSenderLastName"",
                      ""RequestTimeUtc"": """ + DateTime.Now.ToString() + @""",
                      ""RequestTimeCoverPageString"":""" + DateTime.Now.ToString() + @""",
                      ""TimeZone"":""Pacific Standard Time""
                    },
                    ""RequestContext"": {
                      ""Version"": 4,
                      ""Medium"": ""Download"",
                      ""TocDocumentTitle"":null,
                      ""ContextView"": """ + options.ContextView + @""",
                      ""DocumentContentType"": ""Case"",
                      ""DocumentGuid"": null, 
                      ""ParentDocumentGuid"": """ + parentDocumentGuid + @""",
                      ""UserSearchQueryString"": null,
                      ""SearchTermsToHighlight"":[" + terms + @"],
                      ""SearchTermHighlighting"": {
                              ""HighlightedTerms"":[" + terms + @" ],
                              ""SecondaryHighlightedTerms"":[]
                      },
                      ""SearchTermsToHighlightUrl"": ""Search/v2/result/standard/navigation/i0adc18f100000121886a0dc29bbd5ec8"",
                      ""RelatedInfoCategory"": null,
                      ""UserTextHighlighted"": false,
                      ""UserMadeExplicitSelections"": false,
                      ""TotalListCount"": 0,
                      ""SelectedItems"": [" + selectedItems + @"],
                      ""ContextProviderData"": null,
                      ""PageContextData"": ""testContext"",
                      ""HasKMCite"":" + options.KmFlag.ToString().ToLower() + @"
                    },
                    ""Options"": {
                       ""Version"":3,
                       ""WebsiteRequestId"":""9a500035-1818-4cb9-bd16-a16770e68d7f"",
                       ""EmailAndPrint"": false,
                       ""Format"": """ + options.Format + @""",
                       ""RequestTimeUtc"":""2008-04-12T12:53:00Z"",
                       ""RecipientOptions"": {
                              ""EmailAddresses"":[""test1@email.com"",""test2@email.com""],
                              ""EmailSubject"":""MyEmailSubject"",
                              ""Comment"":""MyComment""
                       },
                       ""AnnotationsSelection"": """ + options.AnnotationsSelection + @""",
                       ""ListAsFullText"": " + ListAsFullText.ToString().ToLower() + @",
                       ""ItemsToDeliver"": 2,
                       ""LayoutOptions"": {
                              ""RightNoteMargin"": " + options.RightNoteMargin.ToString().ToLower() + @",
                              ""WestlawLinks"": false,
                              ""TermHighlighting"": " + options.TermHiglighting.ToString().ToLower() + @",
                              ""CoverPage"": " + options.CoverPage.ToString().ToLower() + @", 
                              ""SnippetsWithinLists"": true,
                              ""CoverPageComment"":" + @"""" + options.CoverPageComment + @"""" + @",
                              ""Headnotes"": """ + options.HeadnotesLayout + @""",
                              ""OriginalImageLink"" : " + options.OriginalImageLink.ToString().ToLower() + @",
                              ""KeyCiteTreatment"" : " + options.KeyCiteTreatment.ToString().ToLower() + @",
                              ""SelectedFootnotesFormat"": """ + options.FootnotesFormat + @"""
            
                       },
                       ""ContentLimitOptions"": {
                              ""OnlyPagesWithSearchTerms"": " + options.PagesWithTerms.ToString().ToLower() + @",
                              ""StarPages"": ""25-35"",
                              ""DualColumnsForCases"": " + options.DualColumn.ToString().ToLower() + @",
                              ""SynopsisAndHeadnotesOnly"": false,
                              ""StatutoryTextOnly"": true
                       },
                       ""AdditionalContentOptions"": {
                            ""IsKeyCiteWarningNeeded"": false,
                            ""CitingReferencesLimit"": 500,
                            ""StatutesAffectedLimit"": 0,
                            ""Credits"": false,
                            ""FilingsLimit"": 100,
                            ""AppellateHistoryLimit"": 0,
                            ""TopicsLimit"": 10,
                            ""VersionsLimit"": 100,
                            ""AppellateHistoryDiagram"": false,
                            ""History"": false,
                            ""References"": false,
                            ""CitingReferences"": false,
                            ""RelatedOpinionsLimit"": 0,
                            ""BillDrafts"": false,
                            ""NotesOfDecisionsLimit"": 0,
                            ""CreditsLimit"": 0,
                            ""ReportsAndRelatedMaterialsLimit"": 0,
                            ""ExcludeCourtDocsFromCitingReferences"": false,
                            ""Versions"": false,
                            ""RelatedOpinions"": false,
                            ""HistoryLimit"": 0,
                            ""Topics"": false,
                            ""IncludeDocWithRILists"": " + IncludeDocWithRiLists.ToString().ToLower() + @",
                            ""CrossReferences"": false,
                            ""NegativeTreatments"": false,
                            ""NegativeTreatmentsLimit"": 0,
                            ""ReferencesLimit"": 0,
                            ""AppellateHistory"": false,
                            ""LegislativeHistoryNotesLimit"": 0,
                            ""LegislativeHistoryNotes"": false,
                            ""ReportsAndRelatedMaterials"": false,
                            ""CrossReferencesLimit"": 0,
                            ""BillDraftsLimit"": 0,
                            ""Filings"": false,
                            ""NotesOfDecisions"": false,
                            ""StatutesAffected"": false            
                       }
                     }
                  }");

            return sb.ToString();
        }
    }
}