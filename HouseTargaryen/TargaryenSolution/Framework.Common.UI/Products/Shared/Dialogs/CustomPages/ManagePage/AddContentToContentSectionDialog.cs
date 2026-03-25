namespace Framework.Common.UI.Products.Shared.Dialogs.CustomPages.ManagePage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    using Framework.Common.UI.Products.Shared.Enums;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Add Content to Content Section Dialog
    /// </summary>
    public class AddContentToContentSectionDialog : BaseManagePageDialog
    {
        private const string CategoryPageCheckboxByNumberLctMask =
            "(//div[@id='cp_selectContent_browse']//input[@type='checkbox' and not(@checked)])[{0}]";

        private const string CategoryPageCheckboxLctMask =
            "//div[@class='co_formInline' and .//*[contains(text(),\"{0}\")]]//input";

        private const string CategoryPageLinkLctMask =
            "//div[@class='co_formInline']//*[contains(text(), '{0}') and contains(@class,'cp_selectContent_item')]";
     
        private const string LinkLctMask = "id('cp_selectContent_innerTab')/descendant::a[text()='{0}']";

        private const string TabItemLctMask =
            "//div[@id='cp_selectContent_tabs']//*[contains(@class, 'co_tabLink') and contains(text(), '{0}')]";

        private static readonly By ContentToRemoveLocator =
            By.XPath("//div[@id='cp_selectContent_selected']/ul/li/a[@class='cp_selectedContent_removeLink']");

        /// <summary>
        /// Add Category Page List
        /// </summary>
        /// <param name="categoryPageList">Category Page Name</param>
        public void AddCategoryPageList(IList<string> categoryPageList)
            => categoryPageList.ToList().ForEach(this.AddCategoryPage);

        /// <summary>
        /// Add Category Page List. 
        /// The key in <see cref="KeyValuePair{TKey,TValue}"/> specifies whether current category should be checked or should be clicked as link.
        /// </summary>
        /// <param name="contentList"> Content List </param>
        public void AddCategoryPageList(IList<KeyValuePair<WebElementType, string>> contentList)
        {
            foreach (KeyValuePair<WebElementType, string> content in contentList)
            {
                DriverExtensions.WaitForJavaScript();
                switch (content.Key)
                {
                    case WebElementType.Checkbox:
                        this.AddCategoryPage(content.Value);
                        break;

                    case WebElementType.Link:
                        DriverExtensions.WaitForElement(By.XPath(string.Format(LinkLctMask, content.Value))).Click();
                        break;

                    default:
                        throw new ArgumentException($"The is no proper behavior for '{content.Key}' key.");
                }
            }
        }

        /// <summary>
        /// Add specified count of Category Pages
        /// </summary>
        /// <param name="categoryPagesCountToAdd">Category Page Name</param>
        public void AddCategoryPages(int categoryPagesCountToAdd)
        {
            for (int i = 1; i <= categoryPagesCountToAdd; i++)
            {
                DriverExtensions.Click(By.XPath(string.Format(CategoryPageCheckboxByNumberLctMask, i)));
            }
        }

        /// <summary>
        /// Goes to defined path. The path should be started with proper Tab Name.
        /// The path example: {tab_name}/{link_name_1}/{link_name_2}/.../{link_name_n}  
        /// </summary>
        /// <param name="path"> Path to add </param>
        /// <returns> The <see cref="AddContentToContentSectionDialog"/>. </returns>
        public AddContentToContentSectionDialog GoToPath(string path = "All Content")
        {
            string[] linkNames = path.Split('/', '\\').Select(str => str.Trim()).ToArray();

            // click root tab section as root element
            DriverExtensions.WaitForElement(By.XPath(string.Format(TabItemLctMask, linkNames[0]))).Click();

            // go by path
            linkNames.Skip(1).ToList().ForEach(
                link =>
                    {
                        DriverExtensions.WaitForElement(By.XPath(string.Format(CategoryPageLinkLctMask, link))).Click();
                        DriverExtensions.WaitForJavaScript();
                    });
            DriverExtensions.WaitForJavaScript();
            return this;
        }

        /// <summary>
        /// Removes All Selected categories
        /// </summary>
        public void RemoveAllSelectedCategories()
        {
            int count = DriverExtensions.GetElements(ContentToRemoveLocator).Count;
            for (int i = 0; i < count; i++)
            {
                DriverExtensions.WaitForElement(ContentToRemoveLocator).Click();
            }
        }

        /// <summary>
        /// Remove Category Page
        /// </summary>
        /// <param name="categoryPagesToRemove">Category Pages Names to remove</param>
        public void RemoveCategoryPages(params string[] categoryPagesToRemove)
        {
            foreach (string categoryPage in categoryPagesToRemove)
            {
                DriverExtensions.GetElements(ContentToRemoveLocator)
                                .FirstOrDefault(it => it.Text.Equals(categoryPage))?.Click();
            }
        }

        private void AddCategoryPage(string categoryPageName)
        {
            By checkBoxLocator = By.XPath(string.Format(CategoryPageCheckboxLctMask, categoryPageName));
            DriverExtensions.WaitForElement(checkBoxLocator);
            DriverExtensions.SetCheckbox(checkBoxLocator, true);
        }
    }
}