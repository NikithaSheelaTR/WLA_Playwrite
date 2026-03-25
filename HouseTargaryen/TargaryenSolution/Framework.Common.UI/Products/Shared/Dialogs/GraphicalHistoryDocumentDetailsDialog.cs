namespace Framework.Common.UI.Products.Shared.Dialogs
{
    using System;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdge.Components.History;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.ResultList;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.CommonTypes.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Dialog that pops up when clicking Open preview
    /// </summary>
    public class GraphicalHistoryDocumentDetailsDialog : BaseModuleRegressionDialog
    {
        private static readonly By PanelHeaderLocator = By.XPath("//*[@class='Panel-heading' and text()='Document preview']");
        private static readonly By DocumentTitleLocator = By.Id("co_titleLink");
        private static readonly By CloseButtonLocator = By.XPath("//button[@class='Panel-close']");
        private static readonly By FolderButtonLocator = By.XPath("//*[@id='co_docToolbarSaveToWidget']//button");
        private static readonly By DeliveryButtonLocator = By.Id("deliveryLink1");
        private static readonly By KeyCiteFlagLocator = By.XPath("//div[@class='GH-Panel-keycite-container']//a");
        private static readonly By CopyCitationButtonLocator = By.Id("co_copyCitation");
        private static readonly By BlueBoxContainerLocator = By.XPath("//div[@class='Athens-browseBox']");

        /// <summary>
        /// Browse box component
        /// </summary>
        public PrecisionFullBrowseBoxComponent BrowseBox => new PrecisionFullBrowseBoxComponent(DriverExtensions.GetElement(BlueBoxContainerLocator));

        /// <summary>
        /// Close button document details panel
        /// </summary>
        public ILabel HeaderLabel => new Label(PanelHeaderLocator);

        /// <summary>
        /// Close button document details panel
        /// </summary>
        public IButton CloseButton => new Button(CloseButtonLocator);

        /// <summary>
        /// Folder button document details panel
        /// </summary>
        public IButton FolderButton => new Button(FolderButtonLocator);

        /// <summary>
        /// Delivery button document details panel
        /// </summary>
        public IButton DeliveryButton => new Button(DeliveryButtonLocator);

        /// <summary>
        /// Document title link
        /// </summary>
        public ILink DocumentTitleLink => new Link(DocumentTitleLocator);

        /// <summary>
        /// Copy Citation button document details panel
        /// </summary>
        public IButton CopyCitationButton => new Button(CopyCitationButtonLocator);

        /// <summary>
        /// Document Preview Tab Panel
        /// </summary>
        public DocumentPreviewTabPanel DocumentPreviewTabPanel { get; } = new DocumentPreviewTabPanel();

        /// <summary>
        /// Gets the key cite flag.
        /// </summary>
        public KeyCiteFlag KeyCiteFlag
        {
            get
            {
                if (DriverExtensions.IsDisplayed(KeyCiteFlagLocator))
                {
                    string flagClass = DriverExtensions.GetAttribute("class", KeyCiteFlagLocator);
                    return flagClass.GetEnumValueByPropertyModel<KeyCiteFlag, WebElementInfo>(
                        model => model.ClassName,
                        String.Empty,
                        @"Resources/EnumPropertyMaps/WestlawEdge/Folders");
                }

                return KeyCiteFlag.NoFlag;
            }
        }
    }
}