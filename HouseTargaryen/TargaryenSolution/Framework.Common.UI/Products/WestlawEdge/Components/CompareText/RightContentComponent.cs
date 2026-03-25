namespace Framework.Common.UI.Products.WestlawEdge.Components.CompareText
{
    using OpenQA.Selenium;

    /// <summary>
    /// Right content component on the Side By Side View tab
    /// </summary>
    public class RightContentComponent : BaseContentComponent
    {
        private static readonly By ContainerLocator = By.XPath(".//div[@class='co_overlayBox_rightContent']");
        
        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}