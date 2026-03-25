namespace Framework.Common.UI.Products.Shared.Elements
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Constants;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Execution;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    /// <summary>
    /// The elements collection. The implementation if IReadOnlyCollection. Used for a collection of elements
    /// </summary>
    /// <typeparam name="TElement">The elements type</typeparam>
    public class ElementsCollection<TElement> : IReadOnlyCollection<TElement> where TElement : BaseWebElement
    {
        /// <summary>
        /// The IEnumerable inner implementation.
        /// </summary>
        private readonly IEnumerable<TElement> enumerableImplementation;

        /// <summary>
        /// Initializes a new instance of the <see cref="ElementsCollection{TElement}"/> class.
        /// </summary>
        /// <param name="elementsBys">
        /// The elements by.
        /// </param>
        public ElementsCollection(params By[] elementsBys)
        {
            SafeMethodExecutor.Execute(() => DriverExtensions.WaitForElement(new ByChained(elementsBys), WebDriverTimeouts.ElementDisplayed));

            IList<IWebElement> elements = DriverExtensions.GetElements(elementsBys);

            this.enumerableImplementation = elements.Any()
                                                ? elements.Select(item => (TElement)Activator.CreateInstance(typeof(TElement), item))
                                                : new List<TElement>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElementsCollection{TElement}"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <param name="elementsBys">
        /// The elements by.
        /// </param>
        public ElementsCollection(IWebElement container, params By[] elementsBys)
        {
            SafeMethodExecutor.Execute(() => DriverExtensions.WaitForElement(container, new ByChained(elementsBys), WebDriverTimeouts.ElementDisplayed));

            IList<IWebElement> elements = DriverExtensions.GetElements(container, elementsBys);

            this.enumerableImplementation = elements.Any()
                                                ? elements.Select(item => (TElement)Activator.CreateInstance(typeof(TElement), item))
                                                : new List<TElement>();
        }

        /// <inheritdoc />
        public int Count => this.enumerableImplementation.Count();

        /// <inheritdoc />
        public IEnumerator<TElement> GetEnumerator() => this.enumerableImplementation.GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)this.enumerableImplementation).GetEnumerator();
    }
}