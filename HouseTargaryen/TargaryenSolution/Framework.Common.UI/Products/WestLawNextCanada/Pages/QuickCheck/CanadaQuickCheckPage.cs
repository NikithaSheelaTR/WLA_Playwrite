namespace Framework.Common.UI.Products.WestLawNextCanada.Pages.QuickCheck
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck;
    using Framework.Common.UI.Products.WestlawEdge.Pages.QuickCheck;
    using Framework.Common.UI.Products.WestLawNextCanada.Components.QuickCheck;
    using OpenQA.Selenium;

    /// <summary>
    /// Canada Quick Check page class.
    /// </summary>
    public class CanadaQuickCheckPage : QuickCheckUploadPage
    {
        private static readonly By ColumnHeaderLocator = By.ClassName("DA-ColumnLeftHeader");
        private static readonly By CheckWorkTileLocator = By.XPath("//div[contains(@class,'co_column check-work')]");
        private static readonly By AnalyzeDocumentButtonLocator = By.XPath("//div[@class='DA-UploadOption DA-UploadOption-button']/a");
        private static readonly By SetAsDefaultButtonLocator = By.XPath("//div[@class='DA-SetAsDefault']/button");
        private static readonly By BackToStartButtonLocator = By.XPath("//div[@class='DA-BackToStart']/a");
        private static readonly By UploadComponentLocator = By.XPath("//div[contains(@class,'DA-CardContainer') and contains(@class,'upload-work')]");
        private static readonly By SecurityInformationButtonLocator = By.XPath("//span[contains(@class,'icon_info-blueOutline')]");
        private static readonly By CitedAuthorityComponentLocator = By.XPath("//div[contains(@class,'DA-ColumnRight')]");
        
        /// <summary>
        /// Gets the check work tile.
        /// </summary>
        public QuickCheckLandingTileComponent CheckWorkTile { get; } = new QuickCheckLandingTileComponent(CheckWorkTileLocator);

        /// <summary>
        /// Gets the upload file component for Quick Check page.
        /// </summary>
        public UploadFileComponent UploadFileComponent { get; } = new UploadFileComponent(UploadComponentLocator);

        /// <summary>
        /// Gets the component responsible for managing cited authority data.
        /// </summary>
        public CitedAuthorityComponent CitedAuthorityComponent { get; } = new CitedAuthorityComponent(CitedAuthorityComponentLocator);

        /// <summary>
        /// Gets the column heading label.
        /// </summary>
        public ILabel ColumnHeadingLabel => new Label(ColumnHeaderLocator);

        /// <summary>
        /// Analyze Document button
        /// </summary>
        public IButton AnalyzeDocumentButton => new Button(AnalyzeDocumentButtonLocator);

        /// <summary>
        /// Sets the default button for Quick Check page.
        /// </summary>
        public IButton SetAsDefaultButton  => new Button(SetAsDefaultButtonLocator);

        /// <summary>
        /// Gets the back to start button for Quick Check page.
        /// </summary>
        public IButton BackToStartButton => new Button(BackToStartButtonLocator);

        /// <summary>
        /// Gets the security information button for Quick Check page.
        /// </summary>
        public IButton SecurityInformationButton => new Button(SecurityInformationButtonLocator);
    }
}