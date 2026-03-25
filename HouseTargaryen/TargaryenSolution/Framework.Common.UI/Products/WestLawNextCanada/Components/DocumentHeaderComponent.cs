namespace Framework.Common.UI.Products.WestLawNextCanada.Components
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.WestLawNextCanada.Enums;
    using Framework.Common.UI.Products.WestLawNextCanada.Pages;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using sun.reflect.generics.tree;
    using System.Collections.ObjectModel;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;

    /// <summary>
    /// Document Headers
    /// </summary>
    public class DocumentHeaderComponent : BaseModuleRegressionComponent
    {
        private static readonly By DocumentHeaderContainerLocator = By.XPath("//*[@id='co_docHeader']");

        private static readonly By DocumentTitleLocator = By.XPath(".//*[@id='title']");

        private static readonly By DocumentTitleInfoLocator = By.XPath(".//*[@id='titleInfo']");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => DocumentHeaderContainerLocator;

        /// <summary>
        /// Document Title   Label
        /// </summary>
        public ILabel DocumentTitleLabel => new Label(this.ComponentLocator, DocumentTitleLocator);

        /// <summary>
        /// Document Title Info  Label
        /// </summary>
        public ILabel DocumentTitleInfoLabel => new Label(this.ComponentLocator, DocumentTitleInfoLocator);

        ///
        ///  Returns a HTML color string from a hex or rgb value string
        /// @param  colorText     The Color rgb or hex value as a String
        ///  @return The resulting HTML color representation of the color   
        public String GetColorValue(String colorText)
        {
            Color color = Color.Black;

            Regex regex = new Regex(@"rgba?\((?:[ ]*([0-9]+)[ ]*)(?:,[ ]*([0-9]+)[ ]*)(?:,[ ]*([0-9]+)[ ]*)(?:,[ ]*([0-9]+)[ ]*)?\)");
            Match match = regex.Match(colorText);
            if (match.Success)
            {
                GroupCollection groups = match.Groups;
                if (groups[4].Value.Equals(""))
                {
                    color = Color.FromArgb(Convert.ToInt32(groups[1].Value), Convert.ToInt32(groups[2].Value), Convert.ToInt32(groups[3].Value));
                }
                else
                {
                    color = Color.FromArgb(Convert.ToInt32(groups[4].Value), Convert.ToInt32(groups[1].Value), Convert.ToInt32(groups[2].Value), Convert.ToInt32(groups[3].Value));
                }
            }
            else if (colorText.StartsWith("#"))
            {
                color = ColorTranslator.FromHtml(colorText);
            }
            else
                color = Color.FromName(colorText);

            return ColorTranslator.ToHtml(color);
        }
    }
}


