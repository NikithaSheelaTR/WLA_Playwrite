namespace Framework.Common.UI.Products.WestlawAdvantage.Dialogs.DeepResearch
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Recent Questions Dialog upon clicking the question text area
    /// </summary>
    public class RecentQuestionsDialog : BaseModuleRegressionDialog
    {
        private static readonly By ContainerLocator = By.XPath("//*[contains(@class,'RecentQuestions-module__dialog')]");
        private static readonly By RecentQuestionsTabLocator = By.XPath(".//saf-tab-v3[contains(text(),'Recent questions')]");
        private static readonly By RecentQuestionButtonsLocator = By.XPath(".//li/button[contains(@class, 'Question-module__searchButton')]");
        private static readonly By ProgressBarLabelLocator = By.XPath("//saf-progress-ring-v3[@role='progressbar']");
        private static readonly By SavedQuestionsTabLocator = By.XPath(".//saf-tab-v3[contains(text(),'Saved questions')]");
        private const string RecentQuestionLctMask = ".//li/button[contains(@class, 'Question-module__searchButton') and contains(text(),'{0}')]";
        private const string SaveQuestionLctMask = ".//li/button[contains(@class, 'Question-module__favoriteButton') and starts-with(@aria-label,'Save') and contains(@aria-label,'{0}')]";
        private const string RemoveQuestionLctMask = ".//li/button[contains(@class, 'Question-module__favoriteButton') and starts-with(@aria-label,'Remove') and contains(@aria-label,'{0}')]";

        /// <summary>
        /// Recent Questions Tab
        /// </summary>
        public IButton RecentQuestionsTab => new Button(ContainerLocator, RecentQuestionsTabLocator);

        /// <summary>
        ///  All Recent Question Buttons/links
        /// </summary>
        public IReadOnlyCollection<IButton> RecentQuestionButtons => new ElementsCollection<Button>(ContainerLocator, RecentQuestionButtonsLocator);

        /// <summary>
        /// Saved Questions Tab
        /// </summary>
        /// /// <param name="question">Question to save</param>
        public IButton RecentQuestionLinkWithText(string question) => new Button(ContainerLocator, By.XPath(string.Format(RecentQuestionLctMask, question)));

        /// <summary>
        /// Progress bar label
        /// </summary>
        public ILabel ProgressBarLabel => new Label(ProgressBarLabelLocator);

        /// <summary>
        /// Saved Questions Tab
        /// </summary>
        public IButton SavedQuestionsTab => new Button(ContainerLocator, SavedQuestionsTabLocator);

        /// <summary>
        /// Save question
        /// </summary>
        /// <param name="question">Question to save</param>
        public void SaveQuestion(string question)
        {
            IButton saveButton = new Button(ContainerLocator, By.XPath(string.Format(SaveQuestionLctMask, question)));
            if (saveButton.Displayed)
            {
                saveButton.Click();
            }
        }

        /// <summary>
        /// Remove saved question
        /// </summary>
        /// <param name="question">Question to remove</param>
        public void RemoveSavedQuestion(string question)
        {
            IReadOnlyCollection<IButton> removeButtons = new ElementsCollection<Button>(ContainerLocator, By.XPath(string.Format(RemoveQuestionLctMask, question)));
            if (removeButtons.Count > 0)
            {
                removeButtons.First().Click();
            }
        }

        /// <summary>
        /// Is question saved
        /// </summary>
        /// <param name="question">question</param>
        /// <returns>true if question is saved</returns>
        public bool IsQuestionSaved(string question)
        {
            IReadOnlyCollection<IButton> removeButtons = new ElementsCollection<Button>(ContainerLocator, By.XPath(string.Format(RemoveQuestionLctMask, question)));
            return removeButtons.Count > 0;
        }
    }
}




