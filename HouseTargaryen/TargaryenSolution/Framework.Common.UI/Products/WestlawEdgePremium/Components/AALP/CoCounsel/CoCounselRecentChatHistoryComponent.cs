using Framework.Common.UI.Products.Shared.Components;
using Framework.Common.UI.Products.Shared.Items;
using Framework.Common.UI.Products.WestlawEdgePremium.DropDowns;
using Framework.Common.UI.Products.WestlawEdgePremium.Items.AALP;
using Framework.Common.UI.Products.WestlawEdgePremium.Items.AALP.CoCounsel;
using OpenQA.Selenium;

namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP.CoCounsel
{
    /// <summary>
    /// CoCounsel Recent Chat History Component
    /// </summary>
    public class CoCounselRecentChatHistoryComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@class='matter-sidebar']");
        private static readonly By RecentChatItemLocator = By.XPath(".//div[@class = 'chat-list-container recent-chats']//div[@class = 'selectable-item__child-container']");
        private static readonly By NewChatDropdownContainerLocator = By.XPath("//div[@data-testid='new-folder-or-chat-button-container']");

        /// <summary>
        /// CoCounsel Recent Chat list
        /// </summary>
        public ItemsCollection<CoCounselRecentChatItem> CoCounselRecentChatItems => new ItemsCollection<CoCounselRecentChatItem>(this.ComponentLocator, RecentChatItemLocator);

        /// <summary>
        /// New Chat dropdown
        /// </summary>
        public CoCounselNewChatDropdown NewChatDropdown { get; } = new CoCounselNewChatDropdown(NewChatDropdownContainerLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}
