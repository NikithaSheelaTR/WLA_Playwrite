namespace Framework.Common.UI.Products.WestLawNextCanada.Pages.HomePage
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.Document.RI;
    using Framework.Common.UI.Products.Shared.Components.KeyNumber;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.WestlawEdge.Components.Toolbar;
    using Framework.Common.UI.Products.WestLawNextCanada.Components;
    using Framework.Common.UI.Products.WestLawNextCanada.Components.HomePage;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Wlnc HomePage Tests
    /// </summary>
    public class WlncHomePage : CommonSearchHomePage
    {
        string letter;
        private static readonly By LinkOutBoxLocator = By.XPath("//*[@id='co_linkOutBox']");
        private static readonly By SubHeaderNameLocator = By.XPath("//*[@id='co_browsePageLabel']");
        private static readonly By DocCitationLocator = By.XPath("//*[@id= 'co_docHeaderCitation']");
        private static readonly By CanadaTrainingPageTitleLocator = By.XPath("//p[@class='hero-lead rebranding']");
        private static readonly By CoCiteLocator = By.ClassName("co_cites");
        private static readonly By WelcomeDialogLocator = By.XPath("//*[@id= 'co_welcomeCenterLightbox']");
        private static readonly By ParentNodeLocator = By.CssSelector("div.co_browseTocHeading");
        private static readonly By ChildNodeLocator = By.XPath("//ul[starts-with(@class, 'co_browseContent')]/li");
        private static readonly By ChildNodeCheckBoxLocator = By.XPath("//ul[starts-with(@class, 'co_browseContent')]//input");
        private static readonly By ChildNodeLinkLocator = By.XPath("//*[@class=' co_tocItemLink ']//span");
        private static readonly By ParentNodesWithCheckBoxLocator = By.XPath("//*[@class= 'co_browseTocHeading ']/input");
        private static readonly By LetterLinkLocator = By.XPath("//ul[@id='coid_website_indexCharacterLinks']//a");
        private static readonly By IndexLetterLinkLocator = By.XPath("//*[@id='co_tocItemLinks_0']//a");
        private static readonly By DisabledLetterLinkLocator = By.XPath("//ul[@id='coid_website_indexCharacterLinks']//a[@class='co_disabled']");
        private static readonly By WordLinkLocator = By.XPath("//ul[@class='co_browseContent']//li//a");
        private static readonly By AlphabetHeaderLinkLocator = By.XPath("(.//ul[@class='co_inlineList']/li/a[@href='#co_anchor_index_navigation_E' and text()='E'])[2]");
        private static readonly By MainHeaderLocator = By.XPath("//*[@id='co_browseWidgetTabPanel1']//h2");
        private static readonly By TocLinkLocator = By.CssSelector("a.co_tocItemLink");
        private static readonly By SearchWithinTermLocator = By.CssSelector(".co_searchWithinTerm");
        private static readonly By LatestUpdatesLabelLocator = By.XPath("//div[contains(@class,'coid_website_canadaNewsletterWidget')]//p");
        private static readonly By ViewUpdatesLinkLocator = By.XPath("//a[contains(@id,'coid_view_updates') or contains(@id,'coid_voir_les_derni')]");
        private static readonly By BrowseLegalTopicLinkLocator = By.ClassName("canadian_abridgment_key");
        private static readonly By MySubscriptionsLabelLocator = By.XPath("//div[@id='crsw_sourceSelectorHeader']//span");
        private const string WordLinkIdLctMask = "//*[@id='{0}']";
        private const string RandomOptionLinkLctMask = "//div[contains(@id,'co_anchor')]/div[starts-with(@id,'{0}')]";
        private const string RandomLetterLinkLctMask = "//div[@id='block_{0}']";

        /// <summary>
        /// Toolbar component
        /// </summary>
        public EdgeToolbarComponent Toolbar { get; } = new EdgeToolbarComponent();

        /// <summary>
        /// RelatedInfo Tabs
        /// </summary>
        public RelatedInfoTabComponent RiTabs { get; } = new RelatedInfoTabComponent();

        /// <summary>
        /// Title Search component
        /// </summary>
        public TitleSearchComponent TitleSearchComponent { get; } = new TitleSearchComponent();

        /// <summary>
        /// Get the Browse Component
        /// </summary>
        public new CanadaBrowseTabPanel BrowseTabPanel { get; } = new CanadaBrowseTabPanel();

        /// <summary>
        /// Folders Widget on the right hand side
        /// </summary>
        public FoldersComponent FoldersComponent { get; } = new FoldersComponent();

        /// <summary>
        /// Canada Edge Footer Component
        /// </summary>
        public CanadaNextFooterComponent CanadaFooter { get; } = new CanadaNextFooterComponent();

        /// <summary>
        /// LinkOutboxLabel
        /// </summary>
        public ILabel LinkOutboxLabel => new Label(LinkOutBoxLocator);

        /// <summary>
        /// CitationLabel 
        /// </summary>
        public ILabel CitationLabel => new Label(DocCitationLocator);

        /// <summary>
        /// HelpPageLabel
        /// </summary>
        public ILabel HelpPageLabel => new Label(CanadaTrainingPageTitleLocator);

        /// <summary>
        /// CoCitesDocLabel
        /// </summary>
        public ILabel CoCitesDocLabel => new Label(CoCiteLocator);

        /// <summary>
        /// SubHeaderLabel
        /// </summary>
        public ILabel SubHeaderLabel => new Label(SubHeaderNameLocator);

        /// <summary>
        /// Latest Updates Right pane label
        /// </summary>
        public ILabel LatestUpdatesLabel { get; } = new Label(LatestUpdatesLabelLocator);

        /// <summary>
        /// View Updates Link
        /// </summary>
        public ILink ViewUpdatesLink { get; } = new Link(ViewUpdatesLinkLocator);

        /// <summary>
        /// Browse Legal Topics Link
        /// </summary>
        public ILink BrowseLegalTopicsLink { get; } = new Link(BrowseLegalTopicLinkLocator);

        /// <summary>
        /// My Subscriptions label
        /// </summary>
        public ILabel MySubscriptionsLabel { get; } = new Label(MySubscriptionsLabelLocator);

        /// <summary>
        /// return ChildNodeLabel
        /// </summary>
        public IReadOnlyCollection<ILabel> ChildNodeLabel => new ElementsCollection<Label>(ChildNodeLocator);

        /// <summary>
        /// MainHeaderLabel
        /// </summary>
        public IReadOnlyCollection<ILabel> MainHeaderLabel => new ElementsCollection<Label>(MainHeaderLocator).ToList();

        /// <summary>
        /// ParentNodeLabel
        /// </summary>
        public IReadOnlyCollection<ILabel> ParentNodeLabel => new ElementsCollection<Label>(ParentNodeLocator);
                             
        /// <summary>
        /// ChildNodesLinks
        /// </summary>    
        public IReadOnlyCollection<ILink> ChildNodeLink => new ElementsCollection<Link>(ChildNodeLinkLocator);

        /// <summary>
        /// WillisIndexLetterLink
        /// </summary>
        public IReadOnlyCollection<ILink> WillisIndexLetterLink => new ElementsCollection<Link>(IndexLetterLinkLocator);

        /// <summary>
        /// TocLink
        /// </summary>
        public IReadOnlyCollection<ILink> TocLink => new ElementsCollection<Link>(TocLinkLocator);

        /// <summary>
        /// LetterLink
        /// </summary>
        public IReadOnlyCollection<ILink> LetterLink => new ElementsCollection<Link>(LetterLinkLocator);

        /// <summary>
        /// DisabledLetterLink
        /// </summary>
        public IReadOnlyCollection<ILink> DisabledLetterLink => new ElementsCollection<Link>(DisabledLetterLinkLocator);

        /// <summary>
        /// DocumentLetterLink
        /// </summary>
        public ILink DocumentLetterLink => new Link(By.XPath(string.Format(RandomLetterLinkLctMask, letter) + "//a"));

        /// <summary>
        /// WordLink
        /// </summary>
        public IReadOnlyCollection<ILink> WordLink => new ElementsCollection<Link>(WordLinkLocator);

        /// <summary>
        /// AlphabetHeaderLink
        /// </summary>
        public ILink AlphabetHeaderLink => new Link(AlphabetHeaderLinkLocator);

        /// <summary>
        /// ParentNodeCheckBox
        /// </summary>
        public IReadOnlyCollection<ICheckBox> ParentNodeCheckBox => new ElementsCollection<CheckBox>(ParentNodesWithCheckBoxLocator);

        /// <summary>
        /// ChildNodeCheckBox
        /// </summary>
        public IReadOnlyCollection<ICheckBox> ChildNodeCheckBox => new ElementsCollection<CheckBox>(ChildNodeCheckBoxLocator);

        /// <summary>
        /// SearchWithinTermTextBox
        /// </summary>
        public ITextbox SearchWithinTermTextBox => new Textbox(SearchWithinTermLocator);

        /// <summary>
        /// verifies Dialog is displayed
        /// </summary>
        public bool IsWelcomeBoxDialogDisplayed() => DriverExtensions.IsDisplayed(WelcomeDialogLocator, 5);

        /// <summary>
        /// Verifies random letter visible in viewport
        /// </summary>
        public bool IsRandomLetterVisible() => DriverExtensions.GetElement(By.XPath(string.Format(RandomLetterLinkLctMask, letter))).IsElementInView();

        /// <summary>
        /// Verifies if WordLinkId is in view port
        /// </summary>
        /// <param name="wordlinkId"> the wordlinks from document </param>
        public bool IsWordLinkIdInView(string wordlinkId) => DriverExtensions.GetElement(By.XPath(string.Format(WordLinkIdLctMask, wordlinkId))).IsElementInView();

        /// <summary>
        /// returns random Letters Links
        /// </summary>
        public ILink GetRandomLetterLink()
        {
            bool isLetterDisabled;
            ILink randomLetterNumberLink;

            do
            {
                isLetterDisabled = false;
                randomLetterNumberLink = LetterLink.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                letter = randomLetterNumberLink.Text;
                foreach (var item in DisabledLetterLink)
                {
                    if (letter == item.Text)
                    {
                        isLetterDisabled = true;
                        break;
                    }
                }
            } while (isLetterDisabled);

            return randomLetterNumberLink;
        }
       
        /// <summary>
        /// returns random letter from widget
        /// </summary>
        /// <param name="option"> the wordlinks from document </param>
        public ILink GetLetterLink(string option) => new Link(By.XPath(string.Format(RandomOptionLinkLctMask, option)));

        /// <summary>
        /// returns ChildLinkfromLetterLink
        /// </summary>
        /// <param name="option"> the wordlinks from document </param>
        public IReadOnlyCollection<ILink> GetFirstChildLinkfromLetterLink(string option) => new ElementsCollection<Link>(By.XPath(string.Format(RandomOptionLinkLctMask, option) + "//following-sibling::div//a"));            
                     
        /// <summary>
        /// returns RGB color value
        /// </summary>
        /// <param name="colorText"> the wordlinks from document </param>
        public string GetColorValue(string colorText)
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