namespace Framework.Common.UI.Products.Shared.Dialogs.Foldering
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;

    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using OpenQA.Selenium;

    /// <summary>
    /// After clicking Continue when selecting people/groups to add this is the window that shows them and their roles
    /// </summary>
    public class CollaboratorRolesDialog : BaseModuleRegressionDialog
    {
        private const string PendingTagLctMask = "//td[contains(text(), {0})]//i[@class='pending']";

        private const string RoleLctMask = "//td[text()={0}]//following-sibling::td[1]//select | //div[text()={0}]/following-sibling::div/select";

        private static readonly By BackButtonLocator = By.Id("co_folderingShareFolderGoBack");

        private static readonly By CollaboratorsLocator =
            By.XPath("//*[@class='co_lightboxOverlay']//td[@class='co_detailsTable_content'] | //div[@class='SharedWithTable']//div[@role='row']//div[@role='cell'][1]");

        private static readonly By ShareButtonLocator = By.Id("co_folderingShareFolderCommit");
        
        /// <summary>
        /// Initializes a new instance of the <see cref="CollaboratorRolesDialog"/> class.
        /// </summary>
        public CollaboratorRolesDialog()
        {
            DriverExtensions.WaitForElement(CollaboratorsLocator);
        }

        /// <summary>
        /// Clicks the Back button
        /// </summary>
        /// <returns> New instance of the T page </returns>
        public T ClickBackButton<T>() where T : ICreatablePageObject
            => this.ClickElement<T>(BackButtonLocator);

        /// <summary>
        /// A list of users to share with
        /// </summary>
        /// <returns> List of strings </returns>
        public List<string> GetCollaborators()
            => DriverExtensions.GetElements(CollaboratorsLocator).Select(e => this.StripPendingText(e.Text)).ToList();

        /// <summary>
        /// Gets the currently selected role for a given person
        /// </summary>
        /// <param name="person"> Person to find the role for </param>
        /// <returns> The role for the person parameter </returns>
        public string GetRole(string person)
            => DriverExtensions.GetSelectElementSelectedText(SafeXpath.BySafeXpath(RoleLctMask, person));

        /// <summary>
        /// Determines if a person or email has a "Pending" tag next to them
        /// </summary>
        /// <param name="text"> Person or email to look next to </param>
        /// <returns> True if there is a pending tag present, false otherwise </returns>
        public bool HasPendingTag(string text)
            => DriverExtensions.IsDisplayed(SafeXpath.BySafeXpath(PendingTagLctMask, text), 5);

        /// <summary>
        /// Share button
        /// </summary>
        public IButton ShareButton => new Button(ShareButtonLocator);

        /// <summary>
        /// Replaces current text to new value 
        /// </summary>
        /// <param name="text"> Text for replacing </param>
        /// <returns> Replaced text </returns>
        private string StripPendingText(string text)
        {
            if (text.Contains("(Pending)"))
            {
                text = text.Replace("(Pending)", string.Empty);
            }

            return text.Trim();
        }
    }
}