namespace Framework.Common.UI.Products.Shared.Components.AdvancedSearchTemplates
{
    using System.Text.RegularExpressions;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Raw.WestlawEdge.Utils.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The find and key cite template item.
    /// </summary>
    public class FindAndKeyCiteTemplateItem : BaseItem
    {
        private const string TransformRegEx =
            "\\s?<[/]?span>\\s?|<span class.+>|<input class.+>|\\stype=\"text\".[^>]+|\\n|\\t|\\r|amp;";

        private static readonly By ExampleLabelLocator = By.CssSelector("span.co_FindAndKeycite_example");

        private static readonly By GoButtonLocator = By.CssSelector("input.co_primaryBtn");

        private static readonly By InputLocator = By.XPath("input[@type='text']");

        /// <summary>
        /// Initializes a new instance of the <see cref="FindAndKeyCiteTemplateItem"/> class. 
        /// </summary>
        /// <param name="container"> Container </param>
        public FindAndKeyCiteTemplateItem(IWebElement container)
            : base(container)
        {
        }

        /// <summary>
        /// Gets normalized example string.
        /// </summary>
        public string NormalizedExample
            =>
                DriverExtensions.GetElement(this.Container, ExampleLabelLocator)
                                .Text.Replace("Example: ", string.Empty);

        /// <summary>
        /// Gets template view
        /// Extract span text and text inputs
        /// </summary>
        public string TemplateView => Regex.Replace(this.Container.InnerHtml(), TransformRegEx, string.Empty);

        /// <summary>
        /// Click go button.
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickGoButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.Click(this.Container, GoButtonLocator);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Detects if Example matched string value
        /// </summary>
        /// <param name="value">string value</param>
        /// <returns>Boolean</returns>
        public bool IsExampleMatched(string value)
        {
            // value = I.R.C.§ 104
            // NormalizedExample = 104
            // result = Match

            // value TAX CT Rule 53
            // NormalizedExample = TAX CT Rule 53
            // result = Match
            return value.Trim().EndsWith(this.NormalizedExample);
        }

        /// <summary>
        /// Type query.
        /// </summary>
        /// <param name="query"> The query. </param>
        /// <returns> Find And KeyCite Template Item </returns>
        public FindAndKeyCiteTemplateItem Type(string query)
        {
            // Match query with example.
            const string GroupRegEx = "(.+)";
            string regEx = Regex.Escape(this.TemplateView).Replace("<input>", GroupRegEx);
            if (Regex.IsMatch(query, regEx))
            {
                // Determine text to type for each text input    
                MatchCollection matches = Regex.Matches(query, regEx);

                for (int i = 0; i < matches[0].Groups.Count - 1; i++)
                {
                    IWebElement textInput = DriverExtensions.GetElements(this.Container, InputLocator)[i];
                    textInput.SendKeys(matches[0].Groups[i + 1].Value.Trim());
                }

                if (matches.Count == 0)
                {
                    DriverExtensions.GetElements(this.Container, InputLocator)[0].SendKeys(query);
                }
            }
            else
            {
                IWebElement textInput = DriverExtensions.GetElements(this.Container, InputLocator)[0];
                textInput.SendKeys(query);
            }

            return this;
        }
    }
}