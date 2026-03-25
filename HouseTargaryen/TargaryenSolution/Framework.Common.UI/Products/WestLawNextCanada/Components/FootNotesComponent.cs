using Framework.Common.UI.Interfaces.Elements;
using Framework.Common.UI.Products.Shared.Components;
using Framework.Common.UI.Products.Shared.Elements.Labels;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Common.UI.Products.WestLawNextCanada.Components
{
    /// <summary>
    /// FootNote Component Features
    /// </summary>
    public class FootNotesComponent : BaseModuleRegressionComponent
    {
        private static readonly By FootNoteContainerLocator = By.XPath("//*[@id='co_document']");
    
        private static readonly By FootNoteBodyLocator = By.XPath(".//*[@class='co_footnoteBody']");

         /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => FootNoteContainerLocator;

        /// <summary>
        ///Foot Note Label
        /// </summary>
        /// <returns> Summary Text </returns>
        public ILabel FootNoteLabel => new Label(this.ComponentLocator,FootNoteBodyLocator);
    }
}
