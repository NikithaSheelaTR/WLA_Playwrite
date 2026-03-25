using Framework.Common.UI.Interfaces.Elements;
using Framework.Common.UI.Products.Shared.Components;
using Framework.Common.UI.Products.Shared.Elements.Buttons;
using Framework.Common.UI.Products.Shared.Elements.Textboxes;
using Framework.Common.UI.Products.Shared.Items;
using Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Common.UI.Products.CoCounsel.Components
{
    /// <summary>
    /// Chat Component
    /// </summary>
    public class ChatComponent : BaseModuleRegressionComponent
    {
        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => By.XPath("//div[@class='ai-assistant']");

        private readonly By ChatTextBoxLocator = By.Id("cocounsel-prompt");
        private readonly By SendButtonLocator = By.Id("send");

        /// <summary>
        /// Chat textbox
        /// </summary>
        public ITextbox ChatTextBox => new Textbox(ChatTextBoxLocator);

        /// <summary>
        /// Send button
        /// </summary>
        public IButton SendButton => new Button(SendButtonLocator);

        /// <summary>
        /// Question And AnswerComponent
        /// </summary>
        public QuestionAndAnswerComponent ResponceComponent => new QuestionAndAnswerComponent();
    }
}
