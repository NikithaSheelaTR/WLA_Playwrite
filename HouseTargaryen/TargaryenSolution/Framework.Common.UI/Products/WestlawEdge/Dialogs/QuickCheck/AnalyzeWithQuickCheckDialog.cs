namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.QuickCheck
{
    using System;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck;
    using Framework.Common.UI.Products.WestlawEdge.Enums.QuickCheck;
    using Framework.Common.UI.Products.WestlawEdge.Pages.QuickCheck;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Analyze with quick check dialog for submit to quick check feature
    /// </summary>
    public class AnalyzeWithQuickCheckDialog : BaseModuleRegressionDialog
    {
        private static readonly By CheckYourWorkLocator = By.XPath("//div[contains(@class,'CheckDocWork')]");

        private static readonly By OpponentWorkLocator = By.XPath("//div[contains(@class,'ExamineOpponentWork')]");

        private static readonly By AnalyzeButtonLocator = By.XPath("//button[@class='co_primaryBtn' and text()='Analyze']");

        private static readonly By CancelButtonLocator = By.XPath("//button[@class='co_overlayBox_buttonCancel' and text()='Cancel']");

        private static readonly By CloseButtonLocator = By.XPath("//button[@class='co_overlayBox_closeButton co_iconBtn']");

        /// <summary>
        /// CheckYourWorkTile
        /// </summary>
        public AnalyzeWithQuickCheckTile CheckYourWorkTile => new AnalyzeWithQuickCheckTile(CheckYourWorkLocator);

        /// <summary>
        /// CheckYourWorkTile
        /// </summary>
        public AnalyzeWithQuickCheckTile CheckOpponentWorkTile => new AnalyzeWithQuickCheckTile(OpponentWorkLocator);

        /// <summary>
        /// start analyze button
        /// </summary>
        public IButton AnalyzeButton => new Button(AnalyzeButtonLocator);

        /// <summary>
        /// cancel button
        /// </summary>
        public IButton CancelButton => new Button(CancelButtonLocator);

        /// <summary>
        /// close button
        /// </summary>
        public IButton CloseButton => new Button(CloseButtonLocator);

        /// <summary>
        /// Select option to analyze and click analyze button
        /// </summary>
        /// <param name="uploadOption">Upload option</param>
        /// <returns>Quick Check Recommendation page</returns>
        public QuickCheckRecommendationsPage UploadToQuickCheck(WhatYouWouldLikeToDoOptions uploadOption = WhatYouWouldLikeToDoOptions.CheckYourWork)
        {
            switch (uploadOption)
            {
                case WhatYouWouldLikeToDoOptions.CheckYourWork:
                    this.CheckYourWorkTile.CheckBox.Set(true);
                    break;
                case WhatYouWouldLikeToDoOptions.AnalyzeOpponents:
                   this.CheckOpponentWorkTile.CheckBox.Set(true);
                    break;
                default:
                    throw new NotSupportedException($"upload option {uploadOption} not supported");
            }

            var uploadDialog = this.AnalyzeButton.Click<QuickCheckFileUploadDialog>();
            uploadDialog.WaitUntilFileUpload();

            return DriverExtensions.CreatePageInstance<QuickCheckRecommendationsPage>();
        }
    }
}
