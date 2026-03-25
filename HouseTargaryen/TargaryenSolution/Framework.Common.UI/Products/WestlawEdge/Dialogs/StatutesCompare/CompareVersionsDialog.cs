namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.StatutesCompare
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.WestlawEdge.Components.Document;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The compare versions dialog.
    /// </summary>
    public class CompareVersionsDialog : BaseModuleRegressionDialog
    {
        private static readonly By CloseIconButtonLocator = By.Id("co_statuteCompare_closeImage");

        private static readonly By DeletionsAndAdditionsClassLocator = By.ClassName("co_statutesCompare_count");

        private static readonly By DeliveryDropdownContainerLocator = By.XPath("//div[@id='co_co_statuteCompare_deliveryContainer']");

        private static readonly By DocBodyTitleLocator = By.XPath("(//div[@class='co_documentHead']/h2[@class='co_title'])[2]");

        private static readonly By FooterChunkNumberLocator
            = By.XPath("//div[contains(@class,'co_statutesCompare_content')]/following-sibling::div//input[@class='co_compareCurrentChunkStatus']");

        private static readonly By InfoBoxIconLocator = By.XPath("//div[@class='CompareKey-info']//span[@class='co_moreInfo']");

        private static readonly By InfoBoxMessageLocator = By.XPath("//div[@class='CompareKey-info']//div[@class='co_infoBox_message']");

        private static readonly By TextViewCheckboxLocator = By.XPath("//input[@id ='co_statuteCompare_highlight_toggle']");

        private static readonly By TitleLocator = By.ClassName("co_statutesCompare_subTitle");

        private static readonly By TotalDifferenceLocator = By.ClassName("co_totalNavigationElements");

        private static readonly By CompareVersionsDocumentLocator = By.CssSelector(".co_statutesCompare_content #co_document");

        /// <summary>
        /// Initializes a new instance of the <see cref="CompareVersionsDialog"/> class.
        /// </summary>
        public CompareVersionsDialog()
        {
            DriverExtensions.WaitForCondition(condition => !DriverExtensions.WaitForElement(TotalDifferenceLocator).Text.Equals("Total: 0 differences"), 10000);
        }

        /// <summary>
        /// Gets chunk component.
        /// </summary>
        public CompareDocumentsChunkComponent Chunking { get; } = new CompareDocumentsChunkComponent();

        /// <summary>
        /// Gets chunk component.
        /// </summary>
        public CompareKeyLineComponent CompareKeyLine { get; } = new CompareKeyLineComponent();

        /// <summary>
        /// Gets document comparsion component.
        /// </summary>
        public CompareDocumentsComponent DocumentComparsion { get; } = new CompareDocumentsComponent();

        /// <summary>
        /// Gets document Differences component.
        /// </summary>
        public CompareDocumentDifferenceComponent Differences { get; } = new CompareDocumentDifferenceComponent();

        /// <summary>
        /// Gets the delivery dropdown.
        /// </summary>
        public DeliveryDropdown DeliveryDropdown => new DeliveryDropdown(DeliveryDropdownContainerLocator);

        /// <summary>
        /// Verifies that all pages the additions is highlighted.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if all pages the additions is highlighted. </returns>
        public bool AreAllPagesAdditionsHighlighted() => this.VerifyMarkUpsForAllChunks(()=> this.DocumentComparsion.AreAllAdditionsHighlighted());

        /// <summary>
        /// Verifies that all pages the deletions has strikethrough.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if all pages the additions is highlighted. </returns>
        public bool AreAllPagesDeletionsStrikethrough() => this.VerifyMarkUpsForAllChunks(() => this.DocumentComparsion.AreAllDeletionsStrikethrough());         

        /// <summary>
        /// Verifies that the additions is text view.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if the additions has text view. </returns>
        public bool AreAllAdditionsTextView() => this.AreAllTextView("+");
           
        /// <summary>
        /// Verifies that the deletions has text view.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if the deletions has text view. </returns>
        public bool AreAllDeletionsTextView() => this.AreAllTextView("-");

        /// <summary>
        /// Verifies that all pages the additions is text view.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if all pages the additions has text view. </returns>
        public bool AreAllPagesAdditionsTextView() => this.VerifyMarkUpsForAllChunks(() => this.AreAllAdditionsTextView());
            
        /// <summary>
        /// Verifies that all pages the deletions has text view.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if all pages the deletions has text view. </returns>
        public bool AreAllPagesDeletionsTextView() => this.VerifyMarkUpsForAllChunks(() => this.AreAllDeletionsTextView());

        /// <summary>
        /// Clicks close icon.
        /// </summary>
        /// <typeparam name="T"> T page </typeparam>
        /// <returns> T page </returns>
        public T ClickCloseIcon<T>() where T : ICreatablePageObject
            => this.ClickElement<T>(CloseIconButtonLocator);

        /// <summary>
        /// Turn highlights toggle
        /// </summary>
        /// <param name="turnOn">True to turn on, false - turn off.</param>
        /// <returns>New instance of CompareVersionsDialog.</returns>
        public CompareVersionsDialog TurnHighlightsToggle(bool turnOn = true)
        {
            DriverExtensions.SetCheckbox(TextViewCheckboxLocator, turnOn);
            return DriverExtensions.CreatePageInstance<CompareVersionsDialog>();
        }
                
        /// <summary>
        /// Clicks by difference(Numeration starts from 1).
        /// </summary>
        /// <param name="differenceNumber"> The difference number. </param>
        /// <returns> The <see cref="CompareVersionsDialog"/>. </returns>
        public CompareVersionsDialog ClickByDifference(int differenceNumber)
        {
            this.DocumentComparsion.GetListOfDifferences()[differenceNumber - 1].Click();
            return this;
        }

        /// <summary>
        /// Clicks info box icon.
        /// </summary>
        /// <returns> The <see cref="CompareVersionsDialog"/>. </returns>
        public CompareVersionsDialog ClickInfoBoxIcon()
        {
            DriverExtensions.GetElement(InfoBoxIconLocator).Click();
            return this;
        }

        /// <summary>
        /// Gets document title 
        /// </summary>
        /// <returns>document title</returns>
        public string GetDocumentTitleText() => DriverExtensions.GetText(DocBodyTitleLocator);

        /// <summary>
        /// Gets the title text.
        /// </summary>
        /// <returns> The <see cref="string"/>. The key line text. </returns>
        public string GetTitleText() => DriverExtensions.GetText(TitleLocator);

        /// <summary>
        /// Gets counts of additions and deletions.
        /// </summary>
        /// <returns> The <see cref="string"/>. String with counts. </returns>
        public string GetCountOfAdditionsAndDeletionsLine(int additionsOffset = 0)
        {
            int dCount = 0;
            int aCount = 0;

            if (this.Chunking.IsDisplayed())
            {
                int chunksCount = this.Chunking.GetChunksCount();
                for (int i = 1; i <= chunksCount; i++)
                {
                    this.Chunking.GoToChunk(i);
                    dCount = dCount + this.DocumentComparsion.GetCountOfDeletions();
                    aCount = aCount + this.DocumentComparsion.GetCountOfAdditions();
                }
            }
            else
            {
                dCount = this.DocumentComparsion.GetCountOfDeletions();
                aCount = this.DocumentComparsion.GetCountOfAdditions();
            }
            //needed if insert is split between 2 pages
            aCount = aCount + additionsOffset; 
            return this.FormatAdditionsAndDeletions(aCount,dCount);
        }

        /// <summary>
        /// Gets deletions/additions line.
        /// </summary>
        /// <returns> The <see cref="string"/>. The deletions/additions line. </returns>
        public string GetDeletionsAndAdditionsLineText() => DriverExtensions.GetText(DeletionsAndAdditionsClassLocator).Replace("\r\n", " ");

        /// <summary>
        /// Gets the key line text.
        /// </summary>
        /// <returns> The <see cref="string"/>. The key line text. </returns>
        public string GetDifferencesText() => DriverExtensions.GetText(TotalDifferenceLocator);

        /// <summary>
        /// Gets a chunk number.
        /// </summary>
        /// <returns> The <see cref="int"/>. Chunk Number. If the parse operation is failed, returns -1 </returns>
        public int GetFooterChunkNumber() => StringExtensions.ConvertCountToInt(DriverExtensions.GetAttribute("value", FooterChunkNumberLocator));
            
        /// <summary>
        /// Gets the info box icon text.
        /// </summary>
        /// <returns> The <see cref="string"/>. The info box icon text. </returns>
        public string GetInfoBoxIconText() => DriverExtensions.GetText(InfoBoxMessageLocator);

        /// <summary>
        /// Verifies if 'Highlights' toggle is checked
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if the the checkbox is checked, false otherwise. </returns>
        public bool IsHighlightsToggleChecked() => DriverExtensions.IsCheckboxSelected(TextViewCheckboxLocator);

        /// <summary>
        /// Verifies that the info box icon is displayed.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if the info box icon is displayed. </returns>
        public bool IsInfoBoxIconDisplayed() => DriverExtensions.IsDisplayed(InfoBoxIconLocator);

        /// <summary>
        /// Verifies that the key line is displayed.
        /// </summary>
        /// <returns> The <see cref="bool"/>.True if key line is displayed. </returns>
        public bool IsDifferencesLineDisplayed() => DriverExtensions.IsDisplayed(TotalDifferenceLocator);

        /// <summary>
        /// Verifies that the info box message is displayed.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if the info box message is displayed. </returns>
        public bool IsInfoBoxMessageDisplayed() => DriverExtensions.IsDisplayed(InfoBoxMessageLocator);

        /// <summary>
        /// Get Compare Versions Document Text.
        /// </summary>
        /// <returns> Compare Versions Document Text. </returns>
        public string GetCompareVersionsDocumentText() => DriverExtensions.GetText(CompareVersionsDocumentLocator);

        /// <summary>
        /// Verifies that all text are in view.
        /// </summary> 
        ///  <param name="symbol"> The symbol. </param>
        /// <returns> The <see cref="bool"/>. </returns>
        private bool AreAllTextView (string symbol)
        {
            switch (symbol)
            {
                case "+":
                    return this.VerifyMarksUp(this.CompareKeyLine.GetAddedKeyLineText(),this.DocumentComparsion.GetAllAdditions(), symbol);
                case "-":
                    return this.VerifyMarksUp(this.CompareKeyLine.GetDeletedKeyLineText(), this.DocumentComparsion.GetAllDeletions(), symbol);
                default:
                    return false;
            }
        }

        /// <summary>
        /// Verify markups on all chunks
        /// <param name="method"> The called method to verify mark up  </param> 
        /// </summary> 
        /// <returns> The <see cref="bool"/>. </returns>
        private bool VerifyMarkUpsForAllChunks(Func< bool> method)
        {
            this.Chunking.GoToChunk(1);
            bool pageMarkUp = method.Invoke();
            int chunksCount = this.Chunking.GetChunksCount(); 
            for (int i = 2; i <= chunksCount; i++)
            {
                this.Chunking.GoToChunk(i);
                pageMarkUp = method.Invoke();
            }
            return pageMarkUp;
        }

        /// <summary>
        /// <param name="keyText"></param>
        /// <param name="changes"></param>
        /// <param name="symbol"></param>
        /// Verify mark ups for text changes 
        /// </summary> 
        /// <returns> The <see cref="bool"/>. </returns>
        private bool VerifyMarksUp(string keyText, IEnumerable<IWebElement> changes, string symbol) => 
        changes.ToList().TrueForAll(add => add.GetAttribute("class").Contains("co_textOnlyView") &&
        add.Text.Contains("<<" + symbol) && add.Text.Contains(symbol + ">>"))
        && keyText.Contains("<<" + symbol) && keyText.Contains(symbol + ">>");

        /// <param name="aCount"></param>
        /// <param name="dCount"></param>
        /// Returns additions and deletions in format for compare 
        /// <returns> The <see cref="string"/>. </returns>
        private string FormatAdditionsAndDeletions(int aCount, int dCount)
        {
            string deletionText = dCount == 1 ? "deletion ·" : "deletions ·";
            string additionText = aCount == 1 ? "addition" : "additions";
            return $"{dCount:N0} {deletionText} {aCount:N0} {additionText}";
        }
    }
}