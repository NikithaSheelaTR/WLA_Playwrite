namespace Framework.Common.UI.Products.Shared.Components.CustomPage
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs.CustomPages.ManagePage;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Enums;
    using Framework.Common.UI.Products.Shared.Enums.CustomPage;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Content Section Widget
    /// </summary>
    public class ContentSectionComponent : BaseModuleRegressionComponent
    {
        private static readonly By AddContentButtonLocator =
            By.CssSelector(".cp_emptySection_container>input[value='Add Content']");

        private static readonly By AddCuiContentButtonLocator =
            By.CssSelector("li.add_Cui_content_container>input[value='Add Cui Content']");

        private static readonly By ContentTypeSectionItemLocator = By.CssSelector("li.CP-card-item,li.cp_categoryPageList_item");

        private static readonly By TitleLocator = By.CssSelector("div.CP-card-header .cp_sectionLabel");

        private By locator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentSectionComponent"/> class. 
        /// </summary>
        /// <param name="element"> Content Section component container </param>
        public ContentSectionComponent(By element)
        {
            this.locator = element;
        }

        /// <summary>
        /// Gets Manage Page Drop Down
        /// </summary>
        public ContentSectionMenuDropdown ContentSectionMenuDropdown => new ContentSectionMenuDropdown();

        /// <summary>
        /// Add Content button
        /// </summary>
        public IButton AddContentButton => new Button(this.ComponentLocator, AddContentButtonLocator);

        /// <summary>
        /// Get Content Section items list
        /// </summary>
        public IList<ContentSectionItem> ContentSectionItemsList
            =>
                DriverExtensions.GetElements(this.ComponentLocator, ContentTypeSectionItemLocator)
                                .Select(elem => new ContentSectionItem(elem))
                                .ToList();

        /// <summary>
        /// Get Title of the Content Section component
        /// </summary>
        public string Title => DriverExtensions.GetElement(this.ComponentLocator, TitleLocator).Text;

        /// <summary>
        /// ComponentLocator
        /// </summary>
        protected override By ComponentLocator => this.locator;

        /// <summary>
        /// Add Category Page List. 
        /// The key in <see cref="KeyValuePair{TKey,TValue}"/> specifies whether current category should be checked or should be clicked as link.
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="path"> The path to go firstly </param>
        /// <param name="contentList"> Content List </param>
        /// <returns> New instance of the page </returns>
        public T AddContent<T>(string path, IList<KeyValuePair<WebElementType, string>> contentList)
            where T : ICreatablePageObject
        {
            AddContentToContentSectionDialog addContentDialog = this.GoToPath(path);
            addContentDialog.AddCategoryPageList(contentList);
            return addContentDialog.ClickSaveButton<T>();
        }

        /// <summary>
        /// Add Content to the Content Section
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="categoryPagesList"> List of pages  </param>
        /// <returns> New instance of the page </returns>
        public T AddContent<T>(IList<string> categoryPagesList) where T : ICreatablePageObject
        {
            AddContentToContentSectionDialog addContentDialog = this.ClickAddContent();
            addContentDialog.AddCategoryPageList(categoryPagesList);
            return addContentDialog.ClickSaveButton<T>();
        }

        /// <summary>
        /// Add content from specified path to the Content Section 
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="pathToSelectCategories">The path to select categories from.
        /// The path should be started with proper Tab Name.
        /// The path example: {tab_name}/{link_name_1}/{link_name_2}/.../{link_name_n}
        /// </param>
        /// <param name="countToAdd"> The count of items to select </param>
        /// <returns> New instance of the page </returns>
        public T AddContentByPath<T>(string pathToSelectCategories, int countToAdd) where T : ICreatablePageObject
        {
            AddContentToContentSectionDialog addContentDialog = this.GoToPath(pathToSelectCategories);
            addContentDialog.AddCategoryPages(countToAdd);
            return addContentDialog.ClickSaveButton<T>();
        }

        /// <summary>
        /// Add content from specified path to the Content Section
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="pathToSelectCategories">
        /// The path to select categories from.
        /// The path should start with proper Tab Name.
        /// The path example: {tab_name}/{link_name_1}/{link_name_2}/.../{link_name_n}
        /// </param>
        /// <param name="categoryPagesList"> Categories to add </param>
        /// <returns> New instance of the page </returns>
        public T AddContentByPath<T>(string pathToSelectCategories, IList<string> categoryPagesList)
            where T : ICreatablePageObject
        {
            AddContentToContentSectionDialog addContentDialog = this.GoToPath(pathToSelectCategories);
            addContentDialog.AddCategoryPageList(categoryPagesList);
            return addContentDialog.ClickSaveButton<T>();
        }

        /// <summary>
        /// Add Content to the Content Section
        /// </summary>
        /// <param name="url"> URL with Category Pages </param>
        public void AddCuiContent(string url)
        {
            ConvertCuiDialog convertCuiDialog = this.ClickAddCuiContent();
            convertCuiDialog.AddCuiContent(url);
        }

        /// <summary>
        /// Add content from specified path to the Content Section with existing content
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="pathToSelectCategories">
        /// The path to select categories from.
        /// The path should be started with proper Tab Name.
        /// The path example: {tab_name}/{link_name_1}/{link_name_2}/.../{link_name_n}
        /// </param>
        /// <param name="categoryPagesToAdd"> Categories to add </param>
        /// <returns> New instance of the page </returns>
        public T EditContentByPath<T>(string pathToSelectCategories, IList<string> categoryPagesToAdd)
            where T : ICreatablePageObject
        {
            AddContentToContentSectionDialog addContentDialog = this.ClickEdit();
            if (!string.IsNullOrWhiteSpace(pathToSelectCategories))
            {
                addContentDialog.GoToPath(pathToSelectCategories);
            }

            addContentDialog.AddCategoryPageList(categoryPagesToAdd);
            return addContentDialog.ClickSaveButton<T>();
        }

        /// <summary>
        /// Add content from specified path to the Content Section with existing content
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="pathToSelectCategories">
        /// The path to select categories from.
        /// The path should be started with proper Tab Name.
        /// The path example: {tab_name}/{link_name_1}/{link_name_2}/.../{link_name_n}
        /// </param>
        /// <param name="countToAdd"> The count of items to select
        /// </param>
        /// <param name="uncheckPreviouslySelected"> Specify if previously selected items should be removed firstly
        /// </param>
        /// <returns> New instance of the page </returns>
        public T EditContentByPath<T>(
            string pathToSelectCategories,
            int countToAdd,
            bool uncheckPreviouslySelected = false) where T : ICreatablePageObject
        {
            AddContentToContentSectionDialog addContentDialog = this.ClickEdit();
            if (!string.IsNullOrWhiteSpace(pathToSelectCategories))
            {
                addContentDialog.GoToPath(pathToSelectCategories);
            }

            addContentDialog.AddCategoryPages(countToAdd);
            return addContentDialog.ClickSaveButton<T>();
        }

        /// <summary>
        /// Remove all selected content from Content Section
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T RemoveAllContent<T>() where T : ICreatablePageObject
        {
            AddContentToContentSectionDialog addContentDialog = this.ClickEdit();
            addContentDialog.RemoveAllSelectedCategories();
            return addContentDialog.ClickSaveButton<T>();
        }

        /// <summary>
        /// Remove all selected content from Content Section
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="categoriesToRemove"> The categories To Remove. </param>
        /// <returns> New instance of the page </returns>
        public T RemoveContent<T>(params string[] categoriesToRemove) where T : ICreatablePageObject
        {
            AddContentToContentSectionDialog addContentDialog = this.ClickEdit();
            addContentDialog.RemoveCategoryPages(categoriesToRemove);
            return addContentDialog.ClickSaveButton<T>();
        }

        private AddContentToContentSectionDialog ClickAddContent()
        {
            DriverExtensions.GetElement(this.ComponentLocator, AddContentButtonLocator).Click();
            return new AddContentToContentSectionDialog();
        }

        private ConvertCuiDialog ClickAddCuiContent()
        {
            DriverExtensions.GetElement(this.ComponentLocator, AddCuiContentButtonLocator).Click();
            return new ConvertCuiDialog();
        }

        private AddContentToContentSectionDialog ClickEdit()
        {
            this.ContentSectionMenuDropdown.SelectOption(ContentSectionMenuDropdownOptions.Edit);
            return new AddContentToContentSectionDialog();
        }

        private AddContentToContentSectionDialog GoToPath(string pathToSelectCategories)
        {
            AddContentToContentSectionDialog addContentDislog = this.ClickAddContent();
            return string.IsNullOrWhiteSpace(pathToSelectCategories)
                       ? addContentDislog.GoToPath()
                       : addContentDislog.GoToPath(pathToSelectCategories);
        }
    }
}