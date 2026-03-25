namespace Framework.Common.UI.Products.Shared.Dialogs.CCPA
{
    /// <summary>
    /// In progress requet Dialog
    /// </summary>
    public class InProgressRequestDialog : ProcessBaseDialog
    {
        private const string Title = "Your submission is currently in progress";

        /// <summary>
        /// Initializes a new instance of the <see cref="InProgressRequestDialog"/> class. 
        /// </summary>
        public InProgressRequestDialog()
            : base(Title)
        {
        }
    }
}
