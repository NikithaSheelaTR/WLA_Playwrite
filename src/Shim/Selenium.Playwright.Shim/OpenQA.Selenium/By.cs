using System;

namespace OpenQA.Selenium
{
    public class By
    {
        internal string Mechanism { get; private set; }
        internal string Value { get; private set; }

        protected By() { }

        private By(string mechanism, string value)
        {
            this.Mechanism = mechanism;
            this.Value = value;
        }

        public static By Id(string idToFind) => new By("id", idToFind);
        public static By Name(string nameToFind) => new By("name", nameToFind);
        public static By ClassName(string classNameToFind) => new By("className", classNameToFind);
        public static By CssSelector(string cssSelectorToFind) => new By("cssSelector", cssSelectorToFind);
        public static By LinkText(string linkTextToFind) => new By("linkText", linkTextToFind);
        public static By PartialLinkText(string partialLinkTextToFind) => new By("partialLinkText", partialLinkTextToFind);
        public static By TagName(string tagNameToFind) => new By("tagName", tagNameToFind);
        public static By XPath(string xpathToFind) => new By("xpath", xpathToFind);

        public virtual IWebElement FindElement(ISearchContext context)
        {
            return context.FindElement(this);
        }

        public virtual System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            return context.FindElements(this);
        }

        public override string ToString() => $"By.{Mechanism}: {Value}";

        public override bool Equals(object obj)
        {
            if (obj is By other)
                return this.Mechanism == other.Mechanism && this.Value == other.Value;
            return false;
        }

        public override int GetHashCode() => (Mechanism + Value).GetHashCode();
    }
}
