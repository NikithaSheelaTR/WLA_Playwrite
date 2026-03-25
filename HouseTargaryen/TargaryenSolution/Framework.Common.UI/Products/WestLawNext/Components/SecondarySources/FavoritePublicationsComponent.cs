namespace Framework.Common.UI.Products.WestLawNext.Components.SecondarySources
{
    using System;
    using System.Linq;
    using System.Net;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Enums;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Execution;

    using OpenQA.Selenium;

    /// <summary>
    /// Favorite Publications Widget on new Secondary Sources page
    /// </summary>
    public class FavoritePubLibraryComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.Id("co_libraryFavotieWidget");

        private EnumPropertyMapper<FavoritePublications, WebElementInfo> favoritePublications;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the FavoritePublications enumeration to WebElementInfo map.
        /// </summary>
        private EnumPropertyMapper<FavoritePublications, WebElementInfo> FavoritePublicationsMap
            => this.favoritePublications = this.favoritePublications ?? EnumPropertyModelCache.GetMap<FavoritePublications, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/Components");


        /// <summary>
        /// The are five items displayed.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        public int GetCountOfDisplayedItems()
            => DriverExtensions.GetElements(
                    By.XPath(this.FavoritePublicationsMap[FavoritePublications.PublicationInWidget].LocatorString))
                                .Where(el => el.Displayed)
                                .Count();

        /// <summary>
        /// The click scope icon for pub in widget.
        /// </summary>
        /// <param name="pubName">The pub name.</param>
        /// <returns>The <see cref="ScopeDialog"/>.</returns>
        public ScopeDialog ClickScopeIconForPubInWidget(string pubName)
        {
            IWebElement element =
                DriverExtensions.GetElements(By.XPath(string.Format(this.FavoritePublicationsMap[FavoritePublications.ScopeIconPubWidget].LocatorMask, pubName)))
                                .FirstOrDefault(el => el.Displayed);

            SafeMethodExecutor.Execute(() => element?.Click());
            return new ScopeDialog();
        }

        /// <summary>
        /// The expand collapse widget.
        /// </summary>
        /// <param name="expandCollapse">The expand collapse.</param>
        public void ExpandCollapseWidget(bool expandCollapse)
        {
            if (expandCollapse && DriverExtensions.IsDisplayed(By.XPath(this.FavoritePublicationsMap[FavoritePublications.ExpandWidgetButton].LocatorString)))
            {
                DriverExtensions.WaitForElement(By.XPath(this.FavoritePublicationsMap[FavoritePublications.ExpandWidgetButton].LocatorString)).Click();
            }
            else if (expandCollapse == false && DriverExtensions.IsDisplayed(By.XPath(this.FavoritePublicationsMap[FavoritePublications.CollapseWidgetButton].LocatorString)))
            {
                DriverExtensions.WaitForElement(By.XPath(this.FavoritePublicationsMap[FavoritePublications.CollapseWidgetButton].LocatorString)).Click();
            }
            DriverExtensions.WaitForAnimation();
        }

        /// <summary>
        /// The is carousel displayed.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsCarouselDisplayed() => DriverExtensions.IsDisplayed(By.Id(this.FavoritePublicationsMap[FavoritePublications.Carousel].Id));

        /// <summary>
        /// The is cover image for pub name displayed.
        /// </summary>
        /// <param name="pubName">The pub name.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsCoverImageForPubNameDisplayed(string pubName)
        {
            try
            {
                string imageUrl = DriverExtensions
                    .GetElements(By.XPath(string.Format(this.FavoritePublicationsMap[FavoritePublications.CoverImage].LocatorMask, pubName)))
                    .First(el => el.Displayed).GetAttribute("src");
                var req = (HttpWebRequest)WebRequest.Create(imageUrl);
                req.Method = WebRequestMethods.Http.Get;
                req.Headers.Add("cookie", String.Join(String.Empty, DriverExtensions.GetCookies().Select(coockie => coockie.Name + "=" + coockie.Value + "; ")));
                var resp = (HttpWebResponse)req.GetResponse();
                return resp.StatusCode == HttpStatusCode.OK && resp.ContentType.StartsWith("image");
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// The is next button disabled.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsNextButtonDisabled()
            => DriverExtensions.GetElement(By.XPath(this.FavoritePublicationsMap[FavoritePublications.NextButton].LocatorString)).GetAttribute("class")
            .Contains("disabled");

        /// <summary>
        /// The is next button present.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsNextButtonDisplayed()
            => DriverExtensions.IsDisplayed(By.XPath(this.FavoritePublicationsMap[FavoritePublications.NextButton].LocatorString));

        /// <summary>
        /// The is no pub header text correct.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsNoPubHeaderTextCorrect()
            => DriverExtensions.GetText(By.XPath(this.FavoritePublicationsMap[FavoritePublications.NoPuBsHeader].LocatorString))
            .Trim().Equals(this.FavoritePublicationsMap[FavoritePublications.NoPuBsHeader].Text);

        /// <summary>
        /// The is no pub message correct.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsNoPubMessageCorrect()
            => DriverExtensions.GetText(By.XPath(this.FavoritePublicationsMap[FavoritePublications.NoPuBsText].LocatorString))
            .Trim().Equals(this.FavoritePublicationsMap[FavoritePublications.NoPuBsText].Text);

        /// <summary>
        /// The is previous button disabled.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsPreviousButtonDisabled()
            => DriverExtensions.GetElement(By.XPath(this.FavoritePublicationsMap[FavoritePublications.PreviousButton].LocatorString))
            .GetAttribute("class").Contains("disabled");

        /// <summary>
        /// The is previous button present.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsPreviousButtonDisplayed()
            => DriverExtensions.IsDisplayed(By.XPath(this.FavoritePublicationsMap[FavoritePublications.PreviousButton].LocatorString));

        /// <summary>
        /// The is pub name displayed in widget.
        /// </summary>
        /// <param name="pubName">The pub name.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsPubNameDisplayedInWidget(string pubName)
            => DriverExtensions.GetElements(By.XPath(string.Format(this.FavoritePublicationsMap[FavoritePublications.PublicationName].LocatorMask, pubName)))
            .Any(el => el.Displayed);

        /// <summary>
        /// The is scope icon present for pub in widget.
        /// </summary>
        /// <param name="pubName">The pub name.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsScopeIconPresentForPubInWidget(string pubName)
            => DriverExtensions.GetElements(
                        By.XPath(
                            string.Format(
                                this.FavoritePublicationsMap[FavoritePublications.ScopeIconPubWidget]
                                    .LocatorMask,
                                pubName))).FirstOrDefault(el => el.Displayed).IsDisplayed();

        /// <summary>
        /// The is widget collapsed.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsWidgetCollapsed() =>
            DriverExtensions.IsDisplayed(By.XPath(this.FavoritePublicationsMap[FavoritePublications.ExpandWidgetButton].LocatorString));

        /// <summary>
        /// The is widget expanded.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsWidgetExpanded() =>
            DriverExtensions.IsDisplayed(By.XPath(this.FavoritePublicationsMap[FavoritePublications.CollapseWidgetButton].LocatorString));

        /// <summary>
        /// The is widget title correct.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsWidgetTitleCorrect()
            => DriverExtensions.GetImmediateText(By.XPath(this.FavoritePublicationsMap[FavoritePublications.FavoriteWidgetTitle].LocatorString))
            .Trim().Equals(this.FavoritePublicationsMap[FavoritePublications.FavoriteWidgetTitle].Text);

        /// <summary>
        /// The scroll back forward carousel.
        /// </summary>
        /// <param name="numberOfItemsToScroll">The number of items to scroll.</param>
        /// <param name="backForward">The back forward.</param>
        public void ScrollBackForwardCarousel(int numberOfItemsToScroll, bool backForward = true)
        {
            for (int i = 1; i <= numberOfItemsToScroll; i++)
            {
                DriverExtensions.WaitForElement(
                    backForward
                        ? By.XPath(
                            this.FavoritePublicationsMap[FavoritePublications.NextButton].LocatorString)
                        : By.XPath(
                            this.FavoritePublicationsMap[FavoritePublications.PreviousButton].LocatorString))
                                .Click();
                DriverExtensions.WaitForJavaScript();
                DriverExtensions.WaitForAnimation();
            }
        }
    }
}