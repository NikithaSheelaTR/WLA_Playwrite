namespace Framework.Common.UI.Products.Shared.Dialogs.HomePage
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Dialog that appears when you hover over Community link
    /// </summary>
    public class CommunityDialog : BaseModuleRegressionDialog
    {
        private static readonly By CommunityDialogSignatureLocator = By.CssSelector("#co_community .co_globalNavDropdownBox_content div:last-child small i");
        private static readonly By CommunityDialogTextFirstPartLocator = By.CssSelector("#co_community .co_globalNavDropdownBox_content div:first-child");
        private static readonly By CommunityDialogTextSecondPartLocator = By.CssSelector("#co_community .co_globalNavDropdownBox_content div:last-child");
        private static readonly By CommunityDialogTitleLocator = By.CssSelector("#co_community .co_globalNavDropdownBox_header");
        private static readonly By CloseCommunityDialogButtonLocator = By.XPath("//button[text()='Close Community Information window']");
        private static readonly By ViewCommunityLinkLocator = By.CssSelector(".co_communityLink");

        /// <summary>
        /// Click view community link
        /// </summary>
        /// <typeparam name="T">An instance that implements ICommunityPage</typeparam>
        /// <returns>New instance of T</returns>
        public T ClickViewCommunityLink<T>() where T : ICommunityPage
        {
            this.ClickElement<T>(ViewCommunityLinkLocator);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Is text equal View Community Link text.
        /// </summary>
        /// <returns> View Community Link text </returns>
        public string GetViewCommunityLinkText()
            => DriverExtensions.GetText(ViewCommunityLinkLocator);

        /// <summary>
        /// Is text present in Community PopUp Signature.
        /// </summary>
        /// <param name="text"> Text </param>
        /// <returns> True if Community PopUp contains text </returns>
        public bool IsTextPresentInCommunityDialogSignature(string text)
            => DriverExtensions.IsTextInElement(CommunityDialogSignatureLocator, text);

        /// <summary>
        /// Is text present in Community PopUp Text First Part.
        /// </summary>
        /// <param name="text"> Text </param>
        /// <returns> True if Community PopUp contains text </returns>
        public bool IsTextPresentInCommunityDialogFirstPart(string text)
            => DriverExtensions.IsTextInElement(CommunityDialogTextFirstPartLocator, text);

        /// <summary>
        /// Is text present in Community PopUp Text Second Part.
        /// </summary>
        /// <param name="text"> Text </param>
        /// <returns> True if Community PopUp contains text </returns>
        public bool IsTextPresentInCommunityDialogSecondPart(string text)
            => DriverExtensions.IsTextInElement(CommunityDialogTextSecondPartLocator, text);

        /// <summary>
        /// Is text present in Community PopUp Title.
        /// </summary>
        /// <param name="text"> Text </param>
        /// <returns> True if Community PopUp contains text </returns>
        public bool IsTextPresentInCommunityDialogTitle(string text)
            => DriverExtensions.IsTextInElement(CommunityDialogTitleLocator, text);

        /// <summary>
        /// Close community dialog
        /// </summary>
        /// <typeparam name="T"> page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T CloseCommunityDialog<T>() where T : BaseModuleRegressionPage
            => this.ClickElement<T>(CloseCommunityDialogButtonLocator);
    }
}