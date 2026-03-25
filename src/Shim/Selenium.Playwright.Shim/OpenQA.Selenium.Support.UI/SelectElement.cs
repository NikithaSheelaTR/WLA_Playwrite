using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Selenium.Playwright.Shim.Impl;

namespace OpenQA.Selenium.Support.UI
{
    public class SelectElement
    {
        private readonly IWebElement _element;

        public SelectElement(IWebElement element)
        {
            _element = element;

            string tagName = element.TagName;
            if (tagName == null || tagName.ToLowerInvariant() != "select")
            {
                throw new WebDriverException("Element is not a <select> element");
            }
        }

        public IWebElement WrappedElement => _element;

        public bool IsMultiple
        {
            get
            {
                string multiple = _element.GetAttribute("multiple");
                return multiple != null && multiple != "false";
            }
        }

        public IList<IWebElement> Options
        {
            get
            {
                return _element.FindElements(By.TagName("option")).ToList();
            }
        }

        public IWebElement SelectedOption
        {
            get
            {
                foreach (var option in Options)
                {
                    if (option.Selected) return option;
                }
                throw new NoSuchElementException("No option is currently selected");
            }
        }

        public IList<IWebElement> AllSelectedOptions
        {
            get
            {
                return Options.Where(o => o.Selected).ToList();
            }
        }

        public void SelectByText(string text)
        {
            if (_element is PlaywrightWebElement pwe)
            {
                SyncHelper.RunSync(() => pwe.Locator.SelectOptionAsync(
                    new Microsoft.Playwright.SelectOptionValue { Label = text }));
            }
            else
            {
                foreach (var option in Options)
                {
                    if (option.Text.Trim() == text)
                    {
                        option.Click();
                        return;
                    }
                }
                throw new NoSuchElementException($"Cannot locate option with text: {text}");
            }
        }

        public void SelectByValue(string value)
        {
            if (_element is PlaywrightWebElement pwe)
            {
                SyncHelper.RunSync(() => pwe.Locator.SelectOptionAsync(
                    new Microsoft.Playwright.SelectOptionValue { Value = value }));
            }
            else
            {
                foreach (var option in Options)
                {
                    if (option.GetAttribute("value") == value)
                    {
                        option.Click();
                        return;
                    }
                }
                throw new NoSuchElementException($"Cannot locate option with value: {value}");
            }
        }

        public void SelectByIndex(int index)
        {
            if (_element is PlaywrightWebElement pwe)
            {
                SyncHelper.RunSync(() => pwe.Locator.SelectOptionAsync(
                    new Microsoft.Playwright.SelectOptionValue { Index = index }));
            }
            else
            {
                var options = Options;
                if (index < 0 || index >= options.Count)
                    throw new NoSuchElementException($"Cannot locate option with index: {index}");
                options[index].Click();
            }
        }

        public void DeselectAll()
        {
            if (!IsMultiple)
                throw new WebDriverException("You may only deselect all options on a multi-select");

            foreach (var option in Options)
            {
                if (option.Selected) option.Click();
            }
        }

        public void DeselectByText(string text)
        {
            foreach (var option in Options)
            {
                if (option.Text.Trim() == text && option.Selected)
                {
                    option.Click();
                    return;
                }
            }
        }

        public void DeselectByValue(string value)
        {
            foreach (var option in Options)
            {
                if (option.GetAttribute("value") == value && option.Selected)
                {
                    option.Click();
                    return;
                }
            }
        }

        public void DeselectByIndex(int index)
        {
            var options = Options;
            if (index >= 0 && index < options.Count && options[index].Selected)
            {
                options[index].Click();
            }
        }
    }
}
