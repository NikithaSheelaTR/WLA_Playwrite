namespace Framework.Common.UI.Products.Shared.Components.Folder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;
    using Framework.Core.Utils.Execution;

    using OpenQA.Selenium;

    /// <summary>
    /// The Folder Tree Component is used across many components/pages
    /// </summary>
    public class FolderTreeComponent : BaseModuleRegressionComponent
    {
        private const string ExpandFolderLinkDescriptorLctMask =
            @"//div[contains(@class,'co_tree_element')]//*[text()={0}]/ancestor::li[contains(@class,'co_tree_depth_')]/div/a[contains(@class,'co_tree_expand') and not(contains(@class,'co_hideState'))]";

        private const string FolderItemDescriptorLctMask = @".//div[contains(@class,'co_tree_element') and contains(@class,'co_tree_position')]//*[text()={0}]";

        private const string SharedFolderLctMask = "//a[@title={0} and @class='co_shared']";

        private const string SharedWithMeFolderLctMask = "//div[@id ='cobalt_ro_sharedFolders_folderTree']//a/ancestor::div[contains(@class,'co_shared')]";

        private const string CheckboxLctMask = "./ancestor::div[contains(@class, 'co_tree_element')]/input[@type='checkbox'] | //ul[@class='TreeView'] //input[@type='checkbox' and contains(@aria-label,'{0}')] ";

        private static readonly By CollapseButtonLocator = By.XPath(".//a[contains(@class,'co_tree_collapse') and not(contains(@class,'co_hideState'))]");

        private static readonly By ExpandButtonLocator = By.XPath("//li[@role='treeitem' and @aria-expanded='false']//*[contains(@class,'co_tree_expand')]//span[contains(@class,'TreeViewItemToggle')] | //*[contains(@class,'co_tree_expand')and @aria-expanded='false']");

        private static readonly By ExpandedFolderLocator = By.XPath(".//div[contains(@class,'co_tree_expand')]/a");

        private static readonly By FolgeringTreeToggleLocator = By.XPath("//div[contains(@class,'co_foldering_tree_toggle')]/a");

        private static readonly By FolderItemLinkLocator = By.XPath(".//a/span[@class='co_tree_name_span'] | .//span[@class='TreeViewItemName']");

        private static readonly By RootFolderLocator = By.XPath(".//*[contains(@class,'co_tree_name')]/span[@class='co_tree_name_span'] | .//li[@role='treeitem' and not(ancestor::li)]/div//span[@class='TreeViewItemName']");

        private static readonly By SelectedFolderLocator = By.XPath(".//*[contains(@class,'co_treeFocus') or contains(@class,'TreeButton--isSelected')]//*[contains(@class, 'co_tree_selectable') or @class='TreeViewItemName']");

        private static readonly By FolderTreeRootLocator = By.Id("cobalt_ro_myFolders_folderTree_root");

        private static readonly By TrashFolderLinkLocator = By.Id("cobalt_ro_trash");

        private readonly By componentLocator;

        /// <summary>
        /// Initializes a new instance of the <see cref="FolderTreeComponent"/> class. 
        /// </summary>
        /// <param name="rootElement"> Root element for the tree </param>
        public FolderTreeComponent(By rootElement)
        {
            this.componentLocator = rootElement;
        }

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => this.componentLocator;

        private IWebElement RootFolder =>
            DriverExtensions.WaitForElement(DriverExtensions.WaitForElement(this.ComponentLocator), RootFolderLocator);

        /// <summary>
        /// Select All RequireTerm Checkbox
        /// </summary>
      public ICheckBox FolderCheckbox(string folderName) => new CheckBox(By.XPath(string.Format(CheckboxLctMask, folderName)));

        /// <summary>
        /// Click on the root folder
        /// </summary>
        /// <typeparam name="T">
        /// Page Object
        /// </typeparam>
        /// <returns>
        /// Page Instance
        /// </returns>
        public T ClickRootFolder<T>() where T : ICreatablePageObject
        {
            this.ClickRootFolder();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Select the root folder
        /// </summary>
        public virtual void ClickRootFolder()
        {
            DriverExtensions.WaitForElement(RootFolderLocator);
            this.RootFolder.Click();
        }

        /// <summary>
        /// The expand folder tree till find first folder with required name
        /// </summary>
        /// <param name="folderName"> The folder name. </param>
        public virtual void ExpandFolderTree(string folderName) => this.TryFindFolderInFolderTree(folderName);

        /// <summary>
        /// Find a folder in the folder tree indicated by the folder name
        /// </summary>
        /// <param name="folderPath"> Path to folder </param>
        /// <returns> The name of target folder: last in the path of folder </returns>
        public string ExpandFolderTreeByPath(string folderPath)
        {
            IWebElement root = DriverExtensions.WaitForElementDisplayed(this.ComponentLocator, 10000);

            // Click all Ancestor Expand toggles 
            string[] folders = folderPath.Split('/');
            for (int i = 0; i < folders.Length - 1; i++)
            {
                By parentFolder = SafeXpath.BySafeXpath(ExpandFolderLinkDescriptorLctMask, folders[i]);
                IEnumerable<IWebElement> expandfolders = DriverExtensions.GetElements(root, parentFolder);

                foreach (IWebElement expandFolder in expandfolders)
                {
                    SafeMethodExecutor.Execute(expandFolder.Click).LogDetails();
                }
            }

            return folders.Last();
        }

        /// <summary>
        /// Get the root folder name for the user
        /// </summary>
        /// <returns>Root folder name</returns>
        public virtual string GetRootFolderName() => this.RootFolder.Text;

        /// <summary>
        /// Get all the headers in the tree for comparison to our folder name
        /// </summary>
        /// <returns>listOfHeaders</returns>
        public IList<string> GetTreeHeaders()
        {
            DriverExtensions.WaitForElementDisplayed(FolderTreeRootLocator);
            return DriverExtensions.GetElements(FolgeringTreeToggleLocator).Select(e => e.Text).ToList();
        }

        /// <summary>
        /// Determine if the folder exists (shouldn't matter if it is visible or not)
        /// </summary>
        /// <param name="folderName"> Name of the folder to search for </param>
        /// <returns> True if the folder exists, false otherwise </returns>
        public virtual bool IsFolderExist(string folderName) => this.TryFindFolderInFolderTree(folderName) != null;

        /// <summary>
        /// Returns true if the indicated folder is selected
        /// </summary>
        /// <param name="folderName"> Folder to check </param>
        /// <returns> True if folder selected, false otherwise </returns>
        public virtual bool IsFolderSelected(string folderName) =>
            DriverExtensions.GetElements(this.ComponentLocator, SelectedFolderLocator).Select(e => e.Text).Contains(folderName);

        /// <summary>
        /// Checks the icon of the indicated folder and returns true if the folder is blue
        /// </summary>
        /// <param name="folderName"> Folder to check </param>
        /// <returns> True if folder is shared, false otherwise </returns>
        public virtual bool IsFolderShared(string folderName)
        {
            this.ExpandFolderTree(folderName);
            DriverExtensions.WaitForJavaScript(); // Additional wait allows tree to load
            return DriverExtensions.GetElements(DriverExtensions.WaitForElement(this.ComponentLocator), SafeXpath.BySafeXpath(SharedFolderLctMask, folderName)).Any();
        }

        /// <summary>
        /// Is Folder Shared With Me
        /// </summary>
        /// <param name="folderName"> Folder to check </param>
        /// <returns> True if folder is shared, false otherwise </returns>
        public bool IsSharedWithMeFolder(string folderName)
            => DriverExtensions.GetElements(
                DriverExtensions.WaitForElement(this.ComponentLocator),
                SafeXpath.BySafeXpath(SharedWithMeFolderLctMask, folderName)).Any();

        /// <summary>
        /// clicks on the Trash link to open Trash Page from LeftResearchOrganizer Section
        /// </summary>
        /// <returns>Returns the Trash Page</returns>
        public T OpenTrashView<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(TrashFolderLinkLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Select a folder in the folder tree indicated by the folder name
        /// If folder is disable - expend folder tree till find the first coincidence
        /// </summary>
        /// <param name="folderName"> Folder name to select </param>
        public virtual void SelectFolderByName(string folderName)
        {
            if (!this.IsFolderSelected(folderName))
            {
                IWebElement folder = this.TryFindFolderInFolderTree(folderName);

                if (folder != null)
                {
                    DriverExtensions.Click(folder);
                    DriverExtensions.WaitForJavaScript();
                    DriverExtensions.WaitForPageLoad();
                }
                else
                {
                    throw new Exception("Unable to Select folder with name " + folderName);
                }
            }
        }

        /// <summary>
        /// Expand the folder tree until the folder is visible, then select it
        /// Folder path Example:  "All Matter Documents/Matter Folders R/Restricted Matter Multi Internal and Externals37/Lewis Lewis"
        /// </summary>
        /// <param name="folderPath"> The folder Path. </param>
        public void SelectFolderByPath(string folderPath)
        {
            this.CollapseFolderTree();

            // Define root and select open all headers 
            IWebElement root = DriverExtensions.WaitForElementDisplayed(this.ComponentLocator, 10000);
            IEnumerable<IWebElement> foldersHeaders = DriverExtensions.GetElements(root, ExpandedFolderLocator);

            foreach (IWebElement header in foldersHeaders)
            {
                header.Click();
            }

            string targetFolder = this.ExpandFolderTreeByPath(folderPath);

            // Click the folder
            By folder = SafeXpath.BySafeXpath(FolderItemDescriptorLctMask, targetFolder);
            DriverExtensions.WaitForElement(root, folder).Click();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Select a folder in the folder tree indicated by the folder name
        /// If folder is disable - expend folder tree till find the first coincidence
        /// </summary>
        /// <param name="folderName"> Folder name to select </param>
        public virtual void SelectFolderCheckboxByName(string folderName)
        {
            this.TryFindFolderInFolderTree(folderName);
            this.FolderCheckbox(folderName).Set(true);
        }

        /// <summary>
        /// Collapse Folder Tree
        /// Click all visible minus icons
        /// </summary>
        protected virtual void CollapseFolderTree()
        {
            List<IWebElement> colButtons = this.GetAllVisibleCollapseButtons();
            if (colButtons.Any())
            {
                
                DriverExtensions.Click(colButtons.First());
                DriverExtensions.WaitForJavaScript();
                this.CollapseFolderTree();
            }
        }

        /// <summary>
        /// Get all visible xpand Buttons
        /// </summary>
        /// <returns></returns>
        protected List<IWebElement> GetAllVisibleExpandButtons()
        {
            DriverExtensions.WaitForJavaScript();
            DriverExtensions.WaitForPageLoad();
            return
                DriverExtensions.GetElements(this.ComponentLocator, ExpandButtonLocator).Where(el => el.Displayed).ToList();
        }

        /// <summary>
        /// Expand Folder Tree
        /// Click first visible plus icons
        /// </summary>
        /// <returns> True if the folder tree expandable </returns>
        private bool ExpandFirstExpandableFolder()
        {
            this.RootFolder.WaitForElementDisplayed();
            this.GetAllVisibleExpandButtons().FirstOrDefault()?.Click();

            DriverExtensions.WaitForJavaScript();
            DriverExtensions.WaitForPageLoad();
            return this.GetAllVisibleExpandButtons().Any();
        }

        private List<IWebElement> GetAllVisibleCollapseButtons()
            => DriverExtensions.GetElements(this.ComponentLocator, CollapseButtonLocator).Where(el => el.Displayed).ToList();
               

        private IWebElement GetFolderItemByName(string folderName)
        {
            this.RootFolder.WaitForElementDisplayed();
            return
                DriverExtensions.GetElements(this.ComponentLocator, FolderItemLinkLocator)
                                .Where(elem => elem.Text.StartsWith(folderName))
                                .FirstOrDefault(elem => elem.IsElementInView());
        }

        private bool IsFolderPresented(string folderName) => this.GetFolderItemByName(folderName) != null;

        /// <summary>
        /// Expand Folder Tree until find the folder
        /// Find folder in folder tree
        /// </summary>
        /// <param name="folderName"> The folder Name. </param>
        /// <returns> WebElement if it exist, or null </returns>
        private IWebElement TryFindFolderInFolderTree(string folderName)
        {
            this.RootFolder.WaitForElementDisplayed();

            if (!this.IsFolderPresented(folderName))
            {
                this.CollapseFolderTree();
                bool isFoldetTreeExpandable = true;
                while (isFoldetTreeExpandable)
                {
                    isFoldetTreeExpandable = !this.IsFolderPresented(folderName) && this.ExpandFirstExpandableFolder();
                }
            }

            return this.GetFolderItemByName(folderName);
        }
    }
}