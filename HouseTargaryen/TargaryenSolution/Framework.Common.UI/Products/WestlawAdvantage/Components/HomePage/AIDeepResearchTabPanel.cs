using Framework.Common.UI.Interfaces.Elements;
using Framework.Common.UI.Products.Shared.Elements.Buttons;
using Framework.Common.UI.Products.Shared.Elements.Textboxes;
using Framework.Common.UI.Products.WestLawNext.Components;
using OpenQA.Selenium;

namespace Framework.Common.UI.Products.WestlawAdvantage.Components.HomePage
{
    /// <summary>
    /// AI Deep Research TabPanel on New Home Page
    /// </summary>
    public class AIDeepResearchTabPanel : BaseTabComponent
    {
        private static readonly By QuestionTextAreaLocator = By.XPath(".//saf-text-area-v3[@id='deep-research-input']");
        private static readonly By SendButtonLocator = By.XPath(".//saf-button-v3[@id ='input-button']");
        private static readonly By AIDeepResearchHeaderLocator = By.XPath(".//h2[contains(@class,'ApplicationHeader-module__heading')]");
        private static readonly By AIDeepResearchMessageBoxLocator = By.XPath("//div[contains(@class,'RightColumnHeader-module__rightColumnHeaderWrapper')]");
        private static readonly By AIDeepResearchSaveToFolderButtonLocator = By.XPath("//saf-button-v3[contains(@class,'FolderingButton-module__folderingButton')]");
        private static readonly By AIDeepResearchSaveButtonPopupLocator = By.XPath("//input[@class='co_primaryBtn co_dropdownBox_ok co_saveToDoSave']");


        /// <summary>
        /// Tab Name
        /// </summary>
        protected override string TabName => "AI Deep Research";

        /// <summary>
        /// ComponentLocator
        /// </summary>
        protected override By ComponentLocator => By.XPath("//*[@id = 'panel-1']");

        /// <summary>
        /// Question Tex tArea
        /// </summary>
        public ITextbox QuestionTextArea => new Textbox(ComponentLocator, QuestionTextAreaLocator);

        /// <summary>
        /// Send buttin
        /// </summary>
        public IButton SendButton => new Button(ComponentLocator, SendButtonLocator);

        /// <summary>
        /// AI Deep Research Header 
        /// </summary>
        public IButton AIDeepResearchHeader => new Button(ComponentLocator, AIDeepResearchHeaderLocator);

        /// <summary>
        /// Answer Text Area
        /// </summary>
        public ITextbox AIDeepResearchMessageBox => new Textbox(AIDeepResearchMessageBoxLocator);

        /// <summary>
        /// Save to Folder Button
        /// </summary>
        public IButton AIDeepResearchSaveToFolderButton => new Button(AIDeepResearchSaveToFolderButtonLocator);

        /// <summary>
        /// Save Button
        /// </summary>
        public IButton AIDeepResearchSaveButtonPopup => new Button(AIDeepResearchSaveButtonPopupLocator);
    }
}
