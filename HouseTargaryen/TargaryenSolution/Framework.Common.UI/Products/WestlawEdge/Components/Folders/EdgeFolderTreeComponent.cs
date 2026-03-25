namespace Framework.Common.UI.Products.WestlawEdge.Components.Folders
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.Folder;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// EdgeFolderTreeComponent
    /// </summary>
    public class EdgeFolderTreeComponent : FolderTreeComponent
    {
        private static readonly By RootFolderLocator = By.XPath(".//li[.//ul[contains(@class, 'TreeViewChildWrapper')]]/div[@data-position='0']");
        private static readonly By SelectedFolderLocator = By.XPath(".//div[contains(@class, 'TreeButton--isSelected')]");
        private static readonly By FolderItemLinkLocator = By.XPath(".//span[@class = 'TreeViewItemName']");
        private static readonly By CollapseButtonLocator = By.XPath(".//button[@class='TreeViewItemToggle icon25 icon_addBox-gray']");
        private static readonly By CheckboxLocator = By.XPath("./ancestor::div[contains(@class, 'TreeButton')]/input[@type='checkbox']");
        private static readonly By ExpandButtonLocator = By.XPath(".//span[contains(@class, 'addBox')]");

        private const string SharedFolderLctMask = "//div[@role='button']//span[contains(@class,'folderShared') or contains(@class,'folderOpenShared')]/following-sibling::span[text()={0}]";

        /// <summary>
        /// Initializes a new instance of the <see cref="EdgeFolderTreeComponent"/> class. 
        /// </summary>
        /// <param name="rootElement"> Root element for the tree </param>
        public EdgeFolderTreeComponent(By rootElement) : base(rootElement)
        {
        }

        /// <summary>
        /// Get list of collapse buttons
        /// </summary>
        public IReadOnlyCollection<IButton> CollapseButtons => DriverExtensions.GetElements(this.ComponentLocator, CollapseButtonLocator)
            .Select(i => new Button(i)).ToList();

        /// <summary>
        /// Get list of expand buttons
        /// </summary>
        public IReadOnlyCollection<IButton> ExpandButtons => DriverExtensions.GetElements(this.ComponentLocator, ExpandButtonLocator)
            .Select(i => new Button(i)).ToList();

        private IWebElement RootFolder
        {
            get
            {
                DriverExtensions.WaitForElement(RootFolderLocator);
                return DriverExtensions.GetElement(this.ComponentLocator, RootFolderLocator);
            }
        }

        /// <summary>
        /// Get the root folder name for the user
        /// </summary>
        /// <returns>Root folder name</returns>
        public override string GetRootFolderName() => this.RootFolder.Text;

        /// <summary>
        /// Select a folder in the folder tree indicated by the folder name
        /// If folder is disable - expend folder tree till find the first coincidence
        /// </summary>
        /// <param name="folderName"> Folder name to select </param>
        public override void SelectFolderByName(string folderName)
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
        /// Select a folder in the folder tree indicated by the folder name
        /// If folder is disable - expend folder tree till find the first coincidence
        /// </summary>
        /// <param name="folderName"> Folder name to select </param>
        public override void SelectFolderCheckboxByName(string folderName)
        {
            IWebElement checkbox = DriverExtensions.WaitForElement(this.TryFindFolderInFolderTree(folderName), CheckboxLocator);
            checkbox.SetCheckbox(true);
        }

        /// <summary>
        /// Determine if the folder exists (shouldn't matter if it is visible or not)
        /// </summary>
        /// <param name="folderName"> Name of the folder to search for </param>
        /// <returns> True if the folder exists, false otherwise </returns>
        public override bool IsFolderExist(string folderName) => this.TryFindFolderInFolderTree(folderName) != null;

        /// <summary>
        /// Select the root folder
        /// </summary>
        public override void ClickRootFolder()
        {
            DriverExtensions.WaitForElement(RootFolderLocator);
            this.RootFolder.Click();
        }

        /// <summary>
        /// Get list of folders titles
        /// </summary>
        public List<string> GetFoldersTitles() => DriverExtensions.GetElements(this.ComponentLocator, FolderItemLinkLocator)
            .Select(e => e.Text).ToList();

        /// <summary>
        /// Returns true if the indicated folder is selected
        /// </summary>
        /// <param name="folderName"> Folder to check </param>
        /// <returns> True if folder selected, false otherwise </returns>
        public override bool IsFolderSelected(string folderName) =>
            DriverExtensions.GetElements(this.ComponentLocator, SelectedFolderLocator).Select(e => e.Text).Contains(folderName);

        /// <summary>
        /// Checks the icon of the indicated folder and returns true if the folder is blue
        /// </summary>
        /// <param name="folderName"> Folder to check </param>
        /// <returns> True if folder is shared, false otherwise </returns>
        public override bool IsFolderShared(string folderName)
        {
            this.ExpandFolderTree(folderName);
            DriverExtensions.WaitForJavaScript(); // Additional wait allows tree to load
            return DriverExtensions.GetElements(SafeXpath.BySafeXpath(SharedFolderLctMask, folderName)).Any();
        }

        /// <summary>
        /// The expand folder tree till find first folder with required name
        /// </summary>
        /// <param name="folderName"> The folder name. </param>
        public override void ExpandFolderTree(string folderName) => this.TryFindFolderInFolderTree(folderName);
        
        /// <summary>
        /// Collapse Folder Tree
        /// Click all visible minus icons
        /// </summary>
        protected override void CollapseFolderTree()
        {
            if(this.CollapseButtons.Any(b => b.Displayed))
            {
                this.CollapseButtons.First().Click();
                DriverExtensions.WaitForJavaScript();
                this.CollapseFolderTree();
            }
        }

        private bool IsFolderDisplay(string folderName) => this.GetFolderItemByName(folderName) != null;
        
        private IWebElement GetFolderItemByName(string folderName) =>
            DriverExtensions.GetElements(this.ComponentLocator, FolderItemLinkLocator)
                                .Where(elem => elem.Text.StartsWith(folderName))
                                .FirstOrDefault(elem => elem.IsElementInView());
        
        /// <summary>
        /// Expand Folder Tree
        /// Click first visible plus icons
        /// </summary>
        /// <returns> True if the folder tree expandable </returns>
        private bool ExpandFirstExpandableFolder()
        {
            this.ExpandButtons.FirstOrDefault(i => i.Displayed)?.Click();

            DriverExtensions.WaitForJavaScript();
            DriverExtensions.WaitForPageLoad();
            return this.ExpandButtons.Any(i => i.Displayed);
        }

        /// <summary>
        /// Expand Folder Tree until find the folder
        /// Find folder in folder tree
        /// </summary>
        /// <param name="folderName"> The folder Name. </param>
        /// <returns> WebElement if it exist, or null </returns>
        private IWebElement TryFindFolderInFolderTree(string folderName)
        {
            if (!this.IsFolderDisplay(folderName))
            {
                this.CollapseFolderTree();
                bool isFolderTreeExpandable = true;
                while (isFolderTreeExpandable)
                {
                    isFolderTreeExpandable = !this.IsFolderDisplay(folderName) && this.ExpandFirstExpandableFolder();
                }
            }
            return this.GetFolderItemByName(folderName);
        }
    }
}