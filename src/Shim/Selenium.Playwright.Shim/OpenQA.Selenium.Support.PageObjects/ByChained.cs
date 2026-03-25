using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OpenQA.Selenium.Support.PageObjects
{
    /// <summary>
    /// Mechanism to locate elements within a document using a series of lookups,
    /// each one building on the previous one.
    /// </summary>
    public class ByChained : By
    {
        private readonly By[] _bys;

        public ByChained(params By[] bys)
        {
            _bys = bys;
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            ISearchContext current = context;
            for (int i = 0; i < _bys.Length; i++)
            {
                if (i < _bys.Length - 1)
                {
                    current = current.FindElement(_bys[i]);
                }
                else
                {
                    return current.FindElement(_bys[i]);
                }
            }
            throw new NoSuchElementException("Could not find element by chained locators");
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            ISearchContext current = context;
            for (int i = 0; i < _bys.Length; i++)
            {
                if (i < _bys.Length - 1)
                {
                    current = current.FindElement(_bys[i]);
                }
                else
                {
                    return current.FindElements(_bys[i]);
                }
            }
            return new ReadOnlyCollection<IWebElement>(new List<IWebElement>());
        }

        public override string ToString()
        {
            return "By.Chained([" + string.Join(", ", _bys.Select(b => b.ToString())) + "])";
        }
    }
}
