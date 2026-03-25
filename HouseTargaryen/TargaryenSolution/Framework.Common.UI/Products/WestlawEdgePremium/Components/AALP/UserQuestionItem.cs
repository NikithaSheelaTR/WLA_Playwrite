namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Items;
    using OpenQA.Selenium;

    /// <summary>
    /// User question item
    /// </summary>
    public class UserQuestionItem : BaseItem
    {
        /// <summary>
        /// Constructor
        /// Initializes a new instance of the <see cref="UserQuestionItem"/> class. 
        /// </summary>
        /// <param name="containerElement"></param>
        public UserQuestionItem(IWebElement containerElement) : base(containerElement)
        {
        }

        /// <summary>
        /// Question label
        /// </summary>
        public ILabel QuestionLabel => new Label(this.Container);
    }
}