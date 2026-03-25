namespace Framework.Common.UI.Products.Shared.DomainObjects
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;

    using Framework.Common.Api.Endpoints;
    using Framework.Common.Api.Endpoints.Website;
    using Framework.Common.Api.Endpoints.Website.DataModel.CustomPages;
    using Framework.Common.UI.Utils;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;
    using Framework.Core.Utils;

    using OpenQA.Selenium;

    /// <summary>
    /// Custom Pages Manager
    /// </summary>
    public class CustomPagesManager : BaseUiServiceManager
    {
        /// <summary>
        /// The Page Event Identifier.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        private string pcid;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomPagesManager"/> class.
        /// </summary>
        /// <param name="testExecutionContext">The test execution context.</param>
        /// <param name="product">The product.</param>
        public CustomPagesManager(TestExecutionContext testExecutionContext, CobaltProductInfo product)
            : base(
                testExecutionContext,
                product,
                CobaltProductId.WestlawNext,
                CobaltProductId.WlnTax,
                CobaltProductId.WlnCanada,
                CobaltProductId.WlnCanadaAws,
                CobaltProductId.WestlawEdge,
                CobaltProductId.Anz)
        {
        }

        /// <summary>
        /// Gets the session cookies.
        /// </summary>
        private CookieContainer SessionCookies { get; set; }

        /// <summary>
        /// Removes all user custom pages
        /// </summary>
        public void DeleteAllCustomPages()
        {
            this.UpdateExecutionContext();

            var websiteClient = ApiClientFactory.GetInstance<WebsiteClient>(
                null,
                this.product,
                this.environment,
                this.SessionCookies);

            string errorMessage;
            IEnumerable<string> allUserCustomPagesNames =
                websiteClient.GetAllCustomPages(this.pcid, out errorMessage)
                             .Select(item => item.CustomPageLabel)
                             .Distinct();
            this.DeleteCustomPages(allUserCustomPagesNames);
        }

        /// <summary>
        /// Create Custom Page without content
        /// </summary>
        /// <param name="title"> The title. </param>
        /// <param name="isMasterPage"> The is Master Page. </param>
        public void CreateCustomPage(string title, bool isMasterPage = false)
        {
            var client = ApiClientFactory.GetInstance<WebsiteClient>(
                null,
                this.product,
                this.environment,
                this.SessionCookies);

            CustomPageModel model = this.FormCustomPageCreationModel(title, isMasterPage);
            client.CreateCustomPageItem(model, this.pcid);
        }

        /// <summary>
        /// Create a Custom Page With Content
        /// </summary>
        /// <param name="pageTitle"> The page Title. </param>
        /// <param name="sectionTitle"> The section Title. </param>
        /// <param name="categoryPages"> The category Pages, null by default</param>
        public void CreateCustomPageWithContent(string pageTitle, string sectionTitle, List<string> categoryPages = null)
        {
            string errorMessage;

            var client = ApiClientFactory.GetInstance<WebsiteClient>(
                null,
                this.product,
                this.environment,
                this.SessionCookies);

            CustomPageModel model = this.FormCustomPageCreationModel(pageTitle, false);

            client.CreateCustomPageItem(model, this.pcid);

            CustomPageModel[] allUserCustomPages = client.GetAllCustomPages(this.pcid, out errorMessage);
            IEnumerable<string> customPage =
                allUserCustomPages.Where(cp => cp.CustomPageLabel.Equals(pageTitle)).Select(cp => cp.CustomPageId);
            List<string> list = customPage.ToList();
            model = this.FormCustomPageAddContentModel(pageTitle, false, list[0], sectionTitle, categoryPages);
            client.AddContentToCustomPage(model, this.pcid, list[0]);
        }

        /// <summary>
        /// Removes set custom pages
        /// </summary>
        /// <param name="customPageNames">Custom Pages Names</param>
        public void DeleteCustomPages(IEnumerable<string> customPageNames)
        {
            this.UpdateExecutionContext();

            if (!customPageNames?.Any() ?? false)
            {
                return;
            }

           var websiteClient = ApiClientFactory.GetInstance<WebsiteClient>(
                null,
                this.product,
                this.environment,
                this.SessionCookies);

            string errorMessage;
            var strBuilder = new StringBuilder("\n");

            CustomPageModel[] allUserCustomPages = websiteClient.GetAllCustomPages(this.pcid, out errorMessage);

            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                strBuilder.Append(errorMessage);
            }

            IEnumerable<CustomPageModel> customPagesToDelete =
                allUserCustomPages?.Where(cp => customPageNames.Contains(cp.CustomPageLabel));
            
            if (customPagesToDelete != null)
            {
                foreach (CustomPageModel customPage in customPagesToDelete)
                {
                    strBuilder.Append(
                        $"Attempt to delete custom pages '{customPage.CustomPageLabel}' was ended with status: {websiteClient.DeleteCustomPageByGuid(this.pcid, customPageGuid: customPage.CustomPageId)}\n");
                }
            }

            if (!string.IsNullOrWhiteSpace(strBuilder.ToString()))
            {
                Logger.LogInfo(string.Empty);
                Logger.LogInfo("Custom pages deleting:\n" + strBuilder);
            }
        }

        /// <summary>
        /// Get the collection of names of custom pages
        /// </summary>
        /// <returns> list of all available cuatom pages </returns>
        public IEnumerable<string> GetAvailableCustomPages()
        {
            this.UpdateExecutionContext();

            var websiteClient = ApiClientFactory.GetInstance<WebsiteClient>(
                null,
                this.product,
                this.environment,
                this.SessionCookies);

            string errorMessage = null;

            CustomPageModel[] allCustomPages = websiteClient.GetAllCustomPages(this.pcid, out errorMessage);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                Logger.LogError(errorMessage);
            }

            return allCustomPages?.Select(cp => cp.CustomPageLabel) ?? new List<string>();
        }

        /// <summary>
        /// Update a Custom Page Contacts
        /// </summary>
        public CustomPageSuperAdminModel GetCustomPageSuperAdmin()
        {
            this.UpdateExecutionContext();

            var websiteClient = ApiClientFactory.GetInstance<WebsiteClient>(
                null,
                this.product,
                this.environment,
                this.SessionCookies);
           
            return websiteClient.GetAllSuperAdminCustomPage(this.pcid);
        }

        /// <summary>
        /// Update a Custom Page Contacts
        /// </summary>
        public void UpdateContactCustomPageAdmin(AdminCustomPageModel model)
        {
            this.UpdateExecutionContext();

            var websiteClient = ApiClientFactory.GetInstance<WebsiteClient>(
                null,
                this.product,
                this.environment,
                this.SessionCookies);

            websiteClient.UpdateCustomPageItem(this.pcid, model);
        }

        /// <summary>
        /// Refresh the session cookies
        /// </summary>
        private void UpdateExecutionContext()
        {
            this.SessionCookies = BrowserPool.CurrentBrowser.GetCookies().GetCookieContainerFromCookies();
            this.pcid =
                SecurityHeadersUtils.GetPcid(
                    DriverExtensions.WaitForElement(By.TagName("html")).GetAttribute("innerHTML"));
        }

        /// <summary>
        /// Form Json For Custom Page Creation
        /// </summary>
        /// <param name="title"> custom page title  </param>
        /// <param name="isMasterPage"> True if master page, false by default </param>
        /// <returns> json body </returns>
        private CustomPageModel FormCustomPageCreationModel(string title, bool isMasterPage)
        {
            string customPageType = (isMasterPage == true) ? "mastercustompage" : "custompage.WestlawNext";

            var model = new CustomPageModel
            {
                CustomPageLabel = title,
                CustomPageType = customPageType,
                CustomPageFreeZone = false,
                CustomPageElibrary = false,
                CustomPageAllSelected = true,
                CustomPageContent = new CustomPagesContent { CustomPageCategoryPages = null },
                CustomPageLayout = new CustomPagesLayout
                {
                    Type = "3Column",
                    Columns = new List<CustomPagesColumn>
                {
                    new CustomPagesColumn { Sections = null },
                    new CustomPagesColumn { Sections = new List<CustomPagesSection>() },
                    new CustomPagesColumn { Sections = new List<CustomPagesSection>() }
                }
                }
            };

            return model;
        }

        /// <summary>
        /// Form Json For Custom Page add content
        /// </summary>
        /// <param name="title"> custom page title </param>
        /// <param name="isMasterPage"> true if is master, false by default </param>
        /// <param name="guid"> custom page GUID </param>
        /// <param name="contentSectionName"> content section name </param>
        /// <param name="categoryPages"> category pages list </param>
        /// <returns> json string </returns>
        private CustomPageModel FormCustomPageAddContentModel(string title, bool isMasterPage, string guid, string contentSectionName, List<string> categoryPages = null)
        {
            string customPageType = (isMasterPage == true) ? "mastercustompage" : "custompage.WestlawNext";

            var model = new CustomPageModel
            {
                CustomPageId = guid,
                CustomPageType = customPageType,
                CustomPageLabel = title,
                CustomPageStartPage = false,
                CustomPageValue = new CustomPagesValue
                {
                    Type = "3Column",
                    Columns = new List<CustomPagesColumn>
                        {
                          new CustomPagesColumn
                            {
                                Sections = new List<CustomPagesSection>
                                               {
                                                   new CustomPagesSection
                                                       {
                                                           Type = "categoryPageSection",
                                                           Label = contentSectionName,
                                                           Model = new CustomPagesSectionModel
                                                                       {
                                                                           CategoryPageUris = (categoryPages != null) ? this.GetCategoryPageUris(categoryPages) : null
                                                                       }
                                                       }
                                               }
                            },

                          new CustomPagesColumn
                            {
                                Sections = new List<CustomPagesSection>()
                            },

                          new CustomPagesColumn
                            {
                                Sections = new List<CustomPagesSection>()
                            }
                        }
                },
                CustomPageContent = new CustomPagesContent
                {
                    CustomPageCategoryPages = (categoryPages != null) ? this.GetCategoryPagesInfo(categoryPages) : null
                },
                CustomPageHasSearchableContent = false,
                CustomPageAllSelected = true,
                CustomPagesPermissions = new CustomPagesPermissions(),
                CustomPageAcl = new List<CustomPagesAclModel> { new CustomPagesAclModel() }
            };

            return model;
        }

        /// <summary>
        /// Get Category Pages Info (uri, label, isSearchable) for json body
        /// </summary>
        /// <param name="categoryPages"> category pages list </param>
        /// <returns> CustomPagesCategoryPages list </returns>
        private List<CustomPagesCategoryPages> GetCategoryPagesInfo(List<string> categoryPages)
        {
            List<object> categoryPageUris = this.GetCategoryPageUris(categoryPages);

            var list = new List<CustomPagesCategoryPages>();
            int q = 0;

            foreach (string uri in categoryPageUris)
            {
                var categoryPage = new CustomPagesCategoryPages
                {
                    Uri = categoryPageUris[q].ToString(),
                    Label = categoryPages[q],
                    IsSearchable = true
                };
                list.Add(categoryPage);
                q++;
            }

            return list;
        }

        /// <summary>
        /// Get Category Page Uris for json body
        /// </summary>
        /// <param name="categoryPages"> category pages list </param>
        /// <returns> list of strings </returns>
        private List<object> GetCategoryPageUris(List<string> categoryPages)
        {
            var categoryPageUris = new List<object>();
            categoryPages.ToList().ForEach(x => categoryPageUris.Add(x.Insert(0, "Home/").Replace(" ", string.Empty).Replace("&", string.Empty)));
            return categoryPageUris;
        }
    }
}
