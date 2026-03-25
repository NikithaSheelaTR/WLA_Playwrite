namespace Framework.Common.UI.Raw.WestlawEdge.Models.QuestionAnswer
{
    /// <summary>
    /// Describe question-answer on the 'Indigo Question Answer teaser/page'
    /// </summary>
    public class EdgeQuestionAnswerResultListItemModel
    {
        /// <summary>
        /// Is the Associated content displayed.
        /// </summary>
        public bool IsAssociatedContentDisplayed { get; set; }

        /// <summary>
        /// Is "Yes" feedback button displayed
        /// </summary>
        public bool IsFeedbackYesButtonDisplayed { get; set; }

        /// <summary>
        /// Is "No" feedback button displayed
        /// </summary>
        public bool IsFeedbackNoButtonDisplayed { get; set; }

        /// <summary>
        /// Is "Helpful?" feedback question displayed
        /// </summary>
        public bool IsFeedbackQuestionDisplayed { get; set; }

        /// <summary>
        /// Is "Thank you" feedback text displayed
        /// </summary>
        public bool IsFeedbackThankYouDisplayed { get; set; }
    }
}
